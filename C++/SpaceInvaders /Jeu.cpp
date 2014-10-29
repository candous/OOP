#include "Jeu.h"
#include "MonVaisseau.h"
#include "MesMartien.h"

#include <stdlib.h>
#include <conio.h>
#include <Windows.h>
#include "UIKit.h"


//constructeur
Jeu::Jeu() {
	
	gestionTouchesMvtVaisseau.ajouterTouche('l');
	gestionTouchesMvtVaisseau.ajouterTouche('k');
	gestionTouchesTirVaisseau.ajouterTouche(' ');

	leVaisseau = new MonVaisseau(*this, X_MIN_VAISSEAU, X_MAX_VAISSEAU);	


	switch (niveau) {

		case '1':
		nbLigne = 1;
		break;

		case '2':
		nbLigne = 2;

		break;

		case '3':
		nbLigne = 3;
		break;
		default:
		nbLigne = 3;
		break;
		
	}

	//nbLigne = 3;



	int starY = 5;
	//ligneMartiens[0] = new MesMartien(*this, 5, 5);

	for (int i = 0; i < nbLigne; i++) {
		ligneMartiens[i] = new MesMartien(*this, 5, starY);
		starY += 8;
	}
	
	//ligneMartiens = new MesMartien(*this, 5, 5);
	score = 0;

	nbVie = MAX_VIE;

	}
	

//methode qui affiche le menu
void Jeu::afficherMenu() {
	system("cls");


	UIKit::gotoXY(60, 30);
	UIKit::color(FOREGROUND_GREEN + FOREGROUND_INTENSITY);
	cout << "SPACE INVADERS II" << endl;

	UIKit::gotoXY(65, 37);
	cout << "Niveaux" << endl;
	UIKit::gotoXY(47, 39);
	cout << "(1) Facile (2) Moyen (3) Difficile (0) Quitter" << endl;
	UIKit::gotoXY(47, 44);
	cout << "NIVEAU:" << endl;
	UIKit::curseurVisible(true);
	UIKit::dimensionnerCurseur(100);
	UIKit::gotoXY(54, 44);

	//validation du niveau
	niveau = valid_niveau();

	//changer la vitesse du laser par rapport au niveau de difficulte
	//leVaisseau.vitesseLaser = niveau;


	if (niveau != '0') {
		UIKit::gotoXY(47, 46);
		UIKit::dimensionnerCurseur(100);
		cout << "NOM:" << endl;
		UIKit::gotoXY(52, 46);
		cin >> nom;
	}
}


//methode qui valide le niveau
char Jeu::valid_niveau() {
	int caractere;
	//return que 1,2,3 et 0
	do {
		caractere = toupper(_getch());
	} while ((caractere != '1') && (caractere != '2') && (caractere != '3') && (caractere != '0'));

	return caractere;
}

//getteur
MonVaisseau Jeu::getLeVaisseau() const {
	return *leVaisseau;
}

int Jeu::getScore() {
	return score;
}

int Jeu::getNbVie()const {
	return nbVie;
}
string Jeu::getNom() const {
	return nom;
}

//setteur
void Jeu::setScore(int valeurET) {
	score += valeurET;
}

void Jeu::setNbVie() {
	nbVie--;
}

void Jeu::setVieMax() {
	nbVie = MAX_VIE;
}


// fonction principal du jeu
void Jeu::faireUnTourDeJeu() {
	

	// test collision laser martien et vaisseau si vrai tue le laser pour plus l afficher
	for (int j = 0; j < MAX_LASERS_MARTIEN; j++) {
		for (int ligne = 0; ligne < nbLigne; ligne++) {
			if (ligneMartiens[ligne]->laserMartien[j].isAlive && leVaisseau->collisionLaser(ligneMartiens[ligne]->laserMartien[j].coord)) {
				ligneMartiens[ligne]->laserMartien[j].killLaser();

				if (ligneMartiens[ligne]->getnbMartienLigne() == 0) {
					ligneMartiens[ligne] = ligneMartiens[nbLigne - 1];
					nbLigne--;
				}
			}
		}
	}

	// test collision laser vaisseau et martien si vrai tue le laser pour plus l afficher
	for (int i = 0; i < MAX_LASERS; i++) {
		for (int ligne = 0; ligne < nbLigne; ligne++) {
			if (leVaisseau->leLaser[i].isAlive && ligneMartiens[ligne]->collisionLaser(leVaisseau->leLaser[i].coord)) {
				leVaisseau->leLaser[i].killLaser();
			}
		}
		
	}	

	// fait bouger le vaisseau et tir laser
	leVaisseau->bouger();

	// mouvement et laser des martiens
	for (int ligne = 0; ligne < nbLigne; ligne++) {
		ligneMartiens[ligne]->bougerMartien();
	}	

	// metre le score est le nbVie a jour si ca a changé
	actualisationScoreNbvie();

}



bool Jeu::estTermine() {
	// jeu fini si plus d extra terrestre ou si plus de vie
	return (ExtraTerrestre::getNombreExtraTerrestre() < 1 || nbVie < 1);	
}

// retour pour savoir si on a gagne
bool Jeu::aGagne() {
	bool retour = false;

	if (estTermine() && nbVie > 0)
		retour = true;

	return retour;
}

//methode qui cree la fenetre
void Jeu::creerFenetre() {
	UIKit::setDimensionFenetre(0, 0, X_ECRAN, Y_ECRAN);
	afficherMenu();
}

