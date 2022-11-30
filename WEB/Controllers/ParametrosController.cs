using BL;
using ET;

using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Mvc;
namespace WEB.Controllers
{
    public class ParametrosController : Controller
    {
        

        public ActionResult Index()
        {
            var listaParametros = BLParametros.ListarParametros();
            return View(listaParametros);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Parametro pm)
        {
            try
            {
                if (pm.DescripcionParametro == null)
                {  //aqui pongo cada uno si es null, aunque en la vista preferiria hacerlo, entonces veria si esta validacion es necesaria


                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el nombre del parametro" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    pm.Activo = true;
                    BLParametros.Agregar(pm);
                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet); //el json
            }
        }


        //controlador GET buscar
        public ActionResult GetParametro(int id)
        {
            var Pm = BLParametros.GetParametro(id);
            return View(Pm);
        }

        public ActionResult Editar(int id)
        {
            var Pm = BLParametros.GetParametro(id);
            return View(Pm);
        }

        //controlador POST editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Parametro pm)
        {
            try
            {
                if (pm.CubicajeMaximo == null)
                {
                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar un cubicaje maximo" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    BLParametros.Editar(pm);
                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                // ModelState.AddModelError("", "Ocurrio un error al agregar un Parametro");
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet); //el json
            }

            //return View();
        }


        public JsonResult Eliminar(int id)
        {
            var pm = BLParametros.GetParametro(id);
            try
            {
                BLParametros.Eliminar(id);

                return Json(new
                {
                    msg = pm.DescripcionParametro,
                    funciona = true
                }, JsonRequestBehavior.AllowGet
                    );
            }
            catch (Exception ex)
            {


                return Json(new { msg = "Ocurrio un error al eliminar " + pm.DescripcionParametro + ": " + ex.Message, funciona = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetParametros()//pasarlo
        {
            try
            {
                List<Parametro> lista = BLParametros.ListarParametros();

                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                List<Parametro> listaParametros = new List<Parametro>();


                return Json(new
                {
                    listaParametros,
                    msg = ex.Message

                }, JsonRequestBehavior.AllowGet); ;
            }

        }

        public JsonResult GetParametroJSON(int idBodega)//llamo a id bodega para aqui tener su idParametro. Asi no tengo que hacer dos ajax calls
        {
            try
            {
                var bodega = BLBodega.GetBodega(idBodega);
                var parametro = BLParametros.GetParametro(bodega.ID_Parametros);

                return Json(new
                {
                    pasa = true,
                    parametro
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                var bodega = BLBodega.GetBodega(idBodega);
                var parametro = BLParametros.GetParametro(bodega.ID_Parametros);

                return Json(new
                {
                    pasa = false,
                    parametro,
                    msg = ex.Message

                }, JsonRequestBehavior.AllowGet); ;
            }

        }


    }
}