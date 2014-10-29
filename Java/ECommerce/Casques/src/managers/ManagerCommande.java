package managers;

import java.math.BigDecimal;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Date;
import java.util.Hashtable;
import java.util.Iterator;
import java.util.Properties;
import java.util.Set;










import javax.mail.Message;
import javax.mail.MessagingException;
import javax.mail.PasswordAuthentication;
import javax.mail.Session;
import javax.mail.Transport;
import javax.mail.internet.InternetAddress;
import javax.mail.internet.MimeMessage;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import beans.Produit;
import beans.ProduitCommande;
import beans.User;
import Service.ConnecteurDB;

public class ManagerCommande {

	public static void creerCommande(HttpServletRequest request, HttpServletResponse response, ConnecteurDB connecteur, User user, Hashtable<Integer,ProduitCommande> lePanier){
		
		
		String sqlCommande="INSERT INTO commande (id_client) VALUES(?)";
	
			connecteur.ouvrirConnection();
			Connection connexion=connecteur.getConn();
			PreparedStatement pst=null;
			try {
				pst = connexion.prepareStatement(sqlCommande,PreparedStatement.RETURN_GENERATED_KEYS);
			} catch (SQLException e2) {
				// TODO Auto-generated catch block
				e2.printStackTrace();
			}
	
		
		try {
			
				//creer la commande dans la BD
				connexion.setAutoCommit(false);
				pst.setString(1,user.getEmail());		
				pst.executeUpdate();
				
				int cle=0;
				
				ResultSet rs = pst.getGeneratedKeys();
				if (rs.first()) {
					cle = rs.getInt(1);
				}
				
				//creer les lignesCommande dans la BD
				
				String sqlProduitCommande="INSERT INTO produitcommande (id_commande, id_produit, quantite) VALUES(?,?,?)";
				PreparedStatement pst2=connecteur.getPreparedStatement(sqlProduitCommande);
				
				String sqlStock="UPDATE produit SET quantite=quantite-? WHERE id=?";
				PreparedStatement pst3=connecteur.getPreparedStatement(sqlStock);
				
				//liste de tous les id des produits dans le panier 
				Set listeIdProduit=lePanier.keySet();
				//curseur
				Iterator ligne=listeIdProduit.iterator();
				
				ProduitCommande produitCommande;
				
				while (ligne.hasNext()) {
					
					produitCommande=lePanier.get(ligne.next());
					int id_produit=produitCommande.getProduit().getId();
					int qteProduit=produitCommande.getProduit().getQteProduit();
					int qteAchete=produitCommande.getQuantite();
					int nouveauStock=qteProduit-qteAchete;
					
					pst2.setInt(1,cle);
					pst2.setInt(2,id_produit);
					pst2.setInt(3,qteAchete);
					pst3.setInt(1,qteAchete);
					pst3.setInt(2,id_produit);
					pst2.executeUpdate();
					pst3.executeUpdate();
				}

				connexion.commit();
				envoyerCourriel(request, response, user, cle, lePanier);
		
		}catch (SQLException e) {
			e.printStackTrace();
			try {
				connexion.rollback();
			} catch (SQLException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}
		}
		
	}
	
