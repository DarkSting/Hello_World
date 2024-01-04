using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly JWTController _jwt;
        public AdminController(IAdminRepository adminrepo,JWTController jwt)
        {
            _adminRepo = adminrepo;
            _jwt = jwt;
        }

        // GET: /Admin/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Admin/Register
        [HttpPost]
        public IActionResult Register(AdminLoginDTO admin)
        {
            if (admin.credintials.Password != admin.confirmedPass)
            {
                return View();
            }
            _adminRepo.RegisterAdmin(admin.credintials,admin.credintials.Password);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AircraftManagement()
        {
            return View();
        }

        // GET: /Admin/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Admin/Login
        [HttpPost]
        public IActionResult Login(AdminModel viewmodel)
        {

            bool isValid = _adminRepo.ValidateLogin(viewmodel.Email, viewmodel.Password);

            if (isValid)
            {
                string token = _jwt.AssignToken(viewmodel.Email);
                _jwt.AttachToken(token, Response.Cookies);


                return RedirectToAction("Dashboard"); // Redirect to a dashboard or home page
            }
            else
            {
             
                 ModelState.AddModelError(string.Empty, "Ivalid Login");
                
                return View();
            }
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
