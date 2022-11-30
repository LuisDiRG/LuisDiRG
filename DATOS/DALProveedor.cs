using DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ET;
using DAL;

namespace ProyectoRGSL.DAL
{
     public class DALProveedor
    {
        //Propiedades

        public DataTable ListadoPV(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoPV", SQLCon);
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

        public string Guardar(int nOpcion, Proveedor PV)
        {

            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_GuardarPV", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nOpcion", SqlDbType.Int).Value = nOpcion;
                comando.Parameters.Add("@IdProveedor", SqlDbType.Int).Value = PV.ID_Proveedor;
                comando.Parameters.Add("@cDescripcionpv", SqlDbType.VarChar).Value = PV.DescripcionProveedor;
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

        public string EliminaPV(int idProveedor)
        {
            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_EliminarPV", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@IdProveedor", SqlDbType.Int).Value = idProveedor;
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

        public List<Proveedor> ListarProveedor()
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Proveedor.Where(e => e.Activo==true).ToList();
            }
        }

        public void Agregar(Proveedor proveedor)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Proveedor.Add(proveedor);
                db.SaveChanges();
            }
        }

        public Proveedor GetProveedor(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                //return (Departamento)db.Departamento.Find(id);
                //var departamento = db.Departamento.Find(id);

                //....metalenguaje linq ↓↓  ^^  lambda ↓↓
                return db.Proveedor.Where(d => d.ID_Proveedor == id).FirstOrDefault();
            }
        }

        public void Editar(Proveedor proveedor)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var d = db.Proveedor.Find(proveedor.ID_Proveedor);
                d.DescripcionProveedor = proveedor.DescripcionProveedor;
                db.SaveChanges();
            }
        }

        public void Eliminar(int id) //Este si sirve, no afecta los datos de otros
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var prov = db.Proveedor.Find(id);
                prov.Activo = false;
                db.SaveChanges();
            }
        }

    }
}
