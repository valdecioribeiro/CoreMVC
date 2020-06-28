using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC.Controllers
{
    public class RegistroVendasController : Controller
    {
        private readonly RegistroVendaService _registroVendasServico;

        public RegistroVendasController(RegistroVendaService registroVendasServico)
        {
            _registroVendasServico = registroVendasServico;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProcuraSimples(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _registroVendasServico.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public IActionResult ProcuraAgrupada()
        {
            return View();
        }
    }
}
