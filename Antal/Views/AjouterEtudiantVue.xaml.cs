using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logique d'interaction pour AjouterEtudiantVue.xaml
    /// </summary>
    public partial class AjouterEtudiantVue : Window
    {
        public Utilisateur User
        {
            get;
            set;
        }
        public Etudiant MonEtudiant
        {
            get;
            set;
        }


        List<string> listStatusResidence;
        List<string> listLangue;
        List<string> listNiveauLangue;
        List<string> listFormation;
        List<string> listStatusCarrierre;
        List<string> listInteret;
        List<string> listTechnologie;


        public AjouterEtudiantVue(Utilisateur user)
        {


            InitializeComponent();
            MonEtudiant = new Etudiant();
            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;

            User = user;
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
                StatistiquesMenu.Visibility = System.Windows.Visibility.Hidden;
            }

            userName.Content = User.Nom;
            //remplir les liste pour combo box ou listBox
            listStatusResidence = new List<string>();
            listLangue = new List<string>();
            listNiveauLangue = new List<string>();
            listFormation = new List<string>();
            listStatusCarrierre = new List<string>();
            listInteret = new List<string>();
            listTechnologie = new List<string>();

            foreach (IdDescription id in ListeDescription.listStatusResidence)
                listStatusResidence.Add(id.Description);
            foreach (Langue id in ListeDescription.listLangue)
                listLangue.Add(id.Description);
            foreach (IdDescription id in ListeDescription.listNiveauLangue)
                listNiveauLangue.Add(id.Description);
            foreach (Formation id in ListeDescription.listFormations)
                listFormation.Add(id.Description);
            foreach (IdDescription id in ListeDescription.listStatusCarrieres)
                listStatusCarrierre.Add(id.Description);
            foreach (IdDescription id in ListeDescription.listInterets)
                listInteret.Add(id.Description);
            foreach (IdDescription id in ListeDescription.listTechnologie)
                listTechnologie.Add(id.Description);

            // lier les liste avec la vue
            ChoixStatutsResidenceVue.ItemsSource = listStatusResidence;
            Langue1Choix.ItemsSource = listLangue;
            Langue2Choix.ItemsSource = listLangue;
            Langue3Choix.ItemsSource = listLangue;
            Langue4Choix.ItemsSource = listLangue;
            Niveau1Choix.ItemsSource = listNiveauLangue;
            Niveau2Choix.ItemsSource = listNiveauLangue;
            Niveau3Choix.ItemsSource = listNiveauLangue;
            Niveau4Choix.ItemsSource = listNiveauLangue;
            FormationChoix.ItemsSource = listFormation;
            StatusCarriereChoix.ItemsSource = listStatusCarrierre;
            InteretChoix.ItemsSource = listInteret;
            TechnologieChoix.ItemsSource = listTechnologie;

        }

        // evenement au click des bouton

        private void BtnChoisirPhoto_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // filtres pour extentions
            dlg.Title = "Choisir une photo";
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";

            bool? result = dlg.ShowDialog();

            // si un fichier selectione, changer image et faire un copy
            if (result == true)
            {
                //copier l'image dans un autre repertoire dans l'application
                string nouveauRep = DefinitionConnection.DocumentFolder + "\\images\\";
                if(!Directory.Exists(nouveauRep)) {
                    Directory.CreateDirectory(nouveauRep);
                }
                //nom unique
                string filePath = nouveauRep + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "-" + System.IO.Path.GetFileName(dlg.FileName);
                System.IO.File.Copy(dlg.FileName, filePath, true);
                MonEtudiant.PhotoURL = filePath;

                //afficher image
                string filename = dlg.FileName;
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
                affichePhoto.Fill = brush;
            }
        }

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


        //Ajouter Etudiant
        private void BtnAjouterEtudiant_Click(object sender, RoutedEventArgs e)
        {
            MonEtudiant.Prenom = ChoixPrenomVue.Text;
            MonEtudiant.Nom = ChoixNomVue.Text;
            MonEtudiant.Courriel = ChoixCourrierlVue.Text;
            MonEtudiant.Telephone1 = ChoixTel1Vue.Text;
            MonEtudiant.Telephone2 = ChoixTel2Vue.Text;
            MonEtudiant.Telephone3 = ChoixTel3Vue.Text;
            MonEtudiant.Adresse = ChoixAdresseVue.Text;
            MonEtudiant.Ville = ChoixVilleVue.Text;

            if (ChoixDateNaissaceVue.SelectedDate != null)
            {
                DateTime? ChoixDateTemp = ChoixDateNaissaceVue.SelectedDate;
                Object objectdate = ChoixDateTemp;
                MonEtudiant.DateNaissance = (DateTime)objectdate;
            }

            if(ChoixStatutsResidenceVue.SelectedValue != null)
            MonEtudiant.IdStatusResidence = ListeDescription.recupererIdDescription(ChoixStatutsResidenceVue.SelectedValue.ToString(), ListeDescription.listStatusResidence);
           
            MonEtudiant.Vehicule = ChkBxVehicule.IsChecked;
            MonEtudiant.PermisConduire = ChkBxVehicule.IsChecked;
            MonEtudiant.RiveNord = ChkBxRiveNord.IsChecked;
            MonEtudiant.RiveSud = ChkBxRiveSud.IsChecked;
            MonEtudiant.Commentaire = ChoixCommentaireVue.Text;
            MonEtudiant.Langues = new List<Langue>();


            if (Langue1Choix.SelectedItem != null)
            {
                Langue langue1 = new Langue();
                langue1.Id = ListeDescription.recupererIdLangue(Langue1Choix.SelectedValue.ToString());
                langue1.Niveau = ListeDescription.recupererIdDescription(Niveau1Choix.SelectedValue.ToString(), ListeDescription.listNiveauLangue);
                MonEtudiant.Langues.Add(langue1);
            }

            if (Langue2Choix.SelectedItem != null)
            {
                Langue langue2 = new Langue();
                langue2.Id = ListeDescription.recupererIdLangue(Langue2Choix.SelectedValue.ToString());
                langue2.Niveau = ListeDescription.recupererIdDescription(Niveau2Choix.SelectedValue.ToString(), ListeDescription.listNiveauLangue);
                MonEtudiant.Langues.Add(langue2);
            }

            if (Langue3Choix.SelectedItem != null)
            {
                Langue langue3 = new Langue();
                langue3.Id = ListeDescription.recupererIdLangue(Langue3Choix.SelectedValue.ToString());
                langue3.Niveau = ListeDescription.recupererIdDescription(Niveau3Choix.SelectedValue.ToString(), ListeDescription.listNiveauLangue);
                MonEtudiant.Langues.Add(langue3);
            }

            if (Langue4Choix.SelectedItem != null)
            {
                Langue langue4 = new Langue();
                langue4.Id = ListeDescription.recupererIdLangue(Langue4Choix.SelectedValue.ToString());
                langue4.Niveau = ListeDescription.recupererIdDescription(Niveau4Choix.SelectedValue.ToString(), ListeDescription.listNiveauLangue);
                MonEtudiant.Langues.Add(langue4);
            }

            if (FormationChoix.SelectedValue != null)
            MonEtudiant.IdFormation = ListeDescription.recupererIdFormation(FormationChoix.SelectedValue.ToString());

            if (DateFinFormaionChoix.SelectedDate != null)
            {
                DateTime? dateFin = DateFinFormaionChoix.SelectedDate;
                Object objectdateFin = dateFin;
                MonEtudiant.DateFinFormation = (DateTime)objectdateFin;

            }

            if (StatusCarriereChoix.SelectedValue != null)
            MonEtudiant.IdStatusCarriere = ListeDescription.recupererIdDescription(StatusCarriereChoix.SelectedValue.ToString(), ListeDescription.listStatusCarrieres);


            if (MonEtudiant.Nom.Length <= 0 || MonEtudiant.Prenom.Length <= 0 || MonEtudiant.Courriel.Length <= 0 ||
                MonEtudiant.DateFinFormation == null || StatusCarriereChoix.SelectedValue == null || FormationChoix.SelectedValue == null)
                MessageBox.Show("Veuillez remplir les champs obligatoire", "Ajout d'un étudiant", MessageBoxButton.OK, MessageBoxImage.Information);


            else
            {
                Double SalaireEspereDouble;
                if (Double.TryParse(SalaireChoix.Text, out SalaireEspereDouble))
                    Double.TryParse(SalaireChoix.Text, out SalaireEspereDouble);
                else
                    SalaireEspereDouble = 0;

                Object objectSalaire = SalaireEspereDouble;
                MonEtudiant.SalaireEspere = (Double?)objectSalaire;

                MonEtudiant.PosteDesire = PosteDesirerChoix.Text;
                MonEtudiant.Experiences = ExperiencesAnterieurChoix.Text;


                List<int> listInteretRecherche = null;
                MonEtudiant.Interets = new List<IdDescription>();
                listInteretRecherche = new List<int>();
                if (InteretChoix.SelectedItems != null)
                {
                    foreach (var item in InteretChoix.SelectedItems)
                    {
                        if (item != null)
                        {
                            IdDescription interet = new IdDescription();
                            interet.Id = ListeDescription.recupererIdDescription(item.ToString(), ListeDescription.listInterets);
                            listInteretRecherche.Add(interet.Id);
                            interet.Description = item.ToString();
                            MonEtudiant.Interets.Add(interet);
                        }

                    }
                }
                List<int> listTechnologieRecherche = null;
                MonEtudiant.TechonologiesPreferees = new List<IdDescription>();
                if (TechnologieChoix.SelectedItems != null)
                {
                    listTechnologieRecherche = new List<int>();
                    foreach (var item in TechnologieChoix.SelectedItems)
                    {
                        if (item != null)
                        {
                            IdDescription technologie = new IdDescription();
                            technologie.Id = ListeDescription.recupererIdDescription(item.ToString(), ListeDescription.listTechnologie);
                            listTechnologieRecherche.Add(technologie.Id);
                            technologie.Description = item.ToString();
                            MonEtudiant.TechonologiesPreferees.Add(technologie);
                        }

                    }

                }
                if (MonEtudiant.PhotoURL == null)
                    MonEtudiant.PhotoURL = "images\\ProfilImageVide.png";

                MonEtudiant.Modification = new Modification();
                MonEtudiant.Modification.UtilisateurId = User.Id;
                MonEtudiant.Modification.DateModification = DateTime.Now;

                int reponse = ManagerEtudiant.ajouterEtudiant(MonEtudiant, listInteretRecherche, listTechnologieRecherche, MonEtudiant.Langues);

                if (reponse != -1)
                {
                    ListerEtudiantsVue listeretudiant = new ListerEtudiantsVue(User);
                    listeretudiant.Show();
                    this.Close();
                }
            }
        }


    }
}
