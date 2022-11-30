using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using System.Data.SqlClient;
using ET;
using DATOS;

namespace ProyectoRGSL.DAL
{
    public class DALBodega
    {
        public DataTable ListadoB(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoB", SQLCon);
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


        public string Guardar(int nOpcion, Bodega B)
        {

            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_GuardarB", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nOpcion", SqlDbType.Int).Value = nOpcion;
                comando.Parameters.Add("@IdBodega", SqlDbType.Int).Value = B.ID_Bodega;
                comando.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = B.DescripcionBodega;
                comando.Parameters.Add("@CubicajeMaximo", SqlDbType.Int).Value = B.CubicajeMaximo;
                comando.Parameters.Add("@CubicajeAcumulado", SqlDbType.Int).Value = B.CubicajeAcumulado;
                comando.Parameters.Add("@IDParametros", SqlDbType.Int).Value = B.ID_Parametros;

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
        public string Eliminar(int IDBodega)
        {
            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_EliminarB", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@ID", SqlDbType.Int).Value = IDBodega;
                SQLCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se logro eliminar el dato";

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
        }*/
        public DataTable ListadoBporParametro(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoBporParametro", SQLCon);
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

        public List<Bodega> ListarBodegas()
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Bodega.Where(e => e.Activo == true).ToList();
            }
        }

        public void Agregar(Bodega bodega)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Bodega.Add(bodega);
                db.SaveChanges();
            }
        }

        public Bodega GetBodega(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                //return (Departamento)db.Departamento.Find(id);
                //var departamento = db.Departamento.Find(id);
                db.Configuration.LazyLoadingEnabled = false;
                //....metalenguaje linq ↓↓  ^^  lambda ↓↓
                return db.Bodega.Where(d => d.ID_Bodega == id).FirstOrDefault();
            }
        }

        public Bodega GetBodegaDesc(string desc)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                //return (Departamento)db.Departamento.Find(id);
                //var departamento = db.Departamento.Find(id);
                db.Configuration.LazyLoadingEnabled = false;
                //....metalenguaje linq ↓↓  ^^  lambda ↓↓
                return db.Bodega.Where(d => d.DescripcionBodega == desc).FirstOrDefault();
            }
        }
        

        public void Editar(Bodega bodega)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var d = db.Bodega.Find(bodega.ID_Bodega);
                d.DescripcionBodega = bodega.DescripcionBodega;
                d.CubicajeMaximo = bodega.CubicajeMaximo;
                d.CubicajeAcumulado = bodega.CubicajeAcumulado;
               
                db.SaveChanges();
            }
        }

        public void Eliminar(int id) //este eliminar no hay que usarlo.
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var dpto = db.Bodega.Find(id);
                db.Bodega.Remove(dpto);
                db.SaveChanges();
            }
        }

    }
}
