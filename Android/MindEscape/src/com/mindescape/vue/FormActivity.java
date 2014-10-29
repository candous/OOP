package com.mindescape.vue;
import com.example.mindescape.R;

import android.content.Intent;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.graphics.Color;
import android.os.Bundle;
import android.support.v7.app.ActionBarActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

public class FormActivity extends ActionBarActivity {

	private Intent intent;
	private EditText txtEditPrenom;
	private EditText txtEditNom;
	private TextView txtPrenom;
	private TextView txtNom;
	private TextView txtLevel;
	private TextView txtDeplace;
	private TextView txtTemps;
	private TextView txtEditLvl;
	private TextView txtEditDep;
	private TextView txtEditTem;
	private Button btnEnregistrer;
	private LinearLayout formLayout;
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
		setContentView(R.layout.activity_form);
		
		//recuperer parametres du intent
		intent=getIntent();
		lvl=intent.getIntExtra("level", 0);
		score=intent.getIntExtra("score", 0);
		min=intent.getIntExtra("minutes", 0);
		sec=intent.getIntExtra("seconds", 0);
		//recuperer les composants
		formLayout=(LinearLayout)findViewById(R.id.FormLayout);
		txtEditPrenom=(EditText)findViewById(R.id.editPrenom);
		txtEditNom=(EditText)findViewById(R.id.editNom);
		txtPrenom=(TextView)findViewById(R.id.txtPrenom);
		txtNom=(TextView)findViewById(R.id.txtNom);
		txtLevel=(TextView)findViewById(R.id.txtLvlForm);
		txtDeplace=(TextView)findViewById(R.id.txtDepForm);
		txtTemps=(TextView)findViewById(R.id.txtTemForm);
		txtEditLvl=(TextView)findViewById(R.id.txtEditLvl);
		txtEditDep=(TextView)findViewById(R.id.txtEditDep);
		txtEditTem=(TextView)findViewById(R.id.txtEditTem);
		btnEnregistrer=(Button)findViewById(R.id.btnEnrForm);
		
		//set texts avec les parametres recuperes
		txtEditLvl.setText(""+lvl);
		txtEditDep.setText(""+score);
		 if(min<10 && sec<10)
			 txtEditTem.setText("0"+min+":0"+sec);
		 else if(min<10)
			 txtEditTem.setText("0"+min+":"+sec);
		 else if(sec<10)
			 txtEditTem.setText(min+":0"+sec);
		 else
			 txtEditTem.setText(min+":"+sec);
		
		 //ecouteur
		 btnEnregistrer.setOnClickListener(boutonListener);
		 
		//style
		 formLayout.setBackgroundColor(Color.BLACK);
		 txtEditPrenom.setTextColor(Color.GRAY);
		 txtEditNom.setTextColor(Color.GRAY);
		 txtPrenom.setTextColor(Color.WHITE);
		 txtNom.setTextColor(Color.WHITE);
		 txtLevel.setTextColor(Color.WHITE);
		 txtDeplace.setTextColor(Color.WHITE);
		 txtTemps.setTextColor(Color.WHITE);
		 txtEditLvl.setTextColor(Color.WHITE);
		 txtEditDep.setTextColor(Color.WHITE);
		 txtEditTem.setTextColor(Color.WHITE);
		 btnEnregistrer.setBackgroundColor(Color.GRAY);
		 btnEnregistrer.setTextColor(Color.BLACK);
	}
	//listener
			private OnClickListener boutonListener = new OnClickListener() {
				@Override
				public void onClick(View v)
			    {	
					//recuperer les donnees saisies
					String prenom=txtEditPrenom.getText().toString();
					String nom=txtEditNom.getText().toString();
					
					if(prenom.length()<=0 ||prenom==null || nom.length()<=0 || nom==null)
						Toast.makeText(FormActivity.this, "Saisir le nom et prenom", Toast.LENGTH_LONG).show();
					else
					{
						//stocker dans preferences
						//recuperer le fichier de preferences, sil nexiste pas, il va etre cree
						SharedPreferences prefs=getSharedPreferences("prefs", MODE_PRIVATE);
						//editeur pour modifier le fichier
				        Editor editor = prefs.edit();
				        //stocker les donnees
				        editor.putString("Prenom", prenom);
				        editor.putString("Nom", nom);
				        editor.putInt("Level"+lvl,lvl );
				        editor.putInt("Score"+lvl, score);
				        editor.putInt("Minutes"+lvl, min);
				        editor.putInt("Seconds"+lvl, sec);
				        editor.commit();
				        //aller vers une autre activite
						startActivity(new Intent(FormActivity.this, ScoresActivity.class));
					}
				}
			};
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.form, menu);
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
