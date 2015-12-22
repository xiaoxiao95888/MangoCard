using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mango_Cards.Web.Startup))]
namespace Mango_Cards.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
