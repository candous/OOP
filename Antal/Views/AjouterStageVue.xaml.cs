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
    /// <summary>
    /// Logique d'interaction pour AjouterStageVue.xaml
    /// </summary>
    public partial class AjouterStageVue : Window {


        public Utilisateur User {
            get;
            set;
        }

        public Stage LeStage {
            get;
            set;
        }

        public List<Etudiant> lesEtudiants {
            get;
            set;
        }
        public List<Entreprise> lesEntreprises {
            get;
            set;
        }

        private Style style;

        Etudiant etudiant;
        Entreprise entreprise;

        List<string> listTypeStage;


        // constructeur
        public AjouterStageVue(Utilisateur user) {
            InitializeComponent();
            InitializeComponent();
            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;
            User = user;

            style = this.FindResource("BtnStyleNoHover2") as Style;
            //PERMISSIONS
            //admin
            if(User.IdTypeUtilisateur == 1) {
                BtnComptes.Visibility = System.Windows.Visibility.Visible;
                BtnConfigurations.Visibility = System.Windows.Visibility.Visible;
            }
                //ressources humaines
            else if(User.IdTypeUtilisateur == 2) {
                BtnComptes.Visibility = System.Windows.Visibility.Hidden;
                BtnConfigurations.Visibility = System.Windows.Visibility.Visible;
            } else {
                BtnComptes.Visibility = System.Windows.Visibility.Hidden;
                BtnConfigurations.Visibility = System.Windows.Visibility.Hidden;
                StatistiquesMenu.Visibility = System.Windows.Visibility.Hidden;
            }

            userName.Content = User.Nom;
            lesEtudiants = ManagerEtudiant.recupererListeProfilesEtudiantsRechercheStage();
            lesEntreprises = ManagerEntreprise.recupererListeProfilesEntreprises();
            resultat.Visibility = System.Windows.Visibility.Hidden;

            listTypeStage = new List<string>();

            foreach(IdDescription id in ListeDescription.listTypeStage)
                listTypeStage.Add(id.Description);

            LeStage = new Stage();
            LeStage.IdEntreprise = -1;
            LeStage.IdEtudiant = -1;



            ChoixType.ItemsSource = listTypeStage;

            ajouterEntrepriseVue();
            ajouterEtudiantVue();
        }

        // metohde pour ajouter les liste d etudant

        public void ajouterEtudiantVue() {
            int nbEtudiant = 0;
            int nbEtudiantMax = lesEtudiants.Count;
            StackPanel hPanel = null;

            foreach(Etudiant etudiant in lesEtudiants) {

                // tous les 4 etudiants on créé un panel horizontal
                if(nbEtudiant % 2 == 0 || nbEtudiant == 0) {
                    hPanel = new StackPanel();
                    hPanel.Orientation = Orientation.Horizontal;
                    hPanel.Width = 294;
                }

                // créer le bouton
                Button btn = new Button();
                btn.Style = style;
                btn.DataContext = etudiant;
                btn.Click += RecupererEtudiant;

                //layout du bouton
                StackPanel vPanel = new StackPanel();
                vPanel.Orientation = Orientation.Vertical;
                vPanel.Width = 147;
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
                ajouterTextBlock2(vPanel, etudiant.Prenom + " " + etudiant.Nom, 18);
             
                //créer texte pour formation
                ajouterTextBlock(vPanel, ListeDescription.recupererLaFormation(etudiant.IdFormation), 14);
                               

                btn.Content = vPanel;
                // ajouterEtudiantVue le bouton au paneau horizontal
                hPanel.Children.Add(btn);


                //tous les 4 etudiants on ajoute le hpanel a la liste des etudiants                
                if(nbEtudiant % 2 == 0 || nbEtudiant == nbEtudiantMax && nbEtudiant != 0)
                    ListeEtudiantsVue.Children.Add(hPanel);

                nbEtudiant++;
            }
        }

        //methode pour ajouter la liste d entreprise
        public void ajouterEntrepriseVue() {
            int nbEntreprise = 0;
            int nbEntrepriseMax = lesEntreprises.Count;
            StackPanel hPanel = null;

            foreach(Entreprise entreprise in lesEntreprises) {

                // tous les 4 etudiants on créé un panel horizontal
                if(nbEntreprise % 2 == 0 || nbEntreprise == 0) {
                    hPanel = new StackPanel();
                    hPanel.Orientation = Orientation.Horizontal;
                    hPanel.Width = 294;
                }

                // créer le bouton
                Button btn = new Button();
                btn.Background = null;
                btn.BorderBrush = null;
                btn.DataContext = entreprise;
                btn.Click += RecupererEntreprise;

                //layout du bouton
                StackPanel vPanel = new StackPanel();
                vPanel.Orientation = Orientation.Vertical;
                vPanel.Width = 147;
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
                ajouterTextBlock2(vPanel, entreprise.Nom, 18);
                

                //ajouter Formation
                ajouterTextBlock(vPanel, entreprise.Secteur, 15);

                btn.Content = vPanel;
                // ajouterEtudiantVue le bouton au paneau horizontal
                hPanel.Children.Add(btn);


                //tous les 4 etudiants on ajoute le hpanel a la liste des etudiants
                if(nbEntreprise % 2 == 0 || nbEntreprise == nbEntrepriseMax && nbEntreprise != 0)
                    ListeEntreprisesVue.Children.Add(hPanel);
                nbEntreprise++;
            }
        }
        private void ajouterTextBlock(StackPanel hPanel, string content, int fontSize) {

            TextBox label = new TextBox();
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 20, 20, 20));
            label.FontSize = fontSize;
            label.Background = null;
            label.BorderBrush = null;
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            label.BorderThickness = new Thickness(0);
           // label.IsEnabled = false;
            label.TextWrapping = TextWrapping.Wrap;
            label.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            label.Text = content;
            hPanel.Children.Add(label);
        }

        private void ajouterTextBlock2(StackPanel hPanel, string content, int fontSize) {

            TextBox label = new TextBox();
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 0, 151, 229));
            label.FontSize = fontSize;
            label.Background = null;
            label.BorderBrush = null;
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            label.BorderThickness = new Thickness(0);
            //label.IsEnabled = false;
            label.TextWrapping = TextWrapping.Wrap;
            label.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            label.Text = content;
            hPanel.Children.Add(label);
        }

        // evenment sur les bouton
        private void BtnAjouterStage_Click(object sender, RoutedEventArgs e) {
            bool ajouter = true;

            if(ChoixType.SelectedValue != null && !ChoixType.SelectedValue.ToString().Equals("")) {
                LeStage.TypeStage = ListeDescription.recupererIdDescription(ChoixType.SelectedValue.ToString(), ListeDescription.listTypeStage);
            } else
                ajouter = false;


            if(ChoixDatePlacemet.SelectedDate != null) {
                DateTime? datePlacementValeur = ChoixDatePlacemet.SelectedDate;
                Object datePlacementTemp = ChoixDatePlacemet.SelectedDate;
                LeStage.DatePlacement = (DateTime)datePlacementTemp;
            } else
                ajouter = false;

            double salaireValeur;
            if(Double.TryParse(ChoixSalaire.Text, out salaireValeur))
                LeStage.Salaire = salaireValeur;
            else
                LeStage.Salaire = null;
           
            if(LeStage.IdEntreprise == -1)
                ajouter = false;
            if(LeStage.IdEtudiant == -1)
                ajouter = false;
            
            LeStage.Modification = new Modification();
            LeStage.Modification.DateModification = DateTime.Now;
            LeStage.Modification.UtilisateurId = User.Id;
           
            LeStage.DateDebut = ChoixDateDebut.SelectedDate;
            LeStage.DateFin = ChoixDateFin.SelectedDate;

            LeStage.Retenu = false;
            LeStage.Commentaire = ChoixCommentaire.Text;


            if(ajouter) {
                ManagerStage.ajouterStage(LeStage);
                MessageBox.Show("Stage Ajouté.", "Ajout d'un stage", MessageBoxButton.OK, MessageBoxImage.Information);
                ListerStagesVue listerStages = new ListerStagesVue(User);
                listerStages.Show();
                this.Close();
            }else
                MessageBox.Show("Veuillez renseigner tous les champs", "Ajout d'un stage", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RecupererEtudiant(object sender, RoutedEventArgs e) {
            Button b = (Button)sender;
            etudiant = (Etudiant)b.DataContext;

            ImgEtudiant.Source = new BitmapImage(new Uri(@"" + etudiant.PhotoURL, UriKind.RelativeOrAbsolute));
            NomEtudiantVue.Content = etudiant.Nom;
            LeStage.IdEtudiant = etudiant.Id;

        }
        private void RecupererEntreprise(object sender, RoutedEventArgs e) {
            Button b = (Button)sender;
            entreprise = (Entreprise)b.DataContext;

            ImgEntreprise.Source = new BitmapImage(new Uri(@"" + entreprise.ImageLogo, UriKind.RelativeOrAbsolute));
            NomEntrepriseVue.Content = entreprise.Nom;
            LeStage.IdEntreprise = entreprise.Id;
        }

        private void RechercheEtudiant_Click(object sender, RoutedEventArgs e) {
            String choixRecherche = RechercheEtudiantNom.Text;
            lesEtudiants = ManagerEtudiant.recupererEtudiantParleNom(choixRecherche);
            if(lesEtudiants != null) {
                resultat.Visibility = System.Windows.Visibility.Hidden;
                ListeEtudiantsVue.Children.Clear();
                ajouterEtudiantVue();
            } else
                resultat.Visibility = System.Windows.Visibility.Visible;
        }

        private void RechercheEntreprise_Click(object sender, RoutedEventArgs e) {
            lesEntreprises = ManagerEntreprise.recupererListeProfilesEntreprisesSelonNom(RechercheEntrepriseNom.Text);

            if(lesEntreprises != null) {
                resultat.Visibility = System.Windows.Visibility.Hidden;
                ListeEntreprisesVue.Children.Clear();
                ajouterEntrepriseVue();
            } else
                resultat.Visibility = System.Windows.Visibility.Visible;
        }

        private void EtudiantMenu_Click(object sender, RoutedEventArgs e) {
            
            ListerEtudiantsVue listeretudiant = new ListerEtudiantsVue(User);
            listeretudiant.Show();
            this.Close();
        }

        private void EntrepriseMenu_Click(object sender, RoutedEventArgs e) {
            
            ListerEntreprisesVue listerentreprise = new ListerEntreprisesVue(User);
            listerentreprise.Show();
            this.Close();
        }

        private void CommunicationMenu_Click(object sender, RoutedEventArgs e) {
            
            ListerCommunicationsVue communication = new ListerCommunicationsVue(User);
            communication.Show();
            this.Close();
        }

        private void StageMenu_Click(object sender, RoutedEventArgs e) {
           
            ListerStagesVue listerStages = new ListerStagesVue(User);
            listerStages.Show();
            this.Close();
        }

        private void StatistiquesMenu_Click(object sender, RoutedEventArgs e) {
            StatistiquesVue vue = new StatistiquesVue(User);
            vue.Show();
            this.Close();
        }

        // evenement pour menu configuration
        private void BtnComptes_Click(object sender, RoutedEventArgs e) {
            
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
