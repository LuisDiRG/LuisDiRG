using DATOS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;
using DAL;

namespace DAL
{
    public class DALProducto
    {

        public DataTable Listado(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoP", SQLCon);
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

        public string Guardar(int nOpcion, Producto producto)
        {

            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_GuardarP", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nOpcion", SqlDbType.Int).Value = nOpcion;
                comando.Parameters.Add("@descripcionProducto", SqlDbType.VarChar).Value = producto.DescripcionProducto;
                comando.Parameters.Add("@idProducto", SqlDbType.Int).Value = producto.ID_Producto;
                comando.Parameters.Add("@diferenciaMesesVencimiento", SqlDbType.Int).Value = producto.DiferenciaMesesVencimiento;
                comando.Parameters.Add("@cubicaje", SqlDbType.Decimal).Value = producto.Cubicaje;
                comando.Parameters.Add("@empaque", SqlDbType.Decimal).Value = producto.Empaque;
                comando.Parameters.Add("@costoBruto", SqlDbType.Decimal).Value = producto.CostoBruto;
                comando.Parameters.Add("@descuento", SqlDbType.Decimal).Value = producto.Descuento;
                comando.Parameters.Add("@IVA", SqlDbType.Decimal).Value = producto.IVA;
                comando.Parameters.Add("@costoNeto", SqlDbType.Decimal).Value = producto.CostoNeto;

                
                comando.Parameters.Add("@idTipoProducto",SqlDbType.Int).Value = producto.ID_TipoProducto;
              
                comando.Parameters.Add("@idUnidadMedida", SqlDbType.Int).Value = producto.ID_UnidadMedida;

                if (producto.ID_Proveedor == 0 && producto.ID_Ubicacion == 0)
                {
                    comando.Parameters.Add("@idUbicacion", SqlDbType.Int).Value = null;
                    comando.Parameters.Add("@idProveedor", SqlDbType.Int).Value = null;
                }
                else
                {
                    comando.Parameters.Add("@idUbicacion", SqlDbType.Int).Value = producto.ID_Ubicacion;
                    comando.Parameters.Add("@idProveedor", SqlDbType.Int).Value = producto.ID_Proveedor;

                }


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

        public string Eliminar(int idProducto, int idTipoP, int idUbica)
        {
            SqlConnection SQLCon = new SqlConnection();
            string Rpta = "";

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_EliminarP", SQLCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idProducto", SqlDbType.Int).Value = idProducto;
                comando.Parameters.Add("@idTipoProducto", SqlDbType.Int).Value = idTipoP;
                comando.Parameters.Add("@idUbicacion", SqlDbType.Int).Value = idUbica;
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


        public DataTable Listadocomp(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoPcomp", SQLCon);
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

        public DataTable ListadoPporUbi(string cTexto)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SQLCon = new SqlConnection();

            try
            {
                SQLCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("USP_ListadoPporUbi", SQLCon);
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

        public List<Producto> ListarProducto()
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Producto.Where(p => p.Activo ==true).ToList();
            }
        }


        public void Agregar(Producto pro)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Producto.Add(pro);
                db.SaveChanges();
            }
        }

        public Producto GetProducto(int id)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                //return (Departamento)db.Departamento.Find(id);
                //var departamento = db.Departamento.Find(id);

                //....metalenguaje linq ↓↓  ^^  lambda ↓↓
                return db.Producto.Where(d => d.ID_Producto == id).FirstOrDefault();
            }
        }

        public void Editar(Producto pro)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var d = db.Producto.Find(pro.ID_Producto);
                d.DescripcionProducto = pro.DescripcionProducto;
                d.Fecha_Alta = pro.Fecha_Alta;
                d.DiferenciaMesesVencimiento = pro.DiferenciaMesesVencimiento;
                d.Cubicaje = pro.Cubicaje;
                d.Empaque = pro.Empaque;
                d.CostoBruto = pro.CostoBruto;
                d.Descuento = pro.Descuento;
                d.IVA = pro.IVA;
                d.CostoNeto = pro.CostoNeto;
                d.ID_Ubicacion = pro.ID_Ubicacion;
                d.ID_TipoProducto = pro.ID_TipoProducto;
                d.ID_Proveedor = pro.ID_Proveedor;
                d.ID_UnidadMedida = pro.ID_UnidadMedida;
              
                db.SaveChanges();
            }
        }

        public void Eliminar(int id) //este no hay que usarlo
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                var pro = db.Producto.Find(id);
                db.Producto.Remove(pro);
                db.SaveChanges();
            }
        }

        public static List<Producto> ListaProductosporTP(int idTipoProducto)
        {
            using (var db = new RGSL_ProyectoEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Producto.Where(p => p.Activo == true && p.ID_TipoProducto == idTipoProducto && p.ID_Ubicacion == null &&p.ID_Proveedor == null).ToList();
            }
        }

    }
}
