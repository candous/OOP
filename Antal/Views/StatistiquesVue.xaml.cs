using Entities;
using ExcelLibrary.SpreadSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL;
using PieControls;
using PieControlsTest;
using System.IO;


namespace Views {
    /// <summary>
    /// Interaction logic for StatistiquesVue.xaml
    /// </summary>
    public partial class StatistiquesVue : Window
    {


        public Utilisateur User {
            get;
            set;
        }
        PieDataCollection<PieSegment> collection1;
        List<string> listFormation = new List<string>();
        Dictionary<string, int> nomValeurGraphique;
        int indexChoisi;

        //pour fichier xls
        // donnée requete statistiques generales        
        List<String> colonneStatGeneral;
        List<String> ligneStatGeneral;
        List<List<int>> donneesStatGeneral;
        // stat par formation
        List<String> colonneStatGeneralParFormation;
        List<String> ligneStatGeneralParFormation;
        List<List<int>> donneesStatGeneralParFormation;
        // stat par annee fin formation
        List<String> colonneStatGeneralParAnneeFinFormation;
        List<String> ligneStatGeneralParAnneeFinFormation;
        List<List<int>> donneesStatGeneralParAnneeFinFormation;
        // donnée requete tx Placement selon delais       
        List<String> colonneTxPlacementSelonDelais;
        List<String> ligneTxPlacementSelonDelais;
        List<List<int>> donneesTxPlacementSelonDelais;
        // donnée requete tx Placement selon delais       
        List<String> colonneTxPlacementSelonAn;
        List<String> ligneTxPlacementSelonAn;
        List<List<int>> donneesTxPlacementSelonAn;
        // donnée requete tx Placement selon delais et Ans       
        List<String> colonneTxPlacementSelonDelaisEtAns;
        List<String> ligneTxPlacementSelonDelaisEtAns;
        List<List<int>> donneesTxPlacementSelonDelaisEtAns;
        // donnée requete tx Placement selon delais et Ans et formation      
        List<String> colonneTxPlacementSelonDelaisEtAnsEtFormation;
        List<String> ligneTxPlacementSelonDelaisEtAnsEtFormation;
        List<List<int>> donneesTxPlacementSelonDelaisEtAnsEtFormation;

        // donnée requete retention en emplois       
        List<String> colonneEntrepriseEmploisApresStage;
        List<String> ligneEntrepriseEmploisApresStage;
        List<List<int>> donneesEntrepriseEmploisApresStage;

        // donnée requete entreprise et salaire      
        List<String> colonneEntrepriseSalaire;
        List<String> ligneEntrepriseSalaire;
        List<List<int>> donneesEntrepriseSalaire;

        // donnée requete entreprise et salaire  et formation     
        List<String> colonneEntrepriseSalaireEtFormation;
        List<String> ligneEntrepriseSalaireEtFormation;
        List<List<int>> donneesEntrepriseSalaireEtFormation;

        private int yearMin;
        private int yearMax;

        private Workbook workbook;
        private string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
        private Worksheet worksheet;


        public StatistiquesVue(Utilisateur user) {
            InitializeComponent();
            
            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;

            User = user;
            
            //permissions
            if (User.IdTypeUtilisateur == 1)
            {
                BtnComptes.Visibility = System.Windows.Visibility.Visible;
                BtnConfigurations.Visibility = System.Windows.Visibility.Visible;
            }
            //ressources humaines
            else if (User.IdTypeUtilisateur == 2)
            {
                BtnComptes.Visibility = System.Windows.Visibility.Hidden;
                BtnConfigurations.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                BtnComptes.Visibility = System.Windows.Visibility.Hidden;
                BtnConfigurations.Visibility = System.Windows.Visibility.Hidden;
                StatistiquesMenu.Visibility = System.Windows.Visibility.Hidden;
            }

            userName.Content = User.Nom;

            //peopler les statistiques initiales
            NbEtudiants.Content = ManagerStatistique.recupererNbEtudiants();
            NbEtudiantsStage.Content = ManagerStatistique.NbTousEtudiantsEnStage();
            NbEtudiantsEmploi.Content = ManagerStatistique.NbTousEtudiantsAvecEmploi();
            NbEtudiantsRecherche.Content = ManagerStatistique.NbTousEtudiantsRecherche();

            int reponse1 = ManagerStatistique.recupererNbEtudiants();
            int reponse2 = ManagerStatistique.NbTousEtudiantsAvecEmploi();
            int reponse3 = ManagerStatistique.NbTousEtudiantsRecherche();
            int reponse4 = ManagerStatistique.NbTousEtudiantsEnStage();
            
            //initialization du dictionnaire pour passer au graphique
            nomValeurGraphique = new Dictionary<string, int>();

            nomValeurGraphique.Add("Nombre d'Etudiants", reponse1);
            nomValeurGraphique.Add("En Emploi", reponse2);
            nomValeurGraphique.Add("A la Recherche", reponse3);
            nomValeurGraphique.Add("En Stage", reponse4);

            PopulateCharts(nomValeurGraphique);

            //peopler les formations
            listFormation = new List<string>();

            foreach (Formation id in ListeDescription.listFormations)
                listFormation.Add(id.Description);
            ChoixFormation.ItemsSource = listFormation;

           

        }

