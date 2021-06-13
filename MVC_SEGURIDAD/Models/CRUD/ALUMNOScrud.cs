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
        public carrera carrera { get; set; }
        public int estado { get; set; }
    }

    public class ALUMNOScrudUpdate
    {
        public int id_alumno { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public carrera carrera { get; set; }
        public estadosAlumnos estado { get; set; }
    }

    public enum estadosAlumnos
    {
        inactivo = 0,
        activo = 1,
    }

    public enum carrera
    {
        Tecnico_Desarrollo_de_Sofware,
        Licenciatura_Ciencias_de_la_computacion,
        Tecnico_en_redes,
        Doctorado,
        Lic_Enfermeria,
    }

}