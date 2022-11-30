using DAL;
using ET;
using BL;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Windows.Forms;

namespace BL
{
    public class BLRecepcion
    {
        public static DataTable ListadoRe(string cTexto)
        {
            DALRecepcion Datos = new DALRecepcion();
            return Datos.ListadoRe(cTexto);
        }

        public static DataTable Listado_detalle_Re(int idEncabezadoRecepcion)
        {
            DALRecepcion Datos = new DALRecepcion();
            return Datos.Listado_detalle_Re(idEncabezadoRecepcion);
        }

        public static string GuardarRe(Encabezado_Recepcion recepcion, DataTable tablaR, int cantidadProd)
        {
            DALRecepcion Datos = new DALRecepcion();
            return Datos.GuardarRe(recepcion, tablaR, cantidadProd);
        }

        public static string EliminarRe(int idEncabezadoRecepcion, int cantidadProds)
        {
            DALRecepcion Datos = new DALRecepcion();
            return Datos.Eliminar(idEncabezadoRecepcion, cantidadProds);
        }
        public static bool VerificarProveedorConSuTipoProducto(string descripcionProveedor, string descripcionTipoProducto)
        {
            bool pasa = false;
            DataTable dtP = BLProveedor.Listado("%");
            DataTable dtTP = BLTipoProducto.Listado("%");
            int idProveedor = 0;
            int idTipoProducto = 0;
            if (dtP.Rows.Count > 0 && dtTP.Rows.Count > 0)
            {
                foreach (DataRow e in dtP.Rows)
                {
                    if (descripcionProveedor == (string)e["DescripcionProveedor"])
                    {
                        idProveedor = (int)e["ID_Proveedor"];
                    }

                }


                foreach (DataRow i in dtTP.Rows)
                {
                    if (descripcionTipoProducto == (string)i["DescripcionTipoProducto"])
                    {
                        idTipoProducto = (int)i["ID_TipoProducto"];
                    }
                }
            }

            DataTable dtIntermedia = BLProveedor_TipoProducto.ListadoporIntAmbos(idProveedor, idTipoProducto);

            if (dtIntermedia.Rows.Count > 0)
            {
                pasa = true;
            }
            else
            {
                pasa = false;
            }

            return pasa;
        }


        public static DataTable ListadoPrRe(string cTexto, int opcion)
        {
            DALRecepcion datos = new DALRecepcion();
            return datos.ListadoPrRe(cTexto, opcion);

        }
        public bool ValidarFechas(DateTime FechaI, DateTime FechaV)
        {
            bool pasa = true;

            if (FechaI >= FechaV)
            {
                pasa = true;
            }
            else
            {
                pasa = false;
            }

            return pasa;
        }



