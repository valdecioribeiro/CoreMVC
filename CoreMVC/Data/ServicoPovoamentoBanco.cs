using CoreMVC.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;

using CoreMVC.Models.Enums;

namespace CoreMVC.Data
{
    public class ServicoPovoamentoBanco
    {
        private CoreMVCContext _context;

        public ServicoPovoamentoBanco(CoreMVCContext context) 
        {
            _context = context;
        }

        public void Povoa()
        {
            if(_context.Departamento.Any() ||
               _context.Vendedor.Any()||
               _context.RegistroVenda.Any())
            {
                return; //Banco de dados já populado
            }

            Departamento d1 = new Departamento(1, "Departamento de Eletrônicos");
            Vendedor vendedor = new Vendedor(1, "Valdecio", "Valdecio@gmail.com", 10000, new DateTime(1984, 04, 25), d1);
            RegistroVenda registro = new RegistroVenda(1, new DateTime(2020, 06, 17), 10, StatusVenda.Faturado, vendedor);

            _context.Departamento.AddRange(d1);
            _context.Vendedor.AddRange(vendedor);
            _context.RegistroVenda.AddRange(registro);

            _context.SaveChanges();
        }

        
    }
}
