﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_SEGURIDAD.Models.CRUD
{
    public class DocentescrudInsert
    {
        public string id_docente { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string correo { get; set; }
    }

    public class DocentescrudUpdate
    {
        public string id_docente { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string correo { get; set; }
    }
}