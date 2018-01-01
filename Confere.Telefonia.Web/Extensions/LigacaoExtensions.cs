using Confere.Telefonia.Web.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Extensions
{
    public static class LigacaoExtensions
    {
        public static bool MesmoAno(this Ligacao ligacao, DateTime d2)
        {
            return ligacao.DataHora.Year == d2.Year;
        }

        public static bool Ultimos7Dias(this Ligacao ligacao)
        {
            return UltimosXDias(ligacao, 7);
        }

        public static bool Ultimos30Dias(this Ligacao ligacao)
        {
            return UltimosXDias(ligacao, 30);
        }

        public static bool UltimosXDias(this Ligacao ligacao, int dias)
        {
            var xDiasAtras = DateTime.Now.AddDays(-dias);
            return ligacao.DataHora.CompareTo(xDiasAtras) >= 0;
        }
    }
}