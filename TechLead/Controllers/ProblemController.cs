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
        // GET: Problem
        public ActionResult Create()
        {
            var viewModel = new Exercise
            {
                Domain = _context.Domains.ToList()
            };

            return View(viewModel);
        }
    }
}