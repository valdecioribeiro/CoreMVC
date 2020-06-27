using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC.Data;
using CoreMVC.Models;
using CoreMVC.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
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

        public async Task<IList<Vendedor>> RecuperarTodosAsync()
        {
            return await _contexto.Vendedor.ToListAsync();
        }

        public void Inserir(Vendedor vendedor)
        {
            _contexto.Add(vendedor);
            _contexto.SaveChanges();
        }

        public async Task InserirAsync(Vendedor vendedor)
        {
            _contexto.Add(vendedor);
            await _contexto.SaveChangesAsync();
        }

        public Vendedor BuscarPorID(int id)
        {
            return _contexto.Vendedor.Include(obj => obj.Departamento).FirstOrDefault(x=>x.Id == id);
        }

        public async Task<Vendedor> BuscarPorIDAsync(int id)
        {
            return await _contexto.Vendedor.Include(obj => obj.Departamento).FirstOrDefaultAsync(x => x.Id == id);
        }

        public void RemoverVendedor(int id)
        {
            var obj = _contexto.Vendedor.Find(id);
            _contexto.Vendedor.Remove(obj);
            _contexto.SaveChanges();
        }

        public async Task RemoverVendedorAsync(int id)
        {
            var obj = await _contexto.Vendedor.FindAsync(id);
            _contexto.Vendedor.Remove(obj);
            await _contexto.SaveChangesAsync();
        }

        public void Update(Vendedor vendedor)
        {
            if(!_contexto.Vendedor.Any(x => x.Id == vendedor.Id))
            {
                throw new NotFoundException("Id não encontrado!");
            }
            try
            {
                _contexto.Update(vendedor);
                _contexto.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

            
        }

        public async Task UpdateAsync(Vendedor vendedor)
        {
            bool existe = await _contexto.Vendedor.AnyAsync(x => x.Id == vendedor.Id);
            if (!existe)
            {
                throw new NotFoundException("Id não encontrado!");
            }
            try
            {
                _contexto.Update(vendedor);
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }


        }


    }
}
