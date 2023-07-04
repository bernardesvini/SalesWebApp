using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebApp.Services;
using SalesWebApp.Models;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) // MyComments Quando o método é chamado de um formulario, o framework sabe que trata-se de um POST, por isso chama este método e não o anterior, que é GET
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}
