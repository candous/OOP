#include "MonMartien.h"
#include "Martien.h"


#include <stdlib.h>

//constructeur sans parametre


MonMartien::MonMartien(int type, int valeur, int x, int y) 
:Martien(type, valeur),x(x),y(y),intervalBouger(rand() % 1000 + 300){
	this->isAlive = true;
}


MonMartien::~MonMartien() {
}


void MonMartien::setTableau(int monTab[], int a, int b, int c, int d, int e, int f, int g, int h, int i) {
	monTab[0] = a;	monTab[1] = b;	monTab[2] = c;	monTab[3] = d;	monTab[4] = e;
	monTab[5] = f;	monTab[6] = g;	monTab[7] = h;	monTab[8] = i;
}


//setteur
void MonMartien::resetExtraTerrestre() {
	isAlive = true;
	nombreExtraTerrestre++;
}