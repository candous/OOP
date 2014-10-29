<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
 <% 
	int nombreArticles=(Integer)request.getAttribute("nombreArticles");	
	
	boolean userExists=true;
 	String status=null;
	
	if(session.getAttribute("userCree")==null)
		userExists=false;
	
	else
		status=String.valueOf(session.getAttribute("userCree"));
 
	request.getSession().removeAttribute("userCree");
%> 
<!DOCTYPE html>
<html lang="fr">
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="shortcut icon" href="assets/ico/favicon.png">

    <title> Inscription</title>

    <!-- Bootstrap core CSS -->
    <link href="assets/css/bootstrap.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="assets/css/main.css" rel="stylesheet">
    <link rel="stylesheet" href="assets/css/icomoon.css">
    <link href="assets/css/animate-custom.css" rel="stylesheet">


    
    <link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,300italic,400italic' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Raleway:400,300,700' rel='stylesheet' type='text/css'>
    
    <script src="assets/js/jquery.min.js"></script>
	<script type="text/javascript" src="assets/js/modernizr.custom.js"></script>

<script>
$(document).ready(function(){
	var emailReg = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
	
  $("#inputEmail1").keyup(function(){
	  var valid = emailReg.test(($(this).val()));
	  $("#text1").removeAttr("disabled");
	  if(!valid)
		  $("#text1").attr("disabled", "disabled");
  });
  
});
</script>
    
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="assets/js/html5shiv.js"></script>
      <script src="assets/js/respond.min.js"></script>
    <![endif]-->
  </head>

  <body data-spyin="scroll" data-offset="0" data-target="#navbar-main">
  
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
            <li><a href="#home" class="smoothScroll">Home</a></li>
			<li> <a href="servletIndex?menu=categorie" class="smoothScroll"> Categories</a></li>
			<li> <a href="servletIndex?menu=login" class="smoothScroll">Login</a></li>
			</ul>
			<form method = "get" action = "servletRecherche">
			<ul class="nav navbar-nav navbar-right">
			   <li> <a  class="smoothScroll"></span> <input type = "text" name = "indice"/></a></li>
			   </ul>
			    </form>
			<ul class="nav navbar-nav navbar-right">
			<li> <a href="servletIndex?menu=panier" class="smoothScroll"> <span class="glyphicon glyphicon-tag" style="font-size:18px; color:gray;"></span> Panier(<%=nombreArticles%>)</a></li>
			   </ul>
        </div><!--/.nav-collapse -->
        </div><!--/.nav-collapse -->
      </div>
    </div>
    </div>
   <!-- Fin Secteur Menu -->
  
  
  
  
  
		</br>
		</br>
		</br>
		<div class="container" id="contact" name="contact">
			<div class="row">
			<br>
				<h1 class="centered">Inscription</h1>
				<hr>
				<br>
				<br>
				
				<div class="col-lg-12">
				<% if(!userExists){ %>
					<h3 class="centered">Creez votre compte ici</h3>
					<%} else{ %>
					<h3 class="centered"><%=status %></h3>
					<%} %>
					
					<p class="centered">* Tous les champs précedés d'une étoile sont obligatoires. </p>
					<div class="col-lg-4">
					
					</div>
					<div class="col-lg-4">
						<form action="servletInscription" method="post" class="form-horizontal">
						  <div class="form-group">
						    <label for="inputEmail1" class="col-lg-4 control-label"></label>
						    <div class="col-lg-10">
						      <input type="email" class="form-control" id="inputEmail1" name="email" placeholder="* Email">
						    </div>
						  </div>
						  <div class="form-group">
						    <label for="nom" class="col-lg-4 control-label"></label>
						    <div class="col-lg-10">
						      <input type="text" class="form-control" name="nom"placeholder="* Nom">
						    </div>
						  </div>
						  <div class="form-group">
						    <label for="password" class="col-lg-4 control-label"></label>
						    <div class="col-lg-10">
						      <input type="password" class="form-control"  name="password"placeholder="* Password">
						    </div>
						  </div>
						  <div class="form-group">
						    <label for="adresse" class="col-lg-4 control-label"></label>
						    <div class="col-lg-10">
						      <input type="text" class="form-control"  name="adresse"placeholder="* Adresse">
						    </div>
						  </div>
						  <div class="form-group">
						    <label for="telephone" class="col-lg-4 control-label"></label>
						    <div class="col-lg-10">
						      <input type="text" class="form-control"  name="telephone"placeholder="Telephone">
						    </div>
						  </div>
					
						  <div class="form-group">
						    <div class="col-lg-10">
						      <button type="submit" id = "text1" class="btn btn-success">Inscription</button>
						    </div>
						  </div>
					   </form><!-- form -->
					
				</div><!-- col -->
				</div>
				
				<div class="col-lg-4">
				</div><!-- col -->

			</div><!-- row -->
		
		</div><!-- container -->

		

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
