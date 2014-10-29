package pkgBackingBeans;

import java.io.File;
import java.io.Serializable;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;


import java.util.Calendar;
import java.util.Date;

import javax.faces.application.FacesMessage;
import javax.faces.context.FacesContext;

import org.icefaces.ace.component.fileentry.FileEntry;
import org.icefaces.ace.component.fileentry.FileEntryEvent;
import org.icefaces.ace.component.fileentry.FileEntryResults;

import pkgEntities.NiveauMembre;
import pkgEntities.Photo;
import pkgManagers.PhotoManager;

public class PhotoBackingBean implements Serializable {
	
	private MembreBackingBean mbb;
	private ArrayList<Photo> listePhotosMembre;
	private String chemin;
	private boolean showErreur;
	private String messageErreur;
	
	
	public MembreBackingBean getMbb() {
		return mbb;
	}
	public void setMbb(MembreBackingBean mbb) {
		this.mbb = mbb;
	}
	
	public ArrayList<Photo> getListePhotosMembre() {
		listePhotosMembre=new ArrayList<Photo>();
		listePhotosMembre=mbb.getMembre().getListePhotos();
		
		//supprimer photo du profile de la liste de photos
		for(Photo p:listePhotosMembre)
		{
			if(p.getIsProfil())
				listePhotosMembre.remove(p);
		}
		
		return listePhotosMembre;
	}
	public void setListePhotosMembre(ArrayList<Photo> listePhotosMembre) {
		this.listePhotosMembre = listePhotosMembre;
	}


