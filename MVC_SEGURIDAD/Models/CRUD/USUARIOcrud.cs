using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
namespace MVC_SEGURIDAD.Models.CRUD
{
    public class USUARIOcrudInsert
    {
        [Required]
        public string USUARIO { get; set; }
        [Required]
        [StringLength(8,ErrorMessage ="El {0} debe tener al menos {1} caracteres",MinimumLength =1)]
        [Display(Name ="Password")]
        public string PASSWORD { get; set; }
        [Display(Name = "Confirmacion de Password")]
        [Compare("PASSWORD",ErrorMessage ="Contraseñas no son iguales")]
        public string RePASSWORD { get; set; }
    
        public int CODROL { get; set; }
        
        public int CODESTADO { get; set; }

        public int CodUser { get; set; }

    }

    public class USUARIOcrudUpdate
    {
        [Required]
        public string USUARIO { get; set; }
        
        [StringLength(8, ErrorMessage = "El {0} debe tener al menos {1} caracteres", MinimumLength = 1)]
        [Display(Name = "Password")]
        public string PASSWORD { get; set; }
        [Display(Name = "Confirmacion de Password")]
        [Compare("PASSWORD", ErrorMessage = "Contraseñas no son iguales")]
        public string RePASSWORD { get; set; }

        public int CODROL { get; set; }

        public int CODESTADO { get; set; }

        public int CodUser { get; set; }



    }
}