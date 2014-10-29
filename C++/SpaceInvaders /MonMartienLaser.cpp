#include "MonMartienLaser.h"

#include <stdlib.h>
#include "UIKit.h"

MonMartienLaser::MonMartienLaser() :timerMouvementLaserMartien(60) {
}

MonMartienLaser::MonMartienLaser(int duree) : timerMouvementLaserMartien(duree) {
}

void MonMartienLaser::startLaser(int x, int y) {
	UIKit::color(15);
	coord.setPositionX(x);
	coord.setPositionY(y);
	putLaser();
	isAlive = true;
	
	timerMouvementLaserMartien.demarre();
}


// modification de la methode de Laser pour faire descendre le laser
void MonMartienLaser::moveLaser() {
	if (isAlive && timerMouvementLaserMartien.estPret()) {
		UIKit::color(15);
		removeLaser();
		if (coord.getPositionY() > 0 && coord.getPositionY() < 66) {
			coord.setPositionY(coord.getPositionY() + 1);
			putLaser();
		} else
			isAlive = false;
		
	}
}
