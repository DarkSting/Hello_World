using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RaythosAerospace.Models.Repositories.AirCraftRepository;
using RaythosAerospace.Models.Repositories.InvoiceRepository;
using RaythosAerospace.Models.Repositories.OrderRepository;
using RaythosAerospace.Models.Repositories.PaymentRepository;
using RaythosAerospace.Models.Repositories.ProductRepository;
using RaythosAerospace.Models.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderRepository _orderRepo;
        private readonly IAirCraftRepository _airCraftRepo;
        private readonly IUserRepository _userRepo;
        private readonly IProductRepository _productRepo;
        private readonly JWTController _jwtController;


        public OrderController(IOrderRepository orderRepo,IAirCraftRepository airCraftRepo,IUserRepository userRepo,
            IProductRepository productRepo, JWTController jwtController
            )
        {

            _orderRepo = orderRepo;
            _airCraftRepo = airCraftRepo;
            _userRepo = userRepo;
            _productRepo = productRepo;
            _jwtController = jwtController;

        }
   
        //get all orders for a customer
        [HttpGet]

        public IActionResult ClientOrderHistory()
        {

            //get the current logged users id from the jwt token
            UserModel userModel = _jwtController.GetUserFromTheCookies("JWT", Request);

            if (userModel == null)
            {

                return View("~/Views/Shared/Error.cshtml");
            }


            IList<OrderModel> foundOrders = _orderRepo.GetAllOrdersForAUser(userModel.UserId);

            Dictionary<string, IList<ProductModel>> orderDesc = new Dictionary<string, IList<ProductModel>>();

            //iterating each order and getting the products related to the order
            foreach (OrderModel current in foundOrders)
            {

                orderDesc.Add(current.OrderId, new List<ProductModel>());
                IList<ProductModel> foundProducts = _productRepo.GetAllProductsForAnOrder(current.OrderId);

                //iterating the aircrafts and assign their ids to the aircraft products
                foreach (ProductModel currentProduct in foundProducts)
                {
                    currentProduct.AirCraft = new AirCraftModel();
                    currentProduct.AirCraft.AircraftType = _airCraftRepo.Find(currentProduct.AirCraftId).AircraftType;
                }

                orderDesc[current.OrderId] = foundProducts;

            }

            OrderDTO orderDTO = new OrderDTO
            {
                orders = foundOrders,
                orderdesc = orderDesc
            };

            return View(orderDTO);
        }


        ///getting tracking status of the order
        public IActionResult OrderTracking(string orderId)
        {

            //getting order details from the order repository
            OrderModel foundOrder = _orderRepo.Find(orderId);

         
            //assign the products for the retreived order
            IList<ProductModel> productDesc = _productRepo.GetAllProductsForAnOrder(foundOrder.OrderId);
          
            //iterating the aircrafts and assign them to the aircraft products
            foreach (ProductModel currentProduct in productDesc)
            {
                currentProduct.AirCraft = _airCraftRepo.Find(currentProduct.AirCraftId);
                currentProduct.Customize = _airCraftRepo.GetCustomization(currentProduct.CustomizationId);
            }

            //data trasfer object which contains the details that will be displayed on the view
            OrderDetailsDTO orderDTO = new OrderDetailsDTO
            {
                order = foundOrder,
                products = productDesc
            };

            return View(orderDTO);
        }


        //creating the order
        public OrderModel InitiateOrder(PurchaseViewModel model)
        {

            //getting the shipping details which was selected by the customer
            ShippingModel shippingMethod = _orderRepo.GetShipping(model.shippingId);

            //populate the order details
            OrderModel newOrder = new OrderModel
            {
                OrderDateTime = DateTime.Now.Date,
                OrderId = "OD-" + Guid.NewGuid().ToString(),
                Discounts = 0,
                ShippingAddress = model.shippingAddress,
                ShippingId = shippingMethod.ShippingId,
                ShippingCost = shippingMethod.ShippingCost,
                TotalAmount = model.totalPrice,
                EstimatedDeliveryDate = DateTime.Now.AddMonths(3),
                OrderStatus = OrderStatusEnum.Initiating.ToString(),
                PaymentMethod = "Online",
                UserId = model.UserId,
                Subtotal = model.totalPrice,
                ShippingMethod = Delivery.TransportByShipping.ToString()

            };

            //creating the order
            _orderRepo.Create(newOrder);

            //assign the order ids to the product
            foreach(Product current in model.Products)
            {
                _productRepo.AssigningOrderIDsTotheProduct(newOrder.OrderId, current.ProductID);
            }

            return newOrder;

        }


        //get all orders of customers 
        [HttpGet]
        public IActionResult AdminGetAllOrders()
        {
            OrderDTO foundOrders = new();

            foundOrders.orders = _orderRepo.GetAllOrders();

            foundOrders.orderedUsers = new Dictionary<string, UserModel>();

           foreach(OrderModel current in foundOrders.orders)
            {
                if (!foundOrders.orderedUsers.ContainsKey(current.OrderId))
                {
                    UserModel foundUser = _userRepo.Find(current.UserId);
                    foundUser.Password = "";
                    foundOrders.orderedUsers.Add(current.OrderId, foundUser);
                }
            }

            return View(foundOrders);


        }

        //preparing the values for the dropdowns
        private void PrepareDataForLoadPage(string? userid, string? aircraftid,dynamic bag)
        {
            AirCraftModel foundCraft = _airCraftRepo.Find(aircraftid == null ? "A0001" : aircraftid);
            UserModel foundUser = _userRepo.Find(userid == null ? "U0001" : userid);


            //populating the airbag
            bag.AirCraft = foundCraft;
            bag.User = foundUser;
            bag.PaymentMethod = Enum.GetValues(typeof(OrderStatusEnum))
                            .Cast<OrderStatusEnum>()
                            .Select(e => new SelectListItem
                            {
                                Value = e.ToString(),
                                Text = e.ToString()
                            }).ToList();
            bag.OrderStatus = Enum.GetValues(typeof(PaymentMethodEnum))
                            .Cast<PaymentMethodEnum>()
                            .Select(e => new SelectListItem
                            {
                                Value = e.ToString(),
                                Text = e.ToString()
                            }).ToList();
            bag.Shippings = _orderRepo.GetShippingMethods();
        }

      

      
    }
}
