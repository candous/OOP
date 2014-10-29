using Entities;
using BLL;
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

namespace Views
{
    /// <summary>
    /// Logique d'interaction pour ListerStageVue.xaml
    /// </summary>
    public partial class ListerStagesVue : Window
    {

        public Utilisateur User
        {
            get;
            set;
        }

        private List<Stage> lesStages;

        public Stage LeStageAModifier
        {
            get;
            set;
        }
        private Style style;
        private Style style2;

        public ListerStagesVue(Utilisateur leUser)
        {
            InitializeComponent();
            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;
            lesStages = ManagerStage.recupererListStage();


            style = this.FindResource("BtnStyleNoHover") as Style;
            style2 = this.FindResource("BtnStyleNoHover2") as Style;

            LeStageAModifier = new Stage();
            User = leUser;
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
                BtnAjouterStage.Visibility = System.Windows.Visibility.Hidden;
                BtnValiderRechercher.Visibility = System.Windows.Visibility.Hidden;
                StatistiquesMenu.Visibility = System.Windows.Visibility.Hidden;
            }
            userName.Content = User.Nom;
            BtnValiderRechercher.IsEnabled = false;

            //MessageBox.Show("Saisir le courriel, svp." + lesStages[0].Salaire, "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            ajouterStageVue();
            resultat.Visibility = System.Windows.Visibility.Hidden;
        }

        public ListerStagesVue(Utilisateur leUser, int idStageSelect)
        {
            InitializeComponent();
            Stage stageSelect;
            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;
            stageSelect = ManagerStage.recupererStageParId(idStageSelect);

            style = this.FindResource("BtnStyleNoHover") as Style;
            style2 = this.FindResource("BtnStyleNoHover2") as Style;

            lesStages = new List<Stage>();
            LeStageAModifier = new Stage();
            lesStages.Add(stageSelect);
            User = leUser;
            userName.Content = User.Nom;
            BtnValiderRechercher.IsEnabled = false;

            //MessageBox.Show("Saisir le courriel, svp." + lesStages[0].Salaire, "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            ajouterStageVue();

            afficherDetailsStageSelect(stageSelect);
        }

        public void ajouterStageVue()
        {

            int i = 0;
            foreach (Stage stage in lesStages)
            {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données
                Etudiant etudiant = ManagerEtudiant.recupererProfilesEtudiant(stage.IdEtudiant);
                Entreprise entreprise = ManagerEntreprise.recupererProfilesEntreprises(stage.IdEntreprise);

                // créer le bouton
                Button btn = new Button();
                if(i % 2 == 0)
                    btn.Style = style;
                else
                    btn.Style = style2;
                btn.DataContext = stage;
                btn.Click += afficherDetailsStage;

                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                //hPanel.Width = 148;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //nom prenom etudiant
                ajouterTextBlock(hPanel, etudiant.Prenom + " " + etudiant.Nom , 13 , 130);

                //nom entreprise
                ajouterLabel(hPanel, entreprise.Nom ,13, 90);

                //Date debut  

                if (stage.DateDebut != null) { 
                Object DateTemp = stage.DateDebut;
                DateTime DateDebut = (DateTime)DateTemp;
              
                ajouterLabel(hPanel, DateDebut.ToShortDateString(), 13, 100);
                }else
                    ajouterLabel(hPanel, "", 13, 100);
                //Type de stage
                ajouterTextBlock(hPanel, ListeDescription.recupererDescription(stage.TypeStage, ListeDescription.listTypeStage), 13, 130);

                // retenu ou pas
                CheckBox retenu = new CheckBox();
                retenu.Width = 90;
                retenu.IsEnabled = false;
                if (stage.Retenu == true)
                    retenu.IsChecked = true;
                hPanel.Children.Add(retenu);
                
                if (User.IdTypeUtilisateur == 1 || User.IdTypeUtilisateur == 2)
                {
                    //ajouter l image pour supprimer
                    Image imgSuppr = new Image();
                    imgSuppr.DataContext = stage.Id;
                    imgSuppr.Width = 25;
                    imgSuppr.Height = 25;
                    imgSuppr.Stretch = Stretch.Fill;
                    imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                    imgSuppr.MouseDown += supprimerStage;
                    hPanel.Children.Add(imgSuppr);
                }

                //ajouter le bouton au stackPanel principal
                btn.Content = hPanel;

                ListeStagesVue.Children.Add(btn);


                i++;
            }
        }