        private void recupererDonneePourXls() {
            workbook = new Workbook();


            // pour fichier xls

            // stat general
            colonneStatGeneral = new List<string> { "", "En Emplois", "Sans Emplois", "A la recherche", "En Stage", " nb etudiant total" };
            ligneStatGeneral = new List<string> { "nb Etudiant" };
            donneesStatGeneral = new List<List<int>>();
            List<int> donne = new List<int> { ManagerStatistique.NbTousEtudiantsAvecEmploi(), ManagerStatistique.NbTousEtudiantsSansEmploi(), ManagerStatistique.NbTousEtudiantsRecherche(), ManagerStatistique.NbTousEtudiantsEnStage(), ManagerStatistique.recupererNbEtudiants() };
            donneesStatGeneral.Add(donne);


            // stat par formation
            colonneStatGeneralParFormation = new List<string> { "", "En Emplois", "Sans Emplois", "A la recherche", "En Stage", " nb etudiant total" };
            ligneStatGeneralParFormation = new List<string>();

            foreach(Formation id in ListeDescription.listFormations)
                ligneStatGeneralParFormation.Add(id.Description);

            donneesStatGeneralParFormation = new List<List<int>>();
            foreach(Formation id in ListeDescription.listFormations) {
                List<int> donne1 = new List<int> { ManagerStatistique.NbEtudiantsAvecEmploiParFormation(id.Id), ManagerStatistique.NbEtudiantsSansEmploiParFormation(id.Id), ManagerStatistique.NbEtudiantsRechercheParFormation(id.Id), ManagerStatistique.NbEtudiantsEnStageParFormation(id.Id), ManagerStatistique.NbEtudiantsParFormation(id.Id) };
                donneesStatGeneralParFormation.Add(donne1);
            }


            // stat par Anne de fin de formation
            colonneStatGeneralParAnneeFinFormation = new List<string> { "Année", "En Emplois", "A la recherche", "En Stage", " nb etudiant total" };
            ligneStatGeneralParAnneeFinFormation = new List<string>();
            yearMin = ManagerStatistique.anneeEtudiantPlusAncien();
            yearMax = DateTime.Now.Year;
            int differenceYear;
            if(yearMax - yearMin != 0)

                differenceYear = yearMax - yearMin;
            else
                differenceYear = 1;

            //MessageBox.Show("test " + differenceYear + " " + yearMin + yearMax, "test", MessageBoxButton.OK);

            donneesStatGeneralParAnneeFinFormation = new List<List<int>>();
            for(int i = 0; i <= differenceYear; i++) {

                int nbEtudiant = ManagerStatistique.NbEtudiantsParAnDeFormation(yearMin + i);

                if(nbEtudiant != 0) {
                    List<int> donne2 = new List<int> { ManagerStatistique.NbEtudiantsEnEmploiParAnFormation(yearMin + i), ManagerStatistique.NbEtudiantsEnRechercheParAnFormation(yearMin + i), ManagerStatistique.NbEtudiantsEnStageParAnFormation(yearMin + i), ManagerStatistique.NbEtudiantsParAnDeFormation(yearMin + i) };
                    ligneStatGeneralParAnneeFinFormation.Add((i + yearMin).ToString());
                    donneesStatGeneralParAnneeFinFormation.Add(donne2);
                }

            }

            // Taux placement selon delais
            colonneTxPlacementSelonDelais = new List<string> { "", "5 jours", "30 jours", " superieur a 30 jours", " nb etudiant total" };
            ligneTxPlacementSelonDelais = new List<string> { "nb Etudiant" };
            donneesTxPlacementSelonDelais = new List<List<int>>();

            int jour5 = ManagerStatistique.recupererNbEtudiantsPlacesAvantDelais(5);
            int jour30 = ManagerStatistique.recupererNbEtudiantsPlacesAvantDelais(30) - jour5;
            int supJour30 = ManagerStatistique.recupererNbEtudiantsPlacesApresDelais(30);

            List<int> donne3 = new List<int> { jour5, jour30, supJour30, ManagerStatistique.recupererNbEtudiants() };
            donneesTxPlacementSelonDelais.Add(donne3);



            // Taux placement selon An
            // int nbEtudiantTot = 0;
            colonneTxPlacementSelonAn = new List<string> { "Année", "Placé", "jamaisPlacé", " nb etudiant total" };
            ligneTxPlacementSelonAn = new List<string>();
            donneesTxPlacementSelonAn = new List<List<int>>();
            for(int i = 0; i <= differenceYear; i++) {

                int nbEtudiant = ManagerStatistique.recupererNbEtudiantsParAn(yearMin + i);


                if(nbEtudiant != 0) {

                    jour5 = ManagerStatistique.recupererNbEtudiantsPlacesParAn(yearMin + i);
                    jour30 = ManagerStatistique.recupererNbEtudiantsJamaisPlacesParAnFinFormation(yearMin + i);


                    List<int> donne4 = new List<int> { jour5, jour30, nbEtudiant };
                    donneesTxPlacementSelonAn.Add(donne4);
                    ligneTxPlacementSelonAn.Add((i + yearMin).ToString());

                }
                //  nbEtudiantTot += nbEtudiant;
            }


            // Taux placement selon delais et Ans
            colonneTxPlacementSelonDelaisEtAns = new List<string> { "Année", "5 jours", "30 jours", " superieur a 30 jours", " nb etudiant total" };
            ligneTxPlacementSelonDelaisEtAns = new List<string>();
            donneesTxPlacementSelonDelaisEtAns = new List<List<int>>();
            for(int i = 0; i <= differenceYear; i++) {

                int nbEtudiant = ManagerStatistique.recupererNbEtudiantsParAn(yearMin + i);
                //  MessageBox.Show("test " + nbEtudiant, "test", MessageBoxButton.OK);
                if(nbEtudiant != 0) {

                    jour5 = ManagerStatistique.recupererNbEtudiantsPlacesAvantDelaisParAn(5, yearMin + i);
                    jour30 = ManagerStatistique.recupererNbEtudiantsPlacesAvantDelaisParAn(30, yearMin + i) - jour5;
                    supJour30 = ManagerStatistique.recupererNbEtudiantsPlacesApresDelaisParAn(30,yearMin + i);

                    List<int> donne5 = new List<int> { jour5, jour30, supJour30, nbEtudiant };
                    donneesTxPlacementSelonDelaisEtAns.Add(donne5);
                    ligneTxPlacementSelonDelaisEtAns.Add((i + yearMin).ToString());

                }

            }




            // Taux retenu en emplois apres le stage
            colonneEntrepriseEmploisApresStage = new List<string> { "Année", "place en stage", "retenue en emplois", " pas retenue en emplois" };
            ligneEntrepriseEmploisApresStage = new List<string>();
            donneesEntrepriseEmploisApresStage = new List<List<int>>();
            for(int i = 0; i <= differenceYear; i++) {

                int nbEtudiant = ManagerStatistique.NbEtudiantsEnStageParAn(yearMin + i);
                //  MessageBox.Show("test " + nbEtudiant, "test", MessageBoxButton.OK);
                if(nbEtudiant != 0) {


                    int nbEnEmplois = ManagerStatistique.NbEtudiantsRetenusParAn(yearMin + i);
                    int nbPasEmplois = nbEtudiant - nbEnEmplois;

                    List<int> donne5 = new List<int> { nbEtudiant, nbEnEmplois, nbPasEmplois };
                    donneesEntrepriseEmploisApresStage.Add(donne5);
                    ligneEntrepriseEmploisApresStage.Add((i + yearMin).ToString());

                }

            }



            // Taux Enetreprise salaire
            colonneEntrepriseSalaire = new List<string> { " ", "Entreprise avec salaire", "Entreprise sans salaire", " Entreprise total" };
            ligneEntrepriseSalaire = new List<string>();
            donneesEntrepriseSalaire = new List<List<int>>();
            ligneEntrepriseSalaire.Add("nb Entreprise");

            int nbSansSalaire = ManagerStatistique.NbEntreprisesSansSalaire();
            int nbAvecSalaire = ManagerStatistique.NbEntreprisesAvecSalaire();
            int nbEntreprise = nbSansSalaire + nbAvecSalaire;

            List<int> donne6 = new List<int> { nbAvecSalaire, nbSansSalaire, nbEntreprise };
            donneesEntrepriseSalaire.Add(donne6);


            // salaire par formation
            colonneEntrepriseSalaireEtFormation = new List<string> { " ", "Entreprise avec salaire", "Entreprise sans salaire", " Entreprise total" };
            ligneEntrepriseSalaireEtFormation = new List<string>();

            foreach(Formation id in ListeDescription.listFormations)
                ligneEntrepriseSalaireEtFormation.Add(id.Description);

            donneesEntrepriseSalaireEtFormation = new List<List<int>>();
            foreach(Formation id in ListeDescription.listFormations) {

                int nbSansSalaire1 = ManagerStatistique.NbEntreprisesSansSalaireParFormation(id.Id);
                int nbAvecSalaire1 = ManagerStatistique.NbEntreprisesAvecSalaireParFormation(id.Id);
                int nbEntreprise1 = nbSansSalaire1 + nbAvecSalaire1;

                List<int> donne1 = new List<int> { nbAvecSalaire1, nbSansSalaire1, nbEntreprise1 };
                donneesEntrepriseSalaireEtFormation.Add(donne1);
            }




        }

