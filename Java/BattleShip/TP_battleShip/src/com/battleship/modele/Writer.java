package com.battleship.modele;

import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Date;

public class Writer {
	
	private String nomFichier;
	private FileWriter fichier;
	private PrintWriter pr;
	
	//constructeur
	public Writer()
	{
		nomFichier="Scores.txt";
		try {
			fichier=new FileWriter(nomFichier, true);
			pr = new PrintWriter(fichier,true);
			
			//premiere ligne du fichier
			pr.println("Score de la Partie ");
			fichier.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
	
		
	}
	
	//methode pour ecrire
	public void ecrire(String phrase)
	{
		
		try {
			fichier=new FileWriter(nomFichier, true);
			pr = new PrintWriter(fichier,true);
		
			pr.println(phrase);
		
			fichier.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
}
