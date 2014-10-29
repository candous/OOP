package com.battleship.modele;

import java.util.ArrayList;
import java.util.Random;

public abstract class Joueur {

	private static final int lignesCadre=10;
	// attributs
	private String nom;
	private ArrayList<Bateau> listeBateaux;
	private int points;
	private int nbVies;
	private boolean isAlive;
	
	protected Jeu leJeu;


	// constructeur
	public Joueur(String nom, Jeu leJeu) {
		
		this.nom = nom;
		this.leJeu=leJeu;
		points=0;
		nbVies=12;
		isAlive=true;
		//initialisation des tableaux
		listeBateaux = new ArrayList<Bateau>();
		

		ajouterBateaux();
		
		positionementBateaux();

	}
	
	private void positionementBateaux()
	{
		ArrayList<Integer> casesOccupees = new ArrayList<Integer>();
		ArrayList<Integer> casesTemporaires = new ArrayList<Integer>();
		// random pour positionement horizontal ou vertical du bateau
		int positionement;
		int posY;
		int posX;
		int positionInitiale;

		// positionement de chaque bateau dans la liste
		for (Bateau bateau : listeBateaux) {
			boolean sauvegarde = false;
			do {
				// random pour positionement horizontal ou vertical du bateaux
				positionement = (int) (Math.random() * 2);
				casesTemporaires.clear();

				// 0=horizontal
				if (positionement == 0) {
					bateau.setOrientation(Orientation.HORIZONTAL);
					// choisir la ligne au hasard
					posY = (int) (Math.random() * lignesCadre) + 1;

					// choisir la colonnes au hasard
					posX = (int) (Math.random() * (lignesCadre-(bateau.getTaille()-1))) + 1;

					// calculer la position initiale par rapport aux calculs
					positionInitiale = (posY * lignesCadre) + posX;

					// sauvegarder les positions dans un array temporaire
					for (int i = 0; i < bateau.getTaille(); i++)
						casesTemporaires.add(positionInitiale + i);
				}

				// position verticale
				else {
					bateau.setOrientation(Orientation.VERTICAL);
					
					posY = (int) (Math.random() * (lignesCadre-(bateau.getTaille()-1))) + 1;
					posX = (int) (Math.random() * lignesCadre) + 1;
					positionInitiale = (posY * lignesCadre) + posX;

					// sauvegarder les positions dans un array temporaire
					for (int i = 0; i < bateau.getTaille() * lignesCadre; i += lignesCadre)
						casesTemporaires.add(positionInitiale + i);
				}

				// tester si les positions sont deja occupees
				boolean repete = false;
				for (int pos : casesTemporaires) {
					int k = 0;

					while (k < casesOccupees.size() && pos != casesOccupees.get(k))
						k++;
					if (k < casesOccupees.size())
						repete = true;
				}

				// s'il n'y a pas de doublon dans les 2 listes
				if (repete == false) {
					for (int pos : casesTemporaires)
						casesOccupees.add(pos);

					bateau.setPosition(casesTemporaires);
					sauvegarde = true;
				}

				// System.out.println(sauvegarde);

			} while (sauvegarde == false);
		}

		System.out.println(casesOccupees);
		
	}
	
	private void ajouterBateaux()
	{

		// ajoute des bateaux pour les joueurs
		listeBateaux.add(new PorteAvion());
		listeBateaux.add(new Destroyer());
		listeBateaux.add(new SousMarin());
		listeBateaux.add(new Patrouille());
	}
	
	
	
	
	//methodes
	public void setJeu(Jeu unJeu)
	{
		this.leJeu=unJeu;
	}
	
	public String getNom()
	{
		return nom;
	}
	public int getPoints()
	{
		return points;
	}
	
	public ArrayList<Bateau> getListeBateaux()
	{
		return listeBateaux;
	}
	

	public void ajouterPoint()
	{
		points++;
	}
	
	public void enleverVie()
	{
		nbVies--;
	}
	
	public boolean isAlive()
	{
		if (nbVies==0)
			isAlive=false;
		
		return isAlive;
	}
	
	
	public void resetJoueur()
	{
		points=0;
		nbVies=12;
		isAlive=true;
		listeBateaux = new ArrayList<Bateau>();
		ajouterBateaux();
		positionementBateaux();
		
	}
	

	
	public boolean isTouche(int position) throws Ecoule
	{
		boolean frappe=false;
		
		for(Bateau bateau:listeBateaux)
		{
			int i=0;
			while(i<bateau.getPosition().size() && position!=bateau.getPosition().get(i))
			i++;
			
			if(i<bateau.getPosition().size())
			{
				bateau.enleverVieBateau();
				frappe=true;
				//message de bateau ecoule mais il arrive pas au retour!!
				if(bateau.getVies()==0)
					throw new Ecoule(bateau.getNom()+" ecoule");
			}
		}
		return frappe;
	}
	
	public boolean isTouchePoints(int position)
	{
		boolean frappe=false;
		
		for(Bateau bateau:listeBateaux)
		{
			int i=0;
			while(i<bateau.getPosition().size() && position!=bateau.getPosition().get(i))
			i++;
			
			if(i<bateau.getPosition().size())
				frappe=true;
				
			
		}
		return frappe;
	}
	
	
	
	public abstract int attaquer();
}
