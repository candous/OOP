package com.battleship.modele;

import java.util.ArrayList;

import com.battleship.controleur.Controleur;

public class Jeu {
	
	//attributs
	private Joueur joueurUn;
	private Joueur joueurDeux;
	private Controleur leControleur;
	private Writer ecrivain;
	private boolean reset;


	
	public Jeu(String nomJoueur1, TypeJeu type)
	{
		
		if(type==TypeJeu.LOCALORDIS)
		{
			joueurUn=new JoueurOrdi("Ordi 1",this);
			joueurDeux=new JoueurOrdi("Ordi 2", this);
		}
		else if(type==TypeJeu.LOCAL)
		{
			joueurUn=new JoueurHuman(nomJoueur1, this);
			joueurDeux=new JoueurOrdi("Ordinateur",this);
		}
		
		else
		{
			joueurUn=new JoueurHuman(nomJoueur1, this);
			joueurDeux=new JoueurReseau("Joueur2", this);
		}
			
		ecrivain=new Writer();
	}
	
	public void setControleur(Controleur leControleur)
	{
		this.leControleur=leControleur;
	}
	
	public Controleur getControleur()
	{
		return leControleur;
	}
	
	public Joueur getJoueur1()
	{
		return joueurUn;
	}
	public Joueur getJoueur2()
	{
		return joueurDeux;
	}

	
	public void start()
	{
		do{

		do{
			
			int positionTire = joueurUn.attaquer();
			boolean touche = false;
			touche = joueurDeux.isTouchePoints(positionTire);
			try {
				touche = joueurDeux.isTouche(positionTire);
				leControleur.afficherStatus(touche, joueurDeux, positionTire);
				
				
			} catch (Ecoule e) {
				
				leControleur.afficherStatus(joueurDeux, positionTire, e.getMessage());
			}
				
				if(touche)
				{//si colision, enlever une vie du joueur
					joueurDeux.enleverVie();
					joueurUn.ajouterPoint();
				
			
				leControleur.getVue().afficherPoints(joueurUn.getPoints(), joueurDeux.getPoints());
			}
			
			
			
		
			if(joueurDeux.isAlive())
			{
				//pause avant le joueur 2
				try {
					Thread.sleep(1000);
				} catch (InterruptedException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				
				
				positionTire=joueurDeux.attaquer();
				touche = joueurUn.isTouchePoints(positionTire);
				
				try {
				
					touche=joueurUn.isTouche(positionTire);
					leControleur.afficherStatus(touche, joueurUn, positionTire);

				
				} catch (Ecoule e) {
					//si un bateau a ecoule on affiche une autre message
					leControleur.afficherStatus(joueurUn, positionTire, e.getMessage());
				}
				if(touche)
				{
					joueurUn.enleverVie();
					joueurDeux.ajouterPoint();
				}
				leControleur.getVue().afficherPoints(joueurUn.getPoints(), joueurDeux.getPoints());
			}
			
			
			
			}while(joueurUn.isAlive() && joueurDeux.isAlive());
		
		if(joueurUn.isAlive())
			leControleur.afficherFin(joueurUn);
		else 
			leControleur.afficherFin(joueurDeux);
		
		ecrivain.ecrire(joueurUn.getNom()+"  "+joueurUn.getPoints()+"  x  "+joueurDeux.getNom()+"  "+joueurDeux.getPoints()+"\n"+"\n");
		
		
		
		while (!reset) {
			synchronized (this) {}
		}
		
		}while(reset);
		
	}
	
	public synchronized void reset()
	{
		synchronized(this)
		{
		
		joueurUn.resetJoueur();
		joueurDeux.resetJoueur();
		leControleur.getVue().resetGrilles();
		leControleur.getVue().afficherStatus("C'est Parti!!");
		leControleur.afficherMesBateaux();
		leControleur.getVue().afficherPoints(joueurUn.getPoints(), joueurDeux.getPoints());
		reset=true;
		
		}
	}
	
	
}
