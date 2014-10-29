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
    /// Logique d'interaction pour ListerEntreprisesVue.xaml
    /// </summary>
    public partial class ListerEntreprisesVue : Window
    {
        Utilisateur User;

        List<Entreprise> lesEntreprises;
        List<string> lL;
        List<string> lF;
        List<string> lI;
        List<string> lT;

        public ListerEntreprisesVue(Utilisateur leUser)
        {

            InitializeComponent();
            resultat.Visibility = System.Windows.Visibility.Hidden;
            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;

            lL = new List<string>();
            lF = new List<string>();
            lI = new List<string>();
            lT = new List<string>();
            

            foreach (Langue id in ListeDescription.listLangue)
                lL.Add(id.Description);

            foreach (Formation id in ListeDescription.listFormations)
                lF.Add(id.Description); 

            foreach (IdDescription id in ListeDescription.listTechnologie)
                lI.Add(id.Description);

            foreach (IdDescription id in ListeDescription.listInterets)
                lT.Add(id.Description);

            // pb recuperer bon fichier
            lesEntreprises = ManagerEntreprise.recupererListeProfilesEntreprises();

            Console.WriteLine(lesEntreprises.Count);

            ChoixLangue.ItemsSource = lL;
            ChoixFormation.ItemsSource = lF;
            ChoixInteret.ItemsSource = lI;
            ChoixTechnologie.ItemsSource = lT;
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
                BtnAjouterEntreprise.Visibility = System.Windows.Visibility.Hidden;
                StatistiquesMenu.Visibility = System.Windows.Visibility.Hidden;
            }
            userName.Content = User.Nom;
            ajouterEntrepriseVue();
        }

    
        public void ajouterEntrepriseVue()
        {

            Style style = this.FindResource("BtnStyleNoHover") as Style;

            int nbEntreprise = 0;
            int nbEntrepriseMax = lesEntreprises.Count;
            StackPanel hPanel = null;

            foreach (Entreprise entreprise in lesEntreprises)
            {
                
                // tous les 4 etudiants on créé un panel horizontal
                if (nbEntreprise % 4 == 0 || nbEntreprise == 0)
                {
                    hPanel = new StackPanel();
                    hPanel.Orientation = Orientation.Horizontal;
                    hPanel.Width = 594;
                }

                // créer le bouton
                Button btn = new Button();                
                btn.Style = style;
                btn.DataContext = entreprise.Id;
                btn.Click += afficherDetailsEntreprise;

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
                nomPrenom.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                nomPrenom.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                nomPrenom.FontFamily = new System.Windows.Media.FontFamily("Verdana");
                nomPrenom.Content = entreprise.Nom;
                //ajouter Nom Prenom
                vPanel.Children.Add(nomPrenom);
                                           
                //ajouter Formation
                ajouterTextBlock(vPanel, entreprise.Secteur, 15);

                btn.Content = vPanel;
                // ajouterEtudiantVue le bouton au paneau horizontal
                hPanel.Children.Add(btn);


                //tous les 4 etudiants on ajoute le hpanel a la liste des etudiants
                if (nbEntreprise % 4 == 0 || nbEntreprise == nbEntrepriseMax && nbEntreprise != 0)
                    ListeEntreprisesVue.Children.Add(hPanel);


                nbEntreprise++;
            }




        }

        private void ajouterTextBlock(StackPanel hPanel, string content, int fontSize) {

            TextBox label = new TextBox();
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 20, 20, 20));
            label.BorderBrush = null;
            label.BorderThickness = new Thickness(0);
            label.FontSize = fontSize;
            label.Background = null;
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            //label.IsEnabled = false;
            label.TextWrapping = TextWrapping.Wrap;
            label.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            label.Text = content;
            hPanel.Children.Add(label);
        }

        // evenement au click des boutons
        private void afficherDetailsEntreprise(object sender, RoutedEventArgs e)
        {
            int id = (int)(((Button)sender).DataContext);

            
            ProfileEntrepriseVue entreprise = new ProfileEntrepriseVue(User,id);
            entreprise.Show();
            this.Close();
        }

        //evenement pout rechercher une entreprise
        private void BtnValiderRechercher_Click(object sender, RoutedEventArgs e)
        {
            resultat.Visibility = System.Windows.Visibility.Hidden; 
            String ChoixFormationValeur = (String)ChoixFormation.SelectedValue;
            String ChoixInteretValeur = (String)ChoixInteret.SelectedValue;
            String ChoixTechnologieValeur = (String)ChoixTechnologie.SelectedValue;
            String ChoixLangueValeur = (String)ChoixLangue.SelectedValue;
            
            int idFormation = ListeDescription.recupererIdFormation(ChoixFormationValeur);
            int idLangue = ListeDescription.recupererIdLangue(ChoixLangueValeur);
            int idInteret = ListeDescription.recupererIdDescription(ChoixInteretValeur, ListeDescription.listInterets);
            int idTechnologie = ListeDescription.recupererIdDescription(ChoixInteretValeur, ListeDescription.listTechnologie);

            String idFormationString = idFormation.ToString();
            String idInteretString = idInteret.ToString();
            String idLangueString = idLangue.ToString();
            String idTechnologieString = idTechnologie.ToString();

            //MessageBox.Show(idStatusString + "     " + idFormationString);
            String ChoixNomValeur = ChoixNom.Text;
            String ChoixCourrielValeur = ChoixCourriel.Text;
            String ChoixVilleValeur = ChoixVille.Text;



            Dictionary<String, String> dictionnay = new Dictionary<String, String>();
            //dictionnay.Add("statusValeur", ChoixStatusValeur);
            //dictionnay.Add("ChoixFormation", ChoixFormationValeur);


            if(idFormation != -1)
                dictionnay.Add("idFormation", idFormationString);

            if (idInteret != -1)
                dictionnay.Add("idInteret", idInteretString);

            if (idLangue != -1)
                dictionnay.Add("idLangue", idLangueString);

            if (idTechnologie != -1)
                dictionnay.Add("idTechnologie", idTechnologieString);
                
            dictionnay.Add("nom", ChoixNomValeur);
            dictionnay.Add("courriel", ChoixCourrielValeur);
            dictionnay.Add("ville", ChoixVilleValeur);

            

            lesEntreprises = ManagerEntreprise.recupererListeProfilesEntreprisesSelonRecherche(dictionnay);

            if (lesEntreprises != null)
            {
               
                ListeEntreprisesVue.Children.Clear();
                ajouterEntrepriseVue();
            }
            else
            {
                
                ListeEntreprisesVue.Children.Clear();
                resultat.Visibility = System.Windows.Visibility.Visible;
                lesEntreprises = ManagerEntreprise.recupererListeProfilesEntreprises();
            }
        }

        // ajouter une entreprise
        private void BtnAjouterEntreprise_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Hidden;
            AjouterEntrepriseVue ajouterentreprise = new AjouterEntrepriseVue(User);
            ajouterentreprise.Show();
            this.Close();
        }


        // evenement au click du menu gauche

        // evenement au click du menu gauche

        private void EtudiantMenu_Click(object sender, RoutedEventArgs e) {
            
            ListerEtudiantsVue listeretudiant = new ListerEtudiantsVue(User);
            listeretudiant.Show();
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
