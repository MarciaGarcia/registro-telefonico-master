using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Models
{
    public class LigacaoViewModel
    {
        public int Id { get; set; }
        [Required]
        public int FuncionarioId { get; set; }
        [Required(ErrorMessage = "O funcionário é obrigatório")]
        public string FuncionarioNome { get; set; }
        [Required]
        public int DestinoId { get; set; }
        public string DestinoNome { get; set; }
        [Required]
        public int TelefonistaId { get; set; }
        public string TelefonistaNome { get; set; }
        [Required(ErrorMessage = "O telefone é obrigatório")]
        public string Telefone { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataHora { get; set; }
        public TimeSpan Duracao { get; set; }
        public DateTime HoraInicial { get; set; }
        public DateTime HoraFinal { get; set; }
        public string Contato { get; set; }
        public string Observacao { get; set; }
        public bool Particular { get; set; }

        internal TimeSpan ToDuracao()
        {
            return HoraFinal - HoraInicial;
        }
    }
}