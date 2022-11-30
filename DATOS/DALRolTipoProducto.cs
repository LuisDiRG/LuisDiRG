using DATOS;
using ET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class DALRolTipoProducto
    {


        public DataTable Listado(string idRolTP)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoRolTipoP", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@cTexto", SqlDbType.VarChar).Value = idRolTP; //poner aqui
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

        public string Guardar(int nOpcion, RolTipoProducto RolTipoP)
        {

            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_GuardarRolTipoP", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nOpcion", SqlDbType.Int).Value = nOpcion;
                comando.Parameters.Add("@IdRolTipoP", SqlDbType.Int).Value = RolTipoP.idRolTipoProducto;
                comando.Parameters.Add("@IdRol", SqlDbType.Int).Value = RolTipoP.idRol;
                comando.Parameters.Add("@IdTipoProducto", SqlDbType.Int).Value = RolTipoP.idTipoProducto;

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
        public string Eliminar(int idRolTipoP)
        {
            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_EliminarRol_TipoP", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@IdRol_TipoP", SqlDbType.Int).Value = idRolTipoP;
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


        public DataTable ListadoporIntAmbos(int idRol, int idTipoProducto)
        {

            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("ListadoRolTipoP_porIntAmbos", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol; //poner aqui
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

        public DataTable ListadoporRol(int idRol)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoRolTipoP_porIntaRol", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idRol", SqlDbType.VarChar).Value = idRol;
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


        public List<RolTipoProducto> ListarRolesTipoProducto()
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.RolTipoProducto.Where(e => e.Activo == true).ToList();
            }
        }

        public void Agregar(RolTipoProducto rtp)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.RolTipoProducto.Add(rtp);
                db.SaveChanges();
            }
        }

        public RolTipoProducto GetRolTipoProducto(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                //return (Departamento)db.Departamento.Find(id);
                //var departamento = db.Departamento.Find(id);

                //....metalenguaje linq ↓↓  ^^  lambda ↓↓
                return db.RolTipoProducto.Where(d => d.idRolTipoProducto == id).FirstOrDefault();
            }
        }
        public void Editar(RolTipoProducto rtp)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var d = db.RolTipoProducto.Find(rtp.idRolTipoProducto);
                db.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var rtp = db.RolTipoProducto.Find(id);
                db.RolTipoProducto.Remove(rtp);
                db.SaveChanges();
            }
        }
    }
}
