using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_SEGURIDAD.Models.CRUD
{
     public class ALUMNOScrudInsert
        {
            public int id_alumnno { get; set; }
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string correo { get; set; }
            public string carrera { get; set; }
        }

        public class ALUMNOScrudUpdate
        {
            public int id_alumno { get; set; }
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string correo { get; set; }
            public string carrera { get; set; }
        }
    
}