using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Botines.Web.Startup))]
namespace Botines.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
