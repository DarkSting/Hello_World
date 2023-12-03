using RaythosAerospace.Models.Repositories.AirCraftRepository;
using RaythosAerospace.Models.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.CartRepository
{
    public class CartModel
    {
        [Key]
        public string CartNumber { get; set; }
        public int UnitPrice { get; set; }

        public string UseId { get; set; }
        public UserModel User { get; set; }
        public string Description { get; set; }


        public ICollection<CartItemModel> CartItems { get; set; }
    }
}
