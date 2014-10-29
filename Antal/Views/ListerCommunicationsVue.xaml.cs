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

namespace Views {
    /// <summary>
    /// Interaction logic for ListerCommunicationsVue.xaml
    /// </summary>
    public partial class ListerCommunicationsVue : Window {

        public Utilisateur User {
            get;
            set;
        }
        List<string> listStatusCommunication;
        List<string> listTypeCommunication;
        
        bool? isEtudiant;

        private Style style;
        private Style style2;
        
        //liste de toutes les communications
        List<Communication> communicationsEtudiants;
        List<Communication> communicationsEntreprises;

        // pour afficher une communication
        public Communication communication { get; set; }
        public Etudiant etudiant { get; set; }
        public Entreprise entreprise { get; set; }
        private string messageOk = "Communication Modifiée";

        public ListerCommunicationsVue(Utilisateur leUser) {
            InitializeComponent();
            communication = new Communication();
            

            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;

            style = this.FindResource("BtnStyleNoHover") as Style;
            style2 = this.FindResource("BtnStyleNoHover2") as Style;


            resultat.Visibility = System.Windows.Visibility.Hidden;
            //charger dropdown
            listStatusCommunication = new List<string>();
            listTypeCommunication = new List<string>();

            foreach(IdDescription id in ListeDescription.listStatusCommunication)
                listStatusCommunication.Add(id.Description);
            foreach(IdDescription id in ListeDescription.listTypeCommunication)
                listTypeCommunication.Add(id.Description);

            ChoixStatus.ItemsSource = listStatusCommunication;
            ChoixType.ItemsSource = listTypeCommunication;
            
            //recup User
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
            //visiteur
            else
            {
                BtnComptes.Visibility = System.Windows.Visibility.Hidden;
                BtnConfigurations.Visibility = System.Windows.Visibility.Hidden;
                BtnModifierCommunication.Visibility = System.Windows.Visibility.Hidden;
                BtnAjouterCommunication.Visibility = System.Windows.Visibility.Hidden;
                StatistiquesMenu.Visibility = System.Windows.Visibility.Hidden;
            }
            userName.Content = User.Nom;
            
            // recupere les liste de communication
            communicationsEtudiants = null;
            communicationsEntreprises = null;
            
            communicationsEtudiants = ManagerCommunication.recupererCommunicationsEtudiant();
            communicationsEntreprises = ManagerCommunication.recupererCommunicationsEntreprise();
            
            if (communicationsEntreprises != null)
                ajouterCommunicationEntrepriseVue();

            if (communicationsEtudiants != null)
                ajouterCommunicationEtudiantVue();


            BtnModifierCommunication.IsEnabled = false;
            
        }

        public ListerCommunicationsVue(Utilisateur leUser, Communication com, bool isEtudiant)
        {
            InitializeComponent();
            communication = com;


            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;
            resultat.Visibility = System.Windows.Visibility.Hidden;


            style = this.FindResource("BtnStyleNoHover") as Style;
            style2 = this.FindResource("BtnStyleNoHover2") as Style;

            //charger dropdown
            listStatusCommunication = new List<string>();
            listTypeCommunication = new List<string>();

            foreach (IdDescription id in ListeDescription.listStatusCommunication)
                listStatusCommunication.Add(id.Description);
            foreach (IdDescription id in ListeDescription.listTypeCommunication)
                listTypeCommunication.Add(id.Description);

            ChoixStatus.ItemsSource = listStatusCommunication;
            ChoixType.ItemsSource = listTypeCommunication;

            //recup User
            User = leUser;
            userName.Content = User.Nom;

            // recupere les liste de communication
            communicationsEtudiants = null;
            communicationsEntreprises = null;
            if (isEtudiant)
            {
                communicationsEntreprises = ManagerCommunication.recupererCommunicationsEntreprise();
                communicationsEtudiants = new List<Communication>();
                communicationsEtudiants.Add(com);
                afficherDetailsCommunicationEtudiantSelect(com);
   
            }
            else
            {
                communicationsEtudiants = ManagerCommunication.recupererCommunicationsEtudiant();
                communicationsEntreprises = new List<Communication>();
                communicationsEntreprises.Add(com);
                afficherDetailsCommunicationEntrepriseSelect(com);
            }

            if (communicationsEntreprises != null)
                ajouterCommunicationEntrepriseVue();

            if (communicationsEtudiants != null)
                ajouterCommunicationEtudiantVue();


            BtnModifierCommunication.IsEnabled = false;

        }