        public static int IngresarUbicacionAProductos(DataTable tDetalle, string descripcionBodega, string descripcionProveedor)
        {
            try
            {
                //Agarrar las ubicaciones ya creadas, y que sean iguales a la bodega seleccionada
                DataTable dtU = BLUbicacion.ListadoDesOcupado("%");
                DataTable dtB = BLBodega.Listado("%");
                DataTable dtTP = BLTipoProducto.Listado("%");
                DataTable dtR = BLRack.Listado("%");
                DataTable dtProv = BLProveedor.Listado("%");
                List<int> ListaidProductos = new List<int>();
                List<int> ListaUbicacionesDisponibles = new List<int>();
                string descripcionTipoProducto = "";
                string descripcionRackPermitido = "";
                int pasa = 0;
                int idProveedor = 0;
                Console.WriteLine(dtU.Rows.Count);

                foreach (DataRow dr in tDetalle.Rows)
                {
                    ListaidProductos.Add((int)dr["ID Producto"]); //Agarro todos los ID productos
                    descripcionTipoProducto = (string)dr["Descripcion Tipo Producto"];//ocupo el tipo de producto
                }
                Console.WriteLine(ListaidProductos.Count);
                //agarro el id de Rack permitido del tipo de producto y lo busco por su descripcion
                foreach (DataRow dr in dtTP.Rows)
                {
                    if (descripcionTipoProducto == (string)dr["DescripcionTipoproducto"])
                    {
                        descripcionRackPermitido = (string)dr["DescripcionRack"];//
                        Console.WriteLine();
                    }
                }

                //voy a recorrer para agarrar una ubicacion que coincida con la desc de bodega y que sea permitido almacenar por el idRackPermitido
                foreach (DataRow dr in dtU.Rows)
                {
                    if (descripcionBodega == (string)dr["DescripcionBodega"] && descripcionRackPermitido == (string)dr["DescripcionRack"])
                    {
                        ListaUbicacionesDisponibles.Add((int)dr["ID_Ubicacion"]);
                        Console.WriteLine();//
                    }
                }

                foreach (DataRow dr in dtProv.Rows)
                {
                    if (descripcionProveedor == (string)dr["DescripcionProveedor"])
                    {
                        idProveedor = (int)dr["ID_Proveedor"];
                        Console.WriteLine();
                    }
                }


                //Ya cuando tengo las ubicaciones disponibles para los productos
                //que tenian como requisitos:
                //1. Que fuesen del rack permitido, que solo es uno.
                //2. Que la ubicacion fuese en la bodega mencionada
                //3. Verificar que tenga la misma cantidad o mayor de ubicaciones, que de productos
                //4.Traer el objeto de producto en si, y actualizarlo aca en el prodgrama.
                BLProducto blp = new BLProducto();

                if (ListaidProductos.Count <= ListaUbicacionesDisponibles.Count)
                {
                    if (blp.ValidarSumaCubiProd(tDetalle) == true)
                    {
                        int indice = 0;

                        foreach (int e in ListaidProductos)
                        {
                            DataTable dtprod = BLProducto.Listadocomp(Convert.ToString(e)); //aqui agarramos el producto en la posicion e.

                            int idProducto = (int)dtprod.Rows[0]["ID_Producto"];
                            string descripcion = (string)dtprod.Rows[0]["DescripcionProducto"];
                            DateTime fechaA = (DateTime)dtprod.Rows[0]["Fecha_Alta"];
                            int diferenciaMV = (int)dtprod.Rows[0]["DiferenciaMesesVencimiento"];
                            decimal cubicaje = (decimal)dtprod.Rows[0]["Cubicaje"];
                            int empaque = (int)dtprod.Rows[0]["Empaque"];
                            decimal descuento = (decimal)dtprod.Rows[0]["Descuento"];
                            decimal IVA = (decimal)dtprod.Rows[0]["IVA"];
                            decimal costoBruto = (decimal)dtprod.Rows[0]["CostoBruto"];
                            decimal costoNeto = (decimal)dtprod.Rows[0]["CostoNeto"];
                            // int idUbicacion = (int)dtprod.Rows[0]["ID_Ubicacion"];
                            int idTP = (int)dtprod.Rows[0]["ID_TipoProducto"];
                            // int idProv = (int)dtprod.Rows[0]["ID_Proveedor"];
                            int idUM = (int)dtprod.Rows[0]["ID_UnidadMedida"];

                            int idUbicacion = ListaUbicacionesDisponibles[indice];//convertir el

                            Producto producto = new Producto();
                            producto.ID_Producto = idProducto;
                            producto.DescripcionProducto = descripcion;
                            producto.Fecha_Alta = fechaA;
                            producto.DiferenciaMesesVencimiento = diferenciaMV;
                            producto.Cubicaje = cubicaje;
                            producto.Empaque = empaque;
                            producto.Descuento = descuento;
                            producto.IVA = IVA;
                            producto.CostoBruto = costoBruto;
                            producto.CostoNeto = costoNeto;
                            producto.ID_Ubicacion = idUbicacion;
                            producto.ID_TipoProducto = idTP;
                            producto.ID_Proveedor = idProveedor;
                            producto.ID_UnidadMedida = idUM;

                            string Rpta = "", Rpta2 = "";

                            Rpta = BLProducto.Guardar(2, producto);

                            pasa = 1;//todo bien
                            indice++;
                            if (Rpta == "OK")
                            {
                                Console.WriteLine("Si lo actualizo");
                                //actualizar el cubicaje acum de rack
                                //1. agarramos de producto, el id de ubicacion
                                //2. de id ubicacion, la descripcion del Rack
                                //3. de la descr de rack, mandamos esa desc a un listado de Rack, para que nos traiga todos los datos del rack
                                //4. le agregamos al atributo de rack "cubicaje acumulado", el cubicaje del producto.
                            }
                        }
                    }
                    else
                    {
                        pasa = 2; //el cubicaje de los prods
                                  //MessageBox.Show("El cubicaje de los productos más el cubicaje acumulado del rack, sobrepasan el cubicaje Maximo del mismo ");
                    }
                }
                else
                {
                    pasa = 3;//no hay ubicacines suficientes
                             // MessageBox.Show("No hay ubicaciones suficientes para guardar los productos seleccionados"); //luis hp
                }
                return pasa;
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }
        }




