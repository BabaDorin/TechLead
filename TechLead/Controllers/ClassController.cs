using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechLead.Models;

namespace TechLead.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ClassViewModel classViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Create", classViewModel);
                }

                Class @class = ClassFromViewModelToModel(classViewModel);
                @class.ClassInvitationCode = Guid.NewGuid().ToString();

                string random = GenerateRandom6CharCode();
                //Check if random already exists
                //set classInvitationCode = random if does not.

                return View("~/Views/Home/Index.cshtml");
            }
            catch (Exception e)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            
        }

        public string GenerateRandom6CharCode()
        {
            Random rnd = new Random();
            string chars = "ABCDEFGHJIKLMNOPQRSTUVXYZabcdefghjiklmnopqrstuvxyz1234567890";
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[rnd.Next(chars.Length)]).ToArray());
        }

        public Class ClassFromViewModelToModel(ClassViewModel classViewModel)
        {
            Class @class = new Class
            {
                ClassName = classViewModel.ClassName,
                ClassInfo = classViewModel.ClassInfo
            };

            return @class;
        }
    } 
}