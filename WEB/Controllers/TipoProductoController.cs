using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace WEB.Controllers
{
    public class TipoProductoController : Controller
    {
        public ActionResult Index()
        {
            //tengo el que no es DTO y lo paso a DTO
            //tengo que quitar el idRackPermitido y agregarle la descripcion
           var listaTipoProductoDTO =  BLTipoProducto.ListaConvertirADTO();

            return View(listaTipoProductoDTO);
        }

        [HttpGet]
        public ActionResult Crear()
        {
           // var racks = BLRack.ListarRack();

           // ViewBag.Racks = racks;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Tipo_Producto tipoProducto)
        {
            try
            {
                if (tipoProducto.DescripcionTipoProducto == null || tipoProducto.StockMaximo == null || tipoProducto.StockMinimo == null || tipoProducto.ID_RackPermitido == null)
                {  //aqui pongo cada uno si es null, aunque en la vista preferiria hacerlo, entonces veria si esta validacion es necesaria


                    return Json( new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el nombre del tipo producto" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    tipoProducto.Activo = true;
                    tipoProducto.StockActual = 0;
                    tipoProducto.noRegistrados = 0;
                    BLTipoProducto.Agregar(tipoProducto);
                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(new { ok = false, msg = ex.InnerException.InnerException.Message }, JsonRequestBehavior.AllowGet); //el json
            }
        }


        //controlador GET buscar
        public ActionResult GetTipoProducto(int id)
        {
            var tipoProducto = BLTipoProducto.GetTipoProducto(id);
            var tpDTO = BLTipoProducto.UnoConvertirADTO(tipoProducto);
            return View(tpDTO);
        }

        public ActionResult Editar(int id)
        {
            var tipoProducto = BLTipoProducto.GetTipoProducto(id);
            return View(tipoProducto);
        }

        //controlador POST editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Tipo_Producto tipoProducto)
        {
            try
            {
                if (tipoProducto.ID_TipoProducto == null)
                {
                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el tipoProducto" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    BLTipoProducto.Editar(tipoProducto);
                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                // ModelState.AddModelError("", "Ocurrio un error al agregar una Unidad de Medida");
                return Json(new { ok = false, msg = ex.InnerException.InnerException.Message }, JsonRequestBehavior.AllowGet); //el json
            }

            //return View();
        }


        public JsonResult EliminarJSON(int? id)
        {
            var um = BLTipoProducto.GetTipoProducto((int)id);

            return Json(um);
        }

        
        public ActionResult Eliminar(int id)
        {
            var tp = BLTipoProducto.GetTipoProducto(id);
            try
            {
                BLTipoProducto.Eliminar(id);
                return Json(new
                {
                    msg = tp.DescripcionTipoProducto,
                    funciona = true
                }, JsonRequestBehavior.AllowGet
                     );
            }
            catch (Exception ex)
            {

                return Json(new { msg = "Ocurrio un error al eliminar " + tp.DescripcionTipoProducto + ": " + ex.Message, funciona = false }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult GetTiposProducto()//pasarlo a rackDTO
        {
            try
            {
                List<Tipo_Producto> lista = BLTipoProducto.ListarTipoProducto();

                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                List<Tipo_Producto> lista = BLTipoProducto.ListarTipoProducto();


                return Json(new
                {
                    lista,
                    msg = ex.Message

                }, JsonRequestBehavior.AllowGet); ;
            }

        }


        public JsonResult DescripcionRackPermitido(int id)
        {
            string rackDescription = "";
            string message;
            try
            {
                var rack = BLRack.GetRack(id);

                rackDescription = rack.DescripcionRack;
                message = "OK";
                return Json(
                    message,
                    rackDescription
                    );

            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(
              message,
              rackDescription
              );
            }

        }
        
        public JsonResult ValidaStock(int stockMin, int stockMax)
        {
            
            try
            {
                bool pasa = BLTipoProducto.ValidaStock(stockMin, stockMax);

                if ( pasa == true )
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
                string message = ex.Message;
                return Json(
             new { pasa = 2, message }, JsonRequestBehavior.AllowGet
              );
            }

        }

        
    }
}