package pkgManagers;

import java.util.ArrayList;

import org.apache.ibatis.session.SqlSession;

import pkgEntities.HairColor;
import pkgEntities.SkinColor;
import pkgServices.ConnecteurMyBatis;

public class SkinColorManager {
	
	public static ArrayList<SkinColor> getListeSkinColors(){
		ArrayList<SkinColor> list = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			list = (ArrayList<SkinColor>)session.selectList(SkinColor.class.getName()+".getListeSkinColors", null);
		}  
		finally{
			session.close();
		}
		
		
		return list;
	}
	
	
	public static SkinColor getSkinColorById(int id){
		SkinColor c = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			c = (SkinColor)session.selectOne(SkinColor.class.getName()+".getSkinColorById", id);
		}  
		finally{
			session.close();
		}
		return c;
	}
	
	
	
	public static SkinColor getSkinColorByMemberId(int memberId){
		SkinColor c = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			c = (SkinColor)session.selectOne(SkinColor.class.getName()+".getSkinColorByMemberId", memberId);
		}  
		finally{
			session.close();
		}
		return c;
	}
}
