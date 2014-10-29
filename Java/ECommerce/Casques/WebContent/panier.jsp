<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
 <%@page import="java.util.*"%>
 <%@page import="beans.*"%>
 <%@ page import="java.math.BigDecimal"%>
  
 <% 
	int nombreArticles=(Integer)request.getAttribute("nombreArticles");
 
	String provenance="panier.jsp";
 	request.getSession().setAttribute("categorie", provenance);
 	
 	Hashtable<Integer,ProduitCommande> lePanier=(Hashtable<Integer,ProduitCommande>)session.getAttribute("LePanier");
 	
 	
 	boolean panierExiste=false;
 	
 	if(nombreArticles>0)
 		panierExiste=true;
 	
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
    <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css'>
		<link rel="stylesheet" type="text/css" href="assets2/css/bootstrap.min.css"/>
		<link rel="stylesheet" type="text/css" href="assets2/css/custom.css"/>	

    <title>Panier</title>

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
        </div>
        </div><!--/.nav-collapse -->
      </div>
    </div>
 
   <!-- Fin Secteur Menu -->


		
		<section class="section-divider textdivider divider1">
			<div class="container">
			<%if(panierExiste){ %>
				<h1>VOS ACHATS SONT PRÊTS À COMMANDER</h1>
				<hr>
				<p>There’s more to design than meets the eye. It’s when it meets the heart that design creates a meaningful, lasting connection with the audience.</p>
				<%if(userExists){ %>
  				<a href="servletAfficherHistorique"><button type="button" class="btn btn-default" >HISTORIQUE D'ACHATS</button></a>
  				<%} %>
				
				<%}  else{%>
				<h1>VOUS N'AVEZ PAS DE PRODUITS </h1>
				<hr>
				<p>Vous etes bienvenue pour acheter nos produits, allez dans la session catégories et amusez-vous.</p>
				<div class="btn-group">
  				<button type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown">VOIR CATEGORIES <span class="caret"></span>
  				</button>
				  <ul class="dropdown-menu" role="menu">
				    <li><a href="servletAfficherProduits?id=retro">Rétro</a></li>
				    <li><a href="servletAfficherProduits?id=custom">Custom</a></li>
				    <li><a href="servletAfficherProduits?id=sport">Sport</a></li>
				  </ul>
				</div>
				<%if(userExists){ %>
  				<a href="servletAfficherHistorique"><button type="button" class="btn btn-default" >HISTORIQUE D'ACHATS</button></a>
  				<%} %>

				<%} %>
			</div><!-- container -->
		</section><!-- section -->




		<%if(panierExiste){ %>
		
		<div class="container text-center">

			<div class="col-md-3 col-sm-0 text-left">
			</div>
			<div class="col-md-6 col-sm-12 text-left">
				<ul>
				<br>
			<%
				BigDecimal prixTotal=new BigDecimal(0);
				//liste de tous les id des produits dans le panier 
				Set listeIdProduit=lePanier.keySet();
				//curseur
				Iterator ligne=listeIdProduit.iterator();
				
				ProduitCommande produitCommande;
				
				while (ligne.hasNext()) {
					produitCommande=lePanier.get(ligne.next());
					 %>
				 <form action="servletGererPanier" method="post">
					<li class="row">
						<span class="quantity"><input  type="text" name="quantite"  style="border: none" value="<%=produitCommande.getQuantite()%>"/></span> 
						<span class="itemName"><%=produitCommande.getProduit().getNom()%></span>
						<% BigDecimal total=produitCommande.getProduit().getPrixProduitVendu().multiply(new BigDecimal(produitCommande.getQuantite()));%>
						
							<span class="price"><input   class="btn btn-default" type="submit" name="bouton" value="MAJ"/></span>
						         	<span class="price"><input class="text-right"  class="btn btn-default" type="submit" name="bouton" value="Delete"/></span>
						         	<span class="price">$ <%=produitCommande.getProduit().getPrixProduitVendu().multiply(new BigDecimal(produitCommande.getQuantite()))%></span>
							<input type="hidden" name="id" value="<%=produitCommande.getProduit().getId()%>"> 
						         	<input type="hidden" name="nom" value="<%=produitCommande.getProduit().getNom()%>">
						         	<input type="hidden" name="description" value="<%=produitCommande.getProduit().getDescriptionProduit()%>"> 
						         	<input type="hidden" name="cout" value="<%=produitCommande.getProduit().getPrixProduitCoutant()%>">  
						         	<input type="hidden" name="prix" value="<%=produitCommande.getProduit().getPrixProduitVendu()%>">
						         	<input type="hidden" id="stock" name="stock" value="<%=produitCommande.getProduit().getQteProduit()%>"> 
						         	<input type="hidden" name="url" value="<%=produitCommande.getProduit().getUrl()%>">
						         	<input type="hidden" name="categorie" value="<%=produitCommande.getProduit().getNoCategorie()%>"> 
						         	
					</li>	     	
						</form>
					<%
					prixTotal=prixTotal.add(total);	
					} %>
				
				<li class="row totals"> 
 						<span class="itemName">Total:</span>
						<span class="price">$ <%=prixTotal%></span>
						<span class="order"> <a href="servletCommande" class="text-center">COMMANDER</a></span>		
 				</li>
 				
				</ul>
		
				</div>

			</div>
					
		<% } else {%>
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
