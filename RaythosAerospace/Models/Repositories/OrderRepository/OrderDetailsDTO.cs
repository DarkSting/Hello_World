using RaythosAerospace.Models.Repositories.ProductRepository;
using RaythosAerospace.Models.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.OrderRepository
{
    public class OrderDetailsDTO
    {
        public OrderModel order { get; set; }

        public Dictionary<string, UserModel> orderedUsers { get; set; }

        public UserModel user { get; set; }
        public IList<ProductModel> products { get; set; }

    }
}
