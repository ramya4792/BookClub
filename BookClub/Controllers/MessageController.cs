using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookClub.Data;
using BookClub.Migrations;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using BookClub.Models;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Humanizer;

namespace BookClub.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public MessageController(ApplicationDbContext dbContext, UserManager<ApplicationUser> user)
        {
            context = dbContext;
            userManager = user;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            return View(context.AddMessages.Where(x => x.ReceiverId == user.Id).ToList());
        }
        public IActionResult Send(AddMessageViewModel addMessageViewModel)
        {
            return View(addMessageViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Send(AddMessageViewModel addMessageViewModel, string message, int id)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid)
            {

                BookClub.Models.AddMessage newMessage = new BookClub.Models.AddMessage
                {
                    Message = message,
                    ReceiverId = id,
                    Date = DateTime.Now,
                    UserId = int.Parse(userManager.GetUserId(HttpContext.User))
                };
                context.AddMessages.Add(newMessage);
                context.SaveChanges();

                return Redirect("/Home");
            }
            return View();
        }
        /*public IActionResult Download()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Download(int id)
        {
            Book download = context.Books.Single(b => b.ID == id);
            ViewBag.File = download.File;
            return View();
        }*/
    }
}
