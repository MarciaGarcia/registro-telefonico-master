using Confere.Telefonia.Web.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Models
{
    public class DestinoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QtdeLigacoes { get; set; }
        public int NumeroFuncionarios { get; set; }
        public double DuracaoMedia { get; set; }
        public int AnoAtual { get; set; }

        public Destino ToDestino()
        {
            return new Destino
            {
                Id = this.Id,
                Nome = this.Nome,
            };
        }
    }
}