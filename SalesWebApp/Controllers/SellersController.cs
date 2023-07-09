using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebApp.Services;
using SalesWebApp.Models;
using SalesWebApp.Models.ViewModels;
using SalesWebApp.Services.Exceptions;
using System.Diagnostics;

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
        public IActionResult Delete(int? id) // ? indica que paramentro é opcional
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});

            var obj = _sellerService.FindbyId(id.Value); // usasse o value, pois este é opcional

            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" }); 

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) // MyComments Quando o método é chamado de um formulario, o framework sabe que trata-se de um POST, por isso chama este método e não o anterior, que é GET
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id) // ? indica que paramentro é opcional
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" }); 

            var obj = _sellerService.FindbyId(id);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" }); 

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            var obj = _sellerService.FindbyId(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
                return RedirectToAction(nameof(Error), new { message = "Id don't match" });
            try
            {
                _sellerService.Update(seller);
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id
            };

            return View(viewModel);
        }


    }
}
