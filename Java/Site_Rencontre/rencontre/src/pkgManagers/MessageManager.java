package pkgManagers;

import java.util.ArrayList;
import java.util.HashMap;

import org.apache.ibatis.session.SqlSession;

import pkgEntities.Clinsdoeil;
import pkgEntities.Favoris;
import pkgEntities.Message;
import pkgEntities.Photo;
import pkgServices.ConnecteurMyBatis;

public class MessageManager {
	
	public static ArrayList<Message> getListeMessagesRecusByMemberId(int memberId){
		ArrayList<Message> liste = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			liste = (ArrayList<Message>)session.selectList(Message.class.getName()+".getListeMessagesRecusByMemberId", memberId);
		}  
		finally{
			session.close();
		}
		
		return liste;
	}
	

	/**
	 * 
	 * @param toMemberId (le membre connecte)
	 * @return
	 */
	public static ArrayList<Message> getMessagesPasLus(int toMemberId){
		ArrayList<Message> listeMessagesPasLus = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			listeMessagesPasLus = (ArrayList<Message>)session.selectList(Message.class.getName()+".getMessagesPasLus", toMemberId);
		}  
		finally{
			session.close();
		}
		
		return listeMessagesPasLus;
	}

	
	
	public static ArrayList<Message> getListeMessagesEnvoyesByMemberId(int memberId){
		ArrayList<Message> liste = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			liste = (ArrayList<Message>)session.selectList(Message.class.getName()+".getListeMessagesEnvoyesByMemberId", memberId);
		}  
		finally{
			session.close();
		}
		
		return liste;
	}

	
	public static ArrayList<Message> getListeMessagesByMembers(int fromId,int toId){
		ArrayList<Message> liste = null;
		HashMap<String, Integer> hmap = new HashMap<String, Integer>();
		hmap.put("key_to", toId);
		hmap.put("key_from", fromId);
		
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			liste = (ArrayList<Message>)session.selectList(Message.class.getName()+".getListeMessagesByMembers", hmap);
		}  
		finally{
			session.close();
		}
		return liste;
	}
	
	
	public static int addMessage(Message m){
		int ret = 0;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			ret = (int) session.insert(Message.class.getName()+".addMessage", m);
			session.commit();
		} catch(Exception e){
			session.rollback();
			e.printStackTrace();
		} finally{
			session.close();
		}
		
		return ret;
	}
	
	
	public static int marquerMessages(int toId,Boolean dejaLu){
		int ret = 0;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		HashMap<String, Object> hmap= new HashMap<String, Object>();
		hmap.put("key_toId", toId);
		hmap.put("key_dejalu", dejaLu);	
		try{
			ret = (int) session.update(Message.class.getName()+".marquerMessages", hmap);
			session.commit();
		} catch(Exception e){
			session.rollback();
			e.printStackTrace();
		} finally{
			session.close();
		}
		
		return ret;
	}


	public static Message getMessageById(int i) {
		Message m = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			m = (Message)session.selectOne(Message.class.getName()+".getMessageById", i);
		}  
		finally{
			session.close();
		}
		
		return m;
	}


	public static int deleteMessage(Message m) {
		int ret = 0;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
        try {
            session.delete(Message.class.getName() + ".deleteMessage", m);
            session.commit();
        } catch (Exception ex) {
            ex.printStackTrace();
            session.rollback();
        } finally {
            session.close();
        }
		
        return ret;
	}



	
}
