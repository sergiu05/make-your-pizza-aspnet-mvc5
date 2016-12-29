using MakeYourPizza.Domain.Abstract;
using MakeYourPizza.Domain.Entities;
using MakeYourPizza.WebUI.Models;
using MakeYourPizza.WebUI.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakeYourPizza.WebUI.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class AdminController : Controller
    {
        private IGenericRepository<Ingredient> ingredients;
        private IGenericRepository<Category> categories;
        private IFileWrapper filewrapper;

        public AdminController(IGenericRepository<Ingredient> ingredients, IGenericRepository<Category> categories, IFileWrapper filewrapper)
        {
            this.ingredients = ingredients;
            this.categories = categories;
            this.filewrapper = filewrapper;
        }
        // GET: Admin
        public ViewResult Index()
        {
            IngredientsListViewModel model = new IngredientsListViewModel()
            {
                Ingredients = ingredients.Get(null, null, "category")
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new IngredientSelectListViewModel(new Ingredient(), categories.Get()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Price, CategoryId")]Ingredient ingredient, HttpPostedFileBase file)
        {
            if (!(file != null && file.ContentLength > 0)) /* TBA: mime type checking */
            {
                ModelState.AddModelError("file", "Please upload a file.");
            }
            
            if (ModelState.IsValid)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                file.SaveAs(path);

                ingredient.CreatedAt = DateTime.Now;
                ingredient.UpdatedAt = ingredient.CreatedAt;
                ingredient.Imagename = fileName;

                ingredients.Insert(ingredient);
                ingredients.Save();

                TempData["message"] = string.Format("{0} has been created.", ingredient.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(new IngredientSelectListViewModel(ingredient, categories.Get()));
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Ingredient ingredient = ingredients.GetById(id);
            return View(new IngredientSelectListViewModel(ingredient, categories.Get()));
        }

        [HttpPost]
        public ActionResult Edit(Ingredient ingredient, HttpPostedFileBase file)
        {
            Ingredient currentIngredient = ingredients.GetById(ingredient.Id);
            if (currentIngredient == null)
            {
                ModelState.AddModelError("", "There is no ingredient with id of " + ingredient.Id.ToString());
            }
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    file.SaveAs(path);

                    currentIngredient.Imagename = fileName;
                }                
                
                currentIngredient.Name = ingredient.Name;
                currentIngredient.Price = ingredient.Price;
                currentIngredient.CategoryId = ingredient.CategoryId;
                currentIngredient.UpdatedAt = DateTime.Now;

                ingredients.Update(currentIngredient);
                ingredients.Save();

                TempData["message"] = string.Format("{0} has been edited.", ingredient.Name);
                return RedirectToAction("Index");              
            }
            else
            {
                return View(new IngredientSelectListViewModel(ingredient, categories.Get()));
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Ingredient deletedIngredient = ingredients.GetById(id);
            if (deletedIngredient != null)
            {
                ingredients.Delete(deletedIngredient);
                ingredients.Save();
                TempData["message"] = string.Format("{0} was deleted.", deletedIngredient.Name);
                if (filewrapper.Exists(Path.Combine(Server.MapPath("~/Content/Images"), deletedIngredient.Imagename)))
                {
                    filewrapper.Delete(Path.Combine(Server.MapPath("~/Content/Images"), deletedIngredient.Imagename));
                }
            }
            return RedirectToAction("Index");
        }
    }
}