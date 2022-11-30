using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ET
{
    public class ProductoDTO
    {
        public int ID_Producto { get; set; }
        public string DescripcionProducto { get; set; }
        public System.DateTime Fecha_Alta { get; set; }
        public int DiferenciaMesesVencimiento { get; set; }
        public decimal Cubicaje { get; set; }
        public int Empaque { get; set; }
        public decimal CostoBruto { get; set; }
        public decimal Descuento { get; set; }
        public decimal IVA { get; set; }
        public decimal CostoNeto { get; set; }
        public string DescripcionRack { get; set; } //como no podemos ponerlo en null, cuando le asignemos una descirpcion, le ponemos "N/A"
        public string DescripcionTipoProducto { get; set; }
        public string DescripcionProveedor { get; set; }
        public string DescripcionUnidadMedida { get; set; }
        public bool Activo { get; set; }


    }
}