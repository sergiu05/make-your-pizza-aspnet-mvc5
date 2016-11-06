using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MakeYourPizza.Domain.Abstract;
using MakeYourPizza.Domain.Entities;
using System.Collections.Generic;
using MakeYourPizza.WebUI.Controllers;
using MakeYourPizza.WebUI.Models;
using System.Linq;

namespace MakeYourPizza.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<IGenericRepository<Ingredient>> ingredientRepository = new Mock<IGenericRepository<Ingredient>>();        

        [TestMethod]
        public void Can_Retrieve_Ingredients()
        {
            // Arrange
            var categoryOne = new Category { Id = 1, Name = "Dough" };
            var categoryTwo = new Category { Id = 1, Name = "Sauces" };
            var ingredients = new List<Ingredient>()
            {
                new Ingredient { Id = 1, Name = "Ingredient One", Category = categoryOne, Price = 1m },
                new Ingredient { Id = 2, Name = "Ingredient Two", Category = categoryOne, Price = 2m },
                new Ingredient { Id = 3, Name = "Ingredient Three", Category = categoryTwo, Price = 0.5m },
                new Ingredient { Id = 4, Name = "Ingredient Four", Category = categoryOne, Price = .75m },
                new Ingredient { Id = 5, Name = "Ingredient Five", Category = categoryTwo, Price = 2m }
            };
            ingredientRepository.Setup(m => m.Get(null, null, "category")).Returns(ingredients);
                        
            HomeController controller = new HomeController(ingredientRepository.Object);

            // Act
            IngredientsListViewModel result = (IngredientsListViewModel)controller.Index().Model;

            // Assert
            Ingredient[] ingredientArray = result.Ingredients.ToArray();
            Assert.AreEqual(5, ingredientArray.Count());
            Assert.AreEqual("Ingredient One", ingredientArray[0].Name);
            Assert.AreEqual("Ingredient Four", ingredientArray[3].Name);
        }
    }
}
