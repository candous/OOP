#include "Son.h"


//jouer debut de partie
void Son::debutPartie()
{
	PlaySound(TEXT("debut.wav"), NULL, SND_SYNC);
}
//jouer fin de partie perdue
void Son::finPartiePerdue()
{
	PlaySound(TEXT("gameover.wav"), NULL, SND_SYNC);
}
//jouer fin de partie gagne
void Son::finPartieGagne()
{
	PlaySound(TEXT("winner.wav"), NULL, SND_SYNC);
}
//jouer collision
void Son::collision()
{
	PlaySound(TEXT("collision.wav"), NULL, SND_ASYNC);
}

//jouer tir
void Son::tir()
{
	PlaySound(TEXT("laser2.wav"), NULL, SND_ASYNC);
}

//jouer score
void Son::score() {
	PlaySound(TEXT("scores.wav"), NULL, SND_SYNC);
}