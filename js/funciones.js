$(document).ready(function (){
    $("#profes1 div").hover(entrada,salida);
    $("#profes2 div").hover(entrada,salida);
    randomValues();
    Levitar();
    LevitarTit();
    LevitarTit2();
    CambiarWidthImg();

flujoCoontenidoHabilidad = $('.cardAcitudes').html();
    $(window).resize(function(){

        FlujoNormal();

        if(window.screen.width < 1250 && window.screen.width >= 850){
            console.log('BBB');
            $('.bloqueIzq').attr('style','padding-top: 25%!important;');
            $('.cardAcitudes').attr('style','padding-top: 35%!important;');
        }else if(window.screen.width < 850 && window.screen.width >= 830){
            console.log('CCC');
            $('.bloqueIzq').attr('style','padding-top: 30%!important;');
            $('.cardAcitudes').attr('style','padding-top: 60%!important;');
        }else if(window.screen.width < 830 && window.screen.width >= 800){
            console.log('DDD');
            $('.cardAcitudes').attr('style','padding-top: 60%!important;');
        }else if(window.screen.width < 768 && flujoCoontenidoHabilidad == $('.cardAcitudes').html()){
            
            let contenidoHabilidades = $('.cardAcitudes').html();
            let BloqueMario = $('.bloqueMario').html();

            $('.cardAcitudes').html(BloqueMario);
            $('.bloqueMario').html(contenidoHabilidades);
        }
    });
    
});

let flujoCoontenidoHabilidad;
function FlujoNormal(){
    if(window.screen.width > 768 && flujoCoontenidoHabilidad != $('.cardAcitudes').html()){

        let contenidoHabilidades = $('.cardAcitudes').html();
        let BloqueMario = $('.bloqueMario').html();
        $('.cardAcitudes').html(BloqueMario);
        $('.bloqueMario').html(contenidoHabilidades);
    }
}

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
}

function Levitar() {
    anime({
        targets: '.cardQEDW',
        translateY: 20,
        duration: 1000,
        direction: 'alternate',
        easing: 'easeInOutQuad',
        loop: true
    });
}

function LevitarTit() {
    anime({
        targets: '.titQueEsDW',
        translateY: 20,
        duration: 1000,
        direction: 'alternate',
        easing: 'easeInOutQuad',
        loop: true
    });
}

function LevitarTit2() {
    anime({
        targets: '.titQueEsDW2',
        translateY: 20,
        duration: 1000,
        direction: 'alternate',
        easing: 'easeInOutQuad',
        loop: true
    });
}
requestAnimationFrame(loop);

/* header */
function CambiarWidthImg(){
    
    if(window.screen.width > 600){
        $('#imgLogo').attr('width','15%');
    }else{
        $('#imgLogo').attr('width','20%');
    }
}


let x = false;
let y = false;

function AnimacionHeader(inicio, final){
    
    if(x === false){
        anime({
        targets: '#imgLogo',
        width: [inicio, final],
        easing: 'easeInOutQuad'
    });
    x = true;
    }else
        return;
}

function AnimacionHeaderGrande(inicio,final){
    if(y === false){
        console.log('bb');
        anime({
        targets: '#imgLogo',
        width: [inicio, final],
        easing: 'easeInOutQuad'
    });
        y = true;
    }else{
        
        return;
    }
        
}
window.addEventListener('scroll', function() {
  const header = document.querySelector('.fondoHeaderM');
  const hero = document.querySelector('#Inicio');
  const scrollPosition = window.scrollY;
  const imgLogo = document.querySelector('#imgLogo');
  if (scrollPosition >= hero.offsetHeight) {
      if(window.screen.width > 600){
        ClasesLlamar(header);
        AnimacionHeader('10%','5%');
        y = false;
      }else{
        ClasesLlamar(header);
        AnimacionHeader('10%','15%');
        y = false;
      }
    
    
  } else {
    
    if(window.screen.width > 600){
        ClasesLlamar2(header);
        x = false;
        AnimacionHeaderGrande('5%', '10%');
    }else{
        ClasesLlamar2(header);
        x = false;
        AnimacionHeaderGrande('10%', '15%');
    }
  }
});

function ClasesLlamar(header){
    header.classList.add('fixed-top');
    header.classList.remove('sticky-top');
    header.classList.add('fondoHeader');
    header.classList.remove('fondoHeaderFixed');
}
function ClasesLlamar2(header){
    header.classList.remove('fixed-top');
        header.classList.add('sticky-top');
        header.classList.add('fondoHeaderFixed');
        header.classList.remove('fondoHeader');
}
