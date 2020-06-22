using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC.Data;
using CoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreMVC.Services
{
    public class VendedorService
    {
        //readonly previne que a dependencia seja alterada
        private readonly CoreMVCContext _contexto;

        public VendedorService (CoreMVCContext contexto)
        {
            _contexto = contexto;
        }

        public IList<Vendedor> RecuperarTodos()
        {
            return _contexto.Vendedor.ToList();
        }

        public void Inserir(Vendedor vendedor)
        {
            _contexto.Add(vendedor);
            _contexto.SaveChanges();
        }

        public Vendedor BuscarPorID(int id)
        {
            return _contexto.Vendedor.Include(obj => obj.Departamento).FirstOrDefault(x=>x.Id == id);
        }

        public void RemoverVendedor(int id)
        {
            var obj = _contexto.Vendedor.Find(id);
            _contexto.Vendedor.Remove(obj);
            _contexto.SaveChanges();
        }
    }
}
