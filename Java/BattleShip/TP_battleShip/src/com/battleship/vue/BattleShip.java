package com.battleship.vue;

import java.awt.Color;
import java.awt.Component;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.GridLayout;
import java.awt.Image;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.IOException;
import java.util.ArrayList;

import javax.imageio.ImageIO;
import javax.swing.BoxLayout;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.SwingConstants;

import com.battleship.controleur.Controleur;

public class BattleShip extends JFrame {

	private static final long serialVersionUID = 1L;

	// attributs
	private JLabel nomLabel;
	private JLabel pointsJoueur1;
	private JLabel nomLabel2;
	private JLabel pointsJoueur2;
	private JLabel messages;
	private BoutonCadre bouton;
	private BoutonCadre bouton2;
	private JButton reset;
	private JButton quitter;
	private JPanel cadreJoueur1;
	private JPanel cadreJoueur2;
	private JPanel cadres;
	
	Ecouteur ecouteurBouton;

	private ImageIcon img;

	private ArrayList<BoutonCadre> listeBoutons1;
	private ArrayList<BoutonCadre> listeBoutons2;

	Controleur leControleur;

	
	
	// constructeur
	public BattleShip(Controleur leControleur) {
		super("BattleShip");
		System.out.println("constructeur BattleShip");
		this.leControleur = leControleur;
		setSize(1200, 800);
		getContentPane().setLayout(
				new BoxLayout(getContentPane(), BoxLayout.Y_AXIS));

		listeBoutons1 = new ArrayList<BoutonCadre>();
		listeBoutons2 = new ArrayList<BoutonCadre>();

		// Panneau Joueur1
		JPanel joueur1 = new JPanel();
		joueur1.setBackground(Color.WHITE);
		joueur1.setAlignmentY(Component.TOP_ALIGNMENT);
		joueur1.setPreferredSize(new Dimension(100, 300));
		joueur1.setLayout(new GridLayout(0, 1));
		nomLabel = new JLabel("NOM");
		nomLabel.setBackground(Color.WHITE);
		nomLabel.setFont(new Font("SansSerif", Font.PLAIN, 20));
		nomLabel.setVerticalAlignment(SwingConstants.TOP);

		pointsJoueur1 = new JLabel("0");
		pointsJoueur1.setMinimumSize(new Dimension(100, 14));
		pointsJoueur1.setPreferredSize(new Dimension(100, 14));
		pointsJoueur1.setBackground(Color.WHITE);
		pointsJoueur1.setVerticalTextPosition(SwingConstants.TOP);
		pointsJoueur1.setFont(new Font("SansSerif", Font.PLAIN, 90));
		pointsJoueur1.setHorizontalAlignment(SwingConstants.LEFT);

		joueur1.add(pointsJoueur1);
		joueur1.add(nomLabel);

		// Panneau Joueur2
		JPanel joueur2 = new JPanel();
		joueur2.setBackground(Color.WHITE);
		joueur2.setPreferredSize(new Dimension(100, 300));
		joueur2.setLayout(new GridLayout(0, 1));
		nomLabel2 = new JLabel("NOM");
		nomLabel2.setHorizontalAlignment(SwingConstants.CENTER);
		nomLabel2.setHorizontalTextPosition(SwingConstants.CENTER);
		nomLabel2.setBackground(Color.WHITE);
		nomLabel2.setFont(new Font("SansSerif", Font.PLAIN, 20));
		nomLabel2.setVerticalAlignment(SwingConstants.TOP);

		pointsJoueur2 = new JLabel("0");
		pointsJoueur2.setHorizontalTextPosition(SwingConstants.CENTER);
		pointsJoueur2.setBackground(Color.WHITE);
		pointsJoueur2.setFont(new Font("SansSerif", Font.PLAIN, 90));
		pointsJoueur2.setVerticalAlignment(SwingConstants.BOTTOM);
		pointsJoueur2.setHorizontalAlignment(SwingConstants.CENTER);

		joueur2.add(pointsJoueur2);
		joueur2.add(nomLabel2);

		JPanel panelSup = new JPanel();
		panelSup.setBackground(Color.WHITE);
		getContentPane().add(panelSup);
		panelSup.setPreferredSize(new Dimension(200, -150));
		panelSup.add(joueur1);

		JPanel panel = new JPanel();
		panel.setPreferredSize(new Dimension(900, 300));
		panel.add(new JLabel(new ImageIcon("battleship.jpg")));
		panelSup.add(panel);
		panelSup.add(joueur2);

		JPanel panelText = new JPanel();
		panelText.setBackground(Color.WHITE);
		getContentPane().add(panelText);
		messages = new JLabel("C'est Parti!!");
		messages.setFont(new Font("SansSerif", Font.PLAIN, 20));
		panelText.setPreferredSize(new Dimension(200, -325));
		panelText.add(messages);

		// Panneau Central - CADRE
		cadres = new JPanel();
		cadres.setBackground(Color.WHITE);
		cadres.setLayout(new FlowLayout());
		cadreJoueur1 = new JPanel();
		cadreJoueur1.setPreferredSize(new Dimension(450, 450));
		cadreJoueur1.setLayout(new GridLayout(10, 10));
		for (int i = 11; i <= 110; i++) {
			bouton = new BoutonCadre(i);
			listeBoutons1.add(bouton);
			bouton.setEnabled(false);

			cadreJoueur1.add(bouton);
		}
		cadres.setPreferredSize(new Dimension(100, 100));
		cadres.add(cadreJoueur1);

		cadreJoueur2 = new JPanel();
		cadreJoueur2.setMinimumSize(new Dimension(200, 10));
		cadreJoueur2.setPreferredSize(new Dimension(450, 450));
		cadreJoueur2.setLayout(new GridLayout(10, 10));
		// creation des boutons e ajoute des ecouteurs
		ecouteurBouton = new Ecouteur();
		for (int i = 11; i <= 110; i++) {
			bouton2 = new BoutonCadre(i);
			bouton2.setBackground(new Color(230, 230, 230));
			
			listeBoutons2.add(bouton2);
			bouton2.setActionCommand("tirer");
			bouton2.addActionListener(ecouteurBouton);
			cadreJoueur2.add(bouton2);
			/*
			 * bouton2.setIcon(((new ImageIcon(((new ImageIcon(
			 * "mere2.png").getImage() .getScaledInstance(45, 45,
			 * java.awt.Image.SCALE_SMOOTH)))))));
			 */

		}
		cadres.add(cadreJoueur2);
		add(cadres);

		// boutons reset et quitter
		JPanel panelBoutons = new JPanel();
		panelBoutons.setBackground(Color.WHITE);
		panelBoutons.setPreferredSize(new Dimension(10, -320));
		reset = new JButton("Reset");
		reset.setActionCommand("reset");
		reset.addActionListener(ecouteurBouton);
		reset.setBackground(new Color(195, 189, 189));
		reset.setForeground(new Color(75, 75, 75));
		reset.setPreferredSize(new Dimension(120, 40));
		quitter = new JButton("Quitter");
		quitter.setActionCommand("quitter");
		quitter.addActionListener(ecouteurBouton);
		quitter.setBackground(new Color(191, 0, 0));
		quitter.setForeground(new Color(255, 255, 255));
		quitter.setPreferredSize(new Dimension(120, 40));
		panelBoutons.add(reset);
		panelBoutons.add(quitter);
		getContentPane().add(panelBoutons);

		// proprietes du cadre
		setVisible(true);
		setDefaultCloseOperation(EXIT_ON_CLOSE);
		

	}
	public void resetGrilles()
	{
		for(JButton bouton:listeBoutons1 )
			bouton.setBackground(new Color(230, 230, 230));
	
		for(JButton bouton:listeBoutons2 )
			{
				bouton.setBackground(new Color(230, 230, 230));
				bouton.setEnabled(true);
			}
		
		
	}
	

