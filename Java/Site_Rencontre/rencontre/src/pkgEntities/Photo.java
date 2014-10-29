package pkgEntities;

import java.io.Serializable;

public class Photo implements Serializable {
	private int id;
	private int	memberID;
	private String chemin;
	private Boolean isProfil;
	
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public int getMemberID() {
		return memberID;
	}
	public void setMemberID(int memberID) {
		this.memberID = memberID;
	}
	public String getChemin() {
		return chemin;
	}
	public void setChemin(String chemin) {
		this.chemin = chemin;
	}
	public Boolean getIsProfil() {
		return isProfil;
	}
	public void setIsProfil(Boolean isProfil) {
		this.isProfil = isProfil;
	}
	
	@Override
	public String toString() {
		return "Photo [id=" + id + ", memberID=" + memberID + ", chemin="
				+ chemin + ", isProfil=" + isProfil + "]";
	}
	
	
}
