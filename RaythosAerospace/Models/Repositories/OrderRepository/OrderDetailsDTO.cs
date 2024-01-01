using RaythosAerospace.Models.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.OrderRepository
{
    public class OrderDetailsDTO
    {
        public OrderModel order { get; set; }

        public IList<ProductModel> products { get; set; }

    }
}
