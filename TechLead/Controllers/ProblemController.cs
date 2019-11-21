using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechLead.Models;
using System.Data;
using System.IO;
using TechLead.Compiler;
using System.Dynamic;
using System.Text;
using PagedList;
using PagedList.Mvc;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;
using System.Diagnostics;

namespace TechLead.Controllers
{
    public class ProblemController : Controller
    {
        private ApplicationDbContext _context;
        public ProblemController()
        {
            _context = new ApplicationDbContext();
        }

        //[Authorize(Roles = "Administrator")]
        //[Authorize(Roles = "Teacher")]
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
                return RedirectToAction("Compiling", "Problem", submission);

            }
            catch (Exception e)
            {
                ErrorViewModel Error = new ErrorViewModel();
                Error.Title = "Oops, something happened :(";
                Error.Description = e.Message;
                return View("~/Views/Shared/Error.cshtml", Error);
            }
        }
        
        public ActionResult Compiling(Judge0_SubmissionViewModel judge0_submission)
        {
            try
            {
                Exercise e = TempData["Object"] as Exercise;
                Submission submission = new Submission();
                submission.SourceCode = judge0_submission.source_code;
                return View("Home", "Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ExecuteAndCheck(Judge0_SubmissionViewModel judge0_Submission, Submission submission,
            string stdin, string stdout)
        {
            Debug.WriteLine("Going in test 1 >>>");

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
            var viewModel = new Exercise
            {
                Difficulty = _context.Difficulty.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Exercise Exercise)
        {

            if (!ModelState.IsValid)
            {
                Exercise.Difficulty = _context.Difficulty.ToList();
                return View("Create", Exercise);
            }

            _context.Exercises.Add(Exercise);
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
            {
                if (S.ExerciseId==ExerciseIdParam)
                {
                    SubmissionForASpecificExercise.Add(S);
                }
            }
            SubmissionForASpecificExercise.Reverse();
            return View(SubmissionForASpecificExercise.ToList().ToPagedList(page ?? 1, 40));
        }
    }
}