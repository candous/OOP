package com.battleship.vue;

import javax.swing.BoxLayout;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextField;
import javax.swing.SwingConstants;

import com.battleship.controleur.Controleur;
import com.battleship.modele.Jeu;
import com.battleship.modele.TypeJeu;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.GridLayout;
import java.awt.Dimension;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class Menu extends JFrame {
	
	JTextField nomChamp;
	TypeJeu type;
	boolean jeuChoisi;
	
	//constructeur
	public Menu()
	{
		super("Menu BattleShip");
		jeuChoisi=false;
		setVisible(true);
		setDefaultCloseOperation(EXIT_ON_CLOSE);
		setSize(1200, 800);
		getContentPane().setLayout(new BoxLayout(getContentPane(),BoxLayout.Y_AXIS));
		
		//logo
		JPanel panel = new JPanel();
		panel.setPreferredSize(new Dimension(900, 300));
		panel.add(new JLabel(new ImageIcon("battleship.jpg")));
		getContentPane().add(panel);
		panel.setBackground(Color.WHITE);
		getContentPane().setBackground(Color.WHITE);
		
		
		
		//choix du joueur
		JPanel panelJoueur= new JPanel();
		panelJoueur.setBackground(Color.WHITE);
		panelJoueur.setLayout(new GridLayout(0,1));
		JPanel nomJoueur= new JPanel();
		nomJoueur.setMinimumSize(new Dimension(10, -200));
		nomJoueur.setPreferredSize(new Dimension(10, -150));
		nomJoueur.setBackground(Color.WHITE);
		panelJoueur.add(nomJoueur);
		nomJoueur.setLayout(new FlowLayout());
		JLabel nom= new JLabel("Nom:");
		nom.setFont(new Font("SansSerif", Font.PLAIN, 20));
		nomChamp=new JTextField(15);
		nomJoueur.add(nom);
		nomJoueur.add(nomChamp);
		getContentPane().add(panelJoueur);
		
		//type de jeu
		JPanel typeJeu=new JPanel();
		typeJeu.setPreferredSize(new Dimension(10, -50));
		typeJeu.setMinimumSize(new Dimension(10, -200));
		getContentPane().add(typeJeu);
		typeJeu.setBackground(Color.WHITE);
		JLabel choix = new JLabel("Type de Jeu?");
		choix.setFont(new Font("SansSerif", Font.PLAIN, 20));
		typeJeu.add(choix);
		
		//boutons
		JPanel boutons=new JPanel();
		boutons.setPreferredSize(new Dimension(10, 100));
		getContentPane().add(boutons);
		boutons.setBackground(Color.WHITE);
		JButton bouton1= new JButton("Local");
		JButton bouton2= new JButton("Ordi x Ordi");
		JButton bouton3= new JButton("Reseau");
		bouton1.setActionCommand("local");
		bouton2.setActionCommand("ordi");
		bouton3.setActionCommand("reseau");
		Ecouteur ecouteur=new Ecouteur();
		bouton1.addActionListener(ecouteur);
		bouton2.addActionListener(ecouteur);
		bouton3.addActionListener(ecouteur);
		
		boutons.add(bouton1);
		boutons.add(bouton2);
		boutons.add(bouton3);
		
		

		//groupe
		//JLabel groupe=new JLabel("TP JAVA I - Raphael et Rafael");
		JLabel groupe=new JLabel("");
		groupe.setHorizontalTextPosition(SwingConstants.LEFT);
		groupe.setHorizontalAlignment(SwingConstants.LEFT);
		getContentPane().add(groupe);
		groupe.setFont(new Font("SansSerif", Font.PLAIN, 22));
		
		
		
	}

	public TypeJeu getTypeJeu()
	{
		return type;
	}
	
	public boolean jeuChoisi()
	{
		return jeuChoisi;
	}
	
	public String getNom()
	{
		String nom;
		nom=nomChamp.getText();
		
		if (nomChamp.getText().trim().length()==0)
		nom="Joueur1";
			
		return nom;
	}
	
	class Ecouteur implements ActionListener
	{

		@Override
		public void actionPerformed(ActionEvent e) 
		{
			String command=e.getActionCommand();
			
			Jeu monJeu;
			switch(command)
			{
				case "local":
					type=TypeJeu.LOCAL;
					jeuChoisi=true;
					
					break;
				case "ordi":
					
					type=TypeJeu.LOCALORDIS;
					jeuChoisi=true;
					
					break;
				case "reseau":
					
					type=TypeJeu.RESEAU;
					jeuChoisi=true;
					
					break;
			}
			
			
		}
		
		
	}
	
	
/*	public static void main(String[] args) {
		
		new Menu();

	}*/

}
