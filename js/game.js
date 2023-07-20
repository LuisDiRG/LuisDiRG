var text = "";
var limit = 24;
var fruits = [
    "0", "0", "0", "0","0", "0", "0", "0","0", "0", "0", "0","1", "1", "0", "0","0", "0", "0", "0","0", "0", "0", "0",
    "0", "0", "0", "0","0", "0", "0", "0","0", "0", "0", "0","1", "1", "0", "0","0", "0", "0", "0","0", "0", "0", "0",
    "0", "0", "1", "1","1", "1", "1", "1","1", "1", "1", "1","1", "1", "1", "1","1", "1", "1", "1","1", "1", "0", "0",
    "0", "0", "1", "1","1", "0", "0", "0","0", "0", "1", "1","1", "1", "0", "0","0", "0", "0", "1","1", "1", "0", "0",
    "0", "0", "1", "1","1", "0", "0", "0","0", "0", "1", "1","1", "1", "0", "0","0", "0", "0", "1","1", "1", "0", "0",
    "0", "0", "1", "1","1", "0", "0", "0","0", "0", "1", "1","1", "1", "0", "0","0", "0", "0", "1","1", "1", "0", "0",
    "0", "0", "1", "1","0", "0", "0", "0","0", "0", "1", "1","1", "0", "0", "0","0", "0", "0", "1","1", "1", "0", "0",
    "0", "0", "1", "1","0", "0", "0", "0","0", "0", "1", "1","1", "0", "0", "0","0", "0", "0", "1","1", "1", "0", "0",
    "0", "0", "1", "1","1", "1", "1", "1","1", "1", "1", "1","1", "1", "1", "1","1", "1", "1", "1","1", "1", "0", "0",
    "0", "0", "1", "1","1", "1", "1", "1","1", "1", "1", "2","1", "0", "0", "0","0", "0", "0", "0","1", "1", "0", "0",
    "0", "0", "1", "1","1", "1", "1", "1","1", "1", "1", "1","1", "0", "0", "0","0", "0", "0", "0","1", "1", "0", "0",
    "0", "0", "1", "1","1", "0", "0", "0","0", "0", "1", "1","1", "0", "0", "0","0", "0", "0", "0","1", "1", "0", "0",
    "0", "0", "1", "1","1", "1", "1", "1","1", "1", "1", "1","1", "0", "0", "0","0", "0", "0", "0","1", "1", "0", "0",
    "0", "0", "1", "1","1", "1", "1", "1","1", "1", "1", "1","1", "0", "0", "0","0", "0", "0", "0","1", "1", "0", "0",
    "0", "0", "1", "1","1", "0", "1", "1","1", "1", "1", "1","1", "1", "1", "1","1", "1", "1", "1","1", "1", "0", "0",
    "0", "0", "1", "1","1", "1", "1", "1","1", "1", "1", "1","1", "1", "1", "1","1", "1", "1", "1","1", "1", "0", "0",
    "0", "0", "1", "1","1", "1", "1", "1","1", "1", "1", "1","1", "0", "0", "0","0", "0", "0", "1","1", "1", "0", "0",
    "0", "0", "1", "1","1", "1", "1", "0","0", "0", "0", "1","1", "1", "1", "1","1", "1", "1", "1","1", "1", "0", "0",
    "0", "0", "1", "1","1", "1", "1", "0","0", "0", "0", "1","1", "1", "1", "1","1", "1", "1", "1","1", "1", "0", "0",
    "0", "0", "0", "0","0", "0", "1", "0","0", "0", "0", "1","1", "1", "1", "1","1", "1", "0", "0","0", "0", "0", "0",

];
var loopcount = Math.ceil(fruits.length/limit)+1;
for	(var satir = 1; satir < loopcount; satir++) {
    for	(var index = (limit*(satir-1)); index < limit*satir ; index++) {
        text += "<div class='land L"+fruits[index]+"'></div>";
    }
    text += "<div style='clear:both;'></div>";
}

$(document).keydown(function(e) {
    switch(e.which) {
        case 65: // left
            var $curr = $( ".L2" );
            var $onceki = $curr.prev();
            if ($onceki.hasClass("L1")){
                $onceki.css( "background-position", "-32px 0" );
                $curr.removeClass("L2");
                $curr.addClass("L1");
                $onceki.removeClass("L1");
                $onceki.addClass("L2");
            }
            break;

        case 87: // up
            var $curr = $( ".L2" );
            var $onceki = $curr.prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev();
            if ($onceki.hasClass("L1")){
                $onceki.css( "background-position", "-16px 0" );
                $curr.removeClass("L2");
                $curr.addClass("L1");
                $onceki.removeClass("L1");
                $onceki.addClass("L2");
            }
            break;

        case 68: // right
            var $curr = $( ".L2" );
            var $onceki = $curr.next();
            if ($onceki.hasClass("L1")){
                $onceki.css( "background-position", "-48px 0" );
                $curr.removeClass("L2");
                $curr.addClass("L1");
                $onceki.removeClass("L1");
                $onceki.addClass("L2");
            }
            break;

        case 83: // down
            var $curr = $( ".L2" );
            var $onceki = $curr.next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next();
            if ($onceki.hasClass("L1")){
                $onceki.css( "background-position", "0 0" );
                $curr.removeClass("L2");
                $curr.addClass("L1");
                $onceki.removeClass("L1");
                $onceki.addClass("L2");
            }
            break;

        default: return; // exit this handler for other keys
    }
    e.preventDefault(); // prevent the default action (scroll / move caret)
});

$("#map").html(text);

$(document).ready(function() {
    $(".cruz").on("click", function(event) {
        var imageWidth = $(this).width();
        var imageHeight = $(this).height();
        var clickX = event.offsetX;
        var clickY = event.offsetY;

        // Obtener las coordenadas para cada lado de la cruz
        var topSide = (clickY < imageHeight / 3);
        var bottomSide = (clickY > imageHeight * 2 / 3);
        var leftSide = (clickX < imageWidth / 3);
        var rightSide = (clickX > imageWidth * 2 / 3);

        // Ejecutar la función según la posición del clic
        if (topSide) {
            var $curr = $( ".L2" );
            var $onceki = $curr.prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev();
            if ($onceki.hasClass("L1")){
                $onceki.css( "background-position", "-16px 0" );
                $curr.removeClass("L2");
                $curr.addClass("L1");
                $onceki.removeClass("L1");
                $onceki.addClass("L2");
            }
        } else if (bottomSide) {
            var $curr = $( ".L2" );
            var $onceki = $curr.next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next().next();
            if ($onceki.hasClass("L1")){
                $onceki.css( "background-position", "0 0" );
                $curr.removeClass("L2");
                $curr.addClass("L1");
                $onceki.removeClass("L1");
                $onceki.addClass("L2");
            }
        } else if (leftSide) {
            var $curr = $( ".L2" );
            var $onceki = $curr.prev();
            if ($onceki.hasClass("L1")) {
                $onceki.css("background-position", "-32px 0");
                $curr.removeClass("L2");
                $curr.addClass("L1");
                $onceki.removeClass("L1");
                $onceki.addClass("L2");
            }
        } else if (rightSide) {
            var $curr = $( ".L2" );
            var $onceki = $curr.next();
            if ($onceki.hasClass("L1")){
                $onceki.css( "background-position", "-48px 0" );
                $curr.removeClass("L2");
                $curr.addClass("L1");
                $onceki.removeClass("L1");
                $onceki.addClass("L2");
            }
        }
    });

    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
    const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))
});
