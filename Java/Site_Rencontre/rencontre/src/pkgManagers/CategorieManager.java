package pkgManagers;

import java.util.ArrayList;

import org.apache.ibatis.session.SqlSession;

import pkgEntities.Categorie;
import pkgServices.ConnecteurMyBatis;

public class CategorieManager {
	public static ArrayList<Categorie> getListeCategories(){
		ArrayList<Categorie> list = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			list = (ArrayList<Categorie>)session.selectList(Categorie.class.getName()+".getListeCategories", null);
		}  
		finally{
			session.close();
		}
		
		
		return list;
	}
	
	public static Categorie getCategorieById(int id){
		Categorie c = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			c = (Categorie)session.selectOne(Categorie.class.getName()+".getCategorieById", id);
		}  
		finally{
			session.close();
		}
		
		return c;
	}
	
	
	
	public static Categorie getCategorieByMemberId(int memberId){
		Categorie c = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			c = (Categorie)session.selectOne(Categorie.class.getName()+".getCategorieByMemberId", memberId);
		}  
		finally{
			session.close();
		}
		
		return c;
	}
	
	
}
