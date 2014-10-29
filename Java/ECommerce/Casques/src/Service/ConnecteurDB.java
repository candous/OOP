package Service;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.SQLException;

public class ConnecteurDB {

	private Connection conn;
	private String connString;
	private String username,password;
	
	
	public Connection getConn() {
		return conn;
	}


	public void setConn(Connection conn) {
		this.conn = conn;
	}


	public String getUsername() {
		return username;
	}


	public void setUsername(String username) {
		this.username = username;
	}


	public String getPassword() {
		return password;
	}


	public void setPassword(String password) {
		this.password = password;
	}


	public ConnecteurDB(String driver,String connString,String username,String password) {
		
		this.username=username;
		this.password=password;
		this.connString=connString;
		
		try {
			Class.forName(driver);
		} catch (ClassNotFoundException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
		
	}


	public void ouvrirConnection() {
		
		try {
			this.conn=DriverManager.getConnection(connString, username, password);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}
	
	public void fermerConnection() {
		
		try {
			this.conn.close();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public PreparedStatement getPreparedStatement(String sql) {
	
			PreparedStatement pst=null;
			try {
				
				if (conn==null || conn.isClosed()) {
					
					this.ouvrirConnection();
					
				}
				pst = conn.prepareStatement(sql);
			} catch (SQLException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			
			
			return pst;
		
		
	}
	
	public String getConnString() {
		return connString;
	}


	public void setConnString(String connString) {
		this.connString = connString;
	}
}
