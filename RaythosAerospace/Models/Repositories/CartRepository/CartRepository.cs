using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.CartRepository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDBContext _context;
        public CartRepository(AppDBContext context)
        {
            _context = context;
        }
        public CartItemModel AddCartItem(CartItemModel item,string cartid)
        {
            item.CartId = cartid;
            _context.CartItems.Add(item);
            _context.SaveChanges();

            return item;
        }

        //get cartitem count
        public int CartItemCount(string cartid)
        {
            return _context.CartItems.Where(s => s.CartId == cartid).ToList().Count;
        }

        public CartModel CreateCart(CartModel cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();

            return cart;
        }

        //get all cart items
        public IEnumerable<CartItemModel> GetAllCartItems(string cartid)
        {
            return _context.CartItems.Where(s => s.CartId == cartid).ToList();
        }

        public CartModel GetCart(string userid)
        {
            return _context.Carts.FirstOrDefault(u => u.UseId == userid);
        }


        //get inserted date of an item
        public DateTime ItemAddedDate(string id, string cartid)
        {
            return _context.CartItems.FirstOrDefault(i => i.CartId == cartid).ItemAddedDate;
          
        }

        public CartModel RemoveCart(CartModel cart)
        {
            _context.Carts.Remove(cart);
            _context.SaveChanges();

            return cart;
        }

        public CartItemModel RemoveCartItem(string id, string cartid)
        {
            CartItemModel cartItem = _context.CartItems.FirstOrDefault(f => f.CartId == cartid);
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();

            return cartItem;
        }
    }
}
