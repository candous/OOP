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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TpBLL;
using TpEntities;


namespace TpCSharp {
    public partial class Contacts : Window {
        // propriete
        public Personne User {
            get;
            set;
        }

        public Contacts(Personne user) {
            InitializeComponent();
            User = user;
            User.ListeContact = ContactManager.GetListeContactsByUserId(User.Id);
            User.ListePersonnes = PersonneManager.GetListePersonnesByListeContacts(User.ListeContact);
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e) {

            //positionement de la fenetre lors du chargement
            var desktop = System.Windows.SystemParameters.WorkArea;
            this.Left = desktop.Left + 100;//this.Width
            this.Top = desktop.Top + 50;//this.Height

            // recuperation du nom 
            NomUser.Content = User.Nom;

            // image du contact
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(@"" + User.UrlPhoto, UriKind.RelativeOrAbsolute));
            img.Stretch = Stretch.Fill;
            ImgUser.Source = img.Source;

        }

        // evenement au chargement du stack panel
        private void StackPanel_Loaded(object sender, RoutedEventArgs e) {
            ajouterContactTrier(User.ListeContact, User.ListePersonnes);            
        }

        //methode pour ajouter les controles Avec tri favoris et commun contact
        private void ajouterContactTrier(List<Contact> listeTousContact, List<Personne> listeToutesPersonnes) {
            // Separation des contacts en fonction des favoris ou pas favoris
            List<Personne> favoris = new List<Personne>();
            List<Personne> communContact = new List<Personne>();

            for(int i = 0; i < listeTousContact.Count; i++) {
                if(listeTousContact[i].IsFavorite) {
                    favoris.Add(listeToutesPersonnes[i]);
                } else {
                    communContact.Add(listeToutesPersonnes[i]);
                }

            }
            if(favoris.Count > 0) {
                Label l1 = new Label();
                l1.Content = "Favoris";
                l1.Foreground = Brushes.Azure;
                l1.FontSize = 24;
                ListeContactVue.Children.Add(l1);


                ajoutBoutonContact(favoris);
            }


            Label l2 = new Label();
            l2.Content = "Contact";
            l2.Foreground = Brushes.Azure;
            l2.FontSize = 24;
            ListeContactVue.Children.Add(l2);

            ajoutBoutonContact(communContact);
        }


        //methode pour ajouter les controles contacts en fonction d une liste

        private void ajoutBoutonContact(List<Personne> listeContact) {

            foreach(Personne contact in listeContact) {

                // Layout des contact avec un bouton
                Button b = new Button();
                b.DataContext = contact;
                b.Margin = new Thickness(5);
                b.Click += InfoContact;
                // b.Width = 216;
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color =
                    Color.FromArgb(
                        50, // Specifies the transparency of the color.
                        100, // Specifies the amount of red.
                        156, // specifies the amount of green.
                        255); // Specifies the amount of blue.

                b.Background = mySolidColorBrush;
                b.BorderThickness = new Thickness(0, 0, 0, 1);


                // utilisation d un stack pannel pour le layout du bouton
                StackPanel st = new StackPanel();
                st.Orientation = Orientation.Horizontal;
                st.Width = 234;
                st.HorizontalAlignment = HorizontalAlignment.Left;
                st.Background = null;



                // pour effet blur a voir demain avec les autres
                //BlurBitmapEffect myBlurEffect = new BlurBitmapEffect();
                //myBlurEffect.Radius = 5;
                //myBlurEffect.KernelType = KernelType.Gaussian;

                // ajoute image ou stackPanel
                Image imgContact = new Image();
                imgContact.Width = 48;
                imgContact.Height = 48;
                imgContact.Margin = new Thickness(2, 0, 2, 0);
                imgContact.Stretch = Stretch.Fill;
                imgContact.Source = new BitmapImage(new Uri(@"" + contact.UrlPhoto, UriKind.RelativeOrAbsolute));
                //imgContact.BitmapEffect = myBlurEffect;
                st.Children.Add(imgContact);

                // cree un sackPanel vertical pour le texte
                StackPanel stackLabel = new StackPanel();
                stackLabel.Orientation = Orientation.Vertical;
                stackLabel.Width = 100;
                st.Children.Add(stackLabel);

                //Ajute deu label au StackPanelVertical
                Label nomContact = new Label();
                nomContact.Content = contact.Nom;
                nomContact.Margin = new Thickness(1, 0, 1, 0);
                stackLabel.Children.Add(nomContact);

                Label emailContact = new Label();
                emailContact.Content = contact.Courriel;
                emailContact.Margin = new Thickness(1, 0, 1, 0);
                stackLabel.Children.Add(emailContact);

                //ajout image supprimer
                Image imgSuppr = new Image();
                imgSuppr.Width = 45;
                imgSuppr.Height = 45;
                imgSuppr.Stretch = Stretch.Fill;
                imgSuppr.DataContext = contact;
                imgSuppr.Source = new BitmapImage(new Uri(@"images\supprContact.png", UriKind.RelativeOrAbsolute));
                imgSuppr.MouseDown += SupprimerContact;
                st.Children.Add(imgSuppr);

                //ajoute du layout au bouton
                b.Content = st;
                ListeContactVue.Children.Add(b);

            }
        }

