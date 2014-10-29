package pkgEntities;

import java.io.Serializable;

public class HeightRange implements Serializable {

	private static final long serialVersionUID = 1L;
	private int id;
	private int startRange;
	private int endRange;
	
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public int getStartRange() {
		return startRange;
	}
	public void setStartRange(int startRange) {
		this.startRange = startRange;
	}
	public int getEndRange() {
		return endRange;
	}
	public void setEndRange(int endRange) {
		this.endRange = endRange;
	}
	
	
	@Override
	public String toString() {
		return "HeightRange [id=" + id + ", startRange=" + startRange
				+ ", endRange=" + endRange + "]";
	}
	
	
}