        private void ajouterTextBlock(StackPanel hPanel, string content, int fontSize, int width) {

            TextBox label = new TextBox();
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 0, 151, 229));
            label.FontSize = fontSize;
            //label.IsEnabled = false;
            label.BorderThickness = new Thickness(0);
            label.Width = width;
            label.Background = null;
            label.BorderBrush = null;
            label.TextWrapping = TextWrapping.Wrap;
            label.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            label.Text = content;
            hPanel.Children.Add(label);
        }

        private void ajouterLabel(StackPanel hPanel, string content) {

            Label label = new Label();
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 0, 151, 229));
            label.FontSize = 15;
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            label.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            label.Content = content;
            hPanel.Children.Add(label);
        }

        private void ajouterLabel(StackPanel hPanel, string content, int fontSize) {

            Label label = new Label();
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 0, 151, 229));
            label.FontSize = fontSize;

            label.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            label.Content = content;
            hPanel.Children.Add(label);
        }

        private void ajouterLabel(StackPanel hPanel, string content, int fontSize, int width) {

            Label label = new Label();
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 0, 151, 229));
            label.FontSize = fontSize;
            label.Width = width;
            label.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            label.Content = content;
            hPanel.Children.Add(label);
        }


        private void afficherDetailsStage(object sender, RoutedEventArgs e)
        {
            LeStageAModifier = (Stage)(((Button)sender).DataContext);

            BtnValiderRechercher.IsEnabled = true;
            ChoixRetenu.IsChecked = LeStageAModifier.Retenu;
            ChoixDatePlacement.SelectedDate = LeStageAModifier.DatePlacement;
            ChoixDateDebut.SelectedDate = LeStageAModifier.DateDebut;
            ChoixDateFin.SelectedDate = LeStageAModifier.DateFin;
            ChoixSalaire.Text = LeStageAModifier.Salaire.ToString();
            ChoixCommentaire.Text = LeStageAModifier.Commentaire;
            Entreprise entreprise = ManagerEntreprise.recupererProfilesEntreprises(LeStageAModifier.IdEntreprise);
            NomEntreprise.Content = entreprise.Nom;
            PhotoEntreprise.Source = new BitmapImage(new Uri(@"" + entreprise.ImageLogo, UriKind.RelativeOrAbsolute));
            Etudiant etudiant = ManagerEtudiant.recupererProfilesEtudiant(LeStageAModifier.IdEtudiant);
            NomEtudiant.Content = etudiant.Nom;
            PhotoEtudiant.Source = new BitmapImage(new Uri(@"" + etudiant.PhotoURL, UriKind.RelativeOrAbsolute));


        }

        private void afficherDetailsStageSelect(Stage LeStageAModifier)
        {
            

            BtnValiderRechercher.IsEnabled = true;
            ChoixRetenu.IsChecked = LeStageAModifier.Retenu;
            ChoixDatePlacement.SelectedDate = LeStageAModifier.DatePlacement;
            ChoixDateDebut.SelectedDate = LeStageAModifier.DateDebut;
            ChoixDateFin.SelectedDate = LeStageAModifier.DateFin;
            ChoixSalaire.Text = LeStageAModifier.Salaire.ToString();
            ChoixCommentaire.Text = LeStageAModifier.Commentaire;
            Entreprise entreprise = ManagerEntreprise.recupererProfilesEntreprises(LeStageAModifier.IdEntreprise);
            NomEntreprise.Content = entreprise.Nom;
            PhotoEntreprise.Source = new BitmapImage(new Uri(@"" + entreprise.ImageLogo, UriKind.RelativeOrAbsolute));
            Etudiant etudiant = ManagerEtudiant.recupererProfilesEtudiant(LeStageAModifier.IdEtudiant);
            NomEtudiant.Content = etudiant.Nom;
            PhotoEtudiant.Source = new BitmapImage(new Uri(@"" + etudiant.PhotoURL, UriKind.RelativeOrAbsolute));


        }

        private void supprimerStage(object sender, RoutedEventArgs e)
        {

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer ce stage?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (ret == MessageBoxResult.Yes)
            {
                int idImage = (int)((Image)sender).DataContext;

                LeStageAModifier.Id = idImage;
                ManagerStage.supprimerStage(LeStageAModifier.Id);
                lesStages = ManagerStage.recupererListStage();

                ListeStagesVue.Children.Clear();

                if (lesStages != null)
                    ajouterStageVue();
            }


        }

        private void BtnAjouterStage_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Hidden;
            AjouterStageVue vueAjoute = new AjouterStageVue(User);
            vueAjoute.Show();
            this.Close();

        }

        private void BtnValiderModifierStage_Click(object sender, RoutedEventArgs e)
        {
            bool ajouter = true;

            LeStageAModifier.Retenu = ChoixRetenu.IsChecked;

            DateTime? ChoixDateTemp;
            Object objectdate;           

            if (ChoixDatePlacement.SelectedDate != null)
            {
                ChoixDateTemp = ChoixDatePlacement.SelectedDate;
                objectdate = ChoixDateTemp;
                LeStageAModifier.DatePlacement = (DateTime)objectdate;
            }
            else
                ajouter = false; 


            if (ChoixDateDebut.SelectedDate != null)
            {
                ChoixDateTemp = ChoixDateDebut.SelectedDate;
                objectdate = ChoixDateTemp;
                LeStageAModifier.DateDebut = (DateTime)objectdate;
            }
            else
                LeStageAModifier.DateDebut = null;   


            if (ChoixDateFin.SelectedDate != null)
            {
                ChoixDateTemp = ChoixDateFin.SelectedDate;
                objectdate = ChoixDateTemp;
                LeStageAModifier.DateFin = (DateTime)objectdate;
            }
            else
                LeStageAModifier.DateFin = null;

          

            double salaireValeur;
            if (Double.TryParse(ChoixSalaire.Text, out salaireValeur))
                LeStageAModifier.Salaire = salaireValeur;
            else
                LeStageAModifier.Salaire = null;
            
            LeStageAModifier.Commentaire = ChoixCommentaire.Text;

            if (ajouter)
            {
                ManagerStage.modifierStage(LeStageAModifier);
                lesStages = ManagerStage.recupererListStage();

                ListeStagesVue.Children.Clear();

                if (lesStages != null)
                    ajouterStageVue();

                MessageBox.Show("Stage Modifié.", "Modification", MessageBoxButton.OK, MessageBoxImage.Information);
            }else
                MessageBox.Show("Veuillez renseigner tous les champs", "Ajout d'un stage", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RechercheEtudiant_Click(object sender, RoutedEventArgs e)
        {
            String indexEtudiant = RechercheEtudiantNom.Text;
            lesStages = ManagerStage.recupererListeStageParNomEtudiant(indexEtudiant);
            if (lesStages != null)
            {
                resultat.Visibility = System.Windows.Visibility.Hidden;
                ListeStagesVue.Children.Clear();
                ajouterStageVue();
            }
            else
                resultat.Visibility = System.Windows.Visibility.Visible; 
        }

        private void RechercheEntreprise_Click(object sender, RoutedEventArgs e)
        {
            String indexEntreprise = RechercheEntrepriseNom.Text;
            lesStages = ManagerStage.recupererListeStageParNomEntreprise(indexEntreprise);
            if (lesStages != null)
            {
                resultat.Visibility = System.Windows.Visibility.Hidden;
                ListeStagesVue.Children.Clear();
                ajouterStageVue();
            }
            else
                resultat.Visibility = System.Windows.Visibility.Visible;
        }



        // evenement au click du menu gauche

        private void EtudiantMenu_Click(object sender, RoutedEventArgs e)
        {
            
            ListerEtudiantsVue listeretudiant = new ListerEtudiantsVue(User);
            listeretudiant.Show();
            this.Close();
        }

        private void EntrepriseMenu_Click(object sender, RoutedEventArgs e)
        {
            
            ListerEntreprisesVue listerentreprise = new ListerEntreprisesVue(User);
            listerentreprise.Show();
            this.Close();
        }

        private void CommunicationMenu_Click(object sender, RoutedEventArgs e)
        {
            
            ListerCommunicationsVue communication = new ListerCommunicationsVue(User);
            communication.Show();
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