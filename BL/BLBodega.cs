using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using System.Data.SqlClient;
using ET;
using ProyectoRGSL.DAL;

namespace BL
{
     public class BLBodega
    {
        public static DataTable Listado(string cTexto)
        {
            DALBodega Datos = new DALBodega();
            return Datos.ListadoB(cTexto);
        }


        public static string Guardar(int nOpcion, Bodega bodega)
        {
            DALBodega Datos = new DALBodega();
            return Datos.Guardar(nOpcion, bodega);
        }
        /*
        public static string Eliminar(int IDBodega)
        {
            DALBodega Datos = new DALBodega();
            return Datos.Eliminar(IDBodega);
        }
        */

        public static DataTable ListadoBporParametro(string cTexto)
        {
            DALBodega Datos = new DALBodega();
            return Datos.ListadoBporParametro(cTexto);
        }


        public bool validarDescripciones(string descripcion)
        {
            bool pasa = true;

            DataTable dt = Listado(descripcion);
            if (dt.Rows.Count > 0)
            {
                if (descripcion == (string)dt.Rows[0]["DescripcionBodega"])
                {
                    pasa = false;
                }
                else
                {
                    pasa = true;
                }
            }
            else
            {
                pasa = true;
            }

            return pasa;
        }
        public static bool CubicajeMaximoSeamayor(decimal Maximo, decimal Acumulado)
        {
            bool pasa = true;

            if (Acumulado < Maximo)
            {
                pasa = true;
            }
            else
            {
                pasa = false;
            }
            return pasa;
        }
        public static bool VerificarCubicajeMaximoConParametros(decimal cubicajeMaximoBodega, string descripcionParametros)
        {
            bool funci1 = false;
            DataTable Tablilla = BLParametros.Listado(descripcionParametros);
            Console.WriteLine(Tablilla.Rows.Count);

            decimal cubicajeParametros = (decimal)Tablilla.Rows[0]["CubicajeMaximo"];

            if (Tablilla.Rows.Count > 0)
            {
                if (cubicajeMaximoBodega <= cubicajeParametros)
                {
                    funci1 = true;
                }
                else
                {
                    funci1 = false;
                }
            }
            else { funci1 = true; }

            return funci1;
        }



        public static bool ValidarSumaBodegas(decimal cubicajeMaximoBodega, string descripcionParametros,int opcion, int idBodega)
        {
            bool funci = false;

            DataTable Tablona2 = BLParametros.Listado(descripcionParametros);//
            int idParametros = (int)Tablona2.Rows[0]["ID_Parametro"];
            DataTable Tablona = BLBodega.ListadoBporParametro(Convert.ToString(idParametros));//

            if (opcion == 1)
            {
                decimal totalCubicajeMaximoBodegas = 0;
                int indice = 0;
                if (Tablona.Rows.Count > 0)
                {
                    foreach (var e in Tablona.Rows)
                    {
                        totalCubicajeMaximoBodegas = totalCubicajeMaximoBodegas + (decimal)Tablona.Rows[indice]["CubicajeMaximo"];
                        indice++;
                    }

                    totalCubicajeMaximoBodegas = totalCubicajeMaximoBodegas + cubicajeMaximoBodega;
                    decimal CubicajeParametros = (decimal)Tablona2.Rows[0]["CubicajeMaximo"];
                    if (Tablona.Rows.Count > 0 && Tablona2.Rows.Count > 0)
                    {
                        if (totalCubicajeMaximoBodegas <= CubicajeParametros) //significa que esta bien
                        {
                            funci = true;
                        }
                        else
                        {
                            funci = false;
                        }
                    }
                    else
                    {
                        funci = false;

                    }
                }
                else
                {
                    funci = true;
                }
            }
            else
            {
                decimal totalCubicajeMaximoBodegas = 0;
                int indice = 0;
                if (Tablona.Rows.Count > 0)
                {
                    foreach (var e in Tablona.Rows)
                    {
                        totalCubicajeMaximoBodegas = totalCubicajeMaximoBodegas + (decimal)Tablona.Rows[indice]["CubicajeMaximo"];
                        indice++;
                    }
                    var bodegaSinActualizar = BLBodega.GetBodega(idBodega);
                    totalCubicajeMaximoBodegas = totalCubicajeMaximoBodegas - bodegaSinActualizar.CubicajeMaximo;
                    totalCubicajeMaximoBodegas = totalCubicajeMaximoBodegas + cubicajeMaximoBodega;
                    decimal CubicajeParametros = (decimal)Tablona2.Rows[0]["CubicajeMaximo"];
                    if (Tablona.Rows.Count > 0 && Tablona2.Rows.Count > 0)
                    {
                        if (totalCubicajeMaximoBodegas <= CubicajeParametros) //significa que esta bien
                        {
                            funci = true;
                        }
                        else
                        {
                            funci = false;
                        }
                    }
                    else
                    {
                        funci = false;

                    }
                }
                else
                {
                    funci = true;
                }
            }
            

            return funci;
        }


