using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ET
{
    public class TodoRecepcionDTO
    {

        public EncabezadoRecepcionDTO encabezado_Recepcion { get; set; }
        public List<ProductoDTO> listaProductos { get; set; }
    }
}