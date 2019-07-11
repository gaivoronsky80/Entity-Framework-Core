using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PartyFinder.Models;

namespace PartyFinder.Controllers
{
    public class LoginController : Controller
    {
        private MyContext dbContext;
        public LoginController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("signup")]
        public IActionResult Signup(User user)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(e => e.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already exist!");
                    return View("Register");
                }

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                dbContext.Users.Add(user);
                dbContext.SaveChanges();

                var thisuser = dbContext.Users.FirstOrDefault(e => e.Email == user.Email);
                HttpContext.Session.SetInt32("UserID", thisuser.UserId);
                int? IntVariable = HttpContext.Session.GetInt32("UserID");
                System.Console.WriteLine(IntVariable);
                return RedirectToAction("Index", "Home");
            }
            return View("Register");
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("signin")]
        public IActionResult Signin(LoginUser user)
        {
            if(ModelState.IsValid)
            {
                var thisuser = dbContext.Users.FirstOrDefault(e => e.Email == user.Email);
                if(thisuser == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email or Password");
                    return View("Login");
                }

                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(user, thisuser.Password, user.Password);
                if(result == 0)
                {
                    ModelState.AddModelError("Email", "Invalid Email or Password");
                    return View("Login");
                }

                HttpContext.Session.SetInt32("UserID", thisuser.UserId);
                int? IntVariable = HttpContext.Session.GetInt32("UserID");
                System.Console.WriteLine(IntVariable);
                return RedirectToAction("Index", "Home");
            }
            System.Console.WriteLine("Model not Valid");
            return View("Login");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}