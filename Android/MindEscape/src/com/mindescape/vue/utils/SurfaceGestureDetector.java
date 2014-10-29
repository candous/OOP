package com.mindescape.vue.utils;

import android.content.Context;
import android.view.GestureDetector;
import android.view.GestureDetector.OnGestureListener;
import android.view.MotionEvent;
import android.view.SurfaceView;
import android.widget.Toast;

import com.mindescape.modele.Repere;
import com.mindescape.vue.JeuActivity;

public class SurfaceGestureDetector extends SurfaceView implements
		OnGestureListener {

	private JeuActivity parent;

	private GestureDetector gestureDetector;

	public SurfaceGestureDetector(Context context) {
		super(context);

		gestureDetector = new GestureDetector(context, this);

		parent = (JeuActivity) context;

		parent.setDirection(Sens.Aucune);
	}

	public GestureDetector getGestureDetector() {
		return gestureDetector;
	}

	public void setGestureDetector(GestureDetector gestureDetector) {
		this.gestureDetector = gestureDetector;
	}

	@Override
	public boolean onDown(MotionEvent e) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public void onShowPress(MotionEvent e) {
		// TODO Auto-generated method stub

	}

	@Override
	public boolean onSingleTapUp(MotionEvent e) {
		// TODO Auto-generated method stub

		if (parent.getCaseCourant().isClef()) {

			parent.getCaseCourant().setClef(false);
			parent.setIsClefOnView();

			parent.getLeJoueur().setNbcleCourant();

			parent.getNbClef().setText(
					parent.getLeJoueur().getNbcleCourant() + "/"
							+ parent.getLeNiveau().getNombreClefNecessaire());

			Toast.makeText(parent.getApplicationContext(),
					"Vous avez recolte une clef", Toast.LENGTH_SHORT).show();
		} else if (parent.getLeJoueur().getNombredeRepereCourant() < parent
				.getLeNiveau().getNombreRepereMax()
				&& !parent.getCaseCourant().isRepere()) {

			int posX = (int) e.getX() - parent.getRepereImageView().getWidth()/4;
			int posY = (int) e.getY() - parent.getRepereImageView().getHeight()/4;
			int position = parent.getCaseCourant().getPosition();

			Repere leRepere = new Repere(position, posX, posY);

			
			parent.getLeJoueur().setNombredeRepereCourantPlus();
			parent.getNbRepere().setText(
					parent.getLeJoueur().getNombredeRepereCourant() + "/"
							+ parent.getLeNiveau().getNombreRepereMax());
			parent.getCaseCourant().setRepere(true);
			
			parent.ajouterRepere(leRepere);
			parent.afficherReperer(parent.getCaseCourant());
			

		} else if (parent.getCaseCourant().isRepere()) {
			parent.supprimerRepere(parent.getCaseCourant().getPosition());

		} else
			Toast.makeText(parent.getApplicationContext(),
					"Vous n'avez plus de bonus pour utiliser le repere",
					Toast.LENGTH_SHORT).show();

		return false;
	}

	@Override
	public boolean onScroll(MotionEvent e1, MotionEvent e2, float distanceX,
			float distanceY) {
//		float dx = e2.getX() - e1.getX();
//		float dy = e2.getY() - e1.getY();
//
//		if (dx > 0 && dy < 0) {
//			if (dx > (dy * -1))
//
//				parent.setDirection(Sens.Droite);
//			else
//				parent.setDirection(Sens.Haut);
//		} else if (dx < 0 && dy > 0) {
//			if ((dx * -1) > dy)
//				parent.setDirection(Sens.Gauche);
//			else
//				parent.setDirection(Sens.Bas);
//		} else if (dx < 0 && dy < 0) {
//			if ((dx * -1) > (dy * -1))
//				parent.setDirection(Sens.Gauche);
//			else
//				parent.setDirection(Sens.Haut);
//
//		} else if (dx > 0 && dy > 0) {
//			if (dx > dy)
//				parent.setDirection(Sens.Droite);
//			else
//				parent.setDirection(Sens.Bas);
//		} else if (dx > 0) {
//			parent.setDirection(Sens.Droite);
//		} else if (dx < 0) {
//			parent.setDirection(Sens.Gauche);
//		}
//
//		else if (dy < 0) {
//			parent.setDirection(Sens.Haut);
//		} else {
//			parent.setDirection(Sens.Bas);
//		}
		return false;
	}

	@Override
	public void onLongPress(MotionEvent e) {
		// TODO Auto-generated method stub
	}

	@Override
	public boolean onFling(MotionEvent e1, MotionEvent e2, float velocityX,
			float velocityY) {

		float dx = e2.getX() - e1.getX();
		float dy = e2.getY() - e1.getY();

		if (dx > 0 && dy < 0) {
			if (dx > (dy * -1))

				parent.setDirection(Sens.Droite);
			else
				parent.setDirection(Sens.Haut);
		} else if (dx < 0 && dy > 0) {
			if ((dx * -1) > dy)
				parent.setDirection(Sens.Gauche);
			else
				parent.setDirection(Sens.Bas);
		} else if (dx < 0 && dy < 0) {
			if ((dx * -1) > (dy * -1))
				parent.setDirection(Sens.Gauche);
			else
				parent.setDirection(Sens.Haut);

		} else if (dx > 0 && dy > 0) {
			if (dx > dy)
				parent.setDirection(Sens.Droite);
			else
				parent.setDirection(Sens.Bas);
		} else if (dx > 0) {
			parent.setDirection(Sens.Droite);
		} else if (dx < 0) {
			parent.setDirection(Sens.Gauche);
		}

		else if (dy < 0) {
			parent.setDirection(Sens.Haut);
		} else {
			parent.setDirection(Sens.Bas);
		}

		return false;

	}

}
