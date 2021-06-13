using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC_SEGURIDAD.Filters;
using MVC_SEGURIDAD.Models.CRUD;
using MVC_SEGURIDAD.Models;

namespace MVC_SEGURIDAD.Controllers
{
    public class Alumnos_HorarioController : Controller
    {
        // GET: ALUMNOS_Horario
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
        public ActionResult Agregar(Alumnos_HorariocrudInsert modelo)
        {
            DateTime ahora = DateTime.Today;
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            using (var dbData = new SEGURIDADEntities())
            {
                Alumnos_Horario obj = new Alumnos_Horario();
                obj.id_alumnohorario = 1;
                obj.alumno = modelo.alumno;
                obj.grupo = modelo.grupo;
                dbData.Alumnos_Horario.Add(obj);
                dbData.SaveChanges();
            }
            return Redirect(Url.Content("~/Alumnos_Horario/consultar"));

        }

        [Autoriza(objOperacion: 3)]
        [HttpPost]
        public ActionResult Actualizar(Alumnos_HorariocrudUpdate modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            using (var bDatos = new SEGURIDADEntities())
            {
                var objUser = bDatos.Alumnos_Horario.Find(modelo.id_alumnohorario);
                objUser.id_alumnohorario = modelo.id_alumnohorario;
                objUser.alumno = modelo.alumno;
                objUser.grupo = modelo.grupo;
                bDatos.Entry(objUser).State = System.Data.Entity.EntityState.Modified;
                bDatos.SaveChanges();
            }
            return Redirect(Url.Content("~/Alumnos_Horario/Consultar"));
        }

        [Autoriza(objOperacion: 2)]
        public ActionResult Delete(int? id)
        {
            SEGURIDADEntities db = new SEGURIDADEntities();
            Alumnos_Horario Usaar = db.Alumnos_Horario.Find(id);
            db.Alumnos_Horario.Remove(Usaar);
            db.SaveChanges();
            return Redirect(Url.Content("~/Alumnos_Horario/consultar"));
        }
        [Autoriza(objOperacion: 3)]
        public ActionResult Actualizar(int? id)
        {
            Alumnos_HorariocrudUpdate modelo = new Alumnos_HorariocrudUpdate();

            using (var bDatos = new SEGURIDADEntities())
            {
                var objUser = bDatos.Alumnos_Horario.Find(id);

                objUser.id_alumnohorario = modelo.id_alumnohorario;
                objUser.alumno = modelo.alumno;
                objUser.grupo = modelo.grupo;
            }
            return View(modelo);
        }
        //  [Autoriza(objOperacion: 4)]
        public ActionResult Consultar()
        {
            List<Alumnos_Horariovista> list = null;
            using (SEGURIDADEntities bDatos = new SEGURIDADEntities())
            {
                list = (from d in bDatos.Alumnos_Horario
                        where d.id_alumnohorario == 1
                        orderby d.alumno
                        select new Alumnos_Horariovista
                        {
                            id_alumnohorario = d.id_alumnohorario,
                            alumno = d.alumno,
                            grupo = d.grupo,
                        }).ToList();

            }
            return View(list);
        }
    }
}