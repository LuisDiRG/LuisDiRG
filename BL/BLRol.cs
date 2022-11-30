using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL;
using ET;

namespace BL
{
    public class BLRol
    {

        public static DataTable Listado(string cTexto)
        {
            DALRol Datos = new DALRol();
            return Datos.Listado(cTexto);
        }

        public static string Guardar(int nOpcion, Rol rol)
        {
            DALRol Datos = new DALRol();
            return Datos.Guardar(nOpcion, rol);
        }
        /*
        public static string Eliminar(int IdRol)
        {
            DALRol Datos = new DALRol();
            return Datos.Eliminar(IdRol);
        }*/
    public bool validarDescripciones(string descripcion)
    {
        bool pasa = true;

        DataTable dt = Listado(descripcion);
        if (dt.Rows.Count > 0)
        {
            if (descripcion == (string)dt.Rows[0]["DescripcionRol"])
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

        public static DataTable CargarComboBoxPerfil()
        {
            DALRol Datos = new DALRol();
            return Datos.CargarComboBoxPerfil();
        }

        private static DALRol obj = new DALRol();

        public static List<Rol> ListarRol()
        {
            return obj.ListarRol();
        }

        public static void Agregar(Rol rol)
        {
            obj.Agregar(rol);
        }

        public static Rol GetRol(int id)
        {
            return obj.GetRol(id);
        }

        public static void Editar(Rol rol)
        {
            obj.Editar(rol);
        }

        public static void Eliminar(int id)
        {
            obj.Eliminar(id);
        }


       


    }
}