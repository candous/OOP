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
    public partial class AjouterEntrepriseVue : Window {
        public Utilisateur User {
            get;
            set;
        }
        public Entreprise MonEntreprise {
            get;
            set;
        }

        List<string> listLangue;        
        List<string> listFormation;
        List<string> listInteret;
        List<string> listTechnologie;


        public AjouterEntrepriseVue(Utilisateur user) {
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
            }
            else
            {
                BtnComptes.Visibility = System.Windows.Visibility.Hidden;
                BtnConfigurations.Visibility = System.Windows.Visibility.Hidden;
                StatistiquesMenu.Visibility = System.Windows.Visibility.Hidden;
            }

            userName.Content = User.Nom;
            MonEntreprise = new Entreprise();

            //remplir les liste pour combo box ou listBox

            listLangue = new List<string>();
            listFormation = new List<string>();
            listInteret = new List<string>();
            listTechnologie = new List<string>();

            foreach(Langue id in ListeDescription.listLangue)
                listLangue.Add(id.Description);
            foreach(Formation id in ListeDescription.listFormations)
                listFormation.Add(id.Description);
            foreach(IdDescription id in ListeDescription.listInterets)
                listInteret.Add(id.Description);
            foreach(IdDescription id in ListeDescription.listTechnologie)
                listTechnologie.Add(id.Description);

            // lier les liste avec la vue
            ChoixLangueVue.ItemsSource = listLangue;
            ChoixFromationVue.ItemsSource = listFormation;
            ChoixInteretVue.ItemsSource = listInteret;
            ChoixTechnologieVue.ItemsSource = listTechnologie;



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
        private void BtnValiderAjouter_Click(object sender, RoutedEventArgs e) {          

            bool peutAjouter = true;

            if (ChoixNomVue.Text.Length > 0)
                MonEntreprise.Nom = ChoixNomVue.Text;
            else
                peutAjouter = false;

            if(ChoixCourrielVue.Text.Length > 0) 
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
            if(ChoixFromationVue.SelectedItem != null) {
                // pour ajoute dans bdd
                listFormationRechercher = new List<int>();
                MonEntreprise.FormationsRecherchees = new List<IdDescription>();
                foreach(var item in ChoixFromationVue.SelectedItems) {
                    if(item != null) {
                        IdDescription formation = new IdDescription();
                        formation.Id = ListeDescription.recupererIdFormation(item.ToString());
                        listFormationRechercher.Add(formation.Id);
                        formation.Description = item.ToString();
                        MonEntreprise.FormationsRecherchees.Add(formation);
                    }

                }
            } else
                MonEntreprise.FormationsRecherchees = null;


            List<int> listTechnologieRecherche = null;
            if(ChoixTechnologieVue.SelectedItem != null) {
                listTechnologieRecherche = new List<int>();
                MonEntreprise.TechnologiesRecherchees = new List<IdDescription>();
                foreach(var item in ChoixTechnologieVue.SelectedItems) {
                    if(item != null) {
                        IdDescription technologie = new IdDescription();
                        technologie.Id = ListeDescription.recupererIdDescription(item.ToString(),ListeDescription.listTechnologie);
                        listTechnologieRecherche.Add(technologie.Id);
                        technologie.Description = item.ToString();
                        MonEntreprise.TechnologiesRecherchees.Add(technologie);
                    }

                }
            } else
                MonEntreprise.TechnologiesRecherchees = null;

            List<int> listInteretRecherche = null;
            if(ChoixInteretVue.SelectedItem != null) {
                listInteretRecherche = new List<int>();
                MonEntreprise.InteretsRecherches = new List<IdDescription>();
                foreach(var item in ChoixInteretVue.SelectedItems) {
                    if(item != null) {
                        IdDescription interet = new IdDescription();
                        interet.Id = ListeDescription.recupererIdDescription(item.ToString(),ListeDescription.listInterets);
                        listInteretRecherche.Add(interet.Id);
                        interet.Description = item.ToString();
                        MonEntreprise.InteretsRecherches.Add(interet);
                    }

                }
            } else
                MonEntreprise.InteretsRecherches = null;


            if(MonEntreprise.ImageLogo == null)
                MonEntreprise.ImageLogo = "images\\ProfilImageVide.png";

            MonEntreprise.DateSaisie = DateTime.Now;

            MonEntreprise.Modification = new Modification();
            MonEntreprise.Modification.UtilisateurId = User.Id;
            MonEntreprise.Modification.DateModification = DateTime.Now;


            if (peutAjouter)
            {
                // atente pas d inner join dans la requete
                if (ManagerEntreprise.ajouterEntreprise(MonEntreprise, listFormationRechercher, listInteretRecherche, listTechnologieRecherche) && peutAjouter)
                {
                   
                    ListerEntreprisesVue listerentreprise = new ListerEntreprisesVue(User);
                    listerentreprise.Show();
                    this.Close();
                }
            }
            else
                MessageBox.Show("Veuillez saisir les champs obligatoires", "Ajout d'un étudiant", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void EtudiantMenu_Click(object sender, RoutedEventArgs e) {
            
            ListerEtudiantsVue listeretudiant = new ListerEtudiantsVue(User);
            listeretudiant.Show();
            this.Close();
        }

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
