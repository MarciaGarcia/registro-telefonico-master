using Confere.Telefonia.Web.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Confere.Telefonia.Web.Models
{
    public class FuncionarioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string TelefoneFavorito { get; set; }
        public int QtdeLigacoes { get; set; }
        public int QtdeLigacoesParticulares { get; set; }
        public int SetorId { get; set; }
        public string Setor { get; set; }
        public string Destino { get; set; }
        public int DestinoFavoritoId { get; set; }
    }
}