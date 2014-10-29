package pkgManagers;

import java.util.ArrayList;

import org.apache.ibatis.session.SqlSession;

import pkgEntities.Clinsdoeil;
import pkgEntities.Message;
import pkgServices.ConnecteurMyBatis;

public class ClinsdoeilManager {
	
	/**
	 * add a clindoeil
	 * @param clin
	 * @return
	 */
	public static int addClindoeil(Clinsdoeil clin){
		int ret = 0;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try{
			ret = session.insert(Clinsdoeil.class.getName()+".addClindoeil", clin);
			session.commit();
		} catch(Exception e){
			e.printStackTrace();
			session.rollback();
		} finally{
			session.close();
		}
		return ret;
	}
	
	/**
	 * get nombres de clinsdoeils received for the connected member 
	 * @param membreId
	 * @return
	 */
	public static int getNombreClinRecus(int membreId){
		int ret = 0;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try{
			ret = (int) session.selectOne(Clinsdoeil.class.getName()+".getNombresClinsRecu", membreId);
		} finally{
			session.close();
		}
		return ret;
	}
	
	
	
	//TODO
	/**
	 * 
	 * @param toMemberId (le membre connecte)
	 * @return
	 */
	public static ArrayList<Clinsdoeil> getClinsdoeilRecus(int toId){
		ArrayList<Clinsdoeil> clinsdoeilRecus = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try{
			clinsdoeilRecus = (ArrayList<Clinsdoeil>) session.selectList(Clinsdoeil.class.getName()+".getClinsdoeilRecus", toId);
		} finally{
			session.close();
		}
		return clinsdoeilRecus;
	}

	
	public static Clinsdoeil getClinsdoeilById(int i) {
		Clinsdoeil clin = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try{
			clin = (Clinsdoeil) session.selectOne(Clinsdoeil.class.getName()+".getClinsdoeilById", i);
		} finally{
			session.close();
		}
		return clin;
	} 
	
	
	public static int deleteClin(Clinsdoeil clin) {
		int ret = 0;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
        try {
            session.delete(Clinsdoeil.class.getName() + ".deleteClin", clin);
            session.commit();
        } catch (Exception ex) {
            ex.printStackTrace();
            session.rollback();
        } finally {
            session.close();
        }
		
		return  ret;
	}
}
