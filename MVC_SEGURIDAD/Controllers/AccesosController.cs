using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC_SEGURIDAD.Models;
using MVC_SEGURIDAD.Models.CRUD;

namespace MVC_SEGURIDAD.Controllers
{
    public class AccesosController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        // GET: Accesos

        public ActionResult Ingresar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ingresar(string user, string password)
        {
            try
            {
                using (SEGURIDAD_ASISTENCIAEntities bDatos = new SEGURIDAD_ASISTENCIAEntities())
                {
                    var usr = (from d in bDatos.USUARIO
                               where d.USUARIO1 == user.Trim() &&
                                     d.PASSWORD == password.Trim() &&
                                     d.CODESTADO == 1
                               select d).FirstOrDefault();
                    if (usr == null)
                    {
                        ViewBag.Error = "Password y user No existe";
                        return View();
                    }
                    else
                    {
                        Session["Usuario"] = usr;
                        Session["Usr"] = user;
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

    }
}
