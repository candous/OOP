<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
 <%@ page import="beans.*" %>
 
 <% 
	int nombreArticles=(Integer)request.getAttribute("nombreArticles");
 	request.getSession().setAttribute("categorie", "categorie.jsp");
 	
	//recuperer user dans chaque page pour láffichage du menu 
	boolean userExists=false;
	String nom=null;
		
	try{
		
		User leUser=(User)request.getSession().getAttribute("user");
		
		if(leUser!=null)
		{
			userExists=true;
			nom=leUser.getNom();
		}
	
	}catch(Exception e){ }
%>     
    
<!DOCTYPE html>
<html lang="fr">
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="shortcut icon" href="assets/ico/favicon.png">

    <title> Categories</title>

    <!-- Bootstrap core CSS -->
    <link href="assets/css/bootstrap.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="assets/css/main.css" rel="stylesheet">
    <link rel="stylesheet" href="assets/css/icomoon.css">
	<link rel="stylesheet" href="assets/css/personnel.css">
    <link href="assets/css/animate-custom.css" rel="stylesheet">


    
    <link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,300italic,400italic' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Raleway:400,300,700' rel='stylesheet' type='text/css'>
    <script src="assets/js/jquery.min.js"></script>
	<script type="text/javascript" src="assets/js/modernizr.custom.js"></script>
    
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="assets/js/html5shiv.js"></script>
      <script src="assets/js/respond.min.js"></script>
    <![endif]-->
  </head>

  <body data-spy="scroll" data-offset="0" data-target="#navbar-main">
  
   <!-- Secteur Menu -->
    
  	<div id="navbar-main">
      <!-- Fixed navbar -->	
	  
    <div class="navbar navbar-inverse navbar-fixed-top">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
            <span class="icon icon-shield" style="font-size:30px; color:#3498db;"></span>
          </button>
          <a class="navbar-brand hidden-xs hidden-sm" href="#home"><span class="glyphicon glyphicon-road" style="font-size:18px; color:#3498db;"></span></a>
        </div>
        <div class="navbar-collapse collapse">
        <form method = "get" action = "servletRecherche">
			<ul class="nav navbar-nav navbar-right">
			   <li> <a  class="smoothScroll"></span> <input type = "text" name = "indice"/></a></li>
			   </ul>
			    </form>
          <ul class="nav navbar-nav">
            <li><a href="servletIndex?menu=home" class="smoothScroll">Home</a></li>
			<li> <a href="servletIndex?menu=categorie" class="smoothScroll">Categories</a></li>
			
			<%if(!userExists) {%>
			<li> <a href="servletIndex?menu=login" class="smoothScroll">Login</a></li></ul>
			<ul class="nav navbar-nav navbar-right">
			<li> <a href="servletIndex?menu=panier" class="smoothScroll"> <span class="glyphicon glyphicon-tag" style="font-size:18px; color:gray;"></span> Panier(<%=nombreArticles%>)</a></li>
			 </ul> <%} else {%>
			</ul>
			<ul class="nav navbar-nav navbar-right">
			<li class="dropdown" class="smoothScroll">
            <a href="#" data-toggle="dropdown" class="dropdown-toggle"><%=nom %><b class="caret"></b></a>
            <ul class="dropdown-menu">
            <li><a href="servletAfficherHistorique">Historique</a></li>
            <li><a href="servletLogOut" class="smoothScroll">LogOut</a></li>
        </ul>
    </li>	
			<li> <a href="servletIndex?menu=panier" class="smoothScroll"> <span class="glyphicon glyphicon-tag" style="font-size:18px; color:gray;"></span> Panier(<%=nombreArticles%>)</a></li>
			 </ul>
			 <%} %>
        </div><!--/.nav-collapse -->
        </div><!--/.nav-collapse -->
      </div>
    </div>
    </div>
   <!-- Fin Secteur Menu -->
  


		<!-- ==== SECTION DIVIDER4 ==== -->
		<section class="section-divider textdivider divider4">
			<div class="container">
				<h1>NOS CATEGORIES DE CASQUES</h1>
				<hr>
				<p>Toute une recherche pour pouvoir ressortir les differents types de casques les plus envies du moment, allez visiter et soyez temoin de nos success</p>
			</div><!-- container -->
		</section><!-- section -->

		
		<!-- ==== PORTFOLIO ==== -->
		<div class="container" id="categorie" name="categorie">
		<br>
			<div class="row">
				<br>
				<br>
			</div><!-- /row -->
			<div class="container">
			<div class="row">	
			
				<!--Categorie 1 -->
				<div class="col-md-4 ">
			    	<div class="grid mask">
						<figure>
							<a data-toggle="modal" href="servletAfficherProduits?id=retro" ><img class="img-responsive" src="assets/img/portfolio/folio01.jpg" alt=""></a>
							<figcaption>
								<h5>retro</h5>
							</figcaption><!-- /figcaption -->
						</figure><!-- /figure -->
			    	</div><!-- /grid-mask -->
				</div><!-- /col -->
				
				<!-- -Categorie 2 -->
				<div class="col-md-4">
			    	<div class="grid mask">
						<figure>
							<a data-toggle="modal" href="servletAfficherProduits?id=custom" ><img class="img-responsive" src="assets/img/portfolio/folio02.jpg" alt="" ></a>
							<figcaption>
								<h5>custom</h5>
							</figcaption><!-- /figcaption -->
						</figure><!-- /figure -->
			    	</div><!-- /grid-mask -->
				</div><!-- /col -->
				
				<!-- -Categorie 3 -->
				<div class="col-md-4"> 
			    	<div class="grid mask">
						<figure>
							<a data-toggle="modal" href="servletAfficherProduits?id=sport" ><img class="img-responsive" src="assets/img/portfolio/folio03.jpg" alt=""  ></a>
							<figcaption>
								<h5>sport</h5>
							</figcaption><!-- /figcaption -->
						</figure><!-- /figure -->
			    	</div><!-- /grid-mask -->
				</div><!-- /col -->
			</div><!-- /row -->

				
			<br>
			<br>
		</div><!-- /row -->
	</div><!-- /container -->

		
    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
		

	<script type="text/javascript" src="assets/js/bootstrap.min.js"></script>
	<script type="text/javascript" src="assets/js/retina.js"></script>
	<script type="text/javascript" src="assets/js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="assets/js/smoothscroll.js"></script>
	<script type="text/javascript" src="assets/js/jquery-func.js"></script>
  </body>
</html>
