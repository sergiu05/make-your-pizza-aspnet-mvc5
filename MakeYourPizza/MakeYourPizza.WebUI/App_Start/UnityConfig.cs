using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using MakeYourPizza.Domain.Abstract;
using MakeYourPizza.Domain.Entities;
using MakeYourPizza.Domain.Concrete;

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

            container.RegisterType<IGenericRepository<Ingredient>, GenericRepository<Ingredient>>(
                new InjectionConstructor(dbContextType));
            container.RegisterType<IGenericRepository<Category>, GenericRepository<Category>>(
                new InjectionConstructor(dbContextType));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}