using RaythosAerospace.Models.Repositories.AirCraftRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.CartRepository
{
    public class CartItemViewModel
    {
       public AirCraftModel aircraft { get; set; }
       public int count { get; set; }

        public string ColorId { get; set; }

        public string InteriorColorId { get; set; }
        public string EngineId { get; set; }
        public string SeatId { get; set; }

        public bool productAdded { get; set; }
    }
}
