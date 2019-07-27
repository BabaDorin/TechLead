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
        public class ViewModel
        {
            public Exercise  Ex = new Exercise();
            public List<int> Points = new List<int>();
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

                Compiler.Compiler compiler = new Compiler.Compiler();

                //The list with the scores for each test case.
                List<int> ScoredPoints = new List<int>();
                ScoredPoints = compiler.Compilation(Path, E);
                TempData["Score"] = ScoredPoints;

                return RedirectToAction("Results", "Problem", ScoredPoints);
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
    }
}