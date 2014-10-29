package pkgManagers;

import java.util.ArrayList;

import org.apache.ibatis.session.SqlSession;

import pkgEntities.City;
import pkgServices.ConnecteurMyBatis;

public class CityManager {

	public static ArrayList<City> getListeCities() {
		ArrayList<City> list = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			list = (ArrayList<City>)session.selectList(City.class.getName()+".getListeCities", null);
		}  
		finally{
			session.close();
		}
		
		return list;
	}

	
	public static City getCityById(int i) {
		City c = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			c = (City)session.selectOne(City.class.getName()+".getCityById", i);
		}  
		finally{
			session.close();
		}
		return c;
	}


	public static City getCityByMemberId(int membreId) {
		City c = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			c = (City)session.selectOne(City.class.getName()+".getCityByMemberId", membreId);
		}  
		finally{
			session.close();
		}
		return c;
	}

}
