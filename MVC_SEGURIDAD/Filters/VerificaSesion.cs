using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using MVC_SEGURIDAD.Models;
using MVC_SEGURIDAD.Controllers;
using MVC_SEGURIDAD.Models.CRUD;

namespace MVC_SEGURIDAD.Filters
{
    public class VerificaSesion : ActionFilterAttribute //heredar del ActionFilterAttribute
    {
        USUARIO obUsuario = new USUARIO();
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                base.OnActionExecuted(filterContext);
                obUsuario = (USUARIO)HttpContext.Current.Session["Usuario"];//Session en controller
                if (obUsuario == null)//Si la sesion es nula
                {
                    if (filterContext.Controller is AccesosController == false) // valida contolador 
                    {
                        filterContext.HttpContext.Response.Redirect("~/Accesos/Ingresar"); //envia a login
                    }
                }
            }
            catch(Exception)
            {
                filterContext.Result= new RedirectResult("~/Accesos/Ingresar"); //envia a login

            }
            }

    }
}