#pragma once

#include "Laser.h"
#include "Timer.h"

class MonMartienLaser : public Laser {

private:

	Timer timerMouvementLaserMartien;

public:
	MonMartienLaser();
	MonMartienLaser(int duree);
	void startLaser(int x, int y);
	void moveLaser();

	
};

