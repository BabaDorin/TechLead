using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechLead.Models;
using System.Diagnostics;

namespace TechLead.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        Data data = new Data();
        public ActionResult ViewProfile(string userID)
        {
            try
            {
                ProfileViewModel viewModel;
                ApplicationUser User = _context.Users.Find(userID);
                viewModel = new ProfileViewModel(User.UserName, User.PhoneNumber, User.About,
                    User.TotalPoints, User.FirstRegistration, User.Job, User.Email, User.ProfilePhoto);

                //Inserting best submissions into profile view model
                viewModel.bestSubmissions = data.ConvertBestSubmissionFromStringToArray(User.BestSubmisions);

                Debug.WriteLine("Last submissions: ");
                for(int i=0; i<viewModel.bestSubmissions.Length; i++)
                {
                    Debug.WriteLine(viewModel.bestSubmissions[i].ProblemName + " " + viewModel.bestSubmissions[i].MaxScoredPoints);
                }
                
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