	public String getChemin() {
		return chemin;
	}
	public void setChemin(String chemin) {
		this.chemin = chemin;
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
	public String supprimerPhoto(Photo photoSupprimer)
	{
		String destination=null;
		System.out.println(photoSupprimer.getId());
		int deleted=PhotoManager.deletePhoto(photoSupprimer.getId());
		if(deleted>0)
		{
			mbb.getMembre().setListePhotos(PhotoManager.getPhotosByMemberId(mbb.getMembre().getMembreId()));//maj de la liste de photos
			listePhotosMembre=mbb.getMembre().getListePhotos();
		}
		return destination;
	}
	
	public String ajouterPhotoProfil()
	{
			String destination=null;
			
			//suprrimer lancienne photo du phofil
			ArrayList<Photo> listePhotos=new ArrayList<Photo>();
			listePhotos=mbb.getMembre().getListePhotos();
			int idPhotoProfile=0;
			
			for(Photo photo : listePhotos)
			{
				if(photo.getIsProfil())
					idPhotoProfile=photo.getId();
			}
			
			int photoProfileSupprime=PhotoManager.deletePhoto(idPhotoProfile);
			
			if(photoProfileSupprime>0)
			{
				Photo photo= new Photo();
				photo.setMemberID(mbb.getMembre().getMembreId());
				photo.setChemin(chemin);
				photo.setIsProfil(true);
		
				int ajoute=PhotoManager.addPhoto(photo);
				System.out.println("photo ajoute");
				if(ajoute>0)
					mbb.getMembre().setListePhotos(PhotoManager.getPhotosByMemberId(mbb.getMembre().getMembreId()));//maj liste photos
			}
		
		return destination;
	}
	
public void uploadFileMembre(FileEntryEvent ev){
		
		System.out.println("uploadfilemembre");
		//mettre la main sur le fileEntry
		FileEntry fiE = (FileEntry)ev.getSource();
		//récupérer ses results
		FileEntryResults fr = fiE.getResults();
		
		// Create an instance of SimpleDateFormat used for formatting 
				// the string representation of date (month/day/year)
				DateFormat df = new SimpleDateFormat("ddMMyyyyHHmmss");

				// Get the date today using Calendar object.
				Date today = Calendar.getInstance().getTime();        
				// Using DateFormat format method we can create a string 
				// representation of a date with the defined format.
				String reportDate = df.format(today);
				
				NiveauMembre niveau=new NiveauMembre();
				niveau=mbb.getMembre().getNiveauMembre();
				int nbMaxPhotos=niveau.getNbPhotosMax();
				
				if(nbMaxPhotos>mbb.getMembre().getListePhotos().size())
				{
		
		//boucler sur les FileInfo
		for(FileEntryResults.FileInfo fi: fr.getFiles()){
			//s'assurer que le fichier est enregistrer
			if(fi.isSaved()){
				System.out.println("le nom ========= "+reportDate+fi.getFileName());
				//recu le fichier
				File f = fi.getFile();
				
				//TODO verifier que c'est le bon type de fichier
				//renommer
				try {
					String cheminApp=FacesContext.getCurrentInstance().getExternalContext().getRealPath("/");
					String newch=cheminApp;
					
					
					boolean ren=f.renameTo(new File(newch+"photos/"+reportDate+fi.getFileName()));
					
					if (ren) {
						this.chemin="photos/"+reportDate+fi.getFileName();
						System.out.println(newch);
					
					}
					else{
						System.out.println("pas possible. "+newch);
					}
				} catch (Exception e) {
					// TODO: handle exception
					e.printStackTrace();
				}
				
				
				
				//ajouter un message 
				FacesContext.getCurrentInstance().addMessage(fiE.getClientId(),new FacesMessage("le fichier a été uploadé"));
			
				//ajouter la photo a la bd	
					Photo photo= new Photo();
					photo.setMemberID(mbb.getMembre().getMembreId());
							
					photo.setChemin(chemin);
					photo.setIsProfil(false);
				
					int ajoute=PhotoManager.addPhoto(photo);
					
					if(ajoute>0)
					{
						System.out.println("photo ajoute");
						mbb.getMembre().setListePhotos(PhotoManager.getPhotosByMemberId(mbb.getMembre().getMembreId()));//maj liste photos
					}
				}
				else
				{
					showErreur=true;
					messageErreur="Vous n'avez plus le droit d'ajouter de photos";
					System.out.println("pas de doit. limite max photo");
				}
			}}
			
		}
public void uploadPhotoProfile(FileEntryEvent ev){
	
	//suprrimer lancienne photo du phofil
	ArrayList<Photo> listePhotos=new ArrayList<Photo>();
	listePhotos=mbb.getMembre().getListePhotos();
	int idPhotoProfile=0;
	
	for(Photo photo : listePhotos)
	{
		if(photo.getIsProfil())
			idPhotoProfile=photo.getId();
	}
	
	int photoProfileSupprime=PhotoManager.deletePhoto(idPhotoProfile);
	System.out.println(photoProfileSupprime);
	if(photoProfileSupprime>0)
	{
		
	//mettre la main sur le fileEntry
	FileEntry fiE = (FileEntry)ev.getSource();
	//récupérer ses results
	FileEntryResults fr = fiE.getResults();
	
	// Create an instance of SimpleDateFormat used for formatting 
			// the string representation of date (month/day/year)
			DateFormat df = new SimpleDateFormat("ddMMyyyyHHmmss");

			// Get the date today using Calendar object.
			Date today = Calendar.getInstance().getTime();        
			// Using DateFormat format method we can create a string 
			// representation of a date with the defined format.
			String reportDate = df.format(today);
			
	
	//boucler sur les FileInfo
	for(FileEntryResults.FileInfo fi: fr.getFiles()){
		//s'assurer que le fichier est enregistrer
		if(fi.isSaved()){
			System.out.println("le nom ========= "+reportDate+fi.getFileName());
			//recu le fichier
			File f = fi.getFile();
			
			//TODO verifier que c'est le bon type de fichier
			//renommer
			try {
				String cheminApp=FacesContext.getCurrentInstance().getExternalContext().getRealPath("/");
				String newch=cheminApp;
				
				
				boolean ren=f.renameTo(new File(newch+"photos/"+reportDate+fi.getFileName()));
				
				if (ren) {
					this.chemin="photos/"+reportDate+fi.getFileName();
					System.out.println(newch);
				
				}
				else{
					System.out.println("pas possible. "+newch);
				}
			} catch (Exception e) {
				// TODO: handle exception
				e.printStackTrace();
			}
			
			
			
			//ajouter un message 
			FacesContext.getCurrentInstance().addMessage(fiE.getClientId(),new FacesMessage("le fichier a été uploadé"));
		
			Photo photo= new Photo();
			photo.setMemberID(mbb.getMembre().getMembreId());
			photo.setChemin(chemin);
			photo.setIsProfil(true);

			int ajoute=PhotoManager.addPhoto(photo);
			System.out.println("photo profile ajoute");
			if(ajoute>0){
				mbb.getMembre().setListePhotos(PhotoManager.getPhotosByMemberId(mbb.getMembre().getMembreId()));//maj liste photos
				ArrayList<Photo> photos=new ArrayList<Photo>();
				photos=PhotoManager.getProfilPhotosByMemberId(mbb.getMembre().getMembreId());
				mbb.getMembre().setProfilImagePath(photos.get(0).getChemin()); //maj chemin photo profil
			}
			}
			
		}
	}	
	}
	
}
