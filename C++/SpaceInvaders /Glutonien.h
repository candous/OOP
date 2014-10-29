#pragma once

#include "MonMartien.h"
#include "MonVaisseau.h"
#define TAILLE_LARGEUR_MARTIEN 9

class Glutonien : public MonMartien {
private:
	//tableau des cordonnees des point touchable par le laser du vaisseau
	int pointXTouchableLaserVaisseau[TAILLE_LARGEUR_MARTIEN];
	int pointYTouchableLaserVaisseau[TAILLE_LARGEUR_MARTIEN];


public:
	Glutonien(int x, int y);
	//afficher /effacer le Glutonien
	void afficher() const;
	void effacer() const;


	void bouger();


	int getValeurET();

	bool estTouche(Coord laserVaisseau) const;
};

