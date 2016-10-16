using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Salvis.App.Web.Startup))]
namespace Salvis.App.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
