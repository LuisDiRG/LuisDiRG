using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class RolTipoProductoController : Controller
    {
        public ActionResult Index()
        {
            //tengo el que no es DTO y lo paso a DTO
            //tengo que quitar el idRackPermitido y agregarle la descripcion

            var listaRTPDTO = BLRolTipoProducto.ListaConvertirADTO();
            return View(listaRTPDTO);

        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(RolTipoProducto RolTipoProducto)
        {
            try
            {
                if (RolTipoProducto.idRol == null)
                {  //aqui pongo cada uno si es null, aunque en la vista preferiria hacerlo, entonces veria si esta validacion es necesaria

                    ModelState.AddModelError("", "Debe ingresar una rol");
                    return View(RolTipoProducto);
                }
                else
                {
                    RolTipoProducto.Activo = true;
                    BLRolTipoProducto.Agregar(RolTipoProducto);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrio un error al agregar un  Rol");
                return View(RolTipoProducto);
            }
        }


        //controlador GET buscar
        public ActionResult GetRolTipoProducto(int id)
        {
            var RRolTP = BLRolTipoProducto.GetRolTipoProducto(id);
            var RolTPDTO = BLRolTipoProducto.UnoConvertirADTO(RRolTP);
            return View(RolTPDTO);
        }

        public ActionResult Editar(int id)
        {
            var RolTipoProducto = BLRolTipoProducto.GetRolTipoProducto(id);
            return View(RolTipoProducto);
        }

        //controlador POST editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(RolTipoProducto RolTipoProducto)
        {
            try
            {
                if (RolTipoProducto.idRolTipoProducto == null)
                {
                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el rol de tipo producto" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    BLRolTipoProducto.Editar(RolTipoProducto);
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

        public JsonResult EliminarJSON(int? id)
        {
            var rtp = BLRolTipoProducto.GetRolTipoProducto((int)id);

            return Json(rtp);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            var tp = BLRolTipoProducto.GetRolTipoProducto(id);

            try
            {
                BLRolTipoProducto.Eliminar(id);
                return Json(new { funciona = true, msg = "Se elimino" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { msg = "Ocurrio un error al eliminar " + ex.Message, funciona = false }, JsonRequestBehavior.AllowGet);
            
            }


        }




        


        /*
        public JsonResult DescripcionRack(int id)
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

        }*/
        /*
        public JsonResult DescripcionProveedor(int id)
        {
            string proveedorDescription = "";
            string message;
            try
            {
                var proveedor = BLRack.GetRack(id);

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

        }*/
    }
}