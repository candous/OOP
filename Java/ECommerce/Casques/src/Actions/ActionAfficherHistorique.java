package Actions;

import java.io.IOException;
import java.util.ArrayList;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import managers.ManagerCommande;
import managers.ManagerProduits;
import Service.ConnecteurDB;
import beans.Produit;
import beans.User;

public class ActionAfficherHistorique {

	public static void afficherHistorique(HttpServletRequest request,HttpServletResponse response)
	{
		
		ConnecteurDB connecteur=new ConnecteurDB("com.mysql.jdbc.Driver", "jdbc:mysql://localhost:3306/tp", "root", "");
		
		User user=(User)request.getSession().getAttribute("user");
		
		
		ArrayList<Produit> lesProduits= new ArrayList<Produit>();
		lesProduits=ManagerCommande.getListeHistorique(connecteur, user.getEmail());
		
		//envoyer le tableau de produits pour la requete
		request.setAttribute("listeProduitsHistoriques", lesProduits);

		
		//metter a jour le nombre de items dans le panier
		ActionIndex.MAJArticlesPanier(request, response);
		
		
		try {
			
			request.getRequestDispatcher("historique.jsp").forward(request, response);
			
			
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
