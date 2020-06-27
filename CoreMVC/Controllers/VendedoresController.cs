using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC.Models;
using CoreMVC.Models.ViewModels;
using CoreMVC.Services;
using CoreMVC.Services.Exceptions;
using Microsoft.AspNetCore.Http.Features;
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
            if (!ModelState.IsValid)
            {
                var departamentos = _departamento_service.RecuperarTodos();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamentos };
                return View(viewModel);
            }

            _vendedoresService.Inserir(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)//quer dizer que o id é opcional
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
            }

            var obj = _vendedoresService.BuscarPorID(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });
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
                return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
            }

            var obj = _vendedoresService.BuscarPorID(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
            }

            var obj = _vendedoresService.BuscarPorID(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });
            }

            List<Departamento> departamentos = _departamento_service.RecuperarTodos();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departamentos };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamentos = _departamento_service.RecuperarTodos();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamentos }; 
                return View(viewModel);
            }
            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não corresponde!" });
            }

            try 
            { 
            _vendedoresService.Update(vendedor);

            return RedirectToAction(nameof(Index));

            } //pode ser substituído por um application exception. Ele é um super tipo das exceções colocadas abaixo!
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
