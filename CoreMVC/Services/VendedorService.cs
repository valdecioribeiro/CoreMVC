﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC.Data;
using CoreMVC.Models;

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
    }
}