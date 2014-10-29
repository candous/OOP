package com.mindescape.modele;

public class Joueur {

	// Declaration des Attributs
	private int position;
	private String nom;
	private String prenom;
	private String pwd;
	private double score;
	private int nbPartie;
	private int nbcleCourant;
	private double bestScore;
	private int bestNombreDeplacement;
	private int nombreDeplacement;
	private int nombreDePasCourant;
	private int nombredeRepereCourant;
	private int nombredeAffichageCarteCourant;

	// Constructeur
	public Joueur() {

	}

	// Getter Setter
	public int getPosition() {
		return position;
	}

	public void setPosition(int position) {
		this.position = position;
	}

	public String getNom() {
		return nom;
	}

	public void setNom(String nom) {
		this.nom = nom;
	}

	public String getPrenom() {
		return prenom;
	}

	public void setPrenom(String prenom) {
		this.prenom = prenom;
	}

	public String getPwd() {
		return pwd;
	}

	public void setPwd(String pwd) {
		this.pwd = pwd;
	}

	public double getScore() {
		return score;
	}

	public void setScore(double score) {
		this.score = score;
	}

	public int getNbPartie() {
		return nbPartie;
	}

	public void setNbPartie(int nbPartie) {
		this.nbPartie = nbPartie;
	}

	public int getNbcleCourant() {
		return nbcleCourant;
	}

	public void setNbcleCourant() {
		this.nbcleCourant++;
	}

	public double getBestScore() {
		return bestScore;
	}

	public void setBestScore(double bestScore) {
		this.bestScore = bestScore;
	}

	public int getBestNombreDeplacement() {
		return bestNombreDeplacement;
	}

	public void setBestNombreDeplacement(int bestNombreDeplacement) {
		this.bestNombreDeplacement = bestNombreDeplacement;
	}

	public int getNombreDePasCourant() {
		return nombreDePasCourant;
	}

	public void setNombreDePasCourant(int nombreDePasCourant) {
		this.nombreDePasCourant = nombreDePasCourant;
	}

	public int getNombredeRepereCourant() {
		return nombredeRepereCourant;
	}

	public void setNombredeRepereCourantPlus() {
		this.nombredeRepereCourant++;
	}

	public void setNombredeRepereCourantMoins() {
		this.nombredeRepereCourant--;
	}

	public int getNombreDeplacement() {
		return nombreDeplacement;
	}

	public void setNombreDeplacement() {
		this.nombreDeplacement++;
	}

	public int getNombredeAffichageCarteCourant() {
		return nombredeAffichageCarteCourant;
	}

	public void setNombredeAffichageCarteCourant() {
		this.nombredeAffichageCarteCourant++ ;
	}

}
