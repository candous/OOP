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
    /// Interaction logic for ProfileEtudiantVue.xaml
    /// </summary>
    public partial class ProfileEtudiantVue : Window {
        Utilisateur User;

        public Etudiant MonEtudiant {
            get;
            set;
        }

        private Style style;
        private Style style2;


        public ProfileEtudiantVue(Utilisateur user, int idEtudiant) {
            InitializeComponent();
            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;
            this.User = user;

            style = this.FindResource("BtnStyleNoHover") as Style;
            style2 = this.FindResource("BtnStyleNoHover2") as Style;

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
                BtnModifierEtudiant.Visibility = System.Windows.Visibility.Hidden;
                BtnSupprimerEtudiant.Visibility = System.Windows.Visibility.Hidden;
                StatistiquesMenu.Visibility = System.Windows.Visibility.Hidden;
            }
            userName.Content = User.Nom;
            MonEtudiant = ManagerEtudiant.recupererEtudiantParId(idEtudiant);


            ///binder info etudiant avec la vue
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(MonEtudiant.PhotoURL, UriKind.RelativeOrAbsolute));
            affichePhoto.Fill = brush;

            prenomVue.Content = MonEtudiant.Prenom;
            nomVue.Content = MonEtudiant.Nom;
            formationVue.Text = ListeDescription.recupererLaFormation(MonEtudiant.IdFormation);

            if(MonEtudiant.Langues != null) {
                int nblangue = MonEtudiant.Langues.Count;
 
                if(nblangue > 0)
                    langue1Vue.Content = MonEtudiant.Langues[0].Description + " " + ListeDescription.recupererDescription( MonEtudiant.Langues[0].Niveau , ListeDescription.listNiveauLangue);

                if(nblangue > 1)
                    langue2Vue.Content = MonEtudiant.Langues[1].Description + " " + ListeDescription.recupererDescription(MonEtudiant.Langues[1].Niveau, ListeDescription.listNiveauLangue);

                if(nblangue > 2)
                    langue3Vue.Content = MonEtudiant.Langues[2].Description + " " + ListeDescription.recupererDescription(MonEtudiant.Langues[2].Niveau, ListeDescription.listNiveauLangue);

                if(nblangue > 3)
                    langue4Vue.Content = MonEtudiant.Langues[3].Description + " " + ListeDescription.recupererDescription(MonEtudiant.Langues[3].Niveau, ListeDescription.listNiveauLangue);
                
            }           

            courrielVue.Content = MonEtudiant.Courriel;

            if (MonEtudiant.DateNaissance != null)
            {
                Object dateN = MonEtudiant.DateNaissance;
                DateTime dateNtmp = (DateTime)dateN;
                dateNaissanceVue.Content = dateNtmp.ToShortDateString();
            }

            tel1Vue.Content = MonEtudiant.Telephone1;
            tel2Vue.Content = MonEtudiant.Telephone2;
            tel3Vue.Content = MonEtudiant.Telephone3;

            adresseVue.Content = MonEtudiant.Adresse;
            villeVue.Content = MonEtudiant.Ville;

            statusResidenceVue.Content = ListeDescription.recupererDescription(MonEtudiant.IdStatusResidence,ListeDescription.listStatusResidence);

            voitureVue.IsChecked = MonEtudiant.Vehicule;
            permisConduireVue.IsChecked = MonEtudiant.PermisConduire;
            montrealVue.IsChecked = MonEtudiant.Montreal;
            RiveSudVue.IsChecked = MonEtudiant.RiveSud;
            RiveNordVue.IsChecked = MonEtudiant.RiveNord;

            dateFinVue.Content = MonEtudiant.DateFinFormation.ToShortDateString();
            salaireVue.Content = MonEtudiant.SalaireEspere;
            posteVue.Content = MonEtudiant.PosteDesire;


            commentaireVue.Text = MonEtudiant.Commentaire;


            if(MonEtudiant.TechonologiesPreferees != null)
                ajouterTechnologieVue();
            
            if(MonEtudiant.Interets != null)
                ajouterInteretVue();

            if(MonEtudiant.Stages != null)
                ajouterStageVue();

            if(MonEtudiant.Documents != null)
                ajouterDocumentsVue();

            if(MonEtudiant.Communications != null)
                ajouterCommunicationVue();

            if(MonEtudiant.Entrevues != null)
                ajouterEntrevueVue();
        }


        // methode pour ajouter de element dynamique dans la vue

        private void ajouterTechnologieVue() {
            foreach(IdDescription desc in MonEtudiant.TechonologiesPreferees)
                ajouterLabel(listeTechnologieRecherche, desc.Description,14);

        }

        private void ajouterInteretVue() {
            foreach(IdDescription desc in MonEtudiant.Interets)
                ajouterLabel(listeInteretRecherche, desc.Description, 14);

        }


        //ajouter le les stages
        public void ajouterStageVue() {

            int i = 1;
            foreach(Stage stage in MonEtudiant.Stages) {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données

                Entreprise stageEntreprise = ManagerEntreprise.recupererProfilesEntreprises(stage.IdEntreprise);

                // créer le bouton
                Button btn = new Button();
                if(i % 2 == 0)
                    btn.Style = style;
                else
                    btn.Style = style2;
                btn.DataContext = stage.Id;
                btn.Click += afficherStage;

                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                hPanel.Width = 531;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //etudiant 
                ajouterLabel(hPanel, stageEntreprise.Nom ,15,120);

                ////date debut 
                Object dateD = stage.DateDebut;
                DateTime dateDTmps = (DateTime)dateD;
                ajouterLabel(hPanel, dateDTmps.ToShortDateString(),15,120);



                ////date Fin 
                Object dateF = stage.DateFin;
                DateTime dateFTmps = (DateTime)dateF;
                ajouterLabel(hPanel, dateFTmps.ToShortDateString(),15,120);

                //retenu   
                CheckBox retenu = new CheckBox();
                retenu.Width = 120;
                retenu.IsEnabled = false;
                if(stage.Retenu == true)
                    retenu.IsChecked = true;
                hPanel.Children.Add(retenu);

                ////ajouter l image pour supprimer
                //Image imgSuppr = new Image();
                //imgSuppr.Width = 25;
                //imgSuppr.Height = 25;
                //imgSuppr.Stretch = Stretch.Fill;
                //imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                //imgSuppr.MouseDown += supprimerStage;
                //imgSuppr.DataContext = stage.Id;
                //hPanel.Children.Add(imgSuppr);

                //ajouter le bouton au stackPanel principal

                btn.Content = hPanel;
                ListeStagetVue.Children.Add(btn);
                i++;
            }
        }

        //ajout communication
        public void ajouterCommunicationVue() {

            int i = 1;
            foreach(Communication com in MonEtudiant.Communications) {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données
                // créer le bouton
                Button btn = new Button();
                if(i % 2 == 0)
                    btn.Style = style;
                else
                    btn.Style = style2;
                btn.DataContext = com;
                btn.Click += afficherCommunication;

                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                hPanel.Width = 300;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //date 
                ajouterLabel(hPanel, com.DateCommunication.ToShortDateString(), 12,75);

                //type 
                ajouterLabel(hPanel, ListeDescription.recupererDescription(com.TypeCommunication, ListeDescription.listTypeCommunication), 12,75);

                //status   
                ajouterLabel(hPanel, ListeDescription.recupererDescription(com.StatusCommunication, ListeDescription.listStatusCommunication), 12,75);

                //ajouter l image pour supprimer
                Image imgSuppr = new Image();
                imgSuppr.Width = 25;
                imgSuppr.Height = 25;
                imgSuppr.Margin = new Thickness(15, 0, 0, 0);
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += supprimerCommunication;
                imgSuppr.DataContext = com.Id;
                hPanel.Children.Add(imgSuppr);

                //ajouter le bouton au stackPanel principal

                btn.Content = hPanel;
                ListeCommunicationsVue.Children.Add(btn);
                i++;
            }
        }

        //peut pas tester pas de données dans bdd
        public void ajouterDocumentsVue()
        {

            int i = 1;
            foreach (Document doc in MonEtudiant.Documents)
            {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données 
                
                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                hPanel.Background = null;
                hPanel.Width = 300;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //type 
                ajouterLabel(hPanel, doc.Titre, 12, 75);

                //date 
                Object date = doc.DateAjout;
                DateTime dateAjout = (DateTime)date;

                ajouterLabel(hPanel, dateAjout.ToShortDateString(),12,75);

                //Type   
                ajouterLabel(hPanel, ListeDescription.recupererDescription(doc.IdTypeDocument, ListeDescription.listTypeDocument), 12, 75);

                //ajouter l image pour supprimer
                Image imgSuppr = new Image();
                imgSuppr.Width = 25;
                imgSuppr.Height = 25;
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.Margin = new Thickness(15,0,0,0);
                imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += supprimerDocument;
                imgSuppr.DataContext = doc.Id;

                hPanel.Children.Add(imgSuppr);

                //ajouter le bouton au stackPanel principal      
                ListeDocumentVue.Children.Add(hPanel);

                i++;
            }
        }

        //ajouter entrevue
        public void ajouterEntrevueVue() {

            int i = 1;
            foreach(Entrevue entre in MonEtudiant.Entrevues) {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données
                // créer le bouton
                Entreprise entreprise = ManagerEntreprise.recupererProfilesEntreprises(entre.IdEntreprise);

                Button btn = new Button();
                if(i % 2 == 0)
                    btn.Style = style;
                else
                    btn.Style = style2;
                btn.DataContext = entre.Id;
                btn.Click += afficherEntrevue;

                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                hPanel.Width = 300;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //nom
                ajouterLabel(hPanel, entreprise.Nom, 12, 50);
                //date 
                ajouterLabel(hPanel, entre.DateEntrevue.ToShortDateString(), 10, 70);
                //Type 
                ajouterTextBlock(hPanel, ListeDescription.recupererDescription(entre.TypeEntrevue, ListeDescription.listTypeEntrevue), 10, 60);

                //resultat 
                ajouterLabel(hPanel, ListeDescription.recupererDescription(entre.Resultat, ListeDescription.listTypeResultat), 12, 60);


                //ajouter l image pour supprimer
                Image imgSuppr = new Image();
                imgSuppr.Width = 25;
                imgSuppr.Height = 25;
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += supprimerEntrevue;
                imgSuppr.DataContext = entre.Id;


                hPanel.Children.Add(imgSuppr);

                //ajouter le bouton au stackPanel principal

                btn.Content = hPanel;
                ListeEntrevueVue.Children.Add(btn);


                i++;
            }
        }

        //methode pour ajouter un label dynamique 
        private void ajouterTextBlock(StackPanel hPanel, string content, int fontSize, int width) {

            TextBox label = new TextBox();
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 0, 151, 229));
            label.FontSize = fontSize;
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

        //evenement dans la vue

        //Stage
        private void afficherStage(object sender, RoutedEventArgs e) {
            ListerStagesVue stageVue = new ListerStagesVue(User, (int)((Button)sender).DataContext);
            stageVue.Show();
            this.Close();
        }
        private void supprimerStage(object sender, RoutedEventArgs e) {

        }
        private void BtnAjouterStagiare_Click(object sender, RoutedEventArgs e)
        {
            // a ameliorer, rajouter l entreprise
            this.Visibility = System.Windows.Visibility.Hidden;
            AjouterStageVue stagevue = new AjouterStageVue(User);
            stagevue.Show();
            this.Close();
        }

        //communication

        private void afficherCommunication(object sender, RoutedEventArgs e) {
            MessageBox.Show(((Communication)((Button)sender).DataContext).Id.ToString());
            ListerCommunicationsVue communicationVue = new ListerCommunicationsVue(User, (Communication)((Button)sender).DataContext, true);
            communicationVue.Show();
            this.Close();
        }

        private void supprimerCommunication(object sender, RoutedEventArgs e) {
            int idCommunication = (int)(((Image)sender).DataContext);

            Communication com = new Communication();
            com.Id = idCommunication;

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer cette communication ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (ret == MessageBoxResult.Yes)
            {

                ManagerCommunication.supprimerCommunicationEtudiantUtilisateur(com);

                MessageBox.Show("Communication supprimée.", "Suppression d'une communication", MessageBoxButton.OK, MessageBoxImage.Information);

                MonEtudiant.Communications = ManagerCommunication.recupererCommunicationEtudiantUtilisateurParId(MonEtudiant.Id);

                ListeCommunicationsVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste             

                if (MonEtudiant.Communications != null)
                    ajouterCommunicationVue();

            }
            else if (ret == MessageBoxResult.No)
            {
                MessageBox.Show("La communication n'a pas été supprimée.", "Suppression d'une communication", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
        private void BtnAjouterCommunication_Click(object sender, RoutedEventArgs e) {
            this.Visibility = System.Windows.Visibility.Hidden;
            AjouterCommunicationVue com = new AjouterCommunicationVue(User);
            com.Show();
            this.Close();
        }


        //document
        //document        
        private void BtnAjouterDocument_Click(object sender, RoutedEventArgs e)
        {

            AjouterDocumentEtudiantVue document = new AjouterDocumentEtudiantVue(User, MonEtudiant.Id);
            document.ShowDialog();
            if (document.IsModified)
            {
                MonEtudiant.Documents = ManagerDocument.recupererDocumentEtudiant(MonEtudiant.Id);

                ListeDocumentVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste
                ajouterDocumentsVue();
            }
            document.Close();


        }
        private void supprimerDocument(object sender, RoutedEventArgs e)
        {
            int idDocument = (int)(((Image)sender).DataContext);

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer ce document ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (ret == MessageBoxResult.Yes)
            {

                ManagerDocument.supprimerDocumentEtudiant(idDocument);

                MessageBox.Show("Document supprimé.", "Suppression d'un document", MessageBoxButton.OK, MessageBoxImage.Information);

                MonEtudiant.Documents = ManagerDocument.recupererDocumentEtudiant(MonEtudiant.Id);

                ListeDocumentVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste
                if (MonEtudiant.Documents != null)
                    ajouterDocumentsVue();


            }
            else if (ret == MessageBoxResult.No)
            {
                MessageBox.Show("Le document n'a pas été supprimé!", "Suppression d'un document", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }
     
        //entrevue
        private void afficherEntrevue(object sender, RoutedEventArgs e) {
            int idEntrevue = (int)(((Button)sender).DataContext);

            ProfileEntrevueEtudiantVue entrevue = new ProfileEntrevueEtudiantVue(User, idEntrevue);
            entrevue.ShowDialog();
            if (entrevue.IsModified)
            {
                MonEtudiant.Entrevues = ManagerEntrevue.recupererEntrevuesParIdEtudiant(MonEtudiant.Id);

                ListeEntrevueVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste
                if(MonEtudiant.Entrevues != null)
                ajouterEntrevueVue();
            }
            entrevue.Close();
        }
        private void BtnAjouterEntrevue_Click(object sender, RoutedEventArgs e) {
            AjouterEntrevueEtudiantVue entrevue = new AjouterEntrevueEtudiantVue(User, MonEtudiant.Id);
            entrevue.ShowDialog();
            if (entrevue.IsModified)
            {
                MonEtudiant.Entrevues = ManagerEntrevue.recupererEntrevuesParIdEtudiant(MonEtudiant.Id);

                ListeEntrevueVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste
                ajouterEntrevueVue();
            }
            entrevue.Close();
        }

        private void supprimerEntrevue(object sender, RoutedEventArgs e) {
            int idEntrevue = (int)(((Image)sender).DataContext);

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer cette entrevue ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (ret == MessageBoxResult.Yes)
            {

                ManagerEntrevue.supprimerEntrevueParId(idEntrevue);

                MessageBox.Show("Entrevue supprimée.", "Suppression d'une entrevue", MessageBoxButton.OK, MessageBoxImage.Information);

                MonEtudiant.Entrevues = ManagerEntrevue.recupererEntrevuesParIdEtudiant(MonEtudiant.Id);

                ListeEntrevueVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste
             
                if (MonEtudiant.Entrevues != null)
                    ajouterEntrevueVue();


            }
            else if (ret == MessageBoxResult.No)
            {
                MessageBox.Show("L'entrevue n'a pas été supprimé.", "Suppression d'une entrevue", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }


        //etudiant
        private void BtnModifierEtudiant_Click(object sender, RoutedEventArgs e) {
            ModificationEtudiantVue modificationVue = new ModificationEtudiantVue(User, MonEtudiant);
            modificationVue.Show();
            this.Close();
        }


        private void BtnSupprimerEtudiant_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult ret = MessageBox.Show(this, "etes vous sure de vouloir supprimer cet etudiant ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {

                ManagerEtudiant.supprimerEtudiant(MonEtudiant.Id);

                MessageBox.Show("Etudiant supprimé.", "Suppression d'un étudiant", MessageBoxButton.OK, MessageBoxImage.Information);

                Acceuil vue = new Acceuil(User);
                vue.Show();
                this.Close();


            } else if(ret == MessageBoxResult.No) {
                MessageBox.Show("L'étudiant n'a pas été supprimé.", "Suppression d'un étudiant", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
     

        //evenement menu gauche 
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
