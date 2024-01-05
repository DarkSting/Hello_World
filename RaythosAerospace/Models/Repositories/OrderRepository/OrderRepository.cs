﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RaythosAerospace.Models.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDBContext _context;
        public OrderRepository(AppDBContext context)
        {
            _context = context;
        }
        public OrderModel Create(OrderModel order)
        {
             _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }

        public OrderModel Delete(string id)
        {

            OrderModel foundModel = Find(id);
            if (foundModel == null)
            {
                return null;
            }

            _context.Orders.Remove(foundModel);

            return foundModel;
        }

        //Get All orders for a user
        public IList<OrderModel> GetAllOrdersForAUser(string userid)
        {
            IList<OrderModel> foundList = null;
            try
            {
                foundList = _context.Orders.Where(u => u.UserId == userid).ToList();
            }
            catch(Exception e)
            {
                foundList = null;
            }
             

            return foundList;
        }

        public OrderModel Find(string id)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == id);
        }

        public OrderModel Update(OrderModel updated)
        {
            EntityEntry changed = _context.Orders.Attach(updated);
            changed.State = EntityState.Modified;
            _context.SaveChanges();

            return updated;
        }

       public ShippingModel GetShipping(string shippingId)
        {
            return _context.Shippings.Find(shippingId);
        }
        public IEnumerable<ShippingModel> GetShippingMethods()
        {
            return _context.Shippings.ToList();
        }

        public OrderModel UpdateOrderState(OrderModel model)
        {
            EntityEntry changed = _context.Orders.Attach(model);
            changed.State = EntityState.Modified;

            return model;
        }

        public IList<OrderModel> GetAllOrders()
        {
            IList<OrderModel> orders = _context.Orders.ToList();

            foreach(OrderModel current in orders)
            {
                current.Products = _context.Products.Where(p => p.OrderId == current.OrderId).ToList();
                
               foreach(ProductModel product in current.Products)
                {
                    product.AirCraft = _context.AirCrafts.Find(product.AirCraftId);
                }
            }

            return orders;
        }
    }
}
