#pragma once

#include "MonMartien.h"
#include "MonVaisseau.h"
#define TAILLE_LARGEUR_MARTIEN 9


class Plutonien : public MonMartien {
private :
	//tableau des cordonnees des point touchable par le laser du vaisseau
	int pointXTouchableLaserVaisseau[TAILLE_LARGEUR_MARTIEN];
	int pointYTouchableLaserVaisseau[TAILLE_LARGEUR_MARTIEN];

public:

	
	//constructeur
	Plutonien(int x, int y);

	//afficher /effacer le plutonien
	void afficher() const;
	void effacer() const;


	void bouger();
	

	int getValeurET();

	bool estTouche(Coord laserVaisseau) const;

};

