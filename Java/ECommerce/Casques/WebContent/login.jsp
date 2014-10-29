<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
 <% 
	int nombreArticles=(Integer)request.getAttribute("nombreArticles");
 	boolean statusExists=true;
 	String status=null;
	
	if(session.getAttribute("statusLogin")==null)
		statusExists=false;
	
	else
		status=String.valueOf(session.getAttribute("statusLogin"));
 
 
 	//recuperer cookies pour les champs
 	Cookie[] cookies = request.getCookies();
 	boolean cookieExists = false;
    
	String user=null;
	String password=null;
 	
 	if(cookies.length>1){
 	
 		cookieExists = true;
 		
 		for(Cookie cookie: cookies)
 		{
 			if(cookie.getName().equals("user"))
 				user=cookie.getValue();
 			else if(cookie.getName().equals("password"))
 				password=cookie.getValue();
 		}
 	}
    
 	
%>   
    
<!DOCTYPE html>
<html lang="fr">
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="shortcut icon" href="assets/ico/favicon.png">

    <title>Login</title>

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
			<li> <a href="servletIndex?menu=categorie" class="smoothScroll"> Categories</a></li>
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
      </div>
    </div>
    </div>
   <!-- Fin Secteur Menu -->
  
  
  
  
  
		<br>
		<br>
		<br>
		<div class="container" id="contact" name="contact">
			<div class="row">
			<br>
				<h1 class="centered">BIENVENUE À LA PAGE DE LOGIN</h1>
				<hr>
				<br>
				<br>
				
				
				<div class="col-lg-12">
					<h3 class="centered">Fait votre login ici</h3>
					<%
						if (statusExists){
							session.removeAttribute("statusLogin");
							%>
							<p class="centered"><%=status%></p>
						<%} else{ %>
						
					<p class="centered">Entrez vos donnees, svp. Si vous n'avez pas de compte, vous pouvez le créer en appuyant sur le bouton "CREER".</p>
						<%} %>
						<div class="col-lg-4">
					
					</div><!-- col -->
					<div class="col-lg-4">
						<form action="servletIndex" method="post" form-horizontal" role="form">
						  <div class="form-group">
						    <label for="inputEmail1" class="col-lg-4 control-label"></label>
						    <div class="col-lg-10">
						      <input type="email" class="form-control" name="mail" id="inputEmail1" placeholder="Email" <%if(cookieExists){%>value=<%=user%><%} %>>
						    </div>
						  </div>
						  <div class="form-group">
						    <label for="text1" class="col-lg-4 control-label"></label>
						    <div class="col-lg-10">
						      <input type="password" class="form-control" name="password" id="text1" placeholder="Password" <%if(cookieExists){%>value=<%=password%><%} %>>
						    </div>
						  </div>
						  <div class="form-group">
						    <div class="col-lg-10">
						      <button value="access" name="menu" type="submit" class="btn btn-success">Connecter</button>
						      <button value="inscription" name="menu" type="submit" class="btn btn-default">Creer Compte</button>
						    </div>
						  </div>
					   </form><!-- form -->
				</div><!-- col -->
				
				<div class="col-lg-4">
				</div><!-- col -->

			</div><!-- row -->
		
		</div><!-- container -->
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