        private static DALBodega obj = new DALBodega();

        public static List<Bodega> ListarBodega()
        {
            return obj.ListarBodegas();
        }

        /*public static void Agregar(Bodega bd)
        {
            obj.Agregar(bd);
        }*/
        public static int Agregar(int opcion, Bodega bg) //idBodegaAnterior es aquella bodega que se no se le ha descontado el cub maximo del rack
        {
            int valor;

            var parametro = BLParametros.GetParametro(bg.ID_Parametros);

           
                if (VerificarCubicajeMaximoConParametros(bg.CubicajeMaximo, parametro.DescripcionParametro) == true)// 
                {             
                    if (ValidarSumaBodegas(bg.CubicajeMaximo, parametro.DescripcionParametro, opcion, bg.ID_Bodega) == true)//
                    {
                        if (opcion == 1) //esto es que lo crea, entonces no pasa nada, las validaciones que hizo ya bastan
                        {
                            obj.Guardar(opcion, bg);
                        }
                        else //es que va a editar, y falta descontarle el cubicaje maximo del rakc a su bodega anterior
                        {
                            var bodegaAunNoActualizada = BLBodega.GetBodega(bg.ID_Bodega); // aqui tengo la bodega, pero no lo he actualizado aun, entonces me va a traer el id de bodega anterior
                            var parametro2 = BLParametros.GetParametro(bodegaAunNoActualizada.ID_Parametros);//tengo el parametro, solo tengo que descontarle el cub.max del rack
                            parametro.CubicajeMaximo -= bodegaAunNoActualizada.CubicajeMaximo; //tengo que quitarle el cubicaje acumulado del rack anteiror.
                            
                            BLParametros.Guardar(2, parametro2);//
                            /*
                            var bodegaNuevaDeRack = BLBodega.GetBodega(rack.ID_Bodega);//como en el sp no le he agregado el cubicaje max a la nueva bodega, aqui se lo agrego
                            bodegaNuevaDeRack.CubicajeAcumulado += rack.CubicajeMaximo;
                            BLBodega.Guardar(2, bodegaNuevaDeRack);
                            */
                            obj.Guardar(opcion, bg);
                        }


                        valor = 1; //"Se creo la bodega exitosamente "
                    }
                    else
                    {
                        valor = 2;//"La suma de las bodegas seleccionada es superior al cubicaje maximo del parametro ";
                    }

                }
                else
                {
                    valor = 3; //"El cubicaje maximo de la bodega es superior al cubicaje maximo del parametro";
                }
          


           

            return valor;
        }

        public static Bodega GetBodega(int id)
        {
            return obj.GetBodega(id);
        }

        public static Bodega GetBodegaDesc(string desc)
        {
            return obj.GetBodegaDesc(desc);
        }


        public static void Editar(Bodega bd)
        {
            obj.Editar(bd);
        }

        public static void Eliminar(int id)
        {
            obj.Eliminar(id);
        }

         public static List<BodegaDTO> ListaConvertirADTO()
         {
             var bodega = ListarBodega();
             List<BodegaDTO> listaBodegaDTO = new List<BodegaDTO>();

             foreach (var item in bodega) //aqui voy a convertir todo
             {
                 BodegaDTO bgDTO = new BodegaDTO(); //creo el tpDTO
                 //asigno todo igual excepto el idRack
                 bgDTO.ID_Bodega = item.ID_Bodega;
                 bgDTO.DescripcionBodega = item.DescripcionBodega;
                 bgDTO.CubicajeAcumulado = item.CubicajeAcumulado;
                 bgDTO.CubicajeMaximo = item.CubicajeMaximo;
                 bgDTO.Activo = item.Activo; 
                


                 //asigno la desc del parametro
                 var parametro = BLParametros.GetParametro(item.ID_Parametros);
                 bgDTO.DescripcionParametros = parametro.DescripcionParametro; 

                 listaBodegaDTO.Add(bgDTO);
             }

             return listaBodegaDTO;
         }

         public static BodegaDTO UnoConvertirADTO(Bodega bodega)
         {
             BodegaDTO bgDTO = new BodegaDTO(); //creo el tpDTO
                                            //asigno todo igual excepto el idParametro
             bgDTO.ID_Bodega = bodega.ID_Bodega;
             bgDTO.DescripcionBodega = bodega.DescripcionBodega;
             bgDTO.CubicajeAcumulado = bodega.CubicajeAcumulado;
             bgDTO.CubicajeMaximo = bodega.CubicajeMaximo;
             bgDTO.Activo = bodega.Activo;


             //asigno la desc de la bodega
             var parametro = BLParametros.GetParametro(bodega.ID_Parametros);
             bgDTO.DescripcionParametros = parametro.DescripcionParametro; 

             return bgDTO;
         }
        

    }
}
