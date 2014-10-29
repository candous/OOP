package pkgManagers;

import java.util.ArrayList;

import org.apache.ibatis.session.SqlSession;

import pkgEntities.WeightRange;
import pkgServices.ConnecteurMyBatis;

public class WeightRangeManager {
	public static ArrayList<WeightRange> getListeWeightRanges(){
		ArrayList<WeightRange> list = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			list = (ArrayList<WeightRange>)session.selectList(WeightRange.class.getName()+".getListeWeightRanges", null);
		}  
		finally{
			session.close();
		}
		
		
		return list;
	}

	public static WeightRange getWeightRangeById(int id) {
		WeightRange wr = null;
		
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			wr = (WeightRange)session.selectOne(WeightRange.class.getName()+".getWeightRangeById", id);
		}  
		finally{
			session.close();
		}
		return wr;
	}

	public static WeightRange getWeightRangeBymemberId(int membreId) {
		WeightRange wr = null;
		
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			wr = (WeightRange)session.selectOne(WeightRange.class.getName()+".getWeightRangeBymemberId", membreId);
		}  
		finally{
			session.close();
		}
		return wr;
	}
}

