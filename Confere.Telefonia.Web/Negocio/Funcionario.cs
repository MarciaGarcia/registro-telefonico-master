using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Negocio
{
    public class Funcionario
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string TelefoneFavorito { get; set; }
        public int SetorId { get; set; }
        public Setor Setor { get; set; }
        public int? DestinoFavoritoId { get; set; }
        public Destino DestinoFavorito { get; set; }
        public IList<Ligacao> Ligacoes { get; set; }        
        public bool Ativo { get; set; }
    }
}