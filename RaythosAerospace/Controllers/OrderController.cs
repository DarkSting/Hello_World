using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RaythosAerospace.Models.Repositories.AirCraftRepository;
using RaythosAerospace.Models.Repositories.InvoiceRepository;
using RaythosAerospace.Models.Repositories.OrderRepository;
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


        public OrderController(IOrderRepository orderRepo,IAirCraftRepository airCraftRepo,IUserRepository userRepo)
        {
            _orderRepo = orderRepo;
            _airCraftRepo = airCraftRepo;
            _userRepo = userRepo;
        }
        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        [HttpGet]
        public ViewResult Create(string? airCraftId, string? userId)
        {
            PrepareDataForLoadPage(userId, airCraftId, ViewBag);

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
            //PrepareDataForLoadPage(foundModel.UserId,foundModel.AirCraftId,ViewBag);
            return View(foundModel);
        }

        public ActionResult Orders()
        {
            IEnumerable<OrderModel> models = _orderRepo.GetAllOrders();
            return View(models);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        public IActionResult Edit(OrderModel model)
        {

            if (ModelState.IsValid)
            {
                _orderRepo.Update(model);
            }

            //PrepareDataForLoadPage(model.UserId, model.AirCraftId, ViewBag);

            return View();
            
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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
