using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_SEGURIDAD.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Salir()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Accesos");
        }

        [HttpGet]
        public ActionResult noAutorizado(String operacion,String Modulo,String msjErrorExcp)
        {
            ViewBag.Operacion = operacion;
            ViewBag.Modulo = Modulo;
            ViewBag.msjerror = msjErrorExcp;
            return View();
        }
    }
    
}