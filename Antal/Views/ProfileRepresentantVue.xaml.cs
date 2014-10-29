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
    /// Interaction logic for ProfileRepresentantVue.xaml
    /// </summary>
    public partial class ProfileRepresentantVue : Window {
        public Representant MonRepresentant {
            get;
            set;
        }

        public Utilisateur User {
            get;
            set;
        }

        public bool IsModified {
            get;
            set;
        }


        public ProfileRepresentantVue(Utilisateur user, int idRepresentant) {
            InitializeComponent();


            User = user;

            MonRepresentant = ManagerEntreprise.recupererRepresentantParId(idRepresentant);

            List<string> listLangue;
            listLangue = new List<string>();
            foreach(Langue id in ListeDescription.listLangue)
                listLangue.Add(id.Description);
            langueVue.ItemsSource = listLangue;

            prenomVue.Text = MonRepresentant.Prenom;
            nomVue.Text = MonRepresentant.Nom;
            courrielVue.Text = MonRepresentant.Courriel;
            tel1Vue.Text = MonRepresentant.Telephone1;
            tel2Vue.Text = MonRepresentant.Telephone2;
            tel3Vue.Text = MonRepresentant.Telephone3;
            departementVue.Text = MonRepresentant.Departement;
            posteVue.Text = MonRepresentant.Poste;

                       
            langueVue.SelectedValue = ListeDescription.recupererLaLangue( MonRepresentant.IdLangue);



        }

        private void BtnModifierRepresentant_Click(object sender, RoutedEventArgs e) {

            MonRepresentant.Prenom = prenomVue.Text;
            MonRepresentant.Nom = nomVue.Text;
            MonRepresentant.Courriel = courrielVue.Text;
            MonRepresentant.Telephone1 = tel1Vue.Text;
            MonRepresentant.Telephone2 = tel2Vue.Text;
            MonRepresentant.Telephone3 = tel3Vue.Text;
            MonRepresentant.Departement = departementVue.Text;
            MonRepresentant.Poste = posteVue.Text;
            MonRepresentant.Modification = new Modification();
            MonRepresentant.Modification.UtilisateurId = User.Id;
            MonRepresentant.Modification.DateModification = DateTime.Now;

          

            if(!langueVue.SelectedValue.Equals(""))
                MonRepresentant.IdLangue = ListeDescription.recupererIdLangue(langueVue.SelectedValue.ToString());
            else
                MonRepresentant.IdLangue = null;

            if (MonRepresentant.Prenom.Length <= 0 || MonRepresentant.Nom.Length <= 0 ||
                MonRepresentant.Courriel.Length <= 0 || MonRepresentant.Telephone1.Length <= 0)
            {
                MessageBox.Show("Veuillez remplir les champs obligatoires", "Modification representant", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
            }
                
            else
            {
                MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir modifier ce représentant?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (ret == MessageBoxResult.Yes)
                {
                    IsModified = true;
                    ManagerEntreprise.modifierRepresentant(MonRepresentant);

                    this.Close();


                }
            }
        }
    }
}
