using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models.ViewModels
{
    public class VendedorFormViewModel
    {
        public Vendedor vendedor { get; set; }
        public ICollection<Departamento> Departamentos { get; set; }
    }
}
