using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PartyFinder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace PartyFinder.Controllers
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

            List<Party> allParties = dbContext.Parties
                .Include(e => e.Joins)
                .ThenInclude(j => j.User)
                .Include(e => e.Creator)
                .ToList();
            ViewBag.AllParties = allParties;

            int? IntVariable = HttpContext.Session.GetInt32("UserID");
            int sessionID = IntVariable ?? default(int);

            User thisUser = dbContext.Users
                .Include(e => e.Joins)
                .ThenInclude(j => j.Party)
                .Include(e => e.Parties)
                .FirstOrDefault(i => i.UserId == sessionID);
            ViewBag.ThisUser = thisUser;
            return View();
        }

        [Route("info/{PartyId}")]
        [HttpGet]
        public IActionResult Info(int PartyId)
        {
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            Party thisParty = dbContext.Parties
                .Include(e => e.Joins)
                .ThenInclude(Join => Join.User)
                .Include(e => e.Creator)
                .FirstOrDefault(i => i.PartyId == PartyId);
            ViewBag.ThisParty = thisParty;

            int? IntVariable = HttpContext.Session.GetInt32("UserID");
            int sessionID = IntVariable ?? default(int);

            User thisUser = dbContext.Users
                .Include(e => e.Joins)
                .ThenInclude(j => j.Party)
                .Include(e => e.Parties)
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
        public IActionResult Add(Party thisParty)
        {
            if(ModelState.IsValid)
            {
                DateTime Today = DateTime.Now;
                if(thisParty.Date < Today)
                {
                    ModelState.AddModelError("Date", "Date must be current or in the future.");
                    return View("Create");
                }

                int? IntVariable = HttpContext.Session.GetInt32("UserID");
                int sessionID = IntVariable ?? default(int);

                thisParty.UserId = sessionID;
                dbContext.Parties.Add(thisParty);
                dbContext.SaveChanges();

                int PartyId = thisParty.PartyId;
                return RedirectToAction("Info", new{PartyId = PartyId});
            }
            return View("Create");
        }

        [Route("delete/{partyId}")]
        [HttpGet]
        public IActionResult Delete(int partyId)
        {
            Party thisParty = dbContext.Parties
                .FirstOrDefault(i => i.PartyId == partyId);
            dbContext.Parties.Remove(thisParty);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [Route("join/{partyId}")]
        [HttpGet]
        public IActionResult Join(int partyId)
        {
            int? IntVariable = HttpContext.Session.GetInt32("UserID");
            int sessionId = IntVariable ?? default(int);

            Join newJoin = new Join();
            newJoin.UserId = sessionId;
            newJoin.PartyId = partyId;
            dbContext.Joins.Add(newJoin);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [Route("unjoin/{partyId}")]
        [HttpGet]
        public IActionResult UnJoin(int partyId)
        {
            int? IntVariable = HttpContext.Session.GetInt32("UserID");
            int sessionID = IntVariable ?? default(int);

            Join oldJoin = dbContext.Joins
                .FirstOrDefault(u => u.UserId == sessionID && u.PartyId == partyId);
            dbContext.Joins.Remove(oldJoin);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

    }
}
