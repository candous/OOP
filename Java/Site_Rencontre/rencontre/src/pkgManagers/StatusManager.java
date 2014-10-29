package pkgManagers;

import java.util.ArrayList;

import org.apache.ibatis.session.SqlSession;

import pkgEntities.Status;
import pkgServices.ConnecteurMyBatis;

public class StatusManager {

	public static ArrayList<Status> getListeStatus() {
		ArrayList<Status> list = null;
		
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try{
			list = (ArrayList<Status>)session.selectList(Status.class.getName()+".getListeStatus", null);
		}  
		finally{
			session.close();
		}
		
		return list;
	}

	public static Status getStatusById(int i) {
		Status s = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try{
			s = (Status)session.selectOne(Status.class.getName()+".getStatusById", i);
		}  
		finally{
			session.close();
		}
		
		return s;
	}
	
	

	public static Status getStatusByMemberId(int membreId) {
		Status s = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try{
			s = (Status)session.selectOne(Status.class.getName()+".getStatusByMemberId", membreId);
		}  
		finally{
			session.close();
		}
		
		return s;
	}
}
