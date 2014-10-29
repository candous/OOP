package com.mindescape.vue;
import com.example.mindescape.R;
import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.os.Bundle;
import android.util.DisplayMetrics;
import android.view.Menu;
import android.view.MenuItem;
import android.view.Window;
import android.view.WindowManager;
import android.view.animation.AlphaAnimation;
import android.view.animation.Animation;
import android.view.animation.Animation.AnimationListener;
import android.widget.ImageView;
import android.widget.RelativeLayout;

public class ConnexionActivity extends Activity {

	private ImageView logo;
	private RelativeLayout layout;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		//enlever barre de titre
	    this.requestWindowFeature(Window.FEATURE_NO_TITLE);
	    //enlever barre de notification
	    this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);

		setContentView(R.layout.activity_connexion);
		
		logo=(ImageView)findViewById(R.id.logoIntro);
		layout=(RelativeLayout)findViewById(R.id.layout);
		
		layout.setBackgroundColor(Color.BLACK);
		
		Bitmap logo2=BitmapFactory.decodeResource(getResources(), R.drawable.logo2);

		//resize de l'image par rapport a l'ecran
        DisplayMetrics display = new DisplayMetrics();
        getWindowManager().getDefaultDisplay().getMetrics(display);
        int screenWidth = display.widthPixels;
        int  screenHeight = display.heightPixels; 
        
        logo2 = Bitmap.createScaledBitmap(logo2, screenWidth/2, screenHeight/3, true);
		logo.setImageBitmap(logo2);

		//animation logo
		AlphaAnimation aa;
		aa=new AlphaAnimation(0, 1);
		aa.setDuration(3000);
		logo.setAnimation(aa);
		//ecouteur de l'animation
		aa.setAnimationListener(new AnimationListener() {
			
			@Override
			public void onAnimationStart(Animation arg0) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void onAnimationRepeat(Animation arg0) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void onAnimationEnd(Animation arg0) {
				//lancer la nouvelle activite
				startActivity(new Intent(ConnexionActivity.this, LevelActivity.class));
				finish();
			}
		});
		
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.connexion, menu);
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
