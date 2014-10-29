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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConfigurationAntal {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public bool IsFile;

        public String Serveur;
        public String DataBase;
        public String DocumentFolder;

        public string[] lines;
        public string[] newLines;


        string fileName;

        public MainWindow() {
            InitializeComponent();
            IsFile = true;
            fileName = "config.antal";


            lireFichierConfiguration();

            if(IsFile) {


                ServeurNameVue.Text = Serveur;
                BdNameVue.Text = DataBase;
                RepertoireNameVue.Text = DocumentFolder;
            }

        }

        public void lireFichierConfiguration() {

            try {

                using(StreamReader reader = new StreamReader(@"config.antal")) {
                    reader.ReadToEnd();
                }
            } catch(FileNotFoundException) {
                IsFile = false;
            }

            if(IsFile) {
                lines = System.IO.File.ReadAllLines(@"config.antal");

                foreach(string line in lines) {
                    if(line.StartsWith("server"))
                        Serveur = line.Substring(8);

                    if(line.StartsWith("database"))
                        DataBase = line.Substring(10);

                    if(line.StartsWith("documents"))
                        DocumentFolder = line.Substring(11);
                }
            }
        }



        public void ecrireFichierConfiguration() {
            if(!ServeurNameVue.Text.Equals("") &&
                !BdNameVue.Text.Equals("") && 
                !RepertoireNameVue.Text.Equals("") ) {

                if(System.IO.File.Exists(fileName)) {
                    try {
                        System.IO.File.Delete(fileName);
                        
                    } catch(System.IO.IOException e) {
                        Console.WriteLine(e.Message);
                        return;
                    }
                }

                if(!System.IO.File.Exists(fileName)) {

                    using(System.IO.FileStream fs = System.IO.File.Create(fileName))
                        for(byte i = 0; i < 100; i++)
                            fs.WriteByte(i);
                   

                }          


                string serveurTemps = "server :"+ ServeurNameVue.Text;
                string BdTemps = "database :" + BdNameVue.Text;
                string RerpertoireTemps = "documents :" + RepertoireNameVue.Text;


                newLines = new string[3];
                newLines[0] = serveurTemps;
                newLines[1] = BdTemps;
                newLines[2] = RerpertoireTemps;

                System.IO.File.WriteAllLines(fileName, newLines);

                MessageBox.Show("Configuration mise a jour", " Configuration", MessageBoxButton.OK);
            } else
                MessageBox.Show("Veuillez Remplir Tous les Champs", "", MessageBoxButton.OK);
        }


        private void BtnModifierFichierConfiguration_Click(object sender, RoutedEventArgs e) {
            ecrireFichierConfiguration();
        }
    }
}
