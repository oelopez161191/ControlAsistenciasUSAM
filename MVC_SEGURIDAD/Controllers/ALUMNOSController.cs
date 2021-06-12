using MVC_SEGURIDAD.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_SEGURIDAD.Models.CRUD;
using MVC_SEGURIDAD.Models;


namespace MVC_SEGURIDAD.Controllers
{
    public class AlumnosController : Controller
    {
        // GET: ALUMNOS
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult salir()
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

        [HttPost]
        public ActionResult Agregar(ALUMNOScrudInsert modelo)
        {
            DateTime ahora = DateTime.Today;
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            using (var dbData = new SEGURIDADEntities())
            {
                Alumnos obj = new Alumnos();
                obj.id_alumno = modelo.id_alumnno;
                obj.nombre = modelo.nombre;
                obj.apellido = modelo.apellido;
                obj.correo = modelo.correo;
                obj.carrera = modelo.carrera;
                dbData.Alumnos.Add(obj);
                dbData.SaveChanges();
            }
            return Redirect(Url.Content("~/Alumnos/consultar"));

        }

        [Autoriza(objOperacion:3)]
        [HttpPost]
        public ActionResult Actualizar(ALUMNOScrudUpdate modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            using (var bDatos=new SEGURIDADEntities())
            {
                var objUser = bDatos.Alumnos.Find(modelo.id_alumno);
                objUser.id_alumno = modelo.id_alumno;
                objUser.nombre = modelo.nombre;
                objUser.apellido = modelo.apellido;
                objUser.correo = modelo.correo;
                objUser.carrera = modelo.carrera;

                bDatos.Entry(objUser).State = System.Data.Entity.EntityState.Modified;
                bDatos.SaveChanges();
            }
            return Redirect(Url.Content("~/Usuarios/Consultar"));
        }

        [Autoriza(objOperacion:2)]
        public ActionResult Delete(int? id)
        {
            SEGURIDADEntities db = new SEGURIDADEntities();
            Alumnos Usaar = db.Alumnos.Find(id);
            db.Alumnos.Remove(Usaar);
            db.SaveChanges();
            return Redirect(Url.Content("~/Alumnos/consultar"));
        }
        [Autoriza(objOperacion:3)]
        public ActionResult Actualizar(int? id)
        {
            ALUMNOScrudUpdate modelo = new ALUMNOScrudUpdate();

            using(var bDatos = new SEGURIDADEntities())
            {
                var objUser = bDatos.Alumnos.Find(id);

                objUser.id_alumno = modelo.id_alumno;
                objUser.nombre = modelo.nombre;
                objUser.apellido = modelo.apellido;
                objUser.correo = modelo.correo;
                objUser.carrera = modelo.carrera;
            }
            return View(modelo);
        }
        //  [Autoriza(objOperacion: 4)]
        public ActionResult Consultar()
        {
            List<ALUMNOSvista> list = null;
            using (SEGURIDADEntities bDatos =new SEGURIDADEntities())
            {
                list = (from d in bDatos.Alumnos
                        where d.id_alumno == 1
                        orderby d.id_alumno
                        select new ALUMNOSvista
                        {
                            id_alumnno = d.id_alumno,
                            nombre = d.nombre,
                            apellido = d.apellido,
                            correo = d.correo,
                            carrera = d.carrera,
                        }).ToList();

            }
            return View(list);
        }

    }
}