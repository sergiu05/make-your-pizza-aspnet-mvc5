using MakeYourPizza.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourPizza.Domain.Concrete
{
    public class PizzaDbContext : IdentityDbContext<AppUser>
    {
        public PizzaDbContext() : base("PizzaDbContext", throwIfV1Schema: false) { }
        public static PizzaDbContext Create()
        {
            return new PizzaDbContext();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}