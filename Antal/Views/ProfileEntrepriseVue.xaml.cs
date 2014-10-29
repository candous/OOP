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

    public partial class ProfileEntrepriseVue : Window {
        Utilisateur User;

        public Entreprise MonEntreprise {
            get;
            set;
        }

        private Style style;
        private Style style2;

        public ProfileEntrepriseVue(Utilisateur user, int idEntreprise) {
            InitializeComponent();
            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;
            this.User = user;

            style = this.FindResource("BtnStyleNoHover") as Style;
            style2 = this.FindResource("BtnStyleNoHover2") as Style;

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
                BtnSupprimerEntreprise.Visibility = System.Windows.Visibility.Hidden;
                BtnModifierEntreprise.Visibility = System.Windows.Visibility.Hidden;
                StatistiquesMenu.Visibility = System.Windows.Visibility.Hidden;

            }
            userName.Content = User.Nom;
            this.MonEntreprise = ManagerEntreprise.recupererEntreprise(idEntreprise);


            if(MonEntreprise.Representants != null)
                ajouterRepresentantVue();
            if(MonEntreprise.FormationsRecherchees != null)
                ajouterFormationVue();
            if(MonEntreprise.TechnologiesRecherchees != null)
                ajouterTechnologieVue();
            if(MonEntreprise.InteretsRecherches != null)
                ajouterInteretVue();

            if(MonEntreprise.stages != null)
                ajouterStageVue();

            if(MonEntreprise.Documents != null)
                ajouterDocumentsVue();

            if(MonEntreprise.Communications != null)
                ajouterCommunicationVue();

            if(MonEntreprise.Entrevues != null)
                ajouterEntrevueVue();

            //binder info avec Mon entreprise
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(MonEntreprise.ImageLogo, UriKind.RelativeOrAbsolute));
            affichePhoto.Fill = brush;

            nomVue.Content = MonEntreprise.Nom;
            langueVue.Content = ListeDescription.recupererLaLangue(MonEntreprise.Langue);
            villeVue.Content = MonEntreprise.Adresse + " " +  MonEntreprise.Ville;
            tel1Vue.Content = MonEntreprise.Telephone1;
            tel2Vue.Content = MonEntreprise.Telephone2;
            tel3Vue.Content = MonEntreprise.Telephone3;

            emailVue.Content = MonEntreprise.Email;

            commentaireVue.Text = MonEntreprise.Commentaire;

        }


        //pour ajouter les representant a la vue de maniere dynamique
        public void ajouterRepresentantVue() {
            int i = 1;


            foreach(Representant representant in MonEntreprise.Representants) {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données
                // créer le bouton
                Button btn = new Button();
                if(i % 2 == 0)
                    btn.Style = style;
                else
                    btn.Style = style2;
                btn.DataContext = representant.Id;
                btn.Click += afficherRepresentant;

                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                hPanel.Width = 531;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //nom prenom representant
                ajouterLabel(hPanel, representant.Nom + " " + representant.Prenom, 15, 140);

                //courriel representant
                ajouterLabel(hPanel, representant.Courriel, 15, 160);

                //telephone representant  !!!!!!!!!!!!!!!!!!!!  pb avec la BD a disucter
                ajouterLabel(hPanel, representant.Telephone1, 15, 130);

                //ajouter l image pour supprimer
                Image imgSuppr = new Image();
                imgSuppr.Width = 25;
                imgSuppr.Height = 25;
                imgSuppr.Margin = new Thickness(15, 0, 0, 0);
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += supprimerRepresentant;
                hPanel.Children.Add(imgSuppr);
                imgSuppr.DataContext = representant.Id;

                //ajouter le bouton au stackPanel principal

                btn.Content = hPanel;
                ListeRepresentantVue.Children.Add(btn);
                i++;
            }
        }

        //probleme entite Entreprise pas de liste de formation interet technologie
        private void ajouterFormationVue() {
            foreach(IdDescription desc in MonEntreprise.FormationsRecherchees)
                ajouterLabel(listeFormationRecherche, desc.Description);
        }

        private void ajouterTechnologieVue() {
            foreach(IdDescription desc in MonEntreprise.TechnologiesRecherchees)
                ajouterLabel(listeTechnologieRecherche, desc.Description);

        }

        private void ajouterInteretVue() {
            foreach(IdDescription desc in MonEntreprise.InteretsRecherches)
                ajouterLabel(listeInteretRecherche, desc.Description);

        }

        public void ajouterStageVue() {

            int i = 1;
            foreach(Stage stage in MonEntreprise.stages) {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données

                Etudiant stageEtudiant = ManagerEtudiant.recupererProfilesEtudiant(stage.IdEtudiant);

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
                ajouterLabel(hPanel, stageEtudiant.Nom + " " + stageEtudiant.Prenom, 15, 120);

                ////date debut 
                Object dateD = stage.DateDebut;
                DateTime dateDTmps = (DateTime)dateD;
                ajouterLabel(hPanel, dateDTmps.ToShortDateString(), 15, 120);

                ////date Fin 
                Object dateF = stage.DateFin;
                DateTime dateFTmps = (DateTime)dateF;
                ajouterLabel(hPanel, dateFTmps.ToShortDateString(), 15, 120);

                //retenu   
                CheckBox retenu = new CheckBox();
                retenu.IsEnabled = false;
                retenu.VerticalAlignment = System.Windows.VerticalAlignment.Center;
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
                //hPanel.Children.Add(imgSuppr);
                //imgSuppr.DataContext = stage.Id;

                //ajouter le bouton au stackPanel principal

                btn.Content = hPanel;
                ListeStagetVue.Children.Add(btn);


                i++;
            }
        }

        //peut pas tester pas de données dans bdd
        public void ajouterDocumentsVue() {

            int i = 1;
            foreach(Document doc in MonEntreprise.Documents) {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données 
               
                //layout du bouton
                StackPanel hPanel = new StackPanel();
               
                    hPanel.Background = null;
                hPanel.Orientation = Orientation.Horizontal;
                hPanel.Width = 300;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //type 
                ajouterLabel(hPanel, doc.Titre, 12, 75);

                //date 
                Object date = doc.DateAjout;
                DateTime dateAjout = (DateTime)date;

                ajouterLabel(hPanel, dateAjout.ToShortDateString(), 12, 75);

                //Type   
                ajouterLabel(hPanel, ListeDescription.recupererDescription(doc.IdTypeDocument, ListeDescription.listTypeDocument), 12, 75);

                //ajouter l image pour supprimer
                Image imgSuppr = new Image();
                imgSuppr.Width = 25;
                imgSuppr.Margin = new Thickness(15, 0, 0, 0);
                imgSuppr.Height = 25;
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += supprimerDocument;
                imgSuppr.DataContext = doc.Id;

                hPanel.Children.Add(imgSuppr);

                //ajouter le bouton au stackPanel principal      
                ListeDocumentVue.Children.Add(hPanel);

                i++;
            }
        }

        public void ajouterCommunicationVue() {

            int i = 1;
            foreach(Communication com in MonEntreprise.Communications) {
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
                ajouterLabel(hPanel, com.DateCommunication.ToShortDateString(), 12, 75);

                //type 
                ajouterLabel(hPanel, ListeDescription.recupererDescription(com.TypeCommunication, ListeDescription.listTypeCommunication), 12, 75);

                //status   
                ajouterLabel(hPanel, ListeDescription.recupererDescription(com.StatusCommunication, ListeDescription.listStatusCommunication), 12, 75);

                //ajouter l image pour supprimer
                Image imgSuppr = new Image();
                imgSuppr.Width = 25;
                imgSuppr.Height = 25;
                imgSuppr.Margin = new Thickness(15, 0, 0, 0);
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += supprimerCommunication;
                hPanel.Children.Add(imgSuppr);
                imgSuppr.DataContext = com.Id;

                //ajouter le bouton au stackPanel principal

                btn.Content = hPanel;
                ListeCommunicationsVue.Children.Add(btn);
                i++;
            }
        }


        public void ajouterEntrevueVue() {

            int i = 1;
            foreach(Entrevue entre in MonEntreprise.Entrevues) {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données
                // créer le bouton
                Etudiant etudiant = ManagerEtudiant.recupererProfilesEtudiant(entre.IdEtudiant);

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
                ajouterLabel(hPanel, etudiant.Nom, 12, 50);
                //date 
                ajouterLabel(hPanel, entre.DateEntrevue.ToShortDateString(), 10, 70);
                //Type 
                ajouterLabel(hPanel, ListeDescription.recupererDescription(entre.TypeEntrevue, ListeDescription.listTypeEntrevue), 10, 60);

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


        private void ajouterLabel(StackPanel hPanel, string content) {

            Label label = new Label();
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 0, 151, 229));
            label.FontSize = 15;
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

        //evenement sur les differents elements
        //representant
        private void afficherRepresentant(object sender, RoutedEventArgs e) {
            int idRepresentant = (int)(((Button)sender).DataContext);

            ProfileRepresentantVue representant = new ProfileRepresentantVue(User, idRepresentant);
            representant.ShowDialog();
            if(representant.IsModified) {
                MonEntreprise.Representants = ManagerEntreprise.recupererRepresentant(MonEntreprise.Id);

                ListeRepresentantVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste
                ajouterRepresentantVue();
            }
            representant.Close();
        }
        private void supprimerRepresentant(object sender, RoutedEventArgs e) {
            int idRepresentant = (int)(((Image)sender).DataContext);


            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer ce représentant ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {

                ManagerEntreprise.supprimerRepresentant(idRepresentant);


                MonEntreprise.Representants = ManagerEntreprise.recupererRepresentant(MonEntreprise.Id);

                ListeRepresentantVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste

                if(MonEntreprise.Representants != null)
                    ajouterRepresentantVue();
                MessageBox.Show("Représentant supprimée.", "Suppression d'un représentant", MessageBoxButton.OK, MessageBoxImage.Information);


            } else if(ret == MessageBoxResult.No) {
                MessageBox.Show("Le représentant n'a pas été supprimée.", "Suppression d'un représentant", MessageBoxButton.OK, MessageBoxImage.Information);

            }


        }
        private void BtnAjouterRepresentant_Click(object sender, RoutedEventArgs e) {
            AjouterRepresentant representant = new AjouterRepresentant(User, MonEntreprise.Id);
            representant.ShowDialog();
            if(representant.IsModified) {
                MonEntreprise.Representants = ManagerEntreprise.recupererRepresentant(MonEntreprise.Id);

                ListeRepresentantVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste
                ajouterRepresentantVue();
            }
            representant.Close();



        }
        //Stage
        private void afficherStage(object sender, RoutedEventArgs e) {

            ListerStagesVue stageVue = new ListerStagesVue(User, (int)((Button)sender).DataContext);
            stageVue.Show();
            this.Close();

        }
        private void supprimerStage(object sender, RoutedEventArgs e) {

        }
        private void BtnAjouterStagiare_Click(object sender, RoutedEventArgs e) {
            // a ameliorer, rajouter l entreprise
            this.Visibility = System.Windows.Visibility.Hidden;
            AjouterStageVue listeretudiant = new AjouterStageVue(User);
            listeretudiant.Show();
            this.Close();
        }

        //communication
        private void afficherCommunication(object sender, RoutedEventArgs e) {
            ListerCommunicationsVue communicationVue = new ListerCommunicationsVue(User, (Communication)((Button)sender).DataContext, false);
            communicationVue.Show();
            this.Close();
        }

        private void supprimerCommunication(object sender, RoutedEventArgs e) {
            int idCommunication = (int)(((Image)sender).DataContext);

            Communication com = new Communication();
            com.Id = idCommunication;

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer cette communication ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {

                ManagerCommunication.supprimerCommunicationEntrepriseUtilisateur(com);

                MessageBox.Show("Communication supprimée.", "Suppression d'une communication", MessageBoxButton.OK, MessageBoxImage.Information);

                MonEntreprise.Communications = ManagerCommunication.recupererCommunicationEntrepriseUtilisateurParId(MonEntreprise.Id);

                ListeCommunicationsVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste
                if(MonEntreprise.Communications != null)
                    ajouterCommunicationVue();

            } else if(ret == MessageBoxResult.No) {
                MessageBox.Show("La communication n'a pas été supprimé.", "Suppression d'une communication", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void BtnAjouterCommunication_Click(object sender, RoutedEventArgs e) {
            // a ameliorer, rajouter l entreprise
            this.Visibility = System.Windows.Visibility.Hidden;
            AjouterCommunicationVue com = new AjouterCommunicationVue(User);
            com.Show();
            this.Close();
        }
        //document        
        private void BtnAjouterDocument_Click(object sender, RoutedEventArgs e) {

            AjouterDocumentEntrepriseVue document = new AjouterDocumentEntrepriseVue(User, MonEntreprise.Id);
            document.ShowDialog();
            if(document.IsModified) {
                MonEntreprise.Documents = ManagerDocument.recupererDocumentEntreprise(MonEntreprise.Id);

                ListeDocumentVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste
                ajouterDocumentsVue();
            }
            document.Close();


        }
        private void supprimerDocument(object sender, RoutedEventArgs e) {
            int idDocument = (int)(((Image)sender).DataContext);

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer ce document ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {

                ManagerDocument.supprimerDocumentEntreprise(idDocument);

                MessageBox.Show("Document supprimé.", "Suppression d'un document", MessageBoxButton.OK, MessageBoxImage.Information);

                MonEntreprise.Documents = ManagerDocument.recupererDocumentEntreprise(MonEntreprise.Id);

                ListeDocumentVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste
                ajouterDocumentsVue();


            } else if(ret == MessageBoxResult.No) {
                MessageBox.Show("Le document n'a pas été supprimé.", "Suppression d'un document", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }
        //entrevue
        private void afficherEntrevue(object sender, RoutedEventArgs e) {
            int idEntrevue = (int)(((Button)sender).DataContext);

            ProfileEntrevueEntrepriseVue entrevue = new ProfileEntrevueEntrepriseVue(User, idEntrevue);
            entrevue.ShowDialog();
            if(entrevue.IsModified) {
                MonEntreprise.Entrevues = ManagerEntrevue.recupererEntrevuesParIdEntreprise(MonEntreprise.Id);

                ListeEntrevueVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste
                ajouterEntrevueVue();
            }
            entrevue.Close();
        }
        private void BtnAjouterEntrevue_Click(object sender, RoutedEventArgs e) {
            AjouterEntrevueEntrepriseVue entrevue = new AjouterEntrevueEntrepriseVue(User, MonEntreprise.Id);
            entrevue.ShowDialog();
            if(entrevue.IsModified) {
                MonEntreprise.Entrevues = ManagerEntrevue.recupererEntrevuesParIdEntreprise(MonEntreprise.Id);

                ListeEntrevueVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste
                ajouterEntrevueVue();
            }
            entrevue.Close();
        }


        private void supprimerEntrevue(object sender, RoutedEventArgs e) {
            int idEntrevue = (int)(((Image)sender).DataContext);

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer cette entrevue ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {

                ManagerEntrevue.supprimerEntrevueParId(idEntrevue);

                MessageBox.Show("Entrevue supprimée.", "Suppression d'une entrevue", MessageBoxButton.OK, MessageBoxImage.Information);

                MonEntreprise.Entrevues = ManagerEntrevue.recupererEntrevuesParIdEntreprise(MonEntreprise.Id);

                ListeEntrevueVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste
                if (MonEntreprise.Entrevues != null)
                ajouterEntrevueVue();


            } else if(ret == MessageBoxResult.No) {
                MessageBox.Show("L'entrevue n'a pas été supprimé.", "Suppression d'une entrevue", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }


        //entreprise event
        private void BtnSupprimerEntreprise_Click(object sender, RoutedEventArgs e) {

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer cette entreprise ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {

                ManagerEntreprise.supprimerEntreprise(MonEntreprise.Id);

                MessageBox.Show("Entreprise supprimée.", "Suppression d'une entreprise", MessageBoxButton.OK, MessageBoxImage.Information);

                Acceuil vue = new Acceuil(User);
                vue.Show();
                this.Close();


            } else if(ret == MessageBoxResult.No) {
                MessageBox.Show("L'entreprise n'a pas été supprimée.", "Suppression d'une entreprise", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }



        private void BtnModifierEntreprise_Click(object sender, RoutedEventArgs e) {
            this.Visibility = System.Windows.Visibility.Hidden;
            ModifierEntrepriseVue vue = new ModifierEntrepriseVue(User, MonEntreprise);
            vue.Show();
            this.Close();
        }


        //methode commune au autres pages
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
