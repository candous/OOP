#pragma once

#include "MonVaisseau.h"
#include "GestionTouches.h"
#include "MesMartien.h"
#include <string>
#include "Son.h"
using namespace std;

#define MAX_VIE 3
#define LIGNES_MARTIEN_MAX 3
#define X_MIN_VAISSEAU 1
#define X_MAX_VAISSEAU 98
#define X_ECRAN 140
#define Y_ECRAN 70
#define X_CADRE 105
#define Y_CADRE 67

class MonVaisseau;
class MesMartien;

class Jeu {
private:
	// ligne de martien
	
	//MesMartien *ligneMartiens;
	MesMartien *ligneMartiens[LIGNES_MARTIEN_MAX];
	int nbLigne;

	
	// le vaiseau
	MonVaisseau *leVaisseau;

	// score du jeu en fonction des matiens tués
	int score;

	// nombre de chance avant de mourir
	int nbVie;

	// nom du joueur
	string nom;	

	// methode qui valide la saisie
	char valid_niveau();
	//methode qui affiche le menu
	void afficherMenu();

public:

	Son son;
	// instance de son : pour jouer les son de depart et de fin
	//MonSon son;

	// pour option menu 
	char niveau;
	char reesayer;

	int nbVieTemps = getNbVie();
	int scoreTemps = getScore();

	//instance de gestion touche (permettra d avoir plusieurs touches appuyées en meme temps)
	GestionTouches gestionTouchesMvtVaisseau, gestionTouchesTirVaisseau;
	
	// constructeur de jeu
	Jeu();

	//getteur
	MonVaisseau getLeVaisseau() const;
	int getScore();
	string getNom() const;
	int getNbVie()const;
	int getNbLigne()const;
	bool aGagne();

	//setteur
	void setScore(int valeurET);
	void setNbVie();
	void setNbLigne();
	void setVieMax();

	// fonction principal du jeu qui fait les actions des martiens et du vaisseau
	void faireUnTourDeJeu();

	// determine si le jeu est fini ou pas
	bool estTermine();

	//methode qui cree la fenetre
	void creerFenetre();
	//methode qui affiche le cadre
	void afficherCadre();
	//methode qui affiche fin perdue
	void affichageFin();
	//methode pour rejour
	void rejouer();
	//affichage bye
	void auRevoir();
	//validation O/N
	char oui_non();

	
	void actualisationScoreNbvie();
	

};