using BLL;
using Entities;
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

namespace Views {

    public partial class AjouterCommunicationVue : Window {
        public Utilisateur User {
            get;
            set;
        }

        Communication com;

        List<Etudiant> lesEtudiants;
        List<Entreprise> lesEntreprises;

        List<string> listStatusCommunication;
        List<string> listTypeCommunication;

       private Style style;

        public AjouterCommunicationVue(Utilisateur user) {
            InitializeComponent();
            User = user;
            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;
            style = this.FindResource("BtnStyleNoHover") as Style;

            //PERMISSIONS
            //admin
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
                BtnValiderAjouter.Visibility = System.Windows.Visibility.Hidden;
                StatistiquesMenu.Visibility = System.Windows.Visibility.Hidden;
            }

            userName.Content = User.Nom;
            com = new Communication();
            com.IdTo = -1;

            listStatusCommunication = new List<string>();
            listTypeCommunication = new List<string>();

            foreach(IdDescription id in ListeDescription.listStatusCommunication)
                listStatusCommunication.Add(id.Description);
            foreach(IdDescription id in ListeDescription.listTypeCommunication)
                listTypeCommunication.Add(id.Description);

            ChoixStatus.ItemsSource = listStatusCommunication;
            ChoixType.ItemsSource = listTypeCommunication;

            //comportament par defaut de l affichage

            resultat.Visibility = System.Windows.Visibility.Hidden; 

            lesEtudiants = ManagerEtudiant.recupererListeProfilesEtudiantsRechercheStage();
            ajouterEtudiantVue();
        }




        //methode pour ajouter les etudiants ou les entreprises


        public void ajouterEntrepriseVue() {
            int nbEntreprise = 0;
            int nbEntrepriseMax = lesEntreprises.Count;
            StackPanel hPanel = null;

            foreach(Entreprise entreprise in lesEntreprises) {

                // tous les 4 etudiants on créé un panel horizontal
                if(nbEntreprise % 4 == 0 || nbEntreprise == 0) {
                    hPanel = new StackPanel();
                    hPanel.Orientation = Orientation.Horizontal;
                    hPanel.Width = 594;
                }

                // créer le bouton
                Button btn = new Button();
                btn.Style = style;
                btn.DataContext = entreprise;
                btn.Click += ajouterIdToCommunication;

                //layout du bouton
                StackPanel vPanel = new StackPanel();
                vPanel.Orientation = Orientation.Vertical;
                vPanel.Width = 148;
                vPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                vPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //ellipse et image du contact
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 80;
                ellipse.Height = 80;
                ellipse.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                ellipse.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                // ellipse.Margin = new Thickness(30, 10, 0, 0);

                ImageBrush imgContact = new ImageBrush();
                imgContact.Stretch = Stretch.Fill;
                imgContact.ImageSource = new BitmapImage(new Uri(@"" + entreprise.ImageLogo, UriKind.RelativeOrAbsolute));
                ellipse.Fill = imgContact;
                //ajout image a vpanel
                vPanel.Children.Add(ellipse);

                //créer un texte pour nom prenom
                Label nomPrenom = new Label();
                nomPrenom.Foreground = new SolidColorBrush(Color.FromArgb(100, 0, 151, 229));
                nomPrenom.FontSize = 20;
                nomPrenom.FontFamily = new System.Windows.Media.FontFamily("Verdana");
                nomPrenom.Content = entreprise.Nom;
                nomPrenom.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                nomPrenom.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                //ajouter Nom Prenom
                vPanel.Children.Add(nomPrenom);

                //créer texte pour formation
                ajouterTextBlock(vPanel, entreprise.Secteur, 15);

                btn.Content = vPanel;
                // ajouterEtudiantVue le bouton au paneau horizontal
                hPanel.Children.Add(btn);


                //tous les 4 etudiants on ajoute le hpanel a la liste des etudiants
                if(nbEntreprise % 4 == 0 || nbEntreprise == nbEntrepriseMax && nbEntreprise != 0)
                    ListeEtudiantsEntrepriseVue.Children.Add(hPanel);


                nbEntreprise++;
            }




        }


