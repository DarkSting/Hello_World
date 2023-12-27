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
using Microsoft.AspNetCore.Cors;

namespace RaythosAerospace.Controllers
{
    public class CartController : Controller
    {

        private readonly ICartRepository _cartRepo;
        private readonly IAirCraftRepository _aircraftRepo;

        public CartController(ICartRepository cartRepo, IAirCraftRepository aircraftRepo)
        {

            _cartRepo = cartRepo;
            _aircraftRepo = aircraftRepo;

        }

        
        [HttpPost]
        public IActionResult AddToCart([FromBody]CartItemViewModel item)
        {
            string userId = "U0001";

            CartModel foundCart = _cartRepo.GetCart(userId);

            CartItemModel cartitem = new CartItemModel
            {
                AirCraftId = item.aircraft.AircraftId,
                CartId = foundCart.CartNumber,
                ItemAddedDate = DateTime.Now,
                UnitPrice = item.aircraft.AirCraftPrice,

            };

            _cartRepo.AddCartItem(cartitem);

            return View("~/Views/AirCraft/Customize.cshtml",item );
        }

        // GET: CartController
        [HttpGet]
        public IActionResult ViewUserCart(string userid)
        {

            
            userid = "U0001";

           PaymentModel model = _prepareDataForLoad(ViewBag,userid);

            TempData["StripePayment"] = JsonConvert.SerializeObject(model);

            return View(model);
        }

 
        [HttpPost]
        
        public ViewResult Checkout(PaymentModel paymet)
        {
            string userid = "U0001";

            PaymentModel model = _prepareDataForLoad(ViewBag, userid);

            model.Amount = paymet.Amount;

            TempData["StripePayment"] = JsonConvert.SerializeObject(model);

            return View("~/Views/Payment/Checkout.cshtml",model);
        }

        private PaymentModel _prepareDataForLoad(dynamic bag,string userid)
        {
            //getting the cart and cart items for a given user
            CartModel foundCart = _cartRepo.GetCart(userid);
            Dictionary<string, AirCraftModel> aircrafts = new Dictionary<string, AirCraftModel>();
            IEnumerable<CartItemModel> cartitems = _cartRepo.GetAllCartItems(foundCart.CartNumber);

            //iterating the aircrafts and adding them into the dictionary
            foreach(CartItemModel current in cartitems)
            {
                if (!aircrafts.ContainsKey(current.AirCraftId))
                {
                    AirCraftModel foundAirCraft = _aircraftRepo.Find(current.AirCraftId);

                    //removing circular reference
                    foundAirCraft.CartItems = null;

                    aircrafts.Add(current.AirCraftId, foundAirCraft);
                }

                //removing circular reference
                current.AirCraft = null;
            }

            bag.Cart = foundCart;
            bag.CartItems = cartitems;
            bag.AirCrafts = aircrafts;

            PaymentModel paymentdet = new PaymentModel();

            paymentdet.aircrafts = aircrafts;
            paymentdet.cartitems = cartitems;

            return paymentdet;

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
