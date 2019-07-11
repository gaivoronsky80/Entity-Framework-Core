using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LoginAndRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoginAndRegistration.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index(User user)
        {
            return View("Index");
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create(User user)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return RedirectToAction("Index");
                } else {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    return RedirectToAction("Success");
                }
            } else {
                return RedirectToAction("Index");
            }
        }

        [Route("login")]
        [HttpGet]
        public IActionResult Login(User user)
        {
            return View();
        }

        [Route("enter")]
        [HttpGet]
        [HttpPost]
        public IActionResult Enter(LoginUser userSubmission)
        {
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == userSubmission.Email);
                if(userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Login");
                } else {
                    var hasher = new PasswordHasher<LoginUser>();
                    var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);
                    if(result == 0)
                    {
                        ModelState.AddModelError("Password", "Password incorect!");
                        return RedirectToAction("Login");
                    } else {
                        return RedirectToAction("Success");
                    }
                }

            } else {
                return RedirectToAction("Login");
            }
        }

        [Route("success")]
        [HttpGet]
        public IActionResult Success()
        {
            List<User> AllUsers = dbContext.Users.ToList();
            return View(AllUsers);
        }

        [Route("delete/{id}")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            User user = dbContext.Users.FirstOrDefault(d => d.UserId == id);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
            return RedirectToAction("Success");
        }
    }
}
