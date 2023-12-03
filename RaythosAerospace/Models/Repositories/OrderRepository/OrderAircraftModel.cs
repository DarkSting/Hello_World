using RaythosAerospace.Models.Repositories.AirCraftRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.OrderRepository
{
    public class OrderAircraftModel
    {
        public string OrderId { get; set; }
        public OrderModel Order { get; set; }

        public string AirCraftId { get; set; }
        public AirCraftModel AirCraft { get; set; }
    }
}
