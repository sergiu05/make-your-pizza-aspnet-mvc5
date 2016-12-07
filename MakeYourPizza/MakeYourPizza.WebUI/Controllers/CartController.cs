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

        public CartController(IGenericRepository<Ingredient> repository)
        {
            this.repository = repository;
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
    }
}