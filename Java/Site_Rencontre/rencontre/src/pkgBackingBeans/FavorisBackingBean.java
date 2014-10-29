package pkgBackingBeans;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Date;

import pkgEntities.Clinsdoeil;
import pkgEntities.Membre;
import pkgEntities.Message;
import pkgEntities.NiveauMembre;
import pkgManagers.ClinsdoeilManager;
import pkgManagers.FavorisManager;
import pkgManagers.MembreManager;
import pkgManagers.MessageManager;
import services.SendEmail;

public class FavorisBackingBean implements Serializable {

	//user session
	private MembreBackingBean mbb;
	private Membre membreSupprimer;
	//afficher listes favoris
	private ArrayList<Membre> listeFavorisOnline;
	
	//recuperer la reponse
	private String reponse;
	

	public MembreBackingBean getMbb() {
		return mbb;
	}

	public void setMbb(MembreBackingBean mbb) {
		this.mbb = mbb;
	}

	public Membre getMembreSupprimer() {
		return membreSupprimer;
	}

	public void setMembreSupprimer(Membre membreSupprimer) {
		this.membreSupprimer = membreSupprimer;
	}

	public ArrayList<Membre> getListeFavorisOnline() {
		listeFavorisOnline=new ArrayList<Membre>();
		ArrayList<Membre> listeFavoris=new ArrayList<Membre>();
		listeFavoris=mbb.getListeFavoris();
		
		for(Membre m: listeFavoris)
		{
			if(m.getIsOnline()==true)
				listeFavorisOnline.add(m);
		}
		return listeFavorisOnline;
	}

	public void setListeFavorisOnline(ArrayList<Membre> listeFavorisOnline) {
		this.listeFavorisOnline = listeFavorisOnline;
	}

	public String getReponse() {
		return reponse;
	}

	public void setReponse(String reponse) {
		this.reponse = reponse;
	}

	public String ajouterFavoris(Membre membreCible)
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
			int ajoute=FavorisManager.addFavoris(new Date(), mbb.getMembre().getMembreId(), membreCible.getMembreId());
			
			boolean recevoirCourriel=false;
			recevoirCourriel=membreCible.getInformed_added_by_others();
			if(ajoute>0 && recevoirCourriel){
				//envoyer courriel a qui a ete ajoute
				SendEmail.envoyerMail(mbb.getMembre(), membreCible, "ajoute");
			}
		}
		return destination;
	}
	
	public String supprimerFavoris(Membre membreCible)
	{
		String destination=null;
		
		int deleted=FavorisManager.deleteFavoris(mbb.getMembre().getMembreId(), membreCible.getMembreId());
		
		boolean recevoirCourriel=false;
		recevoirCourriel=membreCible.getInformed_removed_by_others();
		if(deleted>0 && recevoirCourriel){
			//envoyer courriel a qui a ete supprime
			SendEmail.envoyerMail(mbb.getMembre(), membreCible, "supprime");
		}
			
		return destination;
	}
	
	public String envoyerClinDoeil(Membre membreCible)
	{
		String destination=null;
		
		Clinsdoeil clin=new Clinsdoeil();
		clin.setFromId(mbb.getMembre().getMembreId());
		clin.setToId(membreCible.getMembreId());
		//get date now
		Date date = new Date();
		clin.setDateEnvoi(date);
		
		ClinsdoeilManager.addClindoeil(clin);
		
		return destination;
	}
	
	public String envoyerMessage(Membre membreCible)
	{
		String destination=null;
		Message message=new Message();
		message.setContenu(reponse);
		message.setDejalu(false);
		message.setMsgFrom(mbb.getMembre().getMembreId());
		message.setMsgTo(membreCible.getMembreId());
		message.setReplyToMsgId(message.getMessageid());
		message.setSendDate(new Date());
		
		int ajoute=MessageManager.addMessage(message);
		boolean recevoirCourriel=false;
		recevoirCourriel=membreCible.getInformed_message_received();
		
		if(ajoute>0 && recevoirCourriel){
				SendEmail.envoyerMail(mbb.getMembre(), membreCible, "message");
		
	}
		return destination;
	}
	
	public void	supprimerAmis()
	{
		int deleted=FavorisManager.deleteFavoris(mbb.getMembre().getMembreId(), membreSupprimer.getMembreId());
		
		boolean recevoirCourriel=false;
		recevoirCourriel=membreSupprimer.getInformed_removed_by_others();
		if(deleted>0 && recevoirCourriel){
			//envoyer courriel a qui a ete supprime
			SendEmail.envoyerMail(mbb.getMembre(), membreSupprimer, "supprime");
		}	
	}
	
	
}
