using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;
using DAL;
using DATOS;

namespace DAL
{
    public class DALTipoProducto
    {

        public DataTable Listado(string cTexto)
        {
            SqlDataReader Resultado; // para poder leer la BD
            DataTable Tabla = new DataTable(); //la tabla que se va a poner en el data grind view
            SqlConnection SQLCon = new SqlConnection();

            try
            {

                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoTP", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@cTexto", SqlDbType.VarChar).Value = cTexto;
                SQLCon.Open(); // abrir conexion
                Resultado = Comando.ExecuteReader(); // retorna un SqlDataReader, que es para leer filas de una BD
                Tabla.Load(Resultado);
                return Tabla;
            }catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SQLCon.State == ConnectionState.Open) SQLCon.Close();// si esta abierta, se cierra
            }
        }

        public string Guardar(int nOpcion, Tipo_Producto tipoP)
        {

            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {

                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_GuardarTP", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@nOpcion", SqlDbType.Int).Value = nOpcion;
                Comando.Parameters.Add("@IdTipoProducto", SqlDbType.Int).Value = tipoP.ID_TipoProducto;
                Comando.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = tipoP.DescripcionTipoProducto;
                Comando.Parameters.Add("@stockMinimo", SqlDbType.Int).Value = tipoP.StockMinimo;
                Comando.Parameters.Add("@stockMaximo", SqlDbType.Int).Value = tipoP.StockMaximo;
                Comando.Parameters.Add("@idRackPermitido", SqlDbType.Int).Value = tipoP.ID_RackPermitido;

                SQLCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se logro registrar el dato";
            }catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SQLCon.State == ConnectionState.Open) SQLCon.Close();// si esta abierta, se cierra
            }
            return Rpta;
        }
        /*

        public string Eliminar(int idTipoProducto)
        {
            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_EliminarTP", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@ID",SqlDbType.Int).Value = idTipoProducto;
                SQLCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se logro eliminar el dato";

            }catch(Exception ex)
            {
                Rpta=ex.Message;
            }
            finally
            {
                if (SQLCon.State == ConnectionState.Open) SQLCon.Close();// si esta abierta, se cierra
            }
            return Rpta;
        }*/

        public List<Tipo_Producto> ListarTipoProducto()
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Tipo_Producto.Where(d => d.Activo == true).ToList();
            }
        }

        public void Agregar(Tipo_Producto tp)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Tipo_Producto.Add(tp);
                db.SaveChanges();
            }
        }


        public Tipo_Producto GetTipoProducto(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                //return (Departamento)db.Departamento.Find(id);
                //var departamento = db.Departamento.Find(id);
                db.Configuration.LazyLoadingEnabled = false;
                //....metalenguaje linq ↓↓  ^^  lambda ↓↓
                return db.Tipo_Producto.Where(d => d.ID_TipoProducto == id).FirstOrDefault();
            }
        }

        public void Editar(Tipo_Producto tp)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var d = db.Tipo_Producto.Find(tp.ID_TipoProducto);
                d.DescripcionTipoProducto = tp.DescripcionTipoProducto;
                d.StockMinimo = tp.StockMinimo;
                d.StockMaximo = tp.StockMaximo;
                d.ID_RackPermitido = tp.ID_RackPermitido;
                d.StockActual = tp.StockActual;
                d.noRegistrados = tp.noRegistrados;
                db.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var tp = db.Tipo_Producto.Find(id);
                tp.Activo = false;
                db.SaveChanges();
            }
        }

    }
}
