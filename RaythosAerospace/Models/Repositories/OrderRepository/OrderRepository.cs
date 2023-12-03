using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    }
}
