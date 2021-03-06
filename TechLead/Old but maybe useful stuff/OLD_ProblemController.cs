﻿//using Microsoft.AspNet.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using TechLead.Models;
//using System.Data;
//using System.IO;
//using TechLead.Compiler;
//using System.Dynamic;
//using System.Text;
//using PagedList;
//using PagedList.Mvc;
//using System.Security;
//using System.Security.Policy;
//using System.Security.Permissions;

//namespace TechLead.Controllers
//{
//    public class ProblemController : Controller
//    {
//        private ApplicationDbContext _context;
//        public ProblemController()
//        {
//            _context = new ApplicationDbContext();
//        }

//        //[Authorize(Roles = "Administrator")]
//        //[Authorize(Roles = "Teacher")]
//        // GET: Problem
//        [HttpGet]
//        public ActionResult Details(int id)
//        {
//            try
//            {
//                Exercise e = _context.Exercises.Single(ex => ex.Id == id);

//                //Here we store the object. We will need it later for the 'Compiling' view.
//                TempData["Object"] = e;
//                return View(e);
//            }
//            catch (Exception)
//            {
//                ErrorViewModel Error = new ErrorViewModel();
//                Error.Title = "Error";
//                Error.Description = "We could not find the exercise specified";
//                return View("~/Views/Shared/Error.cshtml", Error);
//            }
//        }

//        [HttpPost]
//        public ActionResult Details(HttpPostedFileBase file)
//        {
//            //First of all, clear the folder with solutions.
//            System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Solutions/"));
//            foreach (FileInfo fileinfo in di.GetFiles())
//            {
//                fileinfo.Delete();
//            }

//            if (file == null)
//            {
//                ErrorViewModel Error = new ErrorViewModel();
//                Error.Title = "Error";
//                Error.Description = "You did not upload any solution ):";
//                return View("~/Views/Shared/Error.cshtml",Error);
//            }
//            //Store the file and send the path to 'Compiling'
//            try
//            {
//                if (file.ContentLength > 0&&file.ContentLength< 10000000)
//                {
//                    var fileName = Path.GetFileName(file.FileName);
//                    var path = Path.Combine(Server.MapPath("~/Solutions/"), fileName);
//                    TempData["FileLocation"] = path;
//                    file.SaveAs(path);
//                    ModelState.Clear();
//                }
//                else
//                {
//                    List<string> Error = new List<string>();
//                    Error.Add("Error");
//                    Error.Add("Somethig is wrong with your solution (Maybe it is bigger than 10MB)");
//                    return View("~/Views/Shared/Error.cshtml",Error);
//                }
//            }
//            catch (Exception E)
//            {
//                List<string> Error = new List<string>();
//                Error.Add("Error");
//                Error.Add(E.ToString());
//                return View("~/Views/Shared/Error.cshtml", Error);
//            }
            
//            //When the user submits a solution to a specific problem, he will be redirected to the 'Compiling' page of the
//            //Problem controller.
//            return RedirectToAction("Compiling","Problem");
//        }








//        public ActionResult Compiling()
//        {
//            try
//            {
//                //Here we extract the data from TempData and pass it to the view.
//                Exercise E = TempData["Object"] as Exercise;

//                //Here we extract the path of the source code (solution).
//                string Path = TempData["FileLocation"].ToString();
//                TempData.Keep();

//                Submission SubmissionInstance = new Submission();
//                Compiler.Compiler compiler = new Compiler.Compiler();

//                //For the first, we will copy the source code content 
//                var fileStream = new FileStream(Path, FileMode.Open);
//                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
//                {
//                    SubmissionInstance.SourceCode = streamReader.ReadToEnd();
//                }
//                fileStream.Close();

//                //The list with the scores for each test case.
//                List<int> ScoredPoints = new List<int>();
//                List<int> ExecutionTime = new List<int>();
//                ScoredPoints.Clear();

//                //AppDomain
//                //PermissionSet pset = new PermissionSet(PermissionState.None);
//                //pset.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
//                //pset.AddPermission(new FileIOPermission(PermissionState.None));
//                //pset.AddPermission(new ReflectionPermission(PermissionState.None));

//                //AppDomainSetup setup = new AppDomainSetup();
//                //setup.ApplicationBase = Server.MapPath("~/Solutions/");

//                //AppDomain sandbox = AppDomain.CreateDomain("Submited Code", null, setup, pset);


//                ScoredPoints = compiler.Compilation(Path, E, out ExecutionTime);
//                TempData["Score"] = null;
//                TempData["Score"] = ScoredPoints;
//                //Insert the submission into the database
                
//                if (Request.IsAuthenticated)
//                {
//                    SubmissionInstance.User = _context.Users.FirstOrDefault(x => x.Id == User.Identity.GetUserId());
//                }

