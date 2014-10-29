package com.battleship.modele;

import java.util.ArrayList;

public class JoueurOrdi extends Joueur {
	
	//cases ou l'ordi peut tirer
	private ArrayList<Integer> casesOrdi;

	public JoueurOrdi(String nom, Jeu leJeu) {

		super(nom, leJeu);
		
		//cases possibles pour l'ordi attaquer
		casesOrdi=new ArrayList<Integer>();
		for (int i=0;i<100;i++)
		{
			casesOrdi.add(i+11);
		}

	}

	@Override
	//positionement choisi aleatoirement par l'ordi
	public int attaquer()
	{
			boolean trouve=false;
			int retour;
			//tire aleatoire entre les cases 11 et 110
			do{
				retour=(int)(Math.random()*100)+11;
				int j=0;
				while(j<casesOrdi.size() && casesOrdi.get(j)!=retour)
					j++;
				
				if(j<casesOrdi.size())
				{
					//retirer le numero des cases de l' arraylist
					casesOrdi.remove(j);
					trouve=true;
				}
				
			}while(!trouve);
			
			try {
				Thread.sleep(500);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			
			return retour;
	}


	
}
