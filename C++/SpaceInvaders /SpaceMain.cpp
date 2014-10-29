#include "Jeu.h"
#include "UIKit.h"
#include <stdlib.h>
#include <time.h>

#include "Plutonien.h"
#include "MesMartien.h"
#include "Highscores.h"

#include <iostream>
using namespace std;

int main() {


	srand((int)time(NULL));

	Jeu* leJeu;
	
	do {

		leJeu = new Jeu;

		leJeu->creerFenetre();


		while (leJeu->niveau != '0') {
			leJeu->afficherCadre();

			while (!leJeu->estTermine())
				leJeu->faireUnTourDeJeu();
			leJeu->affichageFin();

			afficherScores(leJeu->getNom(), leJeu->getScore());
			leJeu->rejouer();

		}
		
		
	} while (leJeu->reesayer == 'O');
	

	return 0;
}