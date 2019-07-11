using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers
{
    public class HomeController : Controller
    {
        MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.AllChefs = dbContext.Chefs.Include(c => c.DishOfChef);
            return View("Index");
        }

        [Route("/new")]
        [HttpGet]
        public IActionResult NewChef(Chef chef)
        {
            return View("NewChef");
        }

        [Route("/createchef")]
        [HttpGet]
        [HttpPost]
        public RedirectToActionResult CreateChef(Chef chef)
        {
            if(ModelState.IsValid)
            {
                if(((DateTime.Today - chef.DateOfBirth).TotalDays/365) > 18)
                {
                    dbContext.Chefs.Add(chef);
                    dbContext.SaveChanges();
                    return RedirectToAction("Index");
                } else {
                    return RedirectToAction("NewChef");
                }

            } else {
                return RedirectToAction("NewChef");
            }
        }

        [Route("/dishes")]
        [HttpGet]
        public IActionResult Dishes()
        {
            ViewBag.AllDishes = dbContext.Dishes.Include(d => d.Chef);
            return View("Dishes");
        }

        [Route("/dishes/new")]
        [HttpGet]
        public IActionResult NewDish(Dish dish)
        {
            ViewBag.AllChefs = dbContext.Chefs.Include(c => c.DishOfChef);
            return View("NewDish");
        }

        [Route("/createdish")]
        [HttpGet]
        [HttpPost]
        public RedirectToActionResult CreateDish(Dish dish)
        {
            if(ModelState.IsValid)
            {
                dbContext.Dishes.Add(dish);
                dbContext.SaveChanges();
                return RedirectToAction("Dishes");
            } else {
                return RedirectToAction("NewDish");
            }
        }

    }
}