        public void ajouterEtudiantVue() {
            int nbEtudiant = 0;
            int nbEtudiantMax = lesEtudiants.Count;
            StackPanel hPanel = null;

            foreach(Etudiant etudiant in lesEtudiants) {

                // tous les 4 etudiants on créé un panel horizontal
                if(nbEtudiant % 3 == 0 || nbEtudiant == 0) {
                    hPanel = new StackPanel();
                    hPanel.Orientation = Orientation.Horizontal;
                    hPanel.Width = 590;
                }

                // créer le bouton
                Button btn = new Button();
                btn.Style = style;
                btn.DataContext = etudiant;
                btn.Click += ajouterIdToCommunication;


                //layout du bouton
                StackPanel vPanel = new StackPanel();
                vPanel.Orientation = Orientation.Vertical;
                vPanel.Width = 195;
                vPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                vPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //ellipse et image du contact
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 75;
                ellipse.Height = 75;
                ellipse.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                ellipse.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                //  ellipse.Margin = new Thickness(30, 10, 0, 0);

                ImageBrush imgContact = new ImageBrush();
                imgContact.Stretch = Stretch.Fill;
                imgContact.ImageSource = new BitmapImage(new Uri(@"" + etudiant.PhotoURL, UriKind.RelativeOrAbsolute));
                ellipse.Fill = imgContact;
                //ajout image a vpanel
                vPanel.Children.Add(ellipse);

                //créer un texte pour nom prenom
                Label nomPrenom = new Label();
                nomPrenom.Foreground = new SolidColorBrush(Color.FromArgb(100, 0, 151, 229));
                nomPrenom.FontSize = 20;
                nomPrenom.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                nomPrenom.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                nomPrenom.FontFamily = new System.Windows.Media.FontFamily("Verdana");
                nomPrenom.Content = etudiant.Prenom + " " + etudiant.Nom;
                //ajouter Nom Prenom
                vPanel.Children.Add(nomPrenom);

                //créer texte pour formation
                ajouterTextBlock(vPanel, ListeDescription.recupererLaFormation(etudiant.IdFormation), 14);

                btn.Content = vPanel;
                // ajouterEtudiantVue le bouton au paneau horizontal
                hPanel.Children.Add(btn);


                //tous les 4 etudiants on ajoute le hpanel a la liste des etudiants                
                if(nbEtudiant % 3 == 0 || nbEtudiant == nbEtudiantMax && nbEtudiant != 0)
                    ListeEtudiantsEntrepriseVue.Children.Add(hPanel);

                nbEtudiant++;
            }
        }