        // methode pour ajouter les listes dynamiques
        public void ajouterCommunicationEtudiantVue() {

            int i = 0;

           
                foreach (Communication com in communicationsEtudiants)
                {
                    // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données
                    // créer le bouton

                    //recuperer info necessaire (requete)
                    Etudiant etudiant = ManagerEtudiant.recupererProfilesEtudiant(com.IdTo);
                    Utilisateur user = ManagerUtilisateur.recupererUtilisateurParId(com.IdUtilisateur);

                    Button btn = new Button();                   
                    if (i % 2 == 0)
                        btn.Style = style;
                    else
                        btn.Style = style2;                   
                    btn.DataContext = com;                  
                    btn.Click += afficherDetailsCommunicationEtudiant;

                    //layout du bouton
                    StackPanel hPanel = new StackPanel();
                    hPanel.Orientation = Orientation.Horizontal;
                    //hPanel.Width = 148;
                    hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                    //nom prenom etudiant
                    ajouterTextBlock(hPanel, etudiant.Prenom + " " + etudiant.Nom, 13, 110);

                    //nom admin
                    ajouterLabel(hPanel, user.Nom, 13, 95);

                    //date Communication
                    ajouterLabel(hPanel, com.DateCommunication.ToShortDateString(), 13, 95);

                    //Type de stage
                    ajouterLabel(hPanel, ListeDescription.recupererDescription(com.TypeCommunication, ListeDescription.listTypeCommunication), 13, 95);

                    //Type de status
                    ajouterLabel(hPanel, ListeDescription.recupererDescription(com.StatusCommunication, ListeDescription.listStatusCommunication), 13, 95);

                    //ajouter l image pour supprimer
                    if (User.IdTypeUtilisateur == 1 || User.IdTypeUtilisateur == 2)
                    {
                        Image imgSuppr = new Image();
                        imgSuppr.DataContext = com.Id;
                        imgSuppr.Width = 25;
                        imgSuppr.Height = 25;
                        imgSuppr.Stretch = Stretch.Fill;
                        imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                        imgSuppr.MouseDown += supprimerCommunicationEtudiant;
                        hPanel.Children.Add(imgSuppr);
                    }
                    //ajouter le bouton au stackPanel principal
                    btn.Content = hPanel;
                    ListeCommunicationEtudiantVue.Children.Add(btn);
                    i++;
                }
            
        }
       

        public void ajouterCommunicationEntrepriseVue() {

            int i =1;
            
            foreach(Communication com in communicationsEntreprises) {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données
                // créer le bouton
                //recuperer info necessaire (requete)
                Entreprise entreprise = ManagerEntreprise.recupererProfilesEntreprises(com.IdTo);
                Utilisateur user = ManagerUtilisateur.recupererUtilisateurParId(com.IdUtilisateur);

                Button btn = new Button();
                if(i % 2 == 0)
                    btn.Style = style;
                else
                    btn.Style = style2;               
                btn.DataContext = com;
                btn.Click += afficherDetailsCommunicationEntreprise;

                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                //hPanel.Width = 148;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //nom prenom etudiant
                ajouterLabel(hPanel, entreprise.Nom,13,110);

                //nom admin
                ajouterLabel(hPanel, user.Nom,13,95);

                //date Communication
                ajouterLabel(hPanel, com.DateCommunication.ToShortDateString(),13,95);

                //Type de stage
                ajouterLabel(hPanel, ListeDescription.recupererDescription(com.TypeCommunication, ListeDescription.listTypeCommunication),13,95);

                //Type de status
                ajouterLabel(hPanel, ListeDescription.recupererDescription(com.StatusCommunication, ListeDescription.listStatusCommunication),13,95);

                //ajouter l image pour supprimer
                if (User.IdTypeUtilisateur == 1 || User.IdTypeUtilisateur == 2)
                {
                    Image imgSuppr = new Image();
                    imgSuppr.DataContext = com.Id;
                    imgSuppr.Width = 25;
                    imgSuppr.Height = 25;
                    imgSuppr.Stretch = Stretch.Fill;
                    imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                    imgSuppr.MouseDown += supprimerCommunicationEntreprise;
                    hPanel.Children.Add(imgSuppr);
                }
                //ajouter le bouton au stackPanel principal
                btn.Content = hPanel;
                ListeCommunicationEntreprise.Children.Add(btn);

                i++;
            
            }
        }