	public static ArrayList<Produit> getListeHistorique(ConnecteurDB connecteur, String email)
	{	
		ArrayList<Produit> listeProduits=new ArrayList<Produit>();
		
		String sql="SELECT produitcommande.quantite AS qte, produit.nom AS nom, produit.prixVente as prix, produit.url as url FROM commande INNER JOIN produitcommande ON commande.id=produitcommande.id_commande INNER JOIN produit ON produit.id=produitcommande.id_produit WHERE commande.id_client=? ";
		 
		
		PreparedStatement pst=connecteur.getPreparedStatement(sql);
		
		try {
			
			pst.setString(1, email);
			ResultSet resultat=pst.executeQuery();
		
			
			while (resultat.next()) {	
				Produit leProduit=new Produit();
				leProduit.setNom(resultat.getString("nom"));
				leProduit.setQteProduit(resultat.getInt("qte"));
				leProduit.setPrixProduitVendu(resultat.getBigDecimal("prix"));
				leProduit.setUrl(resultat.getString("url"));
				
				listeProduits.add(leProduit);
				
			}
			
			
		} catch (SQLException e) {
			e.printStackTrace();
		}
		
		
		return listeProduits;
	
		
	}
	
	
public static void envoyerCourriel(HttpServletRequest request, HttpServletResponse response, User leUser, int noCommande,  Hashtable<Integer,ProduitCommande> lePanier) {
		
		Properties props = new Properties();
		props.put("mail.smtp.host", "mail.mobile-midia.com");
		props.put("mail.smtp.socketFactory.port", "587");
		props.put("mail.smtp.socketFactory.class",
				"javax.net.ssl.SSLSocketFactory");
		props.put("mail.smtp.auth", "true");
		props.put("mail.smtp.port", "587");
 
		Session session = Session.getDefaultInstance(props,
			new javax.mail.Authenticator() {
				protected PasswordAuthentication getPasswordAuthentication() {
					//Adresse mail et password de l emetteur sur le serveur smtp
					return new PasswordAuthentication("tp@mobile-midia.com","tp123");
				}
			});
 
		try {
			String leMessage = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" + 
					"<html>" + 
					"<head>" + 
					"	<style>/* ------------------------------------- " + 
					"		GLOBAL " + 
					"------------------------------------- */" + 
					"* { " + 
					"	margin:0;" + 
					"	padding:0;" + 
					"}" + 
					"* { font-family: \"Helvetica Neue\", \"Helvetica\", Helvetica, Arial, sans-serif; }" + 
					"" + 
					"img { " + 
					"	max-width: 100%; " + 
					"}" + 
					".collapse {" + 
					"	margin:0;" + 
					"	padding:0;" + 
					"}" + 
					"body {" + 
					"	-webkit-font-smoothing:antialiased; " + 
					"	-webkit-text-size-adjust:none; " + 
					"	width: 100%!important; " + 
					"	height: 100%;" + 
					"}" + 
					"" + 
					"" + 
					"/* ------------------------------------- " + 
					"		ELEMENTS " + 
					"------------------------------------- */" + 
					"a { color: #2BA6CB;}" + 
					"" + 
					".btn {" + 
					"	text-decoration:none;" + 
					"	color: #FFF;" + 
					"	background-color: #666;" + 
					"	padding:10px 16px;" + 
					"	font-weight:bold;" + 
					"	margin-right:10px;" + 
					"	text-align:center;" + 
					"	cursor:pointer;" + 
					"	display: inline-block;" + 
					"}" + 
					"" + 
					"p.callout {" + 
					"	padding:15px;" + 
					"	background-color:#ECF8FF;" + 
					"	margin-bottom: 15px;" + 
					"}" + 
					".callout a {" + 
					"	font-weight:bold;" + 
					"	color: #2BA6CB;" + 
					"}" + 
					"" + 
					"table.social {" + 
					"/* 	padding:15px; */" + 
					"	background-color: #ebebeb;" + 
					"	" + 
					"}" + 
					".social .soc-btn {" + 
					"	padding: 3px 7px;" + 
					"	font-size:12px;" + 
					"	margin-bottom:10px;" + 
					"	text-decoration:none;" + 
					"	color: #FFF;font-weight:bold;" + 
					"	display:block;" + 
					"	text-align:center;" + 
					"}" + 
					"a.fb { background-color: #3B5998!important; }" + 
					"a.tw { background-color: #1daced!important; }" + 
					"a.gp { background-color: #DB4A39!important; }" + 
					"a.ms { background-color: #000!important; }" + 
					"" + 
					".sidebar .soc-btn { " + 
					"	display:block;" + 
					"	width:100%;" + 
					"}" + 
					"" + 
					"/* ------------------------------------- " + 
					"		HEADER " + 
					"------------------------------------- */" + 
					"table.head-wrap { width: 100%;}" + 
					"" + 
					".header.container table td.logo { padding: 15px; }" + 
					".header.container table td.label { padding: 15px; padding-left:0px;}" + 
					"" + 
					"" + 
					"/* ------------------------------------- " + 
					"		BODY " + 
					"------------------------------------- */" + 
					"table.body-wrap { width: 100%;}" + 
					"" + 
					"" + 
					"/* ------------------------------------- " + 
					"		FOOTER " + 
					"------------------------------------- */" + 
					"table.footer-wrap { width: 100%;	clear:both!important;" + 
					"}" + 
					".footer-wrap .container td.content  p { border-top: 1px solid rgb(215,215,215); padding-top:15px;}" + 
					".footer-wrap .container td.content p {" + 
					"	font-size:10px;" + 
					"	font-weight: bold;" + 
					"	" + 
					"}" + 
					"" + 
					"" + 
					"/* ------------------------------------- " + 
					"		TYPOGRAPHY " + 
					"------------------------------------- */" + 
					"h1,h2,h3,h4,h5,h6 {" + 
					"font-family: \"HelveticaNeue-Light\", \"Helvetica Neue Light\", \"Helvetica Neue\", Helvetica, Arial, \"Lucida Grande\", sans-serif; line-height: 1.1; margin-bottom:15px; color:#000;" + 
					"}" + 
					"h1 small, h2 small, h3 small, h4 small, h5 small, h6 small { font-size: 60%; color: #6f6f6f; line-height: 0; text-transform: none; }" + 
					"" + 
					"h1 { font-weight:200; font-size: 44px;}" + 
					"h2 { font-weight:200; font-size: 37px;}" + 
					"h3 { font-weight:500; font-size: 27px;}" + 
					"h4 { font-weight:500; font-size: 23px;}" + 
					"h5 { font-weight:900; font-size: 17px;}" + 
					"h6 { font-weight:900; font-size: 14px; text-transform: uppercase; color:#444;}" + 
					"" + 
					".collapse { margin:0!important;}" + 
					"" + 
					"p, ul { " + 
					"	margin-bottom: 10px; " + 
					"	font-weight: normal; " + 
					"	font-size:14px; " + 
					"	line-height:1.6;" + 
					"}" + 
					"p.lead { font-size:17px; }" + 
					"p.last { margin-bottom:0px;}" + 
					"" + 
					"ul li {" + 
					"	margin-left:5px;" + 
					"	list-style-position: inside;" + 
					"}" + 
					"" + 
					"/* ------------------------------------- " + 
					"		SIDEBAR " + 
					"------------------------------------- */" + 
					"ul.sidebar {" + 
					"	background:#ebebeb;" + 
					"	display:block;" + 
					"	list-style-type: none;" + 
					"}" + 
					"ul.sidebar li { display: block; margin:0;}" + 
					"ul.sidebar li a {" + 
					"	text-decoration:none;" + 
					"	color: #666;" + 
					"	padding:10px 16px;" + 
					"/* 	font-weight:bold; */" + 
					"	margin-right:10px;" + 
					"/* 	text-align:center; */" + 
					"	cursor:pointer;" + 
					"	border-bottom: 1px solid #777777;" + 
					"	border-top: 1px solid #FFFFFF;" + 
					"	display:block;" + 
					"	margin:0;" + 
					"}" + 
					"ul.sidebar li a.last { border-bottom-width:0px;}" + 
					"ul.sidebar li a h1,ul.sidebar li a h2,ul.sidebar li a h3,ul.sidebar li a h4,ul.sidebar li a h5,ul.sidebar li a h6,ul.sidebar li a p { margin-bottom:0!important;}" + 
					"" + 
					"" + 
					"" + 
					"/* --------------------------------------------------- " + 
					"		RESPONSIVENESS" + 
					"		Nuke it from orbit. It's the only way to be sure. " + 
					"------------------------------------------------------ */" + 
					"" + 
					"/* Set a max-width, and make it display as block so it will automatically stretch to that width, but will also shrink down on a phone or something */" + 
					".container {" + 
					"	display:block!important;" + 
					"	max-width:600px!important;" + 
					"	margin:0 auto!important; /* makes it centered */" + 
					"	clear:both!important;" + 
					"}" + 
					"" + 
					"/* This should also be a block element, so that it will fill 100% of the .container */" + 
					".content {" + 
					"	padding:15px;" + 
					"	max-width:600px;" + 
					"	margin:0 auto;" + 
					"	display:block; " + 
					"}" + 
					"" + 
					"/* Let's make sure tables in the content area are 100% wide */" + 
					".content table { width: 100%; }" + 
					"" + 
					"" + 
					"/* Odds and ends */" + 
					".column {" + 
					"	width: 300px;" + 
					"	float:left;" + 
					"}" + 
					".column tr td { padding: 15px; }" + 
					".column-wrap { " + 
					"	padding:0!important; " + 
					"	margin:0 auto; " + 
					"	max-width:600px!important;" + 
					"}" + 
					".column table { width:100%;}" + 
					".social .column {" + 
					"	width: 280px;" + 
					"	min-width: 279px;" + 
					"	float:left;" + 
					"}" + 
					"" + 
					"/* Be sure to place a .clear element after each set of columns, just to be safe */" + 
					".clear { display: block; clear: both; }" + 
					"" + 
					"" + 
					"/* ------------------------------------------- " + 
					"		PHONE" + 
					"		For clients that support media queries." + 
					"		Nothing fancy. " + 
					"-------------------------------------------- */" + 
					"@media only screen and (max-width: 600px) {" + 
					"	" + 
					"	a[class=\"btn\"] { display:block!important; margin-bottom:10px!important; background-image:none!important; margin-right:0!important;}" + 
					"" + 
					"	div[class=\"column\"] { width: auto!important; float:none!important;}" + 
					"	" + 
					"	table.social div[class=\"column\"] {" + 
					"		width:auto!important;" + 
					"	}" + 
					"" + 
					"}</style>" + 
					"<!-- If you delete this meta tag, the ground will open and swallow you. -->" + 
					"<meta name=\"viewport\" content=\"width=device-width\" />" + 
					"" + 
					"<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />" + 
					"<title>CASQUES JAVA II</title>" + 
					"	" + 
					"<link rel=\"stylesheet\" type=\"text/css\" href=\"stylesheets/email.css\" >" + 
					"" + 
					"</head>" + 
					" " + 
					"<body bgcolor=\"#FFFFFF\" topmargin=\"0\" leftmargin=\"0\" marginheight=\"0\" marginwidth=\"0\">" + 
					"" + 
					"<!-- HEADER -->" + 
					"<table class=\"head-wrap\" bgcolor=\"#999999\">" + 
					"	<tr>" + 
					"		<td></td>" + 
					"		<td class=\"header container\" align=\"\">" + 
					"			" + 
					"			<!-- /content -->" + 
					"			<div class=\"content\">" + 
					"				<table bgcolor=\"#999999\" >" + 
					"					" + 
					"				</table>" + 
					"			</div><!-- /content -->" + 
					"			" + 
					"		</td>" + 
					"		<td></td>" + 
					"	</tr>" + 
					"</table><!-- /HEADER -->" + 
					"" + 
					"<!-- BODY -->" + 
					"<table class=\"body-wrap\" bgcolor=\"\">" + 
					"	<tr>" + 
					"		<td></td>" + 
					"		<td class=\"container\" align=\"\" bgcolor=\"#FFFFFF\">" + 
					"			" + 
					"			<!-- content -->" + 
					"			<div class=\"content\">" + 
					"				<table>" + 
					"					<tr>" + 
					"						<td>" + 
					"							" + 
					"							<h2>"+leUser.getNom()+"</h2>" +
					 "                           <p><span>No de commande : "+noCommande+"</span></p>" + 
	                                            
	"				                              <p><span>Date dachat : "+new Date()+"</span><br/></p>" + 
	                                               
					"							<p class=\"lead\">Nous avons le plaisir de vous informer l'enregistrement de votre commande dont vous trouverez le details ci dessous</p>" + 
					"							" + 
					"							<!-- A Real Hero (and a real human being) -->" + 
					"							<p><img src=\"http://i.imgur.com/KMQ8HMM.jpg\" /></p><!-- /hero -->" + 
					"							<h1>Detail de votre commande</h1>" + 
					"						</td>" + 
					"					</tr>" + 
					"				</table>" + 
					"			</div><!-- /content -->" + 
					"			" + 
					"			<!-- content -->";
			        Set listeIdProduit=lePanier.keySet();
                   //curseur
                  Iterator ligne=listeIdProduit.iterator();

                 ProduitCommande produitCommande;
                 BigDecimal total = new BigDecimal(0);
                  while (ligne.hasNext()) {
                  produitCommande=lePanier.get(ligne.next());
                  leMessage+=
					"			<div class=\"content\"><table bgcolor=\"\">" + 
					"				<tr>" + 
					"					<td>" + 
					"						<h3>"+produitCommande.getQuantite()+" x "+produitCommande.getProduit().getNom()+" = $"+produitCommande.getProduit().getPrixProduitCoutant().multiply(new BigDecimal(produitCommande.getQuantite()))+"</h3>" + 
					                         
					"						<hr>"+
	
					"				" + 
					"					</td>" + 
					"				</tr>" + 
					"			</table></div><!-- /content -->";
					total = total.add(produitCommande.getProduit().getPrixProduitCoutant().multiply(new BigDecimal(produitCommande.getQuantite())));
                  }
				
                  leMessage+=
					"		</td>" + 
					"		<td></td>" + 
					"	</tr>" + 
					"</table><!-- /BODY -->" + 
					"</hr>"+
					"<h1>Le prix Total de votre commande est $"+total +"</h1>"+
					"</body>" + 
					"</html>";
					
				
			
			Message message = new MimeMessage(session);
			//adresse mail de l emetteur
			message.setFrom(new InternetAddress("tp@mobile-midia.com"));
			//adresse destinataire
			message.setRecipients(Message.RecipientType.TO,
					InternetAddress.parse("candidopala@gmail.com"));
			message.setSubject("Votre Commande");
			message.setContent(leMessage, "text/html; charset=utf-8");
	
			Transport.send(message);
 
		} catch (MessagingException e) {
			throw new RuntimeException(e);
		}
	}
	
	
}
