package com.battleship.modele;

import java.util.ArrayList;

public class Destroyer extends Bateau {

	static final int taille=3;
	private String nom;
	//constructeur
	public Destroyer()
	{
		super(taille);
		nom="Destroyer";
	}
	
	
	public String getNom()
	{
		return nom;
	}
}


