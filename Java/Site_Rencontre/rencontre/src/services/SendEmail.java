package services;


	import java.util.*;

import javax.mail.*;
import javax.mail.internet.*;
import javax.activation.*;

import pkgEntities.Membre;

	public class SendEmail
	{

	   public static void envoyerMail(Membre membreFaitAction, Membre membreRecoitAction, String type)
	   {    
		   String leMessage=null;
		   String sujet=null;
		   
		   switch(type)
		   {
		   	case "ajoute": 	leMessage=membreRecoitAction.getPseudo()+". Le user "+membreFaitAction.getPseudo()+" vous a ajoute dans ISI Love.";
		   					sujet="ISI LOVE: Vous etes un favori";
		   	break;
		   	case "supprime": leMessage=membreRecoitAction.getPseudo()+". Le user "+membreFaitAction.getPseudo()+" vous a supprime dans ISI Love.";
							sujet="ISI LOVE: Vous avez ete supprime";
		   	break;
		   	case "message": leMessage=membreRecoitAction.getPseudo()+". Le user "+membreFaitAction.getPseudo()+" t'as envoye un message.";
		   					sujet="ISI LOVE: nouveau message";
		   	break;
		   }
				   
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
				
				Message message = new MimeMessage(session);
				//adresse mail de l emetteur
				message.setFrom(new InternetAddress("tp@mobile-midia.com"));
				//adresse destinataire
				message.setRecipients(Message.RecipientType.TO,InternetAddress.parse(membreRecoitAction.getEmail()));
				message.setSubject(sujet);
				message.setContent(leMessage, "text/html; charset=utf-8");
		
				Transport.send(message);
	 
			} catch (MessagingException e) {
				throw new RuntimeException(e);
			}
	     }
	}
	
	

