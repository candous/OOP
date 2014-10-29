package managers;


import java.util.Hashtable;
import java.util.Iterator;
import java.util.Set;

import beans.Produit;
import beans.ProduitCommande;

public class ManagerProduitCommande {

	
	public static Integer nombreArticlesPanier(Hashtable<Integer,ProduitCommande> lePanier)
	{
			
			Integer nbrArticle=0;
			
			if (lePanier.size()>0) {
				
				Set listeIdProduit=lePanier.keySet();
				
				Iterator it=listeIdProduit.iterator();
				
				ProduitCommande ligneCommande;
				
				while (it.hasNext()) {
					ligneCommande=lePanier.get(it.next());
					nbrArticle+=ligneCommande.getQuantite();
				}
				
			}
			
			return nbrArticle;
			
		}
	
	public static void ajouterProduit(Hashtable<Integer,ProduitCommande> lePanier, Produit produit, int quantite)
	{
		
		ProduitCommande produitCommande;
		Integer idProduit=produit.getId();
		
		if (lePanier.containsKey(idProduit))
		{
			produitCommande=lePanier.get(idProduit);
			Integer nouvellequantite=produitCommande.getQuantite()+quantite;
			produitCommande.setQuantite(nouvellequantite);
		}
		
		else
		{
			produitCommande=new ProduitCommande();
			produitCommande.setProduit(produit);
			produitCommande.setQuantite(quantite);
		}
		
		lePanier.put(idProduit, produitCommande);	
		
	}
	
	
	public static void majProduit(Hashtable<Integer,ProduitCommande> lePanier, Produit produit, int quantite)
	{
		ProduitCommande produitCommande;
		Integer idProduit=produit.getId();
		
		produitCommande=lePanier.get(idProduit);
		produitCommande.setQuantite(quantite);
		
		lePanier.put(idProduit, produitCommande);
		
		if(quantite==0)
			lePanier.remove(idProduit);
		
	}
	
	public static void deleteProduit(Hashtable<Integer,ProduitCommande> lePanier, Produit produit)
	{
		Integer idProduit=produit.getId();
		lePanier.remove(idProduit);
	}
	
}
