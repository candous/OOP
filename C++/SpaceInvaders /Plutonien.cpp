#include "Plutonien.h"
#include "MonMartien.h"
#include "UIKit.h"

#include <iostream>
using namespace std;

Plutonien::Plutonien(int x, int y)
:MonMartien(1, 365, x, y){
	
	setTableau(pointXTouchableLaserVaisseau, 0, 1, 2, 3, 4, 5, 6, 7, 8);
	setTableau(pointYTouchableLaserVaisseau, 3, 4, 4, 4, 4, 4, 4, 4, 3);

	coord.setPositionX(x);
	coord.setPositionY(y);
	afficher();		
	ExtraTerrestre::nombreExtraTerrestre++;


}

void Plutonien::afficher() const{
	UIKit::color(13);
	int x = coord.getPositionX(), y = coord.getPositionY();
	Coord::gotoXY(x, y + 0); cout << "#       #";
	Coord::gotoXY(x, y + 1); cout << " #     # ";
	Coord::gotoXY(x, y + 2); cout << " ####### ";
	Coord::gotoXY(x, y + 3); cout << "## ### ##";
	Coord::gotoXY(x, y + 4); cout << " ####### ";
	
}

void Plutonien::effacer() const {
	UIKit::color(13);
	int x = coord.getPositionX(), y = coord.getPositionY();
	Coord::gotoXY(x, y + 0); cout << "         ";
	Coord::gotoXY(x, y + 1); cout << "         ";
	Coord::gotoXY(x, y + 2); cout << "         ";
	Coord::gotoXY(x, y + 3); cout << "         ";
	Coord::gotoXY(x, y + 4); cout << "         ";

}



void Plutonien::bouger() {
	if (intervalBouger.estPret()) {
		effacer();
		jiggleMartien();
		afficher();
	}
}


int Plutonien::getValeurET() {
	return valeurExtraTerrestre;
}


bool Plutonien::estTouche(Coord laserVaisseau) const {
	int x = 0;
	int y = 0;

	while (x < TAILLE_LARGEUR_MARTIEN &&
		   (coord.getPositionX() + pointXTouchableLaserVaisseau[x]) != laserVaisseau.getPositionX())
		   x++;

	while (y < TAILLE_LARGEUR_MARTIEN &&
		   (coord.getPositionY() + pointYTouchableLaserVaisseau[y]) != laserVaisseau.getPositionY())
		   y++;


	return x < TAILLE_LARGEUR_MARTIEN && y < TAILLE_LARGEUR_MARTIEN;
}