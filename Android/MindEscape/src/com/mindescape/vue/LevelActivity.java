package com.mindescape.vue;
import com.example.mindescape.R;

import android.content.Intent;
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
import android.widget.LinearLayout;
import android.widget.TextView;

public class LevelActivity extends ActionBarActivity {

	private LinearLayout layoutLevel;
	private Button btnlvl1;
	private Button btnlvl2;
	private Button btnlvl3;
	private ImageView image;
	private TextView txt;
	private int lvl;
	
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
	
		//enlever barre de titre
	    this.requestWindowFeature(Window.FEATURE_NO_TITLE);
	    //enlever barre de notification
	    this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);

		setContentView(R.layout.level_activity);
		
		//recuperer les composants
		layoutLevel=(LinearLayout)findViewById(R.id.LayoutLevel);
		btnlvl1=(Button)findViewById(R.id.buttonlvl1);
		btnlvl2=(Button)findViewById(R.id.buttonlvl2);
		btnlvl3=(Button)findViewById(R.id.buttonlvl3);
		image=(ImageView)findViewById(R.id.imgLogo);
		txt=(TextView)findViewById(R.id.txtlvl);
		
		//style
		layoutLevel.setBackgroundColor(Color.BLACK);
		txt.setTextColor(Color.WHITE);
		btnlvl1.setBackgroundColor(Color.GRAY);
		btnlvl1.setTextColor(Color.WHITE);
		btnlvl2.setBackgroundColor(Color.GRAY);
		btnlvl2.setTextColor(Color.WHITE);
		btnlvl3.setBackgroundColor(Color.GRAY);
		btnlvl3.setTextColor(Color.WHITE);
	
		//logo
		Bitmap logo2=BitmapFactory.decodeResource(getResources(), R.drawable.logo2);

		//resize de l'image par rapport a l'ecran
        DisplayMetrics display = new DisplayMetrics();
        getWindowManager().getDefaultDisplay().getMetrics(display);
        int screenWidth = display.widthPixels;
        int  screenHeight = display.heightPixels; 
        logo2 = Bitmap.createScaledBitmap(logo2, screenWidth/2, screenHeight/3, true);
		image.setImageBitmap(logo2);
		
		//ecouteurs
		btnlvl1.setOnClickListener(boutonListener);
		btnlvl2.setOnClickListener(boutonListener);
		btnlvl3.setOnClickListener(boutonListener);
	}
	
	//listener boutons
	private OnClickListener boutonListener = new OnClickListener() {
		@Override
		public void onClick(View v)
	    {	
			Button b=(Button)v;
		    if(b.getId()==R.id.buttonlvl1)
		    	lvl=R.xml.level1;
		    else if(b.getId()==R.id.buttonlvl2)
		    	lvl=R.xml.level2;
		    else if(b.getId()==R.id.buttonlvl3)
		    	lvl=R.xml.level3;
		    
		    //envoyer le parametre pour la activite jeu 
		    Intent intent=new Intent(LevelActivity.this,JeuActivity.class);//JeuActivity.class
		    intent.putExtra("level", lvl);
		    startActivity(intent);
	    }
	};
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.level, menu);
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
