package com.battleship.modele;

import java.util.ArrayList;

public abstract class Bateau {

	//attributs
	private int taille;
	private ArrayList<Integer> positions;
	private Orientation orientation;
	private int nbVies;
	
	//constructeur
	public Bateau(int taille)
	{
		this.taille=taille;
		nbVies=taille;
		positions=new ArrayList<Integer>();
		this.positions=positions;
	}
	
	//getters
	public int getTaille()
	{
		return taille;
	}
	public int getVies()
	{
		return nbVies;
	}

	public ArrayList<Integer> getPosition()
	{
		return positions;
	}

	
	public Orientation getOrientation() {
		return orientation;
	}

	//setters

	public void setPosition(ArrayList<Integer> positions)
	{
		this.positions=new ArrayList<Integer>(positions);
	}
	
	public void setOrientation(Orientation orientation) {
		this.orientation = orientation;
	}

	public void enleverVieBateau()
	{
		nbVies--;
	}
	
	public abstract String getNom();
	
}
