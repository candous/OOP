
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@page import="java.util.*"%>
<%@page import="beans.*"%>
<%@ page import="java.math.BigDecimal"%>
  
 <% 
	int nombreArticles=(Integer)request.getAttribute("nombreArticles");
	 ArrayList<Produit> listeHistorique=new  ArrayList<Produit>();
	 
	boolean historique=false;
 	
	if (request.getAttribute("listeProduitsHistoriques")!=null)
	{
 		historique=true;
 		listeHistorique=(ArrayList<Produit>)request.getAttribute("listeProduitsHistoriques");
	}
 		
 	User user=(User)request.getSession().getAttribute("user");
 	String nom=user.getNom();
%>     
    
    
    
<!DOCTYPE html>
<html lang="fr">
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="shortcut icon" href="assets/ico/favicon.png">
    <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css'>
		<link rel="stylesheet" type="text/css" href="assets2/css/bootstrap.min.css"/>
		<link rel="stylesheet" type="text/css" href="assets2/css/custom.css"/>	

    <title>Historique</title>

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
          <ul class="nav navbar-nav">
            <li><a href="servletIndex?menu=home" class="smoothScroll">Home</a></li>
			<li> <a href="servletIndex?menu=categorie" class="smoothScroll">Categories</a></li>
			 
			 </ul>
			 <form method = "get" action = "servletRecherche">
			<ul class="nav navbar-nav navbar-right">
			   <li> <a  class="smoothScroll"></span> <input type = "text" name = "indice"/></a></li>
			   </ul>
			    </form>
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
        </div><!--/.nav-collapse -->
        </div><!--/.nav-collapse -->
      </div>
    </div>
   <!-- Fin Secteur Menu -->
	<br>
		<br>
		<br>
		<div class="container" id="contact" name="contact">
			<div class="row">
			<br>
			
				<h1 class="centered">HISTORIQUE D'ACHATS</h1>
				<hr>
				<br>
				<br>
				<%if(historique) {%>
				<div class="col-lg-12">
					<h3 class="centered">Votre historique d'achats</h3>
					<hr>
				</div>
		<div class="container text-center">

			<div class="col-md-3 col-sm-0 text-left">
			</div>
			<div class="col-md-6 col-sm-12 text-left">
				<br>
				<ul>
				
			<%for(Produit produit: listeHistorique){%>
					<li class="row">
						<span class="quantity"> <input type="text" name="quantite"  style="border: none" value="<%=produit.getQteProduit()%>"/></span>
						<span class="itemName"><%=produit.getNom()%></span>
					
						<span class="price">$ <%=produit.getPrixProduitVendu().multiply(new BigDecimal(produit.getQteProduit()))%></span>
					</li>	     
					<%} %>
				</ul>
		
				</div>

			</div>
					
		<% } else {%>
		<div class="col-lg-12">
					<h3 class="centered">Vous n'avez pas de produits</h3>
				</div>
		<br><br><br>
		<div class="container text-center">

			<div class="col-md-3 col-sm-0 text-left">
			</div>
			<div class="col-md-6 col-sm-12 text-left">
				<ul>
					<li class="row">
						<span class="quantity">0</span>
						<a href="servletAfficherProduits?id=retro" ><span class="itemName">Casques Rétro</span></a>
						<span class="price">$0.00</span>
					</li>
					<li class="row">
						<span class="quantity">0</span>
						<a  href="servletAfficherProduits?id=custom" ><span class="itemName">Casques Custom</span></a>
						<span class="price">$0.00</span>
					</li>
					<li class="row">
						<span class="quantity">0</span>
						<a  href="servletAfficherProduits?id=sport" ><span class="itemName">Casques Sportives</span></a>
						<span class="price">$0.00</span>
					</li>				
				</ul>
			</div>
			</div>
		<%} %>
		
	
		

	<script type="text/javascript" src="assets/js/bootstrap.min.js"></script>
	<script type="text/javascript" src="assets/js/retina.js"></script>
	<script type="text/javascript" src="assets/js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="assets/js/smoothscroll.js"></script>
	<script type="text/javascript" src="assets/js/jquery-func.js"></script>
  </body>
</html>