        private void ajouterTextBlock(StackPanel hPanel, string content, int fontSize, int width) {

            TextBox label = new TextBox();
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 0, 151, 229));
            label.FontSize = fontSize;
           // label.IsEnabled = false;
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


        //evenement a faire sur les bouton


        private void BtnAjouterCommunication_Click(object sender, RoutedEventArgs e) {

            AjouterCommunicationVue ajoutVue = new AjouterCommunicationVue(User);
            ajoutVue.Show();
            this.Close();



        }   

        private void BtnModifierCommunication_Click(object sender, RoutedEventArgs e) {

            communication.StatusCommunication = ListeDescription.recupererIdDescription(ChoixStatus.SelectedValue.ToString(), ListeDescription.listStatusCommunication);
            DateTime? date = ChoixDate.SelectedDate;
            Object objectdate = date;
            communication.DateCommunication = (DateTime)objectdate;
            communication.TypeCommunication = ListeDescription.recupererIdDescription(ChoixType.SelectedValue.ToString(), ListeDescription.listTypeCommunication);
            communication.Commentaire = choixCommentaire.Text;

            if (isEtudiant == true)
            {
                bool ok=ManagerCommunication.modifierCommunicationEtudiantUtilisateur(communication);
                if (ok)
                {
                    MessageBox.Show(messageOk);
                    communicationsEtudiants = ManagerCommunication.recupererCommunicationsEtudiant();
                    ListeCommunicationEtudiantVue.Children.Clear();

                    if (communicationsEtudiants != null)
                        ajouterCommunicationEtudiantVue();
                }
            }
            else
            {
                bool ok = ManagerCommunication.modifierCommunicationEntrepriseUtilisateur(communication);
                if (ok)
                {
                    MessageBox.Show(messageOk);
                    communicationsEntreprises = ManagerCommunication.recupererCommunicationsEntreprise();

                    ListeCommunicationEntreprise.Children.Clear();

                    if (communicationsEntreprises != null)
                        ajouterCommunicationEntrepriseVue();
                }
            }

        }

        private void afficherDetailsCommunicationEtudiant(object sender, RoutedEventArgs e) {
           
            isEtudiant = true;
            BtnModifierCommunication.IsEnabled = true;

            communication = (Communication)(((Button)sender).DataContext);
           
            
            ChoixStatus.SelectedItem = ListeDescription.recupererDescription(communication.StatusCommunication, ListeDescription.listStatusCommunication);
            ChoixDate.SelectedDate = communication.DateCommunication;
            ChoixType.SelectedItem = ListeDescription.recupererDescription(communication.TypeCommunication, ListeDescription.listTypeCommunication);
            choixCommentaire.Text = communication.Commentaire;
            etudiant = ManagerEtudiant.recupererProfilesEtudiant(communication.IdTo);
            LblNomDeEtudiant.Content = etudiant.Nom;
            LblFormation.Text = ListeDescription.recupererLaFormation(etudiant.IdFormation);
            ImageEllipse.Source = new BitmapImage(new Uri(@"" + etudiant.PhotoURL, UriKind.RelativeOrAbsolute));
        }

        private void afficherDetailsCommunicationEtudiantSelect(Communication communicationSelect)
        {
            
            BtnModifierCommunication.IsEnabled = true;
            communication = communicationSelect;
            ChoixStatus.SelectedItem = ListeDescription.recupererDescription(communication.StatusCommunication, ListeDescription.listStatusCommunication);
            ChoixDate.SelectedDate = communication.DateCommunication;
            ChoixType.SelectedItem = ListeDescription.recupererDescription(communication.TypeCommunication, ListeDescription.listTypeCommunication);
            choixCommentaire.Text = communication.Commentaire;
            etudiant = ManagerEtudiant.recupererProfilesEtudiant(communication.IdTo);
            LblNomDeEtudiant.Content = etudiant.Nom;
            LblFormation.Text = ListeDescription.recupererLaFormation(etudiant.IdFormation);
            ImageEllipse.Source = new BitmapImage(new Uri(@"" + etudiant.PhotoURL, UriKind.RelativeOrAbsolute));
        }

