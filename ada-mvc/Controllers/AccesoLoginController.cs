using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ada_mvc.Controllers
{
    public class AccesoLoginController : Controller
    {
        // GET: AccesoLogin
        public ActionResult Login()
        {
            return View();
        }

        // Metodo POST para capturar el User y el Password
        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            try
            {
                // Abrimos conexión a nuestra base de datos
                using (bdEntities db = new bdEntities())
                {
                    // Encriptamos la contraseña que el usuario escribio antes de realizar proceso de autenticación.
                    pass = Encriptation.GetSHA256(pass);
                    // Declaramos variale que captura los datos de los input
                    var oUser = (from d in db.Usuarios
                                 where d.UserName == user.Trim() && d.Password == pass.Trim()
                                 select d).FirstOrDefault();

                    // Validamos la información
                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o contraseña incorrecta";
                        return View();
                    }

                    // Creamos el filtro para bloquear el acceso a las demas páginas
                    Session["User"] = oUser;

                }
                // Si la autenticación del usuario es correcta, redireccionamos a la página de principal.
                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                // En caso de error mandamos un mensaje con el error
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}