	public void afficherMesBateaux(ArrayList<Integer> liste) {
		
		for (int i : liste)	
			listeBoutons1.get(i - 11).setBackground(new Color(108, 106, 106));
		
	}

	public void afficherNoms(String nom, String nom2) {
		nomLabel.setText(nom);
		nomLabel2.setText(nom2);
	}

	public void afficherStatus(String message) {
		messages.setText(message);
		//System.out.println("dans afficherStatus");
	}

	public void afficherPoints(int pointUn, int pointDeux) {
		pointsJoueur1.setText(String.valueOf(pointUn));
		pointsJoueur2.setText(String.valueOf(pointDeux));

	}

	public void bateauPerdu(int position) {
		listeBoutons1.get(position - 11).setBackground(new Color(0, 0, 0));
	}

	public void eau(int position) {
		listeBoutons1.get(position - 11).setBackground(new Color(57, 170, 210));
		// listeBoutons1.get(position-11).setIcon(((new ImageIcon(((new
		// ImageIcon(
		// "mere2.png").getImage()
		// .getScaledInstance(45, 45,
		// java.awt.Image.SCALE_SMOOTH)))))));
	}

	public void bateauFrappe(int position) {
		
		//ImageIcon img = new ImageIcon("battleship.jpg");
		//listeBoutons2.get(position - 11).setIcon(img);
		listeBoutons2.get(position - 11).setBackground(new Color(220, 0, 0));

	}

	public void eau2(int position) {
		listeBoutons2.get(position - 11).setBackground(new Color(57, 170, 210));
	}

	// classe interne ecouteur
	class Ecouteur implements ActionListener {

		@Override
		public void actionPerformed(ActionEvent e) {

			String commande = e.getActionCommand();

			switch (commande) {
			case "tirer":

				BoutonCadre button = (BoutonCadre) e.getSource();
				int pos = button.getPosition();
				//bateauFrappe(pos);
				leControleur.setCoup(pos);

				button.setEnabled(false);

				break;
			case "reset": leControleur.getJeu().reset();
				break;
			case "quitter":
				System.exit(0);
				break;

			}

		}

	}

}