        public void DonneeTxPlacementSelonDelaisEtAnsEtFormation(int idFormation) {
            // Taux placement selon delais et annee et formation

            colonneTxPlacementSelonDelaisEtAnsEtFormation = new List<string> { "Année", "5 jours", "30 jours", " superieur a 30 jours", " nb etudiant total" };
            ligneTxPlacementSelonDelaisEtAnsEtFormation = new List<string>();
            donneesTxPlacementSelonDelaisEtAnsEtFormation = new List<List<int>>();

            int differenceYear;
            if(yearMax - yearMin != 0)

                differenceYear = yearMax - yearMin;
            else
                differenceYear = 1;

            for(int i = 0; i <= differenceYear; i++) {

                int nbEtudiant = ManagerStatistique.recupererNbEtudiantsJamaisPlacesParAnParFormation(yearMin + i, idFormation);
                //  MessageBox.Show("test " + nbEtudiant, "test", MessageBoxButton.OK);
                if(nbEtudiant != 0) {

                    int jour5 = ManagerStatistique.recupererNbEtudiantsPlacesAvantDelaisParAnParFormation(5, yearMin + i, idFormation);
                    int jour30 = ManagerStatistique.recupererNbEtudiantsPlacesAvantDelaisParAnParFormation(30, yearMin + i, idFormation) - jour5;
                    int supJour30 = ManagerStatistique.recupererNbEtudiantsPlacesApresDelaisParAnParFormation(30,yearMin + i, idFormation);

                    List<int> donne5 = new List<int> { jour5, jour30, supJour30, nbEtudiant };
                    donneesTxPlacementSelonDelaisEtAnsEtFormation.Add(donne5);
                    ligneTxPlacementSelonDelaisEtAnsEtFormation.Add((i + yearMin).ToString());

                }

            }

        }

        // generation du fichier XLS
        public void genererTableauXLS(List<String> colonnes, List<String> champs, List<List<int>> donnees, int decallageX, int decallageY) {
            //generation texte colonne et ligne
            for(int i = 0; i < colonnes.Count; i++) {
                worksheet.Cells[0 + decallageX, i + decallageY] = new Cell(colonnes[i]);
                for(int j = 0; j < donnees.Count; j++)
                    worksheet.Cells[j + 1 + decallageX, 0 + decallageY] = new Cell(champs[j]);
            }
            // generation des donnéés
            for(int i = 0; i < donnees.Count; i++)
                for(int j = 0; j < donnees[i].Count; j++)
                    worksheet.Cells[i + 1 + decallageX, j + 1 + decallageY] = new Cell(donnees[i][j]);
        }