        public static bool VerificarRolConSuTipoProducto(int idRol, int idTipoProducto)
        {
            bool pasa = false;

            DataTable dtIntermedia = BLRolTipoProducto.ListadoporIntAmbos(idRol, idTipoProducto);

            if (dtIntermedia.Rows.Count > 0)
            {
                pasa = true;
            }
            else
            {
                pasa = false;
            }

            return pasa;
        }
        public string MostrarStockMensajeTP(string descripcionTipoProducto) //paso la cantidad de prods que entraron ya a la bodega con ese tipo de producto, y decir:
                                                                            //1. Mostrar la cantidad de prods que estan en la bodega de ese TP
        {   //2. Dar sugerencia: -Decir que hace falta x cantidad para satisfacer el stock minimo
            // - Si el stock maximo ya esta en su tope, indicar que ya no se puede ingresar mas productos de ese TP
            // -Si estan los prods en ese rango de stock minimo y maximo, indicar que andan bien de stock
            DataTable dt = BLTipoProducto.Listado(descripcionTipoProducto);
            int StockActual = (int)dt.Rows[0]["StockActual"];

            int stockMinimo = (int)dt.Rows[0]["StockMinimo"];
            int stockMaximo = (int)dt.Rows[0]["StockMaximo"];
            string mensaje = "";

            if (StockActual >= stockMinimo)
            {

                mensaje = "El stock actual se encuentra en la cantidad necesaria: " + StockActual;
            }
            else
            {
                int resultado = stockMinimo - StockActual;
                mensaje = "El stock actual se encuentra por debajo del stock minimo. Se necesitan recepcionar " + resultado + " productos más para cumplir con el stock minimo.";
            }

            if (StockActual == stockMaximo)
            {
                mensaje = "El stock actual (" + StockActual + ") se encuentra en el limite requerido. No se permite ingresar más de los que ya se encuentran.";
            }


            return mensaje;
        }


        public static bool ValidarStockMaximo(string descripcionTipoProducto, int cantidad)
        {
            bool pasa = true;

            DataTable dt = BLTipoProducto.Listado(descripcionTipoProducto);
            int StockActual = (int)dt.Rows[0]["StockActual"];
            int stockMaximo = (int)dt.Rows[0]["StockMaximo"];

            int stockActualConProds = StockActual + cantidad;

            if (stockActualConProds > stockMaximo)
            {
                pasa = false;
            }

            return pasa;
        }


