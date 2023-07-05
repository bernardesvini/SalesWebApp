using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebApp.Services;
using SalesWebApp.Models;
using SalesWebApp.Models.ViewModels;

namespace SalesWebApp.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService; // pois devemos inserir departamento ao vendedor

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {

            var list = _sellerService.FindAll();
            return View(list);

            // Mycomment - abaixo como pensei
            // return View(_sellerService.FindAll());
        }
        //GET
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) // MyComments Quando o método é chamado de um formulario, o framework sabe que trata-se de um POST, por isso chama este método e não o anterior, que é GET
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
        // GET
        public IActionResult Delete (int? id) // ? indica que paramentro é opcional
        {
            if (id == null)
                return NotFound();

            var obj = _sellerService.FindbyId(id.Value); // usasse o value, pois este é opcional

            if (obj == null)
                return NotFound();

            return View(obj);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) // MyComments Quando o método é chamado de um formulario, o framework sabe que trata-se de um POST, por isso chama este método e não o anterior, que é GET
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
