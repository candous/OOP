package com.mindescape.modele;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserException;
import android.content.Context;
import android.content.res.XmlResourceParser;

public class Niveau {

	// atribut
	private int nombreRepereMax;
	private int positionDepart;
	private int nombrePasMax;
	private int nombreClefNecessaire;
	private int nombreAffichageCarte;
	private ArrayList<Case> grilleNiveau;
	private XmlResourceParser xpp;
	private int level;
	private Context context;

	// constructeur
	public Niveau(Context context, int level) {
		this.context = context;
		this.level = level;

		try {
			this.populerGrilleNiveau();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (XmlPullParserException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	// methodes
	private void populerGrilleNiveau() throws IOException,
			XmlPullParserException {
		// Create ResourceParser for XML file
		xpp = context.getResources().getXml(level);
		// check state
		int eventType = xpp.getEventType();
		String name = null;
		Case cTemp = null;
		ArrayList<Integer> positionPosible = null;
		while (eventType != XmlPullParser.END_DOCUMENT) {
			// instead of the following if/else if lines
			// you should custom parse your xml

			switch (eventType) {
			case XmlPullParser.START_DOCUMENT:
				grilleNiveau = new ArrayList<Case>();
				break;

			case XmlPullParser.START_TAG:
				name = xpp.getName();

				if (xpp.getName().equals("CasesPossibles")) {
					positionPosible = new ArrayList<Integer>();
				}

				if (xpp.getName().equals("Case")) {
					cTemp = new Case();
				}
				break;

			case XmlPullParser.END_TAG:
				if (xpp.getName().equals("CasesPossibles")) {
					cTemp.setCasesPossibles(positionPosible);
				}
				if (xpp.getName().equals("Case")) {
					grilleNiveau.add(cTemp);
					cTemp = null;
				}
				break;

			case XmlPullParser.TEXT:
				// Log.i("XML", "name " + name + " valeur : " + xpp.getText());
				if (name.equals("NombreRepereMax")) {
					nombreRepereMax = Integer.parseInt(xpp.getText());
				}
				if (name.equals("NombrePasMax")) {
					nombrePasMax = Integer.parseInt(xpp.getText());
				}
				if (name.equals("NombreClefNecessaire")) {
					nombreClefNecessaire = Integer.parseInt(xpp.getText());
				}

				if (name.equals("PositionDepart")) {
					positionDepart = Integer.parseInt(xpp.getText());
				}
				if (name.equals("NombreAffichageCarte")) {
					nombreAffichageCarte = Integer.parseInt(xpp.getText());
				}

				if (name.equals("Position")) {
					cTemp.setPosition(Integer.parseInt(xpp.getText()));
				}

				if (name.equals("ImageCase")) {
					cTemp.setTypeImage(Integer.parseInt(xpp.getText()));
				}

				if (name.equals("Clef")) {
					boolean isclef = false;

					if (xpp.getText().equals("1"))
						isclef = true;
					cTemp.setClef(isclef);					 
				}

				if (name.equals("Sortie")) {
					boolean isSortie = false;
					if (Integer.parseInt(xpp.getText()) == 1)
						isSortie = true;
					cTemp.setSortie(isSortie);
					
				}

				if (name.equals("PositionPossible")) {
					positionPosible.add(Integer.parseInt(xpp.getText()));
					
				}

				break;

			default:
				break;
			}

			eventType = xpp.next();
		}
		// indicate app done reading the resource.
		xpp.close();

//		for (Case c : grilleNiveau) {
//			Log.i("xml", " position : " + c.getPosition());
//			Log.i("xml", " ImageCase : " + c.getTypeImage());
//			Log.i("xml", " Clef : " + c.isClef());
//			Log.i("xml", " Sortie : " + c.isSortie());
//			Log.i("xml", " position Possible : "
//					+ c.getCasesPossibles().toString());
//		}
	}

	// getteur setteur
	public int getNombreRepereMax() {
		return nombreRepereMax;
	}

	public void setNombreRepereMax(int nombreRepereMax) {
		this.nombreRepereMax = nombreRepereMax;
	}

	public int getNombrePasMax() {
		return nombrePasMax;
	}

	public void setNombrePasMax(int nombrePasMax) {
		this.nombrePasMax = nombrePasMax;
	}

	public int getNombreClefNecessaire() {
		return nombreClefNecessaire;
	}

	public void setNombreClefNecessaire(int nombreClefNecessaire) {
		this.nombreClefNecessaire = nombreClefNecessaire;
	}

	public List<Case> getGrilleNiveau() {
		return grilleNiveau;
	}

	public int getPositionDepart() {
		return positionDepart;
	}

	public void setPositionDepart(int positionDepart) {
		this.positionDepart = positionDepart;
	}

	public int getNombreAffichageCarte() {
		return nombreAffichageCarte;
	}

	public void setNombreAffichageCarte(int nombreAffichageCarte) {
		this.nombreAffichageCarte = nombreAffichageCarte;
	}
}
