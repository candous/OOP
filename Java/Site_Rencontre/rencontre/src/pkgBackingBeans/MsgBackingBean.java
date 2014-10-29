package pkgBackingBeans;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Date;

import pkgEntities.Membre;
import pkgEntities.Message;
import pkgManagers.MembreManager;
import pkgManagers.MessageManager;
import services.SendEmail;

public class MsgBackingBean implements Serializable {
	
	private MembreBackingBean mbb; //user connecte
	private String reponse;
	private String reponseDef;
	private Membre userEnvoye;
	private ArrayList<Message> listeMessagesRecus;
	
	public MembreBackingBean getMbb() {
		return mbb;
	}
	public void setMbb(MembreBackingBean mbb) {
		this.mbb = mbb;
	}
	public String getReponse() {
		if(reponse!=null && reponse!="")
			reponseDef=reponse;
		
		return reponse;
	}
	public void setReponse(String reponse) {
		this.reponse = reponse;
	}
	public String getReponseDef() {
		return reponseDef;
	}
	public void setReponseDef(String reponseDef) {
		this.reponseDef = reponseDef;
	}
	public Membre getUserEnvoye() {
		return userEnvoye;
	}
	public void setUserEnvoye(Membre userEnvoye) {
		this.userEnvoye = userEnvoye;
	}
	public ArrayList<Message> getListeMessagesRecus() {
		listeMessagesRecus=new ArrayList<Message>();
		mbb.getMembre().setListeMessagesRecus(MessageManager.getListeMessagesRecusByMemberId(mbb.getMembre().getMembreId()));
		listeMessagesRecus=mbb.getMembre().getListeMessagesRecus();
		return listeMessagesRecus;
	}
	public void setListeMessagesRecus(ArrayList<Message> listeMessagesRecus) {
		this.listeMessagesRecus = listeMessagesRecus;
	}
	public String getPhotoProfileSender(Message message)
	{
		String chemin=null;
		
		int idUserEnvoye=message.getMsgFrom();
		userEnvoye=MembreManager.getMembreById(idUserEnvoye);
		chemin=userEnvoye.getProfilImagePath();
		return chemin;
	}
	public String getPseudoSender(Message message)
	{
		String pseudo=null;
		
		int idUserEnvoye=message.getMsgFrom();
		userEnvoye=MembreManager.getMembreById(idUserEnvoye);
		pseudo=userEnvoye.getPseudo();
				
		return pseudo;
	}
	//methodes
		public String repondreMessage(Message messageARepondre)
		{
			String destination=null;
			
			Message msgreponse=new Message();
			msgreponse.setContenu(reponseDef);
			msgreponse.setDejalu(false);
			msgreponse.setMsgFrom(mbb.getMembre().getMembreId());
			msgreponse.setMsgTo(messageARepondre.getMsgFrom());
			msgreponse.setReplyToMsgId(msgreponse.getMessageid());
			msgreponse.setSendDate(new Date());
			
			int ajoute=0;
			ajoute=MessageManager.addMessage(msgreponse);
			//savoir si qui a envoye le message veut recevoir une reponse en courriel
			int idUser=messageARepondre.getMsgFrom();
			Membre membreARepondre=MembreManager.getMembreById(idUser);
			boolean recevoirCourriel=false;
			recevoirCourriel=membreARepondre.getInformed_message_received();
			
			if(ajoute>0){
				destination="oui";
				if(recevoirCourriel)
					SendEmail.envoyerMail(mbb.getMembre(), membreARepondre, "message");
				
			}
			return destination;
		}
		
		public String supprimerMessage(Message messageASupprimer)
		{
			String destination=null;
			
			int deleted=MessageManager.deleteMessage(messageASupprimer);
			
			if(deleted>0){
				destination="oui";
				//metre a jour liste de messages
				mbb.getMembre().setListeMessagesRecus(MessageManager.getListeMessagesRecusByMemberId(mbb.getMembre().getMembreId()));
			}
			return destination;
		}
		public String afficherProfileAmis(Message message)
		{
			String redirection=null;
			int idUser=message.getMsgFrom();
			mbb.setProfileAmis(MembreManager.getMembreById(idUser));
			redirection="membre";
			
			return redirection;
		}
		
		
	}
