package pkgBackingBeans;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Date;

import javax.faces.model.SelectItem;

import pkgEntities.Categorie;
import pkgEntities.City;
import pkgEntities.HairColor;
import pkgEntities.Hobby;
import pkgEntities.Membre;
import pkgEntities.NiveauMembre;
import pkgEntities.Photo;
import pkgEntities.Status;
import pkgEntities.WeightRange;
import pkgManagers.CategorieManager;
import pkgManagers.CityManager;
import pkgManagers.HairColorManager;
import pkgManagers.HobbyManager;
import pkgManagers.MembreManager;
import pkgManagers.NiveauMembreManager;
import pkgManagers.PhotoManager;
import pkgManagers.StatusManager;
import pkgManagers.WeightRangeManager;

public class ModifierMembreBackingBean implements Serializable {

	private MembreBackingBean mbb;
	private Membre membreModifie;
	
	private boolean showErreur;
	private String messageErreur;
	
	//binder les infos 
	private Integer age;
	private String description;
	private String email;
	private String password;
	private String pseudo;
	private Integer categorie;
	private Integer hairColor_id;
	private Integer grandeur;
	private Integer weightrange_id;
	private Integer city_id;
	private Boolean smoke;
	private Integer status_id;
	private ArrayList<Integer> hobbies;
	private Boolean courrielMessage;
	private Boolean courrielAjoute;
	private Boolean courrielSupprime;
	private Integer niveauMembre;
	//recuperer le chemin de la photo
	
	
	private ArrayList<SelectItem> listeTypePhysique;
	private ArrayList<SelectItem> listeVilles;
	private ArrayList<SelectItem> listeStatus;
	private ArrayList<SelectItem> listeHobbies;
	private ArrayList<SelectItem> listeCategorie;
	private ArrayList<SelectItem> listeHairColors;
	
	
	public MembreBackingBean getMbb() {
		return mbb;
	}
	public void setMbb(MembreBackingBean mbb) {
		this.mbb = mbb;
	}
	public Membre getMembreModifie() {
		return membreModifie;
	}
	public void setMembreModifie(Membre membreModifie) {
		this.membreModifie = membreModifie;
	}

