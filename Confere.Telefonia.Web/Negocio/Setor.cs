using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Negocio
{
    public class Setor
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sigla { get; set; }
        public bool Ativo { get; set; }
        public IList<Funcionario> Funcionarios { get; set; }
    }
}