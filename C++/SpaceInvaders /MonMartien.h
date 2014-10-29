#pragma once

#include "Martien.h"
#include "Timer.h"
#include "Coord.h"



class MonMartien : public Martien{
protected:
	
	Timer intervalBouger;
	int x, y;
	
public:
	
	MonMartien(int type, int valeur, int x, int y);
	~MonMartien();
	virtual void afficher() const = 0;
	virtual void effacer() const = 0;

	virtual void bouger() = 0;
	virtual int getValeurET() = 0;
	
	virtual bool estTouche(Coord laserVaisseau) const = 0;
	void setTableau(int[], int, int, int, int, int, int, int, int, int);

	//setteur
	void resetExtraTerrestre();

};

