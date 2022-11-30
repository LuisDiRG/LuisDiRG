using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace WEB.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol

        public ActionResult Index()
        {
            var Rol = BLRol.ListarRol();
            return View(Rol);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Rol Rl)
        {
            try
            {
                if (Rl.ID_Rol == null)
                {  //aqui pongo cada uno si es null, aunque en la vista preferiria hacerlo, entonces veria si esta validacion es necesaria

                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el nombre del proyecto" }, JsonRequestBehavior.AllowGet);
                    ModelState.AddModelError("", "Debe ingresar un nombre de un rol");
                    return View(Rl);
                }
                else
                {
                    Rl.Activo = true;
                    BLRol.Agregar(Rl);
                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet); //el json
            }
        }


        //controlador GET buscar
        public ActionResult GetRol(int id)
        {
            var Rl = BLRol.GetRol(id);

            return View(Rl);
        }

        public ActionResult Editar(int id)
        {
            var Rl = BLRol.GetRol(id);
            return View(Rl);
        }

        //controlador POST editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Rol Rl)
        {
            try
            {
                if (Rl.DescripcionRol == null)
                {
                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el rol" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    BLRol.Editar(Rl);
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

        
        public ActionResult Eliminar(int id)
        {
            var rol = BLRol.GetRol(id);
            try
            {
                BLRol.Eliminar(id);
                return Json(new
                {
                    msg = rol.DescripcionRol,
                    funciona = true
                }, JsonRequestBehavior.AllowGet
                     );
            }
            catch (Exception ex)
            {

                
                return Json(new { msg = "Ocurrio un error al eliminar " + rol.DescripcionRol + ": " + ex.Message, funciona = false }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult GetRoles()//pasarlo a rackDTO
        {
            try
            {
                List<Rol> lista = BLRol.ListarRol();

                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                List<Rol> lista = BLRol.ListarRol();


                return Json(new
                {
                    lista,
                    msg = ex.Message

                }, JsonRequestBehavior.AllowGet); ;
            }

        }


    }
}