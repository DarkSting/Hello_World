using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaythosAerospace.Models.Repositories.AirCraftRepository;
using RaythosAerospace.Models.Repositories.CartRepository;
using RaythosAerospace.Models.Repositories.PaymentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RaythosAerospace.Models.Repositories.ProductRepository;


namespace RaythosAerospace.Controllers
{
    public class CartController : Controller
    {

        private readonly ICartRepository _cartRepo;
        private readonly IAirCraftRepository _aircraftRepo;
        private readonly IProductRepository _productRepo;

        public CartController(ICartRepository cartRepo, IAirCraftRepository aircraftRepo,IProductRepository productRepo)
        {

            _cartRepo = cartRepo;
            _aircraftRepo = aircraftRepo;
            _productRepo = productRepo;

        }

        
        [HttpPost]
        public IActionResult AddToCart([FromBody]CartItemViewModel item)
        {
            string userId = "U0001";

            CartModel foundCart = _cartRepo.GetCart(userId);

            AirCraftModel foundcraft = _aircraftRepo.Find(item.aircraft.AircraftId);

            if (foundcraft.ItemCount < item.count)
            {
                ProductModel product = new ProductModel
                {
                    AirCraftId = item.aircraft.AircraftId,
                    CartId = foundCart.CartNumber,
                    AddedDate = DateTime.Now.Date,
                    Count = item.count,
                    UnitPrice = (int)item.aircraft.AirCraftPrice,
                    UserId = userId,
                    ProductId = Guid.NewGuid().ToString(),


                };
                item.productAdded = true;
                _productRepo.AddProduct(product);
            }

          

            return View("~/Views/AirCraft/Customize.cshtml",item);
        }

        // GET: CartController
        [HttpGet]
        public IActionResult ViewUserCart(string userid)
        {

            userid = "U0001";

            return View(_prepareDataForLoad(ViewBag, userid));
        }


        public ViewResult Checkout(PaymentModel model)
        {
            return View("~/Views/Payment/Checkout.cshtml",model);
        }

        private PaymentModel _prepareDataForLoad(dynamic bag,string userid)
        {
            //getting the cart and cart items for a given user
            CartModel foundCart = _cartRepo.GetCart(userid);
            Dictionary<string, CheckoutViewModel> aircrafts = new Dictionary<string, CheckoutViewModel>();
            IEnumerable<CartItemModel> cartitems = _cartRepo.GetAllCartItems(foundCart.CartNumber);

            //iterating the aircrafts and adding them into the dictionary
            foreach(CartItemModel current in cartitems)
            {
                if (!aircrafts.ContainsKey(current.AirCraftId))
                {
                    AirCraftModel foundAirCraft = _aircraftRepo.Find(current.AirCraftId);
                    CheckoutViewModel temp = new CheckoutViewModel
                    {
                        AirCraft = foundAirCraft,

                    };
                    aircrafts.Add(current.AirCraftId, temp);
                }
            }

            bag.Cart = foundCart;
            bag.CartItems = cartitems;
            bag.AirCrafts = aircrafts;

            PaymentModel model = new PaymentModel
            {
                aircrafts = aircrafts,
                cartitems = cartitems
            };

            return model;

        }

        //deletes an item from the cart
        [HttpDelete]
        public IActionResult DeleteItem(string itemid)
        {
            
            _cartRepo.RemoveCartItem(itemid);
    

            var message = new
            {
                msg = $"{itemid} is removed from the cart"

            };

            return Ok(JsonConvert.SerializeObject(message));
        }

       [HttpDelete]
        public IActionResult ReduceItemCount(string itemid)
        {
            CartItemModel deleted = _cartRepo.RemoveCartItemByAirCraftTag(itemid);
            AirCraftModel deletedAirCraft = _aircraftRepo.Find(deleted.AirCraftId);

            var message = new
            {
                msg = $"{deletedAirCraft.AircraftType} is removed from the cart"

            };

            return Ok(JsonConvert.SerializeObject(message));
        }

        // GET: CartController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
