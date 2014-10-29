package pkgEntities;

import java.io.Serializable;

public class NiveauMembre implements Serializable {
	private int niveauMembreId;
	private String description;
	private int nbPhotosMax;
	private int nbFavorisMax;
	private Boolean droitEnvoyerMsg;
	
	public int getId() {
		return niveauMembreId;
	}
	public void setId(int id) {
		this.niveauMembreId = id;
	}
	public String getDescription() {
		return description;
	}
	public void setDescription(String description) {
		this.description = description;
	}
	public int getNbPhotosMax() {
		return nbPhotosMax;
	}
	public void setNbPhotosMax(int nbPhotosMax) {
		this.nbPhotosMax = nbPhotosMax;
	}
	public int getNbFavorisMax() {
		return nbFavorisMax;
	}
	public void setNbFavorisMax(int nbFavorisMax) {
		this.nbFavorisMax = nbFavorisMax;
	}
	public Boolean getDroitEnvoyerMsg() {
		return droitEnvoyerMsg;
	}
	public void setDroitEnvoyerMsg(Boolean droitEnvoyerMsg) {
		this.droitEnvoyerMsg = droitEnvoyerMsg;
	}
	
	@Override
	public String toString() {
		return "NiveauMembre [niveauMembreId=" + niveauMembreId
				+ ", description=" + description + ", nbPhotosMax="
				+ nbPhotosMax + ", nbFavorisMax=" + nbFavorisMax
				+ ", droitEnvoyerMsg=" + droitEnvoyerMsg + "]";
	}

	

	
}
