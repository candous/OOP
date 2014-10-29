package pkgManagers;

import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;

import org.apache.ibatis.session.SqlSession;

import pkgEntities.Favoris;
import pkgServices.ConnecteurMyBatis;

public class FavorisManager {
	
	public static int addFavoris(Date date, int membreFavorisantId, int membreFavoriseId){
		int ret = 0;
		
		HashMap<String, Object> hmap = new HashMap<String, Object>();
		hmap.put("keyDateAjout", date);
		hmap.put("keyMembreFavorisant", membreFavorisantId);
		hmap.put("keyMembreFavorise", membreFavoriseId);


		//test if exists already
		if(!existFavoris(membreFavorisantId, membreFavoriseId)){
			SqlSession session = ConnecteurMyBatis.ouvrirSession();
			
			try{
				ret = (int) session.insert(Favoris.class.getName()+".addFavoris", hmap);
				session.commit();
			} catch(Exception e){
				session.rollback();
				e.printStackTrace();
			} finally{
				session.close();
			}
		}
		
		return ret;
	} 
	
	public static boolean existFavoris(int membreFavorisantId, int membreFavoriseId){
		int ret = 0;
		boolean exist = false;
		HashMap<String, Object> hmap = new HashMap<String, Object>();
		
		hmap.put("keyMembreFavorisant", membreFavorisantId);
		hmap.put("keyMembreFavorise", membreFavoriseId);
		
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			ret = (int) session.selectOne(Favoris.class.getName()+".getFavorisByMembers", hmap);
		} finally{
			session.close();
		}
		
		if(ret != 0){
			exist = true;
		}
		
		return exist;
	}
	
	public static int deleteFavoris(int membreFavorisantId, int membreFavoriseId){
		int ret = 0;
		HashMap<String, Object> hmap = new HashMap<String, Object>();
		hmap.put("keyMembreFavorisant", membreFavorisantId);
		hmap.put("keyMembreFavorise", membreFavoriseId);
		
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			ret = (int) session.delete(Favoris.class.getName()+".deleteFavoris", hmap);
			session.commit();
		} catch(Exception e){
			session.rollback();
			e.printStackTrace();
		} 
		finally{
			session.close();
		}
		
		return ret;
	}

	public static ArrayList<Favoris> getFavorisByMemberId(int membreFavorisantId){
		ArrayList<Favoris> listeFavoris = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			listeFavoris = (ArrayList<Favoris>)session.selectList(Favoris.class.getName()+".getFavorisByMemberId", membreFavorisantId);
			
		}  
		finally{
			session.close();
		}
		
		return listeFavoris;
	}
	
	
	public static ArrayList<Favoris> getListeFavorisOnline(int membreFavorisantId){
		ArrayList<Favoris> listeFavoris = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			listeFavoris = (ArrayList<Favoris>)session.selectList(Favoris.class.getName()+".getListeFavorisOnline", membreFavorisantId);
			
		}  
		finally{
			session.close();
		}
		return listeFavoris;
	}
	
	
}
