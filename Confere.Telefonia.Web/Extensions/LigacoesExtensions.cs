using Confere.Telefonia.Web.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Extensions
{
    public static class LigacoesExtensions
    {
        public static double CalculaDuracaoMedia(this IList<Ligacao> ligacoes)
        {
            return ligacoes
                .DefaultIfEmpty(new Ligacao { Duracao = TimeSpan.FromMinutes(0) } )
                .Select(l => l.Duracao.TotalMinutes).Average();
        }
    }
}