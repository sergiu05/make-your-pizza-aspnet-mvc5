using MakeYourPizza.Domain.Abstract;
using MakeYourPizza.Domain.Entities;
using MakeYourPizza.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakeYourPizza.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IGenericRepository<Ingredient> ingredients;        

        public HomeController(IGenericRepository<Ingredient> ingredients)
        {
            this.ingredients = ingredients;            
        }
        // GET: Home
        public ViewResult Index()
        {
            IngredientsListViewModel model = new IngredientsListViewModel
            {
                Ingredients = this.ingredients.Get(null, null, "category")
            };
            return View(model);
        }
    }
}