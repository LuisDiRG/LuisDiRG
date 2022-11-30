using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            var proveedores = BLProveedor.ListarProveedor();
            return View(proveedores);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Proveedor prov)
        {
            try
            {
                if (prov.DescripcionProveedor == null)
                {  //aqui pongo cada uno si es null, aunque en la vista preferiria hacerlo, entonces veria si esta validacion es necesaria
                
                  
                   
                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    prov.Activo = true;
                    BLProveedor.Agregar(prov);

                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet); //el json
            }
        }

        //controlador GET buscar
        public ActionResult GetProveedor(int id)
        {
            var pv = BLProveedor.GetProveedor(id);

            return View(pv);
        }

        public ActionResult Editar(int id)
        {
            var pv = BLProveedor.GetProveedor(id);
            return View(pv);
        }

        //controlador POST editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Proveedor pr)
        {
            try
            {
                if (pr.DescripcionProveedor == null)
                {
                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el proveedor" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    BLProveedor.Editar(pr);
                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet); //el json
            }

            
        }



       
        public ActionResult Eliminar(int id)
        {

            var pv = BLProveedor.GetProveedor(id);

            try
            {
                BLProveedor.Eliminar(id);
                return Json (new {
                    msg = pv.DescripcionProveedor,
                    funciona = true
                }, JsonRequestBehavior.AllowGet
                    );
            }
            catch (Exception ex)
            {

                return Json(new { msg = "Ocurrio un error al eliminar " + pv.DescripcionProveedor + ": " + ex.Message, funciona = false }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult GetProveedores()//pasarlo a rackDTO
        {
            try
            {
                List<Proveedor> lista = BLProveedor.ListarProveedor();

                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                List<Proveedor> lista = BLProveedor.ListarProveedor();


                return Json(new
                {
                    lista,
                    msg = ex.Message

                }, JsonRequestBehavior.AllowGet); ;
            }

        }
    }
}