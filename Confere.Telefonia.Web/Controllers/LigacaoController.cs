using Confere.Telefonia.Web.Extensions;
using Confere.Telefonia.Web.Models;
using Confere.Telefonia.Web.Negocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Confere.Telefonia.Web.Controllers
{
    [Authorize]
    public class LigacaoController : Controller
    {
        private LigacaoControllerDependencies dependencies;

        public LigacaoController(LigacaoControllerDependencies dep)
        {
            this.dependencies = dep;
        }

        [HttpGet]
        [Route("ligacoes")]
        public ActionResult Index(int? nosUltimosXDias)
        {
            int ultimosDias = nosUltimosXDias.HasValue ? nosUltimosXDias.Value : 0;
            var model = dependencies.LigacaoRepo.Ativos
                .Include(l => l.Funcionario)
                .Include(l => l.Destino)
                .Include(l => l.Telefonista)
                .Where(l => (ultimosDias==0) || l.UltimosXDias(ultimosDias))
                .Select(l => new LigacaoViewModel
                {
                    Id = l.Id,
                    FuncionarioId = l.Funcionario.Id,
                    FuncionarioNome = l.Funcionario.Nome,
                    DestinoId = l.Destino.Id,
                    DestinoNome = l.Destino.Nome,
                    TelefonistaId = l.Telefonista.Id,
                    TelefonistaNome = l.Telefonista.Nome,
                    DataHora = l.DataHora,
                    Contato = l.Contato,
                    Observacao = l.Observacao,
                    Duracao = l.Duracao,
                    Particular = l.Particular,
                    Telefone = l.Telefone
                })
                .OrderByDescending(l => l.DataHora)
                .ToList();
            switch (ultimosDias)
            {
                case 0: 
                    ViewBag.DescricaoFiltro = "todas as ligações do sistema";
                    break;
                case 7:
                    ViewBag.DescricaoFiltro = "ligações dos últimos 7 dias";
                    break;
                case 30:
                    ViewBag.DescricaoFiltro = "ligações dos últimos 30 dias";
                    break;
                default:
                    ViewBag.DescricaoFiltro = $"ligações no ano de {DateTime.Now.Year}";
                    break;
            }
            return View("Ligacoes", model);
        }

        [HttpGet]
        [Route("ligacoes/ultimos-7-dias")]
        public ActionResult Ultimos7Dias()
        {
            return this.Index(7);
        }

        [HttpGet]
        [Route("ligacoes/ultimos-30-dias")]
        public ActionResult Ultimos30Dias()
        {
            return this.Index(30);
        }

        [HttpGet]
        [Route("ligacoes/neste-ano")]
        public ActionResult NesteAno()
        {
            DateTime primeiroDiaDoAno = DateTime.Now.PrimeiroDiaDoAno();
            int diasEntreHojeEPrimeiroDiaDoAno = Convert.ToInt32(DateTime.Now.Subtract(primeiroDiaDoAno).TotalDays);
            return this.Index(diasEntreHojeEPrimeiroDiaDoAno);
        }

        [HttpGet]
        [Route("nova-ligacao")]
        public ActionResult Nova()
        {
            var model = new LigacaoViewModel {
                DataHora = DateTime.Now,
                HoraInicial = DateTime.Now,
                HoraFinal = DateTime.Now,
            };
            ViewBag.Funcionarios = dependencies.FuncionarioRepo.Ativos;
            ViewBag.Destinos = dependencies.DestinoRepo.Ativos;
            ViewBag.Telefonistas = dependencies.TelefonistaRepo.Ativos;
            return View("NovaLigacao", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(LigacaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ligacao = new Ligacao
                {
                    Telefone = model.Telefone,
                    Duracao = model.ToDuracao(),
                    DataHora = model.DataHora,
                    Contato = model.Contato,
                    Particular = model.Particular,
                    Observacao = model.Observacao,
                    TelefonistaId = model.TelefonistaId,
                    DestinoId = model.DestinoId,
                    FuncionarioId = model.FuncionarioId
                };
                dependencies.LigacaoRepo.Incluir(ligacao);
                return RedirectToAction("Index");
            }
            ViewBag.Funcionarios = dependencies.FuncionarioRepo.Ativos;
            ViewBag.Destinos = dependencies.DestinoRepo.Ativos;
            ViewBag.Telefonistas = dependencies.TelefonistaRepo.Ativos;
            return View("NovaLigacao", model);
        }
    }
}