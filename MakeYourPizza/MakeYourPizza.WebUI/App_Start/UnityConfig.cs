using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using MakeYourPizza.Domain.Abstract;
using MakeYourPizza.Domain.Entities;
using MakeYourPizza.Domain.Concrete;
using MakeYourPizza.WebUI.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;
using MakeYourPizza.WebUI.Utilities;

namespace MakeYourPizza.WebUI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            var dbContextType = typeof(PizzaDbContext);
            PizzaDbContext dbContext = new PizzaDbContext();
            container.RegisterInstance(dbContext);

            container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>), new InjectionConstructor(dbContextType));
            //container.RegisterType(typeof(CartController), new InjectionFactory((c,t,s) => new CartController(
            //        container.Resolve<IGenericRepository<Ingredient>>()
            //    )));
            container.RegisterType<IUserStore<AppUser>, UserStore<AppUser>>(
                new InjectionConstructor(dbContextType));
            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(
                    o => System.Web.HttpContext.Current.GetOwinContext().Authentication
                )
            );

            container.RegisterType<IFileWrapper, FileWrapper>();
            container.RegisterType<IOrderProcessor, StoreOrder>("storeorder");

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}