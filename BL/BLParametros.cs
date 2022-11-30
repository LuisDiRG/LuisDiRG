using DAL;
using ET;
using System;
using System.Collections.Generic;
using System.Data;

namespace BL
{
    public class BLParametros
    {
        public static DataTable Listado(string cTexto)
        {
            DALParametros Datos = new DALParametros();
            return Datos.ListadoPA(cTexto);
        }

        public static string Guardar(int nOpcion, Parametro parametros)
        {
            DALParametros Datos = new DALParametros();
            return Datos.Guardar(nOpcion, parametros);
        }
        /*
        public static string Eliminar(int idparametros)
        {
            DALParametros Datos = new DALParametros();
            return Datos.EliminaUM(idparametros);
        }*/

        public static Parametro CargarParametros(int ID_Parametros)
        {
            DataTable dataTable = new DataTable();
            dataTable = DALParametros.CargarParametros(ID_Parametros);
            Parametro ep = new Parametro();

            ep.ID_Parametro = Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
            ep.CubicajeMaximo = Convert.ToDecimal(dataTable.Rows[0].ItemArray[1]);
            ep.HorarioInicio = TimeSpan.Parse(Convert.ToString(dataTable.Rows[0].ItemArray[2]));
            ep.HorarioSalida = TimeSpan.Parse(Convert.ToString(dataTable.Rows[0].ItemArray[3]));
            //ep.HorarioInicio = (Convert.ToDateTime(dataTable.Rows[0].ItemArray[2]));
            //ep.HorarioSalida = Convert.ToDateTime(Convert.ToString(dataTable.Rows[0].ItemArray[3]));
            ep.Activo = Convert.ToBoolean(dataTable.Rows[0].ItemArray[4]);

            return ep;
        }
        
        
        public static bool VerificaInicio(string fechaActual, int IDParametros)
        {
            var horaMinIngreso = TimeSpan.Parse(fechaActual);

            var pa = BLParametros.CargarParametros(IDParametros); //tengo el parametro, para el horarioInicio y Final  

            bool pasa = false;
            //esto hay que revisarlo
            //ocupamos:
            //que la fecha Ingreso este en medio del horarioInicio y Salida
            //fecha de ingreso => horarioInicio
            //fecha de Ingreso =< horarioSalida
            if (((TimeSpan.Compare(horaMinIngreso, pa.HorarioInicio) >= 0) && (TimeSpan.Compare(horaMinIngreso, pa.HorarioSalida) <= 0)) )
            {
                pasa = true;
            }
            return pasa;
        }
        
        

        private static DALParametros obj = new DALParametros();

        public static List<Parametro> ListarParametros()
        {
            return obj.ListarParametro();
        }

        public static void Agregar(Parametro parametro)
        {
            obj.Agregar(parametro);
        }

        public static Parametro GetParametro(int id)
        {
            return obj.GetParametro(id);
        }

        public static void Editar(Parametro parametro)
        {
            obj.Editar(parametro);
        }

        public static void Eliminar(int id)
        {
            obj.Eliminar(id);
        }

    }
}
