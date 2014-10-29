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

namespace TpCSharp
{

    /// <summary>
    /// Logique d'interaction pour Inscription.xaml
    /// </summary>
    ///

    public partial class Inscription : Window
    {
        public string newUrl { get; set; }
        public Inscription()
        {
            newUrl = null;
            InitializeComponent();
        }

        private void Button_Loaded_1(object sender, RoutedEventArgs e)
        {
            //positionement de la fenetre lors du chargement
            var desktop = System.Windows.SystemParameters.WorkArea;
            this.Left = desktop.Right - 900;//this.Width
            this.Top = desktop.Top;

            txtAdresse.IsEnabled = false;
            txtCel.IsEnabled = false;
            txtCompagnie.IsEnabled = false;
            txtCourriel.IsEnabled = true;
            txtNom.IsEnabled = false;
            txtPays.IsEnabled = false;
            txtProvince.IsEnabled = false;
            txtPwd.IsEnabled = true;
            txtSite.IsEnabled = false;
            txtTelephone.IsEnabled = false;
            txtVille.IsEnabled = false;
            btnChoixPhoto.IsEnabled = true;
            btnInscrire.IsEnabled = false;

        }

        private void txtCourriel_LostFocus(object sender, RoutedEventArgs e)
        {
            //adresse de courriel valide
            if (Regex.IsMatch(txtCourriel.Text, @"^[a-zA-Z0-9\w\.-]*@[a-zA-Z0-9\w\.-]*\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                //get personne by courriel
                bool userExists = PersonneManager.UserExistsByEmail(txtCourriel.Text);
               //user n'existe pas
                if (!userExists)
                {
                    txtAdresse.IsEnabled = true;
                    txtCel.IsEnabled = true;
                    txtCompagnie.IsEnabled = true;
                    txtCourriel.IsEnabled = true;
                    txtNom.IsEnabled = true;
                    txtPays.IsEnabled = true;
                    txtProvince.IsEnabled = true;
                    txtPwd.IsEnabled = true;
                    txtSite.IsEnabled = true;
                    txtTelephone.IsEnabled = true;
                    txtVille.IsEnabled = true;
                    btnChoixPhoto.IsEnabled = true;
                    btnInscrire.IsEnabled = true;
                    txtMsgErreur.Text = "";
                }
                else //user invalide
                {
                    txtAdresse.IsEnabled = false;
                    txtCel.IsEnabled = false;
                    txtCompagnie.IsEnabled = false;
                    txtCourriel.IsEnabled = true;
                    txtNom.IsEnabled = false;
                    txtPays.IsEnabled = false;
                    txtProvince.IsEnabled = false;
                    txtPwd.IsEnabled = true;
                    txtSite.IsEnabled = false;
                    txtTelephone.IsEnabled = false;
                    txtVille.IsEnabled = false;
                    btnChoixPhoto.IsEnabled = true;
                    btnInscrire.IsEnabled = false;
                    txtMsgErreur.Text = "Un user avec ce courriel existe deja!";
                }
            }
            else
            {
                txtMsgErreur.Text = "Adresse de courriel n'est pas valide!";
            }
        }

        private void btnInscrire_Click(object sender, RoutedEventArgs e)
        {
            //si les champs obligatoires ont ete saisies, on cree un user
            if(txtCourriel.Text.Length>0 && txtPwd.Password.Length>0 && txtNom.Text.Length>0)
            {
                Personne personneInscrite = new Personne();
                personneInscrite.Adresse = txtAdresse.Text;
                if (date.Text.Length <= 0)
                    personneInscrite.Aniversaire = null;
                else
                personneInscrite.Aniversaire = Convert.ToDateTime(date.Text);

                personneInscrite.Celulaire = txtCel.Text;
                personneInscrite.Compagnie = txtCompagnie.Text;
                personneInscrite.Courriel = txtCourriel.Text;
                personneInscrite.IsUser = true;
                personneInscrite.IsVisible = (bool)chkVisible.IsChecked;//vient comme bool? par default
                personneInscrite.LastVisit = DateTime.Now;
                personneInscrite.ListeContact = new List<Contact>();
                personneInscrite.ListePersonnes = new List<Personne>();
                personneInscrite.Nom = txtNom.Text;
                personneInscrite.Password = txtPwd.Password;
                personneInscrite.Pays = txtPays.Text;
                personneInscrite.Province = txtProvince.Text;
                personneInscrite.SiteWeb = txtSite.Text;
                personneInscrite.Telephone = txtTelephone.Text;
                personneInscrite.Ville = txtVille.Text;
                if (newUrl == null)
                    personneInscrite.UrlPhoto = @"images\photoProfile.png";//utiliser une valeur par default
                else
                    personneInscrite.UrlPhoto = newUrl;

                
                bool userCree=PersonneManager.InsertUser(personneInscrite);
                //tester si la insertion est bien passe
                if (userCree)
                {
                    //stocker le user dans la fenetre login
                    MainWindow.User = personneInscrite;
                   
                    //nouvelles fenetres
                    Contacts contactsWindow = new Contacts(personneInscrite);
                    contactsWindow.Show();
                    this.Close();

                }
                else
                    txtMsgErreur.Text = "L'utilisateur n'a pas ete cree, ressayez, svp.";
            }
            else
                txtMsgErreur.Text = "Saisir les champs obligatoires, svp!";
        }

        private void btnChoixPhoto_Click(object sender, RoutedEventArgs e)
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
                string nouveauRep = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\images\\";
                if (!Directory.Exists(nouveauRep))
                {
                    Directory.CreateDirectory(nouveauRep);
                }
                //nom unique
                string filePath = nouveauRep + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Guid.NewGuid().ToString("N")+System.IO.Path.GetFileName(dlg.FileName);
                System.IO.File.Copy(dlg.FileName, filePath, true);
                newUrl=filePath;

                //afficher image
                string filename = dlg.FileName;
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
                affichePhoto.Fill = brush;
            }
        }
    }
}