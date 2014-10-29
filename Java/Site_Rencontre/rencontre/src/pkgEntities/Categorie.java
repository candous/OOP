package pkgEntities;

import java.io.Serializable;

public class Categorie implements Serializable{
	private int categorieID;
	private String description;
	
	public int getCategorieID() {
		return categorieID;
	}
	public void setCategorieID(int categorieID) {
		this.categorieID = categorieID;
	}
	public String getDescription() {
		return description;
	}
	public void setDescription(String description) {
		this.description = description;
	}
	
	
	@Override
	public String toString() {
		return "Categorie [categorieID=" + categorieID + ", description="
				+ description + "]";
	}
	
	
	
}
