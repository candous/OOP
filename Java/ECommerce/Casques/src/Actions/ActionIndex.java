package Actions;

import java.io.IOException;
import java.util.Hashtable;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import managers.ManagerProduitCommande;
import beans.Produit;
import beans.ProduitCommande;


public class ActionIndex {

	public static Hashtable<Integer,ProduitCommande> getLePanier(HttpServletRequest request, HttpServletResponse response) {
	
		//on verifie la session, s'elle n'existe pas, on la cree
			HttpSession session=request.getSession(true);
			//on recupere le panier stocké dans la session
			Hashtable<Integer,ProduitCommande> lePanier=(Hashtable<Integer,ProduitCommande>)session.getAttribute("LePanier");
	
			//si le panier n'existe pas, on le cree et on l'ajout a la session
			if (lePanier==null) {
				lePanier=new Hashtable<Integer,ProduitCommande>();
				session.setAttribute("LePanier", lePanier);
			}
			
			return lePanier;			
	
	}
	
	public static void MAJArticlesPanier(HttpServletRequest request, HttpServletResponse response)
	{
		
		//recuperation du nombre de articles qui a dans le panier
		int nombreArticles=ManagerProduitCommande.nombreArticlesPanier(getLePanier(request, response));
		request.setAttribute("nombreArticles", nombreArticles);
	}
	
	public static void redirection(HttpServletRequest request, HttpServletResponse response,String menu){
		
		MAJArticlesPanier(request, response);
		
		//redirection
		String destination;
		
		
		switch(menu)
		{
			case "home": destination="index.jsp";
			break;
			case "categorie": destination="categorie.jsp";
			break;
			case "login": destination="login.jsp";
			break;
			case "access": destination="servletLogin";
			break;
			case "panier": destination="panier.jsp";
			break;
			case "inscription": destination="inscription.jsp";
			break;
			default: destination="erreur.jsp";
			break;		
		}
		
		try {
			request.getRequestDispatcher(destination).forward(request, response);
		} catch (ServletException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}
}
