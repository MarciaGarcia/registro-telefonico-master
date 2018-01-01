using Confere.Telefonia.Web.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Models
{
    public class SetorViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sigla { get; set; }
        public int NumeroFuncionarios { get; set; }
        public int QtdeLigacoes { get; set; }
        public int QtdeLigacoesParticulares { get; set; }
        public double DuracaoMedia { get; set; }

        public Setor ToSetor()
        {
            return new Setor
            {
                Id = this.Id,
                Nome = this.Nome,
                Sigla = this.Sigla
            };
        }
    }
}