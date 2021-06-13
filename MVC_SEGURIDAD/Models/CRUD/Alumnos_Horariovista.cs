using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_SEGURIDAD.Models
{
    public class Alumnos_Horariovista
    {
        public int id_alumnohorario { get; set; }
        public int alumno { get; set; }
        public string grupo { get; set; }
    }
}