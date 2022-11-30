using ET;
using ProyectoRGSL.DAL;
using System.Collections.Generic;
using System.Data;


namespace BL
{
    public class BLProveedor
    {
        public static DataTable Listado(string cTexto)
        {
            DALProveedor Datos = new DALProveedor();
            return Datos.ListadoPV(cTexto);
        }

        public static string Guardar(int nOpcion, Proveedor proveedor)
        {
            DALProveedor Datos = new DALProveedor();
            return Datos.Guardar(nOpcion, proveedor);
        }

        /* public static string Eliminar(int idProveedor)
         {
             DALProveedor Datos = new DALProveedor();
             return Datos.EliminaPV(idProveedor);
         }*/

        public bool validarDescripciones(string descripcion)
        {
            bool pasa = true;

            DataTable dt = Listado(descripcion);
            if (dt.Rows.Count > 0)
            {
                if (descripcion == (string)dt.Rows[0]["DescripcionProveedor"])
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


        private static DALProveedor obj = new DALProveedor();

        public static List<Proveedor> ListarProveedor()
        {
            return obj.ListarProveedor();
        }

        public static void Agregar(Proveedor proveedor)
        {
            obj.Agregar(proveedor);
        }

        public static Proveedor GetProveedor(int id)
        {
            return obj.GetProveedor(id);
        }

        public static void Editar(Proveedor proveedor)
        {
            obj.Editar(proveedor);
        }

        public static void Eliminar(int id)
        {
            obj.Eliminar(id);
        }


    }
}
