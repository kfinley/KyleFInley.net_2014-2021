using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KyleFinley.Web.Startup))]
namespace KyleFinley.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
