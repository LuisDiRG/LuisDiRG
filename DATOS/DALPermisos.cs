using DATOS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;
namespace DAL
{
    public class DALPermisos
    {

        public void GuardarPermiso(int nOpcion, Permisos ep)
        {
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                int bit = 0;

                if (ep.Permitidos)
                {
                    bit = 1;
                }

                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_GuardarPermiso", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@nOpcion", SqlDbType.Int).Value = nOpcion;
                Comando.Parameters.Add("@pidPermiso", SqlDbType.Int).Value = ep.ID_Permisos;
                Comando.Parameters.Add("@pidRol", SqlDbType.Int).Value = ep.ID_Rol;
                Comando.Parameters.Add("@pidOpcion", SqlDbType.Int).Value = ep.ID_Opcion;
                Comando.Parameters.Add("@ppermitido", SqlDbType.Bit).Value = bit;
                SQLCon.Open();
                Comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SQLCon.State == ConnectionState.Open) SQLCon.Close();
            }
        }


        public List<Permisos> SeleccionarOpcion(int idPerfil)
        {
            DataTable dt = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                //db.Configuration.LazyLoadingEnabled = false;
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_SeleccionarOpcion", SQLCon);
                SqlDataAdapter da = new SqlDataAdapter();

                Comando.CommandType = CommandType.StoredProcedure;//EL TIPO DE COMANDO QUE ES UN SP
                Comando.Parameters.Add("@pidRol", SqlDbType.Int).Value = idPerfil;
                da.SelectCommand = Comando;
                da.Fill(dt);
                List<Permisos> Opcion = (from row in dt.AsEnumerable()
                                         select new Permisos()
                                         {
                                             ID_Permisos = int.Parse(row["ID_Permisos"].ToString()),
                                             ID_Rol = int.Parse(row["ID_Rol"].ToString()),
                                             ID_Opcion = int.Parse(row["ID_Opcion"].ToString()),
                                             Permitidos = bool.Parse(row["Permitidos"].ToString())
                                         }).ToList();
                return Opcion;


            }

            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (SQLCon.State == ConnectionState.Open) SQLCon.Close();
            }

            return null;
        }


        public DataTable CargarCheckList(int idPerfil)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_CargarCheckList", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Comando;
                da.Fill(Tabla);
                SQLCon.Open();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return Tabla;

        }



    }
}
