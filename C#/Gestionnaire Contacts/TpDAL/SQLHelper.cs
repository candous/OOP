using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpEntities;


namespace TpDAL
{
    public static class SQLHelper
    {
        private static string BDString = @"Data Source= MOOGLOOCH-3; Initial catalog = contactsDB; Integrated security=true";

        public static int GetNbUsers()
        {
            string requete = "select count(*) from personnes where isUser=1";
            int retour;
            using (SqlConnection conn = new SqlConnection(BDString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = requete;
                    retour = (int)cmd.ExecuteScalar();
                }
            }
            return retour;
        }

        public static Personne GetUserLogin(string courriel, string password)
        {
            string requete = "select * from personnes where courriel=@Courriel and password=@Password and isUser=1";


            Personne user = null;
            SqlParameter paramCourriel = new SqlParameter("@Courriel", courriel);
            SqlParameter paramPwd = new SqlParameter("@Password", password);

            using (SqlConnection conn = new SqlConnection(BDString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(paramCourriel);
                    cmd.Parameters.Add(paramPwd);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            user = new Personne();
                            if (!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                user.Id = rdr.GetInt32(rdr.GetOrdinal("id"));

                            //user.Password = rdr.GetString(1);
                            if (rdr["password"] == DBNull.Value)
                                user.Password = null;
                            else
                                user.Password = rdr["password"].ToString();

                            if (rdr["nom"] == DBNull.Value)
                                user.Nom = null;
                            else
                                user.Nom = rdr["nom"].ToString();

                            if (rdr["compagnie"] == DBNull.Value)
                                user.Compagnie = null;
                            else
                                user.Compagnie = rdr["compagnie"].ToString();

                            if (rdr["celulaire"] == DBNull.Value)
                                user.Celulaire = null;
                            else
                                user.Celulaire = rdr["celulaire"].ToString();

                            if (rdr["telephone"] == DBNull.Value)
                                user.Telephone = null;
                            else
                                user.Telephone = rdr["telephone"].ToString();

                            if (rdr["siteWeb"] == DBNull.Value)
                                user.SiteWeb = null;
                            else
                                user.SiteWeb = rdr["siteWeb"].ToString();

                            if (rdr["courriel"] == DBNull.Value)
                                user.Courriel = null;
                            else
                                user.Courriel = rdr["courriel"].ToString();

                            if (rdr["aniversaire"] == DBNull.Value)
                                user.Aniversaire = null;
                            else
                                user.Aniversaire = Convert.ToDateTime(rdr["aniversaire"]);

                            if (rdr["lastVisit"] == DBNull.Value)
                                user.LastVisit = null;
                            else
                                user.LastVisit = Convert.ToDateTime(rdr["lastVisit"]);

                            if (rdr["adresse"] == DBNull.Value)
                                user.Adresse = null;
                            else
                                user.Adresse = rdr["adresse"].ToString();

                            if (rdr["ville"] == DBNull.Value)
                                user.Ville = null;
                            else
                                user.Ville = rdr["ville"].ToString();

                            if (rdr["province"] == DBNull.Value)
                                user.Province = null;
                            else
                                user.Province = rdr["province"].ToString();

                            if (rdr["pays"] == DBNull.Value)
                                user.Pays = null;
                            else
                                user.Pays = rdr["pays"].ToString();

                            if (rdr["urlPhoto"] == DBNull.Value)
                                user.UrlPhoto = null;
                            else
                                user.UrlPhoto = rdr["urlPhoto"].ToString();

                            user.IsUser = (bool)rdr["isUser"];
                            user.IsVisible = (bool)rdr["isVisible"];

                            //initialiser la liste de contacts
                            user.ListeContact = GetListeContactsByUserId(user.Id);
                            //peopler la liste de personnes contacts
                            user.ListePersonnes = GetListePersonnesByListeContacts(user.ListeContact);
                        }
                    }
                }

            }
            return user;
        }

