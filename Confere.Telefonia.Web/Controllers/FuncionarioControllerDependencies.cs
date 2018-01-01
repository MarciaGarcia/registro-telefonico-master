using Confere.Telefonia.Web.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Confere.Telefonia.Web.Controllers
{
    public class FuncionarioControllerDependencies
    {
        public FuncionarioRepository FuncionarioRepo { get; }
        public SetorRepository SetorRepo { get; }
        public DestinoRepository DestinoRepo { get; }

        public FuncionarioControllerDependencies(
            FuncionarioRepository funcRepository,
            SetorRepository setorRepository,
            DestinoRepository destinoRepository)
        {
            FuncionarioRepo = funcRepository;
            SetorRepo = setorRepository;
            DestinoRepo = destinoRepository;
        }
    }
}