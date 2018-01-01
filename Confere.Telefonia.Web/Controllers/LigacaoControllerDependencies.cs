using Confere.Telefonia.Web.Dados;

namespace Confere.Telefonia.Web.Controllers
{
    public class LigacaoControllerDependencies
    {
        public FuncionarioRepository FuncionarioRepo { get; }
        public DestinoRepository DestinoRepo { get; }
        public TelefonistaRepository TelefonistaRepo { get; }
        public LigacaoRepository LigacaoRepo { get; }

        public LigacaoControllerDependencies(
            FuncionarioRepository func,
            DestinoRepository dest,
            TelefonistaRepository tel,
            LigacaoRepository lig
            )
        {
            FuncionarioRepo = func;
            DestinoRepo = dest;
            TelefonistaRepo = tel;
            LigacaoRepo = lig;
        }
    }
}