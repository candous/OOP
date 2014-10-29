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
using System.IO;

namespace Views {
    /// <summary>
    /// Interaction logic for AjouterEntrepriseVue.xaml
    /// </summary>
    public partial class ModifierEntrepriseVue : Window {
        public Utilisateur User {
            get;
            set;
        }
        public Entreprise MonEntreprise {
            get;
            set;
        }

        List<String> listLangue;
     //   List<ListBoxItem> listFormation;
      //  List<ListBoxItem> listInteret;
     //   List<ListBoxItem> listTechnologie;


        public ModifierEntrepriseVue(Utilisateur user, Entreprise entreprise) {
            InitializeComponent();
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
                BtnValiderModifier.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                BtnComptes.Visibility = System.Windows.Visibility.Hidden;
                BtnConfigurations.Visibility = System.Windows.Visibility.Hidden;
                StatistiquesMenu.Visibility = System.Windows.Visibility.Hidden;
            }
            userName.Content = User.Nom;
            MonEntreprise = entreprise;

            //remplir les liste pour combo box ou listBox

            listLangue = new List<String>();
     


            foreach(Langue id in ListeDescription.listLangue)
                listLangue.Add(id.Description);


            ChoixFromationVue.Items.Clear();
            foreach(Formation id in ListeDescription.listFormations) {
                ListBoxItem lb = new ListBoxItem();
                lb.Content = id.Description;

                if(MonEntreprise.FormationsRecherchees != null)
                    foreach(IdDescription item in MonEntreprise.FormationsRecherchees)
                        if(id.Id == item.Id)
                            lb.IsSelected = true;
                ChoixFromationVue.Items.Add(lb);
                    
            }

            ChoixInteretVue.Items.Clear();
            foreach(IdDescription id in ListeDescription.listInterets) {
                ListBoxItem lb = new ListBoxItem();
                lb.Content = id.Description;

                if(MonEntreprise.InteretsRecherches != null)
                    foreach(IdDescription item in MonEntreprise.InteretsRecherches)
                        if(id.Id == item.Id)
                            lb.IsSelected = true;
                ChoixInteretVue.Items.Add(lb);

            }


            ChoixTechnologieVue.Items.Clear();
            foreach(IdDescription id in ListeDescription.listTechnologie) {
                ListBoxItem lb = new ListBoxItem();
                lb.Content = id.Description;

                if(MonEntreprise.TechnologiesRecherchees != null)
                    foreach(IdDescription item in MonEntreprise.TechnologiesRecherchees)
                        if(id.Id == item.Id)
                            lb.IsSelected = true;
                ChoixTechnologieVue.Items.Add(lb);

            }

            // lier les liste avec la vue
            ChoixLangueVue.ItemsSource = listLangue;

            // binder toutes les valeur de l entreprise avec la vue
            ChoixNomVue.Text = MonEntreprise.Nom;
            ChoixCourrielVue.Text = MonEntreprise.Email;
            ChoixTel1Vue.Text = MonEntreprise.Telephone1;
            ChoixTel2Vue.Text = MonEntreprise.Telephone2;
            ChoixTel3Vue.Text = MonEntreprise.Telephone3;

            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(MonEntreprise.ImageLogo, UriKind.RelativeOrAbsolute));
            affichePhoto.Fill = brush;

