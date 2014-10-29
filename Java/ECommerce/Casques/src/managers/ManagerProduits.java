package managers;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import Service.ConnecteurDB;
import beans.Produit;

public class ManagerProduits {

	public static ArrayList<Produit> getListeProduits(ConnecteurDB connecteur, String categorie)
	{
		
		ArrayList<Produit> listeProduits=new ArrayList<Produit>();
		
		String sql="SELECT * FROM produit LEFT JOIN categorie ON produit.id_categorie=categorie.id WHERE categorie.nom=? ";
		
		PreparedStatement pst=connecteur.getPreparedStatement(sql);
		
		try {
			
			pst.setString(1, categorie);
			ResultSet resultat=pst.executeQuery();
			
			
			
			while (resultat.next()) {
				Produit produit=new Produit();
				
				produit.setId(resultat.getInt("id"));
				produit.setNom(resultat.getString("nom"));
				produit.setDescriptionProduit(resultat.getString("description"));
				produit.setPrixProduitCoutant(resultat.getBigDecimal("prixCout"));
				produit.setPrixProduitVendu(resultat.getBigDecimal("prixVente"));
				produit.setQteProduit(resultat.getInt("quantite"));
				produit.setUrl(resultat.getString("url"));
				produit.setNoCategorie(resultat.getInt("id_categorie"));
				
				listeProduits.add(produit);
				
			}
			
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			System.out.println("dans le catch");
			e.printStackTrace();
		}
		
		return listeProduits;
	
	}
	public static ArrayList<Produit> getListeProduitscherche(ConnecteurDB connecteur, String indice)
	{
		
		ArrayList<Produit> listeProduits=new ArrayList<Produit>();
		
		String sql="SELECT * FROM produit WHERE nom LIKE ?";
		
		PreparedStatement pst=connecteur.getPreparedStatement(sql);
		
		try {
			
			pst.setString(1, "%"+indice+"%");
			ResultSet resultat=pst.executeQuery();
			
			
			
			while (resultat.next()) {
				Produit produit=new Produit();
				
				produit.setId(resultat.getInt("id"));
				produit.setNom(resultat.getString("nom"));
				produit.setDescriptionProduit(resultat.getString("description"));
				produit.setPrixProduitCoutant(resultat.getBigDecimal("prixCout"));
				produit.setPrixProduitVendu(resultat.getBigDecimal("prixVente"));
				produit.setQteProduit(resultat.getInt("quantite"));
				produit.setUrl(resultat.getString("url"));
				produit.setNoCategorie(resultat.getInt("id_categorie"));
				
				listeProduits.add(produit);
				
			}
			
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			System.out.println("dans le catch");
			e.printStackTrace();
		}
		
		return listeProduits;
	
	}
	
	
	
	
}

	
	
	

