package pkgEntities;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Date;

import javax.swing.text.StyleContext.SmallAttributeSet;


public class Membre implements Serializable {
	private int membreId;
	private String nom;
	private String prenom;
	private String description;
	private String pseudo;
	private String password;
	private int age;
	private String email;
	private Categorie categorie;
	private Boolean isOnline;
	private NiveauMembre niveauMembre;
	private Boolean sexe;
	private Date lastVisit;
	private Boolean smoke;
	private int height;
	private HairColor hairColor;
	private SkinColor skinColor;
	private EyeColor eyeColor;
	private WeightRange weightRange;
	private City city;
	private Status status;
	private Boolean informed_message_received;
	private Boolean informed_added_by_others;
	private Boolean informed_removed_by_others;
	private String profilImagePath;
	
	private ArrayList<Membre> listeFavoris;
	private ArrayList<Hobby> listeHobbies;
	private ArrayList<Photo> listePhotos;
	private ArrayList<Message> listeMessagesRecus;
	private ArrayList<Message> listeMessagesPasLus;
	private ArrayList<Message> listeMessagesEnvoyes;
	
	public int getMembreId() {
		return membreId;
	}
	public void setMembreId(int membreId) {
		this.membreId = membreId;
	}
	public String getProfilImagePath() {
		return profilImagePath;
	}
	public void setProfilImagePath(String profilImagePath) {
		this.profilImagePath = profilImagePath;
	}
	public ArrayList<Message> getListeMessagesEnvoyes() {
		return listeMessagesEnvoyes;
	}
	public void setListeMessagesEnvoyes(ArrayList<Message> listeMessagesEnvoyes) {
		this.listeMessagesEnvoyes = listeMessagesEnvoyes;
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
	public String getDescription() {
		return description;
	}
	public void setDescription(String description) {
		this.description = description;
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
	public int getAge() {
		return age;
	}
	public void setAge(int age) {
		this.age = age;
	}
	public String getEmail() {
		return email;
	}
	public void setEmail(String email) {
		this.email = email;
	}
	public Categorie getCategorie() {
		return categorie;
	}
	public void setCategorie(Categorie categorie) {
		this.categorie = categorie;
	}
	public Boolean getIsOnline() {
		return isOnline;
	}
	public void setIsOnline(Boolean isOnline) {
		this.isOnline = isOnline;
	}
	public NiveauMembre getNiveauMembre() {
		return niveauMembre;
	}
	public void setNiveauMembre(NiveauMembre niveauMembre) {
		this.niveauMembre = niveauMembre;
	}
	public Boolean getSexe() {
		return sexe;
	}
	public void setSexe(Boolean sexe) {
		this.sexe = sexe;
	}
	public Date getLastVisit() {
		return lastVisit;
	}
	public void setLastVisit(Date lastVisit) {
		this.lastVisit = lastVisit;
	}
	public Boolean getSmoke() {
		return smoke;
	}
	public void setSmoke(Boolean smoke) {
		this.smoke = smoke;
	}
	public int getHeight() {
		return height;
	}
	public void setHeight(int height) {
		this.height = height;
	}
	public HairColor getHairColor() {
		return hairColor;
	}
	public void setHairColor(HairColor hairColor) {
		this.hairColor = hairColor;
	}
	public SkinColor getSkinColor() {
		return skinColor;
	}
	public void setSkinColor(SkinColor skinColor) {
		this.skinColor = skinColor;
	}
	public EyeColor getEyeColor() {
		return eyeColor;
	}
	public void setEyeColor(EyeColor eyeColor) {
		this.eyeColor = eyeColor;
	}
	public WeightRange getWeightRange() {
		return weightRange;
	}
	public void setWeightRange(WeightRange weightRange) {
		this.weightRange = weightRange;
	}
	public City getCity() {
		return city;
	}
	public void setCity(City city) {
		this.city = city;
	}
	public Status getStatus() {
		return status;
	}
	public void setStatus(Status status) {
		this.status = status;
	}
	public Boolean getInformed_message_received() {
		return informed_message_received;
	}
	public void setInformed_message_received(Boolean informed_message_received) {
		this.informed_message_received = informed_message_received;
	}
	public Boolean getInformed_added_by_others() {
		return informed_added_by_others;
	}
	public void setInformed_added_by_others(Boolean informed_added_by_others) {
		this.informed_added_by_others = informed_added_by_others;
	}
	public Boolean getInformed_removed_by_others() {
		return informed_removed_by_others;
	}
	public void setInformed_removed_by_others(Boolean informed_removed_by_others) {
		this.informed_removed_by_others = informed_removed_by_others;
	}
	public ArrayList<Membre> getListeFavoris() {
		return listeFavoris;
	}
	public void setListeFavoris(ArrayList<Membre> listeFavoris) {
		this.listeFavoris = listeFavoris;
	}
	public ArrayList<Hobby> getListeHobbies() {
		return listeHobbies;
	}
	public void setListeHobbies(ArrayList<Hobby> listeHobbies) {
		this.listeHobbies = listeHobbies;
	}
	public ArrayList<Photo> getListePhotos() {
		return listePhotos;
	}
	public void setListePhotos(ArrayList<Photo> listePhotos) {
		this.listePhotos = listePhotos;
	}
	
	public ArrayList<Message> getListeMessagesRecus() {
		return listeMessagesRecus;
	}
	public void setListeMessagesRecus(ArrayList<Message> listeMessagesRecus) {
		this.listeMessagesRecus = listeMessagesRecus;
	}
	public ArrayList<Message> getListeMessagesPasLus() {
		return listeMessagesPasLus;
	}
	public void setListeMessagesPasLus(ArrayList<Message> listeMessagesPasLus) {
		this.listeMessagesPasLus = listeMessagesPasLus;
	}
	
	@Override
	public String toString() {
		return "Membre [membreId=" + membreId + ", nom=" + nom + ", prenom="
				+ prenom + ", description=" + description + ", pseudo="
				+ pseudo + ", password=" + password + ", age=" + age
				+ ", email=" + email + ", categorie=" + categorie
				+ ", isOnline=" + isOnline + ", niveauMembre=" + niveauMembre
				+ ", sexe=" + sexe + ", lastVisit=" + lastVisit + ", smoke="
				+ smoke + ", height=" + height + ", hairColor=" + hairColor
				+ ", skinColor=" + skinColor + ", eyeColor=" + eyeColor
				+ ", weightRange=" + weightRange + ", city=" + city
				+ ", status=" + status + ", informed_message_received="
				+ informed_message_received + ", informed_added_by_others="
				+ informed_added_by_others + ", informed_removed_by_others="
				+ informed_removed_by_others + ", profilImagePath="
				+ profilImagePath + ", listeFavoris=" + listeFavoris
				+ ", listeHobbies=" + listeHobbies + ", listePhotos="
				+ listePhotos + ", listeMessagesRecus=" + listeMessagesRecus
				+ ", listeMessagesPasLus=" + listeMessagesPasLus
				+ ", listeMessagesEnvoyes=" + listeMessagesEnvoyes + "]";
	}

	
}
