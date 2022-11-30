using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ET;
using DATOS;

namespace DAL
{
    public class DALUnidadMedida
    {
        //Propiedades

        public DataTable ListadoUM(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoUM", SQLCon);
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

        public string Guardar(int nOpcion, Unidad_Medida UM)
        {

            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_Guardar_UM", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nOpcion", SqlDbType.Int).Value = nOpcion;
                comando.Parameters.Add("@ID_UnidadMedida", SqlDbType.Int).Value = UM.ID_UnidadMedida;
                comando.Parameters.Add("@DescripcionUnidadMedida", SqlDbType.VarChar).Value = UM.DescripcionUnidadMedida;
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

        public string EliminaUM(int idUnidadMedida)
        {
            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_Eliminar_UM", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@ID_UnidadMedida", SqlDbType.Int).Value = idUnidadMedida;
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


        //NUEVOS METODOS
        public List<Unidad_Medida> ListarUnidadMedida()
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Unidad_Medida.Where(d => d.Activo == true).ToList();
            }
        }

        public void Agregar(Unidad_Medida dpto)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Unidad_Medida.Add(dpto);
                db.SaveChanges();
            }
        }

        public Unidad_Medida GetUnidadMedida(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                //return (Departamento)db.Departamento.Find(id);
                //var departamento = db.Departamento.Find(id);

                //....metalenguaje linq ↓↓  ^^  lambda ↓↓
                return db.Unidad_Medida.Where(d => d.ID_UnidadMedida == id).FirstOrDefault();

            }
        }

        public void Editar(Unidad_Medida um)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var d = db.Unidad_Medida.Find(um.ID_UnidadMedida);
                d.DescripcionUnidadMedida = um.DescripcionUnidadMedida;
                db.SaveChanges();
            }
        }

        public void Eliminar(int id)//Esta se la podemos dejar porque lo deja en false, y no hay que cambiar otros datos
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var um = db.Unidad_Medida.Find(id);
              
                um.Activo = false;
            
                db.SaveChanges();
            }
        }
    }
}