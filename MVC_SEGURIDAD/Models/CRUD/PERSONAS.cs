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
    
    public partial class PERSONAS
    {
        public int CODPERSONA { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }
        public string GENERO { get; set; }
        public Nullable<int> CODUSER { get; set; }
    
        public virtual USUARIO USUARIO { get; set; }
    }
}
