using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime PrimeiroDiaDoAno(this DateTime date, int? ano = null)
        {
            ano = ano.GetValueOrDefault(DateTime.Now.Year);
            DateTime primeiroDia;
            DateTime.TryParse($"1/1/{ano}", out primeiroDia);
            return primeiroDia;
        }
    }
}