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

        [Autoriza(objOperacion: 3)]
        [HttpPost]
        //ModulosCrudUpdate hace referencia al modelo que viene de la pagina web
        public ActionResult Actualizar(MODULOScrudUpdate modelo)
        {
            //Se valida el modelo que viene 
            if(!ModelState.IsValid)
            {
                return View(modelo);
            }

            using (var bDatos = new SEGURIDADEntities())
            {
                var objModulo = bDatos.MODULOS.Find(modelo.CODMOD);
                objModulo.CODMOD = modelo.CODMOD;
                objModulo.NOMBRE = modelo.NOMBRE;

                //se ejecuta la actualización 
                bDatos.Entry(objModulo).State = System.Data.Entity.EntityState.Modified;
                bDatos.SaveChanges();
            }
            return Redirect(Url.Content("~/Modulos/Consultar"));
        }

        //Accion que se realiza al guardar e editar
        // a Este se le aqrega la página actualizar
        [Autoriza(objOperacion: 3)]
        public ActionResult Actualizar(int? id)
        {
            MODULOScrudUpdate modelo = new MODULOScrudUpdate();

            using (var bDatos = new SEGURIDADEntities())
            {
                var objModulos = bDatos.MODULOS.Find(id);
                modelo.CODMOD = objModulos.CODMOD;
                modelo.NOMBRE = objModulos.NOMBRE;
            }

            return View(modelo);
        }

        //Solo se utiliza un método para borrar
        [Autoriza(objOperacion: 2)]
        public ActionResult Delete(int? id)
        {
            SEGURIDADEntities db = new SEGURIDADEntities();
            MODULOS modulo = db.MODULOS.Find(id);
            db.MODULOS.Remove(modulo);
            db.SaveChanges();

            return Redirect(Url.Content(("~/Modulos/Consultar")));
        }


    }
}