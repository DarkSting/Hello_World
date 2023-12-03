using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        OrderModel Find(string id);
        OrderModel Create(OrderModel order);
        OrderModel Update(OrderModel updated);
        OrderModel Delete(string id);
    }
}
