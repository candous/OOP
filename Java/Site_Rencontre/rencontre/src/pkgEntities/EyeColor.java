package pkgEntities;

import java.io.Serializable;

public class EyeColor implements Serializable {
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
		return "EyeColor [id=" + id + ", description=" + description + "]";
	}
}
