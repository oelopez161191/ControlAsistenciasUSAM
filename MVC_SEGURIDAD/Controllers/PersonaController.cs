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
    public class PersonaController : Controller
    {
        // GET: Persona
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

        //Se reciben datos para apregar 
        [HttpPost]
        public ActionResult Agregar(PERSONAcrudInsert modelo)
        {
            using (var dbData = new SEGURIDADEntities())
            {
                PERSONAS obj = new PERSONAS();
                obj.CODPERSONA = modelo.CODPERSONA;
                obj.NOMBRES = modelo.NOMBRES;
                obj.APELLIDOS = modelo.APELLIDOS;
                obj.GENERO = modelo.GENERO;
                obj.CODUSER= modelo.CODUSER;
                dbData.PERSONAS.Add(obj);
                dbData.SaveChanges();
            }
            return Redirect(Url.Content("~/Persona/Consultar"));
        }


        public ActionResult Consultar()
        {
            List<PERSONAvista> list = null;
            using (SEGURIDADEntities bDatos = new SEGURIDADEntities())
            {
                list = (from d in bDatos.PERSONAS
                        orderby d.CODPERSONA
                        select new PERSONAvista
                        {
                            CODPERSONA = d.CODPERSONA,
                            NOMBRES = d.NOMBRES,
                            APELLIDOS = d.APELLIDOS,
                            GENERO = d.GENERO,
                            CODUSER = d.CODUSER
                        }).ToList();
            }
            return View(list);
        }


        [Autoriza(objOperacion: 3)]
        [HttpPost]
        //ModulosCrudUpdate hace referencia al modelo que viene de la pagina web
        public ActionResult Actualizar(PERSONAcrudUpdate modelo)
        {
            //Se valida el modelo que viene 
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            using (var bDatos = new SEGURIDADEntities())
            {
                var objPersona = bDatos.PERSONAS.Find(modelo.CODPERSONA);
                objPersona.CODPERSONA = modelo.CODPERSONA;
                objPersona.NOMBRES = modelo.NOMBRES;
                objPersona.APELLIDOS = modelo.APELLIDOS;
                objPersona.GENERO = modelo.GENERO;
                objPersona.CODUSER = modelo.CODUSER;

                //se ejecuta la actualización 
                bDatos.Entry(objPersona).State = System.Data.Entity.EntityState.Modified;
                bDatos.SaveChanges();
            }
            return Redirect(Url.Content("~/Persona/Consultar"));
        }

        //Accion que se realiza al guardar e editar
        // a Este se le aqrega la página actualizar
        [Autoriza(objOperacion: 3)]
        public ActionResult Actualizar(int? id)
        {
            PERSONAcrudUpdate modelo = new PERSONAcrudUpdate();

            using (var bDatos = new SEGURIDADEntities())
            {
                var objPersona = bDatos.PERSONAS.Find(id);
                modelo.CODPERSONA = objPersona.CODPERSONA;
                modelo.NOMBRES = objPersona.NOMBRES;
                modelo.APELLIDOS = objPersona.APELLIDOS;
                modelo.GENERO = objPersona.GENERO;
                modelo.CODUSER = objPersona.CODUSER;
            }

            return View(modelo);
        }

        //Solo se utiliza un método para borrar
        [Autoriza(objOperacion: 2)]
        public ActionResult Delete(int? id)
        {
            SEGURIDADEntities db = new SEGURIDADEntities();
            PERSONAS p = db.PERSONAS.Find(id);
            db.PERSONAS.Remove(p);
            db.SaveChanges();

            return Redirect(Url.Content(("~/Persona/Consultar")));
        }

    }

}