#pragma once
#include "MonMartien.h"
#include "Jeu.h"
#include "MonMartienLaser.h"
#include "Timer.h"


#define MAX_MARTIEN_LIGNE 8
#define MAX_LASERS_MARTIEN 10
enum TypeMonMartien { plutonien, venusien, glutonien };

class Jeu;


class MesMartien {
private:
	
	//nb Martien dans la ligne (pour quand il y aura plusieurs lignes a gerer)
	int nbMartienLigne;
	// tableau de pointeur de MonMartien
	
	
	MonMartien *ligneMartiens[MAX_MARTIEN_LIGNE];
	TypeMonMartien mesMartien;
	
	
	// interval entre chaque tir des martien
	Timer intervalTirMartien;
	// liaison entre le jeu est les martiens(permettra d acceder au vaisseau a traver le jeu)
	Jeu *leJeu;
	// obtenir un indice de laser inutilisé
	int getDeadLaser();

public:
	// constructeur 
	MesMartien(Jeu &leJeu, int x, int y);
	// destructeur car utilise new dans le constructeur
	~MesMartien();
	// action des martiens (bouger martien + tirer laser)
	void bougerMartien();
	//getteur
	int getnbMartienLigne() const;
	//
	////setteur
	//void resetExtraTerrestre();

	//colision entre martien est laser 
	bool collisionLaser(Coord laserCoord);

	// tableau de laser des martiens
	MonMartienLaser laserMartien[MAX_LASERS_MARTIEN];
	
};

