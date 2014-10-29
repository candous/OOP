package pkgManagers;

import java.util.ArrayList;
import java.util.HashMap;

import org.apache.ibatis.session.SqlSession;

import pkgEntities.Categorie;
import pkgEntities.Clinsdoeil;
import pkgEntities.HairColor;
import pkgEntities.Membre;
import pkgEntities.Message;
import pkgEntities.NiveauMembre;
import pkgEntities.Photo;
import pkgServices.ConnecteurMyBatis;
import pkgSupportClasses.SearchCriteria;

public class MembreManager {
	@SuppressWarnings("unchecked")
	public static ArrayList<Membre> getTousLesMembres() {
		ArrayList<Membre> listeTousLesMembres = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try {
			listeTousLesMembres = (ArrayList<Membre>) session.selectList(
					Membre.class.getName() + ".getTousLesMembres", null);
			if (listeTousLesMembres != null) {
				for (Membre m : listeTousLesMembres) {
					m.setListeMessagesRecus(MessageManager
							.getListeMessagesRecusByMemberId(m.getMembreId()));

					m.setListeMessagesPasLus(MessageManager.getMessagesPasLus(m
							.getMembreId()));
					m.setListeMessagesEnvoyes(MessageManager
							.getListeMessagesEnvoyesByMemberId(m.getMembreId()));
					
					if(PhotoManager.getProfilPhotosByMemberId(m.getMembreId())!= null){
						m.setProfilImagePath(PhotoManager
								.getProfilPhotosByMemberId(m.getMembreId()).get(0)
								.getChemin());

					}
					
					
				}
			}
		} finally {
			session.close();
		}
		return listeTousLesMembres;
	}

	public static Membre getMembreById(int memberId) {
		Membre m = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try {
			m = (Membre) session.selectOne(Membre.class.getName()
					+ ".getMembreById", memberId);
			if (m != null) {
				m.setListeMessagesRecus(MessageManager
						.getListeMessagesRecusByMemberId(m.getMembreId()));

				m.setListeMessagesPasLus(MessageManager.getMessagesPasLus(m
						.getMembreId()));
				m.setListeMessagesEnvoyes(MessageManager
						.getListeMessagesEnvoyesByMemberId(m.getMembreId()));
				
				if(PhotoManager
						.getProfilPhotosByMemberId(m.getMembreId()) != null){
					m.setProfilImagePath(PhotoManager
							.getProfilPhotosByMemberId(m.getMembreId()).get(0)
							.getChemin());
				}
				

			}
		} finally {
			session.close();
		}

		return m;
	}

	// TODO
	public static Membre getMembreByNickname(String nickname) {
		Membre m = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try {
			m = (Membre) session.selectOne(Membre.class.getName()
					+ ".getMembreByNickname", nickname);
			if (m != null) {
				m.setListeMessagesRecus(MessageManager
						.getListeMessagesRecusByMemberId(m.getMembreId()));

				m.setListeMessagesPasLus(MessageManager.getMessagesPasLus(m
						.getMembreId()));
				m.setListeMessagesEnvoyes(MessageManager
						.getListeMessagesEnvoyesByMemberId(m.getMembreId()));
				m.setProfilImagePath(PhotoManager
						.getProfilPhotosByMemberId(m.getMembreId()).get(0)
						.getChemin());
			}
		} finally {
			session.close();
		}
		return m;
	}

	public static Membre getMembreByNicknamePassword(String pseudo, String pw) {
		Membre m = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try {
			HashMap<String, Object> hmap = new HashMap<String, Object>();
			hmap.put("pseudo_key", pseudo);
			hmap.put("pw_key", pw);

			m = (Membre) session.selectOne(Membre.class.getName()
					+ ".getMembreByNicknamePassword", hmap);
			if (m != null) {
				m.setListeMessagesRecus(MessageManager
						.getListeMessagesRecusByMemberId(m.getMembreId()));
				;
				m.setListeMessagesPasLus(MessageManager.getMessagesPasLus(m
						.getMembreId()));
				m.setListeMessagesEnvoyes(MessageManager
						.getListeMessagesEnvoyesByMemberId(m.getMembreId()));
				m.setProfilImagePath(PhotoManager
						.getProfilPhotosByMemberId(m.getMembreId()).get(0)
						.getChemin());
			}
		} finally {
			session.close();
		}
		return m;
	}

