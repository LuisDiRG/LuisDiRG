using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ET;
using DATOS;
using System.Web.UI.WebControls;

namespace DAL
{
    public class DALParametros
    {
        //Propiedades

        public DataTable ListadoPA(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoPA", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@cTexto", SqlDbType.VarChar).Value = cTexto;
                SQLCon.Open();
                Resultado = Comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;
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

        public string Guardar(int nOpcion, Parametro PA)
        {

            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_GuardarPA", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nOpcion", SqlDbType.Int).Value = nOpcion;
                comando.Parameters.Add("@ID_Parametros", SqlDbType.Int).Value = PA.ID_Parametro;
                comando.Parameters.Add("@CubicajeMaximo", SqlDbType.Decimal).Value = PA.CubicajeMaximo;
                comando.Parameters.Add("@HorarioInicio", SqlDbType.DateTime).Value = PA.HorarioInicio;
                comando.Parameters.Add("@HorarioSalida", SqlDbType.DateTime).Value = PA.HorarioSalida;
                SQLCon.Open();
                Rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se logro registrar el dato";

            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SQLCon.State == ConnectionState.Open) SQLCon.Close();
            }
            return Rpta;
        }

        public string EliminaUM(int idParametros)
        {
            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_EliminarPA", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Parametros", SqlDbType.Int).Value = idParametros;
                SQLCon.Open();
                Rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se logró eliminar el dato";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SQLCon.State == ConnectionState.Open) SQLCon.Close();
            }

            return Rpta;

        }

        public static DataTable CargarParametros(int IDParametros)
        {
            DataTable CargarParametros = new DataTable();

            SqlConnection SQLCon = new SqlConnection();


            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_CargarParametros", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@ID_Parametros", SqlDbType.Int).Value = IDParametros;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = comando;
                da.Fill(CargarParametros);
                SQLCon.Open();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SQLCon.State == ConnectionState.Open) SQLCon.Close();
            }

            return CargarParametros;
        }

        public List<Parametro> ListarParametro()
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Parametro.Where(e => e.Activo == true).ToList();
            }
        }

        public void Agregar(Parametro parametro)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Parametro.Add(parametro);
                db.SaveChanges();
            }
        }

        public Parametro GetParametro(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                //return (Departamento)db.Departamento.Find(id);
                //var departamento = db.Departamento.Find(id);
                db.Configuration.LazyLoadingEnabled = false;

                //....metalenguaje linq ↓↓  ^^  lambda ↓↓
                return db.Parametro.Where(d => d.ID_Parametro == id).FirstOrDefault();
            }
        }

        public void Editar(Parametro parametro)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var d = db.Parametro.Find(parametro.ID_Parametro);
                d.DescripcionParametro = parametro.DescripcionParametro;
                d.CubicajeMaximo = parametro.CubicajeMaximo;
                d.HorarioInicio = parametro.HorarioInicio;
                d.HorarioSalida = parametro.HorarioSalida;

                db.SaveChanges();
            }
        }

        public void Eliminar(int id) //este tmb puede servir porque no hay que cambiar datos de otras entidades
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var d = db.Parametro.Find(id);
                d.Activo = false;
               
                db.SaveChanges();
            }
        }
    }
}
