package com.battleship.modele;

import java.util.ArrayList;

public class JoueurHuman extends Joueur {

	public JoueurHuman(String nom, Jeu leJeu) {
		super(nom, leJeu);

	}

	public int attaquer() {
		boolean pret = false;
		while (!pret) {
			synchronized (leJeu.getControleur()) {
				pret = leJeu.getControleur().getCoupPret();
			}
		}

		synchronized (leJeu.getControleur()) {
			leJeu.getControleur().setCoupPret(false);
		}

		// avant
		// while(!leJeu.getControleur().getCoupPret());
		// leJeu.getControleur().setCoupPret(false);

		return leJeu.getControleur().getCoup();
	}

	
}
