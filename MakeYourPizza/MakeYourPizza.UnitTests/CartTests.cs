using MakeYourPizza.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourPizza.UnitTests
{
    /* Learning source: "Pro ASP.NET MVC 5", Adam Freeman, p.216 */
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Arrange
            ProductInterface p1 = new Ingredient { Id = 1, Name = "Ingredient One" };
            ProductInterface p2 = new Ingredient { Id = 2, Name = "Ingredient Two" };
            ProductInterface p3 = new Ingredient { Id = 3, Name = "Ingredient Thress" };
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 10);
            target.AddItem(p2);
            target.AddItem(p3);
            CartLine[] results = target.Lines.ToArray();

            //Assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
            Assert.AreEqual(results[2].Product, p3);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange
            ProductInterface p1 = new Ingredient { Id = 1, Name = "Ingredient One" };
            ProductInterface p2 = new Ingredient { Id = 2, Name = "Ingredient Two" };
            ProductInterface p3 = new Ingredient { Id = 3, Name = "Ingredient Three" };
            Cart target = new Cart();

            // Act
            target.AddItem(p1);
            target.AddItem(p2, 5);
            target.AddItem(p3, 2);
            target.AddItem(p1, 2);
            target.AddItem(p3, 1);
            CartLine[] results = target.Lines.OrderBy(l => l.Product.Id).ToArray();

            // Assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0].Quantity, 3);
            Assert.AreEqual(results[1].Quantity, 5);
            Assert.AreEqual(results[2].Quantity, 3);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            // Arrange
            ProductInterface p1 = new Ingredient { Id = 1, Name = "i1" };
            ProductInterface p2 = new Ingredient { Id = 2, Name = "i2" };
            ProductInterface p3 = new Ingredient { Id = 3, Name = "i3" };
            Cart target = new Cart();

            target.AddItem(p1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            // Act
            target.RemoveLine(p2);
            Assert.IsTrue(target.Lines.Count() == 2);
            Assert.IsTrue(target.Lines.Where(l => l.Product == p2).Count() == 0);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Arrange
            ProductInterface p1 = new Ingredient { Id = 1, Name = "i1", Price = 10M };
            ProductInterface p2 = new Ingredient { Id = 2, Name = "i2", Price = 5M };
            Cart target = new Cart();

            // Act
            target.AddItem(p1);
            target.AddItem(p2, 2);
            decimal result = target.ComputeTotalValue();

            // Assert
            Assert.IsTrue(result == 20M);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Arrange
            ProductInterface p1 = new Ingredient { Id = 1, Name = "i1", Price = 10M };
            ProductInterface p2 = new Ingredient { Id = 2, Name = "i2", Price = 5M };
            Cart target = new Cart();

            // Act
            target.AddItem(p1);
            target.AddItem(p2);
            target.Clear();

            //Assert
            Assert.IsTrue(target.Lines.Count() == 0);
        }
    }
}