        public static bool VerificarDMV(int DMV, DateTime fechaInicio, DateTime fechaVencimiento)
        {
            bool pasa = false;
           
            //agarro diferenciaMV de ese producto
            //Comparo fechaInicio con fechaVencimiento
            //al comparar las dos fechas, me tiene que dar como resultado la cantidad de meses


            int diferenciaMesesVencimiento = DMV;
            int diferenciaReal = ((fechaVencimiento.Year * 12 + fechaVencimiento.Month) - (fechaInicio.Year * 12 + fechaInicio.Month));
            int dif = 0;

            if (diferenciaReal < 0)
            {
                dif = diferenciaReal * -1;
            }else
            {
                dif = diferenciaReal; 
            }
         
            if (diferenciaMesesVencimiento < dif)
            {
                pasa = true;
            }

            return pasa;
        }
        
        private static DALRecepcion obj = new DALRecepcion(); 

        public static List<Encabezado_Recepcion> ListarEncabezadoRecepcion()
        {
            return obj.ListarEncabezadoRecepcion();
        }

        public static List<EncabezadoRecepcionDTO> ListaEncabezadoConvertirADTO()
        {
            var encabezadoR = ListarEncabezadoRecepcion();
            List<EncabezadoRecepcionDTO> listaERDTO = new List<EncabezadoRecepcionDTO>();

            foreach (var item in encabezadoR) //aqui voy a convertir todo
            {
                var erDTO = new EncabezadoRecepcionDTO(); 
                
                erDTO.ID_EncabezadoRecepcion = item.ID_EncabezadoRecepcion;
                erDTO.DescripcionEncabezadoRecepcion = item.DescripcionEncabezadoRecepcion;
                erDTO.FechaIngreso = item.FechaIngreso;
                erDTO.fechaVencimiento = item.fechaVencimiento;
                erDTO.Activo = item.Activo;

                //desc usuario
                var usuario = BLUsuario.GetUsuario(item.ID_Usuario);
                erDTO.NombreUsuario = usuario.NombreUsuario;
                //asigno la desc de la bodega
                var bodega = BLBodega.GetBodega(item.ID_Bodega);
                erDTO.DescripcionBodega = bodega.DescripcionBodega;
                //asigno la desc de la proveedor
                var proveedor = BLProveedor.GetProveedor(item.ID_Proveedor);
                erDTO.DescripcionProveedor = proveedor.DescripcionProveedor;
                //asigno la desc de la tipoProducto
                var tipoP = BLTipoProducto.GetTipoProducto(item.ID_TipoProducto);
                erDTO.DescripcionTipoProducto = tipoP.DescripcionTipoProducto;
                //asigno la desc de la Parametro
                var parametro = BLParametros.GetParametro(item.ID_Parametro);
                erDTO.DescripcionParametro = parametro.DescripcionParametro;

                listaERDTO.Add(erDTO);
            }

            return listaERDTO;
        }

        public static EncabezadoRecepcionDTO UnoConvertirADTO(Encabezado_Recepcion item)
        {
            var erDTO = new EncabezadoRecepcionDTO();

            erDTO.ID_EncabezadoRecepcion = item.ID_EncabezadoRecepcion;
            erDTO.DescripcionEncabezadoRecepcion = item.DescripcionEncabezadoRecepcion;
            erDTO.FechaIngreso = item.FechaIngreso;
            erDTO.fechaVencimiento = item.fechaVencimiento;
            erDTO.Activo = item.Activo;

            //desc usuario
            var usuario = BLUsuario.GetUsuario(item.ID_Usuario);
            erDTO.NombreUsuario = usuario.NombreUsuario;
            //asigno la desc de la bodega
            var bodega = BLBodega.GetBodega(item.ID_Bodega);
            erDTO.DescripcionBodega = bodega.DescripcionBodega;
            //asigno la desc de la proveedor
            var proveedor = BLProveedor.GetProveedor(item.ID_Proveedor);
            erDTO.DescripcionProveedor = proveedor.DescripcionProveedor;
            //asigno la desc de la tipoProducto
            var tipoP = BLTipoProducto.GetTipoProducto(item.ID_TipoProducto);
            erDTO.DescripcionTipoProducto = tipoP.DescripcionTipoProducto;
            //asigno la desc de la Parametro
            var parametro = BLParametros.GetParametro(item.ID_Parametro);
            erDTO.DescripcionParametro = parametro.DescripcionParametro;

            return erDTO;
        }

