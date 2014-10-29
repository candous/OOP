#pragma once

#define MAX_TOUCHES 20

class GestionTouches {
private:
	//	touches test�es
	char touches[MAX_TOUCHES];
	int nbTouches;

public:
	//	constructeur
	GestionTouches();

	//	ajout d'une touche � tester
	void ajouterTouche(char touche);

	//	retourne la premi�re touche	press�e parmi celles � tester
	char estPressee();

	//	retourne true si la touche pass�e en param�tre est press�e
	bool estPresse(char touche);
};