        private void afficherDetailsCommunicationEntreprise(object sender, RoutedEventArgs e) {

        
            isEtudiant = false;
            BtnModifierCommunication.IsEnabled = true;
            communication = (Communication)(((Button)sender).DataContext);

            ChoixStatus.SelectedItem = ListeDescription.recupererDescription(communication.StatusCommunication, ListeDescription.listStatusCommunication);
            ChoixDate.SelectedDate = communication.DateCommunication;
            ChoixType.SelectedItem = ListeDescription.recupererDescription(communication.TypeCommunication, ListeDescription.listTypeCommunication);
            choixCommentaire.Text = communication.Commentaire;
            entreprise = ManagerEntreprise.recupererProfilesEntreprises(communication.IdTo);
            LblNomDeEtudiant.Content = entreprise.Nom;
            LblFormation.Text = entreprise.Secteur;
            ImageEllipse.Source = new BitmapImage(new Uri(@"" + entreprise.ImageLogo, UriKind.RelativeOrAbsolute));
        }

        private void afficherDetailsCommunicationEntrepriseSelect(Communication communicationSelect)
        {
            
            BtnModifierCommunication.IsEnabled = true;
            communication = communicationSelect;

            ChoixStatus.SelectedItem = ListeDescription.recupererDescription(communication.StatusCommunication, ListeDescription.listStatusCommunication);
            ChoixDate.SelectedDate = communication.DateCommunication;
            ChoixType.SelectedItem = ListeDescription.recupererDescription(communication.TypeCommunication, ListeDescription.listTypeCommunication);
            choixCommentaire.Text = communication.Commentaire;
            entreprise = ManagerEntreprise.recupererProfilesEntreprises(communication.IdTo);
            LblNomDeEtudiant.Content = entreprise.Nom;
            LblFormation.Text = entreprise.Secteur;
            ImageEllipse.Source = new BitmapImage(new Uri(@"" + entreprise.ImageLogo, UriKind.RelativeOrAbsolute));
        }
        private void supprimerCommunicationEntreprise(object sender, RoutedEventArgs e) {

             MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer cette communication?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
             if (ret == MessageBoxResult.Yes)
             {
                 int idImage = (int)((Image)sender).DataContext;

                 communication.Id = idImage;
                 ManagerCommunication.supprimerCommunicationEntrepriseUtilisateur(communication);
                 communicationsEntreprises = ManagerCommunication.recupererCommunicationsEntreprise();

                 ListeCommunicationEntreprise.Children.Clear();

                 if (communicationsEntreprises != null)
                     ajouterCommunicationEntrepriseVue();
             }

        }
        private void supprimerCommunicationEtudiant(object sender, RoutedEventArgs e) {
            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer cette communication?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (ret == MessageBoxResult.Yes)
            {
                int idImage = (int)((Image)sender).DataContext;
                communication.Id = idImage;
                ManagerCommunication.supprimerCommunicationEtudiantUtilisateur(communication);
                communicationsEtudiants = ManagerCommunication.recupererCommunicationsEtudiant();
                ListeCommunicationEtudiantVue.Children.Clear();

                if (communicationsEtudiants != null)
                    ajouterCommunicationEtudiantVue();
            }
            
        }

        // evenement au click du menu gauche

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
            
            ConfigurationVue configvue = new ConfigurationVue(User);
            configvue.Show();
            this.Close();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e) {
            Acceuil vue = new Acceuil(User);
            vue.Show();
            this.Close();
        }

        private void RechercheEntreprise_Click(object sender, RoutedEventArgs e) {
            
           
            string ChoixRechercheValeur = RechercheEntrepriseNom.Text;
            communicationsEntreprises = ManagerCommunication.recupererCommunicationEntrepriseUtilisateurRecherche(ChoixRechercheValeur);
            if (communicationsEntreprises != null)
            {
                resultat.Visibility = System.Windows.Visibility.Hidden;
                ListeCommunicationEntreprise.Children.Clear();
                ajouterCommunicationEntrepriseVue();
            }
            else
                resultat.Visibility = System.Windows.Visibility.Visible; 
        }

        private void RechercheEtudiant_Click(object sender, RoutedEventArgs e) {

           
            string ChoixRechercheValeur = RechercheEtudiantNom.Text;
            communicationsEtudiants = ManagerCommunication.recupererCommunicationEtudiantUtilisateurRecherche(ChoixRechercheValeur);
            if(communicationsEtudiants != null) {
                resultat.Visibility = System.Windows.Visibility.Hidden;
                ListeCommunicationEtudiantVue.Children.Clear();
                ajouterCommunicationEtudiantVue();
            } else
                resultat.Visibility = System.Windows.Visibility.Visible; 
        }

    
  
    }
}
