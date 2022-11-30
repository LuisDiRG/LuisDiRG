//
//variables globales
//
var pasaProvTP = false;
var pasaRolTP = false;
var bodegaPar = false;
var TPBodega = false;
var fechaBien = false;
//

//
////Validaciones
//


var pasaDMV = false;
var pasaFechaParametro = false;
var pasaStockMinMax = false;
var cantidadProds = 0;
var lista = [];
var encabezadoR = {//el objeto completo
        DescripcionEncabezadoRecepcion: "",
        FechaIngreso: "",
        ID_Usuario: 0,
        ID_Bodega: 0,
        ID_Proveedor: 0,
        fechaVencimiento: "",
        ID_TipoProducto: 0,
        ID_Parametro: 0
    }
var pasaUbicacionesProductos = false;
function VerificarDMV() {

    var fechaProd = $(this).parent().parent().find(".fechaAlta").html();//boton de agregar
    var DMV = $(this).parent().parent().find(".DMV").html();
    var fechaActual = $("#fechaIngreso").val();
    var fechaVencimiento = $("#fechaVencimiento").val();

    $.ajax({
        type: "GET",
        url: '/Recepcion/VerificarDMV',
        async: false, //como el codigo no terminaba de ejecutar cuando ocupaba que me asignara la variable, tengo q quitarle la asincronia
        data: {
            DMV: DMV,
            fechaInicio: fechaActual,
            fechaVencimiento: fechaVencimiento
        },
        dataType: "json",
        success: function (result) {

            if (result.pasa == 1) {//todo bien

                pasaDMV = true;
            } else if (result.pasa == 2) {
                pasaDMV = false;
            } else {
                var msg = result.msg;
                EsconderMostrarToast(msg);

            }
        },
        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    });
}

function ValidarFechaParametroConFechaActual() {

    var fechaActual = $("#fechaIngresoEscondido").val();
    var idParametro = $("#idParametros").val();

    $.ajax({
        type: "GET",
        url: '/Recepcion/ValidarFechaParametroConFechaActual',
        async: false, //como el codigo no terminaba de ejecutar cuando ocupaba que me asignara la variable, tengo q quitarle la asincronia
        data: {
            fechaActual: fechaActual,
            idParametro: idParametro,
        },
        dataType: "json",
        success: function (result) {

            if (result.pasa == 1) {//todo bien

                pasaFechaParametro = true;
            } else if (result.pasa == 2) {
                pasaFechaParametro = false;
            } else {
                var msg = result.msg;
                EsconderMostrarToast(msg);

            }
        },
        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    })

}


//si la fecha de V es menor a la actual
$("#fechaVencimiento").change(function () {

    var FV = $(this).val();
    var FI = $("#fechaIngreso").val();

    if (FV > FI) {
        fechaBien = true;
    } else if (FV == FI) {
        fechaBien = true;
    } else {
        var msg = "La fecha de Vencimiento es menor a la actual"

        EsconderMostrarToast(msg);
        fechaBien = false;
    }
})

