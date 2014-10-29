package com.battleship.modele;

public class Ecoule extends Exception{

	String message;
	public Ecoule(String message)
	{
		super(message);
		this.message=message;
	}
	
	public String getMessage()
	{
		return message;
	}

}
