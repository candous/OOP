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
    /// Logique d'interaction pour CompteVue.xaml
    /// </summary>
    public partial class CompteVue : Window {

        public Utilisateur User {
            get;
            set;
        }
     //   List<string> listTypeUtilisateur;

        //liste de tous les utilisateurs
        List<Utilisateur> lesUtlisateurs;

        //Utlisateur a modifier
        Utilisateur utlisateurAModifier;

        public CompteVue(Utilisateur user) {

            InitializeComponent();
            this.User = user;
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
            utlisateurAModifier = new Utilisateur();
            List<string> listTypeUtilisateur = new List<string>();

            foreach(IdDescription id in ListeDescription.listTypeUlisateur)
                listTypeUtilisateur.Add(id.Description);

            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;

            ChoixTypeUtlisateur.ItemsSource = listTypeUtilisateur;

            lesUtlisateurs = ManagerUtilisateur.recupererListUtilisateur();

            if(lesUtlisateurs != null)
                ajouterStageVueEtudiant();

        }
        // methode pour ajouter les listes dynamiques
        public void ajouterStageVueEtudiant() {

            int i = 0;


            foreach(Utilisateur utlisateur in lesUtlisateurs) {
                //MessageBox.Show(utlisateur.Nom);
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données
                // créer le bouton

                Button btn = new Button();
                if(i % 2 == 0)
                    btn.Background = new SolidColorBrush(Color.FromArgb(100, 240, 238, 239));
                else
                    btn.Background = null;
                btn.BorderBrush = null;
                btn.DataContext = utlisateur;
                btn.Click += afficherDetailsUtlisateur;
                btn.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                btn.VerticalAlignment = System.Windows.VerticalAlignment.Top;

                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                //hPanel.Width = 148;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Top;

                //nom utlisateur
                ajouterLabel(hPanel, utlisateur.Nom, 15, 135);

                //Type Utlisateur

                ajouterLabel(hPanel, ListeDescription.recupererDescription(utlisateur.IdTypeUtilisateur, ListeDescription.listTypeUlisateur), 15, 180);

                //chk lire
                CheckBox lire = new CheckBox();
                lire.IsEnabled = false;
                lire.Width = 50;
                if(utlisateur.PeutLire == true)
                    lire.IsChecked = true;
                hPanel.Children.Add(lire);

                //chk ecrire
                CheckBox ecrire = new CheckBox();
                ecrire.IsEnabled = false;
                ecrire.Width = 50;
                if(utlisateur.PeutEcrire == true)
                    ecrire.IsChecked = true;
                hPanel.Children.Add(ecrire);

                //chk creer
                CheckBox creer = new CheckBox();
                creer.IsEnabled = false;
                creer.Width = 50;
                if(utlisateur.PeutCreerUtilisateur == true)
                    creer.IsChecked = true;
                hPanel.Children.Add(creer);


                //ajouter l image pour supprimer
                Image imgSuppr = new Image();
                imgSuppr.DataContext = utlisateur.Id;
                imgSuppr.Margin = new Thickness(40, 0, 0, 0);
                imgSuppr.Width = 25;
                imgSuppr.Height = 25;
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += supprimerUtlisateur;
                hPanel.Children.Add(imgSuppr);

                //ajouter le bouton au stackPanel principal
                btn.Content = hPanel;
                ListeStageVue.Children.Add(btn);
                i++;
            }

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

        private void ajouterTextBlock(StackPanel hPanel, string content, int fontSize, int width) {

            TextBox label = new TextBox();
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 20, 20, 20));
            label.BorderBrush = null;
            label.BorderThickness = new Thickness(0);
            label.FontSize = fontSize;
            label.Width = width;
            label.Background = null;
            //label.IsEnabled = false;
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            label.TextWrapping = TextWrapping.Wrap;
            label.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            label.Text = content;
            hPanel.Children.Add(label);
        }

        //Menu Etudiant
        private void EtudiantMenu_Click(object sender, RoutedEventArgs e) {
            this.Visibility = System.Windows.Visibility.Hidden;
            ListerEtudiantsVue listeretudiant = new ListerEtudiantsVue(User);
            listeretudiant.Show();
            this.Close();
        }

        //Menu Entreprise
        private void EntrepriseMenu_Click(object sender, RoutedEventArgs e) {
            this.Visibility = System.Windows.Visibility.Hidden;
            ListerEntreprisesVue listerentreprise = new ListerEntreprisesVue(User);
            listerentreprise.Show();
            this.Close();
        }

        //Menu Communication
        private void CommunicationMenu_Click(object sender, RoutedEventArgs e) {
            this.Visibility = System.Windows.Visibility.Hidden;
            ListerCommunicationsVue communication = new ListerCommunicationsVue(User);
            communication.Show();
            this.Close();
        }

        //Menu Stage
        private void StageMenu_Click(object sender, RoutedEventArgs e) {
            this.Visibility = System.Windows.Visibility.Hidden;
            ListerStagesVue listerStages = new ListerStagesVue(User);
            listerStages.Show();
            this.Close();
        }

        // Menu Statistique
        private void StatistiquesMenu_Click(object sender, RoutedEventArgs e) {
            this.Visibility = System.Windows.Visibility.Hidden;
            StatistiquesVue vue = new StatistiquesVue(User);
            vue.Show();
            this.Close();
        }

        // Supprimer Utlisateur
        private void supprimerUtlisateur(object sender, RoutedEventArgs e) {


            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer cet Utilisateur?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {
                int idImage = (int)((Image)sender).DataContext;

                int id = (int)((Image)sender).DataContext;

                ManagerUtilisateur.SupprimerUtilisateur(id);
                lesUtlisateurs = ManagerUtilisateur.recupererListUtilisateur();

                ListeStageVue.Children.Clear();
                if(lesUtlisateurs != null)
                    ajouterStageVueEtudiant();
            } else
                MessageBox.Show("Suppresion annulée", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);


        }

        // Afficher detail Utlisateur
        private void afficherDetailsUtlisateur(object sender, RoutedEventArgs e) {
            utlisateurAModifier = (Utilisateur)((Button)sender).DataContext;
            ChoixUtilisateurName.Text = utlisateurAModifier.Nom;
            ChoixModificationName.Text = utlisateurAModifier.MotDePasse;
            ChoixTypeUtlisateur.SelectedItem = ListeDescription.recupererDescription(utlisateurAModifier.IdTypeUtilisateur, ListeDescription.listTypeUlisateur);

        }

        //button modifier
        private void BtnValiderCompte_Click(object sender, RoutedEventArgs e) {
            if(ChoixUtilisateurName.Text.Length <= 0 || ChoixModificationName.Text.Length <= 0 || ChoixTypeUtlisateur.SelectedItem == null)
                MessageBox.Show("Veuillez remplir tous les champs necessaires : ", "Modification Utlisateur", MessageBoxButton.OK, MessageBoxImage.Information);
            else {
                utlisateurAModifier.Nom = ChoixUtilisateurName.Text;
                utlisateurAModifier.MotDePasse = ChoixModificationName.Text;
                utlisateurAModifier.IdTypeUtilisateur = ListeDescription.recupererIdDescription(ChoixTypeUtlisateur.SelectedItem.ToString(), ListeDescription.listTypeUlisateur);

                if(utlisateurAModifier.IdTypeUtilisateur == 1) {

                    utlisateurAModifier.PeutLire = true;
                    utlisateurAModifier.PeutEcrire = true;
                    utlisateurAModifier.PeutCreerUtilisateur = true;

                } else if(utlisateurAModifier.IdTypeUtilisateur == 2) {
                    utlisateurAModifier.PeutLire = true;
                    utlisateurAModifier.PeutEcrire = true;
                    utlisateurAModifier.PeutCreerUtilisateur = false;

                } else {
                    utlisateurAModifier.PeutLire = true;
                    utlisateurAModifier.PeutEcrire = false;
                    utlisateurAModifier.PeutCreerUtilisateur = false;

                }

                ManagerUtilisateur.modifierUtilisateur(utlisateurAModifier);

                lesUtlisateurs = ManagerUtilisateur.recupererListUtilisateur();

                ListeStageVue.Children.Clear();
                if(lesUtlisateurs != null)
                    ajouterStageVueEtudiant();
                MessageBox.Show("Utilisateur Modifié ", "Modification Utlisateur", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //Ajouter Utlisateur
        private void BtnAjouterCommunication_Click(object sender, RoutedEventArgs e) {
            ajouterUtlisateur ajouterUtilisateurVue = new ajouterUtlisateur(User);
            ajouterUtilisateurVue.ShowDialog();

            //rafraichir la liste
            lesUtlisateurs = ManagerUtilisateur.recupererListUtilisateur();
            ListeStageVue.Children.Clear();
            if(lesUtlisateurs != null)
                ajouterStageVueEtudiant();
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
