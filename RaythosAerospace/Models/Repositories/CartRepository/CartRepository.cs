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
        public CartItemModel AddCartItem(CartItemModel item)
        {

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
            List<CartItemModel> models = _context.CartItems.Where(s => s.CartId == cartid).ToList();

            //removing the circular reference in navigation properties 
            foreach(CartItemModel current in models)
            {
                current.Cart = null;
                current.AirCraft = null;
            }

            return models;
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

        public CartItemModel RemoveCartItemByAirCraftTag(string id)
        {
            CartItemModel cartItem = _context.CartItems.FirstOrDefault(f => f.AirCraftId == id);
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();

            return cartItem;
        }

        public void RemoveCartItem(string id)
        {
            IEnumerable<CartItemModel> cartItem = _context.CartItems.Where(f => f.AirCraftId == id).ToList() ;

            if (cartItem.Any())
            {
                _context.CartItems.RemoveRange(cartItem);
                _context.SaveChanges();
            }
           

           
        }
    }
}
