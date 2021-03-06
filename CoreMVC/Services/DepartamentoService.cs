﻿using CoreMVC.Data;
using CoreMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Services
{
    public class DepartamentoService
    {
        private readonly CoreMVCContext _contexto;

        public DepartamentoService(CoreMVCContext contexto)
        {
            _contexto = contexto;
        }

        public List<Departamento> RecuperarTodos()
        {
            return _contexto.Departamento.OrderBy(x => x.Name).ToList();
        }

        public async Task<List<Departamento>> RecuperarTodosAsync()
        {
            return await _contexto.Departamento.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
