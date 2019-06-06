using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TechLead.Startup))]
namespace TechLead
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
