using Confere.Telefonia.Web.Negocio;
using Confere.Telefonia.Web.Seguranca;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Web;

namespace Confere.Telefonia.Web.Models
{
    internal static class UsuarioExtensions
    {
        public static PerfilViewModel ToPerfilViewModel(this Usuario usuario)
        {
            return new PerfilViewModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Login = usuario.Login,
                Email = usuario.Email,
            };
        }
    }

    public class PerfilViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Email { get; set; }

        public string Senha { get; set; }

        [Compare("Senha")]
        public string Confirmacao { get; set; }

        public HttpPostedFileBase ImagemPerfil { get; set; }

        public Usuario ToUsuario()
        {
            var senha = new Senha { TextoPuro = this.Senha };
            return new Usuario
            {
                Id = this.Id,
                Nome = this.Nome,
                Login = this.Login,
                Email = this.Email,
                Senha = senha.TextoHash,
            };
        }
    }
}