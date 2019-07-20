using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechLead.Models;
using System.Data;
using System.IO;

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
            Exercise E = TempData["Object"] as Exercise;
            return RedirectToAction("Compiling","Problem", E.Id);
        }

        public ActionResult Compiling()
        {
            //Find a way how to pass the object id and the file from Details to this controller.
            Exercise e = _context.Exercises.Single(ex => ex.Id == 12);
            return View(e);
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