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
            try
            {
                ApplicationDbContext _context = new ApplicationDbContext();
                List<Exercise> BeginnerExercises = new List<Exercise>();
                foreach (Exercise e in _context.Exercises)
                {
                    if (e.DifficultyId == 4)
                    {
                        BeginnerExercises.Add(e);
                    }
                }
                return View(BeginnerExercises.ToPagedList(page ?? 1, 20));
            }
            catch (Exception E)
            {
                ErrorViewModel Error = new ErrorViewModel();
                Error.Title = "Error";
                Error.Description = E.Message.ToString();
                return View("~/Views/Shared/Error.cshtml", Error);
            }
        }
    }
}