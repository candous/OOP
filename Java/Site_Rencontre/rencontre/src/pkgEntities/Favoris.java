package pkgEntities;

import java.io.Serializable;
import java.util.Date;

public class Favoris implements Serializable {

	private static final long serialVersionUID = 1L;
	private int id;
	private Date dateAjout = new Date();
	private int membreFavorisant;
	private int membreFavorise;
	
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public Date getDateAjout() {
		return dateAjout;
	}
	public void setDateAjout(Date dateAjout) {
		this.dateAjout = dateAjout;
	}
	public int getMembreFavorisant() {
		return membreFavorisant;
	}
	public void setMembreFavorisant(int membreFavorisant) {
		this.membreFavorisant = membreFavorisant;
	}
	public int getMembreFavorise() {
		return membreFavorise;
	}
	public void setMembreFavorise(int membreFavorise) {
		this.membreFavorise = membreFavorise;
	}
	
	
	@Override
	public String toString() {
		return "Favoris [id=" + id + ", dateAjout=" + dateAjout
				+ ", membreFavorisant=" + membreFavorisant
				+ ", membreFavorise=" + membreFavorise + "]";
	}
	
	

}
