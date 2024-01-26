using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaythosAerospace.Models.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Controllers
{
    public class UserController : Controller
    {


        private readonly IUserRepository _userRepo;
        private readonly JWTController _jwt;

        public UserController(IUserRepository userRepo,JWTController jwt)
        {
            _userRepo = userRepo;
            _jwt = jwt;
        }
        // GET: UserController
        public IActionResult Dashboard()
        {
            return View();
        }

        //posts the updated user data
        [HttpPost]
        public IActionResult ProfileUpdate(UserModel model)
        {
            _userRepo.UpdateUser(model);
            return View(model);
        }

        // gets the user from the cookie and retreive the user view
        [HttpGet]
        public IActionResult ProfileUpdate()
        {
            UserModel userModel = _jwt.GetUserFromTheCookies("JWT", Request);
            if (userModel == null)
            {

                return View("~/Views/Shared/Error.cshtml");
            }
            return View(_userRepo.Find(userModel.UserId));
        }

        public IActionResult Login(UserLoginDTO user,IResponseCookies cookies)
        {

            bool isValid = _userRepo.ValidateLogin(user.Credentials.Email,user.Credentials.Password);
            if (isValid)
            {
                string token = _jwt.AssignToken(user.Credentials.Email);
                _jwt.AttachToken(token, cookies);


                return RedirectToAction("Dashboard","User"); 
            }
            else
            {   
                
               ModelState.AddModelError(string.Empty, "Invalid credentials");
                
                
                return View();
            }

        }

        public IActionResult Register(UserLoginDTO user)
        {

            if (user.Credentials.Password != user.ConfirmedPass)
            {
                ModelState.AddModelError(string.Empty, "Password mismatch");
                return View();
            }

            UserModel newModel = user.Credentials;
            _userRepo.RegisterUser(newModel,user.Credentials.Password);

            return View("Login");
        }

        public ViewResult Success()
        {
            return View();
        }

      
    }
}
