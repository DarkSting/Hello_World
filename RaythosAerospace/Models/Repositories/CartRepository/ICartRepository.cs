using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.CartRepository
{
    public interface ICartRepository
    {
        CartItemModel AddCartItem(CartItemModel item);
        CartModel CreateCart(CartModel cart);

        CartModel GetCart(string userid);
        CartModel RemoveCart(CartModel cart);
        CartItemModel RemoveCartItem(string id, string cartid);
        int CartItemCount(string cartid);
        DateTime ItemAddedDate(string id, string cartid);

        IEnumerable<CartItemModel> GetAllCartItems(string cartid);
    }
}
