package pkgBackingBeans;

import java.io.Serializable;
import java.util.ArrayList;

import javax.faces.context.ExternalContext;
import javax.faces.context.FacesContext;
import javax.faces.model.SelectItem;
import javax.servlet.http.HttpSession;

import pkgEntities.Categorie;
import pkgEntities.City;
import pkgEntities.EyeColor;
import pkgEntities.Favoris;
import pkgEntities.HairColor;
import pkgEntities.Hobby;
import pkgEntities.Membre;
import pkgEntities.Message;
import pkgEntities.Photo;
import pkgEntities.SkinColor;
import pkgEntities.Status;
import pkgEntities.WeightRange;
import pkgManagers.CityManager;
import pkgManagers.ClinsdoeilManager;
import pkgManagers.FavorisManager;
import pkgManagers.HobbyManager;
import pkgManagers.MembreManager;
import pkgManagers.MessageManager;
import pkgManagers.PhotoManager;
import services.FacesTools;

public class MembreBackingBean implements Serializable {

	private static final long serialVersionUID = 1L;
	
	//attribut membre
	private Membre membre;
	private HttpSession session;
	private Membre profileAmis;
	
	//profile
	private String pseudo;
	private String password;
	private String photoProfile;
	private int nbMsgPasLues;
	private int nbFavorisOnline;
	private int nbClin;
	private ArrayList<Membre> listeFavoris;
	
		
	public Membre getMembre() {
		
		session=(HttpSession) FacesContext.getCurrentInstance().getExternalContext().getSession(true);
		membre=(Membre)session.getAttribute("user");
		
		return membre;
	}

	public void setMembre(Membre membre) {
		this.membre = membre;
	}

	public HttpSession getSession() {
		return session;
	}

	public void setSession(HttpSession session) {
		this.session = session;
	}
	
	public Membre getProfileAmis() {
		return profileAmis;
	}

	public void setProfileAmis(Membre profileAmis) {
		this.profileAmis = profileAmis;
	}

	public String getPseudo() {
		return pseudo;
	}

	public void setPseudo(String pseudo) {
		this.pseudo = pseudo;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public String getPhotoProfile() {
		
		ArrayList<Photo> listePhotos=new ArrayList<Photo>();
		listePhotos=PhotoManager.getProfilPhotosByMemberId(membre.getMembreId());
		
		for(Photo p : listePhotos){
			if(p.getIsProfil())
				photoProfile=p.getChemin();
		}
		
		if(photoProfile==null)
			photoProfile="photos/profil.jpg";
				
		return photoProfile;
	}
	public void setPhotoProfile(String photoProfile) {
		this.photoProfile = photoProfile;
	}
	public int getNbMsgPasLues() {
		
		ArrayList<Message> listeMessages=new ArrayList<Message>();
		listeMessages=MessageManager.getListeMessagesRecusByMemberId(membre.getMembreId());
		
		nbMsgPasLues=0;
		for(Message m:listeMessages)
		{
			boolean lu=m.getDejalu();
			if(!lu)
				nbMsgPasLues++;
		}	
		return nbMsgPasLues;
	}
	
	public void setNbMsgPasLues(int nbMsgPasLues) {
		this.nbMsgPasLues = nbMsgPasLues;
	}
	
	public int getNbFavorisOnline() {
		//pour utilisation dans le dashboard du user
		nbFavorisOnline=0;
		ArrayList<Favoris> listeFavorisOnline=new ArrayList<Favoris>();
		listeFavorisOnline=FavorisManager.getListeFavorisOnline(membre.getMembreId());
		nbFavorisOnline=listeFavorisOnline.size();
			
		return nbFavorisOnline;
	}
	public void setNbFavorisOnline(int nbFavorisOnline) {
		this.nbFavorisOnline = nbFavorisOnline;
	}
	public int getNbClin() {
		
		nbClin=ClinsdoeilManager.getNombreClinRecus(membre.getMembreId());
		
		return nbClin;
	}
	public void setNbClin(int nbClin) {
		this.nbClin = nbClin;
	}
	public ArrayList<Membre> getListeFavoris() {
		listeFavoris=MembreManager.getFavorisByMembreId(membre.getMembreId());
		
		return listeFavoris;
	}

	public void setListeFavoris(ArrayList<Membre> listeFavoris) {
		this.listeFavoris = listeFavoris;
	}

	
	//METHODES
	public String logOut(){
		
		String out=null;
		ExternalContext ec=FacesContext.getCurrentInstance().getExternalContext();
		int offline=MembreManager.updateOnline(membre.getMembreId(), false);
		if(offline>0)
		{
			HttpSession session=(HttpSession)ec.getSession(false);
			
			try {
				FacesTools.invalidateSession(session);
				ec.redirect("./index.xhtml");
			
			} catch (Exception e) {
				// TODO: handle exception
				e.printStackTrace();
			}
		}
		return out;
	}
	
	public String verifierLogin()
	{
		String destination=null;
		membre=MembreManager.getMembreByNicknamePassword(pseudo, password);
		
		//si bon user
		if(membre!=null){
			//peopler le user
			MembreManager.updateOnline(membre.getMembreId(), true);
			destination="oui";
			session=(HttpSession) FacesContext.getCurrentInstance().getExternalContext().getSession(true);
			session.setAttribute("user", membre);
		
		}
		
		return destination;
	}
	
	public String afficherTableau()
	{
		return "tableau";
	}
	public String afficherMessages(){
		String redirection="messages";
		if(nbMsgPasLues>0)
			MessageManager.marquerMessages(membre.getMembreId(),true);
		
		return redirection;
	}
	
	public String afficherFavoris(){
		String redirection="favoris";
		return redirection;
	}
	
	public String afficherRecherche()
	{
		return "recherche";
	}
	
	public String afficherPhotos(){
		String redirection="photos";
		return redirection;
	}
	public String afficherConfigs(){
		String redirection="configs";
		
		return redirection;
	}
	public String afficherMembre(Membre membreAfficher){
		String redirection=null;
		profileAmis=membreAfficher;
		redirection="membre";
		
		return redirection;
	}
	public String afficherAmisOnline()
	{
		String redirection=null;
		
		if(nbFavorisOnline>0)
			redirection="amisonline";
		
		return redirection;
	}
	public String afficherClin()
	{
		String redirection=null;
		
		if(nbClin>0)
			redirection="clins";
		return redirection;
	}
}