package com.mindescape.vue;

import java.util.ArrayList;
import java.util.Timer;
import java.util.TimerTask;

import android.app.AlertDialog;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.os.Handler;
import android.os.SystemClock;
import android.support.v7.app.ActionBarActivity;
import android.util.Log;
import android.view.Gravity;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup.LayoutParams;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;
import android.widget.Toast;

import com.example.mindescape.R;
import com.mindescape.modele.Case;
import com.mindescape.modele.Joueur;
import com.mindescape.modele.Niveau;
import com.mindescape.modele.Repere;
import com.mindescape.vue.utils.Animate;
import com.mindescape.vue.utils.Sens;
import com.mindescape.vue.utils.SurfaceGestureDetector;

public class JeuActivity extends ActionBarActivity {
	// attribut

	private RelativeLayout mainLayout;
	private FrameLayout surfaceJeu;
	private ImageView mainImg;
	private ImageView animImg;
	private Animate animation = null;

	private int niveau;
	private Niveau leNiveau;
	private Joueur leJoueur;
	private Case caseCourant;
	private SurfaceGestureDetector gestureDetec;

	private Sens direction;
	private TextView nbClef;
	private ImageView clefImgView;

	private FrameLayout frameImgRepere;
	private TextView nbRepere;
	private ArrayList<Repere> repererNiveau;
	private ImageView repereImageView;

	private long startTime = 0L;
	private long timeInMilliseconds = 0L;
	private long endTime = 0L;

	private Button afficherMap;
	private int posJoueurMap;

	private TableLayout tableLayout;
	private ArrayList<ImageView> imageCases;
	private ArrayList<TableRow> tableRows;

	private Case nextCase;
	private int ressImgAnim;
	private int ressImgView;
	private boolean isAnimation;	
	private int tempAffichageCarte;

