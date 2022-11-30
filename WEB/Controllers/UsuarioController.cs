using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            var listaUsuarioDTO = BLUsuario.ListaConvertirADTO();
            return View(listaUsuarioDTO);
        }

        [HttpGet]
        public ActionResult Crear()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Usuario usu)
        {
            try
            {
                if (usu.NombreUsuario == null)
                {  //aqui pongo cada uno si es null, aunque en la vista preferiria hacerlo, entonces veria si esta validacion es necesaria

                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el nombre de usuario del usuario" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    usu.Activo = true;
                    
                    //variable para mensaje
                  BLUsuario.Agregar(1, usu);

                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                    //EL OK PUEDE SER INT, PORQUE CUANDO EL AJAX SE EJECUTE, VAMOS A PONER QUE DEPENDIENDO DEL VALOR DE LA VARIABLE, OK, LE MANDE UN VALOR AL TOAST DIFERENTE, QUE ESE MENSAJE VA A HACER EL DE LA VARIABLE MSG DEL JASON QUE SE LE PAS
                }
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.Message}, JsonRequestBehavior.AllowGet); //el json
            }
        }



        //controlador GET buscar
        public ActionResult GetUsuario(int id)
        {
            var Rk = BLUsuario.GetUsuario(id);
            var RkDTO = BLUsuario.UnoConvertirADTO(Rk);
            return View(RkDTO);
        }

        public ActionResult Editar(int id)
        {
            var Rk = BLUsuario.GetUsuario(id);
            return View(Rk);
        }

        //controlador POST editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Usuario usu)
        {
            try
            {
                if (usu.NombreUsuario == null)
                {
                    return Json(new { ok = false, toRedirect = Url.Action("Index"), message = "Debe de ingresar el Usuario" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // BLRack.Editar(Rk);
                  BLUsuario.Agregar(2, usu);
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
            var usu = BLUsuario.GetUsuario(id);
            try
            {

                BLUsuario.Eliminar(usu.ID_Usuario);

                //el eliminar no ?? o que compile?
                return Json(new
                {
                    msg = usu.NombreUsuario,
                    funciona = true
                }, JsonRequestBehavior.AllowGet
                    );

            }
            catch (Exception ex)
            {


                return Json(new { msg = "Ocurrio un error al eliminar " + usu.NombreUsuario + ": " + ex.InnerException.InnerException.Message, funciona = false }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult GetUsuarios()
        {
            try
            {
                List<Usuario> lista = BLUsuario.ListarUsuario();

                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                List<Usuario> lista = BLUsuario.ListarUsuario();
                return Json(new
                {
                    lista,
                    msg = ex.Message

                }, JsonRequestBehavior.AllowGet); ;
            }

        }

    }
}