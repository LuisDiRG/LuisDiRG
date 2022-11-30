using DATOS;
using ET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class DALUbicacion
    {
        public DataTable Listado(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoU", SQLCon);
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

        public DataTable ListadoporAmbos(string idBodega, string idRack)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoUporAmbos", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idBodega", SqlDbType.VarChar).Value = idBodega;
                Comando.Parameters.Add("@idRack", SqlDbType.VarChar).Value = idRack;
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

        public DataTable ListadoDesOcupados(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoUDesOcupados", SQLCon);
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


        public string Guardar(int nOpcion, Ubicacion ubicacion)
        {

            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_GuardarU", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nOpcion", SqlDbType.Int).Value = nOpcion;
                comando.Parameters.Add("@IdUbicacion", SqlDbType.Int).Value = ubicacion.ID_Ubicacion;
                comando.Parameters.Add("@IdRack", SqlDbType.Int).Value = ubicacion.ID_Rack;
                comando.Parameters.Add("@IdBodega", SqlDbType.Int).Value = ubicacion.ID_Bodega;

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
        public string Eliminar(int idUbicacion)
        {
            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_EliminarU", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@ID", SqlDbType.Int).Value = idUbicacion;
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

        public List<Ubicacion> ListarUbicacion()
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Ubicacion.Where(e => e.Activo == true).ToList();
            }
        }

        public void Agregar(Ubicacion dpto)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Ubicacion.Add(dpto);
                db.SaveChanges();
            }
        }

        public Ubicacion GetUbicacion(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                //return (Departamento)db.Departamento.Find(id);
                //var departamento = db.Departamento.Find(id);

                //....metalenguaje linq ↓↓  ^^  lambda ↓↓
                return db.Ubicacion.Where(d => d.ID_Ubicacion == id).FirstOrDefault();
            }
        }

        public void Editar(Ubicacion ubicacion)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var d = db.Ubicacion.Find(ubicacion.ID_Ubicacion);
                d.ID_Rack = ubicacion.ID_Rack;
                d.ID_Bodega = ubicacion.ID_Bodega;
                db.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var ubicacion = db.Ubicacion.Find(id);
                db.Ubicacion.Remove(ubicacion);
                db.SaveChanges();
            }
        }



    }
}