	public static ArrayList<Membre> getFavorisByMembreId(int memberId) {
		ArrayList<Membre> listeFavoris = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try {
			listeFavoris = (ArrayList<Membre>) session.selectList(
					Membre.class.getName() + ".getFavorisByMembreId", memberId);
			if (listeFavoris != null) {
				for (Membre m : listeFavoris) {
					m.setProfilImagePath(PhotoManager
							.getProfilPhotosByMemberId(m.getMembreId()).get(0)
							.getChemin());
					m.setListeMessagesRecus(MessageManager
							.getListeMessagesRecusByMemberId(m.getMembreId()));
					;
					m.setListeMessagesPasLus(MessageManager.getMessagesPasLus(m
							.getMembreId()));
					m.setListeMessagesEnvoyes(MessageManager
							.getListeMessagesEnvoyesByMemberId(m.getMembreId()));
					m.setListePhotos(PhotoManager.getPhotosByMemberId(m
							.getMembreId()));
					m.setListeHobbies(HobbyManager.getListeHobbiesByMembreId(m
							.getMembreId()));
					m.setCategorie(CategorieManager.getCategorieByMemberId(m
							.getMembreId()));
					m.setNiveauMembre(NiveauMembreManager
							.getNiveauMembreByMemberId(m.getMembreId()));
					m.setHairColor(HairColorManager.getHairColorByMemberId(m
							.getMembreId()));
					m.setSkinColor(SkinColorManager.getSkinColorByMemberId(m
							.getMembreId()));
					m.setEyeColor(EyeColorManager.getEyeColorByMemberId(m
							.getMembreId()));
					m.setCity(CityManager.getCityByMemberId(m.getMembreId()));
					m.setStatus(StatusManager.getStatusByMemberId(m
							.getMembreId()));
					m.setWeightRange(WeightRangeManager
							.getWeightRangeBymemberId(m.getMembreId()));
				}
			}
		} finally {
			session.close();
		}
		return listeFavoris;
	}

	// TODO
	public static ArrayList<Membre> getListeFavorisOnline(int memberId) {
		ArrayList<Membre> listeFavorisOnline = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try {
			listeFavorisOnline = (ArrayList<Membre>) session
					.selectList(Membre.class.getName()
							+ ".getListeFavorisOnline", memberId);
			if (listeFavorisOnline != null) {
				for (Membre m : listeFavorisOnline) {
					m.setProfilImagePath(PhotoManager
							.getProfilPhotosByMemberId(m.getMembreId()).get(0)
							.getChemin());
					m.setListeMessagesRecus(MessageManager
							.getListeMessagesRecusByMemberId(m.getMembreId()));
					m.setListeMessagesPasLus(MessageManager.getMessagesPasLus(m
							.getMembreId()));
					m.setListeMessagesEnvoyes(MessageManager
							.getListeMessagesEnvoyesByMemberId(m.getMembreId()));
					m.setListePhotos(PhotoManager.getPhotosByMemberId(m
							.getMembreId()));
					m.setListeHobbies(HobbyManager.getListeHobbiesByMembreId(m
							.getMembreId()));
					m.setCategorie(CategorieManager.getCategorieByMemberId(m
							.getMembreId()));
					m.setNiveauMembre(NiveauMembreManager
							.getNiveauMembreByMemberId(m.getMembreId()));
					m.setHairColor(HairColorManager.getHairColorByMemberId(m
							.getMembreId()));
					m.setSkinColor(SkinColorManager.getSkinColorByMemberId(m
							.getMembreId()));
					m.setEyeColor(EyeColorManager.getEyeColorByMemberId(m
							.getMembreId()));
					m.setCity(CityManager.getCityByMemberId(m.getMembreId()));
					m.setStatus(StatusManager.getStatusByMemberId(m
							.getMembreId()));
					m.setWeightRange(WeightRangeManager
							.getWeightRangeBymemberId(m.getMembreId()));
				}
			}
		} finally {
			session.close();
		}
		return listeFavorisOnline;
	}

