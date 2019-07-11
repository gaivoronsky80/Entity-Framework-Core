using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BankAccount.Models;

namespace BankAccount.Controllers
{
    public class LoginController : Controller
    {
        private MyContext dbContext;
        public LoginController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("enter")]
        public IActionResult Enter(LoginUser user)
        {
            if(ModelState.IsValid)
            {
                var thisUser = dbContext.Users.FirstOrDefault(i => i.Email == user.Email);
                if(thisUser == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Login");
                }

                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(user, thisUser.Password, user.Password);
                if(result == 0)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Login");
                }
                HttpContext.Session.SetInt32("UserID", thisUser.UserId);
                int? IntVariable = HttpContext.Session.GetInt32("UserID");
                System.Console.WriteLine(IntVariable);
                System.Console.WriteLine("Model valid");
                return Redirect($"/account/{IntVariable}"); 
            }
            System.Console.WriteLine("Model not valid");
            return View("Login");
        }
    }
}