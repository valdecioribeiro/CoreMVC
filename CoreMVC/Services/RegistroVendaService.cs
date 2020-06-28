using CoreMVC.Data;
using CoreMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Services
{
    public class RegistroVendaService
    {
        private readonly CoreMVCContext _contexto;

        public RegistroVendaService(CoreMVCContext contexto){
            _contexto = contexto;
        }

        public async Task<List<RegistroVenda>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _contexto.RegistroVenda select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Data >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Data <= maxDate.Value);
            }
            return await result
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .ToListAsync();
        }
    }
}