        public static DataTable ConvertirListaADataTable(List<ProductoDTO> lista)
        {
            DataTable tablaRecepcion = new DataTable("TablaRecepcion");
            try {

                //DataTable tablaRecepcion = new DataTable("TablaRecepcion");
                tablaRecepcion.Columns.Add("ID Producto", System.Type.GetType("System.Int32"));
                tablaRecepcion.Columns.Add("Descripcion Producto", System.Type.GetType("System.String"));
                tablaRecepcion.Columns.Add("Fecha de Alta", System.Type.GetType("System.DateTime"));
                tablaRecepcion.Columns.Add("Diferencia Meses de Vencimiento", System.Type.GetType("System.Int32"));
                tablaRecepcion.Columns.Add("Cubicaje", System.Type.GetType("System.Decimal"));
                tablaRecepcion.Columns.Add("Empaque", System.Type.GetType("System.Int32"));
                tablaRecepcion.Columns.Add("Descuento", System.Type.GetType("System.Decimal"));
                tablaRecepcion.Columns.Add("IVA", System.Type.GetType("System.Decimal"));
                tablaRecepcion.Columns.Add("Costo Bruto", System.Type.GetType("System.Decimal"));
                tablaRecepcion.Columns.Add("Costo Neto", System.Type.GetType("System.Decimal"));
                tablaRecepcion.Columns.Add("Descripcion Rack", System.Type.GetType("System.String"));
                tablaRecepcion.Columns.Add("Descripcion Bodega", System.Type.GetType("System.String"));
                tablaRecepcion.Columns.Add("Descripcion Tipo Producto", System.Type.GetType("System.String"));
                tablaRecepcion.Columns.Add("Descripcion Proveedor", System.Type.GetType("System.String"));
                tablaRecepcion.Columns.Add("Descripcion U.Medida", System.Type.GetType("System.String"));

                foreach (var item in lista) //
                {
                    //var rack = BLRack.GetRackDesc(item.DescripcionRack);
                    // var bodega = BLBodega.GetBodega(rack.ID_Bodega);
                    tablaRecepcion.Rows.Add(item.ID_Producto, item.DescripcionProducto, item.Fecha_Alta, item.DiferenciaMesesVencimiento, item.Cubicaje, item.Empaque, item.Descuento, item.IVA, item.CostoBruto, item.CostoNeto, item.DescripcionRack, "N/A", item.DescripcionTipoProducto, item.DescripcionProveedor, item.DescripcionUnidadMedida);
                }

                
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
            return tablaRecepcion;
        }



        /////////////////////
        //////////////////////
        ///////////////////////
        ///

        public static Encabezado_Recepcion GetEncabezadoR(int id)
        {
            return obj.GetEncabezadoR(id);
        }


        public static TodoRecepcionDTO JuntarTodoRecepcion(EncabezadoRecepcionDTO encabezado , List<ProductoDTO> lista)
        {
            var TodoRecepcion = new TodoRecepcionDTO();

            TodoRecepcion.encabezado_Recepcion = encabezado;
            TodoRecepcion.listaProductos = lista;

            return TodoRecepcion;   

        }

        public static List<ProductoDTO> ConvertirDataTableALista(DataTable detalle)
        {


            var lista = new List<ProductoDTO>();
           
            for (int i = 0; i < detalle.Rows.Count; i++)
            {
                var productoUnMomento = new ProductoDTO();
                productoUnMomento.ID_Producto = Convert.ToInt32(detalle.Rows[i]["ID_Producto"]);

                var producto = BLProducto.GetProducto(productoUnMomento.ID_Producto);//ya tengo todo el producto

                var productoDTO = BLProducto.UnoConvertirADTO(producto);

                lista.Add(productoDTO);
            }
            return lista;
        }

    }
}




