using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


//importar clases que se usan en el proyecto
using MVC_SEGURIDAD.Filters;
using MVC_SEGURIDAD.Models.CRUD;
using MVC_SEGURIDAD.Models;

namespace MVC_SEGURIDAD.Models.CRUD
{
    public class RolesController : Controller
    {
        // GET: ROLES
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Salir()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Accesos");
        }

        // se recibe la solicitud para redirigir a página de agregar
        [Autoriza(objOperacion: 1)]
        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }

        // Se reciben los datos para agregar
        [HttpPost]
        public ActionResult Agregar(ROLEScrudInsert modelo)
        {
            using (var dbData = new SEGURIDADEntities())
            {
                ROLES obj = new ROLES();
                obj.CODROL = modelo.CODROL;
                obj.NOMROL = modelo.NOMROL;
                dbData.ROLES.Add(obj);
                dbData.SaveChanges();
            }

            return Redirect(Url.Content("~/Roles/Consultar"));
        }

        public ActionResult Consultar()
        {

            List<ROLESvista> list = null;

            using (SEGURIDADEntities bDatos = new SEGURIDADEntities())
            {
                list = (from d in bDatos.ROLES
                        orderby d.CODROL
                        select new ROLESvista
                        {
                            CODROL = d.CODROL,
                            NOMROL = d.NOMROL
                        }).ToList();
            }
                return View(list);
        }

        [Autoriza(objOperacion: 3)]
        [HttpPost]
        //hace referencia al modelo que viene de la pagina web
        public ActionResult Actualizar(ROLEScrudUpdate modelo)
        {
            //Se valida el modelo que viene 
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            using (var bDatos = new SEGURIDADEntities())
            {
                var objModulo = bDatos.ROLES.Find(modelo.CODROL);
                objModulo.CODROL = modelo.CODROL;
                objModulo.NOMROL = modelo.NOMROL;

                //se ejecuta la actualización 
                bDatos.Entry(objModulo).State = System.Data.Entity.EntityState.Modified;
                bDatos.SaveChanges();
            }
            return Redirect(Url.Content("~/Roles/Consultar"));
        }

        //Accion que se realiza al guardar e editar
        // a Este se le aqrega la página actualizar
        [Autoriza(objOperacion: 3)]
        public ActionResult Actualizar(int? id)
        {
            ROLEScrudUpdate modelo = new ROLEScrudUpdate();

            using (var datos = new SEGURIDADEntities())
            {
                var roles = datos.ROLES.Find(id);
                modelo.CODROL = roles.CODROL;
                modelo.NOMROL = roles.NOMROL;
            }
            return View(modelo);
        }

        //para borrar
        public ActionResult Delete(int? id)
        {
            SEGURIDADEntities db = new SEGURIDADEntities();
            ROLES rol = db.ROLES.Find(id);
            db.ROLES.Remove(rol);
            db.SaveChanges();

            return Redirect(Url.Content(("~/Roles/Consultar")));
        }

    }
}