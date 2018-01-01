using System;

namespace Confere.Telefonia.Web.Negocio
{
    public class Ligacao
    {
        public int Id { get; set; }
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
        public int DestinoId { get; set; }
        public Destino Destino { get; set; }
        public int TelefonistaId { get; set; }
        public Telefonista Telefonista { get; set; }
        public string Telefone { get; set; }
        public DateTime DataHora { get; set; }
        public TimeSpan Duracao { get; set; }
        public string Contato { get; set; }
        public string Observacao { get; set; }
        public bool Particular { get; set; }
    }
}