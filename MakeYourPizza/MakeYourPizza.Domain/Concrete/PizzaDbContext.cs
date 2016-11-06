using MakeYourPizza.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourPizza.Domain.Concrete
{
    public class PizzaDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}
