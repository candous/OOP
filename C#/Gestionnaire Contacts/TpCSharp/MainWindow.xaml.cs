using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TpBLL;
using TpEntities;


namespace TpCSharp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Personne User { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            string courriel=txtCourriel.Text;
            string password=txtPwd.Password;
            

            if (courriel != "" && password != "")
            {
                try {
                    User = PersonneManager.GetUserLogin(courriel, password);
                    if(User == null) {
                        MessageBox.Show("Login fail, veulliez ressayer, svp.", "LOGIN FAIL", MessageBoxButton.OK, MessageBoxImage.Error);
                        txtPwd.Password = "";
                    } else {
                        this.Visibility = System.Windows.Visibility.Hidden;
                        //creer nouvelles fenetres ici!
                        Contacts contactsWindow = new Contacts(User);
                        contactsWindow.Show();

                        //update lastVisit BD
                        int retour = PersonneManager.UpdateLastVisit(DateTime.Now, User.Id);
                        this.Close();
                    }
                } catch(Exception) {
                    MessageBox.Show("serveur indisponible");
                    this.Close();
                    throw;
                }
               
            }
            else if(courriel == "")
                MessageBox.Show("Saisir le courriel, svp.", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if (password == "")
                MessageBox.Show("Saisir le password, svp.", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
                MessageBox.Show("Saisir les champs, svp.", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //SQLHelper.GetUserLogin(courriel, password);
        }
        //validation courriel
        private void txtCourriel_KeyUp(object sender, KeyEventArgs e)
        {
            //if(Regex.IsMatch(txtCourriel.Text, @"^[a-zA-Z0-9\w\.-]*@[a-zA-Z0-9\w\.-]*\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            //    btnConnect.IsEnabled = true;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            txtCourriel.Text = "jf@gmail.com";
            txtPwd.Password = "12345";

            //positionement de la fenetre lors du chargement
            var desktop = System.Windows.SystemParameters.WorkArea;
            this.Left = desktop.Right - 900;//this.Width
            this.Top = desktop.Bottom - 600;//this.Height
           // btnConnect.IsEnabled = false;
        }

        private void Label_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Inscription fenetreInscription = new Inscription();
            fenetreInscription.Show();
        }
    }
}
