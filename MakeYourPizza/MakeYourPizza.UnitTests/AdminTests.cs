using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MakeYourPizza.Domain.Abstract;
using MakeYourPizza.Domain.Entities;
using System.Linq.Expressions;
using MakeYourPizza.WebUI.Controllers;
using System.Linq;
using MakeYourPizza.WebUI.Models;
using System.Web.Mvc;
using System.Web;
using MakeYourPizza.WebUI.Utilities;

/* Learning source: "Pro ASP.NET MVC 5", Adam Freeman, p.276 */
namespace MakeYourPizza.UnitTests
{
    /// <summary>
    /// Unit tests for the AdminController methods
    /// </summary>
    [TestClass]
    public class AdminTests
    {
        private AdminController controller; // object under test
        private Mock<IGenericRepository<Ingredient>> mockIngredientRepository; // dependency of the controller
        private Mock<IGenericRepository<Category>> mockCategoryRepository;
        private Mock<IFileWrapper> mockFileWrapper;

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            // Arrange - create the controller and the mock repositories
            mockIngredientRepository = new Mock<IGenericRepository<Ingredient>>();
            mockCategoryRepository = new Mock<IGenericRepository<Category>>();
            mockFileWrapper = new Mock<IFileWrapper>();
            controller = new AdminController(mockIngredientRepository.Object, mockCategoryRepository.Object, mockFileWrapper.Object);

        }
        //
        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            mockIngredientRepository = null;
            mockCategoryRepository = null;
            mockFileWrapper = null;
            controller = null;
        }
        //

        /// <summary>
        /// Check if the index method correctly returns the Ingredient objects that are in the repository
        ///</summary>
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            // Arrange - set expectations on the mocked objects     

            mockIngredientRepository.Setup(m => m.Get(null, null, "category"))
                .Returns(
                    new Ingredient[]
                    {
                        new Ingredient { Id = 1, Name = "P1", Price = 1.10m },
                        new Ingredient { Id = 2, Name = "P2", Price = 2.10m },
                        new Ingredient { Id = 3, Name = "P3", Price = .5m }
                    }
                );

            // Act 
            Ingredient[] result = ((IngredientsListViewModel)controller.Index().ViewData.Model).Ingredients.ToArray();

            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }

        /// <summary>
        /// For the POST-processing Edit action method, make sure that valid updates to the Ingredient object that 
        /// the model binder has created are passed to the ingredient repository to be saved.
        ///</summary>
        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Arrange - create an ingredient
            Ingredient ingredient = new Ingredient { Id = 1,  Name = "Test", Price = 1.5m };
            mockIngredientRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns((int id) => new Ingredient { Id = id, Name = "Test", Price = 1.5m });

            // Arrange - mock a file
            // Source: http://stackoverflow.com/questions/8308899/unit-test-a-file-upload-how
            var mockFile = new Mock<HttpPostedFileBase>();
            mockFile.Setup(x => x.FileName).Returns("test.jpg");
            mockFile.Setup(x => x.ContentLength).Returns(1000);

            var httpContextMock = new Mock<HttpContextBase>();
            var serverMock = new Mock<HttpServerUtilityBase>();
            serverMock.Setup(x => x.MapPath("~/Content/Images")).Returns(@"c:\work\Content\Images");
            httpContextMock.Setup(x => x.Server).Returns(serverMock.Object);

            this.controller.ControllerContext = new ControllerContext(httpContextMock.Object, new System.Web.Routing.RouteData(), controller);

            // Act - try to save the ingredient
            ActionResult result = controller.Edit(ingredient, mockFile.Object);

            // Assert - check that the repository was called
            mockIngredientRepository.Verify(m => m.Update(It.IsAny<Ingredient>()), Times.Once());
            // Assert - check the method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        /// For the POST-processing Edit action method, make sure that invalid updates (where a model error exists)  
        /// are not passed to the ingredient repository.
        ///</summary>
        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - create an ingredient
            Ingredient ingredient = new Ingredient { Id = 1, Name = "Test" };
            mockIngredientRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns((int id) => null);

            // Arrange - mock a file
            var mockFile = new Mock<HttpPostedFileBase>();
            mockFile.Setup(x => x.FileName).Returns("test.jpg");
            mockFile.Setup(x => x.ContentLength).Returns(1000);

            // Arrange - add an error to the model state
            controller.ModelState.AddModelError("error", "error");

            // Act - try to save the product
            ActionResult result = controller.Edit(ingredient, mockFile.Object);

            // Assert - check that the repository was not called
            mockIngredientRepository.Verify(m => m.Update(It.IsAny<Ingredient>()), Times.Never());
            // Assert - check the method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        /// When a valid Ingredient id is passed as a parameter, the action method calls the Delete
        /// method of the repository and passes the Ingredient entity to be deleted.
        ///</summary>
        [TestMethod]
        public void Can_Delete_Valid_Ingredients()
        {
            // Arrange - create an ingredient
            Ingredient ingredient = new Ingredient { Name = "Test", Price = 1.5m, Imagename = "test1.png" };
            // Arrange - set expectation on the mocked objects
            mockIngredientRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) => { ingredient.Id = id; return ingredient; });
            mockFileWrapper.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            mockFileWrapper.Setup(x => x.Delete(It.IsAny<string>()));
            var httpContextMock = new Mock<HttpContextBase>();
            var serverMock = new Mock<HttpServerUtilityBase>();
            serverMock.Setup(x => x.MapPath("~/Content/Images")).Returns(@"c:\work\Content\Images");
            httpContextMock.Setup(x => x.Server).Returns(serverMock.Object);

            this.controller.ControllerContext = new ControllerContext(httpContextMock.Object, new System.Web.Routing.RouteData(), controller);

            // Act - delete the product with id of 10
            controller.Delete(10);

            // Assert - ensure that the repository delete method was called with the correct Ingredient
            mockIngredientRepository.Verify(x => x.Delete(ingredient));
            // Assert - ensure that the filewrapper delete method was called
            mockFileWrapper.Verify(x => x.Delete(It.IsAny<string>()), Times.Once());
        }

        /// <summary>
        /// When a valid Ingredient id is passed as a parameter, the action method calls the Delete
        /// method of the repository and passes the Ingredient entity to be deleted.
        ///</summary>
        [TestMethod]
        public void Cannot_Delete_Invalid_Ingredients()
        {
            // Arrange - set expectations on the mocked objects
            mockIngredientRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) => null);

            // Act - delete an inexistent product with id of -1
            controller.Delete(-1);

            // Assert - ensure that the repository delete method was not called
            mockIngredientRepository.Verify(x => x.Delete(It.IsAny<Ingredient>()), Times.Never());
        }

    }
}