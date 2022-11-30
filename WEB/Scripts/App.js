
//botones del sidebar
$(".btn-sidebar").click(function (event) {
    var target = $(event.target);

    if (target.is(".btn-sidebar")) {
        //estas dos lineas es para quitar el fondo azul a otro boton que se toco anteriormente
        $(".btn-primary").addClass("btn-light"); //a todos los botones, vamos a agregarle el btnlight, por si hay uno que estaba en azul
        $(".btn-primary").removeClass(".btn-primary");//ahora, si uno estaba "presionado", ya le quitamos la clase para q se vea nirmal

       
        localStorage.setItem("BotonSeleccionadoID", JSON.stringify(event.target.id));
        
    }
});



$(document).ready(function () {

    var botonID = "#" + JSON.parse(localStorage.getItem('BotonSeleccionadoID'));
    if (botonID == null) {
        console.log("Error en boton Sidebar")
    } else {

        $(botonID).removeClass("btn-light"); //al que toque, le quito el color del btn normal
        $(botonID).addClass("btn-primary");
    }
});


//para editar : TOAST

function MensajeToastIndex(desc, crud) {
    var msg = "Se " + crud + " " + desc + " correctamente"

    localStorage.setItem("msgCrearEditarEliminar", JSON.stringify(msg));


}

function MostrarToast() {

    var msg = JSON.parse(localStorage.getItem('msgCrearEditarEliminar'));
    //console.log(msg);
    if (msg == "" || msg == "undefined" || msg == null) {
        console.log("No hay nada para mostrar el toast"); //es que no se ha mandado ningun mensaje cuando se crea, actualiza o elimina algo
    } else {

        $("#toast-body").append(msg)

        $(".toast-body").append(msg) //lleno el body del toast con el mensaje de eliminado (por elmomento)

        $("#liveToast").removeClass("hide")

        $("#liveToast").addClass("show")

        setTimeout(function () {
            $("#liveToast").addClass("hide");

            $(".toast-body").empty();
        }, 5000)

        localStorage.setItem("msgCrearEditarEliminar", JSON.stringify(""));
    }
}

$(document).ready(MostrarToast());


function CerrarToast() {

    $("#liveToast").removeClass("show")
    $("#liveToast").addClass("hide")

    $("#liveToastCrear").removeClass("show");
    $("#liveToastCrear").addClass("hide")
}

function LlenarContenidoToast(msg) {

    $("#toast-body").append(msg)
}



//Eliminar MODAL

function llenarDatosModal(id, desc, functionBtn) { //hacer la tabla del q se va a eliminar
    debugger;
    let tituloModalBody = " <p> Esta seguro de eliminar " + desc + " ?</p>";//El contenido titulo del modal

    let texto = "<table class= 'table table-striped'>"; //el cuerpo del modal / la tabla
    texto += "<thead>";
    texto += "<tr>";
    texto += "<th> ID </th>";
    texto += "<th> Descripcion </th>";
    texto += "</tr>";

    texto += "</thead > ";
    texto += "<tbody>";

    texto += "<tr>";
    texto += "<td>" + id + "</th>";
    texto += "<td>" + desc + "</th>";
    texto += "</tr>";

    texto += "</tbody>";

    texto += "</table> ";



    $(".modal-body").append(tituloModalBody);//vamos a insertarle como hijo a esa clase, el contenido del titulo
    $(".modal-body").append(texto);//vamos a insertar la tabla
    hacerBotones(id, functionBtn);


}

function hacerBotones(ident, functionBtn) { //hacer los botones del moda. El tercer parametro es el nombre de la funcion de eliminar
    var buttons = "  <button type='button' class='btn btn-secondary' data-bs-dismiss='modal'>Close</button>";
    buttons += " <button type='button' class='btn btn-primary' data-bs-dismiss='modal' onclick = '" + functionBtn + "(" + ident + ");')> Eliminar</button > ";

    $(".modal-footer").append(buttons); // insertar como hijo al footer
}

function VaciarModelo() { //cuando el boton del modal "Close" se le de click, borre todo el contenido dentro dle modal
    $(".modal-body").empty();
    $(".modal-footer").empty();
}



//
//AJAX ELIMINAR
//
function Eliminar(id) {
    $.ajax({
        type: "POST", //envia un dato al controlador y de el controlador a la base de datos para poder eliminar logicamente una UM
        url: "/UnidadMedida/Eliminar/",  //es la direccion primero se pone el controlador y luego la accion 
        data: { id: id }, //parametros que ocupa la accion de el controlador de UM
        success: function (result) { //cuando ya realiza la accion que se pone en el ulr y no sale ninguna exepcion 
            if (result.funciona) {
                // Mostrar(result.msg);
                MensajeToastIndex(result.msg, "eliminó");
                window.location.href = "https://localhost:44331/UnidadMedida";
                //Url.Action("Index")
            } else {
                //  Mostrar(result.msg);
            }

        },
        error: function (data) {
            alert("Error:" + data.responseText)
        },



    });
}

