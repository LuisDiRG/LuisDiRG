using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ET
{
    public class BodegaDTO
    {
        public int ID_Bodega { get; set; }
        public string DescripcionBodega { get; set; }
        public decimal CubicajeMaximo { get; set; }
        public decimal CubicajeAcumulado { get; set; }
        public bool Activo { get; set; }
        public string DescripcionParametros { get; set; }
    }
}














