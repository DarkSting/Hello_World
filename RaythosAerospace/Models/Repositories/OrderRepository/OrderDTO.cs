using RaythosAerospace.Models.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.OrderRepository
{
    public class OrderDTO
    {
        public IList<OrderModel> orders { get; set; }

        public Dictionary<string,IList<ProductModel>> orderdesc { get; set; }
        
    }
}
