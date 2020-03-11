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
    public class IntermediateController : Controller
    {
        // GET: Branch2
        public ActionResult Intermediatebranch(int? page)
        {
            try
            {
                ApplicationDbContext _context = new ApplicationDbContext();
                List<Exercise> IntermediateExercises = new List<Exercise>();
                foreach (Exercise e in _context.Exercises)
                {
                    if (!e.isArchieved && !e.AvailableOnlyForTheClass)
                    {
                        if (e.DifficultyId == 5)
                        {
                            IntermediateExercises.Add(e);
                        }
                    }
                }
                return View(IntermediateExercises.ToPagedList(page ?? 1, 20));
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