using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class RackController : Controller
    {
        // GET: Rack
        public ActionResult Index()
        {
            var listaRackDTO = BLRack.ListaConvertirADTO();
            return View(listaRackDTO);
        }

        [HttpGet]
        public ActionResult Crear()
        {

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Rack Rk)
        {
            try
            {
                if (Rk.DescripcionRack == null)
                {  //aqui pongo cada uno si es null, aunque en la vista preferiria hacerlo, entonces veria si esta validacion es necesaria

                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el nombre del RACK" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Rk.Activo = true;
                    Rk.CubicajeAcumulado = 0;
                    
                    //variable para mensaje
                    int valor = BLRack.Agregar(1,Rk);
                    
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
        public ActionResult GetRack(int id)
        {
            var Rk = BLRack.GetRack(id);
            var RkDTO = BLRack.UnoConvertirADTO(Rk);
            return View(RkDTO);
        }

        public ActionResult Editar(int id)
        {
            var Rk = BLRack.GetRack(id);
            return View(Rk);
        }

        //controlador POST editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Rack Rk)
        {
            try
            {
                if (Rk.DescripcionRack == null)
                {
                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el rack" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // BLRack.Editar(Rk);
                    int valor = BLRack.Agregar(2, Rk);
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

        public JsonResult Eliminar(int id)
        {
            var rk = BLRack.GetRack(id);
            try
            {

                BLRack.Eliminar(id, rk.ID_Bodega, rk.CubicajeMaximo);
             
               //el eliminar no ?? o que compile?
                return Json(new
                {
                    msg = rk.DescripcionRack,
                    funciona = true
                }, JsonRequestBehavior.AllowGet
                    );

            }
            catch (Exception ex)
            {


                return Json(new { msg = "Ocurrio un error al eliminar " + rk.DescripcionRack + ": " + ex.InnerException.InnerException.Message, funciona = false }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult GetRacks()//pasarlo a rackDTO
        {
            try
            {
                List<Rack> lista = BLRack.ListarRack();

                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                List<Rack> lista = BLRack.ListarRack();
               

                return Json(new
                {
                    lista,
                    msg = ex.Message

                }, JsonRequestBehavior.AllowGet); ;
            }

        }

    }
}