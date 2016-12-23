using MakeYourPizza.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeYourPizza.Domain.Entities;
using MakeYourPizza.Domain.Infrastructure;

namespace MakeYourPizza.Domain.Concrete
{
    public class StoreOrder : IOrderProcessor
    {
        private IGenericRepository<Order> orders;
        private IGenericRepository<Orderdetail> orderdetails;

        public StoreOrder(IGenericRepository<Order> orders, IGenericRepository<Orderdetail> orderdetails)
        {
            this.orders = orders;
            this.orderdetails = orderdetails;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails, AppUser user)
        {
            
            Order order = new Order()
            {
                Address = shippingDetails.Address,
                City = shippingDetails.City,
                Name = shippingDetails.Username,
                PhoneNumber = shippingDetails.PhoneNumber,
                Totalvalue = cart.ComputeTotalValue(),
                UserId = user.Id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            orders.Insert(order);
            
            foreach(CartLine item in cart)
            {
                orderdetails.Insert(new Orderdetail()
                {
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    Quantity = item.Quantity,
                    Order = order,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
            }
            orderdetails.Save();            
        }
    }
}
