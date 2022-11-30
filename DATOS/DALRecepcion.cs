using DATOS;
using System;
using System.Data;
using System.Data.SqlClient;
using ET;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class DALRecepcion
    {
        public DataTable ListadoRe(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_Listado_Re", SQLCon);
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



        public DataTable Listado_detalle_Re(int idEncabezadoRecepcion)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                //db.Configuration.LazyLoadingEnabled = false;
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_Listado_Detalle_Re", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idRecepcionEncabezado", SqlDbType.Int).Value = idEncabezadoRecepcion;
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


        public string GuardarRe(Encabezado_Recepcion Re, DataTable tablaR, int cantidadProd)
        {

            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_GuardarRe", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                //comando.Parameters.Add("@nOpcion", SqlDbType.Int).Value = nOpcion;
                comando.Parameters.Add("@ID_EncabezadoRecepcion", SqlDbType.Int).Value = Re.ID_EncabezadoRecepcion;
                comando.Parameters.Add("@DescripcionEncabezadoRecepcion", SqlDbType.VarChar).Value = Re.DescripcionEncabezadoRecepcion;
                comando.Parameters.Add("@FechaIngreso", SqlDbType.DateTime).Value = Re.FechaIngreso;
                comando.Parameters.Add("@ID_Usuario", SqlDbType.Int).Value = Re.ID_Usuario;
                comando.Parameters.Add("@ID_Bodega", SqlDbType.Int).Value = Re.ID_Bodega;
                comando.Parameters.Add("@ID_Proveedor", SqlDbType.Int).Value = Re.ID_Proveedor;
                comando.Parameters.Add("@fechaVencimiento", SqlDbType.DateTime).Value = Re.fechaVencimiento;
                comando.Parameters.Add("@ID_TipoProducto", SqlDbType.Int).Value = Re.ID_TipoProducto;
                comando.Parameters.Add("@ID_Parametro", SqlDbType.Int).Value = Re.ID_Parametro;
                comando.Parameters.Add("@tDetalle", SqlDbType.Structured).Value = tablaR;
                comando.Parameters.Add("@CantidadARecepcionar", SqlDbType.Int).Value = cantidadProd;

                //
                SQLCon.Open();
                Rpta = comando.ExecuteNonQuery() >= 1 ? "OK" : "No se logro registrar el dato";

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
        public string Eliminar(int IDRecepcionEnc, int cantidadProd)
        {
            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_EliminarRe", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idEncabezadoRecepcion", SqlDbType.Int).Value = IDRecepcionEnc;
                Comando.Parameters.Add("@cantidadProd", SqlDbType.Int).Value = cantidadProd;
                SQLCon.Open();
                Rpta = Comando.ExecuteNonQuery() >= 1 ? "OK" : "No se logro eliminar el dato";

            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SQLCon.State == ConnectionState.Open) SQLCon.Close();// si esta abierta, se cierra
            }
            return Rpta;
        }
        public DataTable ListadoPrRe(string cTexto, int opcion)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoPcomp_Re", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@cTexto", SqlDbType.VarChar).Value = cTexto;
                Comando.Parameters.Add("Opcion", SqlDbType.Int).Value = opcion;
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

        //LINQ

        public List<Encabezado_Recepcion> ListarEncabezadoRecepcion()
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                return db.Encabezado_Recepcion.Where(e => e.Activo == true).ToList();
            }
        }

        public Encabezado_Recepcion GetEncabezadoR(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                //return (Departamento)db.Departamento.Find(id);
                //var departamento = db.Departamento.Find(id);
                db.Configuration.LazyLoadingEnabled = false;
                //....metalenguaje linq ↓↓  ^^  lambda ↓↓
                return db.Encabezado_Recepcion.Where(d => d.ID_EncabezadoRecepcion == id).FirstOrDefault();
            }
        }

       

    }
}
