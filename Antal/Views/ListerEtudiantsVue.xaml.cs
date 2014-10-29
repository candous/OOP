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
    /// Logique d'interaction pour ListerEtudiantsVue.xaml
    /// </summary>
    public partial class ListerEtudiantsVue : Window
    {
        Utilisateur User;

        List<Etudiant> lesEtudiants;
        List<string> lL;
        List<string> lF;
        List<string> lC;

        private Style style;

        public ListerEtudiantsVue(Utilisateur leUser)
        {

            InitializeComponent();
            resultat.Visibility = System.Windows.Visibility.Hidden;
            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;
            Dictionary<string, string> WhereCondition = new Dictionary<string, string>();
            lesEtudiants = ManagerEtudiant.recupererListeProfilesEtudiantsSelonRecherche(WhereCondition);

            style = this.FindResource("BtnStyleNoHover") as Style;

            lL = new List<string>();
            lC = new List<string>();
            lF = new List<string>();

            foreach (Langue id in ListeDescription.listLangue)
                lL.Add(id.Description);
          
            foreach (IdDescription id in ListeDescription.listStatusCarrieres)
                lC.Add(id.Description);
          
            foreach (Formation id in ListeDescription.listFormations)
                lF.Add(id.Description);

            //ChoixLangue.ItemsSource = lL;
            ChoixStatus.ItemsSource = lC;
            ChoixFormation.ItemsSource = lF;
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
                BtnAjouterEtudiant.Visibility = System.Windows.Visibility.Hidden;
                StatistiquesMenu.Visibility = System.Windows.Visibility.Hidden;
            }
            userName.Content = User.Nom;
            ajouterEtudiantVue();
        }     

        public void ajouterEtudiantVue()
        {
            int nbEtudiant = 0;
            int nbEtudiantMax = lesEtudiants.Count;
            StackPanel hPanel = null;

            foreach (Etudiant etudiant in lesEtudiants)
            {
                
                // tous les 4 etudiants on créé un panel horizontal
               if(nbEtudiant % 3 == 0 || nbEtudiant == 0) {
                    hPanel = new StackPanel();
                    hPanel.Orientation = Orientation.Horizontal;
                    hPanel.Width = 590;
                }

                // créer le bouton
                Button btn = new Button();
                btn.Style = style;
                btn.DataContext = etudiant.Id;
                btn.Click += afficherDetailsEtudiant;

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
                nomPrenom.FontSize = 18;
                nomPrenom.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                nomPrenom.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                nomPrenom.FontFamily = new System.Windows.Media.FontFamily("Verdana");
                nomPrenom.Content = etudiant.Prenom + " " + etudiant.Nom;
                //ajouter Nom Prenom
                vPanel.Children.Add(nomPrenom);

               
                //ajouter Formation
                ajouterTextBlock(vPanel, ListeDescription.recupererLaFormation(etudiant.IdFormation), 14);
               

                btn.Content = vPanel;
                // ajouterEtudiantVue le bouton au paneau horizontal
                hPanel.Children.Add(btn);


                //tous les 4 etudiants on ajoute le hpanel a la liste des etudiants                
                if (nbEtudiant % 3 == 0 || nbEtudiant == nbEtudiantMax &&nbEtudiant != 0)
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
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            label.BorderThickness = new Thickness(0);
            //label.IsEnabled = false;
            label.TextWrapping = TextWrapping.Wrap;
            label.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            label.Text = content;
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

        private void afficherDetailsEtudiant(object sender, RoutedEventArgs e)
        {
            int id = (int)(((Button)sender).DataContext);

            
            ProfileEtudiantVue vue = new ProfileEtudiantVue(User, id);
            vue.Show();
            this.Close();
            
        }

        private void BtnAjouterEtudiant_Click(object sender, RoutedEventArgs e)
        {

            AjouterEtudiantVue vue = new AjouterEtudiantVue(User);
            vue.Show();
            this.Close();

        }

        private void BtnValiderRechercher_Click(object sender, RoutedEventArgs e)
        {

            resultat.Visibility = System.Windows.Visibility.Hidden;
            String ChoixStatusValeur = (String)ChoixStatus.SelectedValue;
            String ChoixFormationValeur = (String)ChoixFormation.SelectedValue;

            DateTime? date = ChoixDate.SelectedDate;
            int idFormation = ListeDescription.recupererIdFormation(ChoixFormationValeur);
            int idStatus = ListeDescription.recupererIdDescription(ChoixStatusValeur, ListeDescription.listStatusCarrieres);
            String idFormationString = idFormation.ToString();
            String idStatusString = idStatus.ToString();
            //MessageBox.Show(idStatusString + "     " + idFormationString);
            String ChoixPrenomValeur = ChoixPrenom.Text;
            String ChoixNomValeur = ChoixNom.Text;
            String ChoixCourrielValeur = ChoixCourriel.Text;
            String ChoixVilleValeur = ChoixVille.Text;



            Dictionary<String, String> dictionnay = new Dictionary<String, String>();
            //dictionnay.Add("statusValeur", ChoixStatusValeur);
            //dictionnay.Add("ChoixFormation", ChoixFormationValeur);

            if (date != null)
            {
                String dateValue = date.Value.ToShortDateString();
                dictionnay.Add("dateNaissance", dateValue);
            }
            if (Convert.ToBoolean(PermisCheckBox.IsChecked))
                dictionnay.Add("permisConduire", "1");


            if (Convert.ToBoolean(VoitureCheckBox.IsChecked))
            {
                dictionnay.Add("vehicule", "1");
            }

            if (idFormation != -1)
                dictionnay.Add("idFormation", idFormationString);

            if (idStatus != -1)
                dictionnay.Add("idStatusCarriere", idStatusString);

            dictionnay.Add("prenom", ChoixPrenomValeur);
            dictionnay.Add("nom", ChoixNomValeur);
            dictionnay.Add("courriel", ChoixCourrielValeur);
            dictionnay.Add("ville", ChoixVilleValeur);
            
            

            lesEtudiants = ManagerEtudiant.recupererListeProfilesEtudiantsSelonRecherche(dictionnay);

            if (lesEtudiants != null)
            {
                ListeEtudiantsVue.Children.Clear();
                ajouterEtudiantVue();
            }
            else
            {
                
                ListeEtudiantsVue.Children.Clear();
                resultat.Visibility = System.Windows.Visibility.Visible;
                lesEtudiants = ManagerEtudiant.recupererListeProfilesEtudiantsRechercheStage();
            }

        }

        // evenement au click du menu gauche

        // evenement au click du menu gauche

       
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
