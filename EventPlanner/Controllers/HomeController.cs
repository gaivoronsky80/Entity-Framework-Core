using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace EventPlanner.Controllers
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
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return RedirectToAction("Dashboard");
        }

        [Route("dashboard")]
        [HttpGet]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            List<Event> allEvents = dbContext.Events
                .Include(e => e.Joins)
                .ThenInclude(j => j.User)
                .Include(e => e.Creator)
                .ToList();
            ViewBag.AllEvents = allEvents;

            // List<User> allUsers = dbContext.Users
            //     .Include(i => i.Joins)
            //     .ThenInclude(j => j.Event)
            //     .ToList();
            // ViewBag.AllUsers = allUsers;

            int? IntVariable = HttpContext.Session.GetInt32("UserID");
            int sessionID = IntVariable ?? default(int);

            User thisUser = dbContext.Users
                .Include(e => e.Joins)
                .ThenInclude(j => j.Event)
                .Include(e => e.Events)
                .FirstOrDefault(i => i.UserId == sessionID);
            ViewBag.ThisUser = thisUser;
            return View();
        }

        [Route("info/{EventId}")]
        [HttpGet]
        public IActionResult Info(int EventId)
        {
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            Event thisEvent = dbContext.Events
                .Include(e => e.Joins)
                .ThenInclude(Join => Join.User)
                .Include(e => e.Creator)
                .FirstOrDefault(i => i.EventId == EventId);
            ViewBag.ThisEvent = thisEvent;

            int? IntVariable = HttpContext.Session.GetInt32("UserID");
            int sessionID = IntVariable ?? default(int);

            User thisUser = dbContext.Users
                .Include(e => e.Joins)
                .ThenInclude(j => j.Event)
                .Include(e => e.Events)
                .FirstOrDefault(i => i.UserId == sessionID);
            ViewBag.ThisUser = thisUser;
            return View();
        }

        [Route("create")]
        [HttpGet]
        public IActionResult Create()
        {
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add(Event thisEvent)
        {
            if(ModelState.IsValid)
            {
                DateTime Today = DateTime.Now;
                if(thisEvent.Date < Today)
                {
                    ModelState.AddModelError("Date", "Date must be current or in the future.");
                    return View("Create");
                }

                int? IntVariable = HttpContext.Session.GetInt32("UserID");
                int sessionID = IntVariable ?? default(int);

                thisEvent.UserId = sessionID;
                dbContext.Events.Add(thisEvent);
                dbContext.SaveChanges();

                int EventId = thisEvent.EventId;
                return RedirectToAction("Info", new{EventId = EventId});
            }
            return View("Create");
        }

        [Route("delete/{eventId}")]
        [HttpGet]
        public IActionResult Delete(int eventId)
        {
            Event thisEvent = dbContext.Events
                .FirstOrDefault(i => i.EventId == eventId);
            dbContext.Events.Remove(thisEvent);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [Route("join/{eventId}")]
        [HttpGet]
        public IActionResult Join(int eventId)
        {
            int? IntVariable = HttpContext.Session.GetInt32("UserID");
            int sessionId = IntVariable ?? default(int);

            Join newJoin = new Join();
            newJoin.UserId = sessionId;
            newJoin.EventId = eventId;
            dbContext.Joins.Add(newJoin);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [Route("unjoin/{eventId}")]
        [HttpGet]
        public IActionResult UnJoin(int eventId)
        {
            int? IntVariable = HttpContext.Session.GetInt32("UserID");
            int sessionID = IntVariable ?? default(int);

            Join oldJoin = dbContext.Joins
                .FirstOrDefault(u => u.UserId == sessionID && u.EventId == eventId);
            dbContext.Joins.Remove(oldJoin);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}