        private void saveXls() {
            string nouveauRep = DefinitionConnection.DocumentFolder+"\\statistiques\\";
            if(!Directory.Exists(nouveauRep)) {
                Directory.CreateDirectory(nouveauRep);
            }

            nouveauRep+=  fileName;
            workbook.Save(nouveauRep);
        }

        private void createWorksheet(string sheet) {
            worksheet = new Worksheet(sheet);

        }

        private void addWorkSeet() {
            workbook.Worksheets.Add(worksheet);
        }

        public void ecrireCellule(int X, int Y, string text) {
            worksheet.Cells[X, Y] = new Cell(text);

        }

        private void btnSauvegarde_Click(object sender, RoutedEventArgs e) {
            recupererDonneePourXls();
            int decallage = 1;

            // Feuille 1 stat general
            createWorksheet("Statistiques generales");
            ecrireCellule(0, 2, "Statistiques generale de l ecole");
            ecrireCellule(0, 0, DateTime.Now.ToShortDateString());
            genererTableauXLS(colonneStatGeneral, ligneStatGeneral, donneesStatGeneral, decallage, 2);

            decallage += donneesStatGeneral.Count + 4;
            ecrireCellule(decallage, 2, "Statistiques generale par Formation");
            genererTableauXLS(colonneStatGeneralParFormation, ligneStatGeneralParFormation, donneesStatGeneralParFormation, decallage + 1, 2);

            decallage += donneesStatGeneralParFormation.Count + 4;
            ecrireCellule(decallage, 2, "Statistiques generale par An");
            genererTableauXLS(colonneStatGeneralParAnneeFinFormation, ligneStatGeneralParAnneeFinFormation, donneesStatGeneralParAnneeFinFormation, decallage + 1, 2);

            addWorkSeet();

            // Feuille 2 taux placement
            createWorksheet("Taux de placement en Stage");
            ecrireCellule(0, 0, DateTime.Now.ToShortDateString());
            decallage = 1;
            ecrireCellule(0, 2, "Taux de placement en stage selon delais");
            genererTableauXLS(colonneTxPlacementSelonDelais, ligneTxPlacementSelonDelais, donneesTxPlacementSelonDelais, decallage + 1, 2);

            decallage += donneesTxPlacementSelonDelais.Count + 4;
            ecrireCellule(decallage, 2, "Taux de placement en stage selon Annee");
            genererTableauXLS(colonneTxPlacementSelonAn, ligneTxPlacementSelonAn, donneesTxPlacementSelonAn, decallage + 1, 2);

            decallage += donneesTxPlacementSelonAn.Count + 4;
            ecrireCellule(decallage, 2, "Taux de placement en stage selon Annee et delais");
            genererTableauXLS(colonneTxPlacementSelonDelaisEtAns, ligneTxPlacementSelonDelaisEtAns, donneesTxPlacementSelonDelaisEtAns, decallage + 1, 2);


            decallage += donneesTxPlacementSelonDelaisEtAns.Count + 4;
            ecrireCellule(decallage - 1, 2, "Taux de placement en stage selon Annee formation et delais");
            foreach(Formation id in ListeDescription.listFormations) {
                DonneeTxPlacementSelonDelaisEtAnsEtFormation(id.Id);
                ecrireCellule(decallage, 2, id.Description);
                genererTableauXLS(colonneTxPlacementSelonDelaisEtAnsEtFormation, ligneTxPlacementSelonDelaisEtAnsEtFormation, donneesTxPlacementSelonDelaisEtAnsEtFormation, decallage + 1, 2);
                decallage += donneesTxPlacementSelonDelaisEtAnsEtFormation.Count + 4;
            }

            addWorkSeet();


            //Feuille 3 entreprise
            createWorksheet("Entreprise");
            decallage = 1;
            ecrireCellule(0, 2, "Nb etudiant en emplois pas en emplois apres stage");
            genererTableauXLS(colonneEntrepriseEmploisApresStage, ligneEntrepriseEmploisApresStage, donneesEntrepriseEmploisApresStage, decallage + 1, 2);

            decallage += donneesEntrepriseEmploisApresStage.Count + 4;
            ecrireCellule(decallage, 2, "Nb Entreprise avec et sans salaire");
            genererTableauXLS(colonneEntrepriseSalaire, ligneEntrepriseSalaire, donneesEntrepriseSalaire, decallage + 1, 2);

            decallage += donneesEntrepriseSalaire.Count + 4;
            ecrireCellule(decallage, 2, "Salaire ou pas par formation");
            genererTableauXLS(colonneEntrepriseSalaireEtFormation, ligneEntrepriseSalaireEtFormation, donneesEntrepriseSalaireEtFormation, decallage + 1, 2);

            addWorkSeet();
            // enregister fichier
            saveXls();

        }



