function getRacks() {


    $.ajax({
        type: "GET",
        url: '/Rack/GetRacks',
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {

                $("#idRacks").append('<option value=' + item.ID_Rack + '>' + item.DescripcionRack + '</option>');
            });
        },

        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    });



}

getRacks();


//combobox bodega
function getBodegas() {
  
    $.ajax({
        type: "GET",
        url: '/Bodega/GetBodegas',
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {
-
                $("#idBodegas").append('<option value=' + item.ID_Bodega + '>' + item.DescripcionBodega + '</option>');
                         
            });
           // QuemarDatos();
        },

        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    });
}

getBodegas();

//combobox Parametro

function getParametros() {


    $.ajax({
        type: "GET",
        url: '/Parametros/GetParametros',
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {
                
                    $("#idParametros").append('<option value=' + item.ID_Parametro + '>' + item.DescripcionParametro + '</option>');

            });
           // QuemarDatos();
        },

        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    });



}
getParametros();




function getRolTipoProductos() {


    $.ajax({
        type: "GET",
        url: '/RolTipoProducto/GetRolTipoProducto',
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {

                $("#idRolTipoProducto").append('<option value=' + item.ID_Rack + '>' + item.DescripcionRack + '</option>');
            });
        },

        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    });



}

//combobox TipoProducto
function getTiposProducto() {


    $.ajax({
        type: "GET",
        url: '/TipoProducto/GetTiposProducto',
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {

                $("#idTiposProducto").append('<option value=' + item.ID_TipoProducto + '>' + item.DescripcionTipoProducto + '</option>');
            });
          //  QuemarDatos();
        },

        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    });



}

getTiposProducto();

//combobox UM
function getUnidadesMedida() {

    console.log("Entra")

    $.ajax({
        type: "GET",
        url: '/UnidadMedida/GetUnidadesMedida',
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {

                $("#idUnidadesMedida").append('<option value=' + item.ID_UnidadMedida + '>' + item.DescripcionUnidadMedida + '</option>');
            });
        },

        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    });



}

getUnidadesMedida();
//pROVEEDOR

function getProveedor() {
    $.ajax({
        type: "GET",
        url: '/Proveedor/GetProveedores',
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {

                $("#idProveedores").append('<option value=' + item.ID_Proveedor + '>' + item.DescripcionProveedor + '</option>');
            });

        },

        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    });
}

getProveedor();

function getRoles() {

    $.ajax({
        type: "GET",
        url: '/Rol/GetRoles',
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {
                -
                    $("#idRoles").append('<option value=' + item.ID_Rol + '>' + item.DescripcionRol + '</option>');

            });
        },

        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    });

}

getRoles();

//Usuarios
function getUsuarios() {

    $.ajax({
        type: "GET",
        url: '/Usuario/GetUsuarios',
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {

                $("#idUsuarios").append('<option value=' + item.ID_Usuario + '>' + item.PrimerNombre + " " + item.PrimerApellido + '</option>');
            });
           // QuemarDatos();
        },

        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    });
}

getUsuarios();

/*
function QuemarDatos() {
    var selectUsuarios = $("#idUsuarios").find("option");
    var selectProveedor = $("#idProveedores").find("option");
    var selectTP = $("#idTiposProducto").find("option");
    var selectBodega = $("#idBodegas").find("option");
    var fecha = $("#fechaVencimiento").val("2023-12-09");
    var selectP = $("#idParametros").find("option");

    $.each(selectUsuarios, function (key, item) {
      
        if ($(item).val() == 1) {
            $(item).attr("selected", true);
        }
    });

    $.each(selectTP, function (key, item) {
      
        if ($(item).val() == 1) {
            $(item).attr("selected", true);
        }
    });

    $.each(selectProveedor, function (key, item) {
       
        if ($(item).val() == 12) {
            $(item).attr("selected", true);
        }
    });

    $.each(selectBodega, function (key, item) {
       
        if ($(item).val() == 2) {
            $(item).attr("selected", true);
        }
    });

    $.each(selectP, function (key, item) {

        if ($(item).val() == 1) {
            $(item).attr("selected", true);
        }
    });

    //AsignarParametro();//parametro

    RelacionProveedorTipoProducto();

    RelacionRolTipoProducto();
}
*/

function getUsuarios() {

    $.ajax({
        type: "GET",
        url: '/Usuario/GetUsuarios',
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {

                $("#idUsuarios").append('<option value=' + item.ID_Usuario + '>' + item.PrimerNombre + " " + item.PrimerApellido + '</option>');
            });
           // QuemarDatos();
        },

        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    });
}

