using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Web;
using TechLead.Models;
using System.Diagnostics;

[assembly: OwinStartupAttribute(typeof(TechLead.Startup))]
namespace TechLead
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        private void CreateRoles()
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole
                {
                    Name = "User"
                };
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Administrator"))
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                roleManager.Create(role);
            }

            //Setting the administrators
            ApplicationUser user1, user2;
            try
            {
                user1 = userManager.FindByEmail("bvd.dorin@gmail.com");
                user1.UserRole = "Administrator";
                userManager.AddToRoleAsync(user1.Id, user1.UserRole);
                _context.Entry(user1).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                Debug.WriteLine("'bvd.dorin@gmail.com was not found. No Administration role was assigned to this user");
            }
            try
            {
                user2 = userManager.FindByEmail("mickellogin17@gmail.com");
                user2.UserRole = "Administrator";
                userManager.AddToRoleAsync(user2.Id, user2.UserRole);
                _context.Entry(user2).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                Debug.WriteLine("mickellogin17@gmail.com was not found. No Administration role was assigned to this user");
            }
        }
    }
}
