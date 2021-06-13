using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_SEGURIDAD.Models.CRUD
{

    public class HORARIOMATERIAcrudInsert 
    {
        public string idcodigo_horariomateria { get; set; }
        public int horario { get; set; }
        public string materia { get; set; }
        public string docente { get; set; }
    }

    public class HORARIOMATERIAcrudUpdate
    {
        public string idcodigo_horariomateria { get; set; }
        public int horario { get; set; }
        public string materia { get; set; }
        public string docente { get; set; }
        public object[] id_Horariocontroller { get; internal set; }
    }
}