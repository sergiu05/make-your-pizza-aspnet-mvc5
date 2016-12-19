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
    public class CartController : Controller
    {
        private IGenericRepository<Ingredient> repository;
        private IOrderProcessor[] processors;

        public CartController(IGenericRepository<Ingredient> repository, IOrderProcessor[] processors)
        {
            this.repository = repository;
            this.processors = processors;
        }

        public ActionResult Index()
        {
            return PartialView(new CartIndexViewModel { Cart = GetCart() });
        }

       
        public JsonResult AddToCart(int id)
        {
            ProductInterface product = repository.GetById(id);
            if (product != null)
            {
                GetCart().AddItem(product);
            }
            return Json(new { ProductName = product.Name }, JsonRequestBehavior.DenyGet);
        }

        
        public JsonResult RemoveFromCart(int id)
        {
            ProductInterface product = repository.GetById(id);
            if (product != null)
            {
                GetCart().RemoveLine(product);
                return Json(new { Deleted = true }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { Deleted = false }, JsonRequestBehavior.DenyGet);
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Checkout()
        {
            return View(new ShippingDetails() { Username = User.Identity.Name});
        }

        [HttpPost]
        public ActionResult Checkout(ShippingDetails shippingDetails)
        {
            Cart cart = GetCart();

            if (ModelState.IsValid)
            {
                foreach(IOrderProcessor processor in processors)
                {
                    processor.ProcessOrder(cart, shippingDetails);
                }
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}