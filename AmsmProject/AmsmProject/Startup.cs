using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AmsmProject.Startup))]
namespace AmsmProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
