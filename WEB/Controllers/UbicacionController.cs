using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BL;
using ET;
namespace WEB.Controllers
{
    public class UbicacionController : Controller
    {
        // GET: Ubicacion

        public ActionResult Index()
        {
            var UbicacionesDTO = BLUbicacion.ListaConvertirADTO();
            return View(UbicacionesDTO);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Ubicacion ub)
        {
            try
            {
                if (ub.ID_Rack == null)
                {  //aqui pongo cada uno si es null, aunque en la vista preferiria hacerlo, entonces veria si esta validacion es necesaria


                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el nombre del proyecto" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ub.Ocupado = false;
                    ub.Activo = true;
                    BLUbicacion.Agregar(ub);
                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet); //el json
            }
        }

        //controlador GET buscar
        public ActionResult GetUbicacion(int id)
        {
            var ubi = BLUbicacion.GetUbicacion(id);

            return View(ubi);
        }

        public ActionResult Editar(int id)
        {
            var ubi = BLUbicacion.GetUbicacion(id);
            return View(ubi);
        }

        //controlador POST editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Ubicacion ub)
        {
            try
            {
                if (ub.ID_Ubicacion == null)
                {
                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el rol" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    BLUbicacion.Editar(ub);
                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
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
            var ub = BLUbicacion.GetUbicacion(id);
            try
            {
                BLUbicacion.Eliminar(id);

                return Json(new
                {
                    msg = ub.Ocupado,
                    funciona = true
                }, JsonRequestBehavior.AllowGet
                    );
            }
            catch (Exception ex)
            {


                return Json(new { msg = "Ocurrio un error al eliminar " + ub.Ocupado + ": " + ex.Message, funciona = false }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult GetUbicaciones()//pasarlo a ubicacionDTO
        {
            try
            {
                List<Ubicacion> lista = BLUbicacion.ListarUbicacion();

                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                List<Ubicacion> lista = BLUbicacion.ListarUbicacion();


                return Json(new
                {
                    lista,
                    msg = ex.Message

                }, JsonRequestBehavior.AllowGet); ;
            }

        }

    }
}