        private void BtnValiderConnection_Click(object sender, RoutedEventArgs e) {
            stackGrafic.Visibility = Visibility.Visible;           
            btnSauvegarde.Visibility = Visibility.Visible;

            //vider le graphique pour placer les bonnees valeurs
            nomValeurGraphique.Clear();

            int IdFormation=0;
            int jours = 0;
            int annee = 0;
            int anneeFin=0;
            int anneePlacement = 0;
            int reponse1=0;
            int reponse2=0;
            int reponse3=0;
            int reponse4=0;
            int reponse5=0;
            bool parse1=false;
            bool parse2=false;
            switch (indexChoisi)
            {
                case 0:
                    parse1 = Int32.TryParse(panel1txt.Text, out jours);
                    if (panel1txt.Text != "" && parse1)
                    {
                        cacherMessageErreur();
                        jours = int.Parse(panel1txt.Text);

                        reponse1 = ManagerStatistique.recupererNbEtudiants(); //total d'etudiants
                        reponse2 = ManagerStatistique.recupererNbEtudiantsPlacesAvantDelais(jours);//places avant les jours
                        reponse3 = ManagerStatistique.recupererNbEtudiantsPlacesApresDelais(jours); // places apres
                        reponse4 = ManagerStatistique.recupererNbEtudiantsPasPlaces();//pas places

                        //ajouter les reponses avec les legendes dans le dictionaire
                        nomValeurGraphique.Add("Total d'etudiants", reponse1);
                        nomValeurGraphique.Add("Places avant le delais", reponse2);
                        nomValeurGraphique.Add("Places apres le delais", reponse3);
                        nomValeurGraphique.Add("Etudiants jamais places", reponse4);
                    }
                    else
                        afficherMessageErreur();
                    break;
                case 1:
                    parse1 = Int32.TryParse(panel1txt.Text, out jours);
                    parse2 = Int32.TryParse(panel4txt.Text, out anneeFin);

                    if (panel1txt.Text != "" && panel4txt.Text != "" && parse1 && parse2) 
                    {
                        jours = int.Parse(panel1txt.Text);
                        anneeFin = int.Parse(panel4txt.Text);
                        cacherMessageErreur();

                        reponse1 = ManagerStatistique.recupererNbEtudiantsParAn(anneeFin);
                        reponse2 = ManagerStatistique.recupererNbEtudiantsPlacesAvantDelaisParAn(jours, anneeFin);
                        reponse3 = ManagerStatistique.recupererNbEtudiantsPlacesApresDelaisParAn(jours, anneeFin);
                        reponse4 = ManagerStatistique.recupererNbEtudiantsJamaisPlacesParAn(anneeFin);

                        nomValeurGraphique.Add("Finissants en " + anneeFin, reponse1);
                        nomValeurGraphique.Add("Places avant le delais en " + anneeFin, reponse2);
                        nomValeurGraphique.Add("Places apres le delais en " + anneeFin, reponse3);
                        nomValeurGraphique.Add("Finissants en " + anneeFin + " jamais places.", reponse4);
                    }
                    else
                        afficherMessageErreur();
                    break;
                case 2:
                    parse1 = Int32.TryParse(panel2txt.Text, out annee);

                    if (panel2txt.Text != "" && parse1)
                    {
                        annee = int.Parse(panel2txt.Text);
                        cacherMessageErreur();
                        reponse1 = ManagerStatistique.recupererNbEtudiantsParAn(annee);
                        reponse2 = ManagerStatistique.recupererNbEtudiantsPlacesParAn(annee);
                        reponse3 = ManagerStatistique.recupererNbEtudiantsJamaisPlacesParAnFinFormation(annee);

                        nomValeurGraphique.Add("Finissants en " + annee, reponse1);
                        nomValeurGraphique.Add("Etudiants places en " + annee, reponse2);
                        nomValeurGraphique.Add("Finissants en " + annee + "pas places en " + annee, reponse3);
                    }
                    else
                        afficherMessageErreur();
                    break;

                case 3:
                    
                    //retour de l'id de la formation choisie
                    if (ChoixFormation.SelectedValue != null && !ChoixFormation.SelectedValue.ToString().Equals(""))
                    {
                        IdFormation = ListeDescription.recupererIdFormation(ChoixFormation.SelectedValue.ToString());
                        cacherMessageErreur();
                        reponse1 = ManagerStatistique.NbEtudiantsParFormation(IdFormation);
                        reponse2 = ManagerStatistique.NbEtudiantsPlacesStageParFormation(IdFormation);
                        reponse3 = reponse1 - reponse2;

                        nomValeurGraphique.Add("Etudiants en " + ChoixFormation.Text, reponse1);
                        nomValeurGraphique.Add("Etudiants Places", reponse2);
                        nomValeurGraphique.Add("Etudiants pas Places", reponse3);
                    }
                    else
                        afficherMessageErreur();
                    break;
                case 4:
                    parse1 = Int32.TryParse(panel4txt.Text, out anneeFin);
                    if (ChoixFormation.SelectedValue != null && !ChoixFormation.SelectedValue.ToString().Equals("") && panel4txt.Text != "" && parse1)
                    {
                        //taux d'emplacement en stage par formation et par an
                        anneeFin = int.Parse(panel4txt.Text);
                        IdFormation = ListeDescription.recupererIdFormation(ChoixFormation.SelectedValue.ToString());
                        cacherMessageErreur();
                        reponse1 = ManagerStatistique.NbEtudiantsParFormationEtAn(IdFormation, anneeFin);
                        reponse2 = ManagerStatistique.NbEtudiantsPlacesStageParFormationEtAn(IdFormation, anneeFin);
                        reponse3 = reponse1 - reponse2;

                        nomValeurGraphique.Add("Finissant en " + ChoixFormation.Text + " en " + anneeFin, reponse1);
                        nomValeurGraphique.Add("Places en " + anneeFin, reponse2);
                        nomValeurGraphique.Add("Pas Places en " + anneeFin, reponse3);
                    }
                    else
                        afficherMessageErreur();
                    break;
                case 5:
                    parse1 = Int32.TryParse(panel4txt.Text, out anneeFin);
                    parse2 = Int32.TryParse(panel5txt.Text, out anneePlacement);
                    if (panel4txt.Text != "" && panel5txt.Text != "" && parse1 && parse2)
                    {
                        //nb etudiants placés en stage par an et date de fin de formation
                        anneeFin = int.Parse(panel4txt.Text);
                        anneePlacement = int.Parse(panel5txt.Text);
                        cacherMessageErreur();
                        reponse1 = ManagerStatistique.recupererNbEtudiantsParAn(anneeFin);//finissants en anneeFin
                        reponse2 = ManagerStatistique.NbEtudiantsSansStageParAnEtFinFormation(anneeFin, anneePlacement);//pas places en anneePlacement
                        reponse3 = reponse1 - reponse2;

                        nomValeurGraphique.Add("Finissant en " + anneeFin, reponse1);
                        nomValeurGraphique.Add("Pas Places en " + anneePlacement, reponse2);
                        nomValeurGraphique.Add("Places en " + anneePlacement, reponse3);
                    }
                    else
                        afficherMessageErreur();
                    break;
                case 6:
                    parse1 = Int32.TryParse(panel5txt.Text, out anneePlacement);
                    if (panel5txt.Text != "" && parse1)
                    {
                        //nb etudiant en emplois par an selon suite stage (retenus)
                        anneePlacement = int.Parse(panel5txt.Text);
                        cacherMessageErreur();
                        reponse1 = ManagerStatistique.NbEtudiantsEnStageParAn(anneePlacement);
                        reponse2 = ManagerStatistique.NbEtudiantsRetenusParAn(anneePlacement);
                        reponse3 = reponse1 - reponse2;

                        nomValeurGraphique.Add("Etudiants places en stage en" + anneePlacement, reponse1);
                        nomValeurGraphique.Add("Etudiants retenus places en " + anneePlacement, reponse2);
                        nomValeurGraphique.Add("Etudiants pas retenus places en " + anneePlacement, reponse3);
                    }
                    else
                        afficherMessageErreur();
                    break;
                case 7:
                    //NbEtudiantsRetenusParAnEtFormation
                    parse1 = Int32.TryParse(panel4txt.Text, out anneeFin);
                    if (ChoixFormation.SelectedValue != null && !ChoixFormation.SelectedValue.ToString().Equals("") && panel4txt.Text != "" && parse1)
                    {
                        IdFormation = ListeDescription.recupererIdFormation(ChoixFormation.SelectedValue.ToString());
                        anneeFin = int.Parse(panel4txt.Text);
                        cacherMessageErreur();
                        reponse1 = ManagerStatistique.NbEtudiantParAnEtFormation(anneeFin, IdFormation);//finissant en formation en annee
                        reponse2 = ManagerStatistique.NbEtudiantsRetenusParAnEtFormation(anneeFin, IdFormation);//retenus en formation finissants en annee
                        reponse3 = reponse1 - reponse2;//le reste

                        nomValeurGraphique.Add("Finissants en " + anneeFin + " en " + ChoixFormation.SelectedValue.ToString(), reponse1);
                        nomValeurGraphique.Add("Retenus " + anneeFin, reponse2);
                        nomValeurGraphique.Add("Pas Retenus ou Pas de stage en" + anneeFin, reponse3);
                    }
                    else
                        afficherMessageErreur();

                    break;
                case 8:
                    parse1 = Int32.TryParse(panel4txt.Text, out anneeFin);
                    if (panel4txt.Text != "" && parse1)
                    {
                        cacherMessageErreur();
                        //Nb Etudiants En Emploi Par An de Formation
                        anneeFin = int.Parse(panel4txt.Text);
                        reponse1 = ManagerStatistique.NbEtudiantsParAnDeFormation(anneeFin);
                        reponse2 = ManagerStatistique.NbEtudiantsEnEmploiParAnFormation(anneeFin);
                        reponse3 = reponse1 - reponse2;//le reste

                        nomValeurGraphique.Add("Finissants en" + anneeFin, reponse1);
                        nomValeurGraphique.Add("Employes ", reponse2);
                        nomValeurGraphique.Add("Plas Employes", reponse3);
                    }
                    else
                        afficherMessageErreur();
                    break;
                case 9:
                    parse1 = Int32.TryParse(panel4txt.Text, out anneeFin);
                    if (panel4txt.Text != "" && parse1)
                    {
                        cacherMessageErreur();
                        //Statistiques par AN de formation
                        anneeFin = int.Parse(panel4txt.Text);
                        reponse1 = ManagerStatistique.NbEtudiantsParAnDeFormation(anneeFin);
                        reponse2 = ManagerStatistique.NbEtudiantsEnEmploiParAnFormation(anneeFin);
                        reponse3 = ManagerStatistique.NbEtudiantsEnRechercheParAnFormation(anneeFin);
                        reponse4 = ManagerStatistique.NbEtudiantsEnStageParAnFormation(anneeFin);

                        nomValeurGraphique.Add("Finissants en" + anneeFin, reponse1);
                        nomValeurGraphique.Add("En Emploi ", reponse2);
                        nomValeurGraphique.Add("A la Recherche", reponse3);
                        nomValeurGraphique.Add("En Stage", reponse4);
                    }
                    else
                        afficherMessageErreur();
                    break;
                case 10:
                    cacherMessageErreur();
                    //statistiques generales
                    //ca donne pas 100% pcqu'il y a des etudiants sans emplois qui sont dans recherche ou stage!!!
                    reponse1 = ManagerStatistique.recupererNbEtudiants();
                    reponse2 = ManagerStatistique.NbTousEtudiantsAvecEmploi();
                    reponse3 = ManagerStatistique.NbTousEtudiantsSansEmploi();
                    reponse4 = ManagerStatistique.NbTousEtudiantsRecherche();
                    reponse5 = ManagerStatistique.NbTousEtudiantsEnStage();
            
                    nomValeurGraphique.Add("Nombre d'Etudiants", reponse1);
                    nomValeurGraphique.Add("En Emploi ", reponse2);
                    nomValeurGraphique.Add("Sans Emploi ", reponse3);
                    nomValeurGraphique.Add("A la Recherche", reponse4);
                    nomValeurGraphique.Add("En Stage", reponse5);
                    break;
                case 11:
                    //statistiques generales par formation
                    //ca donne pas 100% pcqu'il y a des etudiants sans emplois qui sont dans recherche ou stage!!!
                    
                    if (ChoixFormation.SelectedValue != null && !ChoixFormation.SelectedValue.ToString().Equals(""))
                    {
                        cacherMessageErreur();
                        IdFormation = ListeDescription.recupererIdFormation(ChoixFormation.SelectedValue.ToString());
                        reponse1 = ManagerStatistique.NbEtudiantsParFormation(IdFormation);
                        reponse2 = ManagerStatistique.NbEtudiantsAvecEmploiParFormation(IdFormation);
                        reponse3 = ManagerStatistique.NbEtudiantsSansEmploiParFormation(IdFormation);
                        reponse4 = ManagerStatistique.NbEtudiantsRechercheParFormation(IdFormation);
                        reponse5 = ManagerStatistique.NbEtudiantsEnStageParFormation(IdFormation);

                        nomValeurGraphique.Add("Nombre d'Etudiants en " + ChoixFormation.SelectedValue.ToString(), reponse1);
                        nomValeurGraphique.Add("En Emploi ", reponse2);
                        nomValeurGraphique.Add("Sans Emploi ", reponse3);
                        nomValeurGraphique.Add("A la Recherche", reponse4);
                        nomValeurGraphique.Add("En Stage", reponse5);
                    }
                    else
                        afficherMessageErreur();

                    break;
                case 12:
                    cacherMessageErreur();
                    reponse2 = ManagerStatistique.NbEntreprisesAvecSalaire();
                    reponse3 = ManagerStatistique.NbEntreprisesSansSalaire();
                    reponse1 = reponse3 + reponse2;

                    nomValeurGraphique.Add("Entreprises avec stages", reponse1);
                    nomValeurGraphique.Add("Avec Salaire ", reponse2);
                    nomValeurGraphique.Add("Sans Salaire ", reponse3);

                    break;
                case 13:
                    if (ChoixFormation.SelectedValue != null && !ChoixFormation.SelectedValue.ToString().Equals(""))
                    {
                        cacherMessageErreur();
                        IdFormation = ListeDescription.recupererIdFormation(ChoixFormation.SelectedValue.ToString());
                        reponse2 = ManagerStatistique.NbEntreprisesAvecSalaireParFormation(IdFormation);
                        reponse3 = ManagerStatistique.NbEntreprisesSansSalaireParFormation(IdFormation);
                        reponse1 = reponse3 + reponse2;

                        nomValeurGraphique.Add("Entreprises avec stages en " + ChoixFormation.SelectedValue.ToString(), reponse1);
                        nomValeurGraphique.Add("Avec Salaire ", reponse2);
                        nomValeurGraphique.Add("Sans Salaire ", reponse3);
                    }
                    else
                        afficherMessageErreur();

                    //MessageBox.Show(reponse1.ToString() + " " + reponse2.ToString() + " " + reponse3.ToString() + " " + reponse4.ToString() + " " + reponse5.ToString());
                    break;
            }
            //passer le dictionaire en parametre pour l'affichage en graphique
            PopulateCharts(nomValeurGraphique);

        }
        private void afficherMessageErreur()
        {
            message.Text = "Saisir tous les champs";
            message.Visibility = Visibility.Visible;
        }
        private void cacherMessageErreur()
        {
            message.Visibility = Visibility.Hidden;
        }
       
