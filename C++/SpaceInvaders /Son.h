#pragma once
#include<Windows.h>


class Son
{

public:

	//jouer debut de partie
	void debutPartie();
	//jouer fin de partie perdue
	void finPartiePerdue();
	//jouer fin de partie gagne
	void finPartieGagne();
	//jouer collision
	void collision();
	//jouer tir
	void tir();
	// jouer fin
	void score();
};