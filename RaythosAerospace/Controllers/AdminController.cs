﻿using Microsoft.AspNetCore.Mvc;
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
    }
}
