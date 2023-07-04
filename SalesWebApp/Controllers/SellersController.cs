using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebApp.Services;

namespace SalesWebApp.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService service)
        {
            _sellerService = service;
        }

        public IActionResult Index()
        {

            var list = _sellerService.FindAll();
            return View(list);
           
            // Mycomment - abaixo como pensei
           // return View(_sellerService.FindAll());
        }
    }
}
