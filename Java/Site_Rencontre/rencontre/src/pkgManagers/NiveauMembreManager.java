package pkgManagers;

import java.util.ArrayList;

import org.apache.ibatis.session.SqlSession;

import pkgEntities.Categorie;
import pkgEntities.NiveauMembre;
import pkgServices.ConnecteurMyBatis;

public class NiveauMembreManager {
	
	public static ArrayList<NiveauMembre> getListeNiveauMembres(){
		ArrayList<NiveauMembre> listeNiveauMembres = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			listeNiveauMembres = (ArrayList<NiveauMembre>)session.selectList(NiveauMembre.class.getName()+".getListeNiveauMembres", null);
		}  
		finally{
			session.close();
		}
		
		
		return listeNiveauMembres;
	}
	
	public static NiveauMembre getNiveauMembreByMemberId(int memberId){
		NiveauMembre nm = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			nm = (NiveauMembre)session.selectOne(NiveauMembre.class.getName()+".getNiveauMembreByMemberId", memberId);
		}  
		finally{
			session.close();
		}
		return nm;
	}

	public static NiveauMembre getNiveauMembreById(int id) {
		NiveauMembre nm = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
		try{
			nm = (NiveauMembre)session.selectOne(NiveauMembre.class.getName()+".getNiveauMembreById", id);
		}  
		finally{
			session.close();
		}	
		return nm;
	}
}
