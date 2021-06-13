using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//importar clases que se usan en el proyecto
using MVC_SEGURIDAD.Filters;
using MVC_SEGURIDAD.Models.CRUD;

namespace MVC_SEGURIDAD.Controllers
{
    public class HorariosController : Controller
    {
        // GET: Horarios
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
        public ActionResult Agregar(HORARIOcrudInsert modelo)
        {
            DateTime ahora = DateTime.Today;
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            using (var dbData = new SEGURIDADEntities())
            {
                horario obj = new horario();
                obj.id_horario = modelo.id_horario;
                obj.hora = modelo.hora;
                dbData.horario.Add(obj);
                dbData.SaveChanges();
            }

            return Redirect(Url.Content("~/Horarios/Consultar"));
        }

        //  [Autoriza(objOperacion: 4)]
        public ActionResult Consultar()
        {
            List<HORARIOvista> list = null;
            using (SEGURIDADEntities bDatos = new SEGURIDADEntities())
            {
                list = (from d in bDatos.horario
                        orderby d.id_horario
                        select new HORARIOvista
                        {
                            id_horario = d.id_horario,
                            hora = d.hora,
                        }).ToList();
            }

            return View(list);
        }

        [Autoriza(objOperacion: 2)]
        public ActionResult Delete(string id)
        {
            SEGURIDADEntities db = new SEGURIDADEntities();
            horario mat = db.horario.Find(id);
            db.horario.Remove(mat);
            db.SaveChanges();
            return Redirect(Url.Content("~/Horarios/Consultar"));
        }
    }
}