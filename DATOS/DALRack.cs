using DATOS;
using ET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace DAL
{
    public class DALRack
    {
        //Propiedades

        public DataTable ListadoR(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoR", SQLCon);
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

        public string GuardarR(int nOpcion, Rack RK)
        {

            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_GuardarR", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nOpcion", SqlDbType.Int).Value = nOpcion;
                comando.Parameters.Add("@ID_Rack", SqlDbType.Int).Value = RK.ID_Rack;
                comando.Parameters.Add("@DescripcionRack", SqlDbType.VarChar).Value = RK.DescripcionRack;
                comando.Parameters.Add("@CubicajeAcumulado", SqlDbType.Float).Value = RK.CubicajeAcumulado;
                comando.Parameters.Add("@CubicajeMaximo", SqlDbType.Float).Value = RK.CubicajeMaximo;
                comando.Parameters.Add("@ID_Bodega", SqlDbType.Int).Value = RK.ID_Bodega;
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

        public string EliminaR(int idRack, int IdBodega, decimal CubicajeMaximo)
        {
            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_EliminarR", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Parametros", SqlDbType.Int).Value = idRack;
                comando.Parameters.Add("@ID_Bodega", SqlDbType.Int).Value = IdBodega;
                comando.Parameters.Add("@CubicajeMaximo", SqlDbType.Decimal).Value = CubicajeMaximo;
                SQLCon.Open();
                Rpta = comando.ExecuteNonQuery() >= 1 ? "OK" : "No se logró eliminar el dato";
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


        public DataTable ListadoRporBodega(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoRporBodega", SQLCon);
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

        public List<Rack> ListarRack()
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                return db.Rack.Where(d => d.Activo == true).ToList();
            }
        }

        public void Agregar(Rack rack)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Rack.Add(rack);
                db.SaveChanges();
            }
        }

        public Rack GetRack(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                //....metalenguaje linq ↓↓  ^^  lambda ↓↓
                return db.Rack.Where(d => d.ID_Rack == id).FirstOrDefault();
            }
        }

        public Rack GetRackDesc(string desc)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                //....metalenguaje linq ↓↓  ^^  lambda ↓↓
                return db.Rack.Where(d => d.DescripcionRack == desc).FirstOrDefault();
            }
        }

        public void Editar(Rack rack)//no me sirve porque cambio los datos de todo
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var d = db.Rack.Find(rack.ID_Rack);
                d.DescripcionRack = rack.DescripcionRack;
                d.CubicajeAcumulado = rack.CubicajeAcumulado;
                d.CubicajeMaximo = rack.CubicajeMaximo;
                d.ID_Bodega = rack.ID_Bodega;

                db.SaveChanges();
            }
        }
        /*
        public void Eliminar(int id)//ni este
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var rack = db.Rack.Find(id);
                rack.Activo = false;
                db.SaveChanges();
            }
        }*/
    }
}