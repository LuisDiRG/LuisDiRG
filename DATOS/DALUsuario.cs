using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;
using ET;
using DATOS;

namespace DAL
{
    public class DALUsuario
    {

        public DataTable Listado(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoUsuario", SQLCon);
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
        public DataTable ListadoUsuarioPorIDUsuario(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoUsuarioPorIDUsuario", SQLCon);
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


        public string Guardar(int nOpcion, Usuario usuario)
        {

            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_GuardarUsuario", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nOpcion", SqlDbType.Int).Value = nOpcion;
                comando.Parameters.Add("@ID_Usuarios", SqlDbType.Int).Value = usuario.ID_Usuario;
                comando.Parameters.Add("@PrimerNombre", SqlDbType.VarChar).Value = usuario.PrimerNombre;
                comando.Parameters.Add("@SegundoNombre", SqlDbType.VarChar).Value = usuario.SegundoNombre;
                comando.Parameters.Add("@PrimerApellido", SqlDbType.VarChar).Value = usuario.PrimerApellido;
                comando.Parameters.Add("@SegundoApellido", SqlDbType.VarChar).Value = usuario.SegundoApellido;
                comando.Parameters.Add("@NombreUsuario", SqlDbType.VarChar).Value = usuario.NombreUsuario;
                comando.Parameters.Add("@Contraseña", SqlDbType.VarChar).Value = usuario.Contraseña;
                comando.Parameters.Add("@ID_Rol", SqlDbType.Int).Value = usuario.ID_Rol;
                comando.Parameters.Add("@ID_Cedula", SqlDbType.Int).Value = usuario.ID_Cedula;


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
        public string Eliminar(int idUsuario)
        {
            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_EliminarUsuario", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@ID_Usuarios", SqlDbType.Int).Value = idUsuario;
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


        public List<Usuario> ListarUsuario()
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Usuario.Where(e => e.Activo == true).ToList();
            }
        }

        public void Agregar(Usuario usu)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Usuario.Add(usu);
                db.SaveChanges();
            }
        }

        public Usuario GetUsuario(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                //return (Departamento)db.Departamento.Find(id);
                //var departamento = db.Departamento.Find(id);

                //....metalenguaje linq ↓↓  ^^  lambda ↓↓
                return db.Usuario.Where(d => d.ID_Usuario == id).FirstOrDefault();
            }
        }

        public void Editar(Usuario usua)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var d = db.Usuario.Find(usua.ID_Usuario);
                d.PrimerNombre = usua.PrimerNombre;
                d.SegundoNombre = usua.SegundoNombre;
                d.NombreUsuario = usua.NombreUsuario;
                d.Contraseña = usua.Contraseña;
                d.ID_Rol = usua.ID_Rol;
                d.ID_Cedula = usua.ID_Cedula;
                db.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var usu = db.Usuario.Find(id);
                usu.Activo = false;
                db.SaveChanges();
            }
        }
    }
}
