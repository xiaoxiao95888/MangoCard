using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MangoCard_Cards.Admin.Startup))]
namespace MangoCard_Cards.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
