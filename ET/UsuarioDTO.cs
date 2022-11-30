using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ET
{
    public class UsuarioDTO
    {
        public int ID_Usuario { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string NombreUsuario { get; set; } 
        public string Contraseña { get; set; }
        public bool Activo { get; set; } 
        public string DescripcionRol { get; set; } 
        public string ID_Cedula { get; set; } 
     
        
    }
}


