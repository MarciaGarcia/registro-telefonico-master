using System.Collections.Generic;
using System.Linq;
using Confere.Telefonia.Web.Negocio;
using Confere.Telefonia.Web.Seguranca;
using System.Diagnostics;

namespace Confere.Telefonia.Web.Dados
{
    public class UsuarioRepository : IRepository<Usuario>
    {
        private TelefoniaContexto contexto;

        public UsuarioRepository(TelefoniaContexto contexto)
        {
            this.contexto = contexto;
        }

        /// <summary>
        /// Propriedade pública com lista dos usuários ativos. 
        /// </summary>
        public IQueryable<Usuario> Ativos => contexto.Usuarios.Where(u => u.Ativo);

        /// <summary>
        /// Autenticação do usuário. Permite usuários com senha nula.
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <param name="senha">Senha do usuário</param>
        /// <returns></returns>
        public Usuario Autenticar(string login, Senha senha)
        {
            Debug.WriteLine("Entrou no método de autenticação.");
            return contexto.Usuarios
                .FirstOrDefault(u => 
                    (u.Login == login) && 
                    ( (u.Senha == null) || (u.Senha.Equals(senha.TextoHash)) )
                );
        }

        /// <summary>
        /// Inclusão do usuário.
        /// </summary>
        /// <param name="usuario">Objeto a ser incluído na base.</param>
        public void Incluir(Usuario usuario)
        {
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
        }

        /// <summary>
        /// Alteração do usuário.
        /// </summary>
        /// <param name="usuario">Objeto a ser alterado na base.</param>
        public void Alterar(Usuario usuario)
        {
            contexto.Usuarios.Update(usuario);
            contexto.SaveChanges();
        }

        /// <summary>
        /// Exclusão lógica do usuário
        /// </summary>
        /// <param name="id">Chave primária do usuário.</param>
        public void Excluir(int id)
        {
            var usu = contexto.Usuarios.First(u => u.Id == id);
            usu.Ativo = false;
            contexto.Usuarios.Update(usu);
            contexto.SaveChanges();
        }
    }
}