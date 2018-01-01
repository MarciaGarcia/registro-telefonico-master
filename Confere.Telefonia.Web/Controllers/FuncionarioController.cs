using Confere.Telefonia.Web.Dados;
using Confere.Telefonia.Web.Models;
using Confere.Telefonia.Web.Negocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Confere.Telefonia.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class FuncionarioController : Controller
    {
        private FuncionarioControllerDependencies dependencies;

        public FuncionarioController(FuncionarioControllerDependencies dep)
        {
            dependencies = dep;
        }

        [Route("funcionarios")]
        public ActionResult Index()
        {
            var model = dependencies.FuncionarioRepo.Ativos
                .Include(f => f.Setor)
                .Include(f => f.DestinoFavorito)
                .Select(f => new FuncionarioViewModel
                {
                    Id = f.Id,
                    Nome = f.Nome,
                    TelefoneFavorito = f.TelefoneFavorito,
                    SetorId = f.Setor.Id,
                    Setor = f.Setor.Nome,
                    Destino = f.DestinoFavorito.Nome,
                    DestinoFavoritoId = f.DestinoFavorito.Id,
                    QtdeLigacoes = f.Ligacoes.Count,
                    QtdeLigacoesParticulares = f.Ligacoes.Where(l => l.Particular).Count(),
                })
                .ToList();
            ViewBag.Setores = dependencies.SetorRepo.Ativos.ToList();
            ViewBag.Destinos = dependencies.DestinoRepo.Ativos.ToList();
            return View("Funcionarios", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alterar(FuncionarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var func = dependencies.FuncionarioRepo.Ativos.First(f => f.Id == model.Id);
                func.Nome = model.Nome;
                func.TelefoneFavorito = model.TelefoneFavorito;
                func.SetorId = model.SetorId;
                func.DestinoFavoritoId = model.DestinoFavoritoId;
                dependencies.FuncionarioRepo.Alterar(func);
                return RedirectToAction("Index");
            }
            //TODO: refazer
            return new HttpUnauthorizedResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(int id)
        {
            dependencies.FuncionarioRepo.Excluir(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(FuncionarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var func = new Funcionario
                {
                    Nome = model.Nome,
                    TelefoneFavorito = model.TelefoneFavorito,
                    SetorId = model.SetorId,
                    DestinoFavoritoId = model.DestinoFavoritoId
                };
                dependencies.FuncionarioRepo.Incluir(func);
                return RedirectToAction("Index");
            }
            //TODO: refazer
            return new HttpUnauthorizedResult();
        }
    }
}