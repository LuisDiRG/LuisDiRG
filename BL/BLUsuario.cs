using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ET;
namespace BL
{
    public class BLUsuario
    {

        public static DataTable Listado(string cTexto)
        {
            DALUsuario Datos = new DALUsuario();
            return Datos.Listado(cTexto);
        }

        public static string Guardar(int nOpcion, Usuario usuario)
        {
            DALUsuario Datos = new DALUsuario();
            return Datos.Guardar(nOpcion, usuario);
        }
        /*
        public static string Eliminar(int ID_Usuario)
        {
            DALUsuario Datos = new DALUsuario();
            return Datos.Eliminar(ID_Usuario);
        }*/

        public static DataTable ListadoUsuarioPorIDUsuario(string cTexto)
        {
            DALUsuario Datos = new DALUsuario();
            return Datos.ListadoUsuarioPorIDUsuario(cTexto);
        }

        public bool validarDescripciones(int idCedula, int idUsuario)
        {
            bool pasa = true;

            DataTable dt = Listado(Convert.ToString(idCedula));
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    if (idUsuario == (int)dt.Rows[0]["ID_Usuarios"]) //si voy a cambiar el mismo, (esto porque la descripcion no puedo cambiarla si no hago esto)
                    {
                        pasa = true;
                    }
                    else
                    {
                        if (idCedula == (int)dt.Rows[0]["ID_Cedula"])
                        {
                            pasa = false;
                        }
                        else
                        {
                            pasa = false;
                        }
                    }

                }
                else
                {
                    int indice = 0;
                    foreach (var e in dt.Rows)
                    {
                        Console.WriteLine();
                        if (idCedula == (int)dt.Rows[indice]["ID_Cedula"])
                        {
                            pasa = false;
                        }
                        indice++;

                    }
                }


            }
            else
            {
                pasa = true;
            }

            return pasa;
        }

        private static DALUsuario obj = new DALUsuario();

        public static List<Usuario> ListarUsuario()
        {
            return obj.ListarUsuario();
        }

        public static void Agregar(int opcion, Usuario usu)
        {
            obj.Guardar(opcion,usu);
        }

        public static Usuario GetUsuario(int id)
        {
            return obj.GetUsuario(id);
        }

        public static void Editar(Usuario usua)
        {
            obj.Editar(usua);
        }

        public static void Eliminar(int id)
        {
            obj.Eliminar(id);
        }


        
        public static List<UsuarioDTO> ListaConvertirADTO() 
        {
            var usuarios = ListarUsuario();
            List<UsuarioDTO> listaUsuDTO = new List<UsuarioDTO>();

            foreach (var item in usuarios) //aqui voy a convertir todo
            {
                UsuarioDTO usuDTO = new UsuarioDTO(); //creo el tpDTO
                //asigno todo igual excepto el idRack
                usuDTO.ID_Usuario = item.ID_Usuario;
                usuDTO.PrimerNombre = item.PrimerNombre;
                usuDTO.SegundoNombre = item.SegundoNombre;
                usuDTO.NombreUsuario = item.NombreUsuario;
                usuDTO.Contraseña = item.Contraseña;
                usuDTO.PrimerApellido = item.PrimerApellido;
                usuDTO.SegundoApellido = item.SegundoApellido;
                usuDTO.ID_Cedula = item.ID_Cedula;
               
                usuDTO.Activo = item.Activo;


                //asigno la desc de la bodega
                var rol = BLRol.GetRol(item.ID_Rol);
                usuDTO.DescripcionRol = rol.DescripcionRol;

                listaUsuDTO.Add(usuDTO);
            }

            return listaUsuDTO;
        }

        public static UsuarioDTO UnoConvertirADTO(Usuario item)
        {
            UsuarioDTO usuDTO = new UsuarioDTO(); //creo el tpDTO
                                                  //asigno todo igual excepto el idRack
            usuDTO.ID_Usuario = item.ID_Usuario;
            usuDTO.NombreUsuario = item.NombreUsuario;
            usuDTO.Contraseña = item.Contraseña;
            usuDTO.PrimerApellido = item.PrimerApellido;
            usuDTO.SegundoApellido = item.SegundoApellido;
            usuDTO.ID_Cedula = item.ID_Cedula;

            usuDTO.Activo = item.Activo;


            //asigno la desc de la bodega
            var rol = BLRol.GetRol(item.ID_Rol);
            usuDTO.DescripcionRol = rol.DescripcionRol;

            return usuDTO;
        }

    }
}

