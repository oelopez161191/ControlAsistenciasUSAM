using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_SEGURIDAD.Models.CRUD
{
    public class HORARIOMATERIAvista
    {
        public string codigo_horariomateria { get; set; }
        public int horario { get; set; }
        public string materia { get; set; }
        public string docente { get; set; }
    }
}