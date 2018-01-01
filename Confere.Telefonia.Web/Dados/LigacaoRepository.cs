using System.Linq;
using Confere.Telefonia.Web.Negocio;

namespace Confere.Telefonia.Web.Dados
{
    public class LigacaoRepository : IRepository<Ligacao>
    {
        private TelefoniaContexto contexto;

        public LigacaoRepository(TelefoniaContexto contexto)
        {
            this.contexto = contexto;
        }

        /// <summary>
        /// Propriedade pública com todas as ligações da base. 
        /// Não existem ligações inativas, nesse caso todas serão retornadas.
        /// Aconselha-se incluir algum filtro na lista, devido ao alto potencial de crescimento dessa tabela.
        /// </summary>
        public IQueryable<Ligacao> Ativos => contexto.Ligacoes;

        /// <summary>
        /// Alteração da ligação na base.
        /// </summary>
        /// <param name="objeto">Objeto a ser modificado.</param>
        public void Alterar(Ligacao objeto)
        {
            contexto.Update(objeto);
            contexto.SaveChanges();
        }

        /// <summary>
        /// Exclusão da ligação na base.
        /// </summary>
        /// <param name="id">Identificador da ligação</param>
        public void Excluir(int id)
        {
            var ligacao = Ativos.First(l => l.Id == id);
            contexto.Ligacoes.Remove(ligacao);
            contexto.SaveChanges();
        }

        /// <summary>
        /// Inclusão da ligação na base.
        /// </summary>
        /// <param name="objeto">Objeto a ser incluído.</param>
        public void Incluir(Ligacao objeto)
        {
            contexto.Ligacoes.Add(objeto);
            contexto.SaveChanges();
        }
    }
}