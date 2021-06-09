﻿using System;
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
    public class MateriasController : Controller
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
        [Autoriza(objOperacion: 1)]
        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(MATERIAScrudInsert modelo)
        {
            DateTime ahora = DateTime.Today;
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            using (var dbData = new SEGURIDADEntities())
            {
                Materia obj = new Materia();
                obj.id_materia = modelo.id_materia;
                obj.nombre_materia = modelo.nombre_materia;
                obj.facultad = modelo.facultad;
                dbData.Materia.Add(obj);
                dbData.SaveChanges();
            }

            return Redirect(Url.Content("~/Materias/Consultar"));
        }


        [Autoriza(objOperacion: 3)]
        [HttpPost]
        public ActionResult Actualizar(MATERIAScrudUpdate modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            using (var bDatos = new SEGURIDADEntities())
            {
                var objMateria = bDatos.Materia.Find(modelo.id_materia);
                objMateria.nombre_materia = modelo.nombre_materia;
                objMateria.facultad = modelo.facultad;

                bDatos.Entry(objMateria).State = System.Data.Entity.EntityState.Modified;
                bDatos.SaveChanges();
            }
            return Redirect(Url.Content("~/Materias/Consultar"));
        }

        [Autoriza(objOperacion: 2)]
        public ActionResult Delete(string id)
        {
            SEGURIDADEntities db = new SEGURIDADEntities();
            Materia mat = db.Materia.Find(id);
            db.Materia.Remove(mat);
            db.SaveChanges();
            return Redirect(Url.Content("~/Materia/Consultar"));
        }

        [Autoriza(objOperacion: 3)]
        public ActionResult Actualizar(string id)
        {
            MATERIAScrudUpdate modelo = new MATERIAScrudUpdate();

            using (var bDatos = new SEGURIDADEntities())
            {
                var objdocente = bDatos.Materia.Find(id);
                objdocente.id_materia = modelo.id_materia;
                objdocente.nombre_materia = modelo.nombre_materia;
                objdocente.facultad = modelo.facultad;

            }
            return View(modelo);
        }
        //  [Autoriza(objOperacion: 4)]
        public ActionResult Consultar()
        {
            List<MATERIASvista> list = null;
            using (SEGURIDADEntities bDatos = new SEGURIDADEntities())
            {
                list = (from d in bDatos.Materia
                        orderby d.id_materia
                        select new MATERIASvista
                        {
                            id_materia = d.id_materia,
                            nombre_materia = d.nombre_materia,
                            facultad = d.facultad,
                        }).ToList();


            }

            return View(list);
        }
    }
}