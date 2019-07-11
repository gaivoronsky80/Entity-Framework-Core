using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BankAccount.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Controllers
{
    public class AccountController : Controller
    {
        private MyContext dbContext;
        public AccountController(MyContext context)
        {
            dbContext = context;
        }

        [Route("account/{id}")]
        [HttpGet]    
        public IActionResult Account(int id)
        {
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Register", "Register");
            }
            int? UserId = HttpContext.Session.GetInt32("UserID");
            User thisUser = dbContext.Users
                .Include(t => t.TransactionsOfUser)
                .FirstOrDefault(t => t.UserId == UserId);
            ViewBag.UserInfo = thisUser;

            float total = 0;
            foreach(Transaction i in thisUser.TransactionsOfUser)
            {
                total += i.Amount;
            };
            ViewBag.Total = total;
            return View();
        }

        [Route("money")]
        [HttpGet] 
        [HttpPost]
        public IActionResult Money(Transaction money)
        {
            float f = money.Amount;
            float truncated = (float)(Math.Truncate((double)f*100.0)/100.0);
            money.Amount = (float)(Math.Round((double)f, 2));

            int? IntVariable = HttpContext.Session.GetInt32("UserID");
            int UserId = IntVariable ?? default(int);
            float total = 0;
            User thisUser = dbContext.Users
                .Include(i => i.TransactionsOfUser)
                .FirstOrDefault(i => i.UserId == UserId);
            foreach(Transaction j in thisUser.TransactionsOfUser)
            {
                total += j.Amount;
            };
            if(total + money.Amount < 0)
            {
                ModelState.AddModelError("Amount", "Not enough to withdraw!");
                ViewBag.UserInfo = thisUser;
                ViewBag.Total = total;
                return View("Account");
            };
            
            money.UserId = UserId;
            dbContext.Transactions.Add(money);
            dbContext.SaveChanges();
            return RedirectToAction("Account");
        }
    }
}