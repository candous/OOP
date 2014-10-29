package Actions;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.Hashtable;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import managers.ManagerProduitCommande;
import beans.Produit;
import beans.ProduitCommande;



public class ActionGererPanier {

	public static void ajouterProduitPanier(HttpServletRequest request, HttpServletResponse response, int quantite, Produit produit)
	{
		
		Hashtable<Integer,ProduitCommande> lePanier= ActionIndex.getLePanier(request, response);
		
		ManagerProduitCommande.ajouterProduit(lePanier, produit, quantite);
		
		ActionIndex.MAJArticlesPanier(request, response);
	
		redirection(request, response);
		
	}
	
	
	public static void majProduitPanier(HttpServletRequest request, HttpServletResponse response, int quantite, Produit produit)
	{
		Hashtable<Integer,ProduitCommande> lePanier= ActionIndex.getLePanier(request, response);
		
		ManagerProduitCommande.majProduit(lePanier, produit, quantite);
		
		ActionIndex.MAJArticlesPanier(request, response);
	
		redirection(request, response);
	}
	
	public static void deleteProduitPanier(HttpServletRequest request, HttpServletResponse response, Produit produit)
	{
	
		Hashtable<Integer,ProduitCommande> lePanier= ActionIndex.getLePanier(request, response);
		
		ManagerProduitCommande.deleteProduit(lePanier, produit);
		
		ActionIndex.MAJArticlesPanier(request, response);
	
		redirection(request, response);
		
	}
	
	
	public static void redirection(HttpServletRequest request, HttpServletResponse response)
	{	
		String provenance=String.valueOf(request.getSession().getAttribute("categorie"));
		
		String destination="panier.jsp";
		
		if(provenance!="panier.jsp")
			ActionAfficherProduits.afficherProduits(request, response, provenance);
		
		else{
			
			try {
				request.getRequestDispatcher(destination).forward(request, response);
			} catch (ServletException | IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
	}
	
}
