$(document).ready(function (){
    $("#profes1 div").hover(entrada,salida);
    $("#profes2 div").hover(entrada,salida);
});

function entrada(){
    $(this).addClass("profe");
    $(this).fadeTo("fast","1");
}

function salida(){
    $(this).removeClass("profe");
    $(this).fadeTo("fast","0.65");
}
