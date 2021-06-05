using System.Web;
using System.Web.Mvc;

namespace MVC_SEGURIDAD
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Filters.VerificaSesion()); //agregamos el filtro creado

        }
    }
}
