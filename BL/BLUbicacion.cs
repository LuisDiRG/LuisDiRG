using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ET;

namespace BL
{
    public class BLUbicacion
    {
        public static DataTable Listado(string cTexto)
        {
            DALUbicacion Datos = new DALUbicacion();
            return Datos.Listado(cTexto);
        }

        public static string Guardar(int nOpcion, Ubicacion ubicacion)
        {
            DALUbicacion Datos = new DALUbicacion();
            return Datos.Guardar(nOpcion, ubicacion);
        }
        /*
        public static string Eliminar(int idUbicacion)
        {
            DALUbicacion Datos = new DALUbicacion();
            return Datos.Eliminar(idUbicacion);
        }*/

        public static DataTable ListadoDesOcupado(string cTexto)
        {
            DALUbicacion Datos = new DALUbicacion();
            return Datos.ListadoDesOcupados(cTexto);
        }


        private static DALUbicacion obj = new DALUbicacion();

        public static List<Ubicacion> ListarUbicacion()
        {
            return obj.ListarUbicacion();
        }

        public static void Agregar(Ubicacion ubi)
        {
            obj.Agregar(ubi);
        }

        public static Ubicacion GetUbicacion(int id)
        {
            return obj.GetUbicacion(id);
        }

        public static void Editar(Ubicacion ubi)
        {
            obj.Editar(ubi);
        }

        public static void Eliminar(int id)
        {
            obj.Eliminar(id);
        }

        public static List<UbicacionDTO> ListaConvertirADTO()
        {
            var ubicacion = ListarUbicacion();
            List<UbicacionDTO> listaUbicacionDTO = new List<UbicacionDTO>();

            foreach (var item in ubicacion) //aqui voy a convertir todo
            {
                UbicacionDTO ubDTO = new UbicacionDTO(); //creo el tpDTO
                //asigno todo igual excepto el idRack
                ubDTO.ID_Ubicacion = item.ID_Ubicacion;
                

                //asigno la desc del rack
                var rack = BLRack.GetRack(item.ID_Rack);
                ubDTO.DescripcionRack = rack.DescripcionRack; ;
                //asigno la desc de la bodega
                var bodega = BLBodega.GetBodega(item.ID_Bodega);
                ubDTO.DescripcionBodega = bodega.DescripcionBodega; ;

                listaUbicacionDTO.Add(ubDTO);
            }

            return listaUbicacionDTO;
        }

        public static UbicacionDTO UnoConvertirADTO(Ubicacion ubi)
        {
            UbicacionDTO ubDTO = new UbicacionDTO(); //creo el tpDTO
                                                     //asigno todo igual excepto el idRack
            ubDTO.ID_Ubicacion = ubi.ID_Ubicacion;

            ubDTO.Activo = ubi.Activo;

            //asigno la desc del rack
            var rack = BLRack.GetRack(ubi.ID_Rack);
            ubDTO.DescripcionRack = rack.DescripcionRack; ;



            //asigno la desc de la bodega
            var bodega = BLBodega.GetBodega(ubi.ID_Bodega);
            ubDTO.DescripcionBodega = bodega.DescripcionBodega; ;

            return ubDTO;
        }
    }
}
