using ET;
using ProyectoRGSL.DAL;
using System.Collections.Generic;
using System.Data;
using DAL;
namespace BL
{
    public class BLUnidadMedida
    {

        public static DataTable Listado(string cTexto)
        {
            DALUnidadMedida Datos = new DALUnidadMedida();
            return Datos.ListadoUM(cTexto);
        }
        
        public static string Guardar(int nOpcion, Unidad_Medida unidadMedida)
        {
            DALUnidadMedida Datos = new DALUnidadMedida();
            return Datos.Guardar(nOpcion, unidadMedida);
        }

       /* public static string Eliminar(int idUnidadMedida)
        {
            DALUnidadMedida Datos = new DALUnidadMedida();
            return Datos.EliminaUM(idUnidadMedida);
        }*/

        public bool validarDescripciones(string descripcion)
        {
            bool pasa = true;

            DataTable dt = Listado(descripcion);
            if (dt.Rows.Count > 0)
            {
                if (descripcion == (string)dt.Rows[0]["DescripcionUnidadMedida"])
                {
                    pasa = false;
                }
                else
                {
                    pasa = true;
                }
            }
            else
            {
                pasa = true;
            }

            return pasa;
        }

        private static DALUnidadMedida obj = new DALUnidadMedida();

        public static List<Unidad_Medida> ListarUnidadMedida()
        {
            return obj.ListarUnidadMedida();
        }

        public static void Agregar(Unidad_Medida unimedida)
        {
            obj.Agregar(unimedida);
        }

        public static Unidad_Medida GetUnidadMedida(int id)
        {
            return obj.GetUnidadMedida(id);
        }

        public static void Editar(Unidad_Medida unimedida)
        {
            obj.Editar(unimedida);
        }

        public static void Eliminar(int id)
        {
            obj.Eliminar(id);
        }


    }
}
