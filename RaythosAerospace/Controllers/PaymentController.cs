using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaythosAerospace.CustomServices;
using RaythosAerospace.Models.Repositories.PaymentRepository;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RaythosAerospace.Models.Repositories.CartRepository;
using RaythosAerospace.Models.Repositories.AirCraftRepository;
using Microsoft.AspNetCore.Cors;

namespace RaythosAerospace.Controllers
{


    public class PaymentController : Controller
    {
        private readonly ICustomService _key;
        ILogger<PaymentController> _logger;

        public PaymentController(ICustomService keys, ILogger<PaymentController> logger)
        {
            _key = keys;
            _logger = logger;
            logger.LogDebug(keys.GetPublicKey());
        }


        public IActionResult Checkout(PaymentModel view)
        {
            _logger.LogError(_key.GetSecretKey());
            ViewBag.publicKey = _key.GetPublicKey().Trim();

            return View();
        }


        
        
        public IActionResult ProcessPayment(PaymentModel payment)
        {

            PaymentModel deserializedObj = payment;

            var currency = "usd"; // Currency code
            var successUrl = "https://localhost:44331/payment/paymentsuccess";
            var cancelUrl = "https://localhost:44331/payment/paymentfailed";
            StripeConfiguration.ApiKey = _key.GetSecretKey();

            List<SessionLineItemOptions> items = new List<SessionLineItemOptions>();
            
            //attaching items 
            foreach (AirCraftModel current in deserializedObj.aircrafts.Values)
            {
                string aircraftdesc = current.AircraftType;

                int count = 0;
                double price = 0;

                foreach (CartItemModel model in deserializedObj.cartitems)
                {
                    if (model.AirCraftId == current.AircraftId)
                    {
                        count++;
                        price += model.UnitPrice;

                    }
                }

                items.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = currency,
                        UnitAmount =(long)price,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = current.AircraftType,
                            Description = "test"
                        }
                    }
                        ,
                    Quantity = count
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },



                LineItems = items,

                Mode = "payment",
                    SuccessUrl = successUrl,
                    CancelUrl = cancelUrl,
                };
            //options


            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);

        }

        public IActionResult PaymentSuccess()
        {
            return View();
        }

        public IActionResult PaymentFailed()
        {
            return View();
        }
    } 
}
