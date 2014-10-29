package Actions;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import beans.User;
import managers.ManagerLogin;
import Service.ConnecteurDB;

public class ActionLogin {

	
	public static void verification(HttpServletRequest request, HttpServletResponse response, String mail, String password)
	{
	
		ConnecteurDB connecteur=new ConnecteurDB("com.mysql.jdbc.Driver", "jdbc:mysql://localhost:3306/tp", "root", "");
		
		User leUser=ManagerLogin.verifierUser(connecteur, mail, password);
		
		HttpSession session=request.getSession();
		//retourner pour la provenance
		String provenance=String.valueOf(session.getAttribute("categorie"));
		
		switch(provenance)
		{
			case "retro": provenance="servletAfficherProduits?id=retro";
			break;
			case "custom": provenance="servletAfficherProduits?id=custom";
			break;
			case "sport": provenance="servletAfficherProduits?id=sport";
			break;	
		}
		
		if(leUser!=null)
		{
			//creer un user dans la session
			session.setAttribute("user", leUser);
			//creation d'un cookie pour le user
			Cookie user=new Cookie("user", mail);
			Cookie mdp=new Cookie("password", password);
			user.setMaxAge(30 * 24 * 60 * 60); //1 mois
			mdp.setMaxAge(30 * 24 * 60 * 60);	//1 mois
			response.addCookie(user);
			response.addCookie(mdp);
			
		}
		
		else
		{
			provenance="login.jsp";
			session.setAttribute("statusLogin", "Faille de Login. Reesayez encore une fois, svp.");
		}
		
		
		
		try {
			
			request.getRequestDispatcher(provenance).forward(request, response);
			
			
		} catch (ServletException e) {
			// TODO Auto-generated catch block
			
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} finally
		{
			connecteur.fermerConnection();
		}
		
		
	}	
	
}
