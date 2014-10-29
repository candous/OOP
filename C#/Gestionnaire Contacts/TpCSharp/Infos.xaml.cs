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
using TpEntities;

namespace TpCSharp {
    /// <summary>
    /// Logique d'interaction pour Infos.xaml
    /// </summary>
    public partial class Infos : Window {
        //proprietés
        public Personne User {
            get;
            set;
        }
        public Personne Contact {
            get;
            set;
        }
        public bool IsModified {
            get;
            set;
        }

        //constructeur
        public Infos(Personne user, Personne contact) {
            InitializeComponent();
            User = user;
            Contact = contact;
            grid.DataContext = Contact;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e) {

            //positionement de la fenetre lors du chargement
            var desktop = System.Windows.SystemParameters.WorkArea;
            this.Left = desktop.Right - 900;//this.Width
            this.Top = desktop.Top;//this.Height



        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            ModifyContact mc = new ModifyContact(User, Contact);
            mc.ShowDialog();
            if(mc.IsModified)
                IsModified = true;
            
            this.Close();

        }




    }
}
