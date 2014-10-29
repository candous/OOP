#include "MesMartien.h"
#include "Plutonien.h"
#include "Venusien.h"
#include "Glutonien.h"
#include <stdlib.h>

#define TEMP_ENTRE_TIRE_MARTIEN 1000

// constructeur 
MesMartien::MesMartien(Jeu &leJeu, int x, int y)
:leJeu(&leJeu), intervalTirMartien(1000) {




	for (int i = 0; i < MAX_LASERS_MARTIEN; i++)
		laserMartien[i].isAlive = false;

	//	TypeMonMartien typeMartien;
	nbMartienLigne = MAX_MARTIEN_LIGNE;



	int startX = x;
	for (int i = 0; i < nbMartienLigne; i++) {

		// choix au hasard parmis les trois martiens
		int martien = rand() % 3;

		switch (martien) {
			case plutonien:
			ligneMartiens[i] = new Plutonien(startX, y);
			break;
			case venusien:
			ligneMartiens[i] = new Venusien(startX, y);
			break;
			case glutonien:
			ligneMartiens[i] = new Glutonien(startX, y);
			break;
			
		}
		// espacement entre les martiens
		startX += 12;
	}
}

MesMartien::~MesMartien() {
	delete[] ligneMartiens;
}

// obtenir un indice de laser mort
int MesMartien::getDeadLaser() {
	int i = 0;
	while (i < MAX_LASERS_MARTIEN && laserMartien[i].isAlive)
		i++;

	return i < MAX_LASERS_MARTIEN ? i : -1;
}


// getteur
int MesMartien::getnbMartienLigne() const {
	return nbMartienLigne;
}


////setteur
//void MesMartien::resetExtraTerrestre() {
//	nbMartienLigne = MAX_MARTIEN_LIGNE;
//	for (int i = 0; i < MAX_MARTIEN_LIGNE; i++) {
//		ligneMartiens[i]->resetExtraTerrestre();
//	}
//	
//}

//bouger martien et tirer laser
void MesMartien::bougerMartien() {

	// bouger Martien que si il est vivant
	for (int i = 0; i < nbMartienLigne; i++) {
		if (ligneMartiens[i]->isAlive)
			ligneMartiens[i]->bouger();
	}

	// demarer le laser si interval de tir est pret
	
	if (intervalTirMartien.estPret() && nbMartienLigne > 0 ){
		//recuperer un indice dans le tableau qui est pas utilisé
		int indice = getDeadLaser();
		if (indice != -1) {
			
			//choisit un martien au hasard dans la ligne
			int martienQuiTir = rand() % nbMartienLigne;
			//initialiser le laser
			laserMartien[indice].startLaser(ligneMartiens[martienQuiTir]->coord.getPositionX() + 4, ligneMartiens[martienQuiTir]->coord.getPositionY() + 5);
		}
	}

	// faire bouger les lasers des martiens dans le tableau
	for (int i = 0; i < MAX_LASERS_MARTIEN; i++)
		laserMartien[i].moveLaser();
}

// test coolision laser vaisseau et le martien
 bool MesMartien::collisionLaser(Coord laserCoord){
	 bool collision = false;

	 for (int i = 0; i < nbMartienLigne; i++) {
		 if (ligneMartiens[i]->estTouche(laserCoord)) {
			 // effacer alien
			 ligneMartiens[i]->effacer();
			 // dire qu il est plus vivant
			 ligneMartiens[i]->isAlive = false;			
			 // reduire le nombre d ET
			 ExtraTerrestre::reduireNombreExtraTerrestre();

			
			 ligneMartiens[i] = ligneMartiens[nbMartienLigne - 1];


			 nbMartienLigne--;		
			 leJeu->son.collision();

			 //Augmenter le score du jeu
			 leJeu->setScore(ligneMartiens[i]->getValeurET());
			 collision = true;
		 }
	 }

	 return collision;
}

