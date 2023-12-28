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

        // GET: /Admin/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Admin/Register
        [HttpPost]
        public IActionResult Register(AdminModel admin)
        {
            _adminRepo.RegisterAdmin(admin,admin.Password);
            return RedirectToAction("Login");
        }

        // GET: /Admin/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Admin/Login
        [HttpPost]
        public IActionResult Login(AdminViewModel viewmodel)
        {

            bool isValid = _adminRepo.ValidateLogin(viewmodel.Email, viewmodel.Password);

            if (isValid)
            {
                // Successful login logic (e.g., set authentication cookie, redirect, etc.)
                return RedirectToAction("Index", "Home"); // Redirect to a dashboard or home page
            }
            else
            {
                // Handle invalid login (e.g., return to login page with error message)
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return View(viewmodel);
            }
        }
    }
}
