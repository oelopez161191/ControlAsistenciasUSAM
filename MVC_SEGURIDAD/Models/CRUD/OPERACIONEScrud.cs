using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_SEGURIDAD.Models.CRUD
{
    public class OPERACIONEScrudInsert
    {
        public int CODOPERA { get; set; }

        public string NOMOPERA { get; set; }

        public int? CODMOD { get; set; }
    }

    public class OPERACIONEScrudUpdate
    {
        public int CODOPERA { get; set; }

        public string NOMOPERA { get; set; }

        public int? CODMOD { get; set; }
    }

}