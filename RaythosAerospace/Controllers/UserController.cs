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

        public IActionResult Login(UserModel user,IResponseCookies cookies)
        {

            bool isValid = _userRepo.ValidateLogin(user.Email,user.Password);
            if (isValid)
            {
                string token = _jwt.AssignToken(user.Email);
                _jwt.AttachToken(token, cookies);


                return RedirectToAction("Dashboard","User"); 
            }
            else
            {

                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return View();
            }

        }

        public IActionResult Register(UserModel user)
        {
            _userRepo.RegisterUser(user,user.Password);
            return View("Login");
        }

        public ViewResult Success()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
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

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
