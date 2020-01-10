using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechLead.Models;

namespace TechLead.Controllers
{
    public class AdministrationController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public AdministrationController()
        {
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
        }

        [HttpGet]
        public ActionResult CreateRole() => View();

        [HttpPost]
        public async Task<ActionResult> CreateRole(CreateRoleViewModel createRole)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = createRole.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }
            }

            return View(createRole);
        }
    }
}