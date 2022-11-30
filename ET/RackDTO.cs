using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ET
{
    public class RackDTO
    {

        public int ID_Rack { get; set; }
        public string DescripcionRack { get; set; }
        public decimal CubicajeAcumulado { get; set; }
        public decimal CubicajeMaximo { get; set; }
        public bool Activo { get; set; }
        public string DescripcionBodega { get; set; }
    }
}