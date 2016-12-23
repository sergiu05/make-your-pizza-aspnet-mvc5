using MakeYourPizza.Domain.Abstract;
using MakeYourPizza.Domain.Entities;
using MakeYourPizza.Domain.Infrastructure;
using MakeYourPizza.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
        public AppUser CurrentUser
        {
            get
            {
                return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                /* http://stackoverflow.com/questions/20925822/asp-mvc5-identity-how-to-get-current-applicationuser */
            }
        }

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

        [Authorize]
        [HttpPost]
        public ActionResult Checkout([Bind(Include = "Username, Address, City, PhoneNumber")]ShippingDetails shippingDetails)
        {
            Cart cart = GetCart();
            
            if (ModelState.IsValid)
            {
                foreach(IOrderProcessor processor in processors)
                {
                    processor.ProcessOrder(cart, shippingDetails, CurrentUser);
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