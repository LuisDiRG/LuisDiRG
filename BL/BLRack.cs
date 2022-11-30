using DAL;
using ET;
using System;
using System.Collections.Generic;
using System.Data;

namespace BL
{
    public class BLRack
    {
        public static DataTable Listado(string cTexto)
        {
            DALRack Datos = new DALRack();
            return Datos.ListadoR(cTexto);
        }

        public static string Guardar(int nOpcion, Rack rack)
        {
            DALRack Datos = new DALRack();
            return Datos.GuardarR(nOpcion, rack);
        }
        /*
        public static string Eliminar(int idRack, int idBodega, decimal CubicajeMaximo)
        {
            DALRack Datos = new DALRack();
            return Datos.EliminaR(idRack, idBodega, CubicajeMaximo);
        }*/

        public bool VerificarCubicajeAcumuladoyMaximoRack(decimal cubicajeMaximo, decimal cubicajeAcumulado)
        {
            bool funci = false;

            if (cubicajeAcumulado >= cubicajeMaximo)
            {
                funci = true;
            }
            else
            {
                funci = false;
            }
            return funci;
        }
        //public bool VerificarCubicajeMaximoConBodega(float cubicajeMaximo, float cubicajeBodega)
        //{
        //Mi Proyecto
        //    bool funci = false;

        //    if (cubicajeMaximo >= cubicajeBodega)
        //    {
        //        funci = true;
        //    }
        //    else
        //    {
        //        funci = false;
        //    }
        //    return funci;
        //}

        public bool validarDescripciones(string descripcion, int idRack)
        {
            bool pasa = true;

            DataTable dt = Listado(descripcion);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    if (idRack == (int)dt.Rows[0]["ID_Rack"]) //si voy a cambiar el mismo, (esto porque la descripcion no puedo cambiarla si no hago esto)
                    {
                        pasa = true;
                    }
                    else
                    {
                        if (descripcion == (string)dt.Rows[0]["DescripcionRack"])
                        {
                            pasa = false;
                        }
                        else
                        {
                            pasa = false;
                        }
                    }

                }
                else
                {
                    int indice = 0;
                    foreach (var e in dt.Rows)
                    {
                        if (descripcion == (string)dt.Rows[indice]["DescripcionRack"])
                        {
                            pasa = false;
                        }
                        indice++;

                    }
                }


            }
            else
            {
                pasa = true;
            }

