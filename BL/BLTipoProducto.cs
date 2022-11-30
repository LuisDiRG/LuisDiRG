using DAL;
using ET;
using System.Collections.Generic;
using System.Data;
namespace BL
{
    public class BLTipoProducto
    {

        public static DataTable Listado(string cTexto)
        {
            DALTipoProducto Datos = new DALTipoProducto();
            return Datos.Listado(cTexto);
        }


        public static string Guardar(int nOpcion, Tipo_Producto tipoP)
        {
            DALTipoProducto Datos = new DALTipoProducto();
            return Datos.Guardar(nOpcion, tipoP);
        }
        /*
        public static string Eliminar(int idTipoProducto)
        {
            DALTipoProducto Datos = new DALTipoProducto();
            return Datos.Eliminar(idTipoProducto);
        }*/

        public static bool ValidaStock(int stockmin, int stockmax)
        {
            bool pasa = false;

            if (stockmax > stockmin)
            {
                pasa = true;
            }
            else
            {
                pasa = false;
            }

            return pasa;
        }

        public bool validarDescripciones(string descripcion)
        {
            bool pasa = true;

            DataTable dt = Listado(descripcion);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    if (descripcion == (string)dt.Rows[0]["DescripcionTipoProducto"])
                    {
                        pasa = false;
                    }
                    else
                    {
                        pasa = false;
                    }
                }
                else
                {
                    int indice = 0;
                    foreach (var e in dt.Rows)
                    {
                        if (descripcion == (string)dt.Rows[indice]["DescripcionTipoProducto"])
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

        private static DALTipoProducto obj = new DALTipoProducto();

        public static List<Tipo_Producto> ListarTipoProducto()
        {
            return obj.ListarTipoProducto();
        }

        public static void Agregar(Tipo_Producto rtp)
        {
            obj.Agregar(rtp);
        }

        public static Tipo_Producto GetTipoProducto(int id)
        {
            return obj.GetTipoProducto(id);
        }

        public static void Editar(Tipo_Producto rtp)
        {
            obj.Editar(rtp);
        }

        public static void Eliminar(int id)
        {
            obj.Eliminar(id);
        }

        public static List<TipoProductoDTO> ListaConvertirADTO()
        {
            var tipoProducto = ListarTipoProducto();
            List<TipoProductoDTO> listaTipoProductoDTO = new List<TipoProductoDTO>();

            foreach (var item in tipoProducto) //aqui voy a convertir todo
            {
                TipoProductoDTO tpDTO = new TipoProductoDTO(); //creo el tpDTO
                //asigno todo igual excepto el idRack
                tpDTO.ID_TipoProducto = item.ID_TipoProducto;
                tpDTO.DescripcionTipoProducto = item.DescripcionTipoProducto;
                tpDTO.StockMaximo = item.StockMaximo;
                tpDTO.StockMinimo = item.StockMinimo;
                tpDTO.Activo = item.Activo;
                tpDTO.StockActual = item.StockActual;
                tpDTO.noRegistrados = item.noRegistrados;
                //asigno la desc del rack
                var rack = BLRack.GetRack(item.ID_RackPermitido);
                tpDTO.DescripcionRack = rack.DescripcionRack;

                listaTipoProductoDTO.Add(tpDTO);
            }

            return listaTipoProductoDTO;
        }

        public static TipoProductoDTO UnoConvertirADTO(Tipo_Producto item)
        {
            TipoProductoDTO tpDTO = new TipoProductoDTO(); //creo el tpDTO
                                                           //asigno todo igual excepto el idRack
            tpDTO.ID_TipoProducto = item.ID_TipoProducto;
            tpDTO.DescripcionTipoProducto = item.DescripcionTipoProducto;
            tpDTO.StockMaximo = item.StockMaximo;
            tpDTO.StockMinimo = item.StockMinimo;
            tpDTO.Activo = item.Activo;
            tpDTO.StockActual = item.StockActual;
            tpDTO.noRegistrados = item.noRegistrados;
            //asigno la desc del rack
            var rack = BLRack.GetRack(item.ID_RackPermitido);
            tpDTO.DescripcionRack = rack.DescripcionRack;

            return tpDTO;
        }

    }
}
