using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC.Controllers
{
    public class RegistroVendasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcuraSimples()
        {
            return View();
        }

        public IActionResult ProcuraAgrupada()
        {
            return View();
        }
    }
}
