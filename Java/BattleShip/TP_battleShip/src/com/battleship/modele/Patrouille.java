package com.battleship.modele;

import java.util.ArrayList;

public class Patrouille extends Bateau{
	
	static final int taille=2;
	private String nom;
	//constructeur
	public Patrouille()
	{
		super(taille);
		nom="Patrouille";
	}
	
	public String getNom()
	{
		return nom;
	}
}
