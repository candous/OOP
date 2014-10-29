package Actions;

import java.io.IOException;
import java.util.ArrayList;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import managers.ManagerProduits;
import beans.Produit;
import Service.ConnecteurDB;

public class ActionRecherche {
	public static void rechercheElement(HttpServletRequest request, HttpServletResponse response, String indice){
		ConnecteurDB connecteur=new ConnecteurDB("com.mysql.jdbc.Driver", "jdbc:mysql://localhost:3306/tp1", "root", "");
		ArrayList<Produit> lesProduitsCherche= new ArrayList<Produit>();
		lesProduitsCherche=ManagerProduits.getListeProduitscherche(connecteur, indice);
		request.setAttribute("produitCherche", lesProduitsCherche);
		ActionIndex.MAJArticlesPanier(request, response);
		
        try {
			
			request.getRequestDispatcher("resultat.jsp").forward(request, response);
			
			
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
