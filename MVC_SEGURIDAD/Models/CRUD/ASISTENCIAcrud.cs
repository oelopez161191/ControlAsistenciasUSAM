using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_SEGURIDAD.Models.CRUD
{

        public class AsistenciacrudInsert
        {
            public int id_asistencia { get; set; }

            public DateTime fecha { get; set; }

            public int alumnos_horarios { get; set; }

            public string estado { get; set; }
        }

        public class AsistenciacrudUpdate
        {
            public int id_asistencia { get; set; }

            public DateTime fecha { get; set; }

            public int alumnos_horarios { get; set; }

            public string estado { get; set; }
        }
 
}