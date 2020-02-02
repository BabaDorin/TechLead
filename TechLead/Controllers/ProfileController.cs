using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechLead.Models;

namespace TechLead.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult ViewProfile(string userID)
        {
            Data data = new Data();
            try
            {
                ProfileViewModel viewModel;
                ApplicationUser User = _context.Users.Find(userID);
                viewModel = new ProfileViewModel(User.UserName, User.PhoneNumber, User.About,
                    User.TotalPoints, User.FirstRegistration, User.Job, User.Email, User.ProfilePhoto);

                //Inserting best submissions into profile view model
                viewModel.bestSubmissions = data.ConvertBestSubmissionFromStringToArray(User.BestSubmisions);
                return View(viewModel);
            }
            catch (Exception e)
            {
                ErrorViewModel Error = new ErrorViewModel();
                Error.Title = "Error";
                Error.Description = "Something went wrong - " + e.Message;
                return View("~/Views/Shared/Error.cshtml", Error);
            }
        }
    }
}