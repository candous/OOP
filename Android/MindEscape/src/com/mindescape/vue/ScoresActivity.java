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
import android.view.View.OnClickListener;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

public class ScoresActivity extends ActionBarActivity {

	private Button btnmenu;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		//enlever barre de titre
	    this.requestWindowFeature(Window.FEATURE_NO_TITLE);
	    //enlever barre de notification
	    this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
		setContentView(R.layout.activity_scores);
		
		//recuperer le fichier ou les infos sont stockes
		SharedPreferences prefs=getSharedPreferences("prefs", MODE_PRIVATE);
		//recuperer les valeurs avec des valeurs par defaut
        String nomStocke = prefs.getString("Nom", null);
        String prenomStocke=prefs.getString("Prenom", null);
        int score1Stocke=prefs.getInt("Score1", 0);
        int score2Stocke=prefs.getInt("Score2", 0);
        int score3Stocke=prefs.getInt("Score3", 0);
        int min1Stocke=prefs.getInt("Minutes1", 0);
        int min2Stocke=prefs.getInt("Minutes2", 0);
        int min3Stocke=prefs.getInt("Minutes3", 0);
        int sec1Stocke=prefs.getInt("Seconds1", 0);
        int sec2Stocke=prefs.getInt("Seconds2", 0);
        int sec3Stocke=prefs.getInt("Seconds3", 0);
        
      //recuperer composants
        btnmenu=(Button)findViewById(R.id.btnMenuScores);
        btnmenu.setBackgroundColor(Color.GRAY);
        btnmenu.setOnClickListener(boutonListener);
        ImageView imgScores=(ImageView)findViewById(R.id.imgScores);
        TextView txtNom=(TextView)findViewById(R.id.txtNomScores);
        TextView txtDepEdit=(TextView)findViewById(R.id.txtDepEditScores);
        TextView txtTemEdit=(TextView)findViewById(R.id.txtTemEditScores);
        TextView txtDep2Edit=(TextView)findViewById(R.id.txtDep2EditScores);
        TextView txtTem2Edit=(TextView)findViewById(R.id.txtTem2EditScores);
        TextView txtDep3Edit=(TextView)findViewById(R.id.txtDep3EditScores);
        TextView txtTem3Edit=(TextView)findViewById(R.id.txtTem3EditScores);
        
        //setter les texts
        txtNom.setText(prenomStocke+" "+nomStocke);
        txtDepEdit.setText(""+score1Stocke);
        txtDep2Edit.setText(""+score2Stocke);
        txtDep3Edit.setText(""+score3Stocke);
        
        if(min1Stocke<10 && sec1Stocke<10)
        	txtTemEdit.setText("0"+min1Stocke+":0"+sec1Stocke);
		 else if(min1Stocke<10)
			 txtTemEdit.setText("0"+min1Stocke+":"+sec1Stocke);
		 else if(sec1Stocke<10)
			 txtTemEdit.setText(min1Stocke+":0"+sec1Stocke);
		 else
			 txtTemEdit.setText(min1Stocke+":"+sec1Stocke);
        
        if(min2Stocke<10 && sec2Stocke<10)
        	txtTem2Edit.setText("0"+min2Stocke+":0"+sec2Stocke);
		 else if(min2Stocke<10)
			 txtTem2Edit.setText("0"+min2Stocke+":"+sec2Stocke);
		 else if(sec2Stocke<10)
			 txtTem2Edit.setText(min2Stocke+":0"+sec2Stocke);
		 else
			 txtTem2Edit.setText(min2Stocke+":"+sec2Stocke);
        
        if(min3Stocke<10 && sec3Stocke<10)
        	txtTem3Edit.setText("0"+min3Stocke+":0"+sec3Stocke);
		 else if(min3Stocke<10)
			 txtTem3Edit.setText("0"+min3Stocke+":"+sec3Stocke);
		 else if(sec3Stocke<10)
			 txtTem3Edit.setText(min3Stocke+":0"+sec3Stocke);
		 else
			 txtTem3Edit.setText(min3Stocke+":"+sec3Stocke);
 
        
        //recuperer image pour affichage
      	Bitmap imageScores=BitmapFactory.decodeResource(getResources(), R.drawable.bestscores);

      	//resize de l'image par rapport a l'ecran
      	DisplayMetrics display = new DisplayMetrics();
      	getWindowManager().getDefaultDisplay().getMetrics(display);
      	int screenWidth = display.widthPixels;
      	int  screenHeight = display.heightPixels; 
      	imageScores = Bitmap.createScaledBitmap(imageScores, screenWidth/2, screenHeight/3, true);
      	imgScores.setImageBitmap(imageScores); 
	}

	private OnClickListener boutonListener = new OnClickListener() {
		@Override
		public void onClick(View v)
	    {	
		    startActivity(new Intent(ScoresActivity.this, LevelActivity.class));
	    }
	};
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.scores, menu);
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
