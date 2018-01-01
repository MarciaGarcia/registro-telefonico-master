using Confere.Telefonia.Web.Dados;
using Confere.Telefonia.Web.Models;
using Confere.Telefonia.Web.Negocio;
using Confere.Telefonia.Web.Seguranca;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Confere.Telefonia.Web.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private UsuarioRepository _repo;

        public UsuarioController(UsuarioRepository repo)
        {
            this._repo = repo;
        }

        [Route("usuarios")]
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View("Usuarios", _repo.Ativos.ToList());
        }

        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Autenticar(LoginViewModel model, string returnUrl)
        {
            var senha = new Senha { TextoPuro = model.Senha };
            var usu = _repo.Autenticar(model.Login, senha);
            if (usu != null)
            {
                Debug.WriteLine("Usuário foi autenticado.");
                var ticket = new FormsAuthenticationTicket(1, usu.Login, DateTime.Now, DateTime.Now.AddMinutes(20), false, string.Join(",", usu.Papeis));
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName,FormsAuthentication.Encrypt(ticket)));
                Debug.WriteLine("Cookie de autenticação foi escrito.");
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    Debug.WriteLine("Definindo o timestamp de login.");
                    usu.UltimoLogin = DateTime.Now;
                    _repo.Alterar(usu);
                    return RedirectToAction("Index", "Home");
                }
            }
            return new HttpUnauthorizedResult();
        }

        [HttpGet]
        public ActionResult Perfil()
        {
            var usu = _repo.Ativos.First(u => u.Login == this.User.Identity.Name);
            return View(usu.ToPerfilViewModel());
        }

        private byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Perfil(PerfilViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usu = _repo.Ativos.First(u => u.Id == model.Id);
                usu.Nome = model.Nome;
                usu.Email = model.Email;
                if (model.ImagemPerfil != null)
                {
                    usu.ImagemPerfil = ConvertToBytes(model.ImagemPerfil);
                }
                if (!string.IsNullOrEmpty(model.Senha))
                {
                    var senha = new Senha { TextoPuro = model.Senha };
                    usu.Senha = senha.TextoHash;
                }
                _repo.Alterar(usu);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ImagemPerfil(int id)
        {
            return ImagemPerfilFromQuery(_repo.Ativos.Where(u => u.Id == id));
        }

        [HttpGet]
        [Route("Usuario/ImagemLogin/{login}")]
        public ActionResult ImagemLogin(string login)
        {
            return ImagemPerfilFromQuery(_repo.Ativos.Where(u => u.Login == login));
        }

        private ActionResult ImagemPerfilFromQuery(IEnumerable<Usuario> query)
        {
            byte[] img = query.Select(u => u.ImagemPerfil).FirstOrDefault();
            if (img != null)
            {
                return File(img, "image/png");
            }
            return File("~/Content/img/avatar.png", "image/png");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Incluir(Usuario usuario)
        {
            //TODO: validar campos obrigatórios no servidor
            _repo.Incluir(usuario);
            //TODO: enviar email para o usuário informando a necessidade de redefinir senha
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Excluir(int id)
        {
            _repo.Excluir(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Alterar(Usuario usuario)
        {
            //TODO: validar campos obrigatórios no servidor
            var usu = _repo.Ativos.First(u => u.Id == usuario.Id);
            usu.Nome = usuario.Nome;
            usu.Login = usuario.Login;
            usu.Email = usuario.Email;
            usu.TextoPapeis = usuario.TextoPapeis;
            _repo.Alterar(usu);
            return RedirectToAction("Index");
        }
    }
}