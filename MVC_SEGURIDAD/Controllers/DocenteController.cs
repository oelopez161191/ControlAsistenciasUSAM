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
    public class DocenteController : Controller
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
        public ActionResult Agregar(DocentescrudInsert modelo)
        {
            using (var dbData = new SEGURIDADEntities())
            {
                Docentes obj = new Docentes();
                obj.id_docente = modelo.id_docente;
                obj.nombre = modelo.nombre;
                obj.apellido = modelo.apellido;
                obj.correo = modelo.correo;
                dbData.Docentes.Add(obj);
                dbData.SaveChanges();
            }
            return Redirect(Url.Content("~/Docente/Consultar"));
        }


        //Se ejecuta al ingresar al crud de Operaciones
        public ActionResult Consultar()
        {
            List<DOCENTESvista> list = null;

            using (SEGURIDADEntities datos = new SEGURIDADEntities())
            {
                list = (from d in datos.Docentes
                        orderby d.id_docente
                        select new DOCENTESvista
                        {
                            id_docente = d.id_docente,
                            nombre = d.nombre,
                            apellido = d.apellido,
                            correo = d.correo
                        }).ToList();
            }
            return View(list);
        }

        //obtiene los datos de la página
        //los inserta a la base

        [Autoriza(objOperacion: 3)]
        [HttpPost]
        public ActionResult Actualizar(DocentescrudUpdate modelo)
        {
            //se valida el modelo que viene 
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            using (var datos = new SEGURIDADEntities())
            {
                var objDocentes = datos.Docentes.Find(modelo.id_docente);
                objDocentes.nombre = modelo.nombre;
                objDocentes.apellido = modelo.apellido;
                objDocentes.correo = modelo.correo;

                //Se ejecuta actualización 
                datos.Entry(objDocentes).State = System.Data.Entity.EntityState.Modified;
                datos.SaveChanges();
            }

            return Redirect(Url.Content("~/Docente/Consultar"));
        }


        //Accion que se realiza al guardar e editar
        // a Este se le aqrega la página actualizar
        [Autoriza(objOperacion: 3)]
        public ActionResult Actualizar(string id)
        {
            DocentescrudUpdate modelo = new DocentescrudUpdate();

            using (var bDatos = new SEGURIDADEntities())
            {
                var objDocentes = bDatos.Docentes.Find(id);
                modelo.id_docente = objDocentes.id_docente;
                modelo.nombre = objDocentes.nombre;
                modelo.apellido = objDocentes.apellido;
                modelo.correo = objDocentes.correo;
            }

            return View(modelo);
        }

        //solo se utiliza para borrar
        [Autoriza(objOperacion: 2)]
        public ActionResult Delete(string id)
        {
            SEGURIDADEntities db = new SEGURIDADEntities();
            Docentes dc = db.Docentes.Find(id);
            db.Docentes.Remove(dc);
            db.SaveChanges();

            return Redirect(Url.Content(("~/Docente/Consultar")));
        }
    }
}