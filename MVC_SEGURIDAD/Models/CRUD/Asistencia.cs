//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_SEGURIDAD.Models.CRUD
{
    using System;
    using System.Collections.Generic;
    
    public partial class Asistencia
    {
        public int id_asistencia { get; set; }
        public System.DateTime fecha { get; set; }
        public int alumnos_horarios { get; set; }
        public string estado { get; set; }
    
        public virtual Alumnos_Horario Alumnos_Horario { get; set; }
    }
}
