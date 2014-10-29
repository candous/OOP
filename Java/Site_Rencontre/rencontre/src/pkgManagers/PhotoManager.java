package pkgManagers;

import java.util.ArrayList;

import org.apache.ibatis.session.SqlSession;

import pkgEntities.Favoris;
import pkgEntities.Photo;
import pkgServices.ConnecteurMyBatis;

public class PhotoManager {
	
	
	public static ArrayList<Photo> getProfilPhotosByMemberId(int memberID){
		ArrayList<Photo> listePhoto = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			listePhoto = (ArrayList<Photo>)session.selectList(Photo.class.getName()+".getProfilPhotoByMemberId", memberID);
		}  
		finally{
			session.close();
		}
		
		return listePhoto;
	}
	
	
	
	public static ArrayList<Photo> getPhotosByMemberId(int memberID){
		ArrayList<Photo> listePhoto = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			listePhoto = (ArrayList<Photo>)session.selectList(Photo.class.getName()+".getPhotosByMemberId", memberID);
		}  
		finally{
			session.close();
		}
		
		return listePhoto;
	}
	
	
	//TODO
	/**
	 * for uploading the photo
	 * alse need to update the member 
	 */
	public static int addPhoto(Photo photo){
		int ret = 0;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try{
			ret = (int) session.insert(Photo.class.getName()+".addPhoto", photo);
			session.commit();
		} catch(Exception e){
			session.rollback();
			e.printStackTrace();
		} finally{
			session.close();
		}
		
		return ret;
	}
	
	
	
	public static int deletePhoto(int photoId){
		int ret = 0;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try{
			ret = (int) session.delete(Photo.class.getName()+".deletePhoto", photoId);
			session.commit();
		} catch(Exception e){
			session.rollback();
			e.printStackTrace();
		} finally{
			session.close();
		}
		
		return ret;
	}
	
	
	
}
