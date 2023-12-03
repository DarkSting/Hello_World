using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.OrderRepository
{
    public enum OrderStatusEnum
    {
        Initiating,
        Building,
        Delivering,
        Delivered,
        Completed,
    }
}
