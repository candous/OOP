package pkgEntities;

import java.io.Serializable;
import java.util.Date;

public class Clinsdoeil implements Serializable {
	private int clindoeilid;
	private int fromId;
	private int toId;
	private Date dateEnvoi = new Date();
	
	
	public int getFromId() {
		return fromId;
	}
	public void setFromId(int fromId) {
		this.fromId = fromId;
	}
	public int getToId() {
		return toId;
	}
	public void setToId(int toId) {
		this.toId = toId;
	}
	public Date getDateEnvoi() {
		return dateEnvoi;
	}
	public void setDateEnvoi(Date dateEnvoi) {
		this.dateEnvoi = dateEnvoi;
	}
	public int getClindoeilid() {
		return clindoeilid;
	}
	public void setClindoeilid(int clindoeilid) {
		this.clindoeilid = clindoeilid;
	}

	
}
