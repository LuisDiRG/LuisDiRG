using DAL;
using ET;
using System.Data;
using System;
using System.Collections.Generic;

namespace BL
{
    public class BLProducto
    {
        public static DataTable Listado(string cTexto)
        {
            DALProducto datos = new DALProducto();
            return datos.Listado(cTexto);
        }

        public static DataTable Listadocomp(string cTexto)
        {
            DALProducto datos = new DALProducto();
            return datos.Listadocomp(cTexto);
        }

        public static string Guardar(int nOpcion, Producto producto)
        {
            DALProducto datos = new DALProducto();

            if (nOpcion == 2)//voy a actualizar
            {
                //quitar el no registrado o stock actual (esto l oveo si tiene id proveedor o ubi) al TP
                //ahora se lo agrego al que voy a cambiar
                //listo

                var prodSinActualizar = BLProducto.GetProducto(producto.ID_Producto); //tengo el producto sin actualizar
               //al tp anterior, hay que ver si el producto tiene id ubi o prov
                var TPsinActualizar = BLTipoProducto.GetTipoProducto(prodSinActualizar.ID_TipoProducto); 

                if (prodSinActualizar.ID_Proveedor == null && prodSinActualizar.ID_Ubicacion == null)//significa que es un no registrado
                {
                    TPsinActualizar.noRegistrados--; //le quito uno, que es el prod que voy a actualizar
                }
                else
                {
                    TPsinActualizar.StockActual--;
                }

                BLTipoProducto.Editar(TPsinActualizar);//ya le mando el TP anterior del producto ya actualizado

                //ahora hay que actualizar el TP actual, al que se le asigno el prod actualizado
                var TPNuevo = BLTipoProducto.GetTipoProducto(producto.ID_TipoProducto);
                if (producto.ID_Proveedor == null && producto.ID_Ubicacion == null)//significa que es un no registrado
                {
                    TPNuevo.noRegistrados++; //le quito uno, que es el prod que voy a actualizar
                }
                else
                {
                    TPNuevo.StockActual++;
                }
                BLTipoProducto.Editar(TPNuevo);
            }

            return datos.Guardar(nOpcion, producto);
        }

        public static string Eliminar(int idProducto, int idTipoP, int idUbica)
        {
            DALProducto datos = new DALProducto();
            return datos.Eliminar(idProducto, idTipoP, idUbica);
        }


