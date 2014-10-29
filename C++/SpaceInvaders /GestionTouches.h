#pragma once

#define MAX_TOUCHES 20

class GestionTouches {
private:
	//	touches testées
	char touches[MAX_TOUCHES];
	int nbTouches;

public:
	//	constructeur
	GestionTouches();

	//	ajout d'une touche à tester
	void ajouterTouche(char touche);

	//	retourne la première touche	pressée parmi celles à tester
	char estPressee();

	//	retourne true si la touche passée en paramètre est pressée
	bool estPresse(char touche);
};