using MVC_SEGURIDAD.Filters;
using MVC_SEGURIDAD.Models.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_SEGURIDAD.Controllers
{
    public class Horario_MateriaController : Controller
    {
        // GET: Horario_Materia
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
        public ActionResult Agregar(HORARIOMATERIAcrudInsert modelo)
        {
            using (var dbData = new SEGURIDADEntities())
            {
                Horario_Materia obj = new Horario_Materia();
                obj.idcodigo_horariomateria = modelo.idcodigo_horariomateria;
                obj.horario = modelo.horario;
                obj.materia = modelo.materia;
                obj.docente = modelo.docente;
                dbData.Horario_Materia.Add(obj);
                dbData.SaveChanges();
            }
            return Redirect(Url.Content("~/Horario_Materia/Consultar"));
        }


        //Se ejecuta al ingresar al crud de Operaciones
        public ActionResult Consultar()
        {
            List<HORARIOMATERIAvista> list = null;

            using (SEGURIDADEntities datos = new SEGURIDADEntities())
            {
                list = (from d in datos.Horario_Materia
                        orderby d.idcodigo_horariomateria
                        select new HORARIOMATERIAvista
                        {
                            codigo_horariomateria = d.idcodigo_horariomateria,
                            horario = d.horario,
                            materia = d.materia,
                            docente = d.docente
                        }).ToList();
            }
            return View(list);
        }

        //obtiene los datos de la página
        //los inserta a la base

        [Autoriza(objOperacion: 3)]
        [HttpPost]
        public ActionResult Actualizar(HORARIOMATERIAcrudUpdate modelo)
        {
            //se valida el modelo que viene 
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            using (var datos = new SEGURIDADEntities())
            {
                var objHorario_Materia = datos.Horario_Materia.Find(modelo.id_Horariocontroller);
                objHorario_Materia.idcodigo_horariomateria = modelo.idcodigo_horariomateria;
                objHorario_Materia.horario = modelo.horario;
                objHorario_Materia.materia = modelo.materia;
                objHorario_Materia.docente = modelo.docente;

                //Se ejecuta actualización 
                datos.Entry(objHorario_Materia).State = System.Data.Entity.EntityState.Modified;
                datos.SaveChanges();
            }

            return Redirect(Url.Content("~/Horario_Materia/Consultar"));
        }


        //Accion que se realiza al guardar e editar
        // a Este se le aqrega la página actualizar
        [Autoriza(objOperacion: 3)]
        public ActionResult Actualizar(string id)
        {
            HORARIOMATERIAcrudUpdate modelo = new HORARIOMATERIAcrudUpdate();

            using (var bDatos = new SEGURIDADEntities())
            {
                var objHorario_Materia = bDatos.Horario_Materia.Find(id);
                objHorario_Materia.idcodigo_horariomateria = modelo.idcodigo_horariomateria;
                modelo.horario = objHorario_Materia.horario;
                modelo.materia = objHorario_Materia.materia;
                modelo.docente = objHorario_Materia.docente;
            }

            return View(modelo);
        }

    }
}