        private void ChoixStatutsResidenceVue_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            indexChoisi = Combo.SelectedIndex;
            cacherMessageErreur();
            switch (indexChoisi)
            {
                case 0:
                    panel1.Visibility = Visibility.Visible;
                    panel2.Visibility = Visibility.Collapsed;
                    panel3.Visibility = Visibility.Collapsed;
                    panel4.Visibility = Visibility.Collapsed;
                    panel5.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    panel1.Visibility = Visibility.Visible;
                    panel2.Visibility = Visibility.Collapsed;
                    panel3.Visibility = Visibility.Collapsed;
                    panel4.Visibility = Visibility.Visible;
                    panel5.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    panel1.Visibility = Visibility.Collapsed;
                    panel2.Visibility = Visibility.Visible;
                    panel3.Visibility = Visibility.Collapsed;
                    panel4.Visibility = Visibility.Collapsed;
                    panel5.Visibility = Visibility.Collapsed;
                    break;

                case 3:
                    panel1.Visibility = Visibility.Collapsed;
                    panel2.Visibility = Visibility.Collapsed;
                    panel3.Visibility = Visibility.Visible;
                    panel4.Visibility = Visibility.Collapsed;
                    panel5.Visibility = Visibility.Collapsed;
                    break;
                case 4:
                    panel1.Visibility = Visibility.Collapsed;
                    panel2.Visibility = Visibility.Collapsed;
                    panel3.Visibility = Visibility.Visible;
                    panel4.Visibility = Visibility.Visible;
                    panel5.Visibility = Visibility.Collapsed;
                    break;
                case 5:
                    panel1.Visibility = Visibility.Collapsed;
                    panel2.Visibility = Visibility.Collapsed;
                    panel3.Visibility = Visibility.Collapsed;
                    panel4.Visibility = Visibility.Visible;
                    panel5.Visibility = Visibility.Visible;
                    break;
                case 6:
                    panel1.Visibility = Visibility.Collapsed;
                    panel2.Visibility = Visibility.Collapsed;
                    panel3.Visibility = Visibility.Collapsed;
                    panel4.Visibility = Visibility.Collapsed;
                    panel5.Visibility = Visibility.Visible;
                    break;
                case 7:
                    panel1.Visibility = Visibility.Collapsed;
                    panel2.Visibility = Visibility.Collapsed;
                    panel3.Visibility = Visibility.Visible;
                    panel4.Visibility = Visibility.Visible;
                    panel5.Visibility = Visibility.Collapsed;
                    break;
                case 8:
                    panel1.Visibility = Visibility.Collapsed;
                    panel2.Visibility = Visibility.Collapsed;
                    panel3.Visibility = Visibility.Collapsed;
                    panel4.Visibility = Visibility.Visible;
                    panel5.Visibility = Visibility.Collapsed;
                    break;
                case 9:
                    panel1.Visibility = Visibility.Collapsed;
                    panel2.Visibility = Visibility.Collapsed;
                    panel3.Visibility = Visibility.Collapsed;
                    panel4.Visibility = Visibility.Visible;
                    panel5.Visibility = Visibility.Collapsed;
                    break;
                case 10:
                    panel1.Visibility = Visibility.Collapsed;
                    panel2.Visibility = Visibility.Collapsed;
                    panel3.Visibility = Visibility.Collapsed;
                    panel4.Visibility = Visibility.Collapsed;
                    panel5.Visibility = Visibility.Collapsed;
                    break;
                case 11:
                    panel1.Visibility = Visibility.Collapsed;
                    panel2.Visibility = Visibility.Collapsed;
                    panel3.Visibility = Visibility.Visible;
                    panel4.Visibility = Visibility.Collapsed;
                    panel5.Visibility = Visibility.Collapsed;
                    break;
                case 12:
                    panel1.Visibility = Visibility.Collapsed;
                    panel2.Visibility = Visibility.Collapsed;
                    panel3.Visibility = Visibility.Collapsed;
                    panel4.Visibility = Visibility.Collapsed;
                    panel5.Visibility = Visibility.Collapsed;
                    break;
                case 13:
                    panel1.Visibility = Visibility.Collapsed;
                    panel2.Visibility = Visibility.Collapsed;
                    panel3.Visibility = Visibility.Visible;
                    panel4.Visibility = Visibility.Collapsed;
                    panel5.Visibility = Visibility.Collapsed;
                    break;
                default:
                    panel1.Visibility = Visibility.Collapsed;
                    panel2.Visibility = Visibility.Collapsed;
                    panel3.Visibility = Visibility.Collapsed;
                    panel4.Visibility = Visibility.Collapsed;
                    panel5.Visibility = Visibility.Collapsed;
                    break;

            }
           

        }   

     
        private void EntrepriseMenu_Click(object sender, RoutedEventArgs e)
        {
            
            ListerEntreprisesVue listerentreprise = new ListerEntreprisesVue(User);
            listerentreprise.Show();
            this.Close();
        }

