using Confere.Telefonia.Web.Extensions;
using Confere.Telefonia.Web.Negocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Dados
{
    public class DestinoRepository : IRepository<Destino>
    {
        private TelefoniaContexto contexto;

        public DestinoRepository(TelefoniaContexto contexto)
        {
            this.contexto = contexto;
        }

        /// <summary>
        /// Propriedade pública com lista dos destinos ativos. 
        /// </summary>
        public IQueryable<Destino> Ativos => contexto.Destinos
            //.Include(d => d.Ligacoes)
            .Where(d => d.Ativo);

        public IQueryable<Destino> DestinosAtivosComLigacoesDoAno(int ano)
        {
            DateTime primeiroDiaDoAno;
            DateTime.TryParse($"1/1/{ano}", out primeiroDiaDoAno);

            var destinos = this.Ativos;
            foreach (var dest in destinos)
            {
                contexto.Entry(dest)
                    .Collection(d => d.Ligacoes)
                        .Query()
                        .Include(l => l.Funcionario)
                        .Where(l => l.MesmoAno(primeiroDiaDoAno))
                    .Load();
            }

            return destinos;
        }

        /// <summary>
        /// Alteração do destino.
        /// </summary>
        /// <param name="objeto">Objeto a ser modificado.</param>
        public void Alterar(Destino objeto)
        {
            contexto.Destinos.Update(objeto);
            contexto.SaveChanges();
        }

        /// <summary>
        /// Exclusão lógica do destino.
        /// </summary>
        /// <param name="id">Identificador do destino.</param>
        public void Excluir(int id)
        {
            var destino = Ativos.First(d => d.Id == id);
            destino.Ativo = false;
            this.Alterar(destino);
        }

        /// <summary>
        /// Inclusão do destino na base.
        /// </summary>
        /// <param name="objeto">Objeto a ser incluído.</param>
        public void Incluir(Destino objeto)
        {
            contexto.Destinos.Add(objeto);
            contexto.SaveChanges();
        }
    }
}