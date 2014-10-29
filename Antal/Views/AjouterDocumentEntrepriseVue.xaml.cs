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
    /// Logique d'interaction pour AjouterDocumentVue.xaml
    /// </summary>
    public partial class AjouterDocumentEntrepriseVue : Window
    {

        public Utilisateur User
        {
            get;
            set;
        }

        List<string> listeTypeDocument;

        public Document MonDocument { get; set; }

        public bool IsModified { get; set; }

        public AjouterDocumentEntrepriseVue(Utilisateur user, int idEtudiant)
        {
            InitializeComponent();
            User = user;

            MonDocument = new Document();
            MonDocument.Modification = new Modification();
            MonDocument.IdProprietaire = idEtudiant;

            listeTypeDocument = new List<string>();

            IsModified = false;

            foreach (IdDescription id in ListeDescription.listTypeDocument)
                listeTypeDocument.Add(id.Description);

            choixTypeDocumentVue.ItemsSource = listeTypeDocument;


        }

        private void BtnAjouterDocument_Click(object sender, RoutedEventArgs e)
        {
            bool ajouter = true;

            string ChoixTypeDocument = null;
            if(choixTypeDocumentVue.SelectedValue != null && !choixTypeDocumentVue.SelectedValue.ToString().Equals("")) {
                ChoixTypeDocument = (string)choixTypeDocumentVue.SelectedValue;
                MonDocument.IdTypeDocument = ListeDescription.recupererIdDescription(choixTypeDocumentVue.SelectedValue.ToString(), ListeDescription.listTypeDocument);
            } else
                ajouter = false;
            
            if(!choixTitreVue.Text.Equals("")) {

                MonDocument.Titre = choixTitreVue.Text;
            } else
                MonDocument.Titre = null;
                  
            MonDocument.DateAjout = DateTime.Now;
            MonDocument.Modification.UtilisateurId = User.Id;
            MonDocument.Modification.DateModification = DateTime.Now;

            if(MonDocument.CheminURL == null || MonDocument.CheminURL.Equals(""))
                ajouter = false;


            if(ajouter) {
                MessageBox.Show("Document ajouté.", "Ajout de doccument", MessageBoxButton.OK, MessageBoxImage.Information);

                ManagerDocument.ajouterDocumentEntreprise(MonDocument);
                IsModified = true;
                this.Close();
            }else
                MessageBox.Show("Veuillez entrer tous les champs.", "Ajout de doccument", MessageBoxButton.OK, MessageBoxImage.Information);
 
        }

        private void BtnChoisirPhoto_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // filtres pour extentions
            dlg.Title = "Choisir un fichier";

            bool? result = dlg.ShowDialog();

            // si un fichier selectione, changer image et faire un copy
            if (result == true)
            {
                //copier l'image dans un autre repertoire dans l'application
                string nouveauRep = DefinitionConnection.DocumentFolder + "\\documentsEntreprises\\";
                if(!Directory.Exists(nouveauRep)) {
                    Directory.CreateDirectory(nouveauRep);
                }
                //nom unique
                string filePath = nouveauRep + DateTime.Now.ToString("yyyyMMddHHmmssfff") +"-" + System.IO.Path.GetFileName(dlg.FileName);
                System.IO.File.Copy(dlg.FileName, filePath, true);
                MonDocument.CheminURL = filePath;             

                //afficher image
                string filename = dlg.FileName;
                choixUrlVue.Text = filename;
            }
        }


    }
}
