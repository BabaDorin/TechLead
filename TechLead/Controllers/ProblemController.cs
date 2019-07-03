using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechLead.Models;

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

        public ActionResult Create()
        {
            var viewModel = new Exercise
            {
                Difficulty = _context.Difficulty.ToList()
            };

            return View(viewModel);
        }
    }
}