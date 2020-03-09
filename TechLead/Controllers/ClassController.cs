using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechLead.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace TechLead.Controllers
{
    public class ClassController : Controller
    {
        public static Data data = new Data();
        private static ApplicationDbContext _context;
        private static UserManager<ApplicationUser> _userManager;

        public ClassController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        }
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
                while(_context.Classes.Any(cl => cl.ClassInvitationCode == random)){
                    random = GenerateRandom6CharCode();
                }
                
                //Now, we are sure that the newly generated invitation code doesn't exists in our db.
                //It's a small chance of duplicates apearring, but however.
                @class.ClassInvitationCode = random;
                @class.CreationDate = DateTime.Now;
                @class.ClassCreatorID = User.Identity.GetUserId();

                //Save data to DB
                _context.Classes.Add(@class);
                _context.SaveChanges();

                return View("~/Views/Home/Index.cshtml");
            }
            catch (Exception e)
            {
                ErrorViewModel Error = new ErrorViewModel
                {
                    Title = "Error",
                    Description = e.Message
                };

                return View("~/Views/Shared/Error.cshtml", Error);
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