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
        ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Branch1
        public ActionResult Beginnerbranch(int? page)
        {
            try
            {
                List<DisplayExerciseGeneralInfoViewModel> BeginnerExercises = new List<DisplayExerciseGeneralInfoViewModel>();
                foreach (Exercise e in _context.Exercises)
                {
                    if (e.DifficultyId == 4)
                    {
                        DisplayExerciseGeneralInfoViewModel displayExerciseGeneralInfoViewModel = new DisplayExerciseGeneralInfoViewModel
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Points = e.Points,
                            DifficultyID = e.DifficultyId,
                            Author = e.Author,
                            AuthorID = e.AuthorID
                        };
                        BeginnerExercises.Add(displayExerciseGeneralInfoViewModel);
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