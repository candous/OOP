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
    /// <summary>
    /// Logique d'interaction pour ConfigurationVue.xaml
    /// </summary>
    public partial class ConfigurationVue : Window
    {

        public Utilisateur User { get; set; }

        List<IdDescription> listeFormation;
        List<IdDescription> listeLangue;
        List<IdDescription> listeCommunication;
        List<IdDescription> listeStatusCarriere;
        List<IdDescription> listeStage;
        List<IdDescription> listeResultat;
        List<IdDescription> listeEntrevue;
        List<IdDescription> listeDocument;


        public ConfigurationVue(Utilisateur user)
        {
            InitializeComponent();
            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
            this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;
            User = user;
            if (User.IdTypeUtilisateur == 1)
            {
                BtnComptes.Visibility = System.Windows.Visibility.Visible;
            }
            //ressources humaines
            else if (User.IdTypeUtilisateur == 2)
            {
                BtnComptes.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                BtnComptes.Visibility = System.Windows.Visibility.Hidden;
                StatistiquesMenu.Visibility = System.Windows.Visibility.Hidden;
            }
            userName.Content = User.Nom;


            ajouterFormationVue();
            ajouterLangueVue();
            ajouterCommunicationVue();
            ajouterCarriereVue();
            ajouterStageVue();
            ajouterResultatVue();
            ajouterEntrevueVue();
            ajouterDocumentVue();
        }


        private void ajouterFormationVue()
        {
            listeFormation = new List<IdDescription>();
            foreach(Formation id in ListeDescription.listFormations) {
                IdDescription description = new IdDescription();
                description.Id = id.Id;
                description.Description = id.Description;
                listeFormation.Add(description);
            }

            int i = 1;
            foreach (IdDescription description in listeFormation)
            {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données
               

                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                if (i % 2 == 0)
                    hPanel.Background = new SolidColorBrush(Color.FromArgb(100, 240, 238, 239));
                else
                    hPanel.Background = null;
                hPanel.Width = 531;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //etudiant 
                ajouterLabel(hPanel, description.Description, 15, 350);

                //ajouter l image pour supprimer
                Image imgSuppr = new Image();
                imgSuppr.Width = 25;
                imgSuppr.Height = 25;
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += supprimerFormation;
                imgSuppr.DataContext = description.Id;

                hPanel.Children.Add(imgSuppr);
                //ajouter le bouton au stackPanel principal

                ListeFormationVue.Children.Add(hPanel);
                i++;
            }
        }

        private void ajouterLangueVue()
        {
            listeLangue = new List<IdDescription>();
            foreach(Langue id in ListeDescription.listLangue) {
                IdDescription description = new IdDescription();
                description.Id = id.Id;
                description.Description = id.Description;
                listeLangue.Add(description);
            }

            int i = 1;
            foreach (IdDescription description in listeLangue)
            {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données


                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                if (i % 2 == 0)
                    hPanel.Background = new SolidColorBrush(Color.FromArgb(100, 240, 238, 239));
                else
                    hPanel.Background = null;
                hPanel.Width = 531;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //etudiant 
                ajouterLabel(hPanel, description.Description,15,350);

                //ajouter l image pour supprimer
                Image imgSuppr = new Image();
                imgSuppr.Width = 25;
                imgSuppr.Height = 25;
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += supprimerLangue;
                imgSuppr.DataContext = description.Id;

                hPanel.Children.Add(imgSuppr);
                //ajouter le bouton au stackPanel principal

                ListeLangueVue.Children.Add(hPanel);
                i++;
            }
        }

        private void ajouterCommunicationVue()
        {

            listeCommunication = new List<IdDescription>();
            foreach(IdDescription id in ListeDescription.listTypeCommunication) {
                IdDescription description = new IdDescription();
                description.Id = id.Id;
                description.Description = id.Description;
                listeCommunication.Add(description);
            }
            int i = 1;
            foreach (IdDescription description in listeCommunication)
            {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données


                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                if (i % 2 == 0)
                    hPanel.Background = new SolidColorBrush(Color.FromArgb(100, 240, 238, 239));
                else
                    hPanel.Background = null;
                hPanel.Width = 531;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //etudiant 
                ajouterLabel(hPanel, description.Description, 15, 350);

                //ajouter l image pour supprimer
                Image imgSuppr = new Image();
                imgSuppr.Width = 25;
                imgSuppr.Height = 25;
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += supprimerCommunication;
                imgSuppr.DataContext = description.Id;

                hPanel.Children.Add(imgSuppr);
                //ajouter le bouton au stackPanel principal

                ListeTypeCommunicationVue.Children.Add(hPanel);
                i++;
            }
        }
        
        private void ajouterCarriereVue()
        {
            listeStatusCarriere = new List<IdDescription>();
            foreach(IdDescription id in ListeDescription.listStatusCarrieres) {
                IdDescription description = new IdDescription();
                description.Id = id.Id;
                description.Description = id.Description;
                listeStatusCarriere.Add(description);
            }
            int i = 1;
            foreach (IdDescription description in listeStatusCarriere)
            {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données


                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                if (i % 2 == 0)
                    hPanel.Background = new SolidColorBrush(Color.FromArgb(100, 240, 238, 239));
                else
                    hPanel.Background = null;
                hPanel.Width = 531;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //etudiant 
                ajouterLabel(hPanel, description.Description, 15, 350);

                //ajouter l image pour supprimer
                Image imgSuppr = new Image();
                imgSuppr.Width = 25;
                imgSuppr.Height = 25;
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += supprimerCarriere;
                imgSuppr.DataContext = description.Id;

                hPanel.Children.Add(imgSuppr);
                //ajouter le bouton au stackPanel principal

                ListeStatusCarriereVue.Children.Add(hPanel);
                i++;
            }
        }

        private void ajouterStageVue()
        {
            listeStage = new List<IdDescription>();
            foreach(IdDescription id in ListeDescription.listTypeStage) {
                IdDescription description = new IdDescription();
                description.Id = id.Id;
                description.Description = id.Description;
                listeStage.Add(description);
            }
            int i = 1;
            foreach (IdDescription description in listeStage)
            {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données


                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                if (i % 2 == 0)
                    hPanel.Background = new SolidColorBrush(Color.FromArgb(100, 240, 238, 239));
                else
                    hPanel.Background = null;
                hPanel.Width = 531;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //etudiant 
                ajouterLabel(hPanel, description.Description, 15, 350);

                //ajouter l image pour supprimer
                Image imgSuppr = new Image();
                imgSuppr.Width = 25;
                imgSuppr.Height = 25;
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += supprimerStage;
                imgSuppr.DataContext = description.Id;

                hPanel.Children.Add(imgSuppr);
                //ajouter le bouton au stackPanel principal

                ListeTypeStageVue.Children.Add(hPanel);
                i++;
            }
        }

        private void ajouterResultatVue()
        {

            listeResultat = new List<IdDescription>();
            foreach(IdDescription id in ListeDescription.listTypeResultat) {
                IdDescription description = new IdDescription();
                description.Id = id.Id;
                description.Description = id.Description;
                listeResultat.Add(description);
            }
            int i = 1;
            foreach (IdDescription description in listeResultat)
            {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données


                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                if (i % 2 == 0)
                    hPanel.Background = new SolidColorBrush(Color.FromArgb(100, 240, 238, 239));
                else
                    hPanel.Background = null;
                hPanel.Width = 531;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //etudiant 
                ajouterLabel(hPanel, description.Description, 15, 350);

                //ajouter l image pour supprimer
                Image imgSuppr = new Image();
                imgSuppr.Width = 25;
                imgSuppr.Height = 25;
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += supprimerResultat;
                imgSuppr.DataContext = description.Id;

                hPanel.Children.Add(imgSuppr);
                //ajouter le bouton au stackPanel principal

                ListeResultatVue.Children.Add(hPanel);
                i++;
            }
        }

        private void ajouterEntrevueVue()
        {
            listeEntrevue = new List<IdDescription>();
            foreach(IdDescription id in ListeDescription.listTypeEntrevue) {
                IdDescription description = new IdDescription();
                description.Id = id.Id;
                description.Description = id.Description;
                listeEntrevue.Add(description);
            }
            int i = 1;
            foreach (IdDescription description in listeEntrevue)
            {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données


                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                if (i % 2 == 0)
                    hPanel.Background = new SolidColorBrush(Color.FromArgb(100, 240, 238, 239));
                else
                    hPanel.Background = null;
                hPanel.Width = 531;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //etudiant 
                ajouterLabel(hPanel, description.Description, 15, 350);

                //ajouter l image pour supprimer
                Image imgSuppr = new Image();
                imgSuppr.Width = 25;
                imgSuppr.Height = 25;
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += supprimerEntrevue;
                imgSuppr.DataContext = description.Id;

                hPanel.Children.Add(imgSuppr);
                //ajouter le bouton au stackPanel principal

                ListeTypeEntrevueVue.Children.Add(hPanel);
                i++;
            }
        }

        private void ajouterDocumentVue()
        {
            listeDocument = new List<IdDescription>();
            foreach(IdDescription id in ListeDescription.listTypeDocument) {
                IdDescription description = new IdDescription();
                description.Id = id.Id;
                description.Description = id.Description;
                listeDocument.Add(description);
            }

            int i = 1;
            foreach (IdDescription description in listeDocument)
            {
                // !!!!!!!!!!!!!!!!!!!!!!!!!   a verifer les margins quand on aura les données


                //layout du bouton
                StackPanel hPanel = new StackPanel();
                hPanel.Orientation = Orientation.Horizontal;
                if (i % 2 == 0)
                    hPanel.Background = new SolidColorBrush(Color.FromArgb(100, 240, 238, 239));
                else
                    hPanel.Background = null;
                hPanel.Width = 531;
                hPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                hPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //etudiant 
                ajouterLabel(hPanel, description.Description, 15, 350);

                //ajouter l image pour supprimer
                Image imgSuppr = new Image();
                imgSuppr.Width = 25;
                imgSuppr.Height = 25;
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.Source = new BitmapImage(new Uri(@"images\iconX.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += supprimerDocument;
                imgSuppr.DataContext = description.Id;

                hPanel.Children.Add(imgSuppr);
                //ajouter le bouton au stackPanel principal

                ListeDocumentVue.Children.Add(hPanel);
                i++;
            }
        }
        
        //methode pour generer label
   
        private void ajouterLabel(StackPanel hPanel, string content, int fontSize, int width)
        {

            Label label = new Label();
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 0, 151, 229));
            label.FontSize = fontSize;
            label.Width = width;
            label.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            label.Content = content;
            hPanel.Children.Add(label);
        }


        //evenment supprimer
        private void supprimerFormation(object sender, RoutedEventArgs e)        {
            int id = (int)((Image)sender).DataContext;

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer cette formation ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {

                ManagerInformation.supprimerFormation(id);

                MessageBox.Show("Formation supprimée.", "Suppression d'une formation", MessageBoxButton.OK, MessageBoxImage.Information);


                ListeDescription.RemplirList();
                
                ListeFormationVue.Children.Clear();
                
                if(ListeDescription.listFormations != null)
                    ajouterFormationVue();

            } else if(ret == MessageBoxResult.No) {
                MessageBox.Show("Suppression annulée.", "Suppression d'une formation", MessageBoxButton.OK, MessageBoxImage.Information);

            }


        }
       
        private void supprimerLangue(object sender, RoutedEventArgs e)
        {
            int id = (int)((Image)sender).DataContext;

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer cette langue ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {

                ManagerInformation.supprimerLangue(id);

                MessageBox.Show("Langue supprimée.", "Suppression d'une langue", MessageBoxButton.OK, MessageBoxImage.Information);


                ListeDescription.RemplirList();

                ListeLangueVue.Children.Clear();

                if(ListeDescription.listLangue != null)
                    ajouterLangueVue();

            } else if(ret == MessageBoxResult.No) {
                MessageBox.Show("Suppression annulée.", "Suppression d'une langue", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }
        private void supprimerCommunication(object sender, RoutedEventArgs e)
        {
            int id = (int)((Image)sender).DataContext;

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer cette communication ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {

                ManagerInformation.supprimerStatusCommunication(id);

                MessageBox.Show("Communication supprimée.", "Suppression d'une communication", MessageBoxButton.OK, MessageBoxImage.Information);


                ListeDescription.RemplirList();

                ListeTypeCommunicationVue.Children.Clear();

                if(ListeDescription.listTypeCommunication != null)
                    ajouterCommunicationVue();

            } else if(ret == MessageBoxResult.No) {
                MessageBox.Show("Suppression annulée.", "Suppression d'une communication", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }       
        private void supprimerCarriere(object sender, RoutedEventArgs e)
        {
            int id = (int)((Image)sender).DataContext;

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer cette carrière ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {

                ManagerInformation.supprimerStatusCarriere(id);

                MessageBox.Show("Carrière supprimée.", "Suppression d'une carrière", MessageBoxButton.OK, MessageBoxImage.Information);


                ListeDescription.RemplirList();

                ListeStatusCarriereVue.Children.Clear();

                if(ListeDescription.listStatusCarrieres != null)
                    ajouterCarriereVue();

            } else if(ret == MessageBoxResult.No) {
                MessageBox.Show("Suppression annulée.", "Suppression d'une carrière", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
        private void supprimerStage(object sender, RoutedEventArgs e)
        {
            int id = (int)((Image)sender).DataContext;

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer ce stage ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {

                ManagerInformation.supprimerTypeStage(id);

                MessageBox.Show("Stage supprimé.", "Suppression d'un stage", MessageBoxButton.OK, MessageBoxImage.Information);


                ListeDescription.RemplirList();

                ListeTypeStageVue.Children.Clear();

                if(ListeDescription.listTypeStage != null)
                    ajouterStageVue();

            } else if(ret == MessageBoxResult.No) {
                MessageBox.Show("Suppression annulée.", "Suppression d'un stage", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
        private void supprimerResultat(object sender, RoutedEventArgs e)
        {
            int id = (int)((Image)sender).DataContext;

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer ce résultat ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {

                ManagerInformation.supprimerTypeResultat(id);

                MessageBox.Show("Résultat supprimé.", "Suppression d'un résultat", MessageBoxButton.OK, MessageBoxImage.Information);


                ListeDescription.RemplirList();

                ListeResultatVue.Children.Clear();

                if(ListeDescription.listTypeResultat != null)
                    ajouterResultatVue();

            } else if(ret == MessageBoxResult.No) {
                MessageBox.Show("Suppression annulée.", "Suppression d'un résultat", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
        private void supprimerEntrevue(object sender, RoutedEventArgs e)
        {
            int id = (int)((Image)sender).DataContext;

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer cette entrevue ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {

                ManagerInformation.supprimerTypeEntrevue(id);

                MessageBox.Show("Entrevue supprimée.", "Suppression d'une entrevue", MessageBoxButton.OK, MessageBoxImage.Information);


                ListeDescription.RemplirList();

                ListeTypeEntrevueVue.Children.Clear();

                if(ListeDescription.listTypeEntrevue != null)
                    ajouterEntrevueVue();

            } else if(ret == MessageBoxResult.No) {
                MessageBox.Show("Suppression annulée.", "Suppression d'une entrevue", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
        private void supprimerDocument(object sender, RoutedEventArgs e)
        {
            int id = (int)((Image)sender).DataContext;

            MessageBoxResult ret = MessageBox.Show(this, "Êtes-vous sûr de vouloir supprimer ce document ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {

                ManagerInformation.supprimerTypeDocument(id);

                MessageBox.Show("Document supprimé.", "Suppression d'un document", MessageBoxButton.OK, MessageBoxImage.Information);


                ListeDescription.RemplirList();

                ListeDocumentVue.Children.Clear();

                if(ListeDescription.listTypeDocument != null)
                    ajouterDocumentVue();

            } else if(ret == MessageBoxResult.No) {
                MessageBox.Show("Suppression annulée.", "Suppression d'un document", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }


        //evenement ajouter
        private void BtnAjouterFormation_Click(object sender, RoutedEventArgs e)
        {

            if(!choixAjouterFormation.Text.Equals("")) {
                string entree = choixAjouterFormation.Text;
                Formation f = new Formation();
                f.Description = entree;
                f.NbHeure = 10;
                ManagerInformation.ajouterFormation(f);

                ListeDescription.RemplirList();
                ListeFormationVue.Children.Clear();
                ajouterFormationVue();
                MessageBox.Show("Formation ajoutée.", "Ajout d'une formation", MessageBoxButton.OK, MessageBoxImage.Information);

            }else
                MessageBox.Show("Veuillez remplir les champs obligatoires.", "Ajout d'une formation", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void BtnAjouterLangue_Click(object sender, RoutedEventArgs e)
        {
            if(!choixAjouterLangue.Text.Equals("")) {
                string entree = choixAjouterLangue.Text;
                Langue l = new Langue();
                l.Description = entree;
                
                ManagerInformation.ajouterLangue(l);

                ListeDescription.RemplirList();
                ListeLangueVue.Children.Clear();
                ajouterLangueVue();
                MessageBox.Show("Langue ajoutée.", "Ajout d'une langue", MessageBoxButton.OK, MessageBoxImage.Information);

            } else
                MessageBox.Show("Veuillez remplir les champs obligatoires.", "Ajout d'une langue", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void BtnAjouterTypeCommunication_Click(object sender, RoutedEventArgs e)
        {
            if(!choixAjouterCommunication.Text.Equals("")) {
                string entree = choixAjouterCommunication.Text;
                IdDescription desc = new IdDescription();
                desc.Description = entree;

                ManagerInformation.ajouterTypeCommunication(desc);

                ListeDescription.RemplirList();
                ListeTypeCommunicationVue.Children.Clear();
                ajouterCommunicationVue();
                MessageBox.Show("Type de communication ajoutée.", "Ajout d'un type de communication", MessageBoxButton.OK, MessageBoxImage.Information);

            } else
                MessageBox.Show("Veuillez remplir les champs obligatoires.", "Ajout d'un type de communication", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void BtnAjouterStatusCarriere_Click(object sender, RoutedEventArgs e)
        {
            if(!choixAjouterCarriere.Text.Equals("")) {
                string entree = choixAjouterCarriere.Text;
                IdDescription desc = new IdDescription();
                desc.Description = entree;

                ManagerInformation.ajouterStatusCarriere(desc);

                ListeDescription.RemplirList();
                ListeStatusCarriereVue.Children.Clear();
                ajouterCarriereVue();
                MessageBox.Show("Status carrière ajouté.", "Ajout d'un status de carrière", MessageBoxButton.OK, MessageBoxImage.Information);

            } else
                MessageBox.Show("Veuillez remplir les champs obligatoires.", "Ajout d'un status de carrière", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void BtnAjouterTypeStage_Click(object sender, RoutedEventArgs e)
        {
            if(!choixAjouterStage.Text.Equals("")) {
                string entree = choixAjouterStage.Text;
                IdDescription desc = new IdDescription();
                desc.Description = entree;

                ManagerInformation.ajouterTypeStage(desc);

                ListeDescription.RemplirList();
                ListeTypeStageVue.Children.Clear();
                ajouterStageVue();
                MessageBox.Show("Type de stage ajouté.", "Ajout d'un type de stage", MessageBoxButton.OK, MessageBoxImage.Information);

            } else
                MessageBox.Show("Veuillez remplir les champs obligatoires.", "Ajout d'un type de stage", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void BtnAjouterResultat_Click(object sender, RoutedEventArgs e)
        {
            if(!choixAjouterResultat.Text.Equals("")) {
                string entree = choixAjouterResultat.Text;
                IdDescription desc = new IdDescription();
                desc.Description = entree;

                ManagerInformation.ajouterTypeResultat(desc);

                ListeDescription.RemplirList();
                ListeResultatVue.Children.Clear();
                ajouterResultatVue();
                MessageBox.Show("Résultat ajouté.", "Ajout d'un résultat", MessageBoxButton.OK, MessageBoxImage.Information);

            } else
                MessageBox.Show("Veuillez remplir les champs obligatoires.", "Ajout d'un résultat", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void BtnAjouterTypeEntrevue_Click(object sender, RoutedEventArgs e)
        {
            if(!choixAjouterEntrevue.Text.Equals("")) {
                string entree = choixAjouterEntrevue.Text;
                IdDescription desc = new IdDescription();
                desc.Description = entree;

                ManagerInformation.ajouterTypeEntrevue(desc);

                ListeDescription.RemplirList();
                ListeTypeEntrevueVue.Children.Clear();
                ajouterEntrevueVue();
                MessageBox.Show("Type d'entrevue ajouté", "Ajout d'un type d'entrevue", MessageBoxButton.OK, MessageBoxImage.Information);

            } else
                MessageBox.Show("Veuillez remplir les champs obligatoires.", "Ajout d'un type d'entrevue", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void BtnAjouterDocument_Click(object sender, RoutedEventArgs e)
        {
            if(!choixAjouterDocument.Text.Equals("")) {
                string entree = choixAjouterDocument.Text;
                IdDescription desc = new IdDescription();
                desc.Description = entree;

                ManagerInformation.ajouterTypeDocument(desc);

                ListeDescription.RemplirList();
                ListeDocumentVue.Children.Clear();
                ajouterDocumentVue();
                MessageBox.Show("Document ajouté.", "Ajout d'un document", MessageBoxButton.OK, MessageBoxImage.Information);

            } else
                MessageBox.Show("Veuillez remplir les champs obligatoires.", "Ajout d'un document", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        //evenement menu gauche 
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

        private void Image_MouseDown(object sender, MouseButtonEventArgs e) {
           
            Acceuil vue = new Acceuil(User);
            vue.Show();
            this.Close();
        }
    }
}
