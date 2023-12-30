using RaythosAerospace.Models.Repositories.AirCraftRepository;
using RaythosAerospace.Models.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.CartRepository
{
    public class CheckoutModel
    {
        public AirCraftModel AirCraft { get; set; }

        public int Count { get; set; }
        public double UnitPrice { get; set; }
        public bool IsSelected { get; set; }

        public string ProductId { get; set; }

        public double ProductTotalCost { get; set; }
        public CustomizationModel Customization { get; set; }

        public ProductModel Product { get; set; }

        public ColorModel ExteriorClr { get; set; }
        public ColorModel InteriorClr{ get; set; }

        public SeatModel Seat { get; set; }



    }
}
