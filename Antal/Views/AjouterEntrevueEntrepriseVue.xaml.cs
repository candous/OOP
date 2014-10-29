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
    /// Interaction logic for AjouterEntrevueVue.xaml
    /// </summary>
    public partial class AjouterEntrevueEntrepriseVue : Window {

        public Utilisateur User
        {
            get;
            set;
        }

        List<string> listeTypeEntrevue;
        List<string> listeTypeResultat;
        
        public List<Etudiant> lesEtudiants { get; set; }

        public Entrevue MonEntrevue { get; set; }
        public Etudiant MonEtudiant { get; set; }

        public bool IsModified { get; set; }

        public AjouterEntrevueEntrepriseVue(Utilisateur user, int Id)
        {
            InitializeComponent();

            User = user;

            MonEntrevue = new Entrevue();
            MonEntrevue.IdEntreprise = Id;
            MonEntrevue.IdEtudiant = -1;

            listeTypeEntrevue = new List<string>();
            listeTypeResultat = new List<string>();
            IsModified = false;

            foreach (IdDescription id in ListeDescription.listTypeEntrevue)
                listeTypeEntrevue.Add(id.Description);
            foreach (IdDescription id in ListeDescription.listTypeResultat)
                listeTypeResultat.Add(id.Description);

            choixEntrevueVue.ItemsSource = listeTypeEntrevue;
            resultatTypeVue.ItemsSource = listeTypeResultat;

            lesEtudiants = ManagerEtudiant.recupererListeProfilesEtudiantsRechercheStage();

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
                if (nbEtudiant % 2 == 0 || nbEtudiant == 0)
                {
                    hPanel = new StackPanel();
                    hPanel.Orientation = Orientation.Horizontal;
                    hPanel.Width = 594;
                }

                // créer le bouton
                Button btn = new Button();
                btn.Background = null;
                btn.BorderBrush = null;
                btn.DataContext = etudiant;
                btn.Click += RecupererEtudiant;

                //layout du bouton
                StackPanel vPanel = new StackPanel();
                vPanel.Orientation = Orientation.Vertical;
                vPanel.Width = 148;
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
                Label formation = new Label();
                formation.Content = ListeDescription.recupererLaFormation(etudiant.IdFormation);
                formation.FontSize = 15;
                formation.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                formation.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                formation.FontFamily = new System.Windows.Media.FontFamily("Verdana");

                //ajouter Formation
                vPanel.Children.Add(formation);

                btn.Content = vPanel;
                // ajouterEtudiantVue le bouton au paneau horizontal
                hPanel.Children.Add(btn);


                //tous les 4 etudiants on ajoute le hpanel a la liste des etudiants                
                if (nbEtudiant % 2 == 0 || nbEtudiant == nbEtudiantMax && nbEtudiant != 0)
                    ListeEtudiantsVue.Children.Add(hPanel);

                nbEtudiant++;
            }
        }



        // evemenet au click

        private void RecupererEtudiant(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            MonEtudiant = (Etudiant)b.DataContext;

            ImgEtudiant.Source = new BitmapImage(new Uri(@"" + MonEtudiant.PhotoURL, UriKind.RelativeOrAbsolute));
            NomEtudiantVue.Content = MonEtudiant.Nom;
            MonEntrevue.IdEtudiant = MonEtudiant.Id;

        }

        private void RechercheEtudiant_Click(object sender, RoutedEventArgs e)
        {
            lesEtudiants = ManagerEtudiant.recupererEtudiantParleNom(RechercheEtudiantNom.Text);
            if (lesEtudiants != null)
            {
                resultat.Visibility = System.Windows.Visibility.Hidden;
                ListeEtudiantsVue.Children.Clear();
                ajouterEtudiantVue();
            }
            else
                resultat.Visibility = System.Windows.Visibility.Visible; 
        }

        private void BtnAjouterEntrevue_Click(object sender, RoutedEventArgs e)
        {
            bool ajouter = true;

            if(choixEntrevueVue.SelectedValue != null && !choixEntrevueVue.SelectedValue.ToString().Equals("")) {
                MonEntrevue.TypeEntrevue = ListeDescription.recupererIdDescription(choixEntrevueVue.SelectedValue.ToString(), ListeDescription.listTypeEntrevue);
            } else
                MonEntrevue.TypeEntrevue = null;

            if(choixDateVue.SelectedDate != null) {
                Object dateE = choixDateVue.SelectedDate;
                DateTime dateEtmp = (DateTime)dateE;
                MonEntrevue.DateEntrevue = dateEtmp;
            } else
                ajouter = false;

            if(resultatTypeVue.SelectedValue != null && !resultatTypeVue.SelectedValue.ToString().Equals("")) {
                MonEntrevue.Resultat = ListeDescription.recupererIdDescription(resultatTypeVue.SelectedValue.ToString(), ListeDescription.listTypeResultat);
            } else
                MonEntrevue.Resultat = null;

             MonEntrevue.Commentaire = commentaireVues.Text;

             if(MonEntrevue.IdEtudiant == -1) 
                 ajouter = false;
             
            

            MonEntrevue.Modification = new Modification();
            MonEntrevue.Modification.UtilisateurId = User.Id;
            MonEntrevue.Modification.DateModification = DateTime.Now;



            if(ajouter) {
                MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir ajouter cette entrevue?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(ret == MessageBoxResult.Yes) {
                    IsModified = true;
                    ManagerEntrevue.ajouterEntrevue(MonEntrevue);
                    MessageBox.Show("Entrevue ajouter.", "Ajout d'un entrevue", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.Close();
                }
            } else
                MessageBox.Show("Veuillez remplir tous les champs necessaires : ", "Ajout d'un entrevue", MessageBoxButton.OK, MessageBoxImage.Information);




        }
    }
}
