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

namespace Views
{
    /// <summary>
    /// Interaction logic for Acceuil.xaml
    /// </summary>
    public partial class Acceuil : Window
    {

        List<Etudiant> etudiantsAcceuil;
        public Utilisateur User
        {
            get;
            set;
        }


        public Acceuil(Utilisateur user)
        {
            InitializeComponent();
            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;

            User = user;

            ////PERMISSIONS
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
                StatistiquesMenu.Visibility = System.Windows.Visibility.Hidden;
            }

            etudiantsAcceuil = ManagerEtudiant.recupererListeProfilesEtudiantsRechercheStage();


            NbEtudiantRecherche.Content = ManagerStatistique.recupererNbEtudiantsRecherche();
            NbEntrevue10jours.Content = ManagerStatistique.recupererNbEntrevuesDans10Jours();
            NbCommunicationAttente.Content = ManagerCommunication.recupererNbCommunitionEnAttenteUtilisateurEtudiant();
            NbEntrepriseEnregistre.Content = ManagerStatistique.recupererNbEntreprisesEnregistrees();

            // Console.WriteLine(etudiantsAcceuil.Count);
            ajouterEtudiantVue();

            //Console.WriteLine(NbEtudiantRecherche);
            userName.Content = User.Nom;
            
           
        }


        public void ajouterEtudiantVue()
        {
            Style style = this.FindResource("BtnStyleNoHover") as Style;
            int nbEtudiant = 0;
            int nbEtudiantMax = etudiantsAcceuil.Count;
            StackPanel hPanel = null;

            foreach (Etudiant etudiant in etudiantsAcceuil)
            {

                // tous les 4 etudiants on créé un panel horizontal
                if (nbEtudiant % 3 == 0 || nbEtudiant == 0)
                {
                    hPanel = new StackPanel();
                    hPanel.Orientation = Orientation.Horizontal;
                   
                }

                // créer le bouton
                Button btn = new Button();
                btn.Background = null;
                btn.BorderBrush = null;
                btn.Style = style;
                btn.Width = 270;
                btn.Click += afficherDetailsEtudiant;
                btn.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                btn.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                btn.DataContext = etudiant.Id;

                //layout du bouton
                StackPanel vPanel = new StackPanel();
                vPanel.Orientation = Orientation.Vertical;
                //vPanel.Width = 270;
               

                //ellipse et image du contact
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 84;
                ellipse.Height = 84;
                ellipse.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                ellipse.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                ellipse.Margin = new Thickness(30, 10, 0, 0);

                ImageBrush imgContact = new ImageBrush();
                imgContact.Stretch = Stretch.Fill;
                imgContact.ImageSource = new BitmapImage(new Uri(@"" + etudiant.PhotoURL, UriKind.RelativeOrAbsolute));
                ellipse.Fill = imgContact;
                //ajout image a vpanel
                vPanel.Children.Add(ellipse);

                //créer un texte pour nom prenom
                Label nomPrenom = new Label();
                nomPrenom.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                nomPrenom.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                nomPrenom.Foreground = new SolidColorBrush(Color.FromArgb(100, 0, 151, 229));
                nomPrenom.FontSize = 20;
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
                if (nbEtudiant % 3 == 0 || nbEtudiant == nbEtudiantMax && nbEtudiant != 0)
                    ListeEtudiantsVue.Children.Add(hPanel);

                nbEtudiant++;
            }
        }

        private void ajouterTextBlock(StackPanel hPanel, string content, int fontSize) {

            TextBox label = new TextBox();
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 20, 20, 20));
            label.FontSize = fontSize;
            label.Background = null;
            label.BorderBrush = null;
            label.TextWrapping = TextWrapping.Wrap;
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            label.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            label.Text = content;
            hPanel.Children.Add(label);
        }

        //evenement pour page acceuil
        private void afficherDetailsEtudiant(object sender, RoutedEventArgs e)
        {
            int idEdtudiant = (int)((Button)sender).DataContext;

            ProfileEtudiantVue vue = new ProfileEtudiantVue(User, idEdtudiant);
            vue.Show();
            this.Close();
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

        // evenement pour menu configuration
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

        private void Image_MouseDown(object sender, MouseButtonEventArgs e) {
            Acceuil vue = new Acceuil(User);
            vue.Show();
            this.Close();
        }

    }
}
