using Confere.Telefonia.Web.Extensions;
using Confere.Telefonia.Web.Negocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Dados
{
    public class SetorRepository : IRepository<Setor>
    {
        private TelefoniaContexto contexto;

        public SetorRepository(TelefoniaContexto contexto)
        {
            this.contexto = contexto;
        }

        /// <summary>
        /// Propriedade pública com os setores ativos na base.
        /// </summary>
        public IQueryable<Setor> Ativos => contexto.Setores.Where(s => s.Ativo);

        public IQueryable<Setor> SetoresAtivosComLigacoesDoAno(int ano)
        {
            DateTime primeiroDiaDoAno;
            DateTime.TryParse($"1/1/{ano}", out primeiroDiaDoAno);

            var setores = this.Ativos.Include(s => s.Funcionarios);

            foreach (var setor in setores)
            {
                foreach (var funcionario in setor.Funcionarios)
                {
                    contexto.Entry(funcionario)
                        .Collection(f => f.Ligacoes)
                            .Query()
                            .Where(l => l.MesmoAno(primeiroDiaDoAno))
                        .Load();
                }
            }

            return setores;
        }

        /// <summary>
        /// Alteração do setor na base.
        /// </summary>
        /// <param name="objeto">Objeto a ser modificado.</param>
        public void Alterar(Setor objeto)
        {
            contexto.Setores.Update(objeto);
            contexto.SaveChanges();
        }

        /// <summary>
        /// Exclusão lógica do setor na base.
        /// </summary>
        /// <param name="id">Identificador do setor</param>
        public void Excluir(int id)
        {
            var setor = Ativos.First(s => s.Id == id);
            setor.Ativo = false;
            this.Alterar(setor);
        }

        /// <summary>
        /// Inclusão do setor na base.
        /// </summary>
        /// <param name="objeto">Objeto a ser incluído.</param>
        public void Incluir(Setor objeto)
        {
            contexto.Setores.Add(objeto);
            contexto.SaveChanges();
        }
    }
}