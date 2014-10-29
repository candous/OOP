package com.mindescape.vue;
import com.example.mindescape.R;

import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.os.Bundle;
import android.support.v7.app.ActionBarActivity;
import android.util.DisplayMetrics;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

public class GameOverActivity extends ActionBarActivity {
	
	private LinearLayout layout;
	private ImageView imgFin;
	private TextView txtScore;
	private TextView txtLevel;
	private TextView txtPoints;
	private TextView txtTemps;
	private TextView txtTempsJoueur;
	private Button btnEnregistrer;
	private Button btnRecommencer;
	private Intent i;
	private int lvl;
	private int score;
	private int min;
	private int sec;
	
	
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		//enlever barre de titre
	    this.requestWindowFeature(Window.FEATURE_NO_TITLE);
	    //enlever barre de notification
	    this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);

		setContentView(R.layout.activity_game_over);
		
		//recuperer composants
		layout=(LinearLayout)findViewById(R.id.goLayout);
		imgFin=(ImageView)findViewById(R.id.imgFin);
		txtScore=(TextView)findViewById(R.id.txtScore);
		txtLevel=(TextView)findViewById(R.id.txtLevel);
		txtPoints=(TextView)findViewById(R.id.txtPoints);
		txtTemps=(TextView)findViewById(R.id.txtTemps);
		txtTempsJoueur=(TextView)findViewById(R.id.txtTempsJoueur);
		btnEnregistrer=(Button)findViewById(R.id.btnEnr);
		btnRecommencer=(Button)findViewById(R.id.btnRec);
		TextView txtRecord=(TextView)findViewById(R.id.txtRecord);
    	
		
		//style
		layout.setBackgroundColor(Color.BLACK);
		txtScore.setTextColor(Color.WHITE);
		txtLevel.setTextColor(Color.WHITE);
		txtPoints.setTextColor(Color.WHITE);
		txtTemps.setTextColor(Color.WHITE);
		txtTempsJoueur.setTextColor(Color.WHITE);
		btnEnregistrer.setBackgroundColor(Color.GRAY);
		btnRecommencer.setBackgroundColor(Color.GRAY);
		btnEnregistrer.setTextColor(Color.BLACK);
		btnRecommencer.setTextColor(Color.BLACK);
		txtRecord.setTextColor(Color.WHITE);
		txtRecord.setVisibility(View.GONE);
		btnEnregistrer.setEnabled(false);
		btnEnregistrer.setVisibility(View.GONE);
		
		//ecouteurs
		btnEnregistrer.setOnClickListener(boutonListener);
		btnRecommencer.setOnClickListener(boutonListener);
		
		//get image de fin
		Bitmap imageGO=BitmapFactory.decodeResource(getResources(), R.drawable.gameover);

		//resize de l'image par rapport a l'ecran
		 DisplayMetrics display = new DisplayMetrics();
		 getWindowManager().getDefaultDisplay().getMetrics(display);
		 int screenWidth = display.widthPixels;
		 int  screenHeight = display.heightPixels; 
		 imageGO = Bitmap.createScaledBitmap(imageGO, screenWidth/2, screenHeight/3, true);
		 imgFin.setImageBitmap(imageGO);
		 
		 
		 //get parametres qui viennent d'une autre activite
		 i=getIntent();
		 lvl=i.getIntExtra("level", 0);
		 score=i.getIntExtra("score", 0);
		 min=i.getIntExtra("minutes", 0);
		 sec=i.getIntExtra("seconds", 0);
		 txtLevel.setText("Level "+lvl);
		 txtPoints.setText(""+score);
		 if(min<10 && sec<10)
			 txtTempsJoueur.setText("0"+min+":0"+sec);
		 else if(min<10)
			 txtTempsJoueur.setText("0"+min+":"+sec);
		 else if(sec<10)
			 txtTempsJoueur.setText(min+":0"+sec);
		 else
			 txtTempsJoueur.setText(min+":"+sec);
		 
		//recuperer le fichier ou les infos sont stockes
			SharedPreferences prefs=getSharedPreferences("prefs", MODE_PRIVATE);
			//recuperer les valeurs avec des valeurs par defaut
	        int lvlStocke=prefs.getInt("Level"+lvl, 0);
	        int scoreStocke=prefs.getInt("Score"+lvl, 0);
	        int minStocke=prefs.getInt("Minutes"+lvl, 0);
	        int secStocke=prefs.getInt("Seconds"+lvl, 0);
	        
	        if(lvlStocke==lvl || lvlStocke==0)
	        {
	        	//new record
	        	if(scoreStocke==0 && minStocke==0 && secStocke==0 || score<scoreStocke && min<minStocke || score<scoreStocke && min==minStocke && sec<secStocke)
	        	{
	        		btnEnregistrer.setEnabled(true);
	        		btnEnregistrer.setVisibility(View.VISIBLE);
	        		//afficher new record dans la vue
	        		txtRecord.setVisibility(View.VISIBLE);
	        	}
	        }
	    
	}

	//listener boutons
		private OnClickListener boutonListener = new OnClickListener() {
			@Override
			public void onClick(View v)
		    {	
				Button b=(Button)v;
			    if(b.getId()==R.id.btnEnr)
			    {
			    	//envoyer le parametre pour l'autre activite
//			    	i.setClass(GameOverActivity.this, FormActivity.class);
			    	Intent intent=new Intent(GameOverActivity.this, FormActivity.class);
			    	intent.putExtra("level", lvl);
			    	intent.putExtra("score", score);
			    	intent.putExtra("minutes", min);
			    	intent.putExtra("seconds", sec);
				   startActivity(intent);
			    }
			    else if(b.getId()==R.id.btnRec)
			    	startActivity(new Intent(GameOverActivity.this, LevelActivity.class));
		    }
		};
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.game_over, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
}
