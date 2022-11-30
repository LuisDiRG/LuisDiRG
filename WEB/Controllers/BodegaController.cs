using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace WEB.Controllers
{
    public class BodegaController : Controller
    {
        // GET: Bodega

        public ActionResult Index()
        {
            var listaBodegaDTO = BLBodega.ListaConvertirADTO();
            return View(listaBodegaDTO);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Bodega Bg)
        {
            try
            {
                if (Bg.DescripcionBodega == null)
                {  //aqui pongo cada uno si es null, aunque en la vista preferiria hacerlo, entonces veria si esta validacion es necesaria

                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el nombre de la bodega" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Bg.Activo = true;
                    Bg.CubicajeAcumulado = 0;
                    //variable para mensaje
                    int valor = BLBodega.Agregar(1, Bg);

                    return Json(new { ok = true, toRedirect = Url.Action("Index"), valor = valor }, JsonRequestBehavior.AllowGet);
                    //EL OK PUEDE SER INT, PORQUE CUANDO EL AJAX SE EJECUTE, VAMOS A PONER QUE DEPENDIENDO DEL VALOR DE LA VARIABLE, OK, LE MANDE UN VALOR AL TOAST DIFERENTE, QUE ESE MENSAJE VA A HACER EL DE LA VARIABLE MSG DEL JASON QUE SE LE PAS
                }
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet); //el json
            }
        }


        //controlador GET buscar
        public ActionResult GetBodega(int id)
        {
            var Bg = BLBodega.GetBodega(id);
            var BgDTO = BLBodega.UnoConvertirADTO(Bg);
            return View(BgDTO);
        }

        public ActionResult Editar(int id)
        {
            var bg = BLBodega.GetBodega(id);
            return View(bg);
        }

        //controlador POST editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Bodega bg)
        {
            try
            {
                if (bg.DescripcionBodega == null)
                {
                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar la bodega" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // BLRack.Editar(Rk);
                    int valor = BLBodega.Agregar(2, bg);
                    return Json(new { ok = true, toRedirect = Url.Action("Index"), valor = valor }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                // ModelState.AddModelError("", "Ocurrio un error al agregar una Unidad de Medida");
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet); //el json
            }

            //return View();
        }


        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                BLBodega.Eliminar(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Ocurrio un error al eliminar una Bodega");
                return View();
            }


        }

        public JsonResult GetBodegas()//pasarlo a bodegaDTO
        {
            try
            {
                List<Bodega> lista = BLBodega.ListarBodega();

                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                List<Bodega> listaBodegas = BLBodega.ListarBodega();
               

                return Json(new
                {
                    listaBodegas,
                    msg = ex.Message

                }, JsonRequestBehavior.AllowGet); ;
            }

        }

        public JsonResult GetBodegaJSON(int idTipoProducto)//llamo a id bodega para aqui tener su idParametro. Asi no tengo que hacer dos ajax calls
        {
            try
            {
                var tp = BLTipoProducto.GetTipoProducto(idTipoProducto);
                var rack = BLRack.GetRack(tp.ID_RackPermitido);
                var bodega = BLBodega.GetBodega(rack.ID_Bodega);

                return Json(new
                {
                    pasa = true,
                    bodega
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var tp = BLTipoProducto.GetTipoProducto(idTipoProducto);
                var rack = BLRack.GetRack(tp.ID_RackPermitido);
                var bodega = BLBodega.GetBodega(rack.ID_Rack);

                return Json(new
                {
                    pasa = false,
                    bodega,
                    msg = ex.Message

                }, JsonRequestBehavior.AllowGet); ;
            }

        }


    }
}