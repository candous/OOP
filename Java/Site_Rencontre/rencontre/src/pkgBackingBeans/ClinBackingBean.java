package pkgBackingBeans;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Date;

import pkgEntities.Clinsdoeil;
import pkgEntities.Membre;
import pkgEntities.Message;
import pkgManagers.ClinsdoeilManager;
import pkgManagers.MembreManager;


public class ClinBackingBean implements Serializable {

	private MembreBackingBean mbb;
	private Membre userEnvoye;
	private ArrayList<Clinsdoeil> listeClin;
	private Clinsdoeil clin;
	
	public MembreBackingBean getMbb() {
		return mbb;
	}

	public void setMbb(MembreBackingBean mbb) {
		this.mbb = mbb;
	}

	public Membre getUserEnvoye() {
		return userEnvoye;
	}

	public void setUserEnvoye(Membre userEnvoye) {
		this.userEnvoye = userEnvoye;
	}

	public ArrayList<Clinsdoeil> getListeClin() {
		listeClin=new ArrayList<Clinsdoeil>();
		listeClin=ClinsdoeilManager.getClinsdoeilRecus(mbb.getMembre().getMembreId());
			
		return listeClin;
	}

	public void setListeClin(ArrayList<Clinsdoeil> listeClin) {
		this.listeClin = listeClin;
	}

	public Clinsdoeil getClin() {
		return clin;
	}

	public void setClin(Clinsdoeil clin) {
		this.clin = clin;
	}

	public String repondreClin(Clinsdoeil clinARepondre)
	{
		String destination=null;
		//creation du clin reponse
		Clinsdoeil clinReponse=new Clinsdoeil();
		clinReponse.setDateEnvoi(new Date());
		clinReponse.setFromId(mbb.getMembre().getMembreId());
		clinReponse.setToId(clinARepondre.getFromId());
		int repondu=0;
		repondu=ClinsdoeilManager.addClindoeil(clinReponse);
		
		if(repondu>0)
			destination="oui";
		
		return destination;
	}
	
	public String deleteClin(Clinsdoeil clinSupprimer)
	{
		String destination=null;
		int deleted=0;
		deleted=ClinsdoeilManager.deleteClin(clinSupprimer);
		
		if(deleted>0){
			destination="oui";
			//metre a jour liste de clins
			listeClin=ClinsdoeilManager.getClinsdoeilRecus(mbb.getMembre().getMembreId());
		}
		return destination;
	}
	
	public String getPhotoProfileSender(Clinsdoeil clin)
	{
		String chemin=null;
		
		int idUserEnvoye=clin.getFromId();
		userEnvoye=MembreManager.getMembreById(idUserEnvoye);
		chemin=userEnvoye.getProfilImagePath();
				
		return chemin;
	}
	public String getPseudoSender(Clinsdoeil clin)
	{
		String pseudo=null;
		
		int idUserEnvoye=clin.getFromId();
		userEnvoye=MembreManager.getMembreById(idUserEnvoye);
		pseudo=userEnvoye.getPseudo();
				
		return pseudo;
	}
	public String afficherProfileAmis(Clinsdoeil clin)
	{
		String redirection=null;
		
		int idUserEnvoye=clin.getFromId();
		userEnvoye=MembreManager.getMembreById(idUserEnvoye);
		mbb.setProfileAmis(userEnvoye);
		redirection="membre";
				
		return redirection;
	}
}
