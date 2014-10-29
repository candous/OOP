package pkgEntities;

import java.io.Serializable;

public class SkinColor implements Serializable {

	private static final long serialVersionUID = 1L;
	private int id;
	private String description;
	
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public String getDescription() {
		return description;
	}
	public void setDescription(String description) {
		this.description = description;
	}
	
	@Override
	public String toString() {
		return "SkinColor [id=" + id + ", description=" + description + "]";
	}
	
	
}