	// a la creation de l activity
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		startTime = SystemClock.uptimeMillis();
		// enlever barre de titre
		this.requestWindowFeature(Window.FEATURE_NO_TITLE);
		// enlever barre de notification
		this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
				WindowManager.LayoutParams.FLAG_FULLSCREEN);

		setContentView(R.layout.jeu_activity);

		Intent intent = getIntent();
		niveau = intent.getIntExtra("level", R.xml.level1);

		leNiveau = new Niveau(this, niveau);
		leJoueur = new Joueur();
		repererNiveau = new ArrayList<Repere>();
		mainLayout = (RelativeLayout) findViewById(R.id.mainLayout);

		surfaceJeu = (FrameLayout) findViewById(R.id.FrameLayoutJeu);

		nbClef = (TextView) findViewById(R.id.nbClef);
		nbRepere = (TextView) findViewById(R.id.nbRepere);

		nbClef.setText(leJoueur.getNbcleCourant() + "/"
				+ leNiveau.getNombreClefNecessaire());
		nbRepere.setText(leJoueur.getNombredeRepereCourant() + "/"
				+ leNiveau.getNombreRepereMax());

		clefImgView = (ImageView) findViewById(R.id.clefImgView);
		clefImgView.setVisibility(View.INVISIBLE);
		
		frameImgRepere = (FrameLayout) findViewById(R.id.Framerepere);
		repereImageView = (ImageView) findViewById(R.id.imgRepere);		
		repereImageView.setVisibility(View.INVISIBLE);

		mainImg = (ImageView) findViewById(R.id.imageChemin);
		afficherMap = (Button) findViewById(R.id.btnMap);
		// Chercher la case de depart
		caseCourant = getCase(leNiveau.getPositionDepart());

		leJoueur.setPosition(caseCourant.getPosition());
		mainImg.setImageResource(getCheminId(caseCourant));
		// mainImg.setImageResource(R.drawable.chemin11);

		animImg = (ImageView) findViewById(R.id.animImg);

		// La vue pour declancher le scroll et le fling
		gestureDetec = new SurfaceGestureDetector(this);

		afficherMap.setText("Carte "
				+ leJoueur.getNombredeAffichageCarteCourant() + "/"
				+ leNiveau.getNombreAffichageCarte());

		afficherMap.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View arg0) {
				if (leJoueur.getNombredeAffichageCarteCourant() < leNiveau
						.getNombreAffichageCarte()) {
					afficherAlerteDialog();
					leJoueur.setNombredeAffichageCarteCourant();
					afficherMap.setText("Carte "
							+ leJoueur.getNombredeAffichageCarteCourant() + "/"
							+ leNiveau.getNombreAffichageCarte());
				} else
					Toast.makeText(getApplicationContext(),
							"Vous ne pouvez plus afficher la carte",
							Toast.LENGTH_SHORT).show();
			}
		});

		//afficherReperer(caseCourant);
		tempAffichageCarte = 2000;
		
	}

	// methode
	// Chercher le bon image correspondant a la case
	public int getCheminId(Case laCase) {
		int leCheminId = -1;

		int idImage = laCase.getTypeImage();
		switch (idImage) {
		case 0:
			leCheminId = R.drawable.chemin0;
			break;
		case 11:
			leCheminId = R.drawable.chemin11;
			break;
		case 12:
			leCheminId = R.drawable.chemin12;
			break;
		case 13:
			leCheminId = R.drawable.chemin13;
			break;
		case 14:
			leCheminId = R.drawable.chemin14;
			break;
		case 21:
			leCheminId = R.drawable.chemin21;
			break;
		case 22:
			leCheminId = R.drawable.chemin22;
			break;
		case 23:
			leCheminId = R.drawable.chemin23;
			break;
		case 24:
			leCheminId = R.drawable.chemin24;
			break;
		case 31:
			leCheminId = R.drawable.chemin31;
			break;
		case 32:
			leCheminId = R.drawable.chemin32;
			break;
		case 33:
			leCheminId = R.drawable.chemin33;
			break;
		case 34:
			leCheminId = R.drawable.chemin34;
			break;
		case 4:
			leCheminId = R.drawable.chemin4;
			break;
		case 51:
			leCheminId = R.drawable.chemin51;
			break;
		case 52:
			leCheminId = R.drawable.chemin52;
			break;
		case 61:
			leCheminId = R.drawable.chemin61;
			break;
		case 62:
			leCheminId = R.drawable.chemin62;
			break;
		case 63:
			leCheminId = R.drawable.chemin63;
			break;
		case 64:
			leCheminId = R.drawable.chemin64;
			break;
		}

		return leCheminId;
	}

	// Cherche la case parmis tous les cases a partir de la position
	public Case getCase(int indice) {
		Case laCase = new Case();

		int i = 0;
		while (i < leNiveau.getGrilleNiveau().size()
				&& leNiveau.getGrilleNiveau().get(i).getPosition() != indice)
			i++;

		if (i < leNiveau.getGrilleNiveau().size()) {
			laCase = leNiveau.getGrilleNiveau().get(i);
		}

		return laCase;

	}

	// Chercher la case prochaine
	public Case getNextCase(Case laCaseCourant, Sens laDirection) {
		Case nextCase = new Case();
		int indice = 0;
		switch (laDirection) {
		case Droite:
			indice = laCaseCourant.getPosition() + 1;
			break;
		case Gauche:
			indice = laCaseCourant.getPosition() - 1;
			break;
		case Haut:
			indice = laCaseCourant.getPosition() - 10;
			break;
		case Bas:
			indice = laCaseCourant.getPosition() + 10;
			break;
		default:
			break;
		}

		int i = 0;
		while (i < laCaseCourant.getCasesPossibles().size()
				&& laCaseCourant.getCasesPossibles().get(i) != indice)
			i++;
		if (i < laCaseCourant.getCasesPossibles().size())
			nextCase = getCase(caseCourant.getCasesPossibles().get(i));
		else
			nextCase = null;

		return nextCase;
	}

	// afficher l alert dialog
	private void afficherAlerteDialog() {

		final Context c = this;

		final Handler h = new Handler();
		h.post(new Runnable() {

			boolean firstRun = true;

			@Override
			public void run() {

				tableLayout = createTableLayout();
				// Creer l'alerteDialog
				AlertDialog.Builder builder = new AlertDialog.Builder(c);
				builder.setView(tableLayout);

				final AlertDialog alert = builder.create();
				alert.show();

				final Timer t = new Timer();
				t.schedule(new TimerTask() {
					@Override
					public void run() {
						alert.dismiss();
						t.cancel();
					}
				}, tempAffichageCarte);

				h.removeCallbacks(this);
			}
		});

	}

	// creer le table layout qu on verra dans l alert dialog
	private TableLayout createTableLayout() {
		// ArrayList des Images
		imageCases = new ArrayList<ImageView>();
		posJoueurMap = 0;
		for (int i = 0; i < leNiveau.getGrilleNiveau().size(); i++) {
			ImageView image = new ImageView(this);
			image.setLayoutParams(new LayoutParams(100, 100));
			image.setImageResource(getCheminId(leNiveau.getGrilleNiveau()
					.get(i)));
			imageCases.add(image);
			if (leNiveau.getGrilleNiveau().get(i).getPosition() < leJoueur
					.getPosition())
				posJoueurMap++;
		}

		// 1) Create a tableLayout and its params
		TableLayout.LayoutParams tableLayoutParams = new TableLayout.LayoutParams();
		TableLayout tableLayout = new TableLayout(this);
		tableLayout.setBackgroundColor(Color.BLACK);

		// 2) create tableRow params
		TableRow.LayoutParams tableRowParams = new TableRow.LayoutParams();
		tableRowParams.weight = 1;

		// create frame params
		FrameLayout.LayoutParams lFram = new FrameLayout.LayoutParams(100, 100);
		lFram.gravity = Gravity.CENTER;

		FrameLayout.LayoutParams lFram2 = new FrameLayout.LayoutParams(50, 50);
		lFram2.gravity = Gravity.CENTER;

		int[] lignecolone = getNbRowsNbColone(leNiveau.getGrilleNiveau()
				.get(leNiveau.getGrilleNiveau().size() - 1).getPosition());

		int z = 0;
		for (int i = 0, ligne = lignecolone[0]; i < ligne; i++) {
			// 3) create tableRow
			TableRow tableRow = new TableRow(this);
			tableRow.setBackgroundColor(Color.BLACK);

			for (int j = 0, colone = lignecolone[1]; j < colone; j++) {

				FrameLayout f = new FrameLayout(this);
				ImageView img = imageCases.get(z);
				f.addView(img, lFram);

				if (posJoueurMap == z) {
					ImageView posJoueImg = new ImageView(this);
					posJoueImg.setImageResource(R.drawable.repere);
					f.addView(posJoueImg, lFram2);
				}

				tableRow.addView(f, tableRowParams);
				z++;

			}

			// 6) add tableRow to tableLayout
			tableLayout.addView(tableRow, tableLayoutParams);
		}

		return tableLayout;
	}

	// methode pour connaitre le nombre de ligne et de case
	private int[] getNbRowsNbColone(int lastposition) {
		int[] ligneColone = new int[2];
		ligneColone[0] = lastposition / 10;
		ligneColone[1] = lastposition % 10;

		return ligneColone;
	}

	// gestion des evnement
	@Override
	public boolean onTouchEvent(MotionEvent event) {
		// TODO Auto-generated method stub

		boolean consume = true;
		if (animation != null)
			consume = false;
		else
			consume = true;

		setDirection(Sens.Aucune);
		// aviser le gesturedetector de l evenement
		gestureDetec.getGestureDetector().onTouchEvent(event);

		// Jeu
		switch (direction) {
		case Gauche:

			deplacementDansJeu();
			break;
		case Droite:

			deplacementDansJeu();
			break;
		case Haut:

			deplacementDansJeu();
			break;
		case Bas:

			deplacementDansJeu();
			break;

		default:
			setDirection(Sens.Aucune);
			break;
		}

		return consume;

	}

	private void deplacementDansJeu() {

		if(!isAnimation){
			nextCase = getNextCase(caseCourant, direction);

		if (nextCase != null) {
			isAnimation = true;
			getRepereImageView().setVisibility(View.INVISIBLE);
			ressImgAnim = getCheminId(nextCase);
			ressImgView = getCheminId(caseCourant);

			// Declancher l'animation
			animation = new Animate(this, surfaceJeu, mainImg, animImg,
					direction, ressImgAnim, ressImgView);

			animation.deplacement();

			caseCourant = nextCase;
			
			leJoueur.setNombreDeplacement();
			leJoueur.setPosition(caseCourant.getPosition());
			setIsSortieOnView(caseCourant);

		} else
			Toast.makeText(this, "Mouvement Imposible", Toast.LENGTH_SHORT)
					.show();
		}
	}

	// ce qu on fait quand il y a une clef
	public void setIsClefOnView() {

		if (caseCourant.isClef())
			clefImgView.setVisibility(View.VISIBLE);
		else
			clefImgView.setVisibility(View.INVISIBLE);
	}

	// ce qu on fait quand on est a la sortie
	public void setIsSortieOnView(Case casecourante) {
		if (casecourante.isSortie()
				&& leJoueur.getNbcleCourant() == leNiveau
						.getNombreClefNecessaire()) {
			Toast.makeText(
					this,
					"vous avez gagner ce niveau et fait "
							+ leJoueur.getNombreDeplacement() + " deplacements",
					Toast.LENGTH_SHORT).show();

			endTime = SystemClock.uptimeMillis();
			timeInMilliseconds = endTime - startTime;
			int secs = (int) (timeInMilliseconds / 1000);
			int mins = secs / 60;
			secs = secs % 60;

			int lvl = 0;
			if (niveau == R.xml.level1)
				lvl = 1;
			else if (niveau == R.xml.level2)
				lvl = 2;
			else if (niveau == R.xml.level3)
				lvl = 3;

			Intent intent = new Intent(JeuActivity.this, GameOverActivity.class);
			intent.putExtra("level", lvl);
			intent.putExtra("score", leJoueur.getNombreDeplacement());
			intent.putExtra("minutes", mins);
			intent.putExtra("seconds", secs);
			startActivity(intent);

		}
	}

	// Doit etre appeler dans setIsSortieOnView
	public void allerNiveauSuperieur() {
		niveau++;
		if (niveau <= 3) {
			leNiveau = new Niveau(this, niveau);
			// Chercher la case de depart
			caseCourant = getCase(leNiveau.getPositionDepart());

			mainImg.setImageResource(getCheminId(caseCourant));
			// mainImg.setImageResource(R.drawable.chemin11);
		}
	}

	
	// methode pour repere
	// ajouter un repere dans le niveau
	public void ajouterRepere(Repere leRepere) {
		repererNiveau.add(leRepere);		;
	}

	// supprimer un reperer
	public void supprimerRepere(int position) {

		int i = 0;
		while (i < repererNiveau.size()
				&& position != repererNiveau.get(i).getPosition())
			i++;

		if (i < repererNiveau.size())
			repererNiveau.remove(i);

		leJoueur.setNombredeRepereCourantMoins();
		getNbRepere().setText(
				leJoueur.getNombredeRepereCourant() + "/"
						+ leNiveau.getNombreRepereMax());
		caseCourant.setRepere(false);
		afficherReperer(caseCourant);

	}

	//afficher les reprer
	public void afficherReperer(final Case caseCourant) {
		
	
		if (caseCourant.isRepere()) {
			Repere leRepere = getLeRepere(caseCourant.getPosition());
			Log.i("repere","Position " + leRepere.getPosition()+ " x: "+ leRepere.getPosX() + " y: " + leRepere.getPosY() + " posCourante : "+caseCourant.getPosition());
			Log.i("repere","Position avant  l: "+ repereImageView.getLeft() + " t: " + repereImageView.getTop() 
					+ " r: " + repereImageView.getRight() + " b: " + repereImageView.getBottom() );

			repereImageView.layout(
					leRepere.getPosX(),
					leRepere.getPosY() ,
					leRepere.getPosX()+repereImageView.getWidth(),
					leRepere.getPosY()+repereImageView.getHeight() ) ;
			
			repereImageView.setVisibility(View.VISIBLE);
			
			
			Log.i("repere","Position apres l: "+ repereImageView.getLeft() + " t: " + repereImageView.getTop() 
					+ " r: " + repereImageView.getRight() + " b: " + repereImageView.getBottom() );
		//	h.post(r);
		} else {
			repereImageView.setVisibility(View.INVISIBLE);
		}
	}

	private Repere getLeRepere(int position) {
		Repere leRepere = null;
		int i = 0;
		while (i < repererNiveau.size()
				&& position != repererNiveau.get(i).getPosition())
			i++;
		if (i < repererNiveau.size())
			leRepere = repererNiveau.get(i);

		return leRepere;
	}

	// getteur setteur
	public Sens getDirection() {
		return direction;
	}

	public void setDirection(Sens direction) {
		this.direction = direction;
	}

	public FrameLayout getSurfaceJeu() {
		return surfaceJeu;
	}

	public Niveau getLeNiveau() {
		return leNiveau;
	}

	public Joueur getLeJoueur() {
		return leJoueur;
	}

	public Case getCaseCourant() {
		return caseCourant;
	}

	public void setCaseCourant(Case caseCourant) {
		this.caseCourant = caseCourant;
	}

	public ImageView getClefImgView() {
		return clefImgView;
	}

	public TextView getNbClef() {
		return nbClef;
	}

	public RelativeLayout getMainLayout() {
		return mainLayout;
	}

	public TextView getNbRepere() {
		return nbRepere;
	}

	public ImageView getRepereImageView() {
		return repereImageView;
	}

	public void setAnimation() {
		if (!isAnimation)
			isAnimation = true;
		else
			isAnimation = false;
	}

}
