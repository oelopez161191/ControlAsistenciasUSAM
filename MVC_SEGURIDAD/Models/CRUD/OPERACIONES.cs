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
    
    public partial class OPERACIONES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OPERACIONES()
        {
            this.ROL_OPERA = new HashSet<ROL_OPERA>();
        }
    
        public int CODOPERA { get; set; }
        public string NOMOPERA { get; set; }
        public Nullable<int> CODMOD { get; set; }
    
        public virtual MODULOS MODULOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROL_OPERA> ROL_OPERA { get; set; }
    }
}
