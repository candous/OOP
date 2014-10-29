package com.battleship.modele;

import java.util.ArrayList;

public class SousMarin extends Bateau 
{
	static final int taille=3;
	private String nom;
	//constructeur
	public SousMarin()
	{
		super(taille);
		nom="SousMarin";
	}
	
	public String getNom()
	{
		return nom;
	}
	
}
