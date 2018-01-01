using Confere.Telefonia.Web.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Models
{
    public class TelefonistaViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public int RegistrosNaSemana { get; set; }
        public int RegistrosNoMes { get; set; }
        public int RegistrosNoAno { get; set; }

        public Telefonista ToTelefonista()
        {
            return new Telefonista
            {
                Id = this.Id,
                Nome = this.Nome,
            };
        }
    }
}