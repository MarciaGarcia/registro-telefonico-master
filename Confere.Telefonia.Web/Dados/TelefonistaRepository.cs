using Confere.Telefonia.Web.Extensions;
using Confere.Telefonia.Web.Negocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Dados
{
    public class TelefonistaRepository : IRepository<Telefonista>
    {
        private TelefoniaContexto contexto;

        public TelefonistaRepository(TelefoniaContexto contexto)
        {
            this.contexto = contexto;
        }

        /// <summary>
        /// Propriedade pública com a lista dos telefonistas ativos.
        /// </summary>
        public IQueryable<Telefonista> Ativos => contexto.Telefonistas.Where(t => t.Ativo);

        public IQueryable<Telefonista> TelefonistasAtivosComLigacoesDoAno(int ano)
        {
            DateTime primeiroDiaDoAno;
            DateTime.TryParse($"1/1/{ano}", out primeiroDiaDoAno);

            var telefonistas = this.Ativos;

            foreach (var tel in telefonistas)
            {
                contexto.Entry(tel)
                    .Collection(t => t.Ligacoes)
                    .Query()
                    .Where(l => l.MesmoAno(primeiroDiaDoAno))
                    .Load();
            }

            return telefonistas;
        }


        /// <summary>
        /// Alteração do telefonista na base.
        /// </summary>
        /// <param name="objeto">Objeto a ser modificado.</param>
        public void Alterar(Telefonista objeto)
        {
            contexto.Telefonistas.Update(objeto);
            contexto.SaveChanges();
        }

        /// <summary>
        /// Exclusão lógica do telefonista na base.
        /// </summary>
        /// <param name="id">Identificador do telefonista.</param>
        public void Excluir(int id)
        {
            var telefonista = Ativos.First(t => t.Id == id);
            telefonista.Ativo = false;
            contexto.SaveChanges();
        }

        /// <summary>
        /// Inclusão do telefonista na base.
        /// </summary>
        /// <param name="objeto">Objeto a ser incluído.</param>
        public void Incluir(Telefonista objeto)
        {
            contexto.Telefonistas.Add(objeto);
            contexto.SaveChanges();
        }
    }
}