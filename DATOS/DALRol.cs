using DATOS;
using ET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
//using System.Windows.Forms;

namespace DAL
{
    public class DALRol
    {
        public DataTable Listado(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoRol", SQLCon);
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

        public string Guardar(int nOpcion, Rol rol)
        {

            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_GuardarRol", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nOpcion", SqlDbType.Int).Value = nOpcion;
                comando.Parameters.Add("@ID_Rol", SqlDbType.Int).Value = rol.ID_Rol;

                comando.Parameters.Add("@DescripcionRol", SqlDbType.VarChar).Value = rol.DescripcionRol;


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
        public string Eliminar(int IdRol)
        {
            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_EliminarRol", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@IdRol", SqlDbType.Int).Value = IdRol;
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

        public DataTable CargarComboBoxPerfil()
        {
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_CargarComboBox", SQLCon);
                Comando.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Comando;
                da.Fill(Tabla);
                SQLCon.Open();
            }

            catch (Exception ex)
            {
                //  MessageBox.Show( ex.Message);
            }

            return Tabla;
        }



        public List<Rol> ListarRol()
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Rol.Where(e => e.Activo == true).ToList();
            }
        }

        public void Agregar(Rol rol)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Rol.Add(rol);
                db.SaveChanges();
            }
        }

        public Rol GetRol(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                //return (Departamento)db.Departamento.Find(id);
                //var departamento = db.Departamento.Find(id);

                //....metalenguaje linq ↓↓  ^^  lambda ↓↓
                return db.Rol.Where(d => d.ID_Rol == id).FirstOrDefault();
            }
        }

        public void Editar(Rol rol)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var d = db.Rol.Find(rol.ID_Rol);
                d.DescripcionRol = rol.DescripcionRol;

                db.SaveChanges();
            }
        }


        public void Eliminar(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var rol = db.Rol.Find(id);
                db.Rol.Remove(rol);
                db.SaveChanges();
            }
        }
    }
}
