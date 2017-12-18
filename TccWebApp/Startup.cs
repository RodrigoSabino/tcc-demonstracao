using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TccWebApp.Startup))]
namespace TccWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
