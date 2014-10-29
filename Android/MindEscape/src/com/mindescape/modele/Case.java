package com.mindescape.modele;

import java.util.ArrayList;

public class Case {
	// attribut
	private int position;
	private int typeImage;
	private boolean isRepere;
	private ArrayList<Integer> positionRepere;
	private boolean isPas;
	private ArrayList<Integer> positionPas;
	private boolean isClef;
	private boolean isSortie;
	private ArrayList<Integer> casesPossibles;
	
	
	// constructeur
	public Case() {	
		
	}


	
	//getteur setteur

	public int getPosition() {
		return position;
	}
	public void setPosition(int position) {
		this.position = position;
	}
	public int getTypeImage() {
		return typeImage;
	}
	public void setTypeImage(int typeImage) {
		this.typeImage = typeImage;
	}
	public boolean isRepere() {
		return isRepere;
	}
	public void setRepere(boolean isRepere) {
		this.isRepere = isRepere;
	}
	
	public boolean isPas() {
		return isPas;
	}
	public void setPas(boolean isPas) {
		this.isPas = isPas;
	}	
	
	public ArrayList<Integer> getPositionRepere() {
		
		
		return positionRepere;
	}



	public void setPositionRepere(ArrayList<Integer> positionRepere) {
		this.positionRepere = positionRepere;
	}



	public ArrayList<Integer> getPositionPas() {
		return positionPas;
	}



	public void setPositionPas(ArrayList<Integer> positionPas) {
		this.positionPas = positionPas;
	}



	public ArrayList<Integer> getCasesPossibles() {
		return casesPossibles;
	}



	public void setCasesPossibles(ArrayList<Integer> casesPossibles) {
		this.casesPossibles = casesPossibles;
	}



	public boolean isClef() {
		return isClef;
	}
	public void setClef(boolean isClef) {
		this.isClef = isClef;
	}
	public boolean isSortie() {
		return isSortie;
	}
	public void setSortie(boolean isSortie) {
		this.isSortie = isSortie;
	}
	
}
