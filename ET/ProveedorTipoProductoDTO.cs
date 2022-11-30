using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ET
{
    public class ProveedorTipoProductoDTO
    {

        public int idProveedorTipoProducto { get; set; }
        public string descripcionProveedor { get; set; }
        public string descripcionTipoProducto { get; set; }
        public bool Activo { get; set; }
    }
}