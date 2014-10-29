package pkgBackingBeans;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Date;

import pkgEntities.City;
import pkgEntities.Clinsdoeil;
import pkgEntities.HairColor;
import pkgEntities.Membre;
import pkgEntities.Message;
import pkgEntities.NiveauMembre;
import pkgEntities.Photo;
import pkgManagers.CityManager;
import pkgManagers.ClinsdoeilManager;
import pkgManagers.FavorisManager;
import pkgManagers.HobbyManager;
import pkgManagers.MessageManager;
import pkgManagers.PhotoManager;
import services.SendEmail;

public class ProfileFavorisBackingBean implements Serializable {

	private MembreBackingBean mbb;
	private Membre membreVisite;
	private String fumeur;
	private String ville;
	private String reponse;
	private ArrayList<Photo> photosSaufProfile;
	
	
	public MembreBackingBean getMbb() {
		return mbb;
	}
	public void setMbb(MembreBackingBean mbb) {
		this.mbb = mbb;
	}
	public Membre getMembreVisite() {
		membreVisite=mbb.getProfileAmis();
		membreVisite.setListeHobbies(HobbyManager.getListeHobbiesByMembreId(membreVisite.getMembreId()));
		membreVisite.setListePhotos(PhotoManager.getPhotosByMemberId(membreVisite.getMembreId()));
		
		return membreVisite;
	}
	public void setMembreVisite(Membre membreVisite) {
		this.membreVisite = membreVisite;
	}
//	public String getPhotoProfile() {
//		ArrayList<Photo> listePhotos=new ArrayList<Photo>();
//		listePhotos=membreVisite.getListePhotos();
//		
//		for(Photo p:listePhotos)
//		{
//			if(p.getIsProfil())
//				photoProfile=p.getChemin();
//		}
//		//photoProfile=PhotoManager.getProfilPhotosByMemberId(membreVisite.getMembreId()).get(0).getChemin();
//		return photoProfile;
//	}
//	public void setPhotoProfile(String photoProfile) {
//		this.photoProfile = photoProfile;
//	}
	
	public String getFumeur() {
		if(membreVisite.getSmoke())
			fumeur="oui";
		else 
			fumeur="non";
		return fumeur;
	}
	public void setFumeur(String fumeur) {
		this.fumeur = fumeur;
	}
	public String getVille() {
		ville=CityManager.getCityById(membreVisite.getCity().getId()).getDescription(); 
			
		return ville;
	}
	public void setVille(String ville) {
		this.ville = ville;
	}
	public String getReponse() {
		return reponse;
	}
	public void setReponse(String reponse) {
		this.reponse = reponse;
	}
	public ArrayList<Photo> getPhotosSaufProfile() {
	
		photosSaufProfile=membreVisite.getListePhotos();
		//remove photo profile de la liste de photos du user
		for(Photo p:photosSaufProfile)
		{
			if(p.getIsProfil())
				photosSaufProfile.remove(p);
		}
		
		return photosSaufProfile;
	}
	public void setPhotosSaufProfile(ArrayList<Photo> photosSaufProfile) {
		this.photosSaufProfile = photosSaufProfile;
	}
	
	public String envoyerClinDoeil()
	{
		String destination=null;
		
		Clinsdoeil clin=new Clinsdoeil();
		clin.setFromId(mbb.getMembre().getMembreId());
		clin.setToId(membreVisite.getMembreId());
		//get date now
		Date date = new Date();
		clin.setDateEnvoi(date);
		
		ClinsdoeilManager.addClindoeil(clin);
		
		return destination;
	}
	
	public String envoyerMessage()
	{
		String destination=null;
		Message message=new Message();
		message.setContenu(reponse);
		message.setDejalu(false);
		message.setMsgFrom(mbb.getMembre().getMembreId());
		message.setMsgTo(membreVisite.getMembreId());
		message.setReplyToMsgId(message.getMessageid());
		message.setSendDate(new Date());
		
		int ajoute=MessageManager.addMessage(message);
		boolean recevoirCourriel=false;
		recevoirCourriel=membreVisite.getInformed_message_received();
		
		if(ajoute>0 && recevoirCourriel){
				SendEmail.envoyerMail(mbb.getMembre(), membreVisite, "message");
		
	}
		return destination;
	}
	
	public String ajouterFavoris()
	{
		String destination=null;
		//tester niveau du user
		NiveauMembre niveau=new NiveauMembre();
		int maxFavoris=0;
		niveau=mbb.getMembre().getNiveauMembre();
		maxFavoris=niveau.getNbFavorisMax();
		
		ArrayList<Membre> listeFavoris=new ArrayList<Membre>();
		listeFavoris=mbb.getListeFavoris();
		
		if(listeFavoris.size()<maxFavoris){
			//ajouter amis
			int ajoute=FavorisManager.addFavoris(new Date(), mbb.getMembre().getMembreId(), membreVisite.getMembreId());
			if(ajoute>0)
				destination="favoris";
			boolean recevoirCourriel=false;
			recevoirCourriel=membreVisite.getInformed_added_by_others();
			if(ajoute>0 && recevoirCourriel){
				//envoyer courriel a qui a ete ajoute
				SendEmail.envoyerMail(mbb.getMembre(), membreVisite, "ajoute");
			}
		}
		return destination;
	}
}
