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
    public class OperacionesController : Controller
    {
        // GET: Operadores
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Salir()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Accesos");
        }

        //Se recibe la solicitud para redirigir a página de agregar
        [Autoriza(objOperacion: 1)]
        [HttpGet]

        public ActionResult Agregar()
        {
            return View();
        }

        //Se reciben datos para agregar a la base de datos 
        [HttpPost]
        public ActionResult Agregar(OPERACIONEScrudInsert modelo)
        {
            using (var dbData = new SEGURIDADEntities())
            {
                OPERACIONES obj = new OPERACIONES();
                obj.CODOPERA = modelo.CODOPERA;
                obj.NOMOPERA = modelo.NOMOPERA;
                obj.CODMOD = modelo.CODMOD;
                dbData.OPERACIONES.Add(obj);
                dbData.SaveChanges();
            }
            return Redirect(Url.Content("~/Operaciones/Consultar"));
        }


        //Se ejecuta al ingresar al crud de Operaciones
        public ActionResult Consultar()
        {
            List<OPERACIONESvista> list = null;

            using (SEGURIDADEntities datos = new SEGURIDADEntities())
            {
                list = (from d in datos.OPERACIONES
                        orderby d.CODOPERA
                        select new OPERACIONESvista
                        {
                            CODOPERA = d.CODOPERA,
                            NOMOPERA = d.NOMOPERA,
                            CODMOD = d.CODMOD
                        }).ToList();
            }
            return View(list);
        }

        //obtiene los datos de la página
        //los inserta a la base

        [Autoriza(objOperacion: 3)]
        [HttpPost]
        public ActionResult Actualizar(OPERACIONEScrudUpdate modelo)
        {
            //se valida el modelo que viene 
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            using (var datos = new SEGURIDADEntities())
            {
                var objOperaciones = datos.OPERACIONES.Find(modelo.CODOPERA);
                objOperaciones.CODOPERA = modelo.CODOPERA;
                objOperaciones.NOMOPERA = modelo.NOMOPERA;
                objOperaciones.CODMOD = modelo.CODMOD;

                //Se ejecuta actualización 
                datos.Entry(objOperaciones).State = System.Data.Entity.EntityState.Modified;
                datos.SaveChanges();
            }

            return Redirect(Url.Content("~/Operaciones/Consultar"));
        }


        //Accion que se realiza al guardar e editar
        // a Este se le aqrega la página actualizar
        [Autoriza(objOperacion: 3)]
        public ActionResult Actualizar(int? id)
        {
            OPERACIONEScrudUpdate modelo = new OPERACIONEScrudUpdate();

            using (var bDatos = new SEGURIDADEntities())
            {
                var objOperaciones = bDatos.OPERACIONES.Find(id);
                modelo.CODOPERA = objOperaciones.CODOPERA;
                modelo.NOMOPERA = objOperaciones.NOMOPERA;
                modelo.CODMOD = objOperaciones.CODMOD;
            }

            return View(modelo);
        }

        //solo se utiliza para borrar
        [Autoriza(objOperacion: 2)]
        public ActionResult Delete(int? id)
        {
            SEGURIDADEntities db = new SEGURIDADEntities();
            OPERACIONES op = db.OPERACIONES.Find(id);
            db.OPERACIONES.Remove(op);
            db.SaveChanges();

            return Redirect(Url.Content(("~/Operaciones/Consultar")));
        }
    }

}