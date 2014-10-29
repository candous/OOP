package pkgBackingBeans;

import java.io.Serializable;
import java.util.ArrayList;

import javax.faces.application.FacesMessage;
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
import pkgEntities.NiveauMembre;
import pkgEntities.Photo;
import pkgEntities.SkinColor;
import pkgEntities.Status;
import pkgEntities.WeightRange;
import pkgManagers.CategorieManager;
import pkgManagers.CityManager;
import pkgManagers.EyeColorManager;
import pkgManagers.FavorisManager;
import pkgManagers.HairColorManager;
import pkgManagers.HobbyManager;
import pkgManagers.MembreManager;
import pkgManagers.MessageManager;
import pkgManagers.NiveauMembreManager;
import pkgManagers.PhotoManager;
import pkgManagers.SkinColorManager;
import pkgManagers.StatusManager;
import pkgManagers.WeightRangeManager;

public class InscriptionBackingBean implements Serializable {

	private Membre membreInscrit;
	private HttpSession session;
	
	// inscription
	private String nom;
	private String prenom;
	private Integer age;
	private String description;
	private String email;
	private String password;
	private String pseudo;
	private Integer categorie;
	private Integer hairColor_id;
	private Integer skinColor_id;
	private Integer eyeColor_id;
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

	// afficher dans la vue inscription
	private ArrayList<SelectItem> listeCategorie;
	private ArrayList<SelectItem> listeHairColor;
	private ArrayList<SelectItem> listeSkinColor;
	private ArrayList<SelectItem> listeEyeColor;
	private ArrayList<SelectItem> listeTypePhysique;
	private ArrayList<SelectItem> listeStatus;
	private ArrayList<SelectItem> listeHobbiesAfficher;
	private ArrayList<SelectItem> listeVilles;
	private ArrayList<SelectItem> listeNiveux;
	private boolean show;
	private String message;

	
	public Membre getMembreInscrit() {
		return membreInscrit;
	}

	public void setMembreInscrit(Membre membreInscrit) {
		this.membreInscrit = membreInscrit;
	}

	public HttpSession getSession() {
		return session;
	}

	public void setSession(HttpSession session) {
		this.session = session;
	}
	
	
	
	public String getNom() {
		return nom;
	}

	public void setNom(String nom) {
		this.nom = nom;
	}

	public String getPrenom() {
		return prenom;
	}

	public void setPrenom(String prenom) {
		this.prenom = prenom;
	}

	public Integer getAge() {
		return age;
	}

