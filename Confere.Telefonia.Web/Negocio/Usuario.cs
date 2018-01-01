using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Confere.Telefonia.Web.Negocio
{
    public enum Papel
    {
        Gestor,
        Telefonista,
        Administrador
    }

    internal static class PapelExtensions
    {
        public static IList<Papel> ToListaDePapeis(this string texto)
        {
            var mapa = new Dictionary<string, Papel>();
            foreach (Papel papel in Enum.GetValues(typeof(Papel)))
            {
                mapa.Add(papel.ToString(), papel);
            }
            var lista = new List<Papel>();
            var vetor = texto.Split(',');
            foreach (var item in vetor)
            {
                lista.Add(mapa[item]);
            }
            return lista;
        }
    }

    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime? UltimoLogin { get; set; }
        public bool Ativo { get; set; }

        public string TextoPapeis { get; set; }
        public IList<Papel> Papeis
        {
            get { return TextoPapeis.ToListaDePapeis(); }
        }

        public byte[] ImagemPerfil { get; set; }
    }
}