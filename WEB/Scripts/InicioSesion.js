function Ingresar() {

    //Variables
    var usuario = $("#Username").val();
    var password = $("#Password").val();
    pasa = false
    //

    var listaUsuario = JSON.parse(localStorage.getItem('listaUsuario'));

    if (listaUsuario == null || listaUsuario == "undefined" || listaUsuario == "") {

        var msg = "No hay usuarios ingresados";
        EsconderMostrarToast(msg);
    } else {

        listaEmpleado.forEach(usuario => {

            if (usuario.Password == password && usuario.Username == user) {

                window.location = "Layout.cshtml"
                pasa = true;

               // localStorage.setItem('NombreUsuario', JSON.stringify(usuario.Nombre + " " + usuario.Ape1));//esto es para que nos muestre el nombre de usuario cuando ingresemos
            }

        })

        if (pasa == false) {//si no encontro usuario

            var msg = "No hay un usuario existente con esos datos";
            EsconderMostrarToast(msg);
        }
    }
}

//este no
$(document).ready(function () {

    var nombreUsuario = JSON.parse(localStorage.getItem('NombreUsuario'));

    if (nombreUsuario == null || nombreUsuario == "undefined" || nombreUsuario == "") {

        console.log("No hay ningun usuario puesto para el login")
    } else {
        console.log($("#NombreUsuario"));
        $("#NombreUsuario")[0].outerText = nombreUsuario;
    }
})


function CerrarToast() {

    $("#liveToastCrear").removeClass("show");
    $("#liveToastCrear").addClass("hide");
}


function EsconderMostrarToast(msg) {
    $("#liveToastCrear .toast-body").append(msg) //agrega mensaje al toast

    $("#liveToastCrear").removeClass("hide") //quitarle la clase que lo esconde

    $("#liveToastCrear").addClass("show") // ponerle para que lo muestre

    setTimeout(function () {
        $("#liveToastCrear").addClass("hide"); // para que se vuelva a esconder

        $("#liveToastCrear #toast-bodyCrearUM").empty(); //para vaciarlo
    }, 5000)
}