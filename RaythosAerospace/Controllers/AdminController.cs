using Microsoft.AspNetCore.Mvc;
using RaythosAerospace.Models.Repositories.AdminRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepo;

        public AdminController(IAdminRepository adminrepo)
        {
            _adminRepo = adminrepo;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult AdminRegistration()
        {
            return View();
        }

        public IActionResult AircraftManagement()
        {
            return View();
        }

        public IActionResult AddAircraft()
        {
            return View();
        }

        public IActionResult ManageAircrafts()
        {
            return View();
        }

        public IActionResult ManageAircraftsPage()
        {
            return View();
        }

        public IActionResult InventoryManagement()
        {
            return View();
        }

        public IActionResult AddInventoryItem()
        {
            return View();
        }

        public IActionResult ManageInventory()
        {
            return View();
        }

        public IActionResult ManageInventoryItem()
        {
            return View();
        }

        public IActionResult CustomerManagement()
        {
            return View();
        }
    }
}
