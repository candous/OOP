package managers;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;

import beans.User;
import Service.ConnecteurDB;

public class ManagerLogin {

	
	public static User verifierUser(ConnecteurDB connecteur, String mail, String password)
	{
		
		User leUser=null;
		String sql="SELECT * FROM user WHERE email=? ";
		
		String passwordEnc=encrypterMDP(password);
		
		PreparedStatement pst=connecteur.getPreparedStatement(sql);
		
		try {
			
			pst.setString(1, mail);
			ResultSet resultat=pst.executeQuery();
			
			if (resultat.first()) {
				
				if(passwordEnc.equals(resultat.getString("password")))
				{
					leUser=new User();
					leUser.setEmail(resultat.getString("email"));
					leUser.setNom(resultat.getString("nom"));
					leUser.setAdresse(resultat.getString("adresse"));
					leUser.setTelephone(resultat.getString("telephone"));
					leUser.setPassword(passwordEnc);
				}
					
			}
			
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			
			e.printStackTrace();
		}
		
		return leUser;
	}
	
	
	public static String encrypterMDP(String password)
	{
		String mdp=null;
		
		try {
			MessageDigest digestPassword=MessageDigest.getInstance("MD5");
			digestPassword.update(password.getBytes());
			byte[] tableauDeBytes=digestPassword.digest();
			StringBuffer passwordCrypteMD5 = new StringBuffer();
			for (byte unByte : tableauDeBytes) {
//				la notation "%02x" : le symbole  % est obligatoire dans le mask de la methode format, le 02 veut dire le nombre minimum de chiffre a afficher et le x pour dire
//				qu il s agit de faire le formatage en Hexadecimal.
//				l operation & 0xff transforme le byte en un int entre 0 et 255 alors que normalement un byte est entre -128 et 127.
				passwordCrypteMD5.append(String.format("%02x", unByte & 0xff));
			}
			
			mdp=passwordCrypteMD5.toString();
			
		} catch (NoSuchAlgorithmException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return mdp;
		
	}
	
	public static boolean verifierUserBD(ConnecteurDB connecteur, String mail)
	{
		boolean retour=false;
		String sql="SELECT * FROM user WHERE email=? ";
		
		PreparedStatement pst=connecteur.getPreparedStatement(sql);
		
		try {
			
			pst.setString(1, mail);
			ResultSet resultat=pst.executeQuery();
			
			if (resultat.next()) {
				retour=true;
			} 
			}catch (SQLException e) {
				// TODO Auto-generated catch block
				
				e.printStackTrace();
			}
	
		return retour;
	}
	
	public static User creerUser(ConnecteurDB connecteur, String email,String nom,String password,String adresse, String telephone)
	{
		User leUser=new User();
	
		
		String sql="INSERT INTO user (email, nom, adresse, telephone, password) VALUES(?,?,?,?,?)";
		
		String passwordEnc=encrypterMDP(password);
		
		PreparedStatement pst=connecteur.getPreparedStatement(sql);
	
		
		
		try {
				connecteur.getConn().setAutoCommit(false);
				
				pst.setString(1,email);
				pst.setString(2,nom);
				pst.setString(3,adresse);
				pst.setString(4, telephone);
				pst.setString(5,passwordEnc);
				
				int resultat=pst.executeUpdate();
				
				connecteur.getConn().commit();
				
				leUser.setEmail(email);
				leUser.setNom(nom);
				leUser.setAdresse(adresse);
				leUser.setTelephone(telephone);
				leUser.setPassword(passwordEnc);
				
			
				
		
		}catch (SQLException e) {
			e.printStackTrace();
			try {
				connecteur.getConn().rollback();
			} catch (SQLException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}
			
		}
		
		return leUser;
	}
}
		

	