        private void ajouterTextBlock(StackPanel hPanel, string content, int fontSize) {

            TextBox label = new TextBox();
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 20, 20, 20));
            label.BorderBrush = null;
            label.BorderThickness = new Thickness(0);
            label.FontSize = fontSize;
            label.Background = null;
            //label.IsEnabled = false;
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            label.TextWrapping = TextWrapping.Wrap;
            label.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            label.Text = content;
            hPanel.Children.Add(label);
        }

        //evenement au click
        private void bntRechercher_Click(object sender, RoutedEventArgs e) {


            string ChoixRechercheValeur = Recherche.Text;
           // MessageBox.Show("valeur rechercher " + Recherche.Text, "LOGIN FAIL", MessageBoxButton.OK);

            bool? radioCheck = RadioBtnEtudiant.IsChecked;

            if(radioCheck == true) {
                
                lesEtudiants = ManagerEtudiant.recupererEtudiantParleNom(ChoixRechercheValeur);
                if(lesEtudiants != null) {
                    resultat.Visibility = System.Windows.Visibility.Hidden; 
                    ListeEtudiantsEntrepriseVue.Children.Clear();
                    ajouterEtudiantVue();
                }else
                    resultat.Visibility = System.Windows.Visibility.Visible; 
            } else {

                lesEntreprises = ManagerEntreprise.recupererListeProfilesEntreprisesSelonNom(ChoixRechercheValeur);

                if(lesEntreprises != null) {
                    resultat.Visibility = System.Windows.Visibility.Hidden; 
                    ListeEtudiantsEntrepriseVue.Children.Clear();
                    ajouterEntrepriseVue();
                }else
                    resultat.Visibility = System.Windows.Visibility.Visible; 
            }


        }

        private void BtnValiderAjouter_Click(object sender, RoutedEventArgs e) {
            bool ajouter = true;


          //  MessageBox.Show("Test "+ ChoixType.SelectedValue, "Ajout Communication", MessageBoxButton.OK, MessageBoxImage.Information);

            string ChoixStatusValeur = null;
            if(ChoixStatus.SelectedValue !=null && !ChoixStatus.SelectedValue.ToString().Equals("")) {
                 ChoixStatusValeur = (string)ChoixStatus.SelectedValue;
                 com.StatusCommunication = ListeDescription.recupererIdDescription(ChoixStatusValeur, ListeDescription.listStatusCommunication);
            } else
                ajouter = false;

            string ChoixTypeValeur = null;
            if(ChoixType.SelectedValue!=null && !ChoixType.SelectedValue.ToString().Equals("")) {
                 ChoixTypeValeur = (string)ChoixType.SelectedValue;
                 com.TypeCommunication = ListeDescription.recupererIdDescription(ChoixTypeValeur, ListeDescription.listTypeCommunication);
            } else
                ajouter = false;

            string ChoixCommentaireValeur = null;
            if(!ChoixCommentaire.Text.Equals("")) {
                ChoixCommentaireValeur = (string)ChoixCommentaire.Text;
                 com.Commentaire = ChoixCommentaireValeur;
            } 
            
            bool? radioCheck = RadioBtnEtudiant.IsChecked;
           // MessageBox.Show("Test " + com.IdTo, "Ajout Communication", MessageBoxButton.OK, MessageBoxImage.Information);

            if(com.IdTo == -1)
                ajouter = false;
            
            com.IdUtilisateur = User.Id;
            com.DateCommunication = DateTime.Now;
           

            if(ajouter) {

                if(radioCheck == true)
                    ManagerCommunication.ajouterCommunicationEtudiantUtilisateur(com);
                else
                    ManagerCommunication.ajouterCommunicationEntrepriseUtilisateur(com);

               MessageBox.Show("Communication Ajoutée.", "Ajout d'une communication", MessageBoxButton.OK, MessageBoxImage.Information);
            //redirectioner pour la page lister communications
               this.Visibility = System.Windows.Visibility.Hidden;
               ListerCommunicationsVue communication = new ListerCommunicationsVue(User);
               communication.Show();
               this.Close();

            }else
                MessageBox.Show("Veuillez saisir tous les champs pour ajouter.", "Ajout d'une communication", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void ajouterIdToCommunication(object sender, RoutedEventArgs e) {

            Button b = (Button)sender;
            bool? radioCheck = RadioBtnEtudiant.IsChecked;

            if(radioCheck == true) {

                Etudiant etudiant = (Etudiant)b.DataContext;
                com.IdTo = etudiant.Id;
                ImageEllipse.Source = new BitmapImage(new Uri(@"" + etudiant.PhotoURL, UriKind.RelativeOrAbsolute));
                LblNom.Content = etudiant.Prenom;
                LblAutre.Content = etudiant.Nom;


            } else {

                Entreprise entreprise = (Entreprise)b.DataContext;
                com.IdTo = entreprise.Id;
                ImageEllipse.Source = new BitmapImage(new Uri(@"" + entreprise.ImageLogo, UriKind.RelativeOrAbsolute));
                LblNom.Content = entreprise.Nom;
                LblAutre.Content = entreprise.Secteur;
            }
        }
        // evenement au click du menu gauche

        private void EtudiantMenu_Click(object sender, RoutedEventArgs e) {
            this.Visibility = System.Windows.Visibility.Hidden;
            ListerEtudiantsVue listeretudiant = new ListerEtudiantsVue(User);
            listeretudiant.Show();
            this.Close();
        }

        private void EntrepriseMenu_Click(object sender, RoutedEventArgs e) {
            this.Visibility = System.Windows.Visibility.Hidden;
            ListerEntreprisesVue listerentreprise = new ListerEntreprisesVue(User);
            listerentreprise.Show();
            this.Close();
        }

        private void CommunicationMenu_Click(object sender, RoutedEventArgs e) {
            this.Visibility = System.Windows.Visibility.Hidden;
            ListerCommunicationsVue communication = new ListerCommunicationsVue(User);
            communication.Show();
            this.Close();
        }

        private void StageMenu_Click(object sender, RoutedEventArgs e) {
            this.Visibility = System.Windows.Visibility.Hidden;
            ListerStagesVue listerStages = new ListerStagesVue(User);
            listerStages.Show();
            this.Close();
        }

      

        private void RadioBtnEntreprise_Click(object sender, RoutedEventArgs e)
        {
            ListeEtudiantsEntrepriseVue.Children.Clear();
            lesEntreprises = ManagerEntreprise.recupererListeProfilesEntreprises();
            ajouterEntrepriseVue();
        }

        private void RadioBtnEtudiant_Click(object sender, RoutedEventArgs e)
        {
            ListeEtudiantsEntrepriseVue.Children.Clear();
            lesEtudiants = ManagerEtudiant.recupererListeProfilesEtudiantsRechercheStage();
            ajouterEtudiantVue();
            
        }

        private void StatistiquesMenu_Click(object sender, RoutedEventArgs e) {
            StatistiquesVue vue = new StatistiquesVue(User);
            vue.Show();
            this.Close();
        }

        // evenement pour menu configuration
        private void BtnComptes_Click(object sender, RoutedEventArgs e) {
            this.Visibility = System.Windows.Visibility.Hidden;
            CompteVue gestioncomptevue = new CompteVue(User);
            gestioncomptevue.Show();
            this.Close();
        }

        private void BtnConfigurations_Click(object sender, RoutedEventArgs e) {
            this.Visibility = System.Windows.Visibility.Hidden;
            ConfigurationVue configvue = new ConfigurationVue(User);
            configvue.Show();
            this.Close();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e) {
            Acceuil vue = new Acceuil(User);
            vue.Show();
            this.Close();
        }
      
       
    }
}
