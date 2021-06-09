using MVC_SEGURIDAD.Models;
using MVC_SEGURIDAD.Models.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_SEGURIDAD.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class Autoriza : AuthorizeAttribute
    {
        private USUARIO objUser;
        private SEGURIDAD_ASISTENCIAEntities objDatos = new SEGURIDAD_ASISTENCIAEntities();
        private int objOperacion;

        public Autoriza(int objOperacion =0) {
            this.objOperacion = objOperacion;
        }


        public string getNombreOperacion(int CodOpra) {
            var nOper = from o in objDatos.OPERACIONES
                        where o.CODOPERA == CodOpra
                        select o.NOMOPERA;
            String nombOperac;
            try {
                nombOperac = nOper.First();

            }
            catch {
                nombOperac = "";
            }
            return nombOperac;
        }

        public string getNombreModulo(int? CodMod)
        {
            var nModulo = from modulo in objDatos.MODULOS
                        where modulo.CODMOD == CodMod
                        select modulo.NOMBRE;
            String nombModulo;
            try
            {
                nombModulo = nModulo.First();

            }
            catch
            {
                nombModulo = "";
            }
            return nombModulo;
        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String nombreOperacion="";
            String nombreModulo="";
            try {
                objUser = (USUARIO)HttpContext.Current.Session["Usuario"];                 
                    var oprLst= from d in objDatos.ROL_OPERA
                           where d.CODROL== objUser.CODROL &&
                                 d.CODROLOPERA == objOperacion
                                select d;

                if (oprLst.ToList().Count()==0) {
                    var objOpr = objDatos.OPERACIONES.Find(objOperacion);
                    int? codModulo = objOpr.CODMOD;
                    nombreOperacion = getNombreOperacion(objOperacion);
                    nombreModulo = getNombreModulo(codModulo);
                    filterContext.Result = new RedirectResult("~/Error/noAutorizado?operacion="+ nombreOperacion+ ",Modulo="+nombreModulo+ ",msjErrorExcp='falla'"); //envia a login
                }

            }
            catch (Exception) {
                filterContext.Result = new RedirectResult("~/Error/noAutorizado?operacion=" + nombreOperacion + ",Modulo=" + nombreModulo + ",msjErrorExcp='falla'"); //envia a login
            }

        }
    }
}