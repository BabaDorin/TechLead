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
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        [HttpPost]
        public ActionResult Details(HttpPostedFileBase file)
        {
            //First of all, clear the folder with solutions.
            System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Solutions/"));
            foreach (FileInfo fileinfo in di.GetFiles())
            {
                fileinfo.Delete();
            }

            if (file == null)
            {
                List<string> Error = new List<string>();
                Error.Add("Error");
                Error.Add("You did not upload any solution");
                return View("~/Views/Shared/Error.cshtml",Error);
            }
            //Store the file and send the path to 'Compiling'
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Solutions/"), fileName);
                    TempData["FileLocation"] = path;
                    file.SaveAs(path);
                    ModelState.Clear();
                }
                else
                {
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            
            //When the user submits a solution to a specific problem, he will be redirected to the 'Compiling' page of the
            //Problem controller.
            return RedirectToAction("Compiling","Problem");
        }








        public ActionResult Compiling()
        {
            try
            {
                //Here we extract the data from TempData and pass it to the view.
                Exercise E = TempData["Object"] as Exercise;

                //Here we extract the path of the source code (solution).
                string Path = TempData["FileLocation"].ToString();
                TempData.Keep();

                Submission SubmissionInstance = new Submission();
                Compiler.Compiler compiler = new Compiler.Compiler();

                //For the first, we will copy the source code content 
                var fileStream = new FileStream(Path, FileMode.Open);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    SubmissionInstance.SourceCode = streamReader.ReadToEnd();
                }
                fileStream.Close();

                //The list with the scores for each test case.
                List<int> ScoredPoints = new List<int>();
                ScoredPoints.Clear();
                ScoredPoints = compiler.Compilation(Path, E);
                TempData["Score"] = null;
                TempData["Score"] = ScoredPoints;
                //Insert the submission into the database
                
                if (Request.IsAuthenticated)
                {
                    SubmissionInstance.User = _context.Users.FirstOrDefault(x => x.Id == User.Identity.GetUserId());
                }

                int sum = 0;
                //Count the points
                for (int i = 0; i < ScoredPoints.Count(); i++)
                {
                    sum += ScoredPoints[i];
                }
                SubmissionInstance.Points = sum;

                SubmissionInstance.Time = DateTime.Now.ToString("MM/dd/yyyy");
                SubmissionInstance.Exercise = E.Name;
                SubmissionInstance.ExerciseId = E.Id;
                //TempData["Submission"] = SubmissionInstance;
                //Insert the data into DB
                _context.Submissions.Add(SubmissionInstance);
                _context.SaveChanges();
                return RedirectToAction("Results", "Problem");
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        public ActionResult Results()
        {
            List<int> ScoredPoints = TempData["Score"] as List<int>;
            TempData.Keep();
            return View(ScoredPoints);
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









        public ActionResult Submissions()
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
            return View(SubmissionForASpecificExercise);
        }

        
    }
}