function ValidarStockMinMax() {

    var idTipoProducto = $("#idTiposProducto").val();

    $.ajax({
        type: "GET",
        url: '/Recepcion/ValidarStockMaximo',
        async: false,
        data: {
            idTipoProducto: idTipoProducto,
            cantidad: cantidadProds
        },
        dataType: "json",
        success: function (result) {

            if (result.pasa == 1) {

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

function IngresarUbicacionAProductos() {
    //ocupo la lista , desc bodega y proveedor

    idBodega = $("#ID_Bodega").val();
    idProveedor = $("#idProveedores").val();
    lista2 = lista;
    debugger;
    $.ajax({
        type: "POST",
        url: '/Recepcion/IngresarUbicacionAProductos',
        async: false,
        data: {
            idBodega: idBodega,
            idProveedor: idProveedor,
            listaProductos: lista2
        },
        dataType: "json",
        success: function (result) {

            if (result.pasa == 1) {

                pasaUbicacionesProductos = true;
                console.log("Todo bien con la ubicacion")
            } else if (result.pasa == 2) {
                var msg = "No alcanza tood el cubicaje de los productos";

                EsconderMostrarToast(msg)
              
                pasaUbicacionesProductos = false;
            } else if (result.pasa == 3) {
                var msg = "No hay ubicaicones suficientes";
                EsconderMostrarToast(msg)
                console.log("No hay ubicaicones suficientes")
                pasaUbicacionesProductos = false;
            } else {
                console.log("No hizo nada")
            }
        },

        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    });

}
//
//
//
//
//





//
///RELACIONES
//

//los eventos a llamar las relaciones
$("#idTiposProducto").change(RelacionProveedorTipoProducto)
$("#idProveedores").change(RelacionProveedorTipoProducto)
$("#idUsuarios").change(RelacionRolTipoProducto)
$("#idTiposProducto").change(RelacionRolTipoProducto)

//Validar si tienen relaciones

//Prov con TP
function RelacionProveedorTipoProducto() {

    var proveedor = $("#idProveedores");
    var tipoProducto = $("#idTiposProducto");

    $(proveedor).removeClass("bg-danger");
    $(proveedor).removeClass("bg-success");
    $(tipoProducto).removeClass("bg-danger");
    $(tipoProducto).removeClass("bg-success");
    var proveedorDesc = proveedor.children("option:selected")[0].innerText;
    var tipoProductoDesc = tipoProducto.children("option:selected")[0].innerText;

    if (proveedor.children("option:selected")[0].innerText == "Escoger..." || tipoProducto.children("option:selected")[0].innerText == "Escoger...") {

    } else {
        
        $.ajax({
            type: "GET",
            url: '/Recepcion/EnlaceProveedorTipoProducto',
            dataType: "json",
            data: {
                idProveedor: $(proveedor).val(),
                idTipoProducto: $(tipoProducto).val()
            },
            success: function (result) {

                if (result.pasa == 1) {
                    $(proveedor).addClass("bg-success")
                    $(tipoProducto).addClass("bg-success")

                    proveedor.children("option:not(:selected)").addClass("bg-light")
                    tipoProducto.children("option:not(:selected)").addClass("bg-light")

                    pasaProvTP = true;
                } else if (result.pasa == 2) {

                    var msg = proveedorDesc + " no tiene permitido proveer " + tipoProductoDesc;

                    EsconderMostrarToast(msg)
                    //podria agregarle un setTimeOut
                    $(proveedor).addClass("bg-danger")
                    $(tipoProducto).addClass("bg-danger")

                    proveedor.children("option:not(:selected)").addClass("bg-light");
                    tipoProducto.children("option:not(:selected)").addClass("bg-light");

                    pasaProvTP = false;
                } else {

                    var msg = result.msg;

                    EsconderMostrarToast(msg)
                    pasaProvTP = false;
                }

            },

            error: function (data, exception) {
                alert('error:' + data.responseText + "exception: " + exception);
            }
        });

    }
}

function RelacionRolTipoProducto() {

    var usuario = $("#idUsuarios");
    var tipoProducto = $("#idTiposProducto");

    $(usuario).removeClass("bg-danger");
    $(usuario).removeClass("bg-success");

    var usuarioDesc = usuario.children("option:selected")[0].innerText;
    var tipoProductoDesc = tipoProducto.children("option:selected")[0].innerText;

    if (tipoProducto.children("option:selected")[0].innerText == "Escoger..." || usuario.children("option:selected")[0].innerText == "Escoger...") {

    } else {

        $.ajax({
            type: "GET",
            url: '/Recepcion/EnlaceRolTipoProducto',
            dataType: "json",
            data: {
                idUsuario: $(usuario).val(),
                idTipoProducto: $(tipoProducto).val()
            },
            success: function (result) {

                if (result.pasa == 1) {

                    $(usuario).addClass("bg-success")

                    usuario.children("option:not(:selected)").addClass("bg-light")
                    pasaRolTP = true;
                } else if (result.pasa == 2) {

                    var msg = usuarioDesc + " no tiene permitido ingresar " + tipoProductoDesc;

                    EsconderMostrarToast(msg)
                    //podria agregarle un setTimeOut
                    $(usuario).addClass("bg-danger")

                    usuario.children("option:not(:selected)").addClass("bg-light")
                    pasaRolTP = false;
                } else {

                    var msg = result.msg;

                    EsconderMostrarToast(msg);
                    pasaRolTP = false;
                }

            },

            error: function (data, exception) {
                alert('error:' + data.responseText + "exception: " + exception);
            }
        });

    }
}

//
//
//
//
//




//
////Asignarle a otro COMBOBOX
//
//eventos
$("#idTiposProducto").change(AsignarBodega);
//ASIGNARLE UNA BODEGA


function AsignarBodega() {

    var bodega = $("#idBodegas");
    var bodegaInput = $("#ID_Bodega");
    var tipoProducto = $("#idTiposProducto");
    var parametro = $("#idParametros");
    var parametroInput = $("#ID_Parametro");
    $(parametro).empty();
    $(bodega).empty();

    if (tipoProducto.children("option:selected")[0].innerText == "Escoger...") {

    } else {

        $.ajax({
            type: "GET",
            url: '/Bodega/GetBodegaJSON',
            dataType: "json",
            async: false,
            data: {
                idTipoProducto: $(tipoProducto).val()
            },
            success: function (result) {

                if (result.pasa == true) {//trae el parametro

                    getBodegasR(result.bodega.ID_Bodega);
                    $(bodegaInput).val(result.bodega.ID_Bodega)
                    getParametrosR(result.bodega.ID_Parametros)
                    $(parametroInput).val(result.bodega.ID_Parametros)
                    console.log(result.bodega.DescripcionBodega);
                    console.log(result.bodega.ID_Parametros);
                    TPBodega = true;
                } else if (result.pasa == false) {

                    console.log("No se encontro bodega");
                    var msg = result.msg;
                    EsconderMostrarToast(msg);

                    TPBodega = false;
                }
                console.log($(parametroInput).val());
            },

            error: function (data, exception) {
                alert('error:' + data.responseText + "exception: " + exception);
            }
        });

    }
}

//seleccionar el option con el id de la bodega
function getParametrosR(id) {
    var parametro = $("#idParametros");
    //$(parametro).empty();
    $(parametro).attr("disabled", true);

    $.ajax({
        type: "GET",
        url: '/Parametros/GetParametros',
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {

                if (item.ID_Parametro == id) {
                    $("#idParametros").append('<option selected value=' + item.ID_Parametro + '>' + item.DescripcionParametro + '</option>');
                } else {
                    $("#idParametros").append('<option value=' + item.ID_Parametro + '>' + item.DescripcionParametro + '</option>');
                }
            });
        },

        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    });

}


//seleccionar el option con el id del tipoProduco
function getBodegasR(id) {
    var bodega = $("#idBodegas");

    $(bodega).attr("disabled", true);

    $.ajax({
        type: "GET",
        url: '/Bodega/GetBodegas',
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {

                if (item.ID_Bodega == id) {
                    $("#idBodegas").append('<option selected value=' + item.ID_Bodega + '>' + item.DescripcionBodega + '</option>');
                } else {
                    $("#idBodegas").append('<option value=' + item.ID_Bodega + '>' + item.DescripcionBodega + '</option>');
                }
            });
        },

        error: function (data, exception) {
            alert('error:' + data.responseText + "exception: " + exception);
        }
    });

}

//
//
//
//
//
//



//
////OFFCANVA
//

//eventos
$("#btnAgregar").click(LlenarProductosOffCanva);
// Con on():
$(document).on("click", ".btnOffcanvasAgregar", VerificarDMV); //este primero porque tiene que poner la variable en false o true antes de hacer el metodo de cargarbotones agregar
$(document).on("click", ".btnOffcanvasAgregar", ValidarFechaParametroConFechaActual);
$(document).on("click", ".btnOffcanvasAgregar", CargarBotonesAgregar);
$(document).on("click", ".btnOffcanvasEliminar", BtnEliminar);
//llenar el offcanva de prods

function LlenarProductosOffCanva() {
    RelacionProveedorTipoProducto();
    RelacionRolTipoProducto();

    //agarrar tabla del offcanva
    var tablaProductos = $("#tablaProductos tbody");
    $(tablaProductos).empty();
    //otras variables
    var proveedor = $("#idProveedores");
    var tipoProducto = $("#idTiposProducto")
    var usuario = $("#idUsuarios");
    var bodega = $("#idBodegas");
    var parametro = $("idParametros");
    var proveedorDesc = proveedor.children("option:selected")[0].innerText;
    var tipoProductoDesc = tipoProducto.children("option:selected")[0].innerText;
    var usuarioDesc = usuario.children("option:selected")[0].innerText;

    //validar si las relaciones siguen bien

    if (proveedor.children("option:selected").text() == "Escoger..." || tipoProducto.children("option:selected").text() == "Escoger..." || usuario.children("option:selected").text() == "Escoger..." || bodega.children("option:selected").text() == "Escoger..." || parametro.children("option:selected").text() == "Escoger...") {


        MostrarCamposFaltantes();

    } else {
        /*
        if (fechaBien == false) {
            var msg = "La fecha de Vencimiento es menor a la actual"
            EsconderMostrarToast(msg);
        }
        else*/ if (pasaProvTP == false) {
            var msg = proveedorDesc + " no tiene permitido proveer " + tipoProductoDesc;
            EsconderMostrarToast(msg);

        } else if (pasaRolTP == false) {
            var msg = usuarioDesc + " no tiene permitido ingresar " + tipoProductoDesc;
            EsconderMostrarToast(msg);
        } else//todo esta completo
        {
            //atributos para que funcione el offcanva y aparezca
            $("#btnAgregar").attr("data-bs-toggle", "offcanvas");
            $("#btnAgregar").attr("data-bs-target", "#offcanvasScrolling");
            $("#btnAgregar").attr("aria-controls", "#offcanvasScrolling");

            //deshabilitamos todos los botones
            $("#idUsuarios").attr("disabled", true);
            $("#idProveedores").attr("disabled", true);
            $("#idTiposProducto").attr("disabled", true);
            $("#idBodegas").attr("disabled", true);
            $("#idParametros").attr("disabled", true);
            //$("#fechaVencimiento").attr("readonly", true);

            //llenar los productos con el tp
            //agarrar el id del TP del combobox
            var idTipoProducto = $("#idTiposProducto").children("option:selected").val();

            $.ajax({
                type: "GET",
                url: '/Recepcion/getProductosPorSuTP',
                dataType: "json",
                data: {
                    idTipoProducto: idTipoProducto
                },
                success: function (result) {

                    $.each(result, function (key, item) {


                        var htmlRow = "<tr>";

                        htmlRow += "<td class='id'>";
                        htmlRow += item.ID_Producto;
                        htmlRow += "</td>";
                        htmlRow += "<td  class='DescripcionProducto'>";
                        htmlRow += item.DescripcionProducto;
                        htmlRow += "</td>";
                        htmlRow += "<td  class='fechaAlta'>";
                        htmlRow += moment(item.Fecha_Alta).format().slice(0, 10);
                        htmlRow += "</td>";
                        htmlRow += "<td class='DMV'>";
                        htmlRow += item.DiferenciaMesesVencimiento;
                        htmlRow += "</td>";
                        htmlRow += "<td  class='Cubicaje'>";
                        htmlRow += item.Cubicaje;
                        htmlRow += "</td>";
                        htmlRow += "<td  class='Empaque'>";
                        htmlRow += item.Empaque;
                        htmlRow += "</td>";
                        htmlRow += "<td  class='CostoBruto'>";
                        htmlRow += item.CostoBruto;
                        htmlRow += "</td>";
                        htmlRow += "<td class='Descuento'>";
                        htmlRow += item.Descuento;
                        htmlRow += "</td>";
                        htmlRow += "<td class='IVA'>";
                        htmlRow += item.IVA;
                        htmlRow += "</td>";
                        htmlRow += "<td class='CostoNeto'>";
                        htmlRow += item.CostoNeto;
                        htmlRow += "</td>";
                        htmlRow += "<td class='DescripcionRack'>";
                        htmlRow += item.DescripcionRack;
                        htmlRow += "</td>";
                        htmlRow += "<td class='DescripcionTipoProducto'>";
                        htmlRow += item.DescripcionTipoProducto;
                        htmlRow += "</td>";
                        htmlRow += "<td class='DescripcionProveedor'>";
                        htmlRow += item.DescripcionProveedor;
                        htmlRow += "</td>";
                        htmlRow += "<td class='DescripcionUnidadMedida'>";
                        htmlRow += item.DescripcionUnidadMedida;
                        htmlRow += "</td>";

                        htmlRow += "<td class='diferente'>";
                        htmlRow += "<a class='btn btn-outline-success btnOffcanvasAgregar' value='" + item.ID_Producto + "' title='Agregar Producto' >"
                        htmlRow += "<svg xmlns='http://www.w3.org/2000/svg' width='1.5rem' height='1.5rem' fill='currentColor' class='bi bi-plus' viewBox='0 0 16 16'>";
                        htmlRow += "<path d = 'M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z' />";
                        htmlRow += "</svg>";
                        htmlRow += "</a>";
                        htmlRow += "</td>";
                        htmlRow += "</tr>";

                        $(tablaProductos).append(htmlRow)
                    });

                    // $('.btnOffcanvasAgregar').click(CargarTodosBotones);//se pone aqui el evento porque, si lo pongo afuera, como esos botones no se han creado aun, crea el evente click a nada, pero si lo pongo aca les agrega el evente a los botones recine creados
                },

                error: function (data, exception) {
                    alert('error:' + data.responseText + "exception: " + exception);
                }
            });
        }
    }
}


function CargarBotonesAgregar() {
    if (pasaFechaParametro == true) {
        if (pasaDMV == true) {

            var idProd = $(this).attr("value") //me trae el id Prod que habia guardado en el htmlRow

            var htmlBotonQuitar = "<a class='btn btn-outline-danger btnOffcanvasEliminar' value='" + idProd + "' title='Agregar Producto' >"
            htmlBotonQuitar += "<svg xmlns='http://www.w3.org/2000/svg' width='1.5rem' height='1.5rem' fill='currentColor' class='bi bi-dash-circle' viewBox='0 0 16 16'>";
            htmlBotonQuitar += "<path d = 'M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z' />";
            htmlBotonQuitar += "<path d = 'M4 8a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7A.5.5 0 0 1 4 8z' />";
            htmlBotonQuitar += "</svg>";
            htmlBotonQuitar += "</a>";
            var row = "<tr>";
            row += $(this).parent().parent().html();//esto es todo el tr del producto que seleccionamos
            row += "</tr>";
            var tablaRecepcion = $("#tRecepcion tbody");//traigo el elemnto tbody de la tabla

            $(tablaRecepcion).append(row); //lo agrego a la tabla de recepcion, la principal

            var ultimo = $(tablaRecepcion)[0].lastChild; //aarra el ultimo tr de producto creado

            $(ultimo).find(".diferente").html(htmlBotonQuitar); //buscamos el td que tiene el boton para ponerselo para q sea el de eliminar y no el de agregar
            $(this).parent().hide();
        } else {
            var msg = "La diferencia de Meses de Vencimiento entre la fecha de Ingreso y la Fecha de Vencimineto es menor a la minima requerida para el producto";
            EsconderMostrarToast(msg);

            var dtDMV = $(this).parent().parent().find(".DMV");

            $(dtDMV).addClass("bg-warning");

            setTimeout(function () {
                $(dtDMV).removeClass("bg-warning");


                setTimeout(function () {
                    $(dtDMV).addClass("bg-warning");

                    setTimeout(function () {
                        $(dtDMV).removeClass("bg-warning");


                    }, 1000)

                }, 1000)


            }, 1000)
        }

    } else {
        var bodega = $("#idBodegas");//para obtener el id Bodega

        $.ajax({
            type: "GET",
            url: '/Parametros/GetParametroJSON',
            async: false, //como el codigo no terminaba de ejecutar cuando ocupaba que me asignara la variable, tengo q quitarle la asincronia
            data: {
                idBodega: $(bodega).val()
            },
            dataType: "json",
            success: function (result) {

                if (result.pasa == true) {//todo bien

                    var msg = "No es posible recepcionar en la hora actual. <br/>";
                    msg += "Consultar horario de recepcion con el administrador";
                    EsconderMostrarToast(msg);

                } else {
                    console.log("No encontro un parametro");
                }
            },
            error: function (data, exception) {
                alert('error:' + data.responseText + "exception: " + exception);
            }
        });

        Habilitar();


    }

}

function BtnEliminar() {
    var id = $(this).attr("value"); //el id del 

    $("#tablaProductos tbody td").each(function () {

        if ($(this).find("a").attr("value") == id)//el primero indica que, si del td, su etiqueta a(que es el boton), su atributo valor es igual a el valor del que voy a eliminar en la tabla recepcion, entonces:
        {

            var htmlBotonAgregar = "";
            htmlBotonAgregar += "<a class='btn btn-outline-success btnOffcanvasAgregar' value='" + id + "' title='Agregar Producto' >"
            htmlBotonAgregar += "<svg xmlns='http://www.w3.org/2000/svg' width='1.5rem' height='1.5rem' fill='currentColor' class='bi bi-plus' viewBox='0 0 16 16'>";
            htmlBotonAgregar += "<path d = 'M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z' />";
            htmlBotonAgregar += "</svg>";
            htmlBotonAgregar += "</a>";

            $(this).show();

            $(this).html(htmlBotonAgregar);
        }
    })
    $(this).parent().parent().empty(); //lo elimina el tr de la tabla de recepcion
}

//
//
//
//
//
//
//



//
////Acortar CODIGO
//

//habilitar los combobox deshabilitados / restringidos
function Habilitar() {
    //los volvemos a habilitar
    $("#idUsuarios").removeAttr("disabled");
    $("#idProveedores").removeAttr("disabled");
    $("#idTiposProducto").removeAttr("disabled");


    $("#idUsuarios").removeClass("bg-success");
    $("#idProveedores").removeClass("bg-success");
    $("#idTiposProducto").removeClass("bg-success");
    $("#idUsuarios").removeClass("bg-danger");
    $("#idProveedores").removeClass("bg-danger");
    $("#idTiposProducto").removeClass("bg-danger");
}


function MostrarCamposFaltantes() {
    var msg = "Faltan datos*";
    EsconderMostrarToast(msg);
    //ponerle un fondo amarillo por un tiempo
    var optionsSelected = $("option:selected");

    $.each(optionsSelected, function (i, value) {//el value es una opcion

        if ($(value).text() == "Escoger...") {

            $(value).parent().addClass("bg-warning");

            setTimeout(function () {
                $(value).parent().removeClass("bg-warning");


                setTimeout(function () {
                    $(value).parent().addClass("bg-warning");

                    setTimeout(function () {
                        $(value).parent().removeClass("bg-warning");


                    }, 1000)

                }, 1000)


            }, 1000)
        }

    })

}

//
//
//
//


//
////botones
//

$("#btnLimpiar").click(function () {

    Habilitar();

    $("#offcanvasScrolling").removeClass("show")
    $("#tablaProductos tbody").empty();


    //atributos para que funcione el offcanva y aparezca, se los quitamos, para que se le active solo si vuelve a tocar el btn de agregar
    $("#btnAgregar").removeAttr("data-bs-toggle");
    $("#btnAgregar").removeAttr("data-bs-target");
    $("#btnAgregar").removeAttr("aria-controls");
    //

})

//Para ver la cantidad de prods
$("#btnEnviar").click(function () {
    cantidadProds = 0;
    $("#tRecepcion tbody tr").each(function () {

        if ($.trim($(this).html()) != "") {
            cantidadProds++;
        }

    })

})

//
////GUARDAR RECEPCION
//

$("#frmRecepcion").submit(function () {

    event.preventDefault();
    if ($.trim($("#descripcion").val()) == "" || $.trim($("#fechaVencimiento").val()) == "" || $.trim($("#idUsuarios").val()) == "" || $.trim($("#idProveedores").val()) == "" || $.trim($("#idTiposProducto").val()) == "" || $.trim($("#ID_Bodega").val()) == "" || $.trim($("#ID_Parametro").val()) == "") {
        MostrarCamposFaltantes();

    } else if ($.trim($("#tRecepcion tbody").html()) == "") {
        debugger;
        var msg = "No hay productos a recepcionar";
        EsconderMostrarToast(msg);

        var value = ("#tRecepcion tbody");

        $(value).parent().addClass("bg-warning");

        setTimeout(function () {
            $(value).parent().removeClass("bg-warning");

            setTimeout(function () {
                $(value).parent().addClass("bg-warning");

                setTimeout(function () {
                    $(value).parent().removeClass("bg-warning");


                }, 1000)

            }, 1000)


        }, 1000)
    } else
    {
        //debugger;
        //mas validaciones
        ValidarStockMinMax();
        GenerarLista();
        debugger;
        IngresarUbicacionAProductos();
        if (pasaStockMinMax == true) {
            debugger;
            if (pasaUbicacionesProductos == true) {

                //si todo sale bien
                $("#offcanvasScrolling").removeClass("show");

                //boton editar o crear
                var textoBoton = $("#btnEnviar").val();
                debugger;

                $.ajax({
                    type: "POST",
                    url: '/Recepcion/Crear',
                    dataType: "json",
                    async: false,
                    data: {
                        EncabezadoR: EncabezadoR,
                        listaProductos: lista
                    },
                    success: function (result) {
                        if (result.pasa == 1) {
                            if (textoBoton == "Crear") {
                                AgarrarDescripcion("creó", result.toRedirect);
                            }
                        } else if (result.pasa == 2) {
                            console.log("Hubo un problema 2")
                        } else {
                            console.log("Hubo un problema 3")
                        }
                       
                    },

                    error: function (data, exception) {
                        alert('error:' + data.responseText + "exception: " + exception);
                    }
                });
            } else {
                var msg = "No se le asigno ubicaciones";
                EsconderMostrarToast(msg);
            }
        } else {
            var msg = "Si se ingresa los respectivos productos, superan el stock maximo";
            EsconderMostrarToast(msg);
        }

    }
})


function GenerarLista() {

    //variables
    var descripcion = $("#descripcion").val();
    var FechaIngreso = $("#fechaIngresoEscondido").val();
    var fechaVencimiento = $("#fechaVencimiento").val();
    var idUsuarios = $("#idUsuarios").val();
    var idProveedores = parseInt($("#idProveedores").val());
    var idTiposProducto = parseInt($("#idTiposProducto").val());
    var idBodegas = parseInt($("#idBodegas").val());
    var idParametros = parseInt($("#idParametros").val());
    var idTipoProducto = $("#idTiposProducto").val();
    var idUnidadesMedida = $("#idUnidadesMedida").val();
    debugger;

    EncabezadoR = {//el objeto completo
        DescripcionEncabezadoRecepcion: descripcion,
        FechaIngreso: FechaIngreso,
        ID_Usuario: idUsuarios,
        ID_Bodega: idBodegas,
        ID_Proveedor: idProveedores,
        fechaVencimiento: fechaVencimiento,
        ID_TipoProducto: idTiposProducto,
        ID_Parametro: idParametros
    }
    //pasar la tabla de recepcion a una lista
    var id = 0;
    var desc = "";
    var DMV = 0;
    var fechaAlta = "";
    var Cubicaje = 0;
    var Empaque = 0;
    var CostoBruto = 0;
    var Descuento = 0;
    var IVA = 0;
    var CostoNeto = 0;
    var DescripcionRack = "";
    var DescripcionTipoProducto = "";
    var DescripcionProveedor = "";
    var DescripcionUnidadMedida = "";
    //var tRecepcion = $("tRecepcion tbody");

    //var lista = []

    $("#tRecepcion tbody tr").each(function () {

        //console.log($(this))//muestra cada tr

        $(this).children().each(function () {

            //console.log($(this))//muestra cada td del tr

            if ($(this).hasClass("id")) {

                id = $(this).html();
            } else if ($(this).hasClass("DescripcionProducto")) {

                desc = $(this).html();
            } else if ($(this).hasClass("DMV")) {

                DMV = $(this).html();
            } else if ($(this).hasClass("fechaAlta")) {

                fechaAlta = $(this).html();
            } else if ($(this).hasClass("Cubicaje")) {

                Cubicaje = $(this).html();
            } else if ($(this).hasClass("Empaque")) {

                Empaque = $(this).html();
            } else if ($(this).hasClass("CostoBruto")) {

                CostoBruto = $(this).html();
            } else if ($(this).hasClass("Descuento")) {

                Descuento = $(this).html();
            } else if ($(this).hasClass("IVA")) {

                IVA = $(this).html();
            } else if ($(this).hasClass("CostoNeto")) {

                CostoNeto = $(this).html();
            } else if ($(this).hasClass("DescripcionRack")) {

                DescripcionRack = $(this).html();
            } else if ($(this).hasClass("DescripcionTipoProducto")) {

                DescripcionTipoProducto = $(this).html();
            } else if ($(this).hasClass("DescripcionProveedor")) {

                DescripcionProveedor = $(this).html();
            } else if ($(this).hasClass("DescripcionUnidadMedida")) {

                DescripcionUnidadMedida = $(this).html();
            }

        })

        var producto = {//cada obj detalle
            ID_Producto: id,
            DescripcionProducto: desc,
            Fecha_Alta: fechaAlta,
            DiferenciaMesesVencimiento: DMV,
            Cubicaje: Cubicaje,
            Empaque: Empaque,
            CostoBruto: CostoBruto,
            Descuento: Descuento,
            IVA: IVA,
            CostoNeto: CostoNeto,
            DescripcionRack: DescripcionRack,
            DescripcionTipoProducto: DescripcionTipoProducto,
            DescripcionProveedor: DescripcionProveedor,
            DescripcionUnidadMedida: DescripcionUnidadMedida
        }
        lista.push(producto);

    })

    console.log(lista);
}


