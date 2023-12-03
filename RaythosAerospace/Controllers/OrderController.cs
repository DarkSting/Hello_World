using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaythosAerospace.Models.Repositories.AirCraftRepository;
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
        public ActionResult Create(string? airCraftId, string? userId)
        {
            AirCraftModel foundCraft = _airCraftRepo.Find("A0001");
            UserModel foundUser = _userRepo.Find("U0001");

            ViewBag.AirCraft = foundCraft;
            ViewBag.User = foundUser;

            return View();
        }

        // POST: OrderController/Create
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

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
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
