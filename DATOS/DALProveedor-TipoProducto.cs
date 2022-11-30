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
    public class DALProveedor_TipoProducto
    {

        public DataTable Listado(string IdProvTP)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoProvTipoP", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@cTexto", SqlDbType.VarChar).Value = IdProvTP; //poner aqui
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

        public string Guardar(int nOpcion, ProveedorTipoProducto ProvTP)
        {

            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_GuardarProvTipoP", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nOpcion", SqlDbType.Int).Value = nOpcion;
                comando.Parameters.Add("@IdProvTipoP", SqlDbType.Int).Value = ProvTP.idProveedorTipoProducto;
                comando.Parameters.Add("@IdProveedor", SqlDbType.Int).Value = ProvTP.idProveedor;
                comando.Parameters.Add("@IdTipoProducto", SqlDbType.Int).Value = ProvTP.idTipoProducto;

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
        /*
        public string Eliminar(int idProveedor_TipoP)
        {
            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_EliminarProv_TipoP", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@IdProv_TipoP", SqlDbType.Int).Value = idProveedor_TipoP;
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

        }*/


        public DataTable ListadoporIntAmbos(int idProveedor, int idTipoProducto)
        {

            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("ListadoProvTipoP_porIntAmbos", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor; //poner aqui
                Comando.Parameters.Add("@idTipoProducto", SqlDbType.Int).Value = idTipoProducto;
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

        public DataTable ListadoporTP(int idTipoProducto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoProvTipoP_porIntaTipoP", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idTipoProducto", SqlDbType.VarChar).Value = idTipoProducto;
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

        public List<ProveedorTipoProducto> ListarProveedorTipoProducto()
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.ProveedorTipoProducto.Where(e => e.Activo == true).ToList();
            }
        }

        public void Agregar(ProveedorTipoProducto provTP)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.ProveedorTipoProducto.Add(provTP);
                db.SaveChanges();
            }
        }

        public ProveedorTipoProducto GetProveedorTipoProducto(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                //return (Departamento)db.Departamento.Find(id);
                //var departamento = db.Departamento.Find(id);

                //....metalenguaje linq ↓↓  ^^  lambda ↓↓
                return db.ProveedorTipoProducto.Where(d => d.idProveedorTipoProducto == id).FirstOrDefault();
            }
        }

        public void Editar(ProveedorTipoProducto provTP)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var d = db.ProveedorTipoProducto.Find(provTP.idProveedorTipoProducto);
                d.idProveedor = provTP.idProveedor;
                d.idTipoProducto = provTP.idTipoProducto;
                db.SaveChanges();
            }
        }

        public void Eliminar(int id)//mejor usar el otro
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var ptp = db.ProveedorTipoProducto.Find(id);
                ptp.Activo = false;
                db.SaveChanges();
            }
        }
    }
}
