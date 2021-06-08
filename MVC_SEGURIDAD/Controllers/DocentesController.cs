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
    public class DocentesController : Controller
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
        using (var dbData = new SEGURIDAD_ASISTENCIAEntities())
        {
            Docentes obj = new Docentes();
            obj.nombre = modelo.nombre;
            obj.apellido = modelo.apellido;
            obj.correo = modelo.correo;
            dbData.Docentes.Add(obj);
            dbData.SaveChanges();
        }
        return Redirect(Url.Content("~/Docentes/Consultar"));
    }


    //Se ejecuta al ingresar al crud de Operaciones
    public ActionResult Consultar()
    {
        List<Docentesvista> list = null;

        using (SEGURIDAD_ASISTENCIAEntities datos = new SEGURIDAD_ASISTENCIAEntities())
        {
            list = (from d in datos.Docentes
                    orderby d.id_docente
                    select new Docentesvista
                    {
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

        using (var datos = new SEGURIDAD_ASISTENCIAEntities())
        {
            var objOperaciones = datos.Docentes.Find(modelo.id_docente);
            objOperaciones.nombre = modelo.nombre;
            objOperaciones.apellido = modelo.apellido;
            objOperaciones.correo = modelo.correo;

            //Se ejecuta actualización 
            datos.Entry(objOperaciones).State = System.Data.Entity.EntityState.Modified;
            datos.SaveChanges();
        }

        return Redirect(Url.Content("~/Docentes/Consultar"));
    }


    //Accion que se realiza al guardar e editar
    // a Este se le aqrega la página actualizar
    [Autoriza(objOperacion: 3)]
    public ActionResult Actualizar(int? id)
    {
        DocentescrudUpdate modelo = new DocentescrudUpdate();

        using (var bDatos = new SEGURIDAD_ASISTENCIAEntities())
        {
            var objOperaciones = bDatos.Docentes.Find(id);
            modelo.nombre = objOperaciones.nombre;
            modelo.apellido = objOperaciones.apellido;
            modelo.correo = objOperaciones.correo;
        }

        return View(modelo);
    }

    //solo se utiliza para borrar
    [Autoriza(objOperacion: 2)]
    public ActionResult Delete(int? id)
    {
        SEGURIDAD_ASISTENCIAEntities db = new SEGURIDAD_ASISTENCIAEntities();
        Docentes op = db.Docentes.Find(id);
        db.Docentes.Remove(op);
        db.SaveChanges();

        return Redirect(Url.Content(("~/Docentes/Consultar")));
    }
}
}