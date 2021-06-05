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
    public class UsuarioController : Controller
    {
        // GET: Usuario
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
        public ActionResult Agregar(USUARIOcrudInsert modelo)
        {
            DateTime ahora = DateTime.Today;
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
          
            using (var dbData = new SEGURIDADEntities())
            {
                USUARIO obj = new USUARIO();
                obj.CODESTADO = 1;
                obj.USUARIO1 = modelo.USUARIO;
                obj.PASSWORD = modelo.PASSWORD;
                obj.CODROL = modelo.CODROL;
                dbData.USUARIO.Add(obj);
                dbData.SaveChanges();
            }

                return Redirect(Url.Content("~/Usuario/Consultar"));
        }


        [Autoriza(objOperacion: 3)]
        [HttpPost]
        public ActionResult Actualizar(USUARIOcrudUpdate modelo)
        {
            if(!ModelState.IsValid)
            {
                return View(modelo);
            }
            using (var bDatos = new SEGURIDADEntities())
            {
                var objUser = bDatos.USUARIO.Find(modelo.CodUser);
                objUser.CODUSER = modelo.CodUser;
                objUser.USUARIO1 = modelo.USUARIO;

                if(modelo.PASSWORD!=null && modelo.PASSWORD.Trim()!="")
                {
                    objUser.PASSWORD = modelo.PASSWORD;
                }
                objUser.CODESTADO = modelo.CODESTADO;
                objUser.CODROL = modelo.CODROL;
                bDatos.Entry(objUser).State = System.Data.Entity.EntityState.Modified;
                bDatos.SaveChanges();
            }
            return Redirect(Url.Content("~/Usuario/Consultar"));
        }



        [Autoriza(objOperacion: 2)]
        public ActionResult Delete(int? id)
        {
            SEGURIDADEntities db = new SEGURIDADEntities();
            USUARIO usuaar = db.USUARIO.Find(id);
            db.USUARIO.Remove(usuaar);
            db.SaveChanges();
            return Redirect(Url.Content("~/Usuario/Consultar"));
        }
        [Autoriza(objOperacion: 3)]
        public ActionResult Actualizar(int? id)
        {
            USUARIOcrudUpdate modelo = new USUARIOcrudUpdate();

            using (var bDatos = new SEGURIDADEntities())
            {
                var objUser = bDatos.USUARIO.Find(id);
                modelo.USUARIO = objUser.USUARIO1;
                modelo.PASSWORD = objUser.PASSWORD;
                modelo.CODESTADO = (int)objUser.CODESTADO;
                modelo.CODROL = (int)objUser.CODROL;
                modelo.CodUser = objUser.CODUSER;

            }
            return View(modelo);
        }
      //  [Autoriza(objOperacion: 4)]
        public ActionResult Consultar()
        {
            List<USUARIOvista> list = null;
            using (SEGURIDADEntities bDatos = new SEGURIDADEntities())
            {
                list = (from d in bDatos.USUARIO
                        where d.CODESTADO == 1
                        orderby d.USUARIO1
                        select new USUARIOvista
                        {
                            CODUSER=d.CODUSER,
                            USUARIO = d.USUARIO1,
                            CODESTADO=d.CODESTADO,
                            CODROL = d.CODROL
                        }).ToList();


            }

                return View(list);
        }
    }
}