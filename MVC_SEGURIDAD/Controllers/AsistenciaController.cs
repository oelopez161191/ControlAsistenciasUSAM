using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//importar clases que se usan en el proyecto
using MVC_SEGURIDAD.Filters;
using MVC_SEGURIDAD.Models.CRUD;
using MVC_SEGURIDAD.Models;
using System.Web.Mvc;

namespace MVC_SEGURIDAD.Controllers
{
    public class AsistenciaController : Controller
    {
        // GET: Modelo

        [Autoriza(objOperacion: 1)]
        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(AsistenciacrudInsert modelo)
        {
            using (var dbData = new SEGURIDADEntities())
            {
                Asistencia obj = new Asistencia();
                obj.fecha = modelo.fecha;
                // agregar campos faltantes  
                dbData.Asistencia.Add(obj);
                dbData.SaveChanges();
            }

            return Redirect(Url.Content("~/Asistencia/Consultar"));
        }

        public ActionResult Consultar()
        {
            List<ASISTENCIAvista> list = null;
            using (SEGURIDADEntities bDatos = new SEGURIDADEntities())
            {
                list = (from d in bDatos.Asistencia
                        where d.id_asistencia == 1
                        orderby d.id_asistencia
                        select new ASISTENCIAvista
                        {
                            id_asistencia = d.id_asistencia,
                            alumnos_horarios = d.alumnos_horarios
                        }).ToList();
            }
            return View(list);
        }



    }
}