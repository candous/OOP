#ifndef __CLASSUTILEGRAPHIQUE_UIKIT_H
#define __CLASSUTILEGRAPHIQUE_UIKIT_H

#include <windows.h>
#include <iostream>
using namespace std;

class UIKit{
public:
	static void curseurVisible(bool visibilite);
	static int wherex();
	static int wherey();
	static void dimensionnerCurseur(int pourcentage);
//	static void definirFenetre(int x1, int y1, int x2, int y2);
	static void gotoXY(int x, int y);
	static void setDimensionFenetre(int x1, int y1, int x2, int y2);
	static int validNumCin();
	static void color(unsigned short laCouleur);
	static void cadre(int cd,int ld,int cf,int lf, int couleur);
};
#endif
