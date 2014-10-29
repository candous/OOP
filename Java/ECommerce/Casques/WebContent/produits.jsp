<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
 <%@ page import="java.util.*" %>
 <%@ page import="beans.*" %>
 <%@ page import="java.math.BigDecimal"%>

 <% 
	int nombreArticles=(Integer)request.getAttribute("nombreArticles");
	ArrayList<Produit> lesProduits=new ArrayList<Produit>();
	lesProduits=(ArrayList<Produit>)request.getAttribute("listeProduits");
	
	request.getSession().setAttribute("categorie", request.getAttribute("categorie"));

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

    <title>Produits</title>

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
	<script>
$(document).ready(function(){
  $("input").keyup(function(){
	  $(this).next().removeAttr("disabled");
	  if( parseInt($(this).val()) > parseInt($(this).next().next().val()) || !($(this).val().match(/^[0-9]{1,10}$/)))
		  $(this).next().attr("disabled", "disabled");
  });
});
</script>
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
			<li class="dropdown" class="smoothScroll">
            <a data-toggle="dropdown" class="dropdown-toggle" href="servletIndex?menu=categorie">Categories<b class="caret"></b></a>
            <ul class="dropdown-menu">
            <li><a  href="servletAfficherProduits?id=retro" >Retro</a></li>
            <li><a  href="servletAfficherProduits?id=custom" >Custom</a></li>
            <li><a  href="servletAfficherProduits?id=sport" >Sport</a></li>
        </ul>
    </li>	
			
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
  


	<br>
	<br>
	<br>
	<div class="container" id="categorie" name="categorie">
		<br>
			<div class="row">
				<br>
				<h1 class="centered"><%=request.getAttribute("categorieAffichage") %></h1>
				<hr>
				<br>
				<br>
			</div><!-- /row -->
		</div>
			<div class="container">
			<div class="row">	
			
			<%	int cpt=0;
				for (Produit produit:lesProduits) {	cpt++;%>
				<!-- PORTFOLIO IMAGE 1 -->
				<div class="col-md-3">
			    	<div class="grid mask">
						<figure>
							<a data-toggle="modal" href="#<%=cpt%>" ><img class="img-responsive" src="<%=produit.getUrl()%>" alt="" ></a>
							<figcaption>
								<%=produit.getNom() %>
							</figcaption>
						</figure><!-- /figure -->
			    	</div><!-- /grid-mask -->
				</div><!-- /col -->
				
				
						      
						  <div class="modal fade" id=<%=cpt%> tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
						    <div class="modal-dialog">
						      <div class="modal-content">
						        <div class="modal-header">
						        
						          <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
						          <h4 class="modal-title"><%=produit.getNom()%></h4><h4><%=produit.getPrixProduitVendu() %>$</h4>
						          <form action="servletGererPanier" method="post">
						         	<input type="hidden" name="id" value="<%=produit.getId()%>"> 
						         	<input type="hidden" name="nom" value="<%=produit.getNom()%>">
						         	<input type="hidden" name="description" value="<%=produit.getDescriptionProduit()%>"> 
						         	<input type="hidden" name="cout" value="<%=produit.getPrixProduitCoutant()%>">  
						         	<input type="hidden" name="prix" value="<%=produit.getPrixProduitVendu()%>">
						         	<input type="hidden" id="stock" name="stock" value="<%=produit.getQteProduit()%>"> 
						         	<input type="hidden" name="url" value="<%=produit.getUrl()%>">
						         	<input type="hidden" name="categorie" value="<%=produit.getNoCategorie()%>"> 
									<input class="text-right" id="qte" type="text" class="col-md-3" name="quantite"/>
									<input class="text-right"  class="btn btn-default" type="submit" name="bouton" value="Ajouter"/>
									<input type="hidden" value = "<%=produit.getQteProduit()%>">
								</form>
						        </div>
						        <div class="modal-body">
						          <p><img class="img-responsive" src="<%=produit.getUrl()%>" alt=""></p>
						          <p><%=produit.getDescriptionProduit() %></p>
						        </div>
						        <div class="modal-footer">
						          <button type="button" class="btn btn-default" data-dismiss="modal">Fermer</button>
						        </div>
						      </div><!-- /.modal-content -->
						    </div><!-- /.modal-dialog -->
						  </div><!-- /.modal -->
				<% } %>
				</div>
				</div>
			

		
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