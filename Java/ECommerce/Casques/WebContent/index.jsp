<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ page import="beans.*" %>

<% 
	int nombreArticles=(Integer)request.getAttribute("nombreArticles");
	request.getSession().setAttribute("categorie", "index.jsp");
	
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

    <title> Bienvenue </title>

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
            <li><a href="#home" class="smoothScroll">Home</a></li>
			<li> <a href="#categorie" class="smoothScroll">Categories</a></li>
			
			<%if(!userExists) {%>
			<li> <a href="servletIndex?menu=login" class="smoothScroll">Login</a></li></ul>
			<ul class="nav navbar-nav navbar-right">
			<li> <a href="servletIndex?menu=panier" class="smoothScroll"> <span class="glyphicon glyphicon-tag" style="font-size:18px; color:gray;"></span > Panier<span id = "article">(<%=nombreArticles%>)</span></a></li>
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
			<li> <a href="servletIndex?menu=panier" class="smoothScroll"> <span class="glyphicon glyphicon-tag" style="font-size:18px; color:gray;"></span id = "nbarticle"> Panier(<%=nombreArticles%>)</a></li>
			 </ul>
			 <%} %>
			 
        </div><!--/.nav-collapse -->
        </div><!--/.nav-collapse -->
      </div>
    </div>
  
   <!-- Fin Secteur Menu -->
  
  
  
  
  
		<!-- ==== HEADERWRAP ==== -->
	    <div id="headerwrap" id="home" name="home">
			<header class="clearfix">
	  		 		
					<!-- Fin Les deux paragraphes sur le logo -->
					
	  		</header>	    
	    </div><!-- /headerwrap -->

		<!-- ==== GREYWRAP ==== -->
		<div id="greywrap">
			<div class="row">
			
			<!-- Description1 -->
				<div class="col-lg-4 callout">
					<span class="icon icon-stack"></span>
					<h2>Retro</h2>
					<p>Le casque vintage est devenu un véritable accessoire "de mode" pour le motard voulant appuyer le look vintage et rétro avec un casque unique et original. </p>
				</div><!-- col-lg-4 -->
		    <!-- Fin Description1 -->

			<!-- Description2 -->
				<div class="col-lg-4 callout">
					<span class="icon icon-eye"></span>
					<h2>Custom</h2>
					<p>Un casque de moto est un casque destiné à la pratique de la moto. Il a pour vocation de protéger la tête du conducteur en amortissant le choc avec le sol. </p>
				</div><!-- col-lg-4 -->	
			<!-- Fin Description2 -->
			
			<!-- Description3 -->
				<div class="col-lg-4 callout">
					<span class="icon icon-heart"></span>
					<h2>Sport</h2>
					<p>Les casques sports sont moins volumineux, en offrant un levier moins important aux forces tangentielles lors d'un impact, sont moins dangereux pour la colonne cervicale. </p>
				</div><!-- col-lg-4 -->	
			<!-- Fin Description3 -->
			</div><!-- row -->
		</div><!-- greywrap -->
		
		<!-- ==== PORTFOLIO ==== -->
		<div class="container" id="categorie" name="categorie">
		<br>
			<div class="row">
				<br>
				<h1 class="centered">NOS CATÉGORIES</h1>
				<hr>
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

		<!-- ==== SECTION DIVIDER4 ==== -->
		<section class="section-divider textdivider divider4">
			<div class="container">
				<h1>Toute une vie de casque</h1>
				<hr>
				<p>Depuis 30 ans nous frabriquons des casques divers categories et de differentes tailles dans une ambiance epoustouflante</p>
			</div><!-- container -->
		</section><!-- section -->
		
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
