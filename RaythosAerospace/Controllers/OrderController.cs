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
        // GET: OrderController
        public ActionResult Index()
        {
           
            return View();
        }

        public IActionResult ViewOrders()
        {
            return View();
        }

        public IActionResult ViewAnOrderForUser(string orderId)
        {
            return View();
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        [HttpGet]

        public IActionResult ClientOrderHistory()
        {


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
                IList<ProductModel> foundProducts = _productRepo.GetProductsByUser(userModel.UserId);

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



        [HttpGet]

        public IActionResult OrderHistory()
        {


            UserModel userModel = _jwtController.GetUserFromTheCookies("JWT",Request);

            if (userModel==null){

                return View("~/Views/Shared/Error.cshtml");
            }

            
            IList<OrderModel> foundOrders = _orderRepo.GetAllOrdersForAUser(userModel.UserId);
            Dictionary<string, IList<ProductModel>> orderDesc = new Dictionary<string, IList<ProductModel>>();
            
            //iterating each order and getting the products related to the order
            foreach(OrderModel current in foundOrders)
            {
               
                orderDesc.Add(current.OrderId, new List<ProductModel>());
                IList<ProductModel> foundProducts = _productRepo.GetProductsByUser(userModel.UserId);

                //iterating the aircrafts and assign their ids to the aircraft products
                foreach(ProductModel currentProduct in foundProducts)
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

        public IActionResult OrderTracking(string orderId)
        {


            OrderModel foundOrder = _orderRepo.Find(orderId);
            IList<ProductModel> productDesc = _productRepo.GetProductsByUser(foundOrder.UserId);
          
            //iterating the aircrafts and assign them to the aircraft products
            foreach (ProductModel currentProduct in productDesc)
            {
                currentProduct.AirCraft = _airCraftRepo.Find(currentProduct.AirCraftId);
                currentProduct.Customize = _airCraftRepo.GetCustomization(currentProduct.CustomizationId);
            }


            OrderDetailsDTO orderDTO = new OrderDetailsDTO
            {
                order = foundOrder,
                products = productDesc
            };

            return View(orderDTO);
        }

        public OrderModel InitiateOrder(PurchaseViewModel model)
        {

            ShippingModel shippingMethod = _orderRepo.GetShipping(model.shippingId);


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

            _orderRepo.Create(newOrder);

            foreach(Product current in model.Products)
            {
                _productRepo.AssigningOrderIDsTotheProduct(newOrder.OrderId, current.ProductID);
            }

            return newOrder;

        }

        // GET: OrderController/Create
        [HttpGet]
        public ViewResult Create(string? airCraftId, string? userId)
        {
            PrepareDataForLoadPage(userId, airCraftId, ViewBag);

            return View();
        }

        [HttpGet]
        public IActionResult AdminGetAllOrders()
        {
            OrderDTO foundOrders = new();

            foundOrders.orders = _orderRepo.GetAllOrders();

            return View(foundOrders);


        }

        [HttpGet]
        public IActionResult OrderEditing(string userId)
        {

            return View();
        }

        [HttpGet]
        public IActionResult OrderEditing(OrderDetailsDTO dto)
        {

            return View();
        }

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

        // POST: OrderController/Create
        [HttpPost]
        public IActionResult Create(OrderModel model)
        {
            

            if (ModelState.IsValid)
                {
                    _orderRepo.Create(model);
                    return RedirectToAction("Home/Index");
                }


            //PrepareDataForLoadPage(model.UserId, model.AirCraftId, ViewBag);
            return View();
        }

        // GET: OrderController/Edit/5
        [HttpGet]
        public ActionResult Edit(string id)
        {
            OrderModel foundModel = _orderRepo.Find(id);

            return View(foundModel);
        }

        public ActionResult Orders()
        {
            IEnumerable<OrderModel> models = _orderRepo.GetAllOrders();
            return View(models);
        }

      
    }
}
