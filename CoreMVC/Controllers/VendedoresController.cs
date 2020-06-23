using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC.Models;
using CoreMVC.Models.ViewModels;
using CoreMVC.Services;
using CoreMVC.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC.Controllers
{
    public class VendedoresController : Controller
    {

        private readonly VendedorService _vendedoresService;
        private readonly DepartamentoService _departamento_service;

        //injeção de dependência
        public VendedoresController(VendedorService vendedoresService, DepartamentoService departamentoService)
        {
            _vendedoresService = vendedoresService;
            _departamento_service = departamentoService;

        }

        public IActionResult Index()
        {
            var lista = _vendedoresService.RecuperarTodos();
            return View(lista);
        }

        public IActionResult Create()
        {
            var departamentos = _departamento_service.RecuperarTodos();
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendedor vendedor)
        {
            _vendedoresService.Inserir(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)//quer dizer que o id é opcional
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _vendedoresService.BuscarPorID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _vendedoresService.RemoverVendedor(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _vendedoresService.BuscarPorID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _vendedoresService.BuscarPorID(id.Value);
            if(obj == null)
            {
                return NotFound();
            }

            List<Departamento> departamentos = _departamento_service.RecuperarTodos();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departamentos };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Vendedor vendedor)
        {
            if (id != vendedor.Id)
            {
                return BadRequest();
            }

            try 
            { 
            _vendedoresService.Update(vendedor);

            return RedirectToAction(nameof(Index));

            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}
