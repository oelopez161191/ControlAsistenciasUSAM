using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_SEGURIDAD.Models.CRUD
{
    public class PERSONAcrudInsert
    {

        public int CODPERSONA { get; set; }

        public string NOMBRES { get; set; }

        public string APELLIDOS { get; set; }

        public string GENERO { get; set; }

        public int? CODUSER { get; set; }
    }

    public class PERSONAcrudUpdate
    {
        public int CODPERSONA { get; set; }

        public string NOMBRES { get; set; }

        public string APELLIDOS { get; set; }

        public string GENERO { get; set; }

        public int? CODUSER { get; set; }
    }
}