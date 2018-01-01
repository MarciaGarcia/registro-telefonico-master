using Confere.Telefonia.Web.Dados;
using Confere.Telefonia.Web.Extensions;
using Confere.Telefonia.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Confere.Telefonia.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class SetorController : Controller
    {
        private SetorRepository _repo;

        public SetorController(SetorRepository repositorio)
        {
            _repo = repositorio;
        }

        [Route("setores")]
        public ActionResult Index()
        {
            var model = _repo.SetoresAtivosComLigacoesDoAno(DateTime.Now.Year)
                .Select(s => new SetorViewModel
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Sigla = s.Sigla,
                    NumeroFuncionarios = s.Funcionarios.Where(f => f.Ativo).Count(),
                    QtdeLigacoes = s.Funcionarios.Select(f => f.Ligacoes.Count).Sum(),
                    QtdeLigacoesParticulares = s.Funcionarios.Select(f => f.Ligacoes.Where(l => l.Particular).Count()).Sum(),
                    DuracaoMedia = s.Funcionarios.Select(f => f.Ligacoes.CalculaDuracaoMedia()).DefaultIfEmpty(0.0).Average(),
                })
                .ToList();
            return View("Setores", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alterar(SetorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var setor = _repo.Ativos.First(s => s.Id == model.Id);
                setor.Nome = model.Nome;
                setor.Sigla = model.Sigla;
                _repo.Alterar(setor);
                return RedirectToAction("Index");
            }
            //TODO: revisar o resultado quando o formulário for invalido
            return new HttpUnauthorizedResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(int id)
        {
            _repo.Excluir(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(SetorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var setor = model.ToSetor();
                _repo.Incluir(setor);
                return RedirectToAction("Index");
            }
            //TODO: revisar o resultado quando o formulário for invalido
            return new HttpUnauthorizedResult();
        }
    }
}