using MakeYourPizza.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakeYourPizza.WebUI.Models
{
    public class IngredientsListViewModel
    {
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }

    public class IngredientSelectListViewModel
    {
        public Ingredient Ingredient { get; set; }
        public SelectList Categories { get; set; }

        public IngredientSelectListViewModel() { }

        public IngredientSelectListViewModel(Ingredient ingredient, IEnumerable categories)
        {
            Ingredient = ingredient;
            Categories = new SelectList(categories, "Id", "Name", ingredient.CategoryId);
        }
    }
}