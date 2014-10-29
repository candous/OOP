package com.battleship.vue;

import javax.swing.JButton;

public class BoutonCadre extends JButton {

	//attributs
	private int position;
	
	//constructeur
	public BoutonCadre(int position)
	{
		this.position=position;
	}
	
	//getter position appuyee
	public int getPosition()
	{
		return position;
	}
	
}