        private void EtudiantMenu_Click(object sender, RoutedEventArgs e)
        {
            
            ListerEtudiantsVue listeretudiant = new ListerEtudiantsVue(User);
            listeretudiant.Show();
            this.Close();
        }

        private void CommunicationMenu_Click(object sender, RoutedEventArgs e)
        {
           
            ListerCommunicationsVue communication = new ListerCommunicationsVue(User);
            communication.Show();
            this.Close();
        }

        private void StageMenu_Click(object sender, RoutedEventArgs e)
        {
            
            ListerStagesVue listerStages = new ListerStagesVue(User);
            listerStages.Show();
            this.Close();
        }

        private void StatistiquesMenu_Click(object sender, RoutedEventArgs e)
        {
            StatistiquesVue vue = new StatistiquesVue(User);
            vue.Show();
            this.Close();
        }

        private void BtnComptes_Click(object sender, RoutedEventArgs e)
        {
            
            CompteVue gestioncomptevue = new CompteVue(User);
            gestioncomptevue.Show();
            this.Close();
        }

        private void BtnConfigurations_Click(object sender, RoutedEventArgs e)
        {
            
            ConfigurationVue configvue = new ConfigurationVue(User);
            configvue.Show();
            this.Close();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Acceuil vue = new Acceuil(User);
            vue.Show();
            this.Close();
        }
        public void PopulateCharts(Dictionary<string, int> listeTest)
        {
            List<Color> ListeCouleurs = new List<Color>();
            ListeCouleurs.Add(Colors.Blue);
            ListeCouleurs.Add(Colors.CadetBlue);
            ListeCouleurs.Add(Colors.Blue);
            ListeCouleurs.Add(Colors.Cyan);
            ListeCouleurs.Add(Colors.Azure);
            ListeCouleurs.Add(Colors.DarkBlue);
            ListeCouleurs.Add(Colors.DarkSlateBlue);

            collection1 = new PieDataCollection<PieSegment>();
            collection1.CollectionName = "Animals";
            int cpt = 0;
            foreach (KeyValuePair<string, int> pair in listeTest)
            {
                if (cpt == 0)
                {
                    graphique.Text = pair.Key + ": " + pair.Value;
                }
                else
                {
                    collection1.Add(new PieSegment { Color = ListeCouleurs[cpt], Value = pair.Value, Name = pair.Key });
                }
                cpt++;
            }

            chart.Data = collection1;
            chart.PopupBrush = Brushes.LightCoral;
           
        }

    }
}