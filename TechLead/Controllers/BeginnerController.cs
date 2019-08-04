using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechLead.Models;
using System.Data;
using PagedList;
using PagedList.Mvc;

namespace TechLead.Controllers
{
    public class BeginnerController : Controller
    {
        // GET: Branch1
        public ActionResult Beginnerbranch(int? page)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            List<Exercise> BeginnerExercises = new List<Exercise>();
            foreach(Exercise e in _context.Exercises)
            {
                if (e.DifficulyId == 4)
                {
                    BeginnerExercises.Add(e);
                }
            }
            return View(BeginnerExercises.ToPagedList(page??1,20));
        }
    }
}