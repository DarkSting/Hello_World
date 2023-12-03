﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaythosAerospace.Models;
using RaythosAerospace.Models.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository _repo;

        public HomeController(IUserRepository repo)
        {
            _repo = repo;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Insert()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
