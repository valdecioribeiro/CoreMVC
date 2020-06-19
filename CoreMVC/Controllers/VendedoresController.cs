using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC.Models;
using CoreMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC.Controllers
{
    public class VendedoresController : Controller
    {

        private readonly VendedorService _vendedoresService;

        //injeção de dependência
        public VendedoresController(VendedorService vendedoresService)
        {
            _vendedoresService = vendedoresService;
        }

        public IActionResult Index()
        {
            var lista = _vendedoresService.RecuperarTodos();
            return View(lista);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendedor vendedor)
        {
            _vendedoresService.Inserir(vendedor);
            return RedirectToAction(nameof(Index));
        }
    }
}
