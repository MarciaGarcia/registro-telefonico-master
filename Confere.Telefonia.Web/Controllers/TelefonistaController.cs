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
    public class TelefonistaController : Controller
    {
        private TelefonistaRepository _repo;

        public TelefonistaController(TelefonistaRepository repositorio)
        {
            _repo = repositorio;
        }

        [Route("telefonistas")]
        public ActionResult Index()
        {
            var model = _repo.TelefonistasAtivosComLigacoesDoAno(DateTime.Now.Year)
                .Select(t => new TelefonistaViewModel {
                    Id = t.Id,
                    Nome = t.Nome,
                    RegistrosNaSemana = t.Ligacoes.Where(l => l.Ultimos7Dias()).Count(),
                    RegistrosNoMes = t.Ligacoes.Where(l => l.Ultimos30Dias()).Count(),
                    RegistrosNoAno = t.Ligacoes.Count,
                })
                .ToList();
            return View("Telefonistas", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alterar(TelefonistaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var telefonista = _repo.Ativos.First(t => t.Id == model.Id);
                telefonista.Nome = model.Nome;
                _repo.Alterar(telefonista);
                return RedirectToAction("Index");
            }
            //TODO: rever o retorno quando o formulário não está válido
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
        public ActionResult Incluir(TelefonistaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var telefonista = model.ToTelefonista();
                _repo.Incluir(telefonista);
                return RedirectToAction("Index");
            }
            //TODO: rever o retorno quando o formulário não está válido
            return new HttpUnauthorizedResult();
        }
    }
}