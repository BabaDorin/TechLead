using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechLead.Models;
using PagedList;
using PagedList.Mvc;

namespace TechLead.Controllers
{
    public class AdvancedController : Controller
    {
        // GET: Branch3
        public ActionResult Advancedbranch(int? page)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            List<Exercise> AdvancedExercises = new List<Exercise>();
            foreach (Exercise e in _context.Exercises)
            {
                if (e.DifficulyId == 6)
                {
                    AdvancedExercises.Add(e);
                }
            }
            return View(AdvancedExercises.ToPagedList(page ?? 1, 20));
        }
    }
}