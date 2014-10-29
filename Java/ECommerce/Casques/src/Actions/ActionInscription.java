package Actions;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import Service.ConnecteurDB;
import beans.User;
import managers.ManagerLogin;

public class ActionInscription {

	
	public static void creerUser(HttpServletRequest request,HttpServletResponse response, String email,String nom,String password,String adresse,String telephone)
	{
		String destination=null;
		ConnecteurDB connecteur=new ConnecteurDB("com.mysql.jdbc.Driver", "jdbc:mysql://localhost:3306/tp", "root", "");
		
		boolean userExist=ManagerLogin.verifierUserBD(connecteur, email);
		
		if(!userExist)
		{
			User leUser=ManagerLogin.creerUser(connecteur, email, nom, password, adresse, telephone);
			request.getSession().setAttribute("user", leUser);
			//creation d'un cookie pour le user
			Cookie user=new Cookie("user", email);
			Cookie mdp=new Cookie("password", password);
			user.setMaxAge(30 * 24 * 60 * 60); //1 mois
			mdp.setMaxAge(30 * 24 * 60 * 60);	//1 mois
			response.addCookie(user);
			response.addCookie(mdp);
			
			//redirection
			destination="servletIndex";
		}
		else
		{
			//status erreur et redirection
			destination="servletIndex?menu=inscription";
			String userCree="Le user avec l'adresse "+email+" existe deja, choisissez un autre";
			request.getSession().setAttribute("userCree",userCree);
			
		}
		
		try {
		
			request.getRequestDispatcher(destination).forward(request, response);
			
			
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
