#include "Timer.h"
#include <time.h>


Timer::Timer(int duree) {
	this->duree = duree;
	demarre();
}

void Timer::setDuree(int duree) {
	this->duree = duree;
	demarre();
}

void Timer::demarre() {
	dateDebutCycle = clock();
}

bool Timer::estPret() {
	bool retour = false;
	if (clock() - dateDebutCycle >= duree) {
		//	nouveau cycle
		dateDebutCycle = clock();
		retour = true;
	}
	return retour;
}

int Timer::getTimeLeft() const {
	return duree - (clock() - dateDebutCycle);
}