
/***********************************slider page D'acceuil**********************/
  var marche = true;
            
            var curseur = 0;
            // chemin d'acces vers le rep images
            var path='image/';
            
            var mesImages = Array('baaner1.jpg','home_banner_seniors.jpg','home_banner_adultes.jpg','banner4.jpg');
            
            
            function change_image(nouvelleImage)
            {
                $('.banner').attr({
                                            src: path+ nouvelleImage
                                            
                });
                
            }
            
            
            function augmenterImage()
            {
                // on fait bouger le curseur
                curseur++;
                if (curseur >= mesImages.length){
                    curseur =0;
                }
                
                // l'image pointée est mesImages[curseur]
                change_image(mesImages[curseur]);
            }
            
          
         
            
            $(function(){
                
                setInterval(function(){
                    if (marche == true){
                        augmenterImage();
                    }
  
                }, 3000);
                
            });
/**********************les slider de la page service**************************/
		 
$(document).ready(function($) {
	
		$("button").click(function() {
	
		$(this).parent().toggleClass("marion");
			
	        $(this).next().slideToggle("slow");

		});

		});
/********************Maps Contacte*************************/

function initialize() {
  var myLatlng = new google.maps.LatLng(45.5929418,-73.7608006);
 
 var mapOptions = {
    zoom: 15,
    center: myLatlng
  }
  var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

 
 var marker = new google.maps.Marker({
      position: myLatlng,
      map: map,
      title: 'Hello World!'
  });
}


google.maps.event.addDomListener(window, 'load', initialize);

/********************page produit*********************************/
$(document).ready(function() {
			$('a#exemple1').fancybox();
			$('a#exemple2').fancybox({
				'overlayShow'	: false,
				'transitionIn'	: 'elastic',
				'transitionOut'	: 'elastic'
			});
			$('a#exemple3').fancybox();
			$('a#exemple4').fancybox({
				'titlePosition'	: 'over'
			});
			$('a#exemple5').fancybox({
				'overlayShow'	: false,
				'transitionIn'	: 'elastic',
				'transitionOut'	: 'elastic'
			});
			
			$('a[rel=diaporama_group]').fancybox({
				'transitionIn'		: 'elastic',
				'transitionOut'		: 'none',
				'titlePosition' 	: 'over',
				'titleFormat'		: function(title, currentArray, currentIndex, currentOpts) {
					return '<span id="fancybox-title-over">Image ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? ' &nbsp; ' + title : '') + '</span>';
				}
			});
			
			$('#LienIframe').fancybox({
				'width' : '100%',
				'height' : '75%',
				'autoScale' : false,
				'transitionIn' : 'none',
				'transitionOut' : 'none',
				'type' : 'iframe'
			});
		});