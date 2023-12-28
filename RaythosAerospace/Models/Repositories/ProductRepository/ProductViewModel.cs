using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.ProductRepository
{
    public class ProductViewModel
    {
        public string ProductId { get; set; }

        public string AirCraftId { get; set; }

        public int UnitPrice { get; set; }
        public int Count { get; set; }
        public string UserId { get; set; }
        public int Customization { get; set; }
        public DateTime AddedDate { get; set; }

        public string CartId { get; set; }
    }
}
