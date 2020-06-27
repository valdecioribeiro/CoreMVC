using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CoreMVC.Models
{
    public class Vendedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1}")]
        public string Nome { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Coloque um e-mail válido!")]
        public string Email { get; set; }

        [Display(Name = "Salário Base")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Range(100.0, 50000.0, ErrorMessage ="{0} deve ser no mínimo {1} e no máximo {2}!")]
        public double SalarioBase { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Nascimento { get; set; }
        public Departamento Departamento { get; set; }
        
        [Display(Name = "Departamento")]
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
