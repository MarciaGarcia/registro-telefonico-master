using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confere.Telefonia.Web.Dados
{
    interface IRepository<T>
    {
        IQueryable<T> Ativos { get; }
        void Incluir(T objeto);
        void Alterar(T objeto);
        void Excluir(int id);
    }
}
