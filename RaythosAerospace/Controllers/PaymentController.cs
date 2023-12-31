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
using RaythosAerospace.Models.Repositories.OrderRepository;
using RaythosAerospace.Models.Repositories.ProductRepository;

namespace RaythosAerospace.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ICustomService _key;
        ILogger<PaymentController> _logger;
        private readonly IAirCraftRepository _airCraftRepo;
        private readonly OrderController _orderController;
        private readonly CartController _cartController;
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        public PaymentController(ICustomService keys, ILogger<PaymentController> logger,IAirCraftRepository airCraftRepository,
            OrderController orderController,CartController cartController,IOrderRepository orderRepo,IProductRepository  productRepo
            )
        {
            _key = keys;
            _logger = logger;
            logger.LogDebug(keys.GetPublicKey());
            _airCraftRepo = airCraftRepository;
            _orderController = orderController;
            _cartController = cartController;
            _orderRepo = orderRepo;
            _productRepo = productRepo;

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

            //validating the inputs
            if (payment.shippingId==null || payment.shippingAddress==null)
            {
                payment.aircrafts = null;
                PaymentModel errorModel = _cartController._prepareDataForLoad(ViewBag, payment.UserId);
                return View("~/Views/Cart/ViewUserCart.cshtml", errorModel); 
            }

            var currency = "usd"; // Currency code
            var successUrl = "https://localhost:44331/payment/paymentsuccess";
            var cancelUrl = "https://localhost:44331/payment/paymentfailed";
            StripeConfiguration.ApiKey = _key.GetSecretKey();

            //stripe session items
            List<SessionLineItemOptions> items = new List<SessionLineItemOptions>();

            //purchas view model to track details for the order controller
            PurchaseViewModel purchasedet = new PurchaseViewModel();

            //gets shipping model
            ShippingModel shipmodel = _orderRepo.GetShipping(payment.shippingId);
            //set the id of the client who purchase the products

            purchasedet.UserId = payment.UserId;
            purchasedet.shippingId = payment.shippingId;
            purchasedet.shippingAddress = payment.shippingAddress;

            try
            {
                foreach (CheckoutModel current in payment.aircrafts.Values)
                {
                    if (current.IsSelected)
                    {

                        AirCraftModel airCraftModel = _airCraftRepo.Find(current.AirCraft.AircraftId);
                      

                        //adding products for purchases 
                        Models.Repositories.PaymentRepository.Product currentProduct = new Models.Repositories.PaymentRepository.Product
                        {

                            AirCraftId = airCraftModel.AircraftId,
                            ProductID = current.ProductId,
                            Count = current.Count,
                            UnitPrice = Convert.ToInt32(current.ProductTotalCost)+shipmodel.ShippingCost,


                        };

                        purchasedet.Products.Add(currentProduct);
                        purchasedet.totalPrice += currentProduct.UnitPrice * currentProduct.Count;

                        items.Add(new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = currency,
                                UnitAmount = Convert.ToInt32(currentProduct.UnitPrice) * 100,
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = airCraftModel.AircraftType,
                                    Description = (airCraftModel.AirCraftPrice<currentProduct.UnitPrice?"Customization added":"Standard")+ "+ Shipping Price Included"
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
            catch(Exception e)
            {
                return View();
            }
            

        }

        public IActionResult PaymentSuccess()
        {
            PurchaseViewModel model = JsonConvert.DeserializeObject<PurchaseViewModel>((TempData["PurchaseData"] as string));

            TempData["PurchaseData"] = null;

           OrderModel createdOrder =  _orderController.InitiateOrder(model);

            return View(createdOrder);
        }

        public IActionResult PaymentFailed()
        {
            return View();
        }
    } 
}
;