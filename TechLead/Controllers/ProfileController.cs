﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechLead.Models;
using System.IO;
using PagedList;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TechLead.Custom_Exceptions;
namespace TechLead.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        Data data = new Data();
        public ProfileController()
        {
            
        }
        public ActionResult ViewProfile(string userID)
        {
            try
            {
                ProfileViewModel viewModel;
                ApplicationUser User = _context.Users.Find(userID);
                viewModel = new ProfileViewModel(User.UserName, User.PhoneNumber, User.About,
                    User.TotalPoints, User.FirstRegistration, User.Job, User.Email, User.ProfilePhoto);

                viewModel.bestSubmissions = data.ConvertBestSubmissionFromStringToArray(User.BestSubmisions);
                if(viewModel.bestSubmissions!=null)
                    viewModel.bestSubmissions.Reverse();

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