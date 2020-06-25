using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookClub.Data;
using BookClub.Models;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace BookClub.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public PaymentController(ApplicationDbContext dbContext, IConfiguration config)
        {
            context = dbContext;
            configuration = config;
        }
        public ActionResult Index()
        {
            return View();
        }
               

        // GET: PaymentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string stripeToken, int id)
        {
            Book bookid =context.Books.Single(b => b.ID == id);
            double amount = bookid.Price;
            StripeConfiguration.ApiKey = configuration["StripeApiKey"];
            var options = new ChargeCreateOptions
            {
                Amount = (long)(amount*100),
                Currency = "usd",
                Description = "testing charge",
                Source = stripeToken
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);
            var model = new ChargeViewModel();
            model.ChargeId = charge.Id;
            model.Amount = charge.Amount * 0.01;
            /*//StripeConfiguration.ApiKey = "sk_test_51GwAkHBHryiDErftLqW5lCWQuNzqUtUd6p3Z7mPP59kD0a0C6hMCCoGYb2Tjg21Y94Q8CgPryg6Sie8Xm0aD1w4e005ycdr6wj";

            var options = new TransferCreateOptions
            {
                Amount = 400,
                Currency = "usd",
                Destination = "acct_1GwAkHBHryiDErft4573201",
                TransferGroup = "ORDER_95",
            };
            var service = new TransferService();
            service.Create(options);*/


            return View("OrderStatus", model);
        }

    }
}
