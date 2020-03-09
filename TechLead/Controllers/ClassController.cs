using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechLead.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Http;

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
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
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

        //Will display data about the class, such as exercises within it and members (And join requests, only
        //for the class creator
        [Authorize]
        public ActionResult Details(int id)
        {
            try
            {
                // - if the user is administrator or classCreator then display the data, if not
                // - check if the user is a member of the class.
                Class @class = _context.Classes.Where(c => c.ClassID == id).FirstOrDefault();

                if (isAdministrator() || User.Identity.GetUserId() == @class.ClassCreatorID)
                {
                    return RedirectToAction("Manage", new { id });
                }
                else
                {
                    //Check if user is a member of the class
                    if (@class.Members.Any(m => m.Id == User.Identity.GetUserId()))
                    {
                        ClassViewModel classViewModel = ClassFromModelToViewModel(@class);
                        return View(classViewModel);
                    }
                    else
                    {
                        ErrorViewModel Error = new ErrorViewModel
                        {
                            Title = "Nope.",
                            Description = "You don't have acces to this page"
                        };
                        return View("~/Views/Shared/Error.cshtml", Error);
                    }
                }
            }
            catch (Exception e)
            {
                ErrorViewModel Error = new ErrorViewModel
                {
                    Title = "Error",
                    Description = "Invalid request. Details: " + e.Message
                };
                return View("~/Views/Shared/Error.cshtml", Error);
            }
            
        }

        [Authorize]
        public ActionResult Manage(int id)
        {
            try
            {
                Class @class = _context.Classes.Where(c => c.ClassID == id).FirstOrDefault();
                if (isAdministrator() || User.Identity.GetUserId() == @class.ClassCreatorID)
                {
                    ClassViewModel classViewModel = ClassFromModelToViewModel(@class);
                    return View(classViewModel);
                }
                else
                {
                    ErrorViewModel Error = new ErrorViewModel
                    {
                        Title = "Nope.",
                        Description = "You don't have acces to this page"
                    };
                    return View("~/Views/Shared/Error.cshtml", Error);
                }
            } catch(Exception e)
            {
                ErrorViewModel Error = new ErrorViewModel
                {
                    Title = "Error",
                    Description = "Invalid request. Details: " + e.Message
                };
                return View("~/Views/Shared/Error.cshtml", Error);
            }
        }

        [Authorize]
        public ActionResult ImportExercise(int classID)
        {
            // Check if user is admin or class creator
            // display a textbox where the user will have to introduce the id of the problem
            // down below show a picture of where to find the problem id.

            // On post method check if model is valid, if the problem specified actually exists
            // and make a connection between the problem and the class
            return View();
        }

        [Authorize]
        public ActionResult CreateExercise(int classId)
        {
            return View();
        }

        public ActionResult SeeMembers()
        {
            return View();
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
        public ClassViewModel ClassFromModelToViewModel(Class @class)
        {
            ClassViewModel classViewModel = new ClassViewModel
            {
                ClassCreatorID = @class.ClassCreatorID,
                ClassInfo = @class.ClassInfo,
                ClassName = @class.ClassName,
                ClassInvittionCode = @class.ClassInvitationCode,
                ClassID = @class.ClassID,
                CreationDate = @class.CreationDate,
                Exercises = @class.Exercises.ToList(),
                Members = @class.Members.ToList()
            };

            return classViewModel;
        }

        public bool isAdministrator()
        {
            if (HttpContext.User != null)
            {
                if (!User.Identity.IsAuthenticated) return false;
                string userId = HttpContext.User.Identity.GetUserId();
                return _userManager.IsInRole(userId, "Administrator");

                //Debug.WriteLine("User: " + HttpContext.User.Identity.Name);
                //Debug.WriteLine("Is Administrator: " + _userManager.IsInRole(userId, "Administrator"));
                //Debug.WriteLine("Role: " + user.UserRole);
            }
            else return false;
        }

    }
}