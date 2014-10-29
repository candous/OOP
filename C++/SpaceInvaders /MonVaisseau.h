#pragma once

#include "Vaisseau.h"
#include "MonVaisseauLaser.h"
#include "Jeu.h"
#include "Timer.h"

#define MAX_LASERS 10
#define TAILLE_LARGEUR_VAISSEAU 9
#define TIMER_VAISSEAU 20
#define TIMER_TIR 500

class Jeu;

class MonVaisseau : public Vaisseau {
private:

	//donnees membres
		// limite deplacement vaisseau
	int minX, maxX;

		// timer du vaisseau et du laser
	Timer timerBouge, timerTir;

		// lien entre le vaisseau et le jeu
	Jeu *leJeu;

		// permettra de savoir si la partie est fini ou pas
	bool isAlive;	

		// deplacer le vaisseau
	void modifierPosition(char key);	

		// afficher / effacer propre a l enfant MonVaisseau

	void effacer();

		// pour le laser, obtenir un indice ou le laser est pas inutile
	int getDeadLaser();
	
		//tableau des cordonnees des point touchable par le laser des martiens
	int pointXTouchableLaserMartien[TAILLE_LARGEUR_VAISSEAU];
	int pointYTouchableLaserMartien[TAILLE_LARGEUR_VAISSEAU];

	//methode pour initialiser le tableau  => question a poser au prof pour faire ca proprement
	void setTableau(int[], int, int, int, int, int, int, int, int, int);

	//vitesse tir
	int vitesseTir;

	//tab de timer pour les lasers
	//Timer laserTimer[MAX_LASERS];

public:
	// Laser vaisseau 
	MonVaisseauLaser leLaser[MAX_LASERS];

	//constructeur 
	MonVaisseau(Jeu &leJeu, int minX, int maxX);

	// bouger et action laser
	void bouger();
	
	// getteur 
	bool getIsAlive()const;
	void afficher();

	//test collision avec les lasers des martiens
	bool estTouche(Coord laserMartien) const;
	bool collisionLaser(Coord laserCoord) const;
};