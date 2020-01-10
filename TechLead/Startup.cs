using Microsoft.Owin;
using Owin;
using TechLead.Models;

[assembly: OwinStartupAttribute(typeof(TechLead.Startup))]
namespace TechLead
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        private void CreateRoles()
        {
            ApplicationDbContext _context = new ApplicationDbContext();

        }
    }
}
