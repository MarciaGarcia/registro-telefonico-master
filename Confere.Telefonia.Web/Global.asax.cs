using Confere.Telefonia.Web.Dados;
using Confere.Telefonia.Web.Negocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Confere.Telefonia.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            using (var contexto = new TelefoniaContexto())
            {
                contexto.Database.Migrate();
                // retirar quando for rodar em outro servidor .....
                //var usu = contexto.Usuarios.FirstOrDefault(u => u.Login == "admin");
                //if (usu != null)
                //{
                //    contexto.Usuarios.Remove(usu);
                //}
                //usu = new Usuario
                //{
                //    Nome = "Administrador",
                //    Login = "admin",
                //    Email = "marcinhagarciarj@gmail.com",
                //    TextoPapeis = "Administrador",
                //};
                //contexto.Usuarios.Add(usu);
                //contexto.SaveChanges();

            }
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var cookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                Context.User = new GenericPrincipal(new GenericIdentity(ticket.Name), ticket.UserData.Split(','));
                Debug.WriteLine(cookie);
            }
        }
    }
}
