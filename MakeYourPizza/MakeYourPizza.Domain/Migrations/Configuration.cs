namespace MakeYourPizza.Domain.Migrations
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MakeYourPizza.Domain.Concrete.PizzaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MakeYourPizza.Domain.Concrete.PizzaDbContext";
        }

        protected override void Seed(MakeYourPizza.Domain.Concrete.PizzaDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Categories.AddOrUpdate(x => x.Id, 
                new Category() { Id = 1, Name = "Crust", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Category() { Id = 2, Name = "Toppings", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Category() { Id = 3, Name = "Cheeses", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Category() { Id = 4, Name = "Sauces", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );
            context.Ingredients.AddOrUpdate(x => x.Id,
                new Ingredient()
                {
                    Id = 1,
                    Name = "Gluten Free Crust",
                    Price = 2.5m,
                    CategoryId = 1,
                    Imagename = "gluten_free.jpg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Ingredient()
                {
                    Id = 2,
                    Name = "Low Carb Pizza Crust",
                    Price = 2.4m,
                    CategoryId = 1,
                    Imagename = "low_carb.jpg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Ingredient()
                {
                    Id = 3,
                    Name = "Pizza Dough Mix",
                    Price = 2.3m,
                    CategoryId = 1,
                    Imagename = "pizza_dough.jpg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Ingredient()
                {
                    Id = 4,
                    Name = "Sliced Pepperoni",
                    Price = 1.25m,
                    CategoryId = 2,
                    Imagename = "sliced_pepperoni.jpg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Ingredient()
                {
                    Id = 5,
                    Name = "Pre-Cooked Bacon Pieces",
                    Price = 1.75m,
                    CategoryId = 2,
                    Imagename = "bacon.jpg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now

                },
                new Ingredient()
                {
                    Id = 6,
                    Name = "Ham",
                    Price = 1.55m,
                    CategoryId = 2,
                    Imagename = "ham.jpg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Ingredient()
                {
                    Id = 7,
                    Name = "Cooked Beef Topping",
                    Price = 1.99m,
                    CategoryId = 2,
                    Imagename = "beef.jpg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Ingredient()
                {
                    Id = 8,
                    Name = "Cooked Italian Sausage",
                    Price = 2.35m,
                    CategoryId = 2,
                    Imagename = "italian_sausage.jpg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Ingredient()
                {
                    Id = 9,
                    Name = "Mozzarella",
                    Price = .95m,
                    CategoryId = 3,
                    Imagename = "mozzarella.jpg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Ingredient()
                {
                    Id = 10,
                    Name = "Parmesan",
                    Price = 1.25m,
                    CategoryId = 3,
                    Imagename = "parmesan.jpg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Ingredient()
                {
                    Id = 11,
                    Name = "Feta",
                    Price = 1.05m,
                    CategoryId = 3,
                    Imagename = "feta.jpg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Ingredient()
                {
                    Id = 12,
                    Name = "Asian Wing Sauce",
                    Price = 1.35m,
                    CategoryId = 4,
                    Imagename = "asian_sauce.jpg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Ingredient()
                {
                    Id = 13,
                    Name = "Buffallo Wing Sauce",
                    Price = 1.2m,
                    CategoryId = 4,
                    Imagename = "buffalo.jpg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );            
        }
    }
}
