using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BrightIdeas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace BrightIdeas.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [Route("bright_ideas")]
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Register", "Login");
            }
            List<Message> allMessages = dbContext.Messages
                .Include(i => i.MessageLikes)
                .ThenInclude(j => j.User)
                .Include(k => k.User)
                .ToList();
            ViewBag.AllMessages = allMessages;

            int? IntVariable = HttpContext.Session.GetInt32("UserID");
            int sessionID = IntVariable ?? default(int);
            User thisUser = dbContext.Users
                .Include(i => i.UserLikes)
                .ThenInclude(j => j.Message)
                .FirstOrDefault(l => l.Id == sessionID);
            ViewBag.ThisUser = thisUser;
            return View();
        }

        [Route("addmessage")]
        [HttpPost]
        public IActionResult AddMessage(FormContent formcontent)
        {
            if(ModelState.IsValid)
            {
                int? IntVariable = HttpContext.Session.GetInt32("UserID");
                int sessionID = IntVariable ?? default(int);

                Message newMessage = new Message();
                newMessage.Content = formcontent.Content;
                newMessage.UserId = sessionID;
                dbContext.Messages.Add(newMessage);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [Route("addlike/{mId}")]
        [HttpGet]
        public IActionResult AddLike(int mId)
        {
            if(ModelState.IsValid)
            {
                int? IntVariable = HttpContext.Session.GetInt32("UserID");
                int sessionID = IntVariable ?? default(int);

                Like newLike = new Like();
                newLike.UserId = sessionID;
                newLike.MessageId = mId;
                dbContext.Likes.Add(newLike);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [Route("delete/{mId}")]
        [HttpGet]
        public IActionResult Delete(int mId)
        {
            Message thisMessage = dbContext.Messages
                .FirstOrDefault(i => i.Id == mId);
            dbContext.Messages.Remove(thisMessage);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("info/{uId}")]
        [HttpGet]
        public IActionResult Info(int uId)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Register", "Login");
            }

            int? IntVariable = HttpContext.Session.GetInt32("UserID");
            int sessionID = IntVariable ?? default(int);
            User thisUser = dbContext.Users
                .Include(i => i.UserLikes)
                .ThenInclude(j => j.Message)
                .FirstOrDefault(l => l.Id == uId);
            ViewBag.ThisUser = thisUser;
            return View();
        }

        [Route("idea/{mId}")]
        [HttpGet]
        public IActionResult Idea(int mId)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Register", "Login");
            }
            List<Message> allMessages = dbContext.Messages
                .Include(i => i.MessageLikes)
                .ThenInclude(j => j.User)
                .Include(k => k.User)
                .ToList();
            ViewBag.AllMessages = allMessages;

            Message thisMessage = dbContext.Messages
                .Include(e => e.MessageLikes)
                .ThenInclude(Like => Like.User)
                .Include(e => e.User)
                .FirstOrDefault(i => i.Id == mId);
            ViewBag.ThisMessage = thisMessage;

            int? IntVariable = HttpContext.Session.GetInt32("UserID");
            int sessionID = IntVariable ?? default(int);
            User thisUser = dbContext.Users
                .Include(i => i.UserLikes)
                .ThenInclude(j => j.Message)
                .FirstOrDefault(l => l.Id == sessionID);
            ViewBag.ThisUser = thisUser;
            return View();
        }
    }
}