	public boolean isShowErreur() {
		return showErreur;
	}
	public void setShowErreur(boolean showErreur) {
		this.showErreur = showErreur;
	}
	public String getMessageErreur() {
		return messageErreur;
	}
	public void setMessageErreur(String messageErreur) {
		this.messageErreur = messageErreur;
	}
	public Integer getAge() {
		age=mbb.getMembre().getAge();
		
		return age;
	}
	public void setAge(Integer age) {
		this.age = age;
	}
	public String getDescription() {
		description=mbb.getMembre().getDescription();
		return description;
	}
	public void setDescription(String description) {
		this.description = description;
	}
	public String getEmail() {
		email=mbb.getMembre().getEmail();
		return email;
	}
	public void setEmail(String email) {
		this.email = email;
	}
	public String getPassword() {
		password=mbb.getMembre().getPassword();
		return password;
	}
	public void setPassword(String password) {
		this.password = password;
	}
	public String getPseudo() {
		pseudo=mbb.getMembre().getPseudo();
		return pseudo;
	}
	public void setPseudo(String pseudo) {
		this.pseudo = pseudo;
	}
	public Integer getCategorie() {
		categorie=mbb.getMembre().getCategorie().getCategorieID();
		
		return categorie;
	}
	public void setCategorie(Integer categorie) {
		this.categorie = categorie;
	}
	public Integer getHairColor_id() {
		hairColor_id=mbb.getMembre().getHairColor().getId();
		return hairColor_id;
	}
	public void setHairColor_id(Integer hairColor_id) {
		this.hairColor_id = hairColor_id;
	}
	public Integer getGrandeur() {
		grandeur=mbb.getMembre().getHeight();
		return grandeur;
	}
	public void setGrandeur(Integer grandeur) {
		this.grandeur = grandeur;
	}
	public Integer getWeightrange_id() {
		weightrange_id=mbb.getMembre().getWeightRange().getId();
		return weightrange_id;
	}
	public void setWeightrange_id(Integer weightrange_id) {
		this.weightrange_id = weightrange_id;
	}
	public Integer getCity_id() {
		city_id=mbb.getMembre().getCity().getId();
		return city_id;
	}
	public void setCity_id(Integer city_id) {
		this.city_id = city_id;
	}
	public Boolean getSmoke() {
		smoke=mbb.getMembre().getSmoke();
		return smoke;
	}
	public void setSmoke(Boolean smoke) {
		this.smoke = smoke;
	}
	public Integer getStatus_id() {
		status_id=mbb.getMembre().getStatus().getId();
		return status_id;
	}
	public void setStatus_id(Integer status_id) {
		this.status_id = status_id;
	}
	public ArrayList<Integer> getHobbies() {
		hobbies=new ArrayList<Integer>();
		ArrayList<Hobby> listeHobbiesCoches=new ArrayList<Hobby>();
		listeHobbiesCoches=mbb.getMembre().getListeHobbies();
		for(Hobby h:listeHobbiesCoches)
		{
			hobbies.add(h.getId());
		}
		return hobbies;
	}
	public void setHobbies(ArrayList<Integer> hobbies) {
		this.hobbies = hobbies;
	}
	public Boolean getCourrielMessage() {
		courrielMessage=mbb.getMembre().getInformed_message_received();
		return courrielMessage;
	}
	public void setCourrielMessage(Boolean courrielMessage) {
		this.courrielMessage = courrielMessage;
	}
	public Boolean getCourrielAjoute() {
		courrielAjoute=mbb.getMembre().getInformed_added_by_others();
		return courrielAjoute;
	}
	public void setCourrielAjoute(Boolean courrielAjoute) {
		this.courrielAjoute = courrielAjoute;
	}
	public Boolean getCourrielSupprime() {
		courrielSupprime=mbb.getMembre().getInformed_removed_by_others();
		return courrielSupprime;
	}
	public void setCourrielSupprime(Boolean courrielSupprime) {
		this.courrielSupprime = courrielSupprime;
	}
	public Integer getNiveauMembre() {
		niveauMembre=mbb.getMembre().getNiveauMembre().getId();
		return niveauMembre;
	}
	public void setNiveauMembre(Integer niveauMembre) {
		this.niveauMembre = niveauMembre;
	}
	
	
	//LISTES AFFICHAGE
	public ArrayList<SelectItem> getListeTypePhysique() {
		
		listeTypePhysique = new ArrayList<SelectItem>();
		ArrayList<WeightRange> typesPhysiques = new ArrayList<WeightRange>();
		typesPhysiques = WeightRangeManager.getListeWeightRanges();
		for (WeightRange wr : typesPhysiques) {
			SelectItem item = new SelectItem(wr.getId(), wr.getDescription());
			listeTypePhysique.add(item);
		}
		return listeTypePhysique;
	}
	public void setListeTypePhysique(ArrayList<SelectItem> listeTypePhysique) {
		this.listeTypePhysique = listeTypePhysique;
	}
	public ArrayList<SelectItem> getListeVilles() {
		
		listeVilles = new ArrayList<SelectItem>();
		ArrayList<City> cities = new ArrayList<City>();
		cities = CityManager.getListeCities();
		for (City c : cities) {
			SelectItem item = new SelectItem(c.getId(), c.getDescription());
			listeVilles.add(item);
		}
		return listeVilles;
	}
	public void setListeVilles(ArrayList<SelectItem> listeVilles) {
		this.listeVilles = listeVilles;
	}
	public ArrayList<SelectItem> getListeStatus() {
		
		listeStatus = new ArrayList<SelectItem>();
		ArrayList<Status> lesStatus = new ArrayList<Status>();
		lesStatus = StatusManager.getListeStatus();
		for (Status st : lesStatus) {
			SelectItem item = new SelectItem(st.getId(), st.getDescription());
			listeStatus.add(item);
		}
		
		return listeStatus;
	}
	public void setListeStatus(ArrayList<SelectItem> listeStatus) {
		this.listeStatus = listeStatus;
	}
	public ArrayList<SelectItem> getListeHobbies() {
		
		listeHobbies = new ArrayList<SelectItem>();
		ArrayList<Hobby> hobbies = new ArrayList<Hobby>();
		hobbies = HobbyManager.getListeHobbies();
		for (Hobby h : hobbies) {
			SelectItem item = new SelectItem(h.getId(), h.getDescription());
			listeHobbies.add(item);
		}
		return listeHobbies;
	}
	public void setListeHobbies(ArrayList<SelectItem> listeHobbies) {
		this.listeHobbies = listeHobbies;
	}
	public ArrayList<SelectItem> getListeCategorie() {
		listeCategorie = new ArrayList<SelectItem>();
		ArrayList<Categorie> categories = new ArrayList<Categorie>();
		categories = CategorieManager.getListeCategories();
		for (Categorie c : categories) {
			SelectItem item = new SelectItem(c.getCategorieID(),
					c.getDescription());
			listeCategorie.add(item);
		}
		
		return listeCategorie;
	}
	public void setListeCategorie(ArrayList<SelectItem> listeCategorie) {
		this.listeCategorie = listeCategorie;
	}
	public ArrayList<SelectItem> getListeHairColors() {
		
		listeHairColors = new ArrayList<SelectItem>();
		ArrayList<HairColor> hairColors = new ArrayList<HairColor>();
		hairColors = HairColorManager.getListeHairColors();
		for (HairColor hc : hairColors) {
			SelectItem item = new SelectItem(hc.getId(), hc.getDescription());
			listeHairColors.add(item);
		}

		return listeHairColors;
	}
	public void setListeHairColors(ArrayList<SelectItem> listeHairColors) {
		this.listeHairColors = listeHairColors;
	}
	
	
	public String modifierUser()
	{
		String redirection=null;
		membreModifie=mbb.getMembre();
		//user pour verifier si quelqun dautre utilise le pseudo 
		Membre user=new Membre();
		user=MembreManager.getMembreByNickname(membreModifie.getPseudo());
		//si un user avec le pseudo nexiste pas ou s'il est le user qu'on change
		if(user==null || user.getMembreId()==membreModifie.getMembreId()){
			//membreModifie.setAge(age);
			//membreModifie.setDescription(description);
			
		//	membreModifie.setEmail(email);
		//	membreModifie.setPassword(password);
		//	membreModifie.setPseudo(pseudo);
			
			//Categorie categ = new Categorie();
			//categ = CategorieManager.getCategorieById(categorie);
			//membreModifie.setCategorie(categ);
			
			//HairColor hc = new HairColor();
			//hc = HairColorManager.getHairColorById(hairColor_id);
			//membreModifie.setHairColor(hc);
			
			//membreModifie.setHeight(grandeur);
			
			//WeightRange wr = new WeightRange();
			//wr = WeightRangeManager.getWeightRangeById(weightrange_id);
			//membreModifie.setWeightRange(wr);
			
		//	City city = new City();
		//	city = CityManager.getCityById(city_id);
		//	membreModifie.setCity(city);
			
		//	membreModifie.setSmoke(smoke);
			
		//	Status st = new Status();
		//	st = StatusManager.getStatusById(status_id);
		//	membreModifie.setStatus(st);
			
			//membreModifie.setInformed_message_received(courrielMessage);
			//membreModifie.setInformed_added_by_others(courrielAjoute);
			//membreModifie.setInformed_removed_by_others(courrielSupprime);
			//NiveauMembre nm = new NiveauMembre();
			//nm = NiveauMembreManager.getNiveauMembreById(niveauMembre);
			//membreModifie.setNiveauMembre(nm);
			
//			ArrayList<Hobby> listeHobbies=new ArrayList<Hobby>();
//			//peopler les hobbies du user
//			for(Integer h:hobbies){
//				Hobby hobby=new Hobby();
//				hobby=HobbyManager.getHobbyById(h);
//				listeHobbies.add(hobby);
//				membreModifie.setListeHobbies(listeHobbies);
		
	//	}
		int modifie=MembreManager.updateMembre(membreModifie);
	}
		return redirection;
}
}
