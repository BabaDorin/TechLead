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

            //Store the file and send the path to 'Compiling'
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

                //The list with the scores for each test case.
                List<int> ScoredPoints = new List<int>();

                Compiler.Compiler compiler = new Compiler.Compiler();
                ScoredPoints = compiler.Compilation(Path);

                TempData.Keep();

                if (System.IO.File.Exists(Path))
                {
                    return View(E);
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