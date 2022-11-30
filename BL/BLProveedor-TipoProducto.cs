using DAL;
using ET;
using System.Collections.Generic;
using System.Data;

namespace BL
{
    public class BLProveedor_TipoProducto
    {

        public static DataTable Listado(string IdProvTP)
        {
            DALProveedor_TipoProducto Datos = new DALProveedor_TipoProducto();
            return Datos.Listado(IdProvTP);
        }

        public static string Guardar(int nOpcion, ProveedorTipoProducto PTP)
        {
            DALProveedor_TipoProducto Datos = new DALProveedor_TipoProducto();
            return Datos.Guardar(nOpcion, PTP);
        }
        /*
        public static string Eliminar(int idProvTipoP)
        {
            DALProveedor_TipoProducto Datos = new DALProveedor_TipoProducto();
            return Datos.Eliminar(idProvTipoP);
        }*/

        public static DataTable ListadoporIntAmbos(int idProveedor, int idTipoProducto)
        {
            DALProveedor_TipoProducto Datos = new DALProveedor_TipoProducto();
            return Datos.ListadoporIntAmbos(idProveedor, idTipoProducto);

        }

        public static DataTable ListadoporTP(int idTipoProducto)
        {
            DALProveedor_TipoProducto Datos = new DALProveedor_TipoProducto();
            return Datos.ListadoporTP(idTipoProducto);

        }

        //


        private static DALProveedor_TipoProducto obj = new DALProveedor_TipoProducto();

        public static List<ProveedorTipoProducto> ListarProveedorTipoProducto()
        {
            return obj.ListarProveedorTipoProducto();
        }

        public static bool Agregar(ProveedorTipoProducto ptp)
        {
            var lista = ListarProveedorTipoProducto();
            int pasa = 1;//guarda / crea
            foreach (var item in lista)
            {
                if (item.idTipoProducto == ptp.idTipoProducto && item.idProveedor == ptp.idProveedor)
                {
                    if (item.idProveedorTipoProducto != ptp.idProveedorTipoProducto) //diferentes ids, mismos datos = ya hay un enlace igual, entonces no puedo crear otro igual
                    {
                        pasa = 0; //no va a registrar
                    }
                    else
                    {
                        pasa = 2; //son mismos datos pero es xq es el mismo, por ende va a actualizar aunque sea a si mismo
                    }

                }
            }

            if (pasa == 1)//crea 
            {
                Guardar(1, ptp);
                return true;
            }
            else
            {
                return false; //esto es porque ya ese enlaze existe o uno de los que se mando por el controller
            }


        }

        public static ProveedorTipoProducto GetProveedorTipoProducto(int id)
        {
            return obj.GetProveedorTipoProducto(id);
        }

        public static bool Editar(ProveedorTipoProducto ptp)
        {
            //vea si no se puede registrar/actua
            var lista = ListarProveedorTipoProducto();
            int pasa = 2; //caso 3
            foreach (var item in lista)
            {
                //casos:
                /*
                 *1. Actualizar con los excatos mismos datos
                 *2. Actualizar con datos iguales a otro id
                 *3. Actualizar con datos diferentes a todos
                 * */



                if (item.idTipoProducto == ptp.idTipoProducto && item.idProveedor == ptp.idProveedor)//los contenidos son iguales
                {
                    if (item.idProveedorTipoProducto == ptp.idProveedorTipoProducto) //es el mismo
                    {
                        pasa = 2; //caso 1
                    }
                    else
                    {
                        pasa = 0; //no va a actualizar
                    }
                }//no le pongo else porque sino el pasa sera del ultimo
            }

            if (pasa == 2)//actualiza
            {
                Guardar(2, ptp);
                return true;
            }
            else
            {
                return false; //esto es porque ya ese enlaze existe o uno de los que se mando por el controller
            }
        }
        
        public static void Eliminar(int id)
        {
            obj.Eliminar(id);
        }
        
        public static List<ProveedorTipoProductoDTO> ListaConvertirADTO()
        {
            var ptps = ListarProveedorTipoProducto();
            List<ProveedorTipoProductoDTO> listaprovTP = new List<ProveedorTipoProductoDTO>();

            foreach (var item in ptps) //aqui voy a convertir todo
            {
                ProveedorTipoProductoDTO provTPDTO = new ProveedorTipoProductoDTO(); //creo el tpDTO
                //asigno todo igual excepto el idRack
                provTPDTO.idProveedorTipoProducto = item.idProveedorTipoProducto;
                provTPDTO.Activo = item.Activo;


                //asigno la desc de la bodega
                var tp = BLTipoProducto.GetTipoProducto(item.idTipoProducto);
                provTPDTO.descripcionTipoProducto = tp.DescripcionTipoProducto; ;
                var prov = BLProveedor.GetProveedor(item.idProveedor);
                provTPDTO.descripcionProveedor = prov.DescripcionProveedor;

                listaprovTP.Add(provTPDTO);
            }

            return listaprovTP;
        }

        public static ProveedorTipoProductoDTO UnoConvertirADTO(ProveedorTipoProducto item)
        {
            ProveedorTipoProductoDTO provTPDTO = new ProveedorTipoProductoDTO(); //creo el tpDTO
                                                                                 //asigno todo igual excepto el idRack
            provTPDTO.idProveedorTipoProducto = item.idProveedorTipoProducto;
            provTPDTO.Activo = item.Activo;


            //asigno la desc de la bodega
            var tp = BLTipoProducto.GetTipoProducto(item.idTipoProducto);
            provTPDTO.descripcionTipoProducto = tp.DescripcionTipoProducto; ;
            var prov = BLProveedor.GetProveedor(item.idProveedor);
            provTPDTO.descripcionProveedor = prov.DescripcionProveedor;

            return provTPDTO;
        }
    }
}