//eliminar rol

function EliminarRol(id) {
    debugger;
    $.ajax({
        type: "GET",
        url: "/Rol/Eliminar/",
        data: { id: id },
        success: function (result) {
            if (result.funciona) {

                MensajeToastIndex(result.msg, "eliminó");
                window.location.href = "https://localhost:44331/Rol";

            } else {
                var msg = result.msg

                EsconderMostrarToast(msg)
            }

        },
        error: function (data) {
            alert("Error:" + data.responseText)
        },



    });
}

//eliminar roltipoproducto

function EliminarRolTipoProducto(id) {
    debugger;
    $.ajax({
        type: "GET",
        url: "/RolTipoProducto/Eliminar/",
        data: { id: id },
        success: function (result) {
            if (result.funciona) {

                MensajeToastIndex(result.msg, "eliminó");
                window.location.href = "https://localhost:44331/RolTipoProducto";

            } else {




            }

        },
        error: function (data) {
            alert("Error:" + data.responseText)
        },



    });
}




//eliminar ubicacion
function EliminarUbicacion(id) {

    $.ajax({
        type: "GET",
        url: "/Ubicacion/Eliminar/",
        data: { id: id },
        success: function (result) {
            if (result.funciona) {

                MensajeToastIndex(result.msg, "eliminó");
                window.location.href = "https://localhost:44331/Ubicacion";

            } else {
                var msg = result.msg
                debugger;
                EsconderMostrarToast(msg)
            }

        },
        error: function (data) {
            debugger;
            alert("Error:" + data.responseText)
        },



    });
}




//eliminar bodega

function EliminarBodega(id) {

    $.ajax({
        type: "GET",
        url: "/Bodega/Eliminar/",
        data: { id: id },
        success: function (result) {
            if (result.funciona) {

                MensajeToastIndex(result.msg, "eliminó");
                window.location.href = "https://localhost:44331/Bodega";

            } else {
                var msg = result.msg
                debugger;
                EsconderMostrarToast(msg)
            }

        },
        error: function (data) {
            debugger;
            alert("Error:" + data.responseText)
        },



    });
}




//eliminar rack

function EliminarRack(id) {

    $.ajax({
        type: "GET",
        url: "/Rack/Eliminar/",
        data: { id: id },
        success: function (result) {
            if (result.funciona) {

                MensajeToastIndex(result.msg, "eliminó");
                window.location.href = "https://localhost:44331/Rack";

            } else {
                var msg = result.msg
                debugger;
                EsconderMostrarToast(msg)
            }

        },
        error: function (data) {
            debugger;
            alert("Error:" + data.responseText)
        },



    });
}


function EliminarProveedor(id) {
    $.ajax({
        type: "GET",
        url: "/Proveedor/Eliminar/",
        data: { id: id },
        success: function (result) {
            if (result.funciona) {
                // Mostrar(result.msg);
                MensajeToastIndex(result.msg, "eliminó");
                window.location.href = "https://localhost:44331/Proveedor";
                //Url.Action("Index")
            } else {
                //  Mostrar(result.msg);
            }

        },
        error: function (data) {
            alert("Error:" + data.responseText)
        },



    });
}

//Tipo Producto
function EliminarTipoProducto(id) {
    $.ajax({
        type: "GET",
        url: "/TipoProducto/Eliminar/",
        data: { id: id },
        success: function (result) {
            if (result.funciona) {
                // Mostrar(result.msg);
                MensajeToastIndex(result.msg, "eliminó");
                window.location.href = "https://localhost:44331/TipoProducto";
                //Url.Action("Index")
            } else {
                //  Mostrar(result.msg);
            }

        },
        error: function (data) {
            alert("Error:" + data.responseText)
        },



    });
}
//PARAMETROS
function EliminarParametros(id) {
    $.ajax({
        type: "GET",
        url: "/Parametros/Eliminar/",
        data: { id: id },
        success: function (result) {
            if (result.funciona) {
                // Mostrar(result.msg);
                MensajeToastIndex(result.msg, "eliminó");
                window.location.href = "https://localhost:44331/Parametros";

            } else {
                var msg = "No elimino el parametro";
                EsconderMostrarToast(msg);
            }
        },
        error: function (data) {
            alert("Error:" + data.responseText)
        },

    });
}


//Producto
function EliminarProducto(id) {
    debugger;
    $.ajax({
        type: "GET",
        url: "/Productos/Eliminar/",
        data: { id: id },
        success: function (result) {
            if (result.funciona) {

                MensajeToastIndex(result.msg, "eliminó");
                window.location.href = "https://localhost:44331/Productos";

            } else {
                var msg = result.msg

                EsconderMostrarToast(msg)
            }

        },
        error: function (data) {
            alert("Error:" + data.responseText)
        },



    });
}

