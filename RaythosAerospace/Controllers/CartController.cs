﻿using Microsoft.AspNetCore.Http;
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
using RaythosAerospace.Models.Repositories.OrderRepository;
using RaythosAerospace.Models.Repositories.UserRepository;

namespace RaythosAerospace.Controllers
{
    public class CartController : Controller
    {

        private readonly ICartRepository _cartRepo;
        private readonly IAirCraftRepository _aircraftRepo;
        private readonly IProductRepository _productRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly JWTController _jwtController;

        public CartController(ICartRepository cartRepo, IAirCraftRepository aircraftRepo,IProductRepository productRepo,
            IOrderRepository orderRepo, JWTController jwtController
            )
        {

            _cartRepo = cartRepo;
            _aircraftRepo = aircraftRepo;
            _productRepo = productRepo;
            _orderRepo = orderRepo;
            _jwtController = jwtController;

        }

        
        //POST
        //adds the product to the cart
        [HttpPost]
        public IActionResult AddToCart(CartItemViewModel item)
        {
            UserModel userModel = _jwtController.GetUserFromTheCookies("JWT", Request);

            if (userModel == null)
            {

                return View("~/Views/Shared/Error.cshtml");
            }

            CartModel foundCart = _cartRepo.GetCart(userModel.UserId);

            AirCraftModel foundcraft = _aircraftRepo.Find(item.aircraft.AircraftId);

            //checking what are the null properties of the model
            var properties = typeof(CartItemViewModel).GetProperties();

            CustomizationModel custom = new CustomizationModel();
            bool isCustomizationEnabled = false;
            custom.CustomId = Guid.NewGuid().ToString();

            foreach(var property in properties)
            {
                var value = property.GetValue(item);
                string propname = property.Name;

                if (value != null && propname== "ColorId")
                {

                    isCustomizationEnabled = true;
                    custom.ExteriorColorId = value as string;

                }
                else if(value != null && propname == "InteriorColorId")
                {
                    isCustomizationEnabled = true;
                    custom.InteriorColorId = value as string;
                }
                else if(value != null && propname == "SeatId")
                {
                    isCustomizationEnabled = true;
                    custom.SeatId = value as string;
                }
            }

            if (isCustomizationEnabled)
            {
                _aircraftRepo.AddCustomization(custom);
            }

            ///assign product model
            if (foundcraft.ItemCount > item.count)
            {
                ProductModel product = new ProductModel
                {
                    AirCraftId = item.aircraft.AircraftId,
                    CartId = foundCart.CartNumber,
                    AddedDate = DateTime.Now.Date,
                    Count = item.count,
                    UnitPrice = (int)item.aircraft.AirCraftPrice,
                    UserId = userModel.UserId,
                    ProductId = Guid.NewGuid().ToString(),
                    CustomizationId = isCustomizationEnabled?custom.CustomId:null

                };
                item.productAdded = true;
                _productRepo.AddProduct(product);
            }

            //reset the model to load the view
            item = null;
            item = new CartItemViewModel();
            item.aircraft = foundcraft;


            return RedirectToAction("AircraftPage", "Aircraft", new { airCraftId = item.aircraft.AircraftId });
        }

        // GET: CartController
        [HttpGet]
        public IActionResult ViewUserCart(string userid)
        {

            UserModel userModel = _jwtController.GetUserFromTheCookies("JWT", Request);

            if (userModel == null)
            {

                return View("~/Views/Shared/Error.cshtml");
            }

            return View(_prepareDataForLoad(ViewBag, userModel.UserId));
        }

      


        public ViewResult Checkout(PaymentModel model)
        {
            return View("~/Views/Payment/Checkout.cshtml",model);
        }

        public PaymentModel _prepareDataForLoad(dynamic bag,string userid)
        {
            //getting the cart and cart items for a given user
            CartModel foundCart = _cartRepo.GetCart(userid);
            Dictionary<string, CheckoutModel> aircrafts = new Dictionary<string, CheckoutModel>();
            IEnumerable<ProductModel> cartitems = _productRepo.GetProductAddedToTheCartByUser(userid);
            IEnumerable<ShippingModel> shippings = _orderRepo.GetShippingMethods();

            //iterating the aircrafts and adding them into the dictionary
            foreach(ProductModel current in cartitems)
            {
                if (!aircrafts.ContainsKey(current.ProductId))
                {
                    AirCraftModel foundAirCraft = _aircraftRepo.Find(current.AirCraftId);
                    ProductModel product = _productRepo.GetProduct(current.ProductId);
                    CustomizationModel customization = _aircraftRepo.GetCustomization(product.CustomizationId);



                    CheckoutModel temp = new CheckoutModel
                    {
                        AirCraft = foundAirCraft,
                        ProductId = current.ProductId,
                        Product = product,
                        Customization = customization,
                        ProductTotalCost = _aircraftRepo.CalculateTotalPriceForAirCraft(current),
                        Count = current.Count,
                        ExteriorClr =  _aircraftRepo.GetColor(customization==null?null:customization.ExteriorColorId),
                        InteriorClr = _aircraftRepo.GetColor(customization == null ? null : customization.InteriorColorId),
                        Seat = _aircraftRepo.GetSeat(customization == null ? null : customization.SeatId)


                    };

                    aircrafts.Add(current.ProductId, temp);


                }
            }

            bag.Cart = foundCart;
            bag.CartItems = cartitems;
            bag.AirCrafts = aircrafts;
            bag.Shippings = shippings;

            PaymentModel model = new PaymentModel
            {
                aircrafts = aircrafts,
                cartitems = cartitems,
                UserId = userid
                
            };

            return model;

        }

        //deletes an item from the cart
        [HttpDelete]
        public IActionResult DeleteItem(string itemid)
        {

    
           ProductModel deletedModel = _productRepo.DeleteProduct(itemid);

            _aircraftRepo.RemoveCustomization(deletedModel.CustomizationId);

            var message = new
            {
                msg = $"{itemid} is removed from the cart"

            };

            return Ok(JsonConvert.SerializeObject(message));

        }

       [HttpDelete]
        public IActionResult ReduceItemCount(string itemid)
        {
            _productRepo.ReduceProductCountByOne(itemid);

            var message = new
            {
                msg = $"{itemid} is removed from the cart"

            };

            return Ok(JsonConvert.SerializeObject(message));
        }

        [HttpPost]
        public IActionResult RemoveCustomization([FromBody]RemoveElementDTO model)
        {


            _aircraftRepo.UpdateCustomization(model);


            var message = new
            {
                msg = $"{model.customtype} customization successfully removed"
            };

            return Ok(JsonConvert.SerializeObject(message));
        }



    }
}
