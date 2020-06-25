using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreMVC.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public double SalarioBase { get; set; }
        public DateTime Nascimento { get; set; }
        public Departamento Departamento { get; set; }
        public int DepartamentoId { get; set; }
        public ICollection<RegistroVenda> Vendas { get; set; } = new List<RegistroVenda>();

        public Vendedor() { }

        public Vendedor(int id, string nome, string email, double salarioBase, DateTime nascimento, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            SalarioBase = salarioBase;
            Nascimento = nascimento;
            Departamento = departamento;
        }

        public void AdicionarVenda(RegistroVenda venda)
        {
            Vendas.Add(venda);
        }

        public void RemoverVenda(RegistroVenda venda)
        {
            Vendas.Remove(venda);
        }

        public double TotalVendas(DateTime inicial, DateTime final)
        {
            return Vendas.Where(venda => venda.Data >= inicial && venda.Data <= final).Sum(venda => venda.Quantidade);
        }


    }
}
