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
    public class BLInicioSesion
    {

        public bool VerificarDatos( string userName, string password)
        {
            DALInicioSesion datos = new DALInicioSesion();

            DataTable dt = datos.ListadoIS(password, userName);
            bool pasa = false;

            if (dt.Rows.Count > 0)//si me pasa un usuario
            {
                pasa = true;
            }else
            {
                pasa = false;
            }

            return pasa;

        }

        public int ObtenerIDRol( string userName, string password)
        {
            DALInicioSesion datos = new DALInicioSesion();

            DataTable dt = datos.ListadoIS(password, userName);
            int idRol = 0;


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    idRol = (int)dr["ID_Rol"];
                }
            }


            return idRol;
        }

        public int ObtenerIDUsuario(string userName, string password)
        {
            DALInicioSesion datos = new DALInicioSesion();

            DataTable dt = datos.ListadoIS(password, userName);
            int idUsuario = 0;


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    idUsuario = (int)dr["ID_Usuarios"];
                }
            }


            return idUsuario;
        }

    }
}
