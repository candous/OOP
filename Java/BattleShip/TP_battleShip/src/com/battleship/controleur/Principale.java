package com.battleship.controleur;

import java.util.ArrayList;

import com.battleship.modele.Jeu;
import com.battleship.modele.Joueur;
import com.battleship.modele.TypeJeu;
import com.battleship.vue.Menu;

public class Principale {
	
	
	public static void main(String[] args) {
		
		Menu leMenu=new Menu();
		
		boolean pret = false;
		while (!pret) {
			synchronized (leMenu) {
				pret = leMenu.jeuChoisi();
			}
		}
		
		Jeu monJeu=new Jeu(leMenu.getNom(),leMenu.getTypeJeu());
		//Jeu monJeu=new Jeu("raphael",TypeJeu.LOCAL);
		new Controleur(monJeu);
		
		monJeu.start();
		
		
	}
	
		
}