//methode qui affiche le cadre
void  Jeu::afficherCadre() {

	//affichage des regles
	system("cls");
	UIKit::curseurVisible(false);
	UIKit::gotoXY(65, 23);
	cout << "REGLES" << endl;
	UIKit::gotoXY(48, 27);
	cout << "1-Tuer tous les Extra Terrestres" << endl;
	UIKit::gotoXY(48, 28);
	cout << "2-Ne pas etre tue par un Extra Terrestre" << endl;
	UIKit::gotoXY(48, 29);
	cout << "3-Ne pas laisser les Extra Terrestre conquerir la planete" << endl;
	UIKit::gotoXY(48, 30);
	cout << "4-Vous avez 3 vies" << endl;

	UIKit::gotoXY(65, 37);
	cout << "TOUCHES" << endl;
	UIKit::gotoXY(52, 41);
	cout << "(K) " << endl;
	UIKit::gotoXY(78, 41);
	cout << "GAUCHE " << endl;

	UIKit::gotoXY(52, 42);
	cout << "(L) " << endl;
	UIKit::gotoXY(78, 42);
	cout << "DROITE " << endl;

	UIKit::gotoXY(52, 43);
	cout << "(BARRE) " << endl;
	UIKit::gotoXY(78, 43);
	cout << "TIRER " << endl;


	//son debut de partie
	son.debutPartie();

	//temps d'affichage des regles
	system("cls");

	//affichage du cadre et dimention de l'ecran
	UIKit::cadre(0, 0, X_CADRE, Y_CADRE, FOREGROUND_GREEN + FOREGROUND_INTENSITY);
	//curseur Invisible
	UIKit::curseurVisible(false);

	//affichage des Points
	UIKit::gotoXY(110, 4);
	cout << "***********************";
	UIKit::gotoXY(110, 5);
	cout << "   SPACE INVADERS II     ";
	UIKit::gotoXY(110, 6);
	cout << "***********************";

	//affichage du nom du joueur

	UIKit::gotoXY(110, 13);
	cout << "------- JOUEUR -------";
	UIKit::gotoXY(112, 16);
	cout << getNom();
	//affichage du niveau
	UIKit::gotoXY(110, 20);
	cout << "------ NIVEAU: " << niveau << " -----";



	UIKit::gotoXY(110, 24);
	cout << "SCORE: " << getScore(); 
	//UIKit::gotoXY(110, 28);
	//cout << "ENNEMIS TUES: "/*<< nbExtras*/;
	UIKit::gotoXY(110, 32);
	cout << "VIES: " << nbVie;
	UIKit::gotoXY(110, 36);
	cout << "----------------------";



	//afficher le vaisseau
	leVaisseau->afficher();

}


void Jeu::actualisationScoreNbvie() {
	

	if (getScore() != scoreTemps) {
		UIKit::gotoXY(110, 24);
		UIKit::color(11);
		cout << "SCORE: " << getScore();
	}

	if (getNbVie() != nbVieTemps) {
		UIKit::gotoXY(110, 32);
		UIKit::color(11);
		cout << "VIES: " << nbVie;
		UIKit::gotoXY(110, 36);
		UIKit::color(15);
		cout << "----------------------";
	}

	//UIKit::gotoXY(110, 28);
	//cout << "ENNEMIS TUES: "/*<< nbExtras*/;
	

}

//affichage a la fin
void Jeu::affichageFin() {
	system("cls");

	if (aGagne() && niveau != '0') {
		UIKit::color(FOREGROUND_GREEN + FOREGROUND_INTENSITY);
		//curseur Invisible
		UIKit::curseurVisible(false);
		//affichage de gagnant
		UIKit::gotoXY(53, 36);
		cout << "LES MARTIENS SONT MORTS!" << endl;

		//music partie gagnee
		son.finPartieGagne();
	}

	else if (!aGagne() && niveau != '0') {
		UIKit::color(FOREGROUND_GREEN + FOREGROUND_INTENSITY);
		//curseur Invisible
		UIKit::curseurVisible(false);
		//affichage martien

		UIKit::gotoXY(67, 26);
		cout << "   **" << endl;
		UIKit::gotoXY(67, 27);
		cout << " ******" << endl;
		UIKit::gotoXY(67, 28);
		cout << "********" << endl;
		UIKit::gotoXY(67, 29);
		cout << "*  **  *" << endl;
		UIKit::gotoXY(67, 30);
		cout << "********" << endl;
		UIKit::gotoXY(67, 31);
		cout << "  *  *  " << endl;
		UIKit::gotoXY(67, 32);
		cout << " *    * " << endl;
		//affichage message de fin 
		UIKit::gotoXY(53, 36);
		cout << "LES MARTIENS ONT CONQUIS LA PLANETE!" << endl;

		//son fin de partie perdue
		son.finPartiePerdue();
	}

	if (niveau != '0')
		Sleep(3000);
}


//affichage bye
void Jeu::auRevoir() {
	system("cls");
	UIKit::color(FOREGROUND_GREEN + FOREGROUND_INTENSITY);
	UIKit::gotoXY(67, 36);
	cout << "BYE" << endl;
	UIKit::gotoXY(53, 65);
}

void Jeu::rejouer() {
	system("cls");
	UIKit::color(FOREGROUND_GREEN + FOREGROUND_INTENSITY);
	UIKit::curseurVisible(false);
	UIKit::gotoXY(63, 36);
	cout << "JOUER ENCORE?(O/N)" << endl;
	reesayer = oui_non(); //attribut ressayer

	//reinitialiser le niveau
	niveau = '0';

	if (reesayer == 'N')
		auRevoir();
}

//validation O/N
char Jeu::oui_non() {
	char caractere;

	do {
		caractere = toupper(_getch());

	} while ((caractere != 'O') && (caractere != 'N'));

	return caractere;

}
