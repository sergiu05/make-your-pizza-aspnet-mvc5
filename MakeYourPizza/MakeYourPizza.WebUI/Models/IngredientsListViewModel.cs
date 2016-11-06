using MakeYourPizza.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeYourPizza.WebUI.Models
{
    public class IngredientsListViewModel
    {
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}