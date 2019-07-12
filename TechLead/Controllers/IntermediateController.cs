using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechLead.Models;

namespace TechLead.Controllers
{
    public class IntermediateController : Controller
    {
        // GET: Branch2
        public ActionResult Intermediatebranch()
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            List<Exercise> IntermediateExercises = new List<Exercise>();
            foreach (Exercise e in _context.Exercises)
            {
                if (e.DifficulyId == 5)
                {
                    IntermediateExercises.Add(e);
                }
            }
            return View(IntermediateExercises);
        }
    }
}