//Eliminar Usuario
function EliminarUsuario(id) {
    debugger;
    $.ajax({
        type: "GET",
        url: "/Usuario/Eliminar/",
        data: { id: id },
        success: function (result) {
            if (result.funciona) {

                MensajeToastIndex(result.msg, "eliminó");
                window.location.href = "https://localhost:44331/Usuario";

            } else {
                var msg = result.msg

                EsconderMostrarToast(msg)
            }

        },
        error: function (data) {
            alert("Error:" + data.responseText)
        },



    });
}

//ProvTipoProducto
function EliminarProveedorTipoProducto(id) {

    $.ajax({
        type: "GET",
        url: "/ProveedorTipoProducto/Eliminar/",
        data: { id: id },
        success: function (result) {
            if (result.funciona) {
                llenarDatosModal

                MensajeToastIndex(result.msg, "eliminó");
                window.location.href = "https://localhost:44331/ProveedorTipoProducto";

            } else {
                var msg = result.msg
                debugger;
                EsconderMostrarToast(msg)
            }

        },
        error: function (data) {
            debugger;
            alert("Error:" + data.responseText)
        },



    });
}


//
////CREAR W/ AJAX
//


////
//////Parametro
////
$("#frmParametro").submit(function (event) {

    event.preventDefault();

    //boton editar o crear
    var textoBoton = $("#btnEnviar").val();
    //variables
    var descripcion = $("#descripcion").val();
    var horarioInicio = $("#HorarioInicio").val();
    var horarioSalida = $("#HorarioSalida").val();
    var cubicajeMaximo = $("#CubicajeMaximo").val();
    debugger;
    if ($.trim(descripcion) == "" || $.trim(horarioInicio) == "" || $.trim(horarioSalida) == ""
        || $.trim(cubicajeMaximo) == "") {
        var descripcion = $("#descripcionEditar").val();
        var msg = "Faltan datos por agregar (*)"

        LlenarMensajeErrorToastCrearEditar(msg)

    } else { //todo bien

        var post_url = $(this).attr("action"); //get form action url, voy a almacenar el action que se va a ejecutar
        var request_method = $(this).attr("method"); //get form GET/POST method, el metodo que yo voy a ejecutar, el post
        var form_data = $(this).serializeArray(); //Encode form elements for submission, datos del formulario serializado, es el json
        console.log(form_data);

        $.ajax({
            url: post_url,
            type: request_method, //tipo del metodo
            data: form_data //los datos recopilados
        }).done(function (response) {

            if (response.ok) {

                if (textoBoton == "Crear") {
                    AgarrarDescripcion("creó", response.toRedirect);
                } else {
                    if (textoBoton == "Editar") {
                        AgarrarDescripcion("actualizó", response.toRedirect);
                    }
                }



            }
            else {
                debugger;
                var msg = response.msg

                EsconderMostrarToast(msg)
            }
        }).fail(function (data) {

            alert(data.responseText);
            var msg = "";
            LlenarMensajeErrorToastCrearEditar(msg)

        });

    }

})




//
////TP
//

var stockMin = false;

