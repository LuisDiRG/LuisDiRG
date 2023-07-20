$(document).ready(function (){
    $("#profes1 div").hover(entrada,salida);
    $("#profes2 div").hover(entrada,salida);
    randomValues();
});

function entrada(){
    $(this).addClass("profe2");
    $(this).fadeTo("fast","1");
}

function salida(){
    $(this).removeClass("profe2");
    $(this).fadeTo("fast","0.65");
}


var animation2 = anime({
    targets: '.arribayabajo2',
    translateY: -12,
    direction: 'alternate',
    loop: true,
    easing: 'easeInOutQuad',
    autoplay: false,
    duration: 3000
});
var animation = anime({
    targets: '.arribayabajo',
    translateY: -12,
    translateX: 30,
    direction: 'alternate',
    loop: true,
    easing: 'easeInOutQuad',
    autoplay: false,
    duration: 3000
});

function loop(t) {

    animation.tick(t);
    animation2.tick(t);


    customRAF = requestAnimationFrame(loop);
}

function randomValues() {
    anime({
        targets: '.imgQEDQ',
        translateX: function() {
            do{
                x = anime.random(-100, window.screen.width-400);
            }while(x < window.screen.width-700 && x > window.screen.width-1300);
            return x;
        },
        easing: 'easeInOutQuad',
        duration: 1050,
        complete: randomValues
    });
    console.log("BBB");
}

requestAnimationFrame(loop);
