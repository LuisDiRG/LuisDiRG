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
    public class BLRolTipoProducto
    {

        public static DataTable Listado(string idRolTP)
        {
            DALRolTipoProducto Datos = new DALRolTipoProducto();
            return Datos.Listado(idRolTP);
        }

        public static string Guardar(int nOpcion, RolTipoProducto RTP)
        {
            DALRolTipoProducto Datos = new DALRolTipoProducto();
            return Datos.Guardar(nOpcion, RTP);
        }
        /*
        public static string Eliminar(int idRolTipoP)
        {
            DALRolTipoProducto Datos = new DALRolTipoProducto();
            return Datos.Eliminar(idRolTipoP);
        }*/

        public static DataTable ListadoporIntAmbos(int idRol, int idTipoProducto)
        {
            DALRolTipoProducto Datos = new DALRolTipoProducto();
            return Datos.ListadoporIntAmbos(idRol, idTipoProducto);

        }

        public static DataTable ListadoporRol(int idRol)
        {
            DALRolTipoProducto Datos = new DALRolTipoProducto();
            return Datos.ListadoporRol(idRol);

        }

        private static DALRolTipoProducto obj = new DALRolTipoProducto();

        public static List<RolTipoProducto> ListarRolTipoProducto()
        {
            return obj.ListarRolesTipoProducto();
        }

        public static void Agregar(RolTipoProducto rtp)
        {
            obj.Agregar(rtp);
        }

        public static RolTipoProducto GetRolTipoProducto(int id)
        {
            return obj.GetRolTipoProducto(id);
        }

        public static void Editar(RolTipoProducto rtp)
        {
            obj.Editar(rtp);
        }

        public static void Eliminar(int id)
        {
            obj.Eliminar(id);
        }

        public static List<RolTipoProductoDTO> ListaConvertirADTO()
        {
            var rack = ListarRolTipoProducto();
            List<RolTipoProductoDTO> listaRolTipoDTO = new List<RolTipoProductoDTO>();

            foreach (var item in rack) //aqui voy a convertir todo
            {
                RolTipoProductoDTO rotpDTO = new RolTipoProductoDTO(); //creo el tpDTO
                //asigno todo igual excepto el idRack
                rotpDTO.idRolTipoProducto = item.idRolTipoProducto;

                var rol = BLRol.GetRol(item.idRol);
                rotpDTO.DescripcionRol = rol.DescripcionRol;

                var tipoproducto = BLTipoProducto.GetTipoProducto(item.idTipoProducto);
                rotpDTO.DescripcionTipoProducto = tipoproducto.DescripcionTipoProducto; ;

                listaRolTipoDTO.Add(rotpDTO);

            }

            return listaRolTipoDTO;
        }

        public static RolTipoProductoDTO UnoConvertirADTO(RolTipoProducto item)
        {
            RolTipoProductoDTO rotpDTO = new RolTipoProductoDTO(); //creo el tpDTO
                                                                 //asigno todo igual excepto el idRack
            rotpDTO.idRolTipoProducto = item.idRolTipoProducto;

            var rol = BLRol.GetRol(item.idRol);
            rotpDTO.DescripcionRol = rol.DescripcionRol;


            var tipoproducto = BLTipoProducto.GetTipoProducto(item.idTipoProducto);
            rotpDTO.DescripcionTipoProducto = tipoproducto.DescripcionTipoProducto; ;

            

            return rotpDTO;
        }



    }

}