        public bool validarDescripciones(string descripcion)
        {
            bool pasa = true;

            DataTable dt = Listado(descripcion);
            if (dt.Rows.Count > 0)
            {
                if (descripcion == (string)dt.Rows[0]["DescripcionProducto"])
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

        public decimal CalcularCostoNeto(decimal costoBruto, decimal descuento, decimal iva)
        {
            decimal costoNeto = 0;

            decimal c1 = costoBruto * descuento;
            decimal c2 = (costoBruto * iva);
            costoNeto = costoBruto + c1 + c2;

            return costoNeto;

        }

        public bool ValidarSumaCubiProd(DataTable tDetalle)
        {
            bool funci = false;
            string descripcionRackPermitido = "";
            string descripcionTipoProducto = (string)tDetalle.Rows[0]["Descripcion Tipo Producto"];
            decimal cubicajeAcumulado = 0, cubicajeMaximo = 0;
            decimal cubicajeAcumuladoConProds = 0;
            //aunque sean mas productos, como todos son del mismo tipo de producto, entonces con agarrarlo desde el primero esta bien
            //agarrar el rack permitido de ese tipo de producto
            //ver el cubicaje acumulado de ese rack y sumarle el cubicaje actual. 
            //si eso no supera el cubicaje maximo, entonces si se le puede recepcionar en la bodega (asignarle un ID de Ubicacion)

            DataTable dtTP = BLTipoProducto.Listado(descripcionTipoProducto); //aqui me trae un tipo de producto
            descripcionRackPermitido = (string)dtTP.Rows[0]["DescripcionRack"];
            DataTable dtR = BLRack.Listado(descripcionRackPermitido);
            Console.WriteLine(dtR.Rows.Count);
            cubicajeAcumulado = (decimal)dtR.Rows[0]["CubicajeAcumulado"]; //traemos el cubicajeAc
            cubicajeMaximo = (decimal)dtR.Rows[0]["CubicajeMaximo"]; //traems el cubicajeMax

            cubicajeAcumuladoConProds = cubicajeAcumulado; //aqui le popngo el acumulado porque, si se lo pusiera en el IF, me lo sumaria n cantidad de veces (n siendo la cantidad de porductos que quiera registrar)
            foreach (DataRow dr in tDetalle.Rows)
            {
                cubicajeAcumuladoConProds = cubicajeAcumuladoConProds +  (decimal)dr["cubicaje"]; //era asi o con mayuscula
                Console.WriteLine();
            }

            if (cubicajeAcumuladoConProds <= (decimal)dtR.Rows[0]["CubicajeMaximo"])
            {//si el cubicaje ac con los prods es menor o igual:
                //1. Actualizar el cubicaje acumulado de Rack
                //2.Asignarle un ID de ubicacion a los distintos productos
                // *. todo esto lo hace otro metodo en BLRecepcion
                funci = true;

            }
            else
            {
                funci = false;
            }



            return funci;
        }

        public static DataTable ListadoPporUbi(string cTexto)
        {
            DALProducto Datos = new DALProducto();
            return Datos.ListadoPporUbi(cTexto);
        }

        private static DALProducto obj = new DALProducto();

        public static List<Producto> ListarProductos()
        {
            return obj.ListarProducto();
        }

        public static void Agregar(Producto pdt)
        {
            obj.Agregar(pdt);
        }

        public static Producto GetProducto(int id)
        {
            return obj.GetProducto(id);
        }

        public static void Editar(Producto pdt)
        {
            obj.Editar(pdt);
        }
        /*
        public static void Eliminar(int id)
        {
            obj.Eliminar(id);
        }
        */
        public static List<ProductoDTO> ListaConvertirADTO()
        {
            var productos = ListarProductos();
            List<ProductoDTO> listaProductosDTO = new List<ProductoDTO>();

            foreach (var item in productos) //aqui voy a convertir todo
            {
                ProductoDTO prodDTO = new ProductoDTO(); //creo el tpDTO
                //asigno todo igual excepto el idRack
                prodDTO.ID_Producto = item.ID_Producto;
                prodDTO.DescripcionProducto = item.DescripcionProducto;
                prodDTO.Fecha_Alta = item.Fecha_Alta;
                prodDTO.DiferenciaMesesVencimiento = item.DiferenciaMesesVencimiento;
                prodDTO.Cubicaje = item.Cubicaje;
                prodDTO.Empaque = item.Empaque;
                prodDTO.CostoBruto = item.CostoBruto;
                prodDTO.CostoNeto = item.CostoNeto;
                prodDTO.Descuento = item.Descuento;
                prodDTO.IVA = item.IVA;
                prodDTO.Activo = item.Activo;


                //asigno la desc de la bodega

                if (item.ID_Ubicacion != null)
                {
                    var Ubicacion = BLUbicacion.GetUbicacion((int)item.ID_Ubicacion);
                    var Rack = BLRack.GetRack(Ubicacion.ID_Rack);
                    prodDTO.DescripcionRack = Rack.DescripcionRack;

                }else
                {
                    prodDTO.DescripcionRack = "N/A";
                }

                if (item.ID_Proveedor != null)
                {
                    var proveedor = BLProveedor.GetProveedor((int)item.ID_Proveedor);
                    prodDTO.DescripcionProveedor = proveedor.DescripcionProveedor;
                }
                else
                {
                    prodDTO.DescripcionProveedor = "N/A";
                }

                var tipoProducto = BLTipoProducto.GetTipoProducto(item.ID_TipoProducto);
                prodDTO.DescripcionTipoProducto = tipoProducto.DescripcionTipoProducto;

                var um = BLUnidadMedida.GetUnidadMedida(item.ID_UnidadMedida);
                prodDTO.DescripcionUnidadMedida = um.DescripcionUnidadMedida;

                listaProductosDTO.Add(prodDTO);
            }

            return listaProductosDTO;
        }

        public static ProductoDTO UnoConvertirADTO(Producto item)
        {
            ProductoDTO prodDTO = new ProductoDTO(); //creo el tpDTO
                                                     //asigno todo igual excepto el idRack
            prodDTO.ID_Producto = item.ID_Producto;
            prodDTO.DescripcionProducto = item.DescripcionProducto;
            prodDTO.Fecha_Alta = item.Fecha_Alta;
            prodDTO.DiferenciaMesesVencimiento = item.DiferenciaMesesVencimiento;
            prodDTO.Cubicaje = item.Cubicaje;
            prodDTO.Empaque = item.Empaque;
            prodDTO.CostoBruto = item.CostoBruto;
            prodDTO.CostoNeto = item.CostoNeto;
            prodDTO.Descuento = item.Descuento;
            prodDTO.IVA = item.IVA;
            prodDTO.Activo = item.Activo;


            //asigno la desc de la bodega

            if (item.ID_Ubicacion != null)
            {
                var Ubicacion = BLUbicacion.GetUbicacion((int)item.ID_Ubicacion);
                var Rack = BLRack.GetRack(Ubicacion.ID_Rack);
                prodDTO.DescripcionRack = Rack.DescripcionRack;

            }
            else
            {
                prodDTO.DescripcionRack = "N/A";
            }

            if (item.ID_Proveedor != null)
            {
                var proveedor = BLProveedor.GetProveedor((int)item.ID_Proveedor);
                prodDTO.DescripcionProveedor = proveedor.DescripcionProveedor;
            }

            var tipoProducto = BLTipoProducto.GetTipoProducto(item.ID_TipoProducto);
            prodDTO.DescripcionTipoProducto = tipoProducto.DescripcionTipoProducto;

            var um = BLUnidadMedida.GetUnidadMedida(item.ID_UnidadMedida);
            prodDTO.DescripcionUnidadMedida = um.DescripcionUnidadMedida;
            return prodDTO;
        }

        public static List<ProductoDTO> ListaConvertirADTOEspecifica( List<Producto> lista)
        {
            
            List<ProductoDTO> listaProductosDTO = new List<ProductoDTO>();

            foreach (var item in lista) //aqui voy a convertir todo
            {
                ProductoDTO prodDTO = new ProductoDTO(); //creo el tpDTO
                //asigno todo igual excepto el idRack
                prodDTO.ID_Producto = item.ID_Producto;
                prodDTO.DescripcionProducto = item.DescripcionProducto;
                prodDTO.Fecha_Alta = item.Fecha_Alta;
                prodDTO.DiferenciaMesesVencimiento = item.DiferenciaMesesVencimiento;
                prodDTO.Cubicaje = item.Cubicaje;
                prodDTO.Empaque = item.Empaque;
                prodDTO.CostoBruto = item.CostoBruto;
                prodDTO.CostoNeto = item.CostoNeto;
                prodDTO.Descuento = item.Descuento;
                prodDTO.IVA = item.IVA;
                prodDTO.Activo = item.Activo;


                //asigno la desc de la bodega

                if (item.ID_Ubicacion != null)
                {
                    var Ubicacion = BLUbicacion.GetUbicacion((int)item.ID_Ubicacion);
                    var Rack = BLRack.GetRack(Ubicacion.ID_Rack);
                    prodDTO.DescripcionRack = Rack.DescripcionRack;

                }
                else
                {
                    prodDTO.DescripcionRack = "N/A";
                }

                if (item.ID_Proveedor != null)
                {
                    var proveedor = BLProveedor.GetProveedor((int)item.ID_Proveedor);
                    prodDTO.DescripcionProveedor = proveedor.DescripcionProveedor;
                }
                else
                {
                    prodDTO.DescripcionProveedor = "N/A";
                }

                var tipoProducto = BLTipoProducto.GetTipoProducto(item.ID_TipoProducto);
                prodDTO.DescripcionTipoProducto = tipoProducto.DescripcionTipoProducto;

                var um = BLUnidadMedida.GetUnidadMedida(item.ID_UnidadMedida);
                prodDTO.DescripcionUnidadMedida = um.DescripcionUnidadMedida;

                listaProductosDTO.Add(prodDTO);
            }

            return listaProductosDTO;
        }

        public static List<Producto> ListaProductosporTP(int idTipoProducto)
        {
            var listaProds = DALProducto.ListaProductosporTP(idTipoProducto);
            return listaProds;
        }
    }

}

