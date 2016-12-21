using MakeYourPizza.Domain.Concrete;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MakeYourPizza.Domain.Infrastructure;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

namespace MakeYourPizza.WebUI
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<PizzaDbContext>(PizzaDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new Microsoft.Owin.PathString("/Account/Login")
            });
        }
    }
}