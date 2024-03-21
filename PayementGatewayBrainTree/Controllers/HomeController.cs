using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Braintree;
using PaymentGatewayBrainTree.Models;
using PaymentGatewayBrainTree.Services;

namespace Hellow_payment.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBrainTreeService _braintreeService;

        public HomeController(IBrainTreeService braintreeService)
        {
            _braintreeService = braintreeService;
        }




        public IActionResult Index()
        {
            //var gateway = _braintreeService.GetGateway();
            //var clientToken = gateway.ClientToken.Generate();
            //ViewBag.ClientToken = clientToken;

            //return View();
            var gateway = _braintreeService.GetGateway();
            var clientToken = gateway.ClientToken.Generate();
            ViewBag.ClientToken = clientToken;

            var data = new BookPurchaseVM
            {
                Id = 1,
                Description = ".NET Payment Gateway Implementation",
                Author = "Javeria",
                Thumbnail = "Payment Gateway",
                Title = "Payment Gateway BrainTree",
                Price = "2000",
                Nonce = ""
            };

            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        [HttpPost]
        public IActionResult Create(BookPurchaseVM model)
        {
            var gateway = _braintreeService.GetGateway();


            var request = new TransactionRequest
            {
                Amount = Convert.ToDecimal("250"),
                PaymentMethodNonce = model.Nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);

            if (result.IsSuccess())
            {
                return View("Success");
            }
            else
            {
                return View("Failure");
            }
        }
    }
}