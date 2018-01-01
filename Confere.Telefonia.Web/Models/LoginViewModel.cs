using Confere.Telefonia.Web.Seguranca;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(12)]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}