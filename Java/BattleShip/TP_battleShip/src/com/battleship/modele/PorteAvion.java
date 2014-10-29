package com.battleship.modele;

import java.util.ArrayList;

public class PorteAvion extends Bateau{

	static final int taille=4;
	private String nom;
	//constructeur
	public PorteAvion() {
		super(taille);
		nom="PorteAvion";
		
	}
	
	public String getNom()
	{
		return nom;
	}
}
