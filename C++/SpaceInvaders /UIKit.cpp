#include "UIKit.h"

//implémentation des méthodes statiques de gestion d'écran

#include <windows.h>
#include <iostream>
using namespace std;

void UIKit::curseurVisible(bool visibilite)
/*
	Tâche: Rend le curseur visible ou invisible
	Entrée: visibilite = le curseur doit être invisible (VRAI ou FAUX)
*/
{
	CONSOLE_CURSOR_INFO curseur;
	GetConsoleCursorInfo (GetStdHandle(STD_OUTPUT_HANDLE), &curseur);
	curseur.bVisible = visibilite;
	SetConsoleCursorInfo (GetStdHandle(STD_OUTPUT_HANDLE), &curseur);
}

int UIKit::wherex()
/*
	Tâche: Retourne la position courante horizontale
	du curseur dans la fenêtre d'affichage
*/
{
	cout.flush();
	PCONSOLE_SCREEN_BUFFER_INFO tampon=new CONSOLE_SCREEN_BUFFER_INFO;
	GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), tampon);
	return tampon->dwCursorPosition.X;
}

int UIKit::wherey()
/*
	Tâche: Retourne la position courante verticale
	du curseur dans la fenêtre d'affichage
*/
{
	cout.flush();
	PCONSOLE_SCREEN_BUFFER_INFO tampon=new CONSOLE_SCREEN_BUFFER_INFO;
	GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), tampon);
	return tampon->dwCursorPosition.Y;
}


void UIKit::dimensionnerCurseur(int pourcentage)
/*
	Change la taille du curseur
	Entrée: pourcentage = le pourcentage de taille du curseur (0% à 100%)
*/
{
	if (pourcentage >= 0 && pourcentage <= 100)
	{
		CONSOLE_CURSOR_INFO curseur;
		GetConsoleCursorInfo (GetStdHandle(STD_OUTPUT_HANDLE), &curseur);
		curseur.dwSize = pourcentage;
		SetConsoleCursorInfo (GetStdHandle(STD_OUTPUT_HANDLE), &curseur);
	}
}

//void UIKit::definirFenetre(int x1, int y1, int x2, int y2)
/*
	Tâche: Dimensionner une fenetre
	Entrées: x1, y1 = coin supérieur gauche de la fenêtre
			 x2, y2 = coin inférieur droit de la fenêtre
*/
/*{
	const SMALL_RECT fenetre = {x1, y1, x2, y2};
	SetConsoleWindowInfo(GetStdHandle(STD_OUTPUT_HANDLE), true, &fenetre);
}*/

/*************************
positioner le curseur
**************************/
void UIKit::gotoXY(int x, int y)
/* 
	Positionne le curseur à l'écran
	Entrées: x = numéro de colonne (0 à 79)
			 y = numéro de ligne (0 à 24)
*/
{   
	cout.flush();
	COORD coord;   
	coord.X = x;
	coord.Y = y;
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), coord);
}




/*************************
configurer les dimension de la fenêtre
**************************/
void UIKit::setDimensionFenetre(int x1, int y1, int x2, int y2)
/*
dimensionner la fenetre
relatif au settings par défaut de la console
*/
{
	COORD coord,max; 
	coord.X =x2+1;
	coord.Y =y2+1;
	//vérifier que la taille voulue est possible
	max=GetLargestConsoleWindowSize(GetStdHandle(STD_OUTPUT_HANDLE));
	if(coord.X>max.X){
		coord.X=max.X;
		x2=max.X-1;
	}
	if(coord.Y>max.Y){
		coord.Y=max.Y;
		y2=max.Y-1;
	}
	SetConsoleScreenBufferSize(GetStdHandle(STD_OUTPUT_HANDLE),coord);

	const SMALL_RECT fenetre = {x1, y1, x2, y2};
	SetConsoleWindowInfo(GetStdHandle(STD_OUTPUT_HANDLE), true, &fenetre);
}

/*
définir la couleur d'affichage
*/
void UIKit::color(unsigned short laCouleur)
{
	cout.flush();
	HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
	SetConsoleTextAttribute (handle, laCouleur);
}



/**************************
focntion cadre
***************************/
/************************Dessiner cadre*/
void UIKit::cadre(int cd,int ld,int cf,int lf, int couleur)
{
int i,l=1;
color(couleur);  //définir la couleur du cadre
gotoXY(cd,ld);
cout<<(char)201;  //coin supérieur gauche
for(i=cd+1;i<cf;i++)
{
	 cout<<(char)205; //ligne horizontale du haut
}
cout<<(char)187;  //coin supérieur droit
for(i=ld+1;i<lf;i++)
{
	 gotoXY(cd,ld+l); 
	 cout<<(char)186;  //ligne verticale gaughe
	 gotoXY(cf,ld+l);
	 cout<<(char)186;  //ligne verticale droite
	 l++;
}
gotoXY(cd,lf);
cout<<(char)200;  //coin inférieur gauche
for(i=cd+1;i<cf;i++)
{
	cout<<(char)205;  //ligne horizontale inférieure
}
cout<<(char)188;  //coin inférieur droit
cout.flush();
color(15);

}


/*********
rouler un exécutable:

  ShellExecute(NULL, "", "blaster.bat", NULL, NULL, SW_HIDE);

COULEUR UIKit::couleurDeFond()
/*
	Retourne la couleur du fond d'écran
*//*
{
	cout.flush();
	PCONSOLE_SCREEN_BUFFER_INFO attribut = new CONSOLE_SCREEN_BUFFER_INFO;
	GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), attribut);
	return static_cast<COULEUR>(attribut->wAttributes >> 4);
}
*/
//COULEUR UIKit::couleurDeTexte()
/*
	Retourne la couleur de texte d'écran
*//*
{
	cout.flush();
	PCONSOLE_SCREEN_BUFFER_INFO attribut = new CONSOLE_SCREEN_BUFFER_INFO;
	GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), attribut);
	return static_cast<COULEUR>(attribut->wAttributes % 16);
}
*/
//void UIKit::cadre(int x1,int y1,int x2,int y2, COULEUR laCouleur)
/*
	Tâche: Trace un cadre à l'écran
	Entrées: x1, y1 = coin supérieur gauche du cadre
			 x2, y2 = coin inférieur droit du cadre
			 couleur = couleur du cadre
*/
/*
{
	int i,l=1;
	changerCouleur(laCouleur);  //définir la couleur du cadre
	gotoXY(x1,y1);
	cout<<(char)201;  //coin supérieur gauche
	for(i=x1+1;i<x2;i++)
	{
		 cout<<(char)205; //ligne horizontale du haut
	}
	cout<<(char)187;  //coin supérieur droit
	for(i=y1+1;i<y2;i++)
	{
		 gotoxy(x1,y1+l); 
		 cout<<(char)186;  //ligne verticale gaughe
		 gotoxy(x2,y1+l);
		 cout<<(char)186;  //ligne verticale droite
		 l++;
	}
	gotoxy(x1,y2);
	cout<<(char)200;  //coin inférieur gauche
	for(i=x1+1;i<x2;i++)
	{
		cout<<(char)205;  //ligne horizontale inférieure
	}
	cout<<(char)188<<endl;  //coin inférieur droit
	cout.flush();
}

*/




/*  *****************/
