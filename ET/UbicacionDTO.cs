using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ET
{
    public class UbicacionDTO
    {
        public int ID_Ubicacion { get; set; }
        public bool Activo { get; set; }
        public string DescripcionRack { get; set; }
        public string DescripcionBodega { get; set; }
        public Nullable<bool> Ocupado { get; set; }


    }
}