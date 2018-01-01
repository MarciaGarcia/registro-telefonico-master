using Confere.Telefonia.Web.Dados;
using Confere.Telefonia.Web.Extensions;
using Confere.Telefonia.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Confere.Telefonia.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class DestinoController : Controller
    {
        private DestinoRepository _repo;

        public DestinoController(DestinoRepository repositorio)
        {
            _repo = repositorio;
        }

        [Route("destinos")]
        public ActionResult Index()
        {
            var anoAtual = DateTime.Now.Year;
            var model = _repo.DestinosAtivosComLigacoesDoAno(anoAtual)
                .Select(d => new DestinoViewModel {
                    Id = d.Id,
                    Nome = d.Nome,
                    QtdeLigacoes = d.Ligacoes.Count(),
                    NumeroFuncionarios = d.Ligacoes.Select(l => l.Funcionario.Id).Distinct().Count(),
                    DuracaoMedia = d.Ligacoes.CalculaDuracaoMedia(),
                    AnoAtual = anoAtual,
                })
                .ToList();
            return View("Destinos", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(DestinoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var destino = model.ToDestino();
                _repo.Incluir(destino);
                return RedirectToAction("Index");
            }
            //TODO: revisar o retorno qdo houver problemas de validação
            return new HttpUnauthorizedResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alterar(DestinoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var destino = _repo.Ativos.First(d => d.Id == model.Id);
                destino.Nome = model.Nome;
                _repo.Alterar(destino);
                return RedirectToAction("Index");
            }
            //TODO: revisar o retorno qdo houver problemas de validação
            return new HttpUnauthorizedResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(int id)
        {
            _repo.Excluir(id);
            return RedirectToAction("Index");
        }
    }
}