	public void setAge(Integer age) {
		this.age = age;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public String getPseudo() {
		return pseudo;
	}

	public void setPseudo(String pseudo) {
		this.pseudo = pseudo;
	}

	public Integer getCategorie() {
		return categorie;
	}

	public void setCategorie(Integer categorie) {
		this.categorie = categorie;
	}

	public Integer getHairColor_id() {
		return hairColor_id;
	}

	public void setHairColor_id(Integer hairColor_id) {
		this.hairColor_id = hairColor_id;
	}

	public Integer getSkinColor_id() {
		return skinColor_id;
	}

	public void setSkinColor_id(Integer skinColor_id) {
		this.skinColor_id = skinColor_id;
	}

	public Integer getEyeColor_id() {
		return eyeColor_id;
	}

	public void setEyeColor_id(Integer eyeColor_id) {
		this.eyeColor_id = eyeColor_id;
	}

	public Integer getGrandeur() {
		return grandeur;
	}

	public void setGrandeur(Integer grandeur) {
		this.grandeur = grandeur;
	}

	public Integer getWeightrange_id() {
		return weightrange_id;
	}

	public void setWeightrange_id(Integer weightrange_id) {
		this.weightrange_id = weightrange_id;
	}

	public Integer getCity_id() {
		return city_id;
	}

	public void setCity_id(Integer city_id) {
		this.city_id = city_id;
	}

	public Boolean getSmoke() {
		return smoke;
	}

	public void setSmoke(Boolean smoke) {
		this.smoke = smoke;
	}

	public Integer getStatus_id() {
		return status_id;
	}

	public void setStatus_id(Integer status_id) {
		this.status_id = status_id;
	}

	public ArrayList<Integer> getHobbies() {
		return hobbies;
	}

	public void setHobbies(ArrayList<Integer> hobbies) {
		this.hobbies = hobbies;
	}

	public Boolean getCourrielMessage() {
		return courrielMessage;
	}

	public void setCourrielMessage(Boolean courrielMessage) {
		this.courrielMessage = courrielMessage;
	}

	public Boolean getCourrielAjoute() {
		return courrielAjoute;
	}

	public void setCourrielAjoute(Boolean courrielAjoute) {
		this.courrielAjoute = courrielAjoute;
	}

	public Boolean getCourrielSupprime() {
		return courrielSupprime;
	}

	public void setCourrielSupprime(Boolean courrielSupprime) {
		this.courrielSupprime = courrielSupprime;
	}

	public Integer getNiveauMembre() {
		return niveauMembre;
	}

	public void setNiveauMembre(Integer niveauMembre) {
		this.niveauMembre = niveauMembre;
	}

	public ArrayList<SelectItem> getListeCategorie() {
		listeCategorie=new ArrayList<SelectItem>();
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

	public ArrayList<SelectItem> getListeHairColor() {
		listeHairColor=new ArrayList<SelectItem>();
		ArrayList<HairColor> hairColors = new ArrayList<HairColor>();
		hairColors = HairColorManager.getListeHairColors();
		for (HairColor hc : hairColors) {
			SelectItem item = new SelectItem(hc.getId(), hc.getDescription());
			listeHairColor.add(item);
		}

		return listeHairColor;
	}

	public void setListeHairColor(ArrayList<SelectItem> listeHairColor) {
		this.listeHairColor = listeHairColor;
	}

	public ArrayList<SelectItem> getListeSkinColor() {
		listeSkinColor=new ArrayList<SelectItem>();
		ArrayList<SkinColor> skinColors = new ArrayList<SkinColor>();
		skinColors = SkinColorManager.getListeSkinColors();
		for (SkinColor sc : skinColors) {
			SelectItem item = new SelectItem(sc.getId(), sc.getDescription());
			listeSkinColor.add(item);
		}
		return listeSkinColor;
	}

	public void setListeSkinColor(ArrayList<SelectItem> listeSkinColor) {
		this.listeSkinColor = listeSkinColor;
	}

	public ArrayList<SelectItem> getListeEyeColor() {
		listeEyeColor=new ArrayList<SelectItem>();
		ArrayList<EyeColor> eyeColors = new ArrayList<EyeColor>();
		eyeColors = EyeColorManager.getListeEyeColors();
		for (EyeColor ec : eyeColors) {
			SelectItem item = new SelectItem(ec.getId(), ec.getDescription());
			listeEyeColor.add(item);
		}
		return listeEyeColor;
	}

	public void setListeEyeColor(ArrayList<SelectItem> listeEyeColor) {
		this.listeEyeColor = listeEyeColor;
	}

	public ArrayList<SelectItem> getListeTypePhysique() {
		listeTypePhysique=new ArrayList<SelectItem>();
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

	public ArrayList<SelectItem> getListeStatus() {
		listeStatus=new ArrayList<SelectItem>();
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

	public void setListeHobbiesAfficher(ArrayList<SelectItem> listeHobbiesAfficher) {
		this.listeHobbiesAfficher = listeHobbiesAfficher;
	}

	public ArrayList<SelectItem> getListeHobbiesAfficher() {
		listeHobbiesAfficher=new ArrayList<SelectItem>();
		ArrayList<Hobby> hobbies = new ArrayList<Hobby>();
		hobbies = HobbyManager.getListeHobbies();
		for (Hobby h : hobbies) {
			SelectItem item = new SelectItem(h.getId(), h.getDescription());
			listeHobbiesAfficher.add(item);
		}

		return listeHobbiesAfficher;
	}

	public ArrayList<SelectItem> getListeVilles() {
		listeVilles=new ArrayList<SelectItem>();
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

	public ArrayList<SelectItem> getListeNiveux() {
		listeNiveux=new ArrayList<SelectItem>();
		ArrayList<NiveauMembre> niveaux = new ArrayList<NiveauMembre>();
		niveaux = NiveauMembreManager.getListeNiveauMembres();
		for (NiveauMembre nm : niveaux) {
			SelectItem item = new SelectItem(nm.getId(), nm.getDescription());
			listeNiveux.add(item);
		}
		
		return listeNiveux;
	}

	public void setListeNiveux(ArrayList<SelectItem> listeNiveux) {
		this.listeNiveux = listeNiveux;
	}

	public boolean isShow() {
		return show;
	}

	public void setShow(boolean show) {
		this.show = show;
	}

	public String getMessage() {
		message="Pseudo Invalide, choisissez un autre, svp.";
		return message;
	}

	public void setMessage(String message) {
		this.message = message;
	}

	public String testerUser() {
		String redirection = null;

		membreInscrit = MembreManager.getMembreByNicknamePassword(pseudo, password);
		// user n'existe pas
		if (membreInscrit == null){
			redirection = "oui";
			setShow(false);
		}
		else{
			// user existe= show error message
			setShow(true);
		}
		return redirection;
	}

	public String inscrire() {
		String redirection = null;
		int modifications = 0;
		// creation du user
		membreInscrit = new Membre();
		membreInscrit.setNom(nom);
		membreInscrit.setPrenom(prenom);
		membreInscrit.setAge(age);
		membreInscrit.setDescription(description);
		membreInscrit.setEmail(email);
		membreInscrit.setPassword(password);
		membreInscrit.setPseudo(pseudo);

		Categorie categ = new Categorie();
		categ = CategorieManager.getCategorieById(categorie);
		membreInscrit.setCategorie(categ);

		HairColor hc = new HairColor();
		hc = HairColorManager.getHairColorById(hairColor_id);
		membreInscrit.setHairColor(hc);

		SkinColor sc = new SkinColor();
		sc = SkinColorManager.getSkinColorById(skinColor_id);
		membreInscrit.setSkinColor(sc);

		EyeColor ec = new EyeColor();
		ec = EyeColorManager.getEyeColorById(eyeColor_id);
		membreInscrit.setEyeColor(ec);

		WeightRange wr = new WeightRange();
		wr = WeightRangeManager.getWeightRangeById(weightrange_id);
		membreInscrit.setWeightRange(wr);

		City city = new City();
		city = CityManager.getCityById(city_id);
		membreInscrit.setCity(city);

		Status st = new Status();
		st = StatusManager.getStatusById(status_id);
		membreInscrit.setStatus(st);
		
		membreInscrit.setHeight(grandeur);
		if(smoke==null)
			smoke=false;
		membreInscrit.setSmoke(smoke);
		if(courrielMessage==null)
			courrielMessage=false;
		if(courrielAjoute==null)
			courrielAjoute=false;
		if(courrielSupprime==null)
			courrielSupprime=false;
		membreInscrit.setInformed_message_received(courrielMessage);
		membreInscrit.setInformed_added_by_others(courrielAjoute);
		membreInscrit.setInformed_removed_by_others(courrielSupprime);
		
		NiveauMembre nm = new NiveauMembre();
		nm = NiveauMembreManager. getNiveauMembreById(niveauMembre);
		membreInscrit.setNiveauMembre(nm);
		membreInscrit.setIsOnline(true);
		
//		ArrayList<Hobby> listeHobbies=new ArrayList<Hobby>();
//		//peopler les hobbies du user
//		for(Integer h:hobbies)
//		{
//			Hobby hobby=new Hobby();
//			hobby=HobbyManager.getHobbyById(h);
//			listeHobbies.add(hobby);
//			membreInscrit.setListeHobbies(listeHobbies);
//		}

		// insertion dans la BD
		int userId = MembreManager.addMembre(membreInscrit);

		if (userId!=0) {
			//photo par default
			Photo photoProfile=new Photo();
			photoProfile.setChemin("photos/profil.jpg");
			photoProfile.setIsProfil(true);
			photoProfile.setMemberID(userId);
			PhotoManager.addPhoto(photoProfile);
			
			ArrayList<Hobby> listHobbies= new ArrayList<Hobby>();
			HobbyManager.addMemberHobbyLinkEntry(userId, listHobbies);

			redirection = "oui";
		}
		
		return redirection;
	}
	public String signup(){
		return "signup";
	}
	public String login(){
		return "login";
	}
}