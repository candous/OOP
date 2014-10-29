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
    /// Logique d'interaction pour AfficherEntrevueEntrepriseVue.xaml
    /// </summary>
    public partial class ProfileEntrevueEntrepriseVue : Window
    {

        public Utilisateur User
        {
            get;
            set;
        }

        List<string> listeTypeEntrevue;
        List<string> listeTypeResultat;       

        public Entrevue MonEntrevue { get; set; }
        public Etudiant MonEtudiant { get; set; }

        public bool IsModified { get; set; }


        public ProfileEntrevueEntrepriseVue(Utilisateur user, int idEntrevue)
        {
            InitializeComponent();
            User = user;

            MonEntrevue = ManagerEntrevue.recupererEntrevueParId(idEntrevue);
            MonEtudiant = ManagerEtudiant.recupererProfilesEtudiant(MonEntrevue.IdEtudiant);


            listeTypeEntrevue = new List<string>();
            listeTypeResultat = new List<string>();
            IsModified = false;

            foreach (IdDescription id in ListeDescription.listTypeEntrevue)
                listeTypeEntrevue.Add(id.Description);
            foreach (IdDescription id in ListeDescription.listTypeResultat)
                listeTypeResultat.Add(id.Description);

            choixEntrevueVue.ItemsSource = listeTypeEntrevue;
            resultatTypeVue.ItemsSource = listeTypeResultat;


            choixEntrevueVue.SelectedValue = ListeDescription.recupererDescription(MonEntrevue.TypeEntrevue, ListeDescription.listTypeEntrevue);
                
       
             Object dateE = MonEntrevue.DateEntrevue;
            DateTime dateEtmp = (DateTime)dateE;
            choixDateVue.SelectedDate =dateEtmp ;

            resultatTypeVue.SelectedValue = ListeDescription.recupererDescription(MonEntrevue.Resultat, ListeDescription.listTypeResultat);

            commentaireVues.Text = MonEntrevue.Commentaire;

            ImgEtudiant.Source = new BitmapImage(new Uri(MonEtudiant.PhotoURL, UriKind.RelativeOrAbsolute));
            NomEtudiantVue.Content = MonEtudiant.Nom + " " + MonEtudiant.Prenom;
            
        }

        private void BtnModifierEntrevue_Click(object sender, RoutedEventArgs e)
        {
            if (choixEntrevueVue.SelectedValue != null)
            MonEntrevue.TypeEntrevue = ListeDescription.recupererIdDescription(choixEntrevueVue.SelectedValue.ToString(), ListeDescription.listTypeEntrevue);
           
            Object dateE = choixDateVue.SelectedDate;
            DateTime dateEtmp = (DateTime)dateE;
            MonEntrevue.DateEntrevue = dateEtmp;

            if (resultatTypeVue.SelectedValue != null)
            MonEntrevue.Resultat = ListeDescription.recupererIdDescription(resultatTypeVue.SelectedValue.ToString(), ListeDescription.listTypeResultat);

            MonEntrevue.Commentaire = commentaireVues.Text;


            MonEntrevue.Modification = new Modification();
            MonEntrevue.Modification.UtilisateurId = User.Id;
            MonEntrevue.Modification.DateModification = DateTime.Now;



            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir modifier cette entrevue?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (ret == MessageBoxResult.Yes)
            {
                IsModified = true;
                ManagerEntrevue.modifierEntrevue(MonEntrevue);
                MessageBox.Show("Entrevue modifier.", "Modifiation Entrevue", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();


            }
        }
    }
}
