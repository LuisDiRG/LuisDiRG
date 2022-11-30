using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;
using DAL;
using System.Data.SqlClient;
using System.Data;
using ProyectoRGSL;

namespace BL
{
    public class BLPermiso
    {

        public void GuardarPermiso(int nOpcion, Permisos ep)
        {
            DALPermisos dp = new DALPermisos();
            dp.GuardarPermiso(nOpcion, ep);
        }

        public List<Permisos> SeleccionarOpcion(int idPerfil)
        {
            DALPermisos dp = new DALPermisos();
            return dp.SeleccionarOpcion(idPerfil);
        }

        public static List<Permisos> CargarCheckList(int idPerfil)
        {
            DALPermisos Datos = new DALPermisos();
            List<Permisos> permisos = new List<Permisos>();
            DataTable lst = Datos.CargarCheckList(idPerfil);


            for (int i = 0; i < Convert.ToInt32(lst.Rows.Count); i++)
            {
                Permisos auxP = new Permisos();
                auxP.ID_Permisos = Convert.ToInt32(lst.Rows[i].ItemArray[0]);
                auxP.Permitidos = Convert.ToBoolean(lst.Rows[i].ItemArray[1]);
                auxP.ID_Rol = Convert.ToInt32(lst.Rows[i].ItemArray[2]);
                auxP.ID_Opcion = Convert.ToInt32(lst.Rows[i].ItemArray[3]);
                
                
                if (permisos.Count == 0 && auxP.ID_Rol == idPerfil)
                {
                    permisos.Add(auxP);
                }
                else
                {
                    foreach (var per in permisos)
                    {
                        if (per.ID_Permisos != auxP.ID_Permisos && auxP.ID_Permisos == idPerfil)
                        {
                            permisos.Add(auxP);
                            break;
                        }
                    }
                }
            }

            return permisos;
        }

        
        ////Metodo booleano que recibe por parametro el boton y el idPerfil activo
        //public static Boolean BotonesPerfil(RJButton btn, int idPerfil)
        //{
        //    //Variables locales
        //    bool retVal = false;
        //    BLPermiso bp = new BLPermiso();//Instancia de la BL de permiso

        //    /*Metodo que retorma todas las opciones, en formato de lista, a las que tiene acceso el perfil
        //     y las almacena en la variable local Lista Opciones*/
        //    var LstOp = bp.SeleccionarOpcion(idPerfil);

        //    foreach (var opc in LstOp)//Se recorre la lista
        //    {
        //        //Valida donde el tag del boton, sea igual al idOpcion
        //        if (opc.IdOpcion == Convert.ToInt32(btn.Tag))
        //        {
        //            //Valida según el acceso permitido (true o false) de la opcion, si puede acceder al boton
        //            if (opc.Permitido)
        //            {
        //                retVal = true;
        //            }
        //        }
        //    }
        //    return retVal; //Retorna ese valor, si tiene acceso true o no false
        //}
    }
}
