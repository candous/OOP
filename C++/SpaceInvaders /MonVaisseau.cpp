#include "MonVaisseau.h"
#include "Jeu.h"

#include "UIKit.h"
#include <Windows.h>
#include <stdlib.h>
#include <iostream>
using namespace std;

// deplacer le vaisseau
void MonVaisseau::modifierPosition(char touche) {
	if ((touche == 'k' && coord.getPositionX() > minX) || (touche == 'l' && coord.getPositionX() < maxX)) {
		effacer();
		Vaisseau::modifierPosition(touche);
		afficher();
	}
}

//affichage personnalisé du vaisseau
void MonVaisseau::afficher() {
	UIKit::color(FOREGROUND_GREEN + FOREGROUND_INTENSITY);
	int x = coord.getPositionX(), y = coord.getPositionY();
	Coord::gotoXY(x, y + 0); cout << "    ^    ";
	Coord::gotoXY(x, y + 1); cout << "    #    ";
	Coord::gotoXY(x, y + 2); cout << "#########";
	Coord::gotoXY(x, y + 3); cout << "#########";
}

// effacer
void MonVaisseau::effacer() {
	int x = coord.getPositionX(), y = coord.getPositionY();
	Coord::gotoXY(x, y + 0); cout << "         ";
	Coord::gotoXY(x, y + 1); cout << "         ";
	Coord::gotoXY(x, y + 2); cout << "         ";
	Coord::gotoXY(x, y + 3); cout << "         ";
}

// obtenir un indice dans le tableau ou laser est inutilisé
int MonVaisseau::getDeadLaser() {
	int i = 0;
	while (i < MAX_LASERS && leLaser[i].isAlive)
		i++;

	return i < MAX_LASERS ? i : -1;
}

//	 constructeur
MonVaisseau::MonVaisseau(Jeu &leJeu, int minX, int maxX) :leJeu(&leJeu), minX(minX), maxX(maxX), timerTir(500), timerBouge(20) {
	//initialier les tableau

	coord.setPositionX(rand() % 50 + 10);
	coord.setPositionY(63);
	setTableau(pointXTouchableLaserMartien, 0, 1, 2, 3, 4, 5, 6, 7, 8);
	setTableau(pointYTouchableLaserMartien, 2, 2, 2, 2, 0, 2, 2, 2, 2);
	
	//initialiser les laser a mort
	for (int i = 0; i < MAX_LASERS; i++)
		leLaser[i].isAlive = false;	

	//vaisseau is alive
	isAlive = true;	
}

void MonVaisseau::bouger() {
	//// vitesse du laser par rapport au menu
	//switch (vitesseLaser) {
	//case '1': vitesseTir = 50; break;
	//case '2': vitesseTir = 90; break;
	//case '3': vitesseTir = 110; break;
	//}

	//	on bouge le vaisseau
	if (timerBouge.estPret())
		modifierPosition(leJeu->gestionTouchesMvtVaisseau.estPressee());

	//	gestion de son laser
	
	if (leJeu->gestionTouchesTirVaisseau.estPressee() == ' ' && timerTir.estPret()) {
		int indice = getDeadLaser();
		if (indice != -1) {
		// si on veut deux laser // a rajouter dans version ameliorer 
			/*	leLaser[indice].startLaser(coord.getPositionX());

			indice = getDeadLaser();*/
			// demarer le laser
			leLaser[indice].startLaser(coord.getPositionX() + 4);
			leJeu->son.tir();
		}		
	}

	//	deplacer le laser
	for (int i = 0; i < MAX_LASERS; i++)	
		leLaser[i].moveLaser();
}

//methode pour initialiser le tableau  
void MonVaisseau::setTableau(int monTab[], int a, int b, int c, int d, int e, int f, int g, int h, int i) {
	monTab[0] = a;	monTab[1] = b;	monTab[2] = c;	monTab[3] = d;	monTab[4] = e;
	monTab[5] = f;	monTab[6] = g;	monTab[7] = h;	monTab[8] = i;
}




bool MonVaisseau::estTouche(Coord laserMartien) const {	
	//bool collision = false;
	int x = 0;
	int y = 0;	

	/*for (int i = 0; i < TAILLE_LARGEUR_VAISSEAU; i++) {
		if ((coord.getPositionX() + pointXTouchableLaserMartien[x]) == laserMartien.getPositionX() &&
			(coord.getPositionY() + pointYTouchableLaserMartien[x]) == laserMartien.getPositionY())
			collision = true;
	}
*/
	while ((coord.getPositionX() + pointXTouchableLaserMartien[x]) != laserMartien.getPositionX() &&
		   x < TAILLE_LARGEUR_VAISSEAU )		   
		   x++;

	while ((coord.getPositionY() + pointYTouchableLaserMartien[y]) != laserMartien.getPositionY() &&
		   y < TAILLE_LARGEUR_VAISSEAU )
		   y++;

	return (x < TAILLE_LARGEUR_VAISSEAU && y < TAILLE_LARGEUR_VAISSEAU);

}


// test coolision laser vaisseau et le martien
bool MonVaisseau::collisionLaser(Coord laserCoord) const {
	bool collision = false;

	// collision avec vaisseau 
	if (estTouche(laserCoord)) {
		leJeu->setNbVie();
		leJeu->son.collision();
		collision = true;	
	}		

	return collision;
}