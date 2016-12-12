using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MakeYourPizza.WebUI.Startup))]
namespace MakeYourPizza.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}




