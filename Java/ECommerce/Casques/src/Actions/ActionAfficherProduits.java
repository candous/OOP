package Actions;

import java.io.IOException;
import java.util.ArrayList;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import managers.ManagerProduitCommande;
import managers.ManagerProduits;
import beans.Produit;
import Service.ConnecteurDB;



public class ActionAfficherProduits {

	public static void afficherProduits(HttpServletRequest request, HttpServletResponse response,String categorie)
	{
		
		ConnecteurDB connecteur=new ConnecteurDB("com.mysql.jdbc.Driver", "jdbc:mysql://localhost:3306/tp", "root", "");
		
		ArrayList<Produit> lesProduits= new ArrayList<Produit>();
		lesProduits=ManagerProduits.getListeProduits(connecteur, categorie);
		
		//envoyer le tableau de produits pour la requete
		request.setAttribute("listeProduits", lesProduits);
		
		//affichage de la categorie dans jsp
		String categorieAffichee;
		switch(categorie)
		{
			case "retro": categorieAffichee="RETRO";
			break;
			case "custom": categorieAffichee="CUSTOM";
			break;
			case "sport": categorieAffichee="SPORTIVES";
			break;
			default:categorieAffichee="CATEGORIE";
			break;
		
		}
		request.setAttribute("categorieAffichage", categorieAffichee);
		request.setAttribute("categorie", categorie);
		
		//metter a jour le nombre de items dans le panier
		ActionIndex.MAJArticlesPanier(request, response);
		
		
		try {
			
			request.getRequestDispatcher("produits.jsp").forward(request, response);
			
			
		} catch (ServletException e) {
			// TODO Auto-generated catch block
			System.out.println("dans le catch");
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
