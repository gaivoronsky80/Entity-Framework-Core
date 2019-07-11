using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDelicious.Models;
using Microsoft.AspNetCore.Http;

namespace CRUDelicious.Controllers
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
        public IActionResult Index()
        {
            List<Dish> AllDishes = dbContext.Dishes.ToList();
            return View(AllDishes);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Show(int id)
        {
            Dish dish = dbContext.Dishes.FirstOrDefault(d => d.DishId == id);
            return View("Show", dish);
        }

        [Route("/new")]
        [HttpGet]
        public IActionResult New(Dish dish)
        {
            return View("New");
        }

        [Route("/create")]
        [HttpGet]
        [HttpPost]
        public RedirectToActionResult Create(Dish dish)
        {
            if(ModelState.IsValid)
            {
                dbContext.Dishes.Add(dish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            } else {
                return RedirectToAction("New");
            }
        }

        [Route("edit/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Dish dish = dbContext.Dishes.FirstOrDefault(d => d.DishId == id);
            return View("Edit", dish);
        }

        [Route("/update/{DishId}")]
        [HttpGet]
        [HttpPost]
        public RedirectToActionResult Update(Dish dish, int DishId)
        {
            if(ModelState.IsValid)
            {
                Dish mydish = dbContext.Dishes.FirstOrDefault(d => d.DishId == DishId);
                mydish.Name = dish.Name;
                mydish.Chef = dish.Chef;
                mydish.Tastiness = dish.Tastiness;
                mydish.Calories = dish.Calories;
                mydish.Description = dish.Description;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            } else {
                return RedirectToAction("Edit");
            }
        }

        [Route("delete/{id}")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Dish dish = dbContext.Dishes.FirstOrDefault(d => d.DishId == id);
            dbContext.Dishes.Remove(dish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
