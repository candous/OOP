package com.battleship.controleur;

import com.battleship.modele.Bateau;
import com.battleship.modele.Jeu;
import com.battleship.modele.Joueur;
import com.battleship.vue.BattleShip;
import com.battleship.vue.MonSon;

public class Controleur {

	// attributs
	private Jeu unJeu;
	private BattleShip vue;
	private MonSon son;

	private int coup;
	private boolean coupPret;

	// constructeur
	public Controleur(Jeu unJeu) {
		this.unJeu = unJeu;
		vue = new BattleShip(this);
		son=new MonSon();
		
		son.play("bg.wav");
		

		
		unJeu.setControleur(this);
		afficherMesBateaux();
		afficherNomJoueurs();

		coupPret = false;

	}

	public BattleShip getVue() {
		return vue;
	}

	public Jeu getJeu()
	{
		return unJeu;
	}
	public MonSon getSon()
	{
		return son;
	}
	
	public void afficherMesBateaux() {
		for (Bateau bateau : unJeu.getJoueur1().getListeBateaux())
			vue.afficherMesBateaux(bateau.getPosition());
	}

	public void afficherNomJoueurs() {
		vue.afficherNoms(unJeu.getJoueur1().getNom(), unJeu.getJoueur2()
				.getNom());
	}

	public void afficherStatus(boolean frappe, Joueur joueurCible, int tire) {

		//si frappe
		if (frappe) {
			vue.afficherStatus(joueurCible.getNom()
					+ " dit: BOOM!! Tu as frappe mon bateau");

			son.play("boom.wav");
			
			// affichage des couleurs dans le cadre
			if (joueurCible == unJeu.getJoueur2())
				vue.bateauFrappe(tire);
			else if(joueurCible == unJeu.getJoueur1())
				vue.bateauPerdu(tire);
		}

		else
		{
			vue.afficherStatus(joueurCible.getNom() + " dit: Splash!! dans l'eau...");
			
			son.play("eau.wav");
			
			// affichage des couleurs dans le cadre
				if (joueurCible == unJeu.getJoueur2())
					vue.eau2(tire);
				else if(joueurCible == unJeu.getJoueur1())
					vue.eau(tire);
		}
			
	}
	//methode surchargee
	public void afficherStatus(Joueur joueurCible, int tire, String message) {

		//si il y a une exception c'est pcq un bateau a ecoule
			vue.afficherStatus(joueurCible.getNom()
					+ " dit: BOOM!! " + message);

			son.play("boom.wav");
			
			// affichage des couleurs dans le cadre
			if (joueurCible == unJeu.getJoueur2())
				vue.bateauFrappe(tire);
			else if(joueurCible == unJeu.getJoueur1())
				vue.bateauPerdu(tire);
			
	}

	
	public void afficherFin(Joueur gagnant) 
	{
		
			vue.afficherStatus(gagnant.getNom()
					+ " a gagne! Felicitations!!");
		}

	public synchronized void setCoup(int coup) {
		this.coup = coup;
		coupPret = true;
	}

	public int getCoup() {
		return coup;
	}

	public synchronized void setCoupPret(boolean coup) {
		coupPret = coup;
	}

	public synchronized boolean getCoupPret() {
		return coupPret;
	}

}
