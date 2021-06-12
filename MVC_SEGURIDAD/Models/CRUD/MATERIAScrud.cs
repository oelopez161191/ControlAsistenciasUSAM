using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_SEGURIDAD.Models.CRUD
{
    public class MATERIAScrudInsert
    {
        public string id_materia { get; set; }
        public string nombre_materia { get; set; }
        public facultades facultad { get; set; }
        public int estado { get; set; }
    }

    public class MATERIAScrudUpdate
    {
        public string id_materia { get; set; }
        public string nombre_materia { get; set; }
        public facultades facultad { get; set; }
        public estadosMateria estado { get; set; }
    }

    public enum estadosMateria
    {
        inactivo,
        activo,
    }

    public enum facultades
    {
        Ciencias_Empresariales,
        Jurisprudencia_y_Ciencias_Sociales,
        Medicina,
        Medicina_Veterinaria_y_Zootecnía,
        Cirugia_Dental,
        Quimica_y_Farmacia,
    }
}