            return pasa;
        }

        public static DataTable ListadoRporBodega(string cTexto)
        {
            DALRack Datos = new DALRack();
            return Datos.ListadoRporBodega(cTexto);
        }

        public static bool VerificarCubicajeMaximoConBodega(decimal cubicajeMaximoRack, string descripcionBodega)
        {
            bool funci1 = false;
            DataTable Tablilla = BLBodega.Listado(descripcionBodega);
            decimal pruebaBodega = (decimal)Tablilla.Rows[0]["CubicajeMaximo"];

            if (Tablilla.Rows.Count > 0)
            {
                if (cubicajeMaximoRack <= pruebaBodega)
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

        public static bool ValidarSumaRacks(decimal cubicajeMaximoRack, string descripcionBodega)
        {
            bool funci = false;
            /*Coger todos los racks de una la bodega escogida, sumar los cubicajes maximos de esos racks a
              esos racks le sumo el cubicaje maximo del rack que se va a registrar y lo comparo con bodega
            return funci1;
            */
            //CREAR UN DATATABLE CON UN LISTADO de racks QUE LE PASE POR PARAMETRO EL ID DE BODEGA (TENES QUE CREAR U NUEVO SP PARA Q BUSQUE UNICAMENTE POR IDBODEGA DEL RACK)
            //cuando ya tenemos todos esos racks
            //agarramos la columna de cubicaje maximo y las sumamos en una variable 
            //si esa variable es mayor a el cuicaje maximo de la bodega es falso

            DataTable Tablona2 = BLBodega.Listado(descripcionBodega);//la bodega con ese id de bodega del rack que quiero registrar
            int idBodega = (int)Tablona2.Rows[0]["ID_Bodega"];
            DataTable Tablona = BLRack.ListadoRporBodega(Convert.ToString(idBodega));//traer todos los racks con el id de bodega del rack que quiero registrar


            decimal totalCubicajeMaximoRacks = 0;
            int indice = 0;
            if (Tablona.Rows.Count > 0)
            {
                foreach (var e in Tablona.Rows)
                {
                    totalCubicajeMaximoRacks = totalCubicajeMaximoRacks + (decimal)Tablona.Rows[indice]["CubicajeMaximo"];
                    indice++;
                }

                totalCubicajeMaximoRacks = totalCubicajeMaximoRacks + cubicajeMaximoRack;

            }
            else
            {
                funci = false;
            }

            decimal CubicajeBodega = (decimal)Tablona2.Rows[0]["CubicajeMaximo"];



            if (Tablona.Rows.Count > 0 && Tablona2.Rows.Count > 0)
            {
                if (totalCubicajeMaximoRacks <= CubicajeBodega) //significa que esta bien
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
                funci = true;

            }


            return funci;
        }


        private static DALRack obj = new DALRack();

        public static List<Rack> ListarRack()
        {
            return obj.ListarRack();
        }

        public static int Agregar(int opcion,Rack rack ) //idBodegaAnterior es aquella bodega que se no se le ha descontado el cub maximo del rack
        {
            int valor;

            var descripcionBodega = BLBodega.GetBodega(rack.ID_Bodega).DescripcionBodega;

            if (VerificarCubicajeMaximoConBodega(rack.CubicajeMaximo, descripcionBodega) == true)
            {
                if (ValidarSumaRacks(rack.CubicajeMaximo, descripcionBodega) == true)
                {
                    if (opcion == 1) //esto es que lo crea, entonces no pasa nada, las validaciones que hizo ya bastan
                    {
                        obj.GuardarR(opcion, rack);
                    }
                    else //es que va a editar, y falta descontarle el cubicaje maximo del rakc a su bodega anterior
                    {
                        var RackAunNoActualizado = BLRack.GetRack(rack.ID_Rack); // aqui tengo el rack, pero no lo he actualizado aun, entonces me va a traer el id de bodega anterior
                        var bodega = BLBodega.GetBodega(RackAunNoActualizado.ID_Bodega);//tengo la bodega, solo tengo que descontarle el cub.max del rack
                        bodega.CubicajeAcumulado -= RackAunNoActualizado.CubicajeMaximo; //tengo que quitarle el cubicaje acumulado del rack anteiror.
                        Console.WriteLine();
                        BLBodega.Guardar(2,bodega);
                        /*
                        var bodegaNuevaDeRack = BLBodega.GetBodega(rack.ID_Bodega);//como en el sp no le he agregado el cubicaje max a la nueva bodega, aqui se lo agrego
                        bodegaNuevaDeRack.CubicajeAcumulado += rack.CubicajeMaximo;
                        BLBodega.Guardar(2, bodegaNuevaDeRack);
                        */
                        obj.GuardarR(opcion, rack);
                    }
                    
                   
                    valor = 1; //"Se creo el rack exitosamente "
                }
                else
                {
                    valor = 2;//"La suma de los racks de la bodega seleccionada es superior al cubicaje maximo de la bodega ";
                }

            }
            else
            {
                valor = 3; //"El cubicaje maximo del rack es superior al cubicaje maximo de la bodega";
            }

            return valor;
        }

        public static Rack GetRack(int id)
        {
            return obj.GetRack(id);
        }

        public static Rack GetRackDesc(string desc)
        {
            return obj.GetRackDesc(desc);
        }

        public static void Editar(Rack rack)
        {
            obj.Editar(rack);
        }

        public static void Eliminar(int id,int idBodega, decimal CubicajeMaximo)
        {
            obj.EliminaR(id, idBodega, CubicajeMaximo);
        }
        
        public static List<RackDTO> ListaConvertirADTO()
        {
            var rack = ListarRack();
            List<RackDTO> listaRackDTO = new List<RackDTO>();

            foreach (var item in rack) //aqui voy a convertir todo
            {
                RackDTO rkDTO = new RackDTO(); //creo el tpDTO
                //asigno todo igual excepto el idRack
                rkDTO.ID_Rack = item.ID_Rack;
                rkDTO.DescripcionRack = item.DescripcionRack;
                rkDTO.CubicajeAcumulado = item.CubicajeAcumulado;
                rkDTO.CubicajeMaximo = item.CubicajeMaximo;
                rkDTO.Activo = item.Activo;
             
               
                //asigno la desc de la bodega
                var bodega = BLBodega.GetBodega(item.ID_Bodega);
                rkDTO.DescripcionBodega = bodega.DescripcionBodega; ;

                listaRackDTO.Add(rkDTO);
            }

            return listaRackDTO;
        }

        public static RackDTO UnoConvertirADTO(Rack rack)
        {
            RackDTO rkDTO = new RackDTO(); //creo el tpDTO
                                           //asigno todo igual excepto el idRack
            rkDTO.ID_Rack = rack.ID_Rack;
            rkDTO.DescripcionRack = rack.DescripcionRack;
            rkDTO.CubicajeAcumulado = rack.CubicajeAcumulado;
            rkDTO.CubicajeMaximo = rack.CubicajeMaximo;
            rkDTO.Activo = rack.Activo;


            //asigno la desc de la bodega
            var bodega = BLBodega.GetBodega(rack.ID_Bodega);
            rkDTO.DescripcionBodega = bodega.DescripcionBodega; ;

            return rkDTO;
        }

        }
    }
