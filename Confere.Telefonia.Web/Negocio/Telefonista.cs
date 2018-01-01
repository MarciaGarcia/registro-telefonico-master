using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Negocio
{
    public class Telefonista
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public IList<Ligacao> Ligacoes { get; set; }
    }
}