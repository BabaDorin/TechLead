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

namespace TechLead.Controllers
{
    public class ProblemController : Controller
    {
        private static Data data = new Data();
        private ApplicationDbContext _context;

        public ProblemController()
        {
            _context = new ApplicationDbContext();
        }
        
        // GET: Problem
        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                Exercise e = _context.Exercises.Single(ex => ex.Id == id);

                //Here we store the object. We will need it later for the 'Compiling' view.
                TempData["Object"] = e;
                return View(e);
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
        public ActionResult Details(HttpPostedFileBase file)
        {
            if(file == null)
            {
                ErrorViewModel Error = new ErrorViewModel();
                Error.Title = "Error";
                Error.Description = "You did not upload any solution ):";
                return View("~/Views/Shared/Error.cshtml", Error);
            }

            try
            {
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
                return RedirectToAction("Compiling", "Problem");

            }
            catch (Exception e)
            {
                ErrorViewModel Error = new ErrorViewModel();
                Error.Title = "Oops, something happened :(";
                Error.Description = e.Message;
                return View("~/Views/Shared/Error.cshtml", Error);
            }
        }
        
        public ActionResult Compiling()
        {
            try
            {
                Exercise e = TempData["Object"] as Exercise;
                Judge0_SubmissionViewModel judge0_submission = TempData["Judge0_Submission"] as Judge0_SubmissionViewModel;
                Submission submission = new Submission();
                submission.SourceCode = judge0_submission.source_code;
                Debug.WriteLine("source code: " + judge0_submission.source_code);
                ExecuteAndCheck(judge0_submission, ref submission, e);
                return View("Index", "Home");
            }
            catch (Exception e)
            {
                ErrorViewModel Error = new ErrorViewModel();
                Error.Title = "Oops, something happened :(";
                Error.Description = e.Message;
                return View("~/Views/Shared/Error.cshtml", Error);
            }
        }

        public void ExecuteAndCheck(Judge0_SubmissionViewModel judge0_Submission, ref Submission submission,
            Exercise E)
        {
            //string[] testImput = new string[10];
            //string[] testOutput = new string[10];
            //testImput[0] = E.TestImput1;
            //testOutput[0] = E.TestOutput1;
            //testImput[1] = E.TestImput2;
            //testOutput[1] = E.TestOutput2;
            //testImput[2] = E.TestImput3;
            //testOutput[2] = E.TestOutput3;
            //testImput[3] = E.TestImput4;
            //testOutput[3] = E.TestOutput4;
            //testImput[4] = E.TestImput5;
            //testOutput[4] = E.TestOutput5;
            //testImput[5] = E.TestImput6;
            //testOutput[5] = E.TestOutput6;
            //testImput[6] = E.TestImput7;
            //testOutput[6] = E.TestOutput7;
            //testImput[7] = E.TestImput8;
            //testOutput[7] = E.TestOutput8;
            //testImput[8] = E.TestImput9;
            //testOutput[8] = E.TestOutput9;
            //testImput[9] = E.TestImput10;
            //testOutput[9] = E.TestOutput10;

            int[] Score = new int[10];
            for(int i=0; i<10; i++)
            {
                //if (testImput[i] != null)
                //{
                //    //The method sends HTTP requests to judge0 API, then, it gets a token as a response.
                //    //After that, having the token, we make another request to get submission details like execution time and so on.
                //    var request = (HttpWebRequest)WebRequest.Create("https://api.judge0.com/submissions/?base64_encoded=true&wait=false");
                //    request.ContentType = "application/json";
                //    request.Method = "POST";
                //    Debug.WriteLine("Going in test >>>");

                //    judge0_Submission.stdin = testImput[i];
                //    judge0_Submission.expected_output = testOutput[i];
                //    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                //    {
                //        string json = buildJson(judge0_Submission);
                //        Debug.WriteLine(json);
                //        streamWriter.Write(json);
                //        streamWriter.Flush();
                //    }
                //    Debug.WriteLine("sending etasamaia");
                //    var httpResponse = (HttpWebResponse)request.GetResponse();
                //    Debug.WriteLine("RASPUNS PRIMIT iobana");
                //    JObject result;
                //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                //    {
                //        result = JObject.Parse(streamReader.ReadToEnd());
                //    }
                //    Debug.WriteLine(result.SelectToken("token"));

                //    //Now we have the token
                //    //Next => get submission detalis using the token si asa mai departe
                //    request = (HttpWebRequest)WebRequest.Create("https://api.judge0.com/submissions/" + result.SelectToken("token") + "?base64_encoded=false&fields=stdout,stderr,status_id,language_id");
                //    request.ContentType = "application/json";
                //    request.Method = "GET";
                //    httpResponse = (HttpWebResponse)request.GetResponse();
                //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                //    {
                //        result = JObject.Parse(streamReader.ReadToEnd());
                //    }
                //    //Next => from result ( which is the result json) build a submission_judge0 object, then
                //    //from the submission_judge0 object build submission object and that's all.
                //    if (result.GetValue("description").ToString()=="Accepted")
                //    {

                //    }
                //    Debug.WriteLine(result);
                //}
            }
        }


