#pragma once

#include "Laser.h"
#include "Timer.h"

class MonVaisseauLaser : public Laser {
private:
	Timer timerMouvement;

public:
	MonVaisseauLaser();
	MonVaisseauLaser(int duree);
	void startLaser(int x);
	void moveLaser();


};