$("#frmTipoProducto").submit(function (event) {

    event.preventDefault();
    //
    var stockMax = 0;
    var stockMin = 0;
    //boton editar o crear
    var textoBoton = $("#btnEnviar").val();
    //variables
    var descripcion = $("#descripcion").val();
     stockMax = $("#stockMaximo").val();
    stockMin = $("#stockMinimo").val();
    var idRackPermitido = $("#idRacks").val();


    if (idRackPermitido == "undefined" || idRackPermitido == "" || idRackPermitido == null) {
        var idRackPermitido = $("#idRacksEditar").val();

    }

    console.log("Desc: " + descripcion + " stockMax: " + stockMax + " stockMin: " + stockMin + "idRack: " + idRackPermitido)

    if ($.trim(descripcion) == "" || $.trim(stockMin) == "" || $.trim(stockMin) == "" ||
        $.trim(idRackPermitido) == "") {

        var msg = "Faltan datos por agregar (*)"

        LlenarMensajeErrorToastCrearEditar(msg)

    } else { //todo bien

        ///AHORA DEBE DE PASAR POR LAS VALIDACIONES DE REGLA DE NEGOCIO:
        ValidarStockMinMaximo();

        if (pasaStockMinMax == false) {

            var msg = "El stock minimo es mayor al stock maximo";

            LlenarMensajeErrorToastCrearEditar(msg)
        } else { //ya despues de validar
            debugger;

            var post_url = $(this).attr("action"); //get form action url, voy a almacenar el action que se va a ejecutar
            var request_method = $(this).attr("method"); //get form GET/POST method, el metodo que yo voy a ejecutar, el post
            var form_data = $(this).serializeArray(); //Encode form elements for submission, datos del formulario serializado, es el json
            console.log(form_data);
            //return;

            $.ajax({
                url: post_url,
                type: request_method, //tipo del metodo
                data: form_data //los datos recopilados
            }).done(function (response) {

                if (response.ok) {

                    if (textoBoton == "Crear") {
                        AgarrarDescripcion("creó", response.toRedirect);
                    } else {
                        if (textoBoton == "Editar") {
                            AgarrarDescripcion("actualizó", response.toRedirect);
                        }
                    }



                }
                else {
                    debugger;
                    var msg = response.msg

                    EsconderMostrarToast(msg)
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {

                var msg = "Ocurrio un error en el ajax";
                LlenarMensajeErrorToastCrearEditar(msg)

            });

        }
    }
})


//metodo stockMin
function ValidarStockMinMaximo(){
   var stockMax = $("#stockMaximo").val();
    var stockMin = $("#stockMinimo").val();
    debugger;
    $.ajax({
        type: "GET",
        url: '/TipoProducto/ValidaStock',
        async: false,
        data: {
            stockMin: stockMin,
            stockMax: stockMax
            },
        dataType: "json",
        success: function (result) {

            if (result.pasa = 1) {
                pasaStockMinMax = true;
            } else {
                pasaStockMinMax = false;
            }
        },

        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    });
}

//
////UM
//

$("#frmUnidadMedida").submit(function (event) {
    event.preventDefault(); //no manda a cargar la pagina de nuevo

    var descripcion = $("#descripcion").val();
    var textoBoton = $("#btnEnviar").val(); //el valor que tiene el boton en la pagina que se esta en el momento

    if ($.trim(descripcion) == "") {
        var msg = "Faltan datos por agregar (*)"

        EsconderMostrarToast(msg);

    } else {

        var post_url = $(this).attr("action"); //get form action url, voy a almacenar el action que se va a ejecutar
        var request_method = $(this).attr("method"); //get form GET/POST method, el metodo que yo voy a ejecutar, el post
        var form_data = $(this).serializeArray(); //Encode form elements for submission, datos del formulario serializado, es el json
        console.log(form_data);
        //return;

        $.ajax({
            url: post_url,
            type: request_method, //tipo del metodo
            data: form_data //los datos recopilados
        }).done(function (response) {

            if (response.ok) {

                if (textoBoton == "Crear") {
                    AgarrarDescripcion("creó", response.toRedirect);
                } else {
                    if (textoBoton == "Editar") {
                        AgarrarDescripcion("actualizó", response.toRedirect);
                    }
                }



            }
            else {

                var msg = response.msg

                EsconderMostrarToast(msg)
            }

        }).fail(function (jqXHR, textStatus, errorThrown) {

        });




    }


})


//
////RolTipoProducto
//

$("#frmRolTipoProducto").submit(function (event) {
    event.preventDefault(); //no manda a cargar la pagina de nuevo

    var descripcion = $("#descripcionRol").val();
    var descripcion = $("#descripcionTipoProducto").val();
    var textoBoton = $("#btnEnviar").val(); //el valor que tiene el boton en la pagina que se esta en el momento

    if ($.trim(descripcion) == "") {
        var msg = "Faltan datos por agregar (*)"

        EsconderMostrarToast(msg);

    } else {

        var post_url = $(this).attr("action"); //get form action url, voy a almacenar el action que se va a ejecutar
        var request_method = $(this).attr("method"); //get form GET/POST method, el metodo que yo voy a ejecutar, el post
        var form_data = $(this).serializeArray(); //Encode form elements for submission, datos del formulario serializado, es el json
        console.log(form_data);
        //return;

        $.ajax({
            url: post_url,
            type: request_method, //tipo del metodo
            data: form_data //los datos recopilados
        }).done(function (response) {

            if (response.ok) {

                if (textoBoton == "Crear") {
                    AgarrarDescripcion("creó", response.toRedirect);
                } else {
                    if (textoBoton == "Editar") {
                        AgarrarDescripcion("actualizó", response.toRedirect);
                    }
                }



            }
            else {

                var msg = response.msg

                EsconderMostrarToast(msg)
            }

        }).fail(function (jqXHR, textStatus, errorThrown) {

        });




    }


})


//
//////
//////
//RACCKKK
//////
//////
$("#frmRack").submit(function (event) {

    event.preventDefault();
    //boton editar o crear
    var textoBoton = $("#btnEnviar").val();
    //variables
    var descripcion = $("#descripcion").val();
    var cubicajeMax = $("#CubicajeMaximo").val();
    var idBodega = $("#idBodegas").val();

    if (idBodega == "undefined" || idBodega == "" || idBodega == null) { //este es unicamente si se esta en la vista de editar
        var idBodega = $("#idBodegasEditar").val();

    }

    if ($.trim(descripcion) == "" || $.trim(cubicajeMax) == "" ||
        $.trim(idBodega) == "") {

        var msg = "Faltan datos por agregar (*)"

        LlenarMensajeErrorToastCrearEditar(msg)

    } else { //todo bien

        ///AHORA DEBE DE PASAR POR LAS VALIDACIONES DE REGLA DE NEGOCIO:

        var post_url = $(this).attr("action"); //get form action url, voy a almacenar el action que se va a ejecutar
        var request_method = $(this).attr("method"); //get form GET/POST method, el metodo que yo voy a ejecutar, el post
        var form_data = $(this).serializeArray(); //Encode form elements for submission, datos del formulario serializado, es el json
        console.log(form_data);
        //return;

        $.ajax({
            url: post_url,
            type: request_method, //tipo del metodo
            data: form_data //los datos recopilados
        }).done(function (response) {



            if (response.ok) { //esto pasa si crea

                if (response.valor == 1) { //se manda al index
                    if (textoBoton == "Crear") {
                        AgarrarDescripcion("creó", response.toRedirect);
                    } else {
                        if (textoBoton == "Editar") {
                            AgarrarDescripcion("actualizó", response.toRedirect);
                        }
                    }
                } else if (response.valor == 2) {
                    msg = "La suma de los racks de la bodega seleccionada es superior al cubicaje maximo de la bodega ";

                    LlenarMensajeErrorToastCrearEditar(msg)
                } else if (response.valor == 3) {
                    msg = "El cubicaje maximo del rack es superior al cubicaje maximo de la bodega";
                    LlenarMensajeErrorToastCrearEditar(msg)
                }
                else {
                    //msg = "No cumplio nada";
                    // LlenarMensajeErrorToastCrearEditar(msg)
                    console.log("No cumplio nada")
                }
            }
            else {
                var msg = response.msg

                EsconderMostrarToast(msg)
            }
            debugger;
            /*if (response.okEditar) { //esto pasa si edita
                debugger;
                if (textoBoton == "Crear") {
                    AgarrarDescripcion("creó", response.toRedirect);
                } else {
                    if (textoBoton == "Editar") {
                        AgarrarDescripcion("actualizó", response.toRedirect);
                    }
                }

            }*/
        }).fail(function (jqXHR, textStatus, errorThrown) {

            var msg = "Ocurrio un error en el ajax";
            LlenarMensajeErrorToastCrearEditar(msg)

        });


    }
})


//////
//////////BODEGA
/////
$("#frmBodega").submit(function (event) {

    event.preventDefault();
    //boton editar o crear
    var textoBoton = $("#btnEnviar").val();
    //variables
    var descripcion = $("#descripcion").val();
    var cubicajeMax = $("#CubicajeMaximo").val();
    var idParametro = $("#idParametros").val();

    if (idParametro == "undefined" || idParametro == "" || idParametro == null) { //este es unicamente si se esta en la vista de editar
        idParametro = $("#idParametrosEditar").val();
        debugger;
    }

    if ($.trim(descripcion) == "" || $.trim(cubicajeMax) == "" ||
        $.trim(idParametro) == "") {

        var msg = "Faltan datos por agregar (*)"

        LlenarMensajeErrorToastCrearEditar(msg)

    } else { //todo bien

        ///AHORA DEBE DE PASAR POR LAS VALIDACIONES DE REGLA DE NEGOCIO:

        var post_url = $(this).attr("action"); //get form action url, voy a almacenar el action que se va a ejecutar
        var request_method = $(this).attr("method"); //get form GET/POST method, el metodo que yo voy a ejecutar, el post
        var form_data = $(this).serializeArray(); //Encode form elements for submission, datos del formulario serializado, es el json
        console.log(form_data);
        //return;

        $.ajax({
            url: post_url,
            type: request_method, //tipo del metodo
            data: form_data //los datos recopilados
        }).done(function (response) {



            if (response.ok) { //esto pasa si crea

                if (response.valor == 1) { //se manda al index
                    if (textoBoton == "Crear") {
                        AgarrarDescripcion("creó", response.toRedirect);
                    } else {
                        if (textoBoton == "Editar") {
                            AgarrarDescripcion("actualizó", response.toRedirect);
                        }
                    }
                } else if (response.valor == 2) {
                    msg = "El espacio de las bodegas es superior al cubicaje maximo de los parametros ";

                    LlenarMensajeErrorToastCrearEditar(msg)
                } else if (response.valor == 3) {
                    msg = "El cubicaje maximo de la bodega es superior al cubicaje maximo de la bodega";
                    LlenarMensajeErrorToastCrearEditar(msg)
                }
                else {
                    //msg = "No cumplio nada";
                    // LlenarMensajeErrorToastCrearEditar(msg)
                    console.log("No cumplio nada")
                }
            }
            else {
                var msg = response.msg

                EsconderMostrarToast(msg)
            }
            debugger;
            /*if (response.okEditar) { //esto pasa si edita
                debugger;
                if (textoBoton == "Crear") {
                    AgarrarDescripcion("creó", response.toRedirect);
                } else {
                    if (textoBoton == "Editar") {
                        AgarrarDescripcion("actualizó", response.toRedirect);
                    }
                }

            }*/
        }).fail(function (jqXHR, textStatus, errorThrown) {

            var msg = "Ocurrio un error en el ajax";
            LlenarMensajeErrorToastCrearEditar(msg)

        });


    }
})

///////
////////////UBICACION
///////
$("#frmUbicacion").submit(function (event) {
    event.preventDefault(); //no manda a cargar la pagina de nuevo

   
    var textoBoton = $("#btnEnviar").val(); //el valor que tiene el boton en la pagina que se esta en el momento
    var idBodega = $("#idBodegas").val();
    var idRack = $("#idRacks").val();
    //var ocupado = $("#checkOcupado").val();

    if (idRack == "undefined" || idRack == "" || idRack == null) { //este es unicamente si se esta en la vista de editar
        var idRack = $("#idRacksEditar").val();

    }

    if (idBodega == "undefined" || idBodega == "" || idBodega == null) { //este es unicamente si se esta en la vista de editar
        var idBodega = $("#idBodegasEditar").val();

    }

    //if (ocupado == "undefined" || ocupado == false || ocupado == null) { //este es unicamente si se esta en la vista de editar
     //   var ocupado = $(#checkOcupadoEditar).val();

    //}

    if ($.trim(idBodega) == "" || $.trim(idRack) == "" || $.trim(idRack) == "") {
        var msg = "Faltan datos por agregar (*)"

        EsconderMostrarToast(msg);

    } else {

        var post_url = $(this).attr("action"); //get form action url, voy a almacenar el action que se va a ejecutar
        var request_method = $(this).attr("method"); //get form GET/POST method, el metodo que yo voy a ejecutar, el post
        var form_data = $(this).serializeArray(); //Encode form elements for submission, datos del formulario serializado, es el json
        console.log(form_data);
        //return;

        $.ajax({
            url: post_url,
            type: request_method, //tipo del metodo
            data: form_data //los datos recopilados
        }).done(function (response) {

            if (response.ok) {

                if (textoBoton == "Crear") {
                    AgarrarDescripcion("creó", response.toRedirect);
                } else {
                    if (textoBoton == "Editar") {
                        AgarrarDescripcion("actualizó", response.toRedirect);
                    }
                }



            }
            else {

                var msg = response.msg

                EsconderMostrarToast(msg)
            }

        }).fail(function (jqXHR, textStatus, errorThrown) {

        });




    }


})

//
//////ROL
//
$("#frmRol").submit(function (event) {
    event.preventDefault(); //no manda a cargar la pagina de nuevo

    var descripcion = $("#descripcion").val();
    var textoBoton = $("#btnEnviar").val(); //el valor que tiene el boton en la pagina que se esta en el momento

    console.log("SUBMIT")

    if ($.trim(descripcion) == "") {
        var msg = "Faltan datos por agregar (*)"

        EsconderMostrarToast(msg);

    } else {

        var post_url = $(this).attr("action"); //get form action url, voy a almacenar el action que se va a ejecutar
        var request_method = $(this).attr("method"); //get form GET/POST method, el metodo que yo voy a ejecutar, el post
        var form_data = $(this).serializeArray(); //Encode form elements for submission, datos del formulario serializado, es el json
        console.log(form_data);
        //return;

        $.ajax({
            url: post_url,
            type: request_method, //tipo del metodo
            data: form_data //los datos recopilados
        }).done(function (response) {

            if (response.ok) {

                if (textoBoton == "Crear") {
                    AgarrarDescripcion("creó", response.toRedirect);
                } else {
                    if (textoBoton == "Editar") {
                        AgarrarDescripcion("actualizó", response.toRedirect);
                    }
                }



            }
            else {

                var msg = response.msg

                EsconderMostrarToast(msg)
            }

        }).fail(function (data) {
            // console.log(data.)
        });




    }


})

//
///Proveedor
//


$("#frmProveedor").submit(function (event) {

    event.preventDefault();//que la pagina no se recargues

    //boton editar o crear
    var textoBoton = $("#btnEnviar").val();
    //variables
    var descripcion = $("#descripcion").val();



    console.log("Desc: " + descripcion)

    if ($.trim(descripcion) == "") {
        debugger;
        var msg = "Faltan datos por agregar (*)"

        LlenarMensajeErrorToastCrearEditar(msg)

    } else { //todo bien
        debugger;
        var post_url = $(this).attr("action"); //get form action url, voy a almacenar el action que se va a ejecutar
        var request_method = $(this).attr("method"); //get form GET/POST method, el metodo que yo voy a ejecutar, el post
        var form_data = $(this).serializeArray(); //Encode form elements for submission, datos del formulario serializado, es el json
        console.log(form_data);
        debugger;

        $.ajax({
            url: post_url,
            type: request_method, //tipo del metodo
            data: form_data //los datos recopilados
        }).done(function (response) {

            if (response.ok) {

                if (textoBoton == "Crear") {
                    AgarrarDescripcion("creó", response.toRedirect);
                } else {
                    if (textoBoton == "Editar") {
                        AgarrarDescripcion("actualizó", response.toRedirect);
                    }
                }
            }
            else {

                var msg = response.msg

                EsconderMostrarToast(msg)
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {

            var msg = "Ocurrio un error en el ajax";
            LlenarMensajeErrorToastCrearEditar(msg)

        });

    }

})
//
////Usuario
//


$("#frmUsuario").submit(function (event) {

    event.preventDefault();
    debugger;

    //boton editar o crear
    var textoBoton = $("#btnEnviar").val();
    //variables

    var post_url = $(this).attr("action"); //get form action url, voy a almacenar el action que se va a ejecutar
    var request_method = $(this).attr("method"); //get form GET/POST method, el metodo que yo voy a ejecutar, el post
    var form_data = $(this).serializeArray(); //Encode form elements for submission, datos del formulario serializado, es el json
    console.log(form_data);
    debugger;

    $.ajax({
        url: post_url,
        type: request_method, //tipo del metodo
        data: form_data //los datos recopilados
    }).done(function (response) {

        if (response.ok) {

            if (textoBoton == "Crear") {
                AgarrarDescripcion("creó", response.toRedirect);
            } else {
                if (textoBoton == "Editar") {
                    AgarrarDescripcion("actualizó", response.toRedirect);
                }
            }
        }
        else {

            var msg = response.msg

            EsconderMostrarToast(msg)
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {

        var msg = "Ocurrio un error en el ajax";
        LlenarMensajeErrorToastCrearEditar(msg)

    });



})


////Usuario

$("#frmProducto").submit(function (event) {

    event.preventDefault();

    //boton editar o crear
    var textoBoton = $("#btnEnviar").val();
    //variables
    var descripcion = $("#descripcion").val();
    var DMV = $("#DMV").val();
    var Cubicaje = $("#Cubicaje").val();
    var Empaque = $("#Empaque").val();
    var CostoBruto = parseInt($("#CostoBruto").val());
    var Descuento = parseInt($("#Descuento").val());
    var IVA = parseInt($("#IVA").val());
    var CostoNeto = parseInt($("#CostoNeto").val());
    var idTipoProducto = $("#idTiposProducto").val();
    var idUnidadesMedida = $("#idUnidadesMedida").val();


    debugger;
    if (idTipoProducto == "undefined" || idTipoProducto == "" || idTipoProducto == null || idUnidadesMedida == "undefined" || idUnidadesMedida == "" || idUnidadesMedida == null) {
        var idTipoProducto = $("#idTiposProductoEditar").val();
        var idUnidadesMedida = $("#idUnidadesMedidaEditar").val();
        debugger;
    }


    debugger;
    if ($.trim(descripcion) == "" || $.trim(DMV) == "" || $.trim(Cubicaje) == "" ||
        $.trim(Empaque) == "" || $.trim(CostoBruto) == "" || $.trim(Descuento) == "" || $.trim(IVA) == "" || $.trim(CostoNeto) == "" || $.trim(idTipoProducto) == ""
        || $.trim(idUnidadesMedida) == "") {

        var msg = "Faltan datos por agregar (*)"

        LlenarMensajeErrorToastCrearEditar(msg)

    } else { //todo bien



        var post_url = $(this).attr("action"); //get form action url, voy a almacenar el action que se va a ejecutar
        var request_method = $(this).attr("method"); //get form GET/POST method, el metodo que yo voy a ejecutar, el post
        var form_data = $(this).serializeArray(); //Encode form elements for submission, datos del formulario serializado, es el json
        console.log(form_data);
        debugger;

        $.ajax({
            url: post_url,
            type: request_method, //tipo del metodo
            data: form_data //los datos recopilados
        }).done(function (response) {

            if (response.ok) {

                if (textoBoton == "Crear") {
                    AgarrarDescripcion("creó", response.toRedirect);
                } else {
                    if (textoBoton == "Editar") {
                        AgarrarDescripcion("actualizó", response.toRedirect);
                    }
                }
            }
            else {
                debugger;
                var msg = response.msg

                EsconderMostrarToast(msg)
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {

            var msg = "Ocurrio un error en el ajax";
            LlenarMensajeErrorToastCrearEditar(msg)

        });


    }
})

//
///ProveedorTipo Producto
//



$(document).ready(function () {

    var Descuento = $("#Descuento").val() * 100;
    var IVA = $("#IVA").val() * 100;

    $("#IVA").val(IVA)
    $("#Descuento").val(Descuento)
});

//////
///////KEYUP / KEYDOWN
/////
$("#IVA, #Descuento, #CostoBruto ").keydown(function () {


    var CostoBruto = $("#CostoBruto").val() / 1;
    var Descuento = $("#Descuento").val() / 100;
    var IVA = $("#IVA").val() / 100;
    var CostoNeto = $("#CostoNeto");

    var CostoBrutoInt = parseInt(CostoBruto);
    var DescuentoInt = parseInt(Descuento);
    var IVAInt = parseInt(IVA)
    console.log(DescuentoInt)
    console.log(CostoBrutoInt)
    console.log(IVAInt)

    if (CostoBruto != "" && Descuento != "" || IVA != "") {
        CostoNeto.val(CostoBrutoInt + (CostoBrutoInt * Descuento) + (CostoBrutoInt * IVA));
    }


})

$("#IVA, #Descuento, #CostoBruto ").keyup(function () {

    var CostoBruto = $("#CostoBruto").val() / 1;
    var Descuento = $("#Descuento").val() / 100;
    var IVA = $("#IVA").val() / 100;
    var CostoNeto = $("#CostoNeto");

    var CostoBrutoInt = parseInt(CostoBruto);
    var DescuentoInt = parseInt(Descuento);
    var IVAInt = parseInt(IVA)
    console.log(DescuentoInt)
    console.log(CostoBrutoInt)
    console.log(IVAInt)

    if (CostoBruto != "" && Descuento != "" || IVA != "") {
        CostoNeto.val(CostoBrutoInt + (CostoBrutoInt * Descuento) + (CostoBrutoInt * IVA));
    }


})


function AgarrarDescripcion(crud, toRedirect) { //crud me refiero si es crear o editar nada mas

    var desc = $("#descripcion").val();

    MensajeToastIndex(desc, crud);

    window.location.href = toRedirect;
}


//
////////////ACORTAR METODOS
//
function EsconderMostrarToast(msg) {
    $("#liveToastCrear .toast-body").append(msg)

    $("#liveToastCrear").removeClass("hide")

    $("#liveToastCrear").addClass("show")

    setTimeout(function () {
        $("#liveToastCrear").addClass("hide");

        $("#liveToastCrear #toast-bodyCrearUM").empty();
    }, 5000)
}

function LlenarMensajeErrorToastCrearEditar(msg) {
    $("#liveToastCrear .toast-body").append(msg)

    $("#liveToastCrear").removeClass("hide")

    $("#liveToastCrear").addClass("show")

    setTimeout(function () {
        $("#liveToastCrear").addClass("hide");

        $("#liveToastCrear .toast-body").empty();
    }, 5000)
}




///LOCALSTORAGE




$(document).ready(function () {

    var usuario = JSON.parse(localStorage.getItem('usuario'));

    var nombreUsuario = $("#nombreUsuario") //Es donde se inserta el nombre usuario

    $(nombreUsuario).html(usuario); //es el html donde se inserto el nombre usuario
})














/////////////////////
////////////////////////////////////////////REGLAS DE NEGOCIO
//////////////////////////
/*
function ValidarStockMinimoMaximo(stockMin, stockMax) {

    if (stockMin > stockMax) {

        return false;
    } else {

        return true;
    }
}*/



function ValidadCubicajeMaximoRackConBodega(CubicajeMaximo, CubicajeBodega) {

    console.log(typeof CubicajeMaximo);
    if (parseInt(CubicajeMaximo) > parseInt(CubicajeBodega)) {
        debugger;
        return false;
    } else {
        debugger;
        return true;
    }


}


////////////////////////////////ELIMINAR RECEPCION


function EliminarRecepcion(id) {

    cantidadProdsEliminar = 0;
    $("#tRecepcion tbody tr").each(function () {

        if ($.trim($(this).html()) != "") {
            cantidadProdsEliminar++;
        }

    })
    debugger;
    $.ajax({
        type: "GET",
        url: "/Recepcion/Eliminar/",
        data: {
            id: id,
            cantidad: cantidadProdsEliminar
        },
        success: function (result) {
            if (result.funciona) {

                MensajeToastIndex(result.msg, "eliminó");
                window.location.href = "https://localhost:44331/Recepcion";

            } else {
                var msg = result.msg

                EsconderMostrarToast(msg)
            }

        },
        error: function (data) {
            alert("Error:" + data.responseText)
        },



    });
}