        //recuperation de la liste de contacts du user
        public static List<Contact> GetListeContactsByUserId(int id)
        {
            string requeteListeContacts = "select*from contacts where idUser=@Id";
            SqlParameter paramId;
            List<Contact> listeContact = null;
            using (SqlConnection conn = new SqlConnection(BDString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requeteListeContacts, conn))
                {
                    //set parametre pour la nouvelle requete
                    paramId = new SqlParameter("@Id", id);

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(paramId);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        listeContact = new List<Contact>();
                        Contact contact;
                        while (rdr.Read())
                        {
                            contact = new Contact();
                            if (!rdr.IsDBNull(rdr.GetOrdinal("idUser")))
                                contact.IdUser = rdr.GetInt32(rdr.GetOrdinal("idUser"));
                            if (!rdr.IsDBNull(rdr.GetOrdinal("idContact")))
                                contact.IdContact = rdr.GetInt32(rdr.GetOrdinal("idContact"));
                            contact.IsFavorite = (bool)rdr["isFavorite"];

                            listeContact.Add(contact);
                        }
                    }
                }
            }
            return listeContact;
        }

        public static List<Personne> GetListePersonnesByListeContacts(List<Contact> listeContacts)
        {
            string requeteListePersonnes = "select * from personnes where id=@Id";
            SqlParameter paramId2;
            List<Personne> listePersonne = new List<Personne>();
            using (SqlConnection conn = new SqlConnection(BDString))
            {
                conn.Open();
                //recuperation de la liste de contacts du user
                using (SqlCommand cmd = new SqlCommand(requeteListePersonnes, conn))
                {
                    foreach (Contact c in listeContacts)
                    {
                        //set parametre pour la nouvelle requete
                        paramId2 = new SqlParameter("@Id", c.IdContact);

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(paramId2);
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            Personne personne;
                            while (rdr.Read())
                            {
                                personne = new Personne();
                                if (!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                    personne.Id = rdr.GetInt32(rdr.GetOrdinal("id"));

                                if (rdr["password"] == DBNull.Value)
                                    personne.Password = null;
                                else
                                    personne.Password = rdr["password"].ToString();

                                if (rdr["nom"] == DBNull.Value)
                                    personne.Nom = null;
                                else
                                    personne.Nom = rdr["nom"].ToString();

                                if (rdr["compagnie"] == DBNull.Value)
                                    personne.Compagnie = null;
                                else
                                    personne.Compagnie = rdr["compagnie"].ToString();

                                if (rdr["celulaire"] == DBNull.Value)
                                    personne.Celulaire = null;
                                else
                                    personne.Celulaire = rdr["celulaire"].ToString();

                                if (rdr["telephone"] == DBNull.Value)
                                    personne.Telephone = null;
                                else
                                    personne.Telephone = rdr["telephone"].ToString();

                                if (rdr["siteWeb"] == DBNull.Value)
                                    personne.SiteWeb = null;
                                else
                                    personne.SiteWeb = rdr["siteWeb"].ToString();

                                if (rdr["courriel"] == DBNull.Value)
                                    personne.Courriel = null;
                                else
                                    personne.Courriel = rdr["courriel"].ToString();

                                if (rdr["aniversaire"] == DBNull.Value)
                                    personne.Aniversaire = null;
                                else
                                    personne.Aniversaire = Convert.ToDateTime(rdr["aniversaire"]);

                                if (rdr["lastVisit"] == DBNull.Value)
                                    personne.LastVisit = null;
                                else
                                    personne.LastVisit = Convert.ToDateTime(rdr["lastVisit"]);

                                if (rdr["adresse"] == DBNull.Value)
                                    personne.Adresse = null;
                                else
                                    personne.Adresse = rdr["adresse"].ToString();

                                if (rdr["ville"] == DBNull.Value)
                                    personne.Ville = null;
                                else
                                    personne.Ville = rdr["ville"].ToString();

                                if (rdr["province"] == DBNull.Value)
                                    personne.Province = null;
                                else
                                    personne.Province = rdr["province"].ToString();

                                if (rdr["pays"] == DBNull.Value)
                                    personne.Pays = null;
                                else
                                    personne.Pays = rdr["pays"].ToString();

                                if (rdr["urlPhoto"] == DBNull.Value)
                                    personne.UrlPhoto = null;
                                else
                                    personne.UrlPhoto = rdr["urlPhoto"].ToString();


                               // personne.IsFavorite = (bool)rdr["isFavorite"];
                                personne.IsUser = (bool)rdr["isUser"];
                                personne.IsVisible = (bool)rdr["isVisible"];

                                listePersonne.Add(personne);
                            }
                        }
                        cmd.Parameters.Clear();
                    }
                }
            }
            return listePersonne;
        }

        public static int UpdateLastVisit(DateTime date, int id)
        {
            string requete = "update personnes set lastVisit=@date where id=@id";
            int affectes;
            SqlParameter paramId = new SqlParameter("@id", id);
            SqlParameter paramDate = new SqlParameter("@date", date);

            using (SqlConnection conn = new SqlConnection(BDString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(paramId);
                    cmd.Parameters.Add(paramDate);
                    affectes = (int)cmd.ExecuteNonQuery();
                }
            }
            return affectes;
        }
        //savoir s'il exist un user ou pas avec le courriel
        public static int? UserExistsByEmail(string courriel)
        {
            string requete = "select id from personnes where courriel=@courriel and isUser=1";
            int? id;
            SqlParameter paramCourriel = new SqlParameter("@courriel", courriel);

            using (SqlConnection conn = new SqlConnection(BDString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(paramCourriel);
                    if (cmd.ExecuteScalar() == DBNull.Value)
                        id = null;
                    else
                        id = (int?)cmd.ExecuteScalar();
                }
            }
            return id;
        }

        public static bool InsertUser(Personne personneInscrite)
        {
            bool cree = false;
            string requete = "insert into personnes (password, nom, compagnie, celulaire, telephone, siteWeb, courriel, aniversaire, lastVisit, adresse, ville, province, pays, urlPhoto, isUser, isVisible) OUTPUT INSERTED.id  values(@password, @nom, @compagnie, @celulaire, @telephone, @siteWeb, @courriel, @aniversaire, @lastVisit, @adresse, @ville, @province, @pays, @urlPhoto, @isUser, @isVisible)";

            SqlParameter password = new SqlParameter("@password", !string.IsNullOrEmpty(personneInscrite.Password) ? personneInscrite.Password : (object)DBNull.Value);
            SqlParameter nom = new SqlParameter("@nom", !string.IsNullOrEmpty(personneInscrite.Nom) ? personneInscrite.Nom : (object)DBNull.Value);
            SqlParameter compagnie = new SqlParameter("@compagnie", !string.IsNullOrEmpty(personneInscrite.Compagnie) ? personneInscrite.Compagnie : (object)DBNull.Value);
            SqlParameter celulaire = new SqlParameter("@celulaire", !string.IsNullOrEmpty(personneInscrite.Celulaire) ? personneInscrite.Celulaire : (object)DBNull.Value);
            SqlParameter telephone = new SqlParameter("@telephone", !string.IsNullOrEmpty(personneInscrite.Telephone) ? personneInscrite.Telephone : (object)DBNull.Value);
            SqlParameter siteWeb = new SqlParameter("@siteWeb", !string.IsNullOrEmpty(personneInscrite.SiteWeb) ? personneInscrite.SiteWeb : (object)DBNull.Value);
            SqlParameter courriel = new SqlParameter("@courriel", !string.IsNullOrEmpty(personneInscrite.Courriel) ? personneInscrite.Courriel : (object)DBNull.Value);

            SqlParameter aniversaire;
            if (personneInscrite.Aniversaire == null)
                aniversaire = new SqlParameter("@aniversaire", DBNull.Value);
            else
                aniversaire = new SqlParameter("@aniversaire", personneInscrite.Aniversaire);

            SqlParameter lastVisit;
            if (personneInscrite.LastVisit == null)
                lastVisit = new SqlParameter("@lastVisit", DBNull.Value);
            else
                lastVisit = new SqlParameter("@lastVisit", personneInscrite.LastVisit);

            SqlParameter adresse = new SqlParameter("@adresse", !string.IsNullOrEmpty(personneInscrite.Adresse) ? personneInscrite.Adresse : (object)DBNull.Value);
            SqlParameter ville = new SqlParameter("@ville", !string.IsNullOrEmpty(personneInscrite.Ville) ? personneInscrite.Ville : (object)DBNull.Value);
            SqlParameter province = new SqlParameter("@province", !string.IsNullOrEmpty(personneInscrite.Province) ? personneInscrite.Province : (object)DBNull.Value);
            SqlParameter pays = new SqlParameter("@pays", !string.IsNullOrEmpty(personneInscrite.Pays) ? personneInscrite.Pays : (object)DBNull.Value);
            SqlParameter urlPhoto = new SqlParameter("@urlPhoto", !string.IsNullOrEmpty(personneInscrite.UrlPhoto) ? personneInscrite.UrlPhoto : (object)DBNull.Value);
            SqlParameter isUser = new SqlParameter("@isUser", personneInscrite.IsUser);
            SqlParameter isVisible = new SqlParameter("@isVisible", personneInscrite.IsVisible);

            using (SqlConnection conn = new SqlConnection(BDString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(password);
                    cmd.Parameters.Add(nom);
                    cmd.Parameters.Add(compagnie);
                    cmd.Parameters.Add(celulaire);
                    cmd.Parameters.Add(telephone);
                    cmd.Parameters.Add(siteWeb);
                    cmd.Parameters.Add(courriel);
                    cmd.Parameters.Add(aniversaire);
                    cmd.Parameters.Add(lastVisit);
                    cmd.Parameters.Add(adresse);
                    cmd.Parameters.Add(ville);
                    cmd.Parameters.Add(province);
                    cmd.Parameters.Add(pays);
                    cmd.Parameters.Add(urlPhoto);
                    cmd.Parameters.Add(isUser);
                    cmd.Parameters.Add(isVisible);

                    personneInscrite.Id = (Int32)cmd.ExecuteScalar();
                    if (personneInscrite.Id != 0 && personneInscrite.Id != null)
                        cree = true;
                }
            }
            return cree;
        }

        public static bool InsertContact(Contact contactAjoute)
        {
            bool cree = false;
            string requete = "insert into contacts (idUser, idContact, isFavorite) values(@idUser, @idContact, @isFavorite)";

            SqlParameter idUser = new SqlParameter("@idUser", contactAjoute.IdUser);
            SqlParameter idContact = new SqlParameter("@idContact",contactAjoute.IdContact);
            SqlParameter isFavorite = new SqlParameter("@isFavorite",contactAjoute.IsFavorite);
           
            using (SqlConnection conn = new SqlConnection(BDString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idUser);
                    cmd.Parameters.Add(idContact);
                    cmd.Parameters.Add(isFavorite);
               
                    int modifs=(int)cmd.ExecuteNonQuery();
                    if (modifs != 0)
                        cree = true;
                }
            }
            return cree;
        }

        public static bool ModifierContact(Contact contactTableModif)
        {
            bool modifie = false;
            string requete = "update contacts SET idUser=@idUser, idContact=@idContact, isFavorite=@isFavorite WHERE idUser=@idUser and idContact=@idContact";

            SqlParameter idUser = new SqlParameter("@idUser", contactTableModif.IdUser);
            SqlParameter idContact = new SqlParameter("@idContact", contactTableModif.IdContact);
            SqlParameter isFavorite = new SqlParameter("@isFavorite", contactTableModif.IsFavorite);

            using (SqlConnection conn = new SqlConnection(BDString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idUser);
                    cmd.Parameters.Add(idContact);
                    cmd.Parameters.Add(isFavorite);
                   

                    int affectes = (int)cmd.ExecuteNonQuery();
                    if (affectes != 0)
                        modifie = true;
                }
            }
            return modifie;
        }

        public static bool ModifierUser(Personne ContactModifier)
        {
            bool modifie = false;
            string requete = "update personnes SET password=@password, nom=@nom, compagnie=@compagnie, celulaire=@celulaire, telephone=@telephone, siteWeb=@siteWeb, courriel=@courriel, aniversaire=@aniversaire, lastVisit=@lastVisit, adresse=@adresse, ville=@ville, province=@province, pays=@pays, urlPhoto=@urlPhoto, isUser=@isUser, isVisible=@isVisible WHERE id=@id";

            SqlParameter id = new SqlParameter("@id", ContactModifier.Id);
            SqlParameter password = new SqlParameter("@password", !string.IsNullOrEmpty(ContactModifier.Password) ? ContactModifier.Password : (object)DBNull.Value);
            SqlParameter nom = new SqlParameter("@nom", !string.IsNullOrEmpty(ContactModifier.Nom) ? ContactModifier.Nom : (object)DBNull.Value);
            SqlParameter compagnie = new SqlParameter("@compagnie", !string.IsNullOrEmpty(ContactModifier.Compagnie) ? ContactModifier.Compagnie : (object)DBNull.Value);
            SqlParameter celulaire = new SqlParameter("@celulaire", !string.IsNullOrEmpty(ContactModifier.Celulaire) ? ContactModifier.Celulaire : (object)DBNull.Value);
            SqlParameter telephone = new SqlParameter("@telephone", !string.IsNullOrEmpty(ContactModifier.Telephone) ? ContactModifier.Telephone : (object)DBNull.Value);
            SqlParameter siteWeb = new SqlParameter("@siteWeb", !string.IsNullOrEmpty(ContactModifier.SiteWeb) ? ContactModifier.SiteWeb : (object)DBNull.Value);
            SqlParameter courriel = new SqlParameter("@courriel", !string.IsNullOrEmpty(ContactModifier.Courriel) ? ContactModifier.Courriel : (object)DBNull.Value);

            SqlParameter aniversaire;
            if (ContactModifier.Aniversaire == null)
                aniversaire = new SqlParameter("@aniversaire", DBNull.Value);
            else
                aniversaire = new SqlParameter("@aniversaire", ContactModifier.Aniversaire);

            SqlParameter lastVisit;
            if (ContactModifier.LastVisit == null)
                lastVisit = new SqlParameter("@lastVisit", DBNull.Value);
            else
                lastVisit = new SqlParameter("@lastVisit", ContactModifier.LastVisit);

            SqlParameter adresse = new SqlParameter("@adresse", !string.IsNullOrEmpty(ContactModifier.Adresse) ? ContactModifier.Adresse : (object)DBNull.Value);
            SqlParameter ville = new SqlParameter("@ville", !string.IsNullOrEmpty(ContactModifier.Ville) ? ContactModifier.Ville : (object)DBNull.Value);
            SqlParameter province = new SqlParameter("@province", !string.IsNullOrEmpty(ContactModifier.Province) ? ContactModifier.Province : (object)DBNull.Value);
            SqlParameter pays = new SqlParameter("@pays", !string.IsNullOrEmpty(ContactModifier.Pays) ? ContactModifier.Pays : (object)DBNull.Value);
            SqlParameter urlPhoto = new SqlParameter("@urlPhoto", !string.IsNullOrEmpty(ContactModifier.UrlPhoto) ? ContactModifier.UrlPhoto : (object)DBNull.Value);
            SqlParameter isUser = new SqlParameter("@isUser", ContactModifier.IsUser);
            SqlParameter isVisible = new SqlParameter("@isVisible", ContactModifier.IsVisible);

            using (SqlConnection conn = new SqlConnection(BDString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(id);
                    cmd.Parameters.Add(password);
                    cmd.Parameters.Add(nom);
                    cmd.Parameters.Add(compagnie);
                    cmd.Parameters.Add(celulaire);
                    cmd.Parameters.Add(telephone);
                    cmd.Parameters.Add(siteWeb);
                    cmd.Parameters.Add(courriel);
                    cmd.Parameters.Add(aniversaire);
                    cmd.Parameters.Add(lastVisit);
                    cmd.Parameters.Add(adresse);
                    cmd.Parameters.Add(ville);
                    cmd.Parameters.Add(province);
                    cmd.Parameters.Add(pays);
                    cmd.Parameters.Add(urlPhoto);
                    cmd.Parameters.Add(isUser);
                    cmd.Parameters.Add(isVisible);

                    int affectes = (int)cmd.ExecuteNonQuery();
                    if (affectes != 0)
                        modifie = true;
                }
            }
            return modifie;
        }

        public static bool SupprimerContact(Contact contactSupprimer)
        {
            bool modifie = false;
            string requete = "delete from contacts WHERE idUser=@idUser and idContact=@idContact";

            SqlParameter idUser = new SqlParameter("@idUser", contactSupprimer.IdUser);
            SqlParameter idContact = new SqlParameter("@idContact", contactSupprimer.IdContact);

            using (SqlConnection conn = new SqlConnection(BDString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idUser);
                    cmd.Parameters.Add(idContact);

                    int affectes = (int)cmd.ExecuteNonQuery();
                    if (affectes != 0)
                        modifie = true;
                }
            }
            return modifie;
        }

        public static bool SupprimerUser(Personne ContactSupprimer)
        {
            bool modifie = false;
            string requete = "delete from personnes WHERE id=@id and isUser=0";

            SqlParameter id = new SqlParameter("@id", ContactSupprimer.Id);
          
            using (SqlConnection conn = new SqlConnection(BDString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(id);
                    
                    int affectes = (int)cmd.ExecuteNonQuery();
                    if (affectes != 0)
                        modifie = true;
                }
            }
            return modifie;
        }

        public static List<Personne> Rechercher(string colonne, string parametre)
        {

            string requeteListePersonnes = "select * from personnes where "+colonne+" LIKE '%@parametre%'";
            
            List<Personne> listePersonne = new List<Personne>();
            using (SqlConnection conn = new SqlConnection(BDString))
            {
                conn.Open();
                //recuperation de la liste de contacts du user
                using (SqlCommand cmd = new SqlCommand(requeteListePersonnes, conn))
                {
 
                    SqlParameter parValeur = new SqlParameter("@parametre", parametre);
                        
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.Add(parColonne);
                        cmd.Parameters.Add(parValeur);
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            Personne personne;
                            while (rdr.Read())
                            {
                                personne = new Personne();
                                if (!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                    personne.Id = rdr.GetInt32(rdr.GetOrdinal("id"));

                                if (rdr["password"] == DBNull.Value)
                                    personne.Password = null;
                                else
                                    personne.Password = rdr["password"].ToString();

                                if (rdr["nom"] == DBNull.Value)
                                    personne.Nom = null;
                                else
                                    personne.Nom = rdr["nom"].ToString();

                                if (rdr["compagnie"] == DBNull.Value)
                                    personne.Compagnie = null;
                                else
                                    personne.Compagnie = rdr["compagnie"].ToString();

                                if (rdr["celulaire"] == DBNull.Value)
                                    personne.Celulaire = null;
                                else
                                    personne.Celulaire = rdr["celulaire"].ToString();

                                if (rdr["telephone"] == DBNull.Value)
                                    personne.Telephone = null;
                                else
                                    personne.Telephone = rdr["telephone"].ToString();

                                if (rdr["siteWeb"] == DBNull.Value)
                                    personne.SiteWeb = null;
                                else
                                    personne.SiteWeb = rdr["siteWeb"].ToString();

                                if (rdr["courriel"] == DBNull.Value)
                                    personne.Courriel = null;
                                else
                                    personne.Courriel = rdr["courriel"].ToString();

                                if (rdr["aniversaire"] == DBNull.Value)
                                    personne.Aniversaire = null;
                                else
                                    personne.Aniversaire = Convert.ToDateTime(rdr["aniversaire"]);

                                if (rdr["lastVisit"] == DBNull.Value)
                                    personne.LastVisit = null;
                                else
                                    personne.LastVisit = Convert.ToDateTime(rdr["lastVisit"]);

                                if (rdr["adresse"] == DBNull.Value)
                                    personne.Adresse = null;
                                else
                                    personne.Adresse = rdr["adresse"].ToString();

                                if (rdr["ville"] == DBNull.Value)
                                    personne.Ville = null;
                                else
                                    personne.Ville = rdr["ville"].ToString();

                                if (rdr["province"] == DBNull.Value)
                                    personne.Province = null;
                                else
                                    personne.Province = rdr["province"].ToString();

                                if (rdr["pays"] == DBNull.Value)
                                    personne.Pays = null;
                                else
                                    personne.Pays = rdr["pays"].ToString();

                                if (rdr["urlPhoto"] == DBNull.Value)
                                    personne.UrlPhoto = null;
                                else
                                    personne.UrlPhoto = rdr["urlPhoto"].ToString();

                                personne.IsUser = (bool)rdr["isUser"];
                                personne.IsVisible = (bool)rdr["isVisible"];

                                listePersonne.Add(personne);
                            }
                    }
                }
            }
            return listePersonne;
        }
    }
}
       