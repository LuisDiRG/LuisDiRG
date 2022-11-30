using BL;
using ET;
using System;
using System.Web.Mvc;
namespace WEB.Controllers
{
    
    public class ProductosController : Controller
    {
        // GET: Productos

        public ActionResult Index()
        {
            var Productos = BLProducto.ListaConvertirADTO();
            return View(Productos);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Producto pr)
        {
            try
            {
                if (pr.DescripcionProducto == "")
                {  //aqui pongo cada uno si es null, aunque en la vista preferiria hacerlo, entonces veria si esta validacion es necesaria

                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //variables que en la vista no se le asigna
                    pr.IVA = pr.IVA / 100;
                    pr.Descuento = pr.Descuento / 100;
                    pr.Fecha_Alta = DateTime.Now;
                    pr.Activo = true;
                    BLProducto.Guardar(1,pr);
                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet); //el json
            }
        }


        //controlador GET buscar
        public ActionResult GetProducto(int id)
        {
            var pr = BLProducto.GetProducto(id);
            var prDTO = BLProducto.UnoConvertirADTO(pr);
            return View(prDTO);
        }

        public ActionResult Editar(int id)
        {
            var pr = BLProducto.GetProducto(id);
            return View(pr);
        }

        //controlador POST editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Producto pr)
        {
            try
            {
                if (pr.ID_Producto == null)
                {
                    {
                        return Json(new { ok = false, toRedirect = Url.Action("Index") });
                    }
                 }
                else
                {
                    pr.IVA = pr.IVA / 100;
                    pr.Descuento = pr.Descuento / 100;
                    BLProducto.Guardar(2,pr);
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
            var pr = BLProducto.GetProducto(id);

            try
            {
                if (pr.ID_Ubicacion == null)
                {
                    BLProducto.Eliminar(id, pr.ID_TipoProducto,0 );
                }
                else
                {
                    BLProducto.Eliminar(id, pr.ID_TipoProducto, (int)pr.ID_Ubicacion );
                }

               
                return Json(new
                {
                    msg = pr.DescripcionProducto,
                    funciona = true
                }, JsonRequestBehavior.AllowGet
                     );
            }
            catch (Exception ex)
            {

                return Json(new { msg = "Ocurrio un error al eliminar " + pr.DescripcionProducto + ": " + ex.Message, funciona = false }, JsonRequestBehavior.AllowGet);
            }


        }


    }
}