#include "MonVaisseauLaser.h"

#include <stdlib.h>
#include "UIKit.h"


// constructeur sans parametre
MonVaisseauLaser::MonVaisseauLaser() :timerMouvement(50) {
	//coord.setPositionY(60);
}

// constructeur avec parametre
MonVaisseauLaser::MonVaisseauLaser(int duree) : timerMouvement(duree) {
	//coord.setPositionY(60);
}

// initialiser laser (position de depart et lance debut Timer)
void MonVaisseauLaser::startLaser(int x) {
	UIKit::color(FOREGROUND_RED + FOREGROUND_INTENSITY);
	coord.setPositionX(x);
	coord.setPositionY(62);
	putLaser();
	isAlive = true;
	timerMouvement.demarre();
}

//deplacer le laser si il est alive et si timer est pret
void MonVaisseauLaser::moveLaser() {
	if (isAlive && timerMouvement.estPret()) {
		UIKit::color(FOREGROUND_RED + FOREGROUND_INTENSITY);

		removeLaser();
		if (coord.getPositionY() > 1) {
			coord.setPositionY(coord.getPositionY() - 1);
			putLaser();
		}
		else
			isAlive = false;
	}

}
