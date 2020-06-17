using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreMVC.Models;

namespace CoreMVC.Data
{
    public class CoreMVCContext : DbContext
    {
        public CoreMVCContext (DbContextOptions<CoreMVCContext> options)
            : base(options)
        {
        }

        public DbSet<CoreMVC.Models.Departamento> Departamento { get; set; }

        public DbSet<Vendedor> Vendedor { get; set; }

        public DbSet<RegistroVenda> RegistroVenda { get; set; }
    }
}
