using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class RecepcionController : Controller
    {
        // GET: Recepcion
        public ActionResult Index()
        {
            var listaDTO = BLRecepcion.ListaEncabezadoConvertirADTO();
            return View(listaDTO);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Encabezado_Recepcion EncabezadoR, List<ProductoDTO> listaProductos)
        {//ya me trae los datos, todo bien

            try
            {
                DataTable dt = BLRecepcion.ConvertirListaADataTable(listaProductos);

                string Rpta = BLRecepcion.GuardarRe(EncabezadoR, dt, listaProductos.Count);

                if (Rpta == "OK")
                {
                    return Json(new
                    {
                        pasa = 1,
                        toRedirect = Url.Action("Index")
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        pasa = 2
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    pasa = 3,
                    msg = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetRecepcion(int id)
        {
            var detalle = BLRecepcion.Listado_detalle_Re(id);
            var EncabezadoR = BLRecepcion.GetEncabezadoR(id);
            var encabezadoRDTO = BLRecepcion.UnoConvertirADTO(EncabezadoR);
            var detalleAProductoDTO = BLRecepcion.ConvertirDataTableALista(detalle);
            var todoRecepcion = BLRecepcion.JuntarTodoRecepcion(encabezadoRDTO, detalleAProductoDTO);

            return View(todoRecepcion);
        }


        public JsonResult EnlaceProveedorTipoProducto(int idProveedor, int idTipoProducto)
        {
            try
            {
                var prov = BLProveedor.GetProveedor(idProveedor);
                var tp = BLTipoProducto.GetTipoProducto(idTipoProducto);
                bool pasa = BLRecepcion.VerificarProveedorConSuTipoProducto(prov.DescripcionProveedor, tp.DescripcionTipoProducto);

                if (pasa == true)//si encontro enlace
                {
                    return Json(new
                    {
                        pasa = 1
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        pasa = 2
                    }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                return Json(new
                {
                    pasa = 3,
                    msg = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult EnlaceRolTipoProducto(int idTipoProducto, int idUsuario)
        {
            try
            {
                var tp = BLTipoProducto.GetTipoProducto(idTipoProducto);
                var usuario = BLUsuario.GetUsuario(idUsuario);
                var rol = BLRol.GetRol(usuario.ID_Rol);
                bool pasa = BLRecepcion.VerificarRolConSuTipoProducto(rol.ID_Rol, tp.ID_TipoProducto);

                if (pasa == true)//si encontro enlace
                {
                    return Json(new
                    {
                        pasa = 1
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        pasa = 2
                    }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                return Json(new
                {
                    pasa = 3,
                    msg = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult getProductosPorSuTP(int idTipoProducto)
        {
            try
            {
                var listaProds = BLProducto.ListaProductosporTP(idTipoProducto);
                List<ProductoDTO> listaDTO = BLProducto.ListaConvertirADTOEspecifica(listaProds);

                return Json(listaDTO, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult VerificarDMV(int DMV, DateTime fechaInicio, DateTime fechaVencimiento)
        {

            try
            {
                bool pasa = BLRecepcion.VerificarDMV(DMV, fechaInicio, fechaVencimiento);

                if (pasa == true)
                {
                    return Json(new { pasa = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(new { pasa = 2 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { pasa = 3, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ValidarFechaParametroConFechaActual(string fechaActual, int idParametro)
        {
            try
            {

                var pasa = BLParametros.VerificaInicio(fechaActual, idParametro);

                if (pasa == true)
                {
                    return Json(new { pasa = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { pasa = 2 }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { pasa = 3, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult ValidarStockMaximo(int idTipoProducto, int cantidad)
        {
            try
            {
                var tp = BLTipoProducto.GetTipoProducto(idTipoProducto);

                bool pasa = BLRecepcion.ValidarStockMaximo(tp.DescripcionTipoProducto, cantidad);

                if (pasa == true)
                {
                    return Json(new { pasa = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { pasa = 2 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { pasa = 3, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult IngresarUbicacionAProductos(List<ProductoDTO> listaProductos, int idBodega, int idProveedor)
        {
            try
            {
                var bodega = BLBodega.GetBodega(idBodega);
                var prov = BLProveedor.GetProveedor(idProveedor);
                DataTable dt = BLRecepcion.ConvertirListaADataTable(listaProductos);
                //pasar de lista a dataTable
                int pasa = BLRecepcion.IngresarUbicacionAProductos(dt, bodega.DescripcionBodega, prov.DescripcionProveedor); //luis compila un toque porfa

                if (pasa == 1)//todo bien
                {
                    return Json(new { pasa = 1 }, JsonRequestBehavior.AllowGet);
                }
                else if (pasa == 2)//el cubicaje de los prods
                {
                    return Json(new { pasa = 2 }, JsonRequestBehavior.AllowGet);
                }
                else if (pasa == 3)//no hay ubicacioes suficientes
                {
                    return Json(new { pasa = 3 }, JsonRequestBehavior.AllowGet);
                }
                else//ver porque no me traeria nada
                {
                    return Json(new { pasa = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { pasa = 4, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult Eliminar(int id, int cantidad)
        {
            var encabezado = BLRecepcion.GetEncabezadoR(id);
            try
            {

               BLRecepcion.EliminarRe(id, cantidad);

                return Json(new
                {
                    msg = encabezado.DescripcionEncabezadoRecepcion,
                    funciona = true
                }, JsonRequestBehavior.AllowGet
                    );

            }
            catch (Exception ex)
            {


                return Json(new { msg = "Ocurrio un error al eliminar " + encabezado.DescripcionEncabezadoRecepcion + ": " + ex.InnerException.InnerException.Message, funciona = false }, JsonRequestBehavior.AllowGet);
            }

            
        }

    }
} 