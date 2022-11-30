using BL;
using ET;

using System;
using System.Web.Mvc;
namespace WEB.Controllers
{
    
    public class PermisoController : Controller
    {
        // GET: Permisos
        
        public ActionResult Index()
        {
           // var Permiso = BLPermiso.ListarPermiso();
           return View();
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Permisos pr)
        {
            try
            {
                if (pr.ID_Permisos == null)
                {  //aqui pongo cada uno si es null, aunque en la vista preferiria hacerlo, entonces veria si esta validacion es necesaria

                    ModelState.AddModelError("", "Debe ingresar un nombre de un Permiso");
                    return View(pr);
                }
                else
                {
                 //   pr.Activo = true;
                  //  BLPermiso.Agregar(pr);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrio un error al agregar un Permiso");
                return View(pr);
            }
        }

        /*
        //controlador GET buscar
        public ActionResult GetPermiso(int id)
        {
          //  var pr = BLPermiso.GetPermiso(id);

          //  return View(pr);
        }

        public ActionResult Editar(int id)
        {
         //   var pr = BLPermiso.GetProducto(id);
         //   return View(pr);
        }
        */
        //controlador POST editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Permisos pr)
        {
            try
            {
                if (pr.ID_Rol == null)
                {
                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el permiso" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                   // BLPermiso.Editar(pr);
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



        //public ActionResult Eliminar(int? id)
        //{

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var um = BLUnidadMedida.GetUnidadMedida(id.Value);

        //    return View(um);
        //}
        /*
        public JsonResult EliminarJSON(int? id)
        {
           // var pr = BLPermiso.GetPermiso((int)id);
         //   return Json(pr);
      }*/

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
            //    BLPermiso.Eliminar(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Ocurrio un error al eliminar un Permiso");
                return View();
            }


        }


    }
}