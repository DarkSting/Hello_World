using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaythosAerospace.CustomServices;
using RaythosAerospace.Models.Repositories.AirCraftRepository;
using RaythosAerospace.Models.Repositories.CartRepository;
using RaythosAerospace.Models.Repositories.PaymentRepository;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RaythosAerospace.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ICustomService _key;
        ILogger<PaymentController> _logger;
        private readonly IAirCraftRepository _airCraftRepo;

        public PaymentController(ICustomService keys, ILogger<PaymentController> logger,IAirCraftRepository airCraftRepository)
        {
            _key = keys;
            _logger = logger;
            logger.LogDebug(keys.GetPublicKey());
            _airCraftRepo = airCraftRepository;
        }


        public IActionResult Checkout(PaymentModel view)
        {
            _logger.LogError(_key.GetSecretKey());
            ViewBag.publicKey = _key.GetPublicKey().Trim();

            return View();
        }


        [HttpPost]
        public IActionResult ProcessPayment(PaymentModel payment)
        {


            var currency = "usd"; // Currency code
            var successUrl = "https://localhost:44331/payment/paymentsuccess";
            var cancelUrl = "https://localhost:44331/payment/paymentfailed";
            StripeConfiguration.ApiKey = _key.GetSecretKey();

            List<SessionLineItemOptions> items = new List<SessionLineItemOptions>();

            PurchaseViewModel purchasedet = new PurchaseViewModel();

            //set the id of the client who purchase the products

            foreach(CheckoutViewModel current in payment.aircrafts.Values)
            {
                if (current.IsSelected)
                {
                    
                    AirCraftModel airCraftModel = _airCraftRepo.Find(current.AirCraft.AircraftId);

                    //adding products for purchases 
                    Models.Repositories.PaymentRepository.Product currentProduct = new Models.Repositories.PaymentRepository.Product
                    {
                        ProductID = airCraftModel.AircraftId,
                        Count = current.Count,
                        UnitPrice = Convert.ToInt32(current.UnitPrice),
                       
                       
                    };

                    purchasedet.Products.Add(currentProduct);
                    purchasedet.totalPrice += currentProduct.UnitPrice * currentProduct.Count;

                    items.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = currency,
                            UnitAmount = Convert.ToInt32(airCraftModel.AirCraftPrice)*100,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = airCraftModel.AircraftType,
                                Description = "need Engine"
                            }
                        }
                        ,
                        Quantity = current.Count
                    });
                }
            }

            TempData["PurchaseData"] = JsonConvert.SerializeObject(purchasedet);

            if (items.Count == 0)
            {
                 return RedirectToAction("ViewUserCart", "Cart");
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },


                //items
                LineItems =items,

                Mode="payment",
                SuccessUrl=successUrl,
                CancelUrl=cancelUrl,
            };
            //options


            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);

        }

        public IActionResult PaymentSuccess()
        {
            PurchaseViewModel model = JsonConvert.DeserializeObject<PurchaseViewModel>((TempData["PurchaseData"] as string));
            TempData["PurchaseData"] = null;

            return View(model);
        }

        public IActionResult PaymentFailed()
        {
            return View();
        }
    } 
}
