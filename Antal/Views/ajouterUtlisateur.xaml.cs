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
using BLL;
using Entities;

namespace Views
{
    /// <summary>
    /// Logique d'interaction pour ajouterUtlisateur.xaml
    /// </summary>
    public partial class ajouterUtlisateur : Window
    {
        private  List<String> typeUtilisateurString;
        public Utilisateur UserLog { get; set; }
        public ajouterUtlisateur(Utilisateur user)
        {
            InitializeComponent();
            UserLog = user;
            
            List<IdDescription> listeIDTypeUtilisateur = ListeDescription.listTypeUlisateur;
            typeUtilisateurString=new List<String>();

            foreach (IdDescription id in listeIDTypeUtilisateur)
            {
                typeUtilisateurString.Add(id.Description);
            }

            ChoixTypeUtilisateur.ItemsSource = typeUtilisateurString;
           

        }

        private void BtnValiderRechercher_Click(object sender, RoutedEventArgs e)
        {
            Utilisateur user = new Utilisateur();

            user.Nom = ChoixUtilisateur.Text;
            user.MotDePasse = ChoixMdp.Password;
            user.IdTypeUtilisateur = ListeDescription.recupererIdDescription(ChoixTypeUtilisateur.SelectedItem.ToString(), ListeDescription.listTypeUlisateur);
            if (user.IdTypeUtilisateur == 1)
            {
                user.PeutLire = true;
                user.PeutEcrire = true;
                user.PeutCreerUtilisateur = true;

            }
            else if (user.IdTypeUtilisateur == 2)
            {
                user.PeutLire = true;
                user.PeutEcrire = true;
                user.PeutCreerUtilisateur = false;

            }
            else
            {
                user.PeutLire = true;
                user.PeutEcrire = false;
                user.PeutCreerUtilisateur = false;

            }
            user.modification=new Modification();
            user.modification.UtilisateurId = UserLog.Id;
            user.modification.DateModification = DateTime.Now;

            ManagerUtilisateur.ajouterUtilisateur(user);

            this.Close();

        }
    }
}