        private static string buildJson(Judge0_SubmissionViewModel judge0)
        {
            return "{ \"source_code\" : \"" + Base64Encode(judge0.source_code) + "\", " +
                "\"language_id\" : \"" + judge0.language_id +"\", " +
                "\"stdin\" : \""+Base64Encode(judge0.stdin) +"\", " +
                "\"stdout\" :\""+Base64Encode(judge0.expected_output)+ "\" }";
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public int LanguageId(string fileName)
        {
            switch (Path.GetExtension(fileName))
            {
                case ".cs":
                    return 17;
                case ".cpp":
                    return 15;
                case ".pas":
                    return 33;
                case ".java":
                    return 28;
                case ".py":
                    return 36;
                default: return -1;
            }
        }

        private void InsertScoresAndExecutionTimesIntoSubmissionInstance(ref Submission submissionInstance, List<int> scoredPoints, List<int> executionTime)
        {
            int TestNr = scoredPoints.Count();
            for (int i = 0; i < TestNr; i++)
            {
                switch (i+1)
                {
                    case 1:
                        submissionInstance.Score1 = scoredPoints[i];
                        submissionInstance.ExecutionTime1 = executionTime[i];
                        break;
                    case 2:
                        submissionInstance.Score2 = scoredPoints[i];
                        submissionInstance.ExecutionTime2 = executionTime[i];
                        break;
                    case 3:
                        submissionInstance.Score3 = scoredPoints[i];
                        submissionInstance.ExecutionTime3 = executionTime[i];
                        break;
                    case 4:
                        submissionInstance.Score4 = scoredPoints[i];
                        submissionInstance.ExecutionTime4 = executionTime[i];
                        break;
                    case 5:
                        submissionInstance.Score5 = scoredPoints[i];
                        submissionInstance.ExecutionTime5 = executionTime[i];
                        break;
                    case 6:
                        submissionInstance.Score6 = scoredPoints[i];
                        submissionInstance.ExecutionTime6 = executionTime[i];
                        break;
                    case 7:
                        submissionInstance.Score7 = scoredPoints[i];
                        submissionInstance.ExecutionTime7 = executionTime[i];
                        break;
                    case 8:
                        submissionInstance.Score8 = scoredPoints[i];
                        submissionInstance.ExecutionTime8 = executionTime[i];
                        break;
                    case 9:
                        submissionInstance.Score9 = scoredPoints[i];
                        submissionInstance.ExecutionTime9 = executionTime[i];
                        break;
                    case 10:
                        submissionInstance.Score10 = scoredPoints[i];
                        submissionInstance.ExecutionTime10 = executionTime[i];
                        break;
                }
            }
        }

        public ActionResult Results()
        {
            List<int> ScoredPoints = TempData["Score"] as List<int>;
            TempData.Keep();
            return View(ScoredPoints);
        }
        public ActionResult SubmissionDetails(int id)
        {
            try
            {
                Submission S = _context.Submissions.Single(sub => sub.SubmissionID == id);
                return View(S);
            }
            catch (Exception)
            {
                ErrorViewModel Error = new ErrorViewModel();
                Error.Title = "Error";
                Error.Description = "We could not find the submission specified. Sorry ):";
                return View("~/Views/Shared/Error.cshtml", Error);
            }
        }

        public ActionResult Create()
        {
            var viewModel = new ExerciseViewModel
            {
                Difficulty = _context.Difficulty.ToList()
            };
            viewModel.Test = new Test[10];
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExerciseViewModel exerciseViewModel)
        {
            if (!ModelState.IsValid)
            {
                exerciseViewModel.Difficulty = _context.Difficulty.ToList();
                return View("Create", exerciseViewModel);
            }

            //If everything is OK, we copy all the data from ExerciseViewModel to Exercise

            Exercise exercise = CopyData(exerciseViewModel);
            _context.Exercises.Add(exercise);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Submissions(int? page)
        {
            Exercise e = TempData["Object"] as Exercise;
            int ExerciseIdParam = e.Id;
            TempData.Keep();
            List<Submission> SubmissionForASpecificExercise = new List<Submission>();
            foreach (Submission S in _context.Submissions)
                if (S.ExerciseId==ExerciseIdParam)
                {
                    SubmissionForASpecificExercise.Add(S);
                }
            SubmissionForASpecificExercise.Reverse();
            return View(SubmissionForASpecificExercise.ToList().ToPagedList(page ?? 1, 40));
        }
        private static Exercise CopyData(ExerciseViewModel ExerciseViewModel)
        {
            Exercise e = new Exercise();
            e.Id = ExerciseViewModel.Id;
            e.Author = ExerciseViewModel.Author;
            e.Name = ExerciseViewModel.Name;
            e.Points = ExerciseViewModel.Points;
            e.Condition = ExerciseViewModel.Condition;
            e.Constraints = ExerciseViewModel.Constraints;
            e.Difficulty = ExerciseViewModel.Difficulty;
            e.DifficulyId = ExerciseViewModel.DifficulyId;
            e.Imput1 = ExerciseViewModel.Imput1;
            e.Imput2 = ExerciseViewModel.Imput2;
            e.Imput3 = ExerciseViewModel.Imput3;
            e.Imput4 = ExerciseViewModel.Imput4;
            e.Imput5 = ExerciseViewModel.Imput5;
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
            e.ImputFormat = ExerciseViewModel.ImputFormat;
            e.NumberOfTests = ExerciseViewModel.NumberOfTests;
            
            for (int i=0; i<10; i++)
            {
                e.InputColection += ExerciseViewModel.Test[i].Input;
                if (i < 9 && ExerciseViewModel.Test[i+1].Input!=null) e.InputColection += data.testCase_Delimitator;

                e.OutputColection += ExerciseViewModel.Test[i].Output;
                if (i < 9 && ExerciseViewModel.Test[i + 1].Output != null) e.OutputColection += data.testCase_Delimitator;
            }
            return e;
        }
    }
}