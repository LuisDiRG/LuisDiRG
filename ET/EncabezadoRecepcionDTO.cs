using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ET
{
    public class EncabezadoRecepcionDTO
    {

        public int ID_EncabezadoRecepcion { get; set; }
        public string DescripcionEncabezadoRecepcion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string NombreUsuario { get; set; }
        public string DescripcionBodega { get; set; }
        public string DescripcionProveedor { get; set; }
        public string DescripcionTipoProducto {  get; set; }
        public string DescripcionParametro { get; set; }
        public bool Activo { get; set; }
    }
}