	// TODO
	/**
	 * la methode pour la recherche
	 * 
	 * @param searchCriteria
	 * @return
	 */
	public static ArrayList<Membre> recherche(
			HashMap<String, Object> searchCriteria) {
		ArrayList<Membre> listeMembres = null;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		try{
			listeMembres = (ArrayList<Membre>) session
					.selectList(Membre.class.getName()
							+ ".recherche", searchCriteria);
			if (listeMembres != null) {
				for (Membre m : listeMembres) {
					m.setListeMessagesRecus(MessageManager
							.getListeMessagesRecusByMemberId(m.getMembreId()));

					m.setListeMessagesPasLus(MessageManager.getMessagesPasLus(m
							.getMembreId()));
					m.setListeMessagesEnvoyes(MessageManager
							.getListeMessagesEnvoyesByMemberId(m.getMembreId()));
					m.setProfilImagePath(PhotoManager
							.getProfilPhotosByMemberId(m.getMembreId()).get(0)
							.getChemin());

				}
			}
			
			
		} finally{
			session.close();
		}
		
		
		return listeMembres;

	}



	
	public static int addMembre(Membre m) {
		int newMembreId = 0;
		HashMap<String, Object> hmap = new HashMap<String, Object>();
		hmap.put("membreId", newMembreId);
		hmap.put("nom", m.getNom());
		hmap.put("prenom", m.getPrenom());
		hmap.put("description", m.getDescription());
		hmap.put("pseudo", m.getPseudo());
		hmap.put("password", m.getPassword());
		hmap.put("age", m.getAge());
		hmap.put("email", m.getEmail());
		hmap.put("isOnline", m.getIsOnline());
		hmap.put("sexe", m.getSexe());
		hmap.put("height", m.getHeight());
		hmap.put("smoke",m.getSmoke() );
		hmap.put("lastVisit", m.getLastVisit());
		hmap.put("informed_message_received",m.getInformed_message_received() );
		hmap.put("informed_added_by_others",m.getInformed_added_by_others() );
		hmap.put("informed_removed_by_others", m.getInformed_removed_by_others() );
		hmap.put("categorieId", m.getCategorie().getCategorieID() );
		hmap.put("niveauMembreId", m.getNiveauMembre().getId() );
		hmap.put("hair_color_id", m.getHairColor().getId());
		hmap.put("skin_color_id", m.getSkinColor().getId());
		hmap.put("eye_color_id", m.getEyeColor().getId());
		hmap.put("weight_range_id", m.getWeightRange().getId());
		hmap.put("city_id", m.getCity().getId());
		hmap.put("status_id", m.getStatus().getId());
		
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		
        try{
            session.insert(Membre.class.getName()+".addMembre", hmap);
            session.commit();
            newMembreId = (int) hmap.get("membreId");
        } catch(Exception ex){
            ex.printStackTrace();
            session.rollback();
        } finally{
            session.close();
        }
		
		return newMembreId;
	}

	// TODO
	/**
	 * la methode pour la modifier le profil
	 * 
	 * @param m
	 * @return
	 */
	public static int updateMembre(Membre m) {
		int ret = 0;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
       try{
    	   HashMap<String, Object> user=new HashMap<String, Object>();
    	   user.put("pseudo", m.getPseudo());
    	   user.put("email", m.getEmail());
    	   user.put("password", m.getPassword());
    	   user.put("weight", m.getWeightRange().getId());
    	   user.put("ville", m.getCity().getId());
    	   user.put("status", m.getStatus().getId());
    	   user.put("fumeur", m.getSmoke());
    	   user.put("userid", m.getMembreId());
    	   
            ret=session.update(Membre.class.getName()+".updateMembre", user);
            session.commit();
        } catch(Exception ex){
            ex.printStackTrace();
            session.rollback();
        } finally{
            session.close();
        }
       return ret;
	}

	
	public static int updateOnline(int membreId, Boolean b) {
		int ret = 0;
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
		HashMap<String, Object> hmap = new HashMap<String, Object>();
		hmap.put("key_memberId", membreId);
		hmap.put("key_online", b);
		
		
        try{
        	
            ret=session.update(Membre.class.getName()+".updateOnline", hmap);
            session.commit();
        } catch(Exception ex){
            ex.printStackTrace();
            session.rollback();
        } finally{
            session.close();
        }
		
		
		return ret;
		
	}

	public static int deleteMembre(Membre m) {
		int ret = 0;
		int id = m.getMembreId();
		SqlSession session = ConnecteurMyBatis.ouvrirSession();
       try{
            session.update(Membre.class.getName()+".deleteMembre", id);
            session.commit();
        } catch(Exception ex){
            ex.printStackTrace();
            session.rollback();
        } finally{
            session.close();
        }
		
		return ret;
	}

}
