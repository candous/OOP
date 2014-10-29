package beans;

import java.math.BigDecimal;


public class Produit {
	
	//attributs
	private int id;
	private String nom;
	private String descriptionProduit;
	private BigDecimal prixProduitCoutant;
	private BigDecimal prixProduitVendu;
	private int qteProduit;
	private String url;
	private int	noCategorie;

	
	public int getId() {
		return id;
	}
	public void setId(int noProduit) {
		this.id = noProduit;
	}
	public String getNom() {
		return nom;
	}
	public void setNom(String nom) {
		this.nom = nom;
	}
	public String getDescriptionProduit() {
		return descriptionProduit;
	}
	public void setDescriptionProduit(String descriptionProduit) {
		this.descriptionProduit = descriptionProduit;
	}
	public BigDecimal getPrixProduitCoutant() {
		return prixProduitCoutant;
	}
	public void setPrixProduitCoutant(BigDecimal prixProduitCoutant) {
		this.prixProduitCoutant = prixProduitCoutant;
	}
	public BigDecimal getPrixProduitVendu() {
		return prixProduitVendu;
	}
	public void setPrixProduitVendu(BigDecimal prixProduitVendu) {
		this.prixProduitVendu = prixProduitVendu;
	}
	public int getQteProduit() {
		return qteProduit;
	}
	public void setQteProduit(int qteProduit) {
		this.qteProduit = qteProduit;
	}
	public String getUrl() {
		return url;
	}
	public void setUrl(String url) {
		this.url = url;
	}
	public int getNoCategorie() {
		return noCategorie;
	}
	public void setNoCategorie(int noCategorie) {
		this.noCategorie = noCategorie;
	}
	
	
}
