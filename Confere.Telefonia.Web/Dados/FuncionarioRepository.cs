using Confere.Telefonia.Web.Extensions;
using Confere.Telefonia.Web.Negocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Dados
{
    public class FuncionarioRepository : IRepository<Funcionario>
    {
        private TelefoniaContexto contexto;

        public FuncionarioRepository(TelefoniaContexto contexto)
        {
            this.contexto = contexto;
        }

        /// <summary>
        /// Propriedade pública com os funcionários ativos na base.
        /// </summary>
        public IQueryable<Funcionario> Ativos => contexto.Funcionarios.Where(f => f.Ativo);

        public IQueryable<Funcionario> FuncionariosAtivosComLigacoesDoAno(int ano)
        {
            DateTime primeiroDiaDoAno;
            DateTime.TryParse($"1/1/{ano}", out primeiroDiaDoAno);

            var funcs = this.Ativos;

            foreach (var func in funcs)
            {
                contexto.Entry(func)
                    .Collection(f => f.Ligacoes)
                    .Query()
                    .Where(l => l.MesmoAno(primeiroDiaDoAno))
                    .Load();
            }

            return funcs;
        }

        /// <summary>
        /// Alteração do funcionário na base.
        /// </summary>
        /// <param name="objeto">Objeto a ser modificado</param>
        public void Alterar(Funcionario objeto)
        {
            contexto.Funcionarios.Update(objeto);
            contexto.SaveChanges();
        }

        /// <summary>
        /// Exclusão lógica do funcionário na base.
        /// </summary>
        /// <param name="id">Identificador do funcionário</param>
        public void Excluir(int id)
        {
            var func = Ativos.First(f => f.Id == id);
            func.Ativo = false;
            this.Alterar(func);
        }

        /// <summary>
        /// Inclusão do funcionário na base.
        /// </summary>
        /// <param name="objeto">Objeto a ser incluído</param>
        public void Incluir(Funcionario objeto)
        {
            contexto.Funcionarios.Add(objeto);
            contexto.SaveChanges();
        }
    }
}