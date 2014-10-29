#include "GestionTouches.h"
#include <Windows.h>

//	constructeur
GestionTouches::GestionTouches() {
	nbTouches = 0;
}

//	ajout d'une touche à tester
void GestionTouches::ajouterTouche(char touche) {
	if (nbTouches < MAX_TOUCHES)
		touches[nbTouches++] = touche;
}

//	retourne la première touche	pressée parmi celles à tester
char GestionTouches::estPressee() {
	int i = 0;
	while (i < nbTouches && 
		((GetAsyncKeyState(toupper(touches[i]))) & 0x8000) == 0)
		i++;

	return i < nbTouches ? touches[i] : -1;
}

//	retourne true si la touche passée en paramètre est pressée
bool GestionTouches::estPresse(char touche) {
	return GetAsyncKeyState(toupper(touche)) & 0x8000;
}