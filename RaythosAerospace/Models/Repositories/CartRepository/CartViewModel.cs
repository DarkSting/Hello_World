using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.CartRepository
{
    public class CartViewModel
    {
        CartModel cart { get; set; }
        List<CartItemModel> cartItems { get; set; }
    }
}
