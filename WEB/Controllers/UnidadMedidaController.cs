using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class UnidadMedidaController : Controller
    {
        // GET: UnidadMedida

        public ActionResult Index()
        {
            var unidadesMedida = BLUnidadMedida.ListarUnidadMedida();
            return View(unidadesMedida);
        }
        //opciones: Mandarle a la vista la descripcion en vez del ID, despues si ocupo el ID mando la desc para obtener el ID
        //Mandarle el ID y desde la vista le cambio la desc.
        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Unidad_Medida um)
        {
            try
            {
                if (um.DescripcionUnidadMedida == null )
                {  //aqui pongo cada uno si es null, aunque en la vista preferiria hacerlo, entonces veria si esta validacion es necesaria


                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el nombre" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                     um.Activo = true;
                     BLUnidadMedida.Agregar(um);
                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet); //el json
            }
        }


        //controlador GET buscar
        public ActionResult GetUnidadMedida(int id)
        {
            var um = BLUnidadMedida.GetUnidadMedida(id);

            return View(um);
        }
        
        public ActionResult Editar(int id)
        {
            var um = BLUnidadMedida.GetUnidadMedida(id);
            return View(um);
        }

        //controlador POST editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Unidad_Medida um)
        {
            try
            {
                if (um.DescripcionUnidadMedida == null)
                {
                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el nombre de la unidad de medida" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    BLUnidadMedida.Editar(um);
                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                // ModelState.AddModelError("", "Ocurrio un error al agregar una Unidad de Medida");
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet); //el json
            }

        }

        public JsonResult GetUnidadesMedida()//pasarlo a rackDTO
        {
            try
            {
                List<Unidad_Medida> lista = BLUnidadMedida.ListarUnidadMedida();

                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                List<Unidad_Medida> lista = BLUnidadMedida.ListarUnidadMedida();


                return Json(new
                {
                    lista,
                    msg = ex.Message

                }, JsonRequestBehavior.AllowGet); ;
            }

        }
       /* [HttpPost]
        public JsonResult Eliminar(int id)
        {
            var um = BLUnidadMedida.GetUnidadMedida(id);
            try
            {
                BLUnidadMedida.Eliminar(id);

                return Json(new
                {
                    msg =  um.DescripcionUnidadMedida ,
                    funciona = true 
                }, JsonRequestBehavior.AllowGet
                    );
            }
            catch (Exception ex)
            {               
                return Json(new { msg = "Ocurrio un error al eliminar " + um.DescripcionUnidadMedida + ": " + ex.Message, funciona = false }, JsonRequestBehavior.AllowGet);
            }


        }*/
    }
}