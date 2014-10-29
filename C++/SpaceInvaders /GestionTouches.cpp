#include "GestionTouches.h"
#include <Windows.h>

//	constructeur
GestionTouches::GestionTouches() {
	nbTouches = 0;
}

//	ajout d'une touche � tester
void GestionTouches::ajouterTouche(char touche) {
	if (nbTouches < MAX_TOUCHES)
		touches[nbTouches++] = touche;
}

//	retourne la premi�re touche	press�e parmi celles � tester
char GestionTouches::estPressee() {
	int i = 0;
	while (i < nbTouches && 
		((GetAsyncKeyState(toupper(touches[i]))) & 0x8000) == 0)
		i++;

	return i < nbTouches ? touches[i] : -1;
}

//	retourne true si la touche pass�e en param�tre est press�e
bool GestionTouches::estPresse(char touche) {
	return GetAsyncKeyState(toupper(touche)) & 0x8000;
}