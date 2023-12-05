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


        [HttpPost]
        public IActionResult ProcessPayment(PaymentModel payment)
        {


            var currency = "usd"; // Currency code
            var successUrl = "https://localhost:44331/payment/paymentsuccess";
            var cancelUrl = "https://localhost:44331/payment/paymentfailed";
            StripeConfiguration.ApiKey = _key.GetSecretKey();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },


                //items
                LineItems = new List<SessionLineItemOptions> {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency=currency,
                            UnitAmount = Convert.ToInt32(payment.Amount) * 100,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Product Name",
                                Description = "Product Description"
                            }
                        }
                        ,
                        Quantity=1
                    }
                },

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
            return View();
        }

        public IActionResult PaymentFailed()
        {
            return View();
        }
    } 
}
