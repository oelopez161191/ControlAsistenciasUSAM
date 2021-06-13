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
    public class GruposController : Controller
    {
        // GET: Grupos
        public ActionResult Index()
        {
            return View();
        }

        [Autoriza(objOperacion: 1)]
        [HttpGet]

        public ActionResult Agregar()
        {
            return View();
        }

        [HttPost]

        public ActionResult Agregar(GRUPOScrudInsert modelo)
        {
            using (var dbData = new SEGURIDADEntities())
            {
                Grupos obj = new Grupos();
                obj.id_grupo = modelo.id_grupo;
                obj.horario_materia = modelo.horario_materia;
                dbData.Grupos.Add(obj);
                dbData.SaveChanges();
            }
            return Redirect(Url.Content("~/Grupos/consultar"));

        }

        [Autoriza(objOperacion: 3)]
        [HttpPost]
        public ActionResult Actualizar(GRUPOScrudUpdate modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            using (var bDatos = new SEGURIDADEntities())
            {
                var objUser = bDatos.Grupos.Find(modelo.id_grupo);
                objUser.id_grupo = modelo.id_grupo;
                objUser.horario_materia = modelo.horario_materia;
                bDatos.Entry(objUser).State = System.Data.Entity.EntityState.Modified;
                bDatos.SaveChanges();
            }
            return Redirect(Url.Content("~/Grupos/Consultar"));
        }

        [Autoriza(objOperacion: 3)]
        public ActionResult Actualizar(string id)
        {
            GRUPOScrudUpdate modelo = new GRUPOScrudUpdate();

            using (var bDatos = new SEGURIDADEntities())
            {
                var objGrupos = bDatos.Grupos.Find(id);
                modelo.id_grupo = objGrupos.id_grupo;
                modelo.horario_materia = objGrupos.horario_materia;
            }

            return View(modelo);
        }




        public ActionResult Consultar()
        {
            List<GRUPOSvista> list = null;

            using (SEGURIDADEntities datos = new SEGURIDADEntities())
            {
                list = (from d in datos.Grupos
                        orderby d.id_grupo
                        select new GRUPOSvista
                        {
                            id_grupo = d.id_grupo,
                            horario_materia = d.horario_materia,
                        }).ToList();
            }
            return View(list);
        }

    }
}