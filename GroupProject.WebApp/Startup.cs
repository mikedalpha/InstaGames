using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GroupProject.WebApp.Startup))]
namespace GroupProject.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
