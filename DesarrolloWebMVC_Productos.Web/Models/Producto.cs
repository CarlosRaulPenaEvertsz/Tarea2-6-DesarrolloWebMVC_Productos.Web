using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesarrolloWebMVC_Productos.Web.Models
{
    public class Producto
    {
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public double Precio { get; set; }
    }
}