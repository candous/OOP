#pragma once

//	la classe des timers
class Timer {

private:
	int duree;				//	en millisecondes
	int dateDebutCycle;

public:
	
	Timer(int duree);
	void setDuree(int duree);
	void demarre();
	bool estPret();
	int getTimeLeft() const;
};
