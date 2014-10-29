package com.mindescape.vue.utils;

import com.mindescape.vue.JeuActivity;

import android.content.Context;
import android.os.Handler;
import android.view.View;
import android.widget.FrameLayout;
import android.widget.ImageView;

public class Animate extends Handler implements Runnable {
	// attribut prive
	FrameLayout surfaceJeu;
	private boolean isRunning = false;
	private Sens sens;
	ImageView mainView;
	ImageView animView;
	int ressImgAnim; 
	int ressImgView;	
	boolean isFirstAnim;
	JeuActivity parent;
	ImageView imgTemps;

	// constructeur
	public Animate(Context context,FrameLayout surfaceJeu, ImageView mainView,
			ImageView animImg, Sens direction,int ressImgAnim, int ressImgView) {
		isRunning = false;
		isFirstAnim  = true;
		this.sens = direction;
		this.surfaceJeu = surfaceJeu;
		this.mainView = mainView;
		this.animView = animImg;
		this.ressImgAnim = ressImgAnim;
		this.ressImgView = ressImgView;
		
		parent = (JeuActivity) context;
		animView.setVisibility(View.INVISIBLE);
		mainView.setImageResource(ressImgView);
		animView.setImageResource(ressImgAnim);

	}

	public void setImageStart() {
		
		
		switch (sens) {
		case Gauche:
			animView.layout(surfaceJeu.getLeft() - animView.getWidth(),
					surfaceJeu.getTop(), surfaceJeu.getLeft(),
					surfaceJeu.getBottom());
			
			break;

		case Droite:

			animView.layout(surfaceJeu.getRight(), surfaceJeu.getTop(),
					surfaceJeu.getRight() + surfaceJeu.getRight(),
					surfaceJeu.getBottom());
			
			break;

		case Haut:
			animView.layout(surfaceJeu.getLeft(), surfaceJeu.getTop()
					- animView.getHeight(), surfaceJeu.getRight(),
					surfaceJeu.getTop());
			
			break;

		case Bas:
			animView.layout(surfaceJeu.getLeft(), surfaceJeu.getBottom(),
					surfaceJeu.getRight(),
					surfaceJeu.getBottom() + animView.getHeight());
			
			break;

		default:

			break;
		}
		
	}

	// methodes

	@Override
	public void run() {
		if (isFirstAnim) {
			setImageStart();			
			isFirstAnim=false;
			animView.setVisibility(View.VISIBLE);
		}
				
		if (isRunning) {				
			switch (this.sens) {
			case Gauche:			
				mainView.layout(mainView.getLeft() + 50, mainView.getTop(),
						mainView.getRight() + 50, mainView.getBottom());

				animView.layout(animView.getLeft() + 50, animView.getTop(),
						animView.getRight() + 50, animView.getBottom());

				if (animView.getRight() >= surfaceJeu.getRight()) 
					isRunning = false;
				break;

			case Droite:
				mainView.layout(mainView.getLeft() - 50, mainView.getTop(),
						mainView.getRight() - 50, mainView.getBottom());

				animView.layout(animView.getLeft() - 50, animView.getTop(),
						animView.getRight() - 50, animView.getBottom());
			
				
				if (animView.getRight() <= surfaceJeu.getRight()) 
					isRunning = false;		
				break;

			case Haut:				
				mainView.layout(mainView.getLeft(), mainView.getTop() + 50,
						mainView.getRight(), mainView.getBottom() + 50);

				animView.layout(animView.getLeft(), animView.getTop() + 50,
						animView.getRight(), animView.getBottom() + 50);

				if (animView.getTop() >= surfaceJeu.getTop()) 
					isRunning = false;
				break;

			case Bas:
				mainView.layout(mainView.getLeft(), mainView.getTop() - 50,
						mainView.getRight(), mainView.getBottom() - 50);

				animView.layout(animView.getLeft(), animView.getTop() - 50,
						animView.getRight(), animView.getBottom() - 50);

				if (animView.getTop() <= surfaceJeu.getTop()) 					
					isRunning = false;
				break;

			default:
				break;
			}			
			
			post(this);
			
		} else {			
			// changement de variable
			imgTemps = mainView;			
			mainView = animView;			
			animView = imgTemps;
			
			// ajuste le mainView si depacement
			mainView.layout(surfaceJeu.getLeft(), surfaceJeu.getTop(),
					surfaceJeu.getRight(),
					surfaceJeu.getBottom());
	
			parent.afficherReperer(parent.getCaseCourant());
			parent.setAnimation();
			parent.setIsClefOnView();
			
			removeCallbacks(this);
		}	
	}

	public void deplacement() {		
		if (!isRunning) {
		isRunning = true;
		post(this);
		}
	}

	public void setSens(Sens sens) {
		this.sens = sens;
		// this.deplacement();
	}

	public boolean getIsRunnig() {
		return this.isRunning;
	}

}