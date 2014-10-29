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
    /// Logique d'interaction pour AddContact.xaml
    /// </summary>
    public partial class AddContact : Window
    {

        public bool IsModifier {
            get;
            set;
        }
        public string NewUrl { get; set; }
        public Personne User { get; set; }
        public AddContact(Personne user)
        {
            InitializeComponent();
            NewUrl = null;
            this.User = user;

            //positionement de la fenetre lors du chargement
            var desktop = System.Windows.SystemParameters.WorkArea;
            this.Left = desktop.Right - 900;//this.Width
            this.Top = desktop.Top;
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
                string filePath = nouveauRep + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Guid.NewGuid().ToString("N") + System.IO.Path.GetFileName(dlg.FileName);
                System.IO.File.Copy(dlg.FileName, filePath, true);
                NewUrl = filePath;

                //afficher image
                string filename = dlg.FileName;
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
                affichePhoto.Fill = brush;
            }
        }

        private void btnInscrire_Click(object sender, RoutedEventArgs e)
        {
            //si les champs obligatoires ont ete saisies, on cree un user
            if (txtCourriel.Text.Length > 0 && txtNom.Text.Length > 0)
            {
                if (Regex.IsMatch(txtCourriel.Text, @"^[a-zA-Z0-9\w\.-]*@[a-zA-Z0-9\w\.-]*\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    Personne personneAjoute = new Personne();
                    Contact contactAjoute = new Contact();

                    personneAjoute.Adresse = txtAdresse.Text;
                    if (date.Text.Length <= 0)
                        personneAjoute.Aniversaire = null;
                    else
                        personneAjoute.Aniversaire = Convert.ToDateTime(date.Text);

                    personneAjoute.Celulaire = txtCel.Text;
                    personneAjoute.Compagnie = txtCompagnie.Text;
                    personneAjoute.Courriel = txtCourriel.Text;
                    personneAjoute.LastVisit = null;
                    personneAjoute.Nom = txtNom.Text;
                    personneAjoute.Password = null;
                    personneAjoute.IsUser = false;
                    personneAjoute.IsVisible = false;
                    personneAjoute.Pays = txtPays.Text;
                    personneAjoute.Province = txtProvince.Text;
                    personneAjoute.SiteWeb = txtSite.Text;
                    personneAjoute.Telephone = txtTelephone.Text;
                    personneAjoute.Ville = txtVille.Text;
                    if (NewUrl == null)
                        personneAjoute.UrlPhoto = @"images\photoProfile.png";//utiliser une valeur par default
                    else
                        personneAjoute.UrlPhoto = NewUrl;

                    contactAjoute.IdUser = User.Id;
                    contactAjoute.IsFavorite = (bool)chkFavorite.IsChecked;

                    bool userCree = PersonneManager.InsertUser(personneAjoute);
                    //tester si la insertion est bien passe
                    if (userCree)
                    {
                        contactAjoute.IdContact = personneAjoute.Id;
                        bool contCree = ContactManager.InsertContact(contactAjoute);
                        if (contCree)
                        {
                            IsModifier = true;
                            this.Close();
                        }
                        else
                            IsModifier = false;
                    }
                    else
                        txtMsgErreur.Text = "Le contact n'a pas ete cree, ressayez, svp.";
                }
                else
                    txtMsgErreur.Text = "Entrez un courriel valide, svp.";
            }
            else
                txtMsgErreur.Text = "Saisir les champs obligatoires, svp!";
        }
    }
}
