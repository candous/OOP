#include "Highscores.h"
#include "UIKit.h"
#include "Son.h"
#include <windows.h>
#include <iostream>
#include <fstream>
#include <iostream>
using namespace std;

#define MAX_NOMS 100
#define MAX_SCORES 100
#define INDICES 100

void afficherScores(string nom, int score){
	system("cls");
	Son monSon;
	
	ofstream monFichierEcrire;
	monFichierEcrire.open("scores.txt", fstream::app);
	monFichierEcrire << nom<<endl;
	monFichierEcrire << score<<endl;
	monFichierEcrire.close();

	string noms[MAX_NOMS];
	int indices[INDICES];
	int scores[MAX_SCORES];
	/*
	ifstream monFichierLire;
	monFichierLire.open("score.txt");
	int x=0;
	while (!monFichierLire.eof())
	{
		getline(monFichierLire, noms[x]);
		getline(monFichierLire, scores[x]);
		++x;
		
	}
	monFichierLire.close();
	cout << x << endl;
	for (int i = 0; i < x + 1; i++){
		cout << "allo" + i;
	}*/
	string margeGauche="                ";
	cout << margeGauche << ".___________.  ______   .______           _______.  ______   ______   .______       _______     _______.\n";
	cout << margeGauche << "|           | /  __  \\  |   _  \\         /       | /      | /  __  \\  |   _  \\     |   ____|   /       |\n";
	cout << margeGauche << "`---|  |----`|  |  |  | |  |_)  |       |   (----`|  ,----'|  |  |  | |  |_)  |    |  |__     |   (----`\n";
	cout << margeGauche << "    |  |     |  |  |  | |   ___/         \\   \\    |  |     |  |  |  | |      /     |   __|     \\   \\    \n";
	cout << margeGauche << "    |  |     |  `--'  | |  |         .----)   |   |  `----.|  `--'  | |  |\\  \\----.|  |____.----)   |   \n";
	cout << margeGauche << "    |__|      \\______/  | _|         |_______/     \\______| \\______/  | _| `._____||_______|_______/    \n";
	cout << "\n\n";
	string ligne;
	ifstream monFichierLire("scores.txt");
	int z=0;
	if (monFichierLire.is_open())
	{
		while (getline(monFichierLire, ligne))
		{
			noms[z]=ligne;
			getline(monFichierLire, ligne);
			scores[z]=stoi(ligne);
			indices[z]=z;
			z++;
		}
		monFichierLire.close();
	}

	for(int i=z; i>0; i--)
	{
		//	mettre la bonne valeur à l'indice i
		for(int j=0; j<i; j++)
		{
			if (scores[j]<scores[j+1])
			{
				//	on échange les éléments d'indices j et j+1
				int temp = scores[j];
				int temp2 = indices[j];
				scores[j] = scores[j+1];
				indices[j] = indices[j+1];
				scores[j+1] = temp;
				indices[j+1] = temp2;
			}
		}
	}
	int largeur=50;
	int hauteur=10;
	UIKit::gotoXY(largeur,hauteur);
	UIKit::color(2);
	cout<<"Rank";
	UIKit::gotoXY(largeur+10,hauteur);
	cout<<"Nom";
	UIKit::gotoXY(largeur+30,hauteur);
	cout<<"Score";
	UIKit::color(7);
	for(int i=0;i<z;i++){
		int indice=indices[i];
		UIKit::gotoXY(largeur,hauteur+(i+2));
		cout<<i+1;
		UIKit::gotoXY(largeur+10,hauteur+(i+2));
		cout<<noms[indice];
		UIKit::gotoXY(largeur+30,hauteur+(i+2));
		cout<<scores[i]<<endl;
	}
	monSon.score();

}