//                int sum = 0;
//                //Count the points
//                for (int i = 0; i < ScoredPoints.Count(); i++)
//                {
//                    sum += ScoredPoints[i];
//                }

//                SubmissionInstance.Points = sum;
//                SubmissionInstance.Time = DateTime.Now.ToString("MM/dd/yyyy");
//                SubmissionInstance.Exercise = E.Name;
//                SubmissionInstance.ExerciseId = E.Id;
//                InsertScoresAndExecutionTimesIntoSubmissionInstance(ref SubmissionInstance, ScoredPoints, ExecutionTime);

//                //Save data about submission. 
//                _context.Submissions.Add(SubmissionInstance);
//                _context.SaveChanges();
//                return RedirectToAction("Results", "Problem");
//            }
//            catch (Exception E)
//            {
//                ErrorViewModel Error = new ErrorViewModel();
//                Error.Title = "Error";
//                Error.Description = E.Message.ToString();
//                return View("~/Views/Shared/Error.cshtml", Error);
//            }
//        }

//        private void InsertScoresAndExecutionTimesIntoSubmissionInstance(ref Submission submissionInstance, List<int> scoredPoints, List<int> executionTime)
//        {
//            int TestNr = scoredPoints.Count();
//            for (int i = 0; i < TestNr; i++)
//            {
//                switch (i+1)
//                {
//                    case 1:
//                        submissionInstance.Score1 = scoredPoints[i];
//                        submissionInstance.ExecutionTime1 = executionTime[i];
//                        break;
//                    case 2:
//                        submissionInstance.Score2 = scoredPoints[i];
//                        submissionInstance.ExecutionTime2 = executionTime[i];
//                        break;
//                    case 3:
//                        submissionInstance.Score3 = scoredPoints[i];
//                        submissionInstance.ExecutionTime3 = executionTime[i];
//                        break;
//                    case 4:
//                        submissionInstance.Score4 = scoredPoints[i];
//                        submissionInstance.ExecutionTime4 = executionTime[i];
//                        break;
//                    case 5:
//                        submissionInstance.Score5 = scoredPoints[i];
//                        submissionInstance.ExecutionTime5 = executionTime[i];
//                        break;
//                    case 6:
//                        submissionInstance.Score6 = scoredPoints[i];
//                        submissionInstance.ExecutionTime6 = executionTime[i];
//                        break;
//                    case 7:
//                        submissionInstance.Score7 = scoredPoints[i];
//                        submissionInstance.ExecutionTime7 = executionTime[i];
//                        break;
//                    case 8:
//                        submissionInstance.Score8 = scoredPoints[i];
//                        submissionInstance.ExecutionTime8 = executionTime[i];
//                        break;
//                    case 9:
//                        submissionInstance.Score9 = scoredPoints[i];
//                        submissionInstance.ExecutionTime9 = executionTime[i];
//                        break;
//                    case 10:
//                        submissionInstance.Score10 = scoredPoints[i];
//                        submissionInstance.ExecutionTime10 = executionTime[i];
//                        break;
//                }
//            }
//        }

//        public ActionResult Results()
//        {
//            List<int> ScoredPoints = TempData["Score"] as List<int>;
//            TempData.Keep();
//            return View(ScoredPoints);
//        }
//        public ActionResult SubmissionDetails(int id)
//        {
//            try
//            {
//                Submission S = _context.Submissions.Single(sub => sub.SubmissionID == id);
//                return View(S);
//            }
//            catch (Exception)
//            {
//                ErrorViewModel Error = new ErrorViewModel();
//                Error.Title = "Error";
//                Error.Description = "We could not find the submission specified. Sorry ):";
//                return View("~/Views/Shared/Error.cshtml", Error);
//            }
//        }









//        public ActionResult Create()
//        {
//            var viewModel = new Exercise
//            {
//                Difficulty = _context.Difficulty.ToList()
//            };

//            return View(viewModel);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(Exercise Exercise)
//        {

//            if (!ModelState.IsValid)
//            {
//                Exercise.Difficulty = _context.Difficulty.ToList();
//                return View("Create", Exercise);
//            }

//            _context.Exercises.Add(Exercise);
//            _context.SaveChanges();
//            return RedirectToAction("Index", "Home");
//        }









//        public ActionResult Submissions(int? page)
//        {
            
//            Exercise e = TempData["Object"] as Exercise;
//            int ExerciseIdParam = e.Id;
//            TempData.Keep();
//            List<Submission> SubmissionForASpecificExercise = new List<Submission>();
//            foreach (Submission S in _context.Submissions)
//            {
//                if (S.ExerciseId==ExerciseIdParam)
//                {
//                    SubmissionForASpecificExercise.Add(S);
//                }
//            }
//            SubmissionForASpecificExercise.Reverse();
//            return View(SubmissionForASpecificExercise.ToList().ToPagedList(page ?? 1, 40));
//        }
//    }
//}