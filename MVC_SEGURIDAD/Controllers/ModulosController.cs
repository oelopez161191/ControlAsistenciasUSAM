using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//importar clases que se usan en el proyecto
using MVC_SEGURIDAD.Filters;
using MVC_SEGURIDAD.Models.CRUD;
using MVC_SEGURIDAD.Models;

namespace MVC_SEGURIDAD.Controllers
{
    public class ModulosController : Controller
    {
        // GET: Modelo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Salir()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Accesos");
        }

        [Autoriza(objOperacion: 1)]
        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Agregar(MODULOScrudInsert modelo)
        {
            using (var dbData = new SEGURIDADEntities())
            {
                MODULOS obj = new MODULOS();
                obj.NOMBRE = modelo.NOMBRE;
                dbData.MODULOS.Add(obj);
                dbData.SaveChanges();
            }

            return Redirect(Url.Content("~/Modulos/Consultar"));
        }


        public ActionResult Consultar()
        {
            List<MODULOSVista> list = null;
            using (SEGURIDADEntities bDatos = new SEGURIDADEntities())
            {
                list = (from d in bDatos.MODULOS
                        where d.CODMOD == 1
                        orderby d.NOMBRE
                        select new MODULOSVista
                        {
                            CODMOD = d.CODMOD,
                            NOMBRE = d.NOMBRE
                        }).ToList();
            }
                return View(list);
        }
    }
}