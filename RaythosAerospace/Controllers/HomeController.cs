using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaythosAerospace.Models;
using RaythosAerospace.Models.Repositories.CartRepository;
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
        private readonly IUserRepository _repo;
        private readonly UserController _userController;
        private readonly ICartRepository _cartRepo;

        public HomeController(IUserRepository repo,UserController userController,ICartRepository cartRepo)
        {
            _repo = repo;
            _userController = userController;
            _cartRepo = cartRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {

            return View();
        }



        [HttpPost]
        public IActionResult Registration(UserModel user)
        {

            if (ModelState.IsValid)
            {

                //creates a new cart and assign the new user
                CartModel newCart = new CartModel
                {
                    CartNumber = "CT-" + Guid.NewGuid().ToString(),
                    UseId = user.UserId,
                    Description = user.Name+" "+"Cart"
                };
                
              IActionResult result = _userController.Register(user);
              _cartRepo.CreateCart(newCart);

                return result;
            }

            return View();
           
        }



        [HttpPost]
        public IActionResult Login(UserLoginDTO logincred)
        {
            return  _userController.Login(logincred,Response.Cookies);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
