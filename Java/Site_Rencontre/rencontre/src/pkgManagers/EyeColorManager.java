package pkgManagers;

import java.util.ArrayList;

import org.apache.ibatis.session.SqlSession;

import pkgEntities.EyeColor;
import pkgEntities.SkinColor;
import pkgServices.ConnecteurMyBatis;

public class EyeColorManager {
	public static ArrayList<EyeColor> getListeEyeColors(){
		ArrayList<EyeColor> list = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			list = (ArrayList<EyeColor>)session.selectList(EyeColor.class.getName()+".getListeEyeColors", null);
		}  
		finally{
			session.close();
		}
		
		
		return list;
	}

	
	public static EyeColor getEyeColorById(int id) {
		EyeColor c = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			c = (EyeColor)session.selectOne(EyeColor.class.getName()+".getEyeColorById", id);
		}  
		finally{
			session.close();
		}
		
		return c;
	}
	
	
	public static EyeColor getEyeColorByMemberId(int memberId){
		EyeColor c = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			c = (EyeColor)session.selectOne(EyeColor.class.getName()+".getEyeColorByMemberId", memberId);
		}  
		finally{
			session.close();
		}
		return c;
	}
}
