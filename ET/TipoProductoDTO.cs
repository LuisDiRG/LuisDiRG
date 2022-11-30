using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ET
{
    public class TipoProductoDTO
    {
        public int ID_TipoProducto { get; set; }
        public string DescripcionTipoProducto { get; set; }
        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }
        public bool Activo { get; set; }
        public string DescripcionRack { get; set; }
        public Nullable<int> StockActual { get; set; }
        public Nullable<int> noRegistrados { get; set; }
    }
}