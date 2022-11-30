using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using ET;

namespace WEB.Controllers
{
    public class InicioSesionController : Controller
    {
        // GET: InicioSesion
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InicioSesion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InicioSesion(String nombusu, String contra)
        {
            var lista = BLUsuario.ListarUsuario(); //se listan todos los usarios por medio de la BL 

            foreach(var item in lista) // para encontrar el usuario y la contrasena con la base de datos 
            {
                if(nombusu == item.NombreUsuario && contra == item.Contraseña) //parametros 
                {
                    return Json(new { ok = true, toRedirect = Url.Action("../Home/Index") }, JsonRequestBehavior.AllowGet); // si da ok, osea que se encontro, nos redirige a la pagina principal 
                }
            }
            return Json(new { ok = false }, JsonRequestBehavior.AllowGet);
        }
    }
}