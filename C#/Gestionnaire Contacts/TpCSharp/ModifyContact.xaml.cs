using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TpBLL;
using TpEntities;

namespace TpCSharp {
    /// <summary>
    /// Logique d'interaction pour ModifyContact.xaml
    /// </summary>
    public partial class ModifyContact : Window {

        //propriete
        public Personne User {
            get;
            set;
        }
        public Personne ContactModifier {
            get;
            set;
        }
        public bool IsModified {
            get;
            set;
        }

        // constructeur
        public ModifyContact(Personne user, Personne contact) {
            InitializeComponent();

            this.User = user;
            this.ContactModifier = contact;
            //positionement de la fenetre lors du chargement
            var desktop = System.Windows.SystemParameters.WorkArea;
            this.Left = desktop.Right - 900;//this.Width
            this.Top = desktop.Top;
            //binder avec la vue
            grid.DataContext = ContactModifier;
        }


        //evenements
        private void btnChoixPhoto_Click(object sender, RoutedEventArgs e) {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // filtres pour extentions
            dlg.Title = "Choisir une photo";
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";

            bool? result = dlg.ShowDialog();

            // si un fichier selectione, changer image et faire un copy
            if(result == true) {
                //copier l'image dans un autre repertoire dans l'application
                string nouveauRep = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\images\\";
                if(!Directory.Exists(nouveauRep)) {
                    Directory.CreateDirectory(nouveauRep);
                }
                //nom unique
                string filePath = nouveauRep + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Guid.NewGuid().ToString("N") + System.IO.Path.GetFileName(dlg.FileName);
                System.IO.File.Copy(dlg.FileName, filePath, true);
                ContactModifier.UrlPhoto = filePath;

                //afficher image
                string filename = dlg.FileName;
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
                affichePhoto.Fill = brush;
            }
        }

        private void btnInscrire_Click(object sender, RoutedEventArgs e) {


            //si les champs obligatoires ont ete saisies, on cree un user
            if(txtCourriel.Text.Length > 0 && txtNom.Text.Length > 0) {
                if(Regex.IsMatch(txtCourriel.Text, @"^[a-zA-Z0-9\w\.-]*@[a-zA-Z0-9\w\.-]*\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$")) {
                    Contact contactTableModif = new Contact();

                    if(date.Text.Length <= 0)
                        ContactModifier.Aniversaire = null;
                    else
                        ContactModifier.Aniversaire = Convert.ToDateTime(date.Text);
                    ContactModifier.Adresse = txtAdresse.Text;
                    ContactModifier.Celulaire = txtCel.Text;
                    ContactModifier.Compagnie = txtCompagnie.Text;
                    ContactModifier.Courriel = txtCourriel.Text;
                    ContactModifier.Nom = txtNom.Text;
                    ContactModifier.Pays = txtPays.Text;
                    ContactModifier.Province = txtProvince.Text;
                    ContactModifier.SiteWeb = txtSite.Text;
                    ContactModifier.Telephone = txtTelephone.Text;
                    ContactModifier.Ville = txtVille.Text;

                    bool userMofifie = PersonneManager.ModifiertUser(ContactModifier);
                    //tester si la insertion est bien passe
                    if(userMofifie) {
                        IsModified = true;
                        this.Close();
                    } else {
                        txtMsgErreur.Text = "Le contact n'a pas ete modifie, ressayez, svp.";
                        IsModified = false;
                    }
                } else
                    txtMsgErreur.Text = "Entrez un courriel valide, svp.";
            } else
                txtMsgErreur.Text = "Saisir les champs obligatoires, svp!";
        }

        private void btnSupp_Click(object sender, RoutedEventArgs e) {
            //requete contacts et apres personnes

            MessageBoxResult ret = MessageBox.Show(this, "etes vous sure de supprimer?", "warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {
                Contact contactSupprimer = new Contact();
                contactSupprimer.IdUser = User.Id;
                contactSupprimer.IdContact = ContactModifier.Id;

                bool contactSupprime = ContactManager.SupprimerContact(contactSupprimer);
                if(contactSupprime) {

                    IsModified = true;
                    this.Close();

                }
            } else if(ret == MessageBoxResult.No) {
                txtMsgErreur.Text = "Le contact n'a pas ete supprime!";
                IsModified = false;
            }

            
        }

    }
}
