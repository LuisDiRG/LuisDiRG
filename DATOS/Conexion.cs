using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Conexion
    {

        //propiedades
        private string Base;
        private string Server;
        private bool Seguridad;
        private static Conexion Con = null;

        private Conexion()
        {
            this.Base = "RGSL Proyecto";
            this.Server = "DESKTOP-2L4DRG";
            this.Seguridad = true;
        }

        //Herramientas, NuGeT, Segunda Opcion, EXAMINAR y buscar sql client
        public SqlConnection CrearConexion()
        {
            SqlConnection cadena = new SqlConnection();

            try
            {
                cadena.ConnectionString = "Server=" + this.Server + ";Database=" + this.Base + ";";
                if (Seguridad)
                {
                    cadena.ConnectionString = cadena.ConnectionString + "Integrated Security=SSPI";
                }
                else
                {
                    //cadena.ConnectionString = cadena.ConnectionString + "User ID"
                }
            }
            catch (Exception ex)
            {
                cadena = null;
                throw ex;
            }

            return cadena;
        }

        public static Conexion GetInstancia()
        {
            if (Con == null)
            {
                Con = new Conexion();
            }

            return Con;
        }
    }
}