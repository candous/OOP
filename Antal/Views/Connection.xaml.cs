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

namespace Views
{
    public partial class Connection : Window
    {
        public Utilisateur User
        {
            get;
            set;
        }


        public Connection()
        {
            InitializeComponent();
            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;            
           DefinitionConnection.lireFichierConfiguration();

        }

        private void BtnValiderConnection_Click(object sender, RoutedEventArgs e)
        {
            User = null;
            string courriel = utilisateur.Text;
            string password = txtPwd.Password;

            //Console.WriteLine("je suis la mainWindows");
            if (courriel != "" && password != "")
            {
                if(DefinitionConnection.IsFile) {
                    try {
                        User = ManagerUtilisateur.recupererUtilisateurConnecte(courriel, password);

                        if(User == null) {
                            MessageBox.Show("Erreur de connection, veuillez réessayer, svp.", "Erreur de connection", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtPwd.Password = "";
                        } else {

                            //creer nouvelles fenetres ici!
                            //  MessageBox.Show("Ca marche ", "LOGIN FAIL", MessageBoxButton.OK, MessageBoxImage.Error);
                            ListeDescription.RemplirList();
                            Acceuil winAcceuil = new Acceuil(User);
                            winAcceuil.Show();
                            this.Close();
                        }
                    } catch(Exception) {
                        MessageBox.Show("Serveur indisponible (vérifie la connection à la base de données)");
                        
                    }
                }else
                    MessageBox.Show("Veuillez remplir le fichier de configuration");

            }
            else if (courriel == "")
                MessageBox.Show("Saisir le courriel, svp.", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if (password == "")
                MessageBox.Show("Saisir le password, svp.", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
                MessageBox.Show("Saisir les champs, svp.", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Exclamation);

        }
    }
}

