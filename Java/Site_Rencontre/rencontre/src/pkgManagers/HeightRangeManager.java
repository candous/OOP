package pkgManagers;

import java.util.ArrayList;

import org.apache.ibatis.session.SqlSession;

import pkgEntities.HeightRange;
import pkgServices.ConnecteurMyBatis;

public class HeightRangeManager {
	public static ArrayList<HeightRange> getListeHeightRanges(){
		ArrayList<HeightRange> list = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			list = (ArrayList<HeightRange>)session.selectList(HeightRange.class.getName()+".getListeHeightRanges", null);
		}  
		finally{
			session.close();
		}
		
		
		return list;
	}
}
