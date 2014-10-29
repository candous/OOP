package Actions;

import java.io.IOException;
import java.io.PrintWriter;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import java.util.Hashtable;
import java.util.Properties;






import managers.ManagerCommande;
import Service.ConnecteurDB;
import beans.ProduitCommande;
import beans.User;

public class ActionCommande {
	
	public static void creerCommande(HttpServletRequest request, HttpServletResponse response, User leUser){
		HttpSession session=request.getSession();
		Hashtable<Integer,ProduitCommande> lePanier=new Hashtable<Integer,ProduitCommande>();
		lePanier=(Hashtable<Integer,ProduitCommande>)session.getAttribute("LePanier");
		
		ConnecteurDB connecteur=new ConnecteurDB("com.mysql.jdbc.Driver", "jdbc:mysql://localhost:3306/tp", "root", "");
		ManagerCommande.creerCommande(request, response, connecteur, leUser, lePanier);
		
		session.removeAttribute("LePanier");
		
		try {
			request.getRequestDispatcher("servletIndex").forward(request,response);
		} catch (ServletException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		
	}

}
