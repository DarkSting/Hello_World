using RaythosAerospace.Models.Repositories.AirCraftRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.CartRepository
{
    public class CheckoutViewModel
    {
        public AirCraftModel AirCraft { get; set; }

        public int Count { get; set; }
        public double UnitPrice { get; set; }
        public bool IsSelected { get; set; }
    }
}
