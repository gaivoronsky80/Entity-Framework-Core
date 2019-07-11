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
    public class RegisterController : Controller
    {
        private MyContext dbContext;
        public RegisterController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(User user)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(i => i.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Register");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                
                var thisUser = dbContext.Users.FirstOrDefault(i => i.Email == user.Email);
                HttpContext.Session.SetInt32("UserID", thisUser.UserId);
                int? IntVariable = HttpContext.Session.GetInt32("UserID");
                System.Console.WriteLine(IntVariable);
                return RedirectToAction("Login", "Login");
            }
            return View("Register");
        }
    }
}