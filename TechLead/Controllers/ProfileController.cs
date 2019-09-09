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
            ProfileViewModel model;
            ApplicationUser User = _context.Users.Find(userID);
            model = new ProfileViewModel(User.UserName, User.PhoneNumber, User.About,
                User.TotalPoints, User.FirstRegistration, User.Job, User.Email,User.ProfilePhotoPath);
            return View(model);
        }
    }
}