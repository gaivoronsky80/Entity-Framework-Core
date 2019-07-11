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
    public class LogoutController : Controller
    {
        private MyContext dbContext;
        public LogoutController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}