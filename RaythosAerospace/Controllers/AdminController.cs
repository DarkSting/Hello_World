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
                string token = _jwt.AssignToken(viewmodel.Email);
                _jwt.AttachToken(token, Response.Cookies);


                return RedirectToAction("Index", "Home"); // Redirect to a dashboard or home page
            }
            else
            {
                
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return View(viewmodel);
            }
        }
    }
}
