using MakeYourPizza.Domain.Concrete;
using MakeYourPizza.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourPizza.Domain.Infrastructure
{
    public class AppRoleManager : RoleManager<AppRole> 
    {
        public AppRoleManager(RoleStore<AppRole> store) : base(store) { }

        public static AppRoleManager Create(
            IdentityFactoryOptions<AppRoleManager> options,
            IOwinContext context
            )
        {
            PizzaDbContext db = context.Get<PizzaDbContext>();
            return new AppRoleManager(new RoleStore<AppRole>(db));
        }
    }
}
