package com.mindescape.modele;

public class Repere {

	//attribut
	private int position;
	private int posX;
	private int posY;

	// constructeur
	public Repere(int position, int posX, int posY) {
		this.position = position;
		this.posX = posX;
		this.posY = posY;
	}

	// getteur setteur
	public int getPosition() {
		return position;
	}

	public void setPosition(int position) {
		this.position = position;
	}

	public int getPosX() {
		return posX;
	}

	public void setPosX(int posX) {
		this.posX = posX;
	}

	public int getPosY() {
		return posY;
	}

	public void setPosY(int posY) {
		this.posY = posY;
	}

	
	
}
