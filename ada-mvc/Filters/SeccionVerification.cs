using ada_mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ada_mvc.Filters
{
    public class SeccionVerification : ActionFilterAttribute
    {
        private Usuarios oUser;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                oUser = (Usuarios)HttpContext.Current.Session["User"];
                if (oUser == null)
                {
                    if (filterContext.Controller is AccesoLoginController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/AccesoLogin/Login");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");
            }
        }
    }
}