        // Handler pour afficher info Contact
        private void InfoContact(object sender, RoutedEventArgs e) {

            Button b = (Button)sender;
            Personne contact = (Personne)b.DataContext;

            Infos infoWindows = new Infos(User, contact);
            infoWindows.ShowDialog();
            if(infoWindows.IsModified) {
                User.ListeContact = ContactManager.GetListeContactsByUserId(User.Id);
                User.ListePersonnes = PersonneManager.GetListePersonnesByListeContacts(User.ListeContact);
                ListeContactVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste
                ajouterContactTrier(User.ListeContact, User.ListePersonnes); 
            }
            infoWindows.Close();
        }

        // Handler pour supprimer un contact
        private void SupprimerContact(object sender, RoutedEventArgs e) {

            MessageBoxResult ret = MessageBox.Show(this, "etes vous sure de vouloir supprimer ce contact?", "warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(ret == MessageBoxResult.Yes) {
                Image img = (Image)sender;
                Personne contact = (Personne)img.DataContext;
                Contact contactSupprimer = new Contact();
                contactSupprimer.IdUser = User.Id;
                contactSupprimer.IdContact = contact.Id;

                bool contactSupprime = ContactManager.SupprimerContact(contactSupprimer);
                if(contactSupprime) {

                    // enlever le contact de la liste en memoire
                    User.ListeContact = ContactManager.GetListeContactsByUserId(User.Id);
                    User.ListePersonnes = PersonneManager.GetListePersonnesByListeContacts(User.ListeContact);
                    //vider le stackpanel
                    ListeContactVue.Children.Clear();
                    //reafficher le stackPanel avec la nouvelle liste
                    ajouterContactTrier(User.ListeContact, User.ListePersonnes); 
                }
            } else if(ret == MessageBoxResult.No) {
                MessageBox.Show("Suppression annulée");
            }
        }

        // Handler ajouter un contact
        private void addContact_MouseDown(object sender, MouseButtonEventArgs e) {
            AddContact windowsAddContact = new AddContact(User);
            windowsAddContact.ShowDialog();
            if(windowsAddContact.IsModifier) {
                User.ListeContact = ContactManager.GetListeContactsByUserId(User.Id);
                User.ListePersonnes = PersonneManager.GetListePersonnesByListeContacts(User.ListeContact);
                ListeContactVue.Children.Clear();
                //reafficher le stackPanel avec la nouvelle liste
                ajouterContactTrier(User.ListeContact, User.ListePersonnes); 
            }

            windowsAddContact.Close();
        }

        //handler rechercher contact
        private void Button_Click(object sender, RoutedEventArgs e) {

            string critereRecherche = ChoixRecherche.SelectionBoxItem.ToString();
            List<Personne> resultRecherche = new List<Personne>();
            
            if(User.ListePersonnes.Count > 0) {
                foreach(Personne contact in User.ListePersonnes) {
                    switch(critereRecherche) {
                        case "nom":
                            if(contact.Nom.ToLower().Contains(SaisieRecherche.Text.ToLower())) {
                                resultRecherche.Add(contact);
                            }
                            break;
                        case "email":
                            if(contact.Courriel.ToLower().Contains(SaisieRecherche.Text.ToLower())) {
                                resultRecherche.Add(contact);
                            }
                            break;
                        case "ville":
                            if(contact.Ville.ToLower().Contains(SaisieRecherche.Text.ToLower())) {
                                resultRecherche.Add(contact);
                            }
                            break;
                        case "pays":
                            if(contact.Pays.ToLower().Contains(SaisieRecherche.Text.ToLower())) {
                                resultRecherche.Add(contact);
                            }
                            break;
                        case "province":
                            if(contact.Province.ToLower().Contains(SaisieRecherche.Text.ToLower())) {
                                resultRecherche.Add(contact);
                            }
                            break;
                        default:
                            break;
                    }
                }
                if(resultRecherche.Count > 0) {
                    ListeContactVue.Children.Clear();
                    Label l1 = new Label();
                    l1.Content = "Resultat Recherche";
                    l1.Foreground = Brushes.Azure;
                    l1.FontSize = 24;
                    ListeContactVue.Children.Add(l1);
                    ajoutBoutonContact(resultRecherche);
                }
            }
        }

        // pour experience utilisateur
        private void SaisieRecherche_GotFocus(object sender, RoutedEventArgs e) {
            SaisieRecherche.Text = "";
        }

        // si appuie sur la photo de l utilisateur
        private void ImgUser_MouseDown(object sender, MouseButtonEventArgs e) {
            Infos infoWindows = new Infos(User, User);
            infoWindows.ShowDialog();
            if(infoWindows.IsModified) {
                User = PersonneManager.GetUserLogin(User.Courriel, User.Password);
                this.ImgUser.Source = new BitmapImage(new Uri(@""+User.UrlPhoto, UriKind.RelativeOrAbsolute));
                this.NomUser.Content = User.Nom;
            }
        }


        // si appuie su refresh
        private void refresh_MouseDown(object sender, MouseButtonEventArgs e) {
            // a voir avec les autres si je rajoute ou pas
            // ListeContactVue.Children.Clear();
            ajouterContactTrier(User.ListeContact, User.ListePersonnes); 
        }


    }
}