            ChoixAdresseVue.Text = MonEntreprise.Adresse;
            ChoixVilleVue.Text = MonEntreprise.Ville;
            ChoixSecteurVue.Text = MonEntreprise.Secteur;
            ChoixLangueVue.SelectedValue = ListeDescription.recupererLaLangue(MonEntreprise.Langue);
            ChoixCommentaireVue.Text = MonEntreprise.Commentaire;


        }



        // evenement au click des boutons
        private void BtnChoisirPhoto_Click(object sender, RoutedEventArgs e) {
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
                MonEntreprise.ImageLogo = filePath;

                //afficher image
                string filename = dlg.FileName;
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
                affichePhoto.Fill = brush;
            }

        }


        //ajouter une entreprise
        private void BtnValiderModifier_Click(object sender, RoutedEventArgs e) {          

            bool peutAjouter = true;

            if(ChoixNomVue.Text != null || !ChoixNomVue.Text.Equals("")) 
                MonEntreprise.Nom = ChoixNomVue.Text;                
             else
                peutAjouter = false;


            if(ChoixCourrielVue.Text != null || !ChoixCourrielVue.Text.Equals("")) 
                MonEntreprise.Email = ChoixCourrielVue.Text;               
             else
                peutAjouter = false;

            if(ChoixTel1Vue.Text != null || !ChoixTel1Vue.Text.Equals(""))
                MonEntreprise.Telephone1 = ChoixTel1Vue.Text;
            else
                MonEntreprise.Telephone1 = null;

            if(ChoixTel2Vue.Text != null || !ChoixTel2Vue.Text.Equals(""))
                MonEntreprise.Telephone2 = ChoixTel2Vue.Text;
            else
                MonEntreprise.Telephone1 = null;

            if(ChoixTel3Vue.Text != null || !ChoixTel3Vue.Text.Equals(""))
                MonEntreprise.Telephone3 = ChoixTel3Vue.Text;
            else
                MonEntreprise.Telephone3 = null;

            if(ChoixAdresseVue.Text != null || !ChoixAdresseVue.Text.Equals(""))
                MonEntreprise.Adresse = ChoixAdresseVue.Text;
            else
                MonEntreprise.Adresse = null;

            if(ChoixVilleVue.Text != null || !ChoixVilleVue.Text.Equals(""))
                MonEntreprise.Ville = ChoixVilleVue.Text;
            else
                MonEntreprise.Ville = null;

            if(ChoixSecteurVue.Text != null || !ChoixSecteurVue.Text.Equals(""))
                MonEntreprise.Secteur = ChoixSecteurVue.Text;
            else
                MonEntreprise.Secteur = null;           

            if(ChoixLangueVue.SelectedItem != null)
                MonEntreprise.Langue = ListeDescription.recupererIdLangue(ChoixLangueVue.SelectedValue.ToString());
            else
                MonEntreprise.Langue = null;

            if(ChoixCommentaireVue.Text != null || !ChoixCommentaireVue.Text.Equals(""))
                MonEntreprise.Commentaire = ChoixCommentaireVue.Text;
            else
                MonEntreprise.Commentaire = null;

            List<int> listFormationRechercher = null;
            if(ChoixFromationVue.SelectedItems != null) {
                // pour ajoute dans bdd
                listFormationRechercher = new List<int>();              
                foreach(ListBoxItem item in ChoixFromationVue.SelectedItems) 
                    if(item != null) 
                        listFormationRechercher.Add(ListeDescription.recupererIdFormation(item.Content.ToString()));  
            }


           

            List<int> listTechnologieRecherche = null;
            if(ChoixTechnologieVue.SelectedItems != null) {
                listTechnologieRecherche = new List<int>();               
                foreach(ListBoxItem item in ChoixTechnologieVue.SelectedItems) 
                    if(item != null) 
                        listTechnologieRecherche.Add(ListeDescription.recupererIdDescription(item.Content.ToString(), ListeDescription.listTechnologie));  
            }


            List<int> listInteretRecherche = null;
            if(ChoixInteretVue.SelectedItems != null) {
                listInteretRecherche = new List<int>();
                foreach(ListBoxItem item in ChoixInteretVue.SelectedItems) 
                    if(item != null)                      
                        listInteretRecherche.Add(ListeDescription.recupererIdDescription(item.Content.ToString(), ListeDescription.listInterets));     
            }

            if(MonEntreprise.ImageLogo == null)
                MonEntreprise.ImageLogo = "images\\ProfilImageVide.png";

            MonEntreprise.DateSaisie = DateTime.Now;

            MonEntreprise.Modification = new Modification();
            MonEntreprise.Modification.UtilisateurId = User.Id;
            MonEntreprise.Modification.DateModification = DateTime.Now;

            

            // atente pas d inner join dans la requete
            //if(ManagerEntreprise.modifierEntreprise(MonEntreprise, listFormationRechercher, listInteretRecherche, listTechnologieRecherche) && peutAjouter)

            if (peutAjouter)
            {         
                ManagerEntreprise.modifierEntreprise(MonEntreprise, listFormationRechercher, listInteretRecherche, listTechnologieRecherche);
                MessageBox.Show("Entreprise modifiée.", "Modification d'une entreprise", MessageBoxButton.OK, MessageBoxImage.Information);
                ProfileEntrepriseVue vue = new ProfileEntrepriseVue(User, MonEntreprise.Id);
                vue.Show();
                this.Close();

            }
               
            else
                MessageBox.Show("Veuillez remplir tous les champs obligatoires", "Modification d'une entreprise", MessageBoxButton.OK, MessageBoxImage.Information);
           

        }

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
