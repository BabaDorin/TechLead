using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechLead.Models;
using System.IO;
using PagedList;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TechLead.Controllers
{

    public class ProblemController : Controller
    {
        public static Data data = new Data();
        private static ApplicationDbContext _context;
        private static UserManager<ApplicationUser> _userManager;

        public ProblemController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                Exercise e = _context.Exercises.Single(ex => ex.Id == id);
                ExerciseViewModel EVM = ExerciseFromModelToViewModel(e,false);
                EVM.MakeSourceCodePublic = true;
                TempData["Object"] = e; 

                return View(EVM);
            }
            catch (Exception)
            {
                ErrorViewModel Error = new ErrorViewModel();
                Error.Title = "Error";
                Error.Description = "We could not find the exercise specified";
                return View("~/Views/Shared/Error.cshtml", Error);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Details(HttpPostedFileBase file, ExerciseViewModel EVM)
        {
            if (file == null)
            {
                ErrorViewModel Error = new ErrorViewModel();
                Error.Title = "Error";
                Error.Description = "You did not upload any solution ):";
                return View("~/Views/Shared/Error.cshtml", Error);
            }
            try
            {
                Debug.WriteLine("EVM HERE! " + EVM.MakeSourceCodePublic);
                //We try to extract the source code in a string and pass it to Judge0 API
                Judge0_SubmissionViewModel submission = new Judge0_SubmissionViewModel();
                submission.source_code = new StreamReader(file.InputStream).ReadToEnd();
                submission.language_id = LanguageId(file.FileName);

                if (submission.language_id == -1)
                {
                    //Unsupported language
                    ErrorViewModel Error = new ErrorViewModel();
                    Error.Title = "Unsupported language :(";
                    Error.Description = "Supported languages: C++ (.cpp), C# (.cs), Java (.java), Python (.py), Pascal (.pas)";
                    return View("~/Views/Shared/Error.cshtml", Error);
                }

                TempData["Judge0_Submission"] = submission;

                return RedirectToAction("Compiling", "Problem", new { EVM.MakeSourceCodePublic });

            }
            catch (Exception e)
            {
                ErrorViewModel Error = new ErrorViewModel();
                Error.Title = "Oops, something happened :(";
                Error.Description = e.Message;
                return View("~/Views/Shared/Error.cshtml", Error);
            }
        }

        [Authorize]
        public ActionResult Compiling(bool MakeSourceCodePublic)
        {
            try
            {
                Debug.WriteLine("Getting data about user");
                string userId = User.Identity.GetUserId();
                ApplicationUser user = _context.Users.Find(userId);
                string usersBestSubmissions = user.BestSubmisions;
                Debug.WriteLine("Done");
                Debug.WriteLine("From string to array");
                BestSubmission[] bestSubmission = data.ConvertBestSubmissionFromStringToArray(usersBestSubmissions);
                Debug.WriteLine("Done");

                Exercise e = TempData["Object"] as Exercise;
                Judge0_SubmissionViewModel judge0_submission = TempData["Judge0_Submission"] as Judge0_SubmissionViewModel;

                //CompileAndTest() return the submission containing performance results for each test case
                //It is being saved to the database.
                //After that, based on the submission object submissionViewModel is created and passed to the view
                //so the user will see his results.
                Submission submission = CompileAndTest(e, judge0_submission);
                submission.MakeSourceCodePublic = MakeSourceCodePublic;
                _context.Submissions.Add(submission);
                _context.SaveChanges();

                Debug.WriteLine("Data");
                double PointsToAddToUsersTotalPoints = 0;
                string ModifiedUsersBestSubmissions;
                if (SubmissionIsInserted(e.Id, bestSubmission))
                {
                    UpdateBestSubmission(ref bestSubmission, submission, ref PointsToAddToUsersTotalPoints);
                    ModifiedUsersBestSubmissions = data.ConvertBestSubmissionFromArrayToString(bestSubmission);
                }
                else
                {
                    InsertBestSubmission(ref usersBestSubmissions, submission, e.Points, ref PointsToAddToUsersTotalPoints);
                    ModifiedUsersBestSubmissions = usersBestSubmissions;
                }
                Debug.WriteLine("Done");
                //Store the new value of totalPoints and BestSubmission;
                Debug.WriteLine("update data in db");
                user.TotalPoints += PointsToAddToUsersTotalPoints;
                user.BestSubmisions = ModifiedUsersBestSubmissions;
                _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                Debug.WriteLine("done");

                return RedirectToAction("SubmissionDetails", "Problem", new { id = submission.SubmissionID });
            }
            catch (Exception e)
            {
                //throw;
                ErrorViewModel Error = new ErrorViewModel();
                Error.Title = "Oops, something happened :(";
                Error.Description = e.Message;
                return View("~/Views/Shared/Error.cshtml", Error);
            }
        }
        
        public Submission CompileAndTest(Exercise e, Judge0_SubmissionViewModel judge0_Submission)
        {
            try
            {
                //Inserting basic data into submission object
                Submission submission = new Submission();
                if (Request.IsAuthenticated)
                {
                    submission.SubmissionAuthorUserName = HttpContext.User.Identity.Name;
                }
                else
                {
                    submission.SubmissionAuthorUserName = "Anonymous";
                }
                submission.Date = DateTime.Now;
                submission.ExerciseId = e.Id;
                submission.Exercise = e.Name;
                submission.ScoredPoints = 0;
                submission.NumberOfTestCases = e.NumberOfTests;
                submission.DistributedPointsPerTestCase = (double)e.Points / e.NumberOfTests;
                submission.SourceCode = judge0_Submission.source_code;
                submission.InputCollection = e.InputColection;
                submission.ExpectedOutput = e.OutputColection;
                submission.OutputCollection = string.Empty;
                submission.PointsPerTestCase = string.Empty;
                submission.ExecutionTimePerTestCase = string.Empty;
                submission.StatusPerTestCase = string.Empty;
                submission.ErrorMessage = string.Empty;

                Debug.WriteLine("The problem has {0} tests, {1} points per test case ", 
                    submission.NumberOfTestCases, submission.DistributedPointsPerTestCase);
                //Now we have to go through each test case, collect data, analyse and
                //build step by step PoinsPerTestCase, ExecutionTimePerTestCase, StatusPerTestCase,
                //ErrorMessage
                Test[] TestCases = data.CreateTests(e.InputColection, e.OutputColection);

                for (int i = 0; i < TestCases.Length; i++)
                {
                    Debug.WriteLine("Test " + i);
                    double Points = 0;
                    int ExecutionTime = 0;
                    int Memory = 0;
                    string Status = string.Empty;
                    string Error = string.Empty;
                    Debug.WriteLine("Going into go through test case");
                    GoThroughTestCase(TestCases[i], ref Points, ref ExecutionTime, e.ExecutionTime, ref Memory, e.MemoryLimit, ref Status, ref Error, judge0_Submission.language_id, judge0_Submission.source_code);

                    //Now we add the results to submission object

                    //Check if Point == 1 it means that the solutition returned the corect answer, so the points are given
                    //Otherwise, Point == 0 => Something is wrong with the solution submitted.
                    submission.ScoredPoints += Points*submission.DistributedPointsPerTestCase;
                    submission.PointsPerTestCase += (Points == 1) ? submission.DistributedPointsPerTestCase.ToString() : "0";
                    submission.ExecutionTimePerTestCase += ExecutionTime.ToString();
                    submission.StatusPerTestCase += Status;
                    submission.ErrorMessage += Error;

                    if (i < TestCases.Length - 1)
                    {
                        submission.PointsPerTestCase += data.Delimitator;
                        submission.ExecutionTimePerTestCase += data.Delimitator;
                        submission.StatusPerTestCase += data.Delimitator;
                        submission.ErrorMessage += data.Delimitator;
                    }

                }

                return submission;
            }
            catch (Exception ess)
            {
                Debug.WriteLine("Exception in Compile and Test => [Maybe it's from API] => " + ess);
                throw ess;
            }
        }

        public void GoThroughTestCase(Test test, ref double Points, ref int ExecutionTimeMs, int ExecutionTimeLimit, ref int MemoryUsed, int MemoryLimit,
            ref string Status, ref string Error, int langID, string sourceCode)
        {
            try
            {
                //Function [GetToken()] returns a json (string formatted) having the token.
                //It will be parsed to json by using JObject.Parse();
                string token = GetToken(test, langID, sourceCode).Result;
                if (token == null)
                {
                    throw new NotImplementedException();
                }

                //Function that returns a json (string formatted) containing the result after running the solution
                JObject result;

                //It takes time for the API to process the data. This do while stays here to make repetitive calls to the API
                //Checking everytime the response. If the status is different from 'In Queue' or 'Processing', it means
                //that the code has been compiled and it returned the response having the results we are looking for.
                do
                {
                    result = JObject.Parse(GetResult(token));

                    //Checking out if our submitted solution has been processed
                    //To not overload the API and to make multiple calls in vain, we use thread.Sleep,
                    //So it waits 100 miliseconds before sending another get request.
                    if (result.SelectToken("status.description").ToString() == "In Queue" ||
                            result.SelectToken("status.description").ToString() == "Processing")
                    {
                        System.Threading.Thread.Sleep(100);
                    }

                } while (result.SelectToken("status.description").ToString() == "In Queue" ||
                              result.SelectToken("status.description").ToString() == "Processing");

                //Now we have the result in a json format, so we are able to insert necessary data.
                // --- 
                //Execution time (json contains a float valus (seconds) but it is being parsed to miliseconds)
                if (result.SelectToken("time") != null)
                    ExecutionTimeMs = (int)((double)result.SelectToken("time")*1000);

                //Status (Accepted, denied etc.)
                Status = (string)result.SelectToken("status.description");

                //Memory used (in kylobites)
                MemoryUsed = int.Parse(result.SelectToken("memory").ToString());

                //Now we check if the program used the right amount of memory (Less or equal to memory limit)
                //For the first we check if the current exercise has some time and memory constrains.
                int MemoryLimitLocal = (MemoryLimit == 0) ? int.MaxValue : MemoryLimit;
                int ExecutionTimeLimitLocal = (ExecutionTimeLimit == 0) ? int.MaxValue : ExecutionTimeLimit;

                if (MemoryUsed > MemoryLimitLocal)
                {
                    //if the program used too much memory
                    bool correctOutput = (test.Output == (string)result.SelectToken("stdout"));
                    Error = "Your program had used too much memory :(";

                    if (ExecutionTimeMs > ExecutionTimeLimitLocal)
                        Error += "\nand it needs too much time to run :(";

                    if (correctOutput) Error += "\nBut the output was correct :)";
                    Points = 0;
                    return;
                }
                else
                {
                    if (ExecutionTimeMs > ExecutionTimeLimitLocal)
                    {
                        //if program's execution needed too much time
                        Error = "Your program needs too much time to run :(";
                        bool correctOutput = (test.Output == (string)result.SelectToken("stdout"));
                        if (correctOutput) Error += "\nBut the output was correct :)";
                        Points = 0;
                        return;
                    }

                    //If we are here, it means that everything is OK with execution time and used memory
                    //This situation occurs when the was not executed, was executed but with errors,
                    //was executed but the result was incorrect or it was executed and the output is correct.
                    Points = (test.Output == (string)result.SelectToken("stdout")) ? 1 : 0;
                    Error = (string)result.SelectToken("compile_output");

                    //If the current output does not match with the correct one, it means that Points = 0 and
                    //there is no Error message inserted into that variable called Error
                    if (Points==0 && Error.Length < 3)
                    {
                        Error = "Incorrect Output";
                    }
                }
            }
            catch (NotImplementedException)
            {
                //This happens when the API has been modified or shut down or whatever.
                Debug.WriteLine("Exception in GoThroughTestCase => The token is null");
                throw new Exception();
            }
            catch(Exception e)
            {
                Debug.WriteLine("Exception in GoThroughTestCase => " + e);
                throw e;
            }
        }

        public string GetResult(string token)
        {
            try
            {
                string result;

                //building the request and passing the parameters we are looking for.
                var request = (HttpWebRequest)WebRequest.Create("https://api.judge0.com/submissions/" + token + "?base64_encoded=false&fields=stdout,stderr,status_id,language_id,compile_output,stdin,message,status,time,memory");
                request.ContentType = "application/json";
                request.Method = "GET";

                //Sending the request and reading the result
                var httpResponse = (HttpWebResponse)request.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception in GetResult => " + e);
                throw e;
            }
        }

        async Task<string> GetToken(Test test, int langID, string SourceCode)
        {
            try
            {
                //The method sends HTTP requests to judge0 API, then, it gets a token as a response.
                //After that, having the token, we make another request to get submission details like execution time and so on.

                //Building the judge0 submission, which will be sent via request
                Judge0JsonModel jsonModel = new Judge0JsonModel();
                jsonModel.source_code =  data.Base64Encode(SourceCode);
                jsonModel.stdin = data.Base64Encode(test.Input);
                jsonModel.language_id = langID;

                //Sending the request
                Debug.WriteLine("Sending the request");
                JObject response;
                
                var json = JsonConvert.SerializeObject(jsonModel);
                var dataToSend = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "https://api.judge0.com/submissions/?base64_encoded=true&wait=false";
                string res;

                //Sending the request and storing the data being returned
                using (var client = new HttpClient())
                {
                    using (HttpResponseMessage resp = await client.PostAsync(url, dataToSend).ConfigureAwait(false))
                    {
                        using (HttpContent content = resp.Content)
                        {
                            res = await content.ReadAsStringAsync().ConfigureAwait(false);
                            Debug.WriteLine(res);
                        }
                    }
                }

                response = JObject.Parse(res);
                return response.SelectToken("token").ToString();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception in GetToken => " + e);
                return null;
            }
        }

        public int LanguageId(string fileName)
        {
            try
            {
                switch (Path.GetExtension(fileName))
                {
                    case ".cs":
                        return 51;
                    case ".cpp":
                        return 53;
                    case ".pas":
                        return 67;
                    case ".java":
                        return 62;
                    case ".py":
                        return 71;
                    default: return -1;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exeption in LanguageId => " + e);
                throw e;
            }
        }

        public ActionResult SubmissionDetails(int id)
        {
            try
            {
                Submission S = _context.Submissions.Single(sub => sub.SubmissionID == id);
                SubmissionViewModel submissionViewModel = SubmissionFromModelToViewModel(S);
                return View(submissionViewModel);
            }
            catch (Exception)
            {
                ErrorViewModel Error = new ErrorViewModel();
                Error.Title = "Error";
                Error.Description = "We could not find the submission specified. Sorry ):";
                return View("~/Views/Shared/Error.cshtml", Error);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            try
            {
                var viewModel = new ExerciseViewModel
                {
                    Difficulty = _context.Difficulty.ToList()
                };
                viewModel.Test = new Test[10];
                return View(viewModel);
            }
            catch (Exception e)
            {
                ErrorViewModel error = new ErrorViewModel
                {
                    Title = "Error",
                    Description = e.ToString()
                };
                return View("~/Views/Shared/Error.cshtml", error);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(ExerciseViewModel exerciseViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    exerciseViewModel.Difficulty = _context.Difficulty.ToList();
                    return View("Create", exerciseViewModel);
                }
                int nrOfTests = 0;
                for (int i = 0; i < 10; i++)
                {
                    if (exerciseViewModel.Test[i].Input != null && exerciseViewModel.Test[i].Input != null)
                    {
                        nrOfTests++;
                    }
                }
                if (nrOfTests == 0)
                {
                    TempData["BackEndTestsNeeded"] = "<script>alert('At leas one backend test case needed');</script>";
                    exerciseViewModel.Difficulty = _context.Difficulty.ToList();
                    return View("Create", exerciseViewModel);
                }

                //If everything is OK, we copy all the data from ExerciseViewModel to Exercise

                if (Request.IsAuthenticated)
                {
                    exerciseViewModel.Author = User.Identity.Name;
                }
                Exercise exercise = ExerciseFromViewModelToModel(exerciseViewModel);
                exercise.AuthorID = User.Identity.GetUserId();
                _context.Exercises.Add(exercise);
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = exercise.Id });
            }
            catch (Exception e)
            {
                ErrorViewModel error = new ErrorViewModel
                {
                    Title = "Error",
                    Description = "Something happened. The problem was not saved.\n Details: " + e.ToString()
                };
                return View("~/Views/Shared/Error.csthml", error);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Update(int ProblemID)
        {
            try
            {
                //Display this page only to the creator of that specific problem, or
                //to an admin
                Exercise E = _context.Exercises.Single(ex => ex.Id == ProblemID);
                var userId = User.Identity.GetUserId();
                if (userId == E.AuthorID || CurrentUser_Administrator())
                {
                    ExerciseViewModel EVM = ExerciseFromModelToViewModel(E,true);
                    EVM.Id = ProblemID;
                    return View(EVM);
                }
                else
                {
                    ErrorViewModel Error = new ErrorViewModel();
                    Error.Title = "Error 404";
                    Error.Description = "Page not found :(";
                    return View("~/Views/Shared/Error.cshtml", Error);
                }
            }
            catch (Exception)
            {
                ErrorViewModel Error = new ErrorViewModel();
                Error.Title = "Error";
                Error.Description = "Unfortunately, something happened. The problem has not been modified. Try again.";
                return View("~/Views/Shared/Error.cshtml", Error);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Update(ExerciseViewModel exerciseViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    exerciseViewModel.Difficulty = _context.Difficulty.ToList();
                    return View("Update", exerciseViewModel);
                }

                //There must be at least one Back-End test case
                int nrOfTests = 0;
                for (int i = 0; i < 10; i++)
                {
                    if (exerciseViewModel.Test[i].Input != null && exerciseViewModel.Test[i].Input != null)
                    {
                        nrOfTests++;
                    }
                }

                if (nrOfTests == 0)
                {
                    TempData["BackEndTestsNeeded"] = "<script>alert('At leas one backend test case needed');</script>";
                    exerciseViewModel.Difficulty = _context.Difficulty.ToList();
                    return View("Update", exerciseViewModel);
                }

                //If everything is OK, the exercise will get updated

                Exercise exercise = ExerciseFromViewModelToModel(exerciseViewModel);
                exercise.AuthorID = HttpContext.User.Identity.GetUserId();
                exercise.Author = HttpContext.User.Identity.Name;
                _context.Entry(exercise).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = exercise.Id });
            }
            catch (Exception e)
            {
                ErrorViewModel error = new ErrorViewModel
                {
                    Title = "Error",
                    Description = "Your problem has not been modified. \nDetails: " + e.ToString()
                };
                return View("~/Views/Shared/Error.cshtml", error);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int ProblemID)
        {
            //Redirect the user to another page like => Do you really want to delete this problem?
            // Yes (Submit type button) and back

            //An extra safety mesure
            Exercise E = _context.Exercises.Single(e => e.Id == ProblemID);
            if ((User.Identity.IsAuthenticated && User.Identity.GetUserId() == E.AuthorID) || User.IsInRole("Administrator"))
            {
                DeleteProblemViewModel deleteProblemViewModel = new DeleteProblemViewModel
                {
                    Id = E.Id,
                    Name = E.Name
                };
                return View(deleteProblemViewModel);
            }

            //If gone so far, something is wrong
            ErrorViewModel error = new ErrorViewModel
            {
                Title = "Error",
                Description = "How do you even get here?"
            };
            return View("~/Views/Shared/Error.cshtml", error);
        }

       [HttpPost]
       public ActionResult Delete(DeleteProblemViewModel deleteProblemViewModel)
        {
            //Deleting an exercise means setting the IsArchieved property to true.
            Exercise E = _context.Exercises.Single(e => e.Id == deleteProblemViewModel.Id);
            _context.Exercises.Remove(E);
            _context.SaveChanges();
            return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult Submissions(int? page)
        {
            Exercise e = TempData["Object"] as Exercise;
            int ExerciseIdParam = e.Id;
            TempData.Keep();
            List<Submission> SubmissionForASpecificExercise = new List<Submission>();
            foreach (Submission S in _context.Submissions)
                if (S.ExerciseId == ExerciseIdParam)
                {
                    SubmissionForASpecificExercise.Add(S);
                }
            SubmissionForASpecificExercise.Reverse();
            return View(SubmissionForASpecificExercise.ToList().ToPagedList(page ?? 1, 40));
        }

        private Exercise ExerciseFromViewModelToModel(ExerciseViewModel ExerciseViewModel)
        {
            Exercise e = new Exercise();
            e.Id = ExerciseViewModel.Id;
            e.isArchieved = ExerciseViewModel.isArchieved;
            e.Author = ExerciseViewModel.Author;
            e.AuthorID = ExerciseViewModel.AuthorID;
            e.Name = ExerciseViewModel.Name;
            e.Points = ExerciseViewModel.Points;
            e.Condition = ExerciseViewModel.Condition;
            e.Constraints = ExerciseViewModel.Constraints;
            e.Difficulty = ExerciseViewModel.Difficulty;
            e.DifficultyId = ExerciseViewModel.DifficultyId;
            e.Input1 = ExerciseViewModel.Input1;
            e.Input2 = ExerciseViewModel.Input2;
            e.Input3 = ExerciseViewModel.Input3;
            e.Input4 = ExerciseViewModel.Input4;
            e.Input5 = ExerciseViewModel.Input5;
            e.Output1 = ExerciseViewModel.Output1;
            e.Output2 = ExerciseViewModel.Output2;
            e.Output3 = ExerciseViewModel.Output3;
            e.Output4 = ExerciseViewModel.Output4;
            e.Output5 = ExerciseViewModel.Output5;
            e.Explanation1 = ExerciseViewModel.Explanation1;
            e.Explanation2 = ExerciseViewModel.Explanation2;
            e.Explanation3 = ExerciseViewModel.Explanation3;
            e.Explanation4 = ExerciseViewModel.Explanation4;
            e.Explanation5 = ExerciseViewModel.Explanation5;
            e.OutputFormat = ExerciseViewModel.OutputFormat;
            e.InputFormat = ExerciseViewModel.InputFormat;

            //This AuxTests array will contain all the tests, without empty imputs.
            //This is the most safe way and we can be sure 100% that to the database will go only valid data.
            Test[] AuxTests = new Test[10];
            e.NumberOfTests = 0;
            for (int i = 0; i < 10; i++)
            {
                if (ExerciseViewModel.Test[i].Input != null && ExerciseViewModel.Test[i].Input != null)
                {
                    AuxTests[e.NumberOfTests] = ExerciseViewModel.Test[i];
                    ++e.NumberOfTests;
                }
            }

            for (int i = 0; i < e.NumberOfTests; i++)
            {
                e.InputColection += AuxTests[i].Input;
                if (i < e.NumberOfTests - 1) e.InputColection += data.Delimitator;

                e.OutputColection += AuxTests[i].Output;
                if (i < e.NumberOfTests - 1) e.OutputColection += data.Delimitator;
            }
            Debug.WriteLine(e.InputColection);
            Debug.WriteLine(e.OutputColection);
            //Note:  Input and Ouput collection properties will contail all the inputs / outputs for the backend
            //processing having a delimitator between them. Everytime when the list of test will be needed,
            //it would be accesibile by calling the data.CreateTests and passing the e.InputCollection and e.OutputCollection.
            return e;
        }
        private SubmissionViewModel SubmissionFromModelToViewModel(Submission submission)
        {
            SubmissionViewModel Svm = new SubmissionViewModel();
            Svm.SubmissionAuthorUserName = submission.SubmissionAuthorUserName;
            Svm.SubmissionID = submission.SubmissionID;
            Svm.Date = submission.Date;
            Svm.ExerciseId = submission.ExerciseId;
            Svm.Exercise = submission.Exercise;
            Svm.ScoredPoints = submission.ScoredPoints;
            //Svm.NumberOfTestCases = submission.NumberOfTestCases;
            Svm.DistributedPointsPerTestCase = submission.DistributedPointsPerTestCase;
            Svm.SourceCode = submission.SourceCode;
            Svm.Inputs = submission.InputCollection.Split(new string[] { data.Delimitator }, StringSplitOptions.None);
            Svm.Outputs = submission.OutputCollection.Split(new string[] { data.Delimitator }, StringSplitOptions.None);
            Svm.ExpectedOutputs = submission.ExpectedOutput.Split(new string[] { data.Delimitator }, StringSplitOptions.None);
            Svm.Points = Array.ConvertAll(submission.PointsPerTestCase.Split(new string[] { data.Delimitator }, StringSplitOptions.None),
                x => double.Parse(x));
            Svm.ExecutionTime = Array.ConvertAll(submission.ExecutionTimePerTestCase.Split(new string[] { data.Delimitator }, StringSplitOptions.None),
                x => int.Parse(x));
            Svm.Status = submission.StatusPerTestCase.Split(new string[] { data.Delimitator }, StringSplitOptions.None);
            Svm.ErrorMessage = submission.ErrorMessage.Split(new string[] { data.Delimitator }, StringSplitOptions.None);
            Svm.NumberOfTestCases = Svm.Points.Length;
            Svm.MakeSourceCodePublic = submission.MakeSourceCodePublic;
            return Svm;
        }
        public ExerciseViewModel ExerciseFromModelToViewModel(Exercise exercise, bool CopyBackendTests)
        {
            var EVM = new ExerciseViewModel
            {
                Difficulty = _context.Difficulty.ToList()
            };
            EVM.Id = exercise.Id;
            EVM.isArchieved = exercise.isArchieved;
            EVM.Name = exercise.Name;
            EVM.Points = exercise.Points;
            EVM.SubmissionsAbove10Points = exercise.SubmissionsAbove10Points;
            EVM.SubmissionsUnder10Points = exercise.SubmissionsUnder10Points;
            EVM.Author = exercise.Author;
            EVM.AuthorID = exercise.AuthorID;
            EVM.DifficultyId = exercise.DifficultyId;
            EVM.Condition = exercise.Condition;
            EVM.InputFormat = exercise.InputFormat;
            EVM.OutputFormat = exercise.OutputFormat;
            EVM.Constraints = exercise.Constraints;
            EVM.Input1 = exercise.Input1;
            EVM.Input2 = exercise.Input2;
            EVM.Input3 = exercise.Input3;
            EVM.Input4 = exercise.Input4;
            EVM.Input5 = exercise.Input5;
            EVM.Output1 = exercise.Output1;
            EVM.Output2 = exercise.Output2;
            EVM.Output3 = exercise.Output3;
            EVM.Output4 = exercise.Output4;
            EVM.Output5 = exercise.Output5;
            EVM.Explanation1 = exercise.Explanation1;
            EVM.Explanation2 = exercise.Explanation2;
            EVM.Explanation3 = exercise.Explanation3;
            EVM.Explanation4 = exercise.Explanation4;
            EVM.Explanation5 = exercise.Explanation5;
            EVM.Test = new Test[10];

            //There is no need to send these "secret" tests to "Details" view.
            if (CopyBackendTests == true)
            {
                Test[] aux = data.CreateTests(exercise.InputColection, exercise.OutputColection);

                for (int i = 0; i < aux.Length; i++)
                {
                    EVM.Test[i] = aux[i];
                }
            }

            return EVM;
        }
        public ActionResult RenderError(ErrorViewModel Err)
        {
            return View("~/Views/Shared/Error.cshtml", Err);
        }

        public void InsertBestSubmission(ref string bestsubmission, Submission submission, double TotalPoints, ref double AddToUsersTotalPoints)
        {
            if (bestsubmission == null) bestsubmission = "";
            string LastSubmission = submission.ExerciseId + data.Delimitator + submission.Exercise + data.Delimitator + submission.SubmissionID + data.Delimitator +
                +TotalPoints + data.Delimitator + submission.ScoredPoints;
            AddToUsersTotalPoints = submission.ScoredPoints;

            //This string should not begin with a delimitator.
            if (bestsubmission.Length>0)
            {
                bestsubmission += data.Delimitator;
            }

            bestsubmission += LastSubmission;
        }

        public bool SubmissionIsInserted(int exerciseID, BestSubmission[] bestSubmissions)
        {
            if (bestSubmissions == null) return false;
            for(int i=0; i<bestSubmissions.Length; i++)
            {
                if (bestSubmissions[i].ProblemID == exerciseID) return true;
            }
            return false;
        }

        public void UpdateBestSubmission(ref BestSubmission[] bestSubmissions, Submission submission, ref double AddToUsersTotalPoints)
        {
            //Look for the last best submission for a specific problem, check if the last submission has less points than the current submission, 
            //if current submission has more points, then update the points value.
            for(int i=0; i<bestSubmissions.Length; i++)
            {
                if(bestSubmissions[i].ProblemID == submission.ExerciseId)
                {
                    if (bestSubmissions[i].MaxScoredPoints <= submission.ScoredPoints)
                    {
                        AddToUsersTotalPoints = submission.ScoredPoints - bestSubmissions[i].MaxScoredPoints;
                        bestSubmissions[i].MaxScoredPoints = submission.ScoredPoints;
                        bestSubmissions[i].SubmissionID = submission.SubmissionID;
                    }
                }
            }
        }

        public bool CurrentUser_Administrator()
        {
            if (HttpContext.User != null)
            {
                string userId = HttpContext.User.Identity.GetUserId();
                return _userManager.IsInRole(userId, "Administrator");

                //Debug.WriteLine("User: " + HttpContext.User.Identity.Name);
                //Debug.WriteLine("Is Administrator: " + _userManager.IsInRole(userId, "Administrator"));
                //Debug.WriteLine("Role: " + user.UserRole);
            }
            else return false;
        }

    }

}