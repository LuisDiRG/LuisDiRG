using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class ProveedorTipoProductoController : Controller
    {
        // GET: ProveedorTipoProducto
        public ActionResult Index()
        {
            var listaProvTP = BLProveedor_TipoProducto.ListaConvertirADTO();
            return View(listaProvTP);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(ProveedorTipoProducto ptp)
        {
            try
            {
                if (ptp.idProveedorTipoProducto == null)
                {  //aqui pongo cada uno si es null, aunque en la vista preferiria hacerlo, entonces veria si esta validacion es necesaria


                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el nombre del proyecto" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ptp.Activo = true;
                    bool pasa =BLProveedor_TipoProducto.Agregar(ptp);
                    if (pasa == true)
                    {
                        return Json(new { ok = 1, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);//todo bien
                    }
                    else
                    {
                        return Json(new { ok = 2, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet); //una relacion ya existia
                    }

                    
                    
                }
            }
            catch (Exception ex)
            {
                return Json(new { ok = 3, msg = ex.Message }, JsonRequestBehavior.AllowGet); //exception
            }
        }


        //controlador GET buscar
        public ActionResult GetProveedorTipoProducto(int id)
        {
            var ptp = BLProveedor_TipoProducto.GetProveedorTipoProducto(id);
            var ptpDTO = BLProveedor_TipoProducto.UnoConvertirADTO(ptp);
            return View(ptpDTO);
        }

        public ActionResult Editar(int id)
        {
            var ptp = BLProveedor_TipoProducto.GetProveedorTipoProducto(id);
            return View(ptp);
        }

        //controlador POST editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ProveedorTipoProducto ptp)
        {
            try
            {
                if (ptp.idProveedorTipoProducto == null)
                {
                    return Json(new { ok = 3, toRedirect = Url.Action("Index"), message = "Debe de ingresar el nombre de la unidad de medida" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    bool pasa = BLProveedor_TipoProducto.Editar(ptp);
                    if (pasa == true)
                    {
                        return Json(new { ok = 1, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);//todo bien
                    }
                    else
                    {
                        return Json(new { ok = 2, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet); //una relacion ya existia
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { ok = 3, msg = ex.Message }, JsonRequestBehavior.AllowGet); //exception
            }

        }


        public JsonResult GetProveedoresTipoProducto()//pasarlo a rackDTO
        {
            try
            {
                List<ProveedorTipoProducto> lista = BLProveedor_TipoProducto.ListarProveedorTipoProducto();

                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                List<ProveedorTipoProducto> lista = BLProveedor_TipoProducto.ListarProveedorTipoProducto();


                return Json(new
                {
                    lista,
                    msg = ex.Message

                }, JsonRequestBehavior.AllowGet); ;
            }

        }

        public JsonResult GetProveedoresTipoProductoDTO()//pasarlo a rackDTO
        {
            try
            {
                List<ProveedorTipoProductoDTO> lista = BLProveedor_TipoProducto.ListaConvertirADTO();

                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                List<ProveedorTipoProductoDTO> lista = BLProveedor_TipoProducto.ListaConvertirADTO();


                return Json(new
                {
                    lista,
                    msg = ex.Message

                }, JsonRequestBehavior.AllowGet); ;
            }

        }

        public JsonResult Eliminar(int id)
        {
            var ptp = BLProveedor_TipoProducto.GetProveedorTipoProducto(id);
            var tp = BLTipoProducto.GetTipoProducto(ptp.idTipoProducto);
            var prov = BLProveedor.GetProveedor(ptp.idProveedor);
            try
            {
                BLProveedor_TipoProducto.Eliminar(id);

                return Json(new
                {
                    msg = tp.DescripcionTipoProducto + " y " + prov.DescripcionProveedor,
                    funciona = true
                }, JsonRequestBehavior.AllowGet
                    );
            }
            catch (Exception ex)
            {

                return Json(new { msg = "Ocurrio un error al eliminar " + tp.DescripcionTipoProducto + " y " + prov.DescripcionProveedor + ": " + ex.Message, funciona = false }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}