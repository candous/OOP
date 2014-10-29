using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL {
    public static class RequeteEntreprise {
        public static string ConnectionString = DefinitionConnection.ConnectionStringCommun;

        //Recuperer Entreprise
        public static Entreprise recupererEntreprise(int idEntreprise) {
            Entreprise entreprise = null;
            SqlParameter idEntrepriseParam = new SqlParameter("@idEntreprise", idEntreprise);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.Parameters.Add(idEntrepriseParam);
                    cmd.CommandText = @"SELECT * FROM entreprise WHERE id=@idEntreprise AND actif=1";

                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        while(rdr.Read()) {
                            entreprise = new Entreprise();
                            entreprise.Modification = new Modification();

                            //remplir l'objet entreprise
                            entreprise.Id = (int)rdr["id"];

                            if(!(rdr["nom"] == DBNull.Value))
                                entreprise.Nom = rdr["nom"].ToString();

                            if(!(rdr["ville"] == DBNull.Value))
                                entreprise.Ville = rdr["ville"].ToString();

                            if(!(rdr["adresse"] == DBNull.Value))
                                entreprise.Adresse = rdr["adresse"].ToString();

                            if(!(rdr["courriel"] == DBNull.Value))
                                entreprise.Email = rdr["courriel"].ToString();

                            if(!(rdr["tel1"] == DBNull.Value))
                                entreprise.Telephone1 = rdr["tel1"].ToString();
                            if(!(rdr["tel2"] == DBNull.Value))
                                entreprise.Telephone2 = rdr["tel2"].ToString();
                            if(!(rdr["tel3"] == DBNull.Value))
                                entreprise.Telephone3 = rdr["tel3"].ToString();

                            if(!(rdr["secteur"] == DBNull.Value))
                                entreprise.Secteur = rdr["secteur"].ToString();

                            entreprise.DateSaisie = Convert.ToDateTime(rdr["dateSaisie"]);

                            if(!(rdr["commentaire"] == DBNull.Value))
                                entreprise.Commentaire = rdr["commentaire"].ToString();

                            if(!(rdr["imageLogo"] == DBNull.Value))
                                entreprise.ImageLogo = rdr["imageLogo"].ToString();

                            if(!(rdr["idLangue"] == DBNull.Value))
                                entreprise.Langue = (int)rdr["idLangue"];
                            else
                                entreprise.Langue = null;

                            entreprise.Actif = (bool)rdr["actif"];
                            entreprise.Modification.UtilisateurId = (int)rdr["idUtilisateur"];
                            entreprise.Modification.DateModification = Convert.ToDateTime(rdr["dateModification"]);
                        }
                    }
                }
            }
            return entreprise;
        }


        //Ajouter Entreprise
        public static bool ajouterEntreprise(Entreprise entreprise) {
            bool cree = false;
            string requete = @"INSERT INTO entreprise (nom, ville, adresse, courriel, tel1, tel2, tel3, secteur, dateSaisie, commentaire, imageLogo,idLangue, idUtilisateur) OUTPUT INSERTED.id VALUES (@nom, @ville, @adresse, @courriel, @tel1, @tel2, @tel3, @secteur, @dateSaisie, @commentaire, @imageLogo,@idLangue, @idUtilisateur)";

            SqlParameter nom = new SqlParameter("@nom", !string.IsNullOrEmpty(entreprise.Nom) ? entreprise.Nom : (object)DBNull.Value);
            SqlParameter ville = new SqlParameter("@ville", !string.IsNullOrEmpty(entreprise.Ville) ? entreprise.Ville : (object)DBNull.Value);
            SqlParameter adresse = new SqlParameter("@adresse", !string.IsNullOrEmpty(entreprise.Adresse) ? entreprise.Adresse : (object)DBNull.Value);
            SqlParameter courriel = new SqlParameter("@courriel", !string.IsNullOrEmpty(entreprise.Email) ? entreprise.Email : (object)DBNull.Value);
            SqlParameter tel1 = new SqlParameter("@tel1", !string.IsNullOrEmpty(entreprise.Telephone1) ? entreprise.Telephone1 : (object)DBNull.Value);
            SqlParameter tel2 = new SqlParameter("@tel2", !string.IsNullOrEmpty(entreprise.Telephone2) ? entreprise.Telephone2 : (object)DBNull.Value);
            SqlParameter tel3 = new SqlParameter("@tel3", !string.IsNullOrEmpty(entreprise.Telephone3) ? entreprise.Telephone3 : (object)DBNull.Value);
            SqlParameter secteur = new SqlParameter("@secteur", !string.IsNullOrEmpty(entreprise.Secteur) ? entreprise.Secteur : (object)DBNull.Value);
            SqlParameter dateSaisie = new SqlParameter("@dateSaisie", entreprise.DateSaisie);
            SqlParameter commentaire = new SqlParameter("@commentaire", !string.IsNullOrEmpty(entreprise.Commentaire) ? entreprise.Commentaire : (object)DBNull.Value);
            SqlParameter imageLogo = new SqlParameter("@imageLogo", !string.IsNullOrEmpty(entreprise.ImageLogo) ? entreprise.ImageLogo : (object)DBNull.Value);
            SqlParameter idLangue = new SqlParameter("@idLangue", entreprise.Langue != null ? entreprise.Langue : (object)DBNull.Value);
            SqlParameter idUtilisateur = new SqlParameter("@idUtilisateur", entreprise.Modification.UtilisateurId);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(nom);
                    cmd.Parameters.Add(ville);
                    cmd.Parameters.Add(adresse);
                    cmd.Parameters.Add(courriel);
                    cmd.Parameters.Add(tel1);
                    cmd.Parameters.Add(tel2);
                    cmd.Parameters.Add(tel3);
                    cmd.Parameters.Add(secteur);
                    cmd.Parameters.Add(dateSaisie);
                    cmd.Parameters.Add(commentaire);
                    cmd.Parameters.Add(imageLogo);
                    cmd.Parameters.Add(idLangue);
                    cmd.Parameters.Add(idUtilisateur);

                    entreprise.Id = (Int32)cmd.ExecuteScalar();
                    if(entreprise.Id > 0)
                        cree = true;
                }
            }
            return cree;
        }

        //Modifier entreprise
        public static bool modifierEntreprise(Entreprise entreprise) {
            bool modifie = false;
            string requete = @"UPDATE entreprise SET nom=@nom, ville=@ville, adresse=@adresse, courriel=@courriel, tel1=@tel1, tel2=@tel2, tel3=@tel3, secteur=@secteur, dateSaisie=@dateSaisie, commentaire=@commentaire, imageLogo=@imageLogo,idLangue=@idLangue, idUtilisateur=@idUtilisateur where id=@id";

            SqlParameter id = new SqlParameter("@id", entreprise.Id);
            SqlParameter nom = new SqlParameter("@nom", !string.IsNullOrEmpty(entreprise.Nom) ? entreprise.Nom : (object)DBNull.Value);
            SqlParameter ville = new SqlParameter("@ville", !string.IsNullOrEmpty(entreprise.Ville) ? entreprise.Ville : (object)DBNull.Value);
            SqlParameter adresse = new SqlParameter("@adresse", !string.IsNullOrEmpty(entreprise.Adresse) ? entreprise.Adresse : (object)DBNull.Value);
            SqlParameter courriel = new SqlParameter("@courriel", !string.IsNullOrEmpty(entreprise.Email) ? entreprise.Email : (object)DBNull.Value);
            SqlParameter tel1 = new SqlParameter("@tel1", !string.IsNullOrEmpty(entreprise.Telephone1) ? entreprise.Telephone1 : (object)DBNull.Value);
            SqlParameter tel2 = new SqlParameter("@tel2", !string.IsNullOrEmpty(entreprise.Telephone2) ? entreprise.Telephone2 : (object)DBNull.Value);
            SqlParameter tel3 = new SqlParameter("@tel3", !string.IsNullOrEmpty(entreprise.Telephone3) ? entreprise.Telephone3 : (object)DBNull.Value);
            SqlParameter secteur = new SqlParameter("@secteur", !string.IsNullOrEmpty(entreprise.Secteur) ? entreprise.Secteur : (object)DBNull.Value);
            SqlParameter dateSaisie = new SqlParameter("@dateSaisie", entreprise.DateSaisie);
            SqlParameter commentaire = new SqlParameter("@commentaire", !string.IsNullOrEmpty(entreprise.Commentaire) ? entreprise.Commentaire : (object)DBNull.Value);
            SqlParameter imageLogo = new SqlParameter("@imageLogo", !string.IsNullOrEmpty(entreprise.ImageLogo) ? entreprise.ImageLogo : (object)DBNull.Value);
            SqlParameter idLangue = new SqlParameter("@idLangue", entreprise.Langue != null ? entreprise.Langue : (object)DBNull.Value);
            SqlParameter idUtilisateur = new SqlParameter("@idUtilisateur", entreprise.Modification.UtilisateurId);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(id);
                    cmd.Parameters.Add(nom);
                    cmd.Parameters.Add(ville);
                    cmd.Parameters.Add(adresse);
                    cmd.Parameters.Add(courriel);
                    cmd.Parameters.Add(tel1);
                    cmd.Parameters.Add(tel2);
                    cmd.Parameters.Add(tel3);
                    cmd.Parameters.Add(secteur);
                    cmd.Parameters.Add(dateSaisie);
                    cmd.Parameters.Add(commentaire);
                    cmd.Parameters.Add(imageLogo);
                    cmd.Parameters.Add(idLangue);
                    cmd.Parameters.Add(idUtilisateur);

                    int affectes = (int)cmd.ExecuteNonQuery();
                    if(affectes != 0)
                        modifie = true;
                }
            }
            return modifie;
        }

        //Supprimer Entreprise
        public static void supprimerEntreprise(int idEntreprise) {
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"UPDATE entreprise SET actif=0 WHERE id=@id";
                    cmd.Parameters.Add(new SqlParameter("id", idEntreprise));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Recuperer liste etudiant d'un entreprise
        public static List<Etudiant> recupererEtudiantPotentielEntreprise(int idEntreprise) {
            List<Etudiant> etudiants = new List<Etudiant>();
            // inserer le nouveau contact
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {


                    cmd.CommandText = @"";
                }
            }
            return etudiants;
        }

        //Recuperer liste representant d'un entreprise
        public static List<Representant> recupererRepresentantsEntreprise(int idEntreprise) {
            List<Representant> listeRepresentants = null;
            SqlParameter idEntrepriseParam = new SqlParameter("@idEntreprise", idEntreprise);
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.Parameters.Add(idEntrepriseParam);
                    cmd.CommandText = @"select * from representant where idEntreprise=@idEntreprise and actif=1";

                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        if(rdr.HasRows) {
                            listeRepresentants = new List<Representant>();
                            while(rdr.Read()) {
                                Representant representant = new Representant();

                                representant.Id = (int)rdr["id"];

                                if(!(rdr["prenom"] == DBNull.Value))
                                    representant.Prenom = rdr["prenom"].ToString();

                                if(!(rdr["nom"] == DBNull.Value))
                                    representant.Nom = rdr["nom"].ToString();

                                if(!(rdr["courriel"] == DBNull.Value))
                                    representant.Courriel = rdr["courriel"].ToString();

                                if(!(rdr["tel1"] == DBNull.Value))
                                    representant.Telephone1 = rdr["tel1"].ToString();

                                if(!(rdr["tel2"] == DBNull.Value))
                                    representant.Telephone2 = rdr["tel2"].ToString();

                                if(!(rdr["tel3"] == DBNull.Value))
                                    representant.Telephone3 = rdr["tel3"].ToString();

                                if(!(rdr["departement"] == DBNull.Value))
                                    representant.Departement = rdr["departement"].ToString();

                                if(!(rdr["poste"] == DBNull.Value))
                                    representant.Poste = rdr["poste"].ToString();

                                representant.IdEntreprise = (int)rdr["idEntreprise"];

                                if(!(rdr["idLangue"] == DBNull.Value))
                                    representant.IdLangue = (int)rdr["idLangue"];
                                else
                                    representant.IdLangue = null;

                                representant.Actif = (bool)rdr["actif"];

                                representant.Modification = new Modification();
                                representant.Modification.UtilisateurId = (int)rdr["idUtilisateur"];
                                representant.Modification.DateModification = Convert.ToDateTime(rdr["dateModification"]);

                                listeRepresentants.Add(representant);
                            }
                        }
                    }
                }
            }
            return listeRepresentants;
        }

        //Ajouter representant
        public static bool ajouterRepresentant(Representant representant) {

            bool cree = false;
            string requete = @"INSERT INTO representant (prenom, nom, courriel, tel1, tel2, tel3, departement, poste, idEntreprise, idLangue, idUtilisateur) OUTPUT INSERTED.id VALUES (@prenom, @nom, @courriel, @tel1, @tel2, @tel3, @departement, @poste, @idEntreprise, @idLangue, @idUtilisateur)";

            SqlParameter prenom = new SqlParameter("@prenom", !string.IsNullOrEmpty(representant.Prenom) ? representant.Prenom : (object)DBNull.Value);
            SqlParameter nom = new SqlParameter("@nom", !string.IsNullOrEmpty(representant.Nom) ? representant.Nom : (object)DBNull.Value);
            SqlParameter courriel = new SqlParameter("@courriel", !string.IsNullOrEmpty(representant.Courriel) ? representant.Courriel : (object)DBNull.Value);
            SqlParameter tel1 = new SqlParameter("@tel1", !string.IsNullOrEmpty(representant.Telephone1) ? representant.Telephone1 : (object)DBNull.Value);
            SqlParameter tel2 = new SqlParameter("@tel2", !string.IsNullOrEmpty(representant.Telephone2) ? representant.Telephone2 : (object)DBNull.Value);
            SqlParameter tel3 = new SqlParameter("@tel3", !string.IsNullOrEmpty(representant.Telephone3) ? representant.Telephone3 : (object)DBNull.Value);
            SqlParameter departement = new SqlParameter("@departement", !string.IsNullOrEmpty(representant.Departement) ? representant.Departement : (object)DBNull.Value);
            SqlParameter poste = new SqlParameter("@poste", !string.IsNullOrEmpty(representant.Poste) ? representant.Poste : (object)DBNull.Value);
            SqlParameter idEntreprise = new SqlParameter("@idEntreprise", representant.IdEntreprise);
            SqlParameter idLangue = new SqlParameter("@idLangue", representant.IdLangue != null ? representant.IdLangue : (object)DBNull.Value);
            SqlParameter idUtilisateur = new SqlParameter("@idUtilisateur", representant.Modification.UtilisateurId);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(prenom);
                    cmd.Parameters.Add(nom);
                    cmd.Parameters.Add(courriel);
                    cmd.Parameters.Add(tel1);
                    cmd.Parameters.Add(tel2);
                    cmd.Parameters.Add(tel3);
                    cmd.Parameters.Add(departement);
                    cmd.Parameters.Add(poste);
                    cmd.Parameters.Add(idEntreprise);
                    cmd.Parameters.Add(idLangue);
                    cmd.Parameters.Add(idUtilisateur);

                    representant.Id = (Int32)cmd.ExecuteScalar();
                    if(representant.Id > 0)
                        cree = true;
                }
            }
            return cree;

        }
        public static Representant recupererRepresentantParId(int idRepresentant) {
            Representant representant = null;
            SqlParameter idRepresentantParam = new SqlParameter("@idRepresentant", idRepresentant);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.Parameters.Add(idRepresentantParam);
                    cmd.CommandText = @"SELECT * FROM representant WHERE id=@idRepresentant AND actif=1";

                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        while(rdr.Read()) {
                            representant = new Representant();
                            representant.Modification = new Modification();

                            //remplir l'objet entreprise
                            representant.Id = (int)rdr["id"];

                            if(!(rdr["prenom"] == DBNull.Value))
                                representant.Prenom = rdr["prenom"].ToString();

                            if(!(rdr["nom"] == DBNull.Value))
                                representant.Nom = rdr["nom"].ToString();

                            if(!(rdr["courriel"] == DBNull.Value))
                                representant.Courriel = rdr["courriel"].ToString();

                            if(!(rdr["tel1"] == DBNull.Value))
                                representant.Telephone1 = rdr["tel1"].ToString();
                            if(!(rdr["tel2"] == DBNull.Value))
                                representant.Telephone2 = rdr["tel2"].ToString();
                            if(!(rdr["tel3"] == DBNull.Value))
                                representant.Telephone3 = rdr["tel3"].ToString();

                            if(!(rdr["departement"] == DBNull.Value))
                                representant.Departement = rdr["departement"].ToString();

                            if(!(rdr["poste"] == DBNull.Value))
                                representant.Poste = rdr["poste"].ToString();

                            representant.IdEntreprise = (int)rdr["idEntreprise"];

                            if(!(rdr["idLangue"] == DBNull.Value))
                                representant.IdLangue = (int)rdr["idLangue"];
                            else
                                representant.IdLangue = null;

                            representant.Actif = (bool)rdr["actif"];
                            representant.Modification.UtilisateurId = (int)rdr["idUtilisateur"];
                            representant.Modification.DateModification = Convert.ToDateTime(rdr["dateModification"]);
                        }
                    }
                }
            }
            return representant;
        }

        //Modifier representant
        public static bool modifierRepresentant(Representant representant) {

            bool modifie = false;
            string requete = @"UPDATE representant SET prenom=@prenom, nom=@nom, courriel=@courriel, tel1=@tel1, tel2=@tel2, tel3=@tel3, departement=@departement, poste=@poste, idEntreprise=@idEntreprise, idLangue=@idLangue, idUtilisateur=@idUtilisateur WHERE id=@id";

            SqlParameter id = new SqlParameter("@id", representant.Id);
            SqlParameter prenom = new SqlParameter("@prenom", !string.IsNullOrEmpty(representant.Prenom) ? representant.Prenom : (object)DBNull.Value);
            SqlParameter nom = new SqlParameter("@nom", !string.IsNullOrEmpty(representant.Nom) ? representant.Nom : (object)DBNull.Value);
            SqlParameter courriel = new SqlParameter("@courriel", !string.IsNullOrEmpty(representant.Courriel) ? representant.Courriel : (object)DBNull.Value);
            SqlParameter tel1 = new SqlParameter("@tel1", !string.IsNullOrEmpty(representant.Telephone1) ? representant.Telephone1 : (object)DBNull.Value);
            SqlParameter tel2 = new SqlParameter("@tel2", !string.IsNullOrEmpty(representant.Telephone2) ? representant.Telephone2 : (object)DBNull.Value);
            SqlParameter tel3 = new SqlParameter("@tel3", !string.IsNullOrEmpty(representant.Telephone3) ? representant.Telephone3 : (object)DBNull.Value);
            SqlParameter departement = new SqlParameter("@departement", !string.IsNullOrEmpty(representant.Departement) ? representant.Departement : (object)DBNull.Value);
            SqlParameter poste = new SqlParameter("@poste", !string.IsNullOrEmpty(representant.Poste) ? representant.Poste : (object)DBNull.Value);
            SqlParameter idEntreprise = new SqlParameter("@idEntreprise", representant.IdEntreprise);
            SqlParameter idLangue = new SqlParameter("@idLangue", representant.IdLangue != null ? representant.IdLangue : (object)DBNull.Value);
            SqlParameter idUtilisateur = new SqlParameter("@idUtilisateur", representant.Modification.UtilisateurId);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(id);
                    cmd.Parameters.Add(prenom);
                    cmd.Parameters.Add(nom);
                    cmd.Parameters.Add(courriel);
                    cmd.Parameters.Add(tel1);
                    cmd.Parameters.Add(tel2);
                    cmd.Parameters.Add(tel3);
                    cmd.Parameters.Add(departement);
                    cmd.Parameters.Add(poste);
                    cmd.Parameters.Add(idEntreprise);
                    cmd.Parameters.Add(idLangue);
                    cmd.Parameters.Add(idUtilisateur);

                    int affectes = (int)cmd.ExecuteNonQuery();
                    if(affectes != 0)
                        modifie = true;
                }
            }
            return modifie;

        }

        public static void supprimerRepresentant(int idRepresentant) {
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"UPDATE representant SET actif=0 WHERE id=@id";
                    cmd.Parameters.Add(new SqlParameter("id", idRepresentant));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Entreprise recupererProfileEntrepriseParId(int idEntreprise) {
            Entreprise ent = null;
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"select * from entreprise where id=@id and actif=1 ";
                    cmd.Parameters.Add(new SqlParameter("@id", idEntreprise));

                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        while(rdr.Read()) {
                            ent = new Entreprise();
                            if(!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                ent.Id = rdr.GetInt32(rdr.GetOrdinal("id"));

                            if(!(rdr["nom"] == DBNull.Value))
                                ent.Nom = rdr["nom"].ToString();

                            if(!(rdr["imageLogo"] == DBNull.Value))
                                ent.ImageLogo = rdr["imageLogo"].ToString();

                            if(!(rdr["secteur"] == DBNull.Value))
                                ent.Secteur = rdr["secteur"].ToString();

                        }
                    }
                }
            }
            return ent;
        }


        //liste des entreprises avec photo, nom et secteur pour l'affichage des profiles
        public static List<Entreprise> recupererListeProfilesEntreprises() {
            List<Entreprise> listeEntreprises = null;
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"select * from entreprise where actif=1 ORDER BY nom ASC";

                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        if(rdr.HasRows) {
                            listeEntreprises = new List<Entreprise>();
                            while(rdr.Read()) {
                                Entreprise entreprise = new Entreprise();
                                if(!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                    entreprise.Id = rdr.GetInt32(rdr.GetOrdinal("id"));

                                if(!(rdr["nom"] == DBNull.Value))
                                    entreprise.Nom = rdr["nom"].ToString();

                                if(!(rdr["imageLogo"] == DBNull.Value))
                                    entreprise.ImageLogo = rdr["imageLogo"].ToString();

                                if(!(rdr["secteur"] == DBNull.Value))
                                    entreprise.Secteur = rdr["secteur"].ToString();

                                listeEntreprises.Add(entreprise);
                            }
                        }
                    }
                }
            }
            return listeEntreprises;
        }

        //liste des entreprises avec photo, nom et secteur pour l'affichage des profiles
        public static List<Entreprise> recupererListeProfilesEntreprisesParNom(string nom) {
            List<Entreprise> listeEntreprises = null;
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"select * from entreprise WHERE nom LIKE @nom";
                    cmd.Parameters.Add(new SqlParameter("nom", "%" + nom + "%"));

                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        if(rdr.HasRows) {
                            listeEntreprises = new List<Entreprise>();
                            while(rdr.Read()) {
                                Entreprise entreprise = new Entreprise();
                                if(!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                    entreprise.Id = rdr.GetInt32(rdr.GetOrdinal("id"));

                                if(!(rdr["nom"] == DBNull.Value))
                                    entreprise.Nom = rdr["nom"].ToString();

                                if(!(rdr["imageLogo"] == DBNull.Value))
                                    entreprise.ImageLogo = rdr["imageLogo"].ToString();

                                if(!(rdr["secteur"] == DBNull.Value))
                                    entreprise.Secteur = rdr["secteur"].ToString();

                                listeEntreprises.Add(entreprise);
                            }
                        }
                    }
                }
            }
            return listeEntreprises;
        }

        //profiles pour l'affichage des entreprises avec photo, nom et formation apres une recherche
        public static List<Entreprise> recupererListeProfilesEntreprisesSelonRecherche(Dictionary<string, string> WhereCondition) {
            List<Entreprise> listeEntreprises = null;
            string requetebasic = @"select distinct e.id, nom,  imageLogo, secteur from entreprise e left join domaineRecherche d on e.id=d.idEntreprise";
            //creation de la requete dynamique
            string requete = SQLHelper.DynamicSQL(WhereCondition, requetebasic);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {

                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        if(rdr.HasRows) {
                            listeEntreprises = new List<Entreprise>();
                            while(rdr.Read()) {
                                Entreprise entreprise = new Entreprise();
                                if(!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                    entreprise.Id = rdr.GetInt32(rdr.GetOrdinal("id"));

                                if(!(rdr["nom"] == DBNull.Value))
                                    entreprise.Nom = rdr["nom"].ToString();

                                if(!(rdr["imageLogo"] == DBNull.Value))
                                    entreprise.ImageLogo = rdr["imageLogo"].ToString();

                                if(!(rdr["secteur"] == DBNull.Value))
                                    entreprise.Secteur = rdr["secteur"].ToString();

                                listeEntreprises.Add(entreprise);
                            }
                        }
                    }
                }
            }
            return listeEntreprises;
        }

        public static List<IdDescription> recupererFormationsRecherchees(int idEntreprise) {
            List<IdDescription> listeFormations = null;

            string requete = @"select distinct f.id as id, f.description as description from domaineRecherche dr inner join formation f on dr.idFormation=f.id where dr.idEntreprise=@idEntreprise and f.actif=1";
            SqlParameter idEntrepriseParam = new SqlParameter("@idEntreprise", idEntreprise);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEntrepriseParam);
                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        if(rdr.HasRows) {
                            listeFormations = new List<IdDescription>();
                            while(rdr.Read()) {
                                IdDescription formation = new IdDescription();
                                formation.Id = (int)rdr["id"];
                                formation.Description = rdr["description"].ToString();

                                listeFormations.Add(formation);
                            }
                        }
                    }
                }
            }
            return listeFormations;
        }

        public static List<IdDescription> recupererTechnologiesRecherchees(int idEntreprise) {
            List<IdDescription> listeTechnologies = null;

            string requete = @"select distinct t.id as id, t.description as description from domaineRecherche dr inner join technologie t on dr.idTechnologie=t.id where dr.idEntreprise=@idEntreprise and t.actif=1";
            SqlParameter idEntrepriseParam = new SqlParameter("@idEntreprise", idEntreprise);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEntrepriseParam);
                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        if(rdr.HasRows) {
                            listeTechnologies = new List<IdDescription>();
                            while(rdr.Read()) {
                                IdDescription technologie = new IdDescription();
                                technologie.Id = (int)rdr["id"];
                                technologie.Description = rdr["description"].ToString();

                                listeTechnologies.Add(technologie);
                            }
                        }
                    }
                }
            }
            return listeTechnologies;
        }

        public static List<IdDescription> recupererInteretsRecherches(int idEntreprise) {
            List<IdDescription> listeInterets = null;

            string requete = @"select distinct i.id as id, i.description as description from domaineRecherche dr inner join interet i on dr.idInteret=i.id where dr.idEntreprise=@idEntreprise and i.actif=1";
            SqlParameter idEntrepriseParam = new SqlParameter("@idEntreprise", idEntreprise);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEntrepriseParam);
                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        if(rdr.HasRows) {
                            listeInterets = new List<IdDescription>();
                            while(rdr.Read()) {
                                IdDescription interet = new IdDescription();
                                interet.Id = (int)rdr["id"];
                                interet.Description = rdr["description"].ToString();

                                listeInterets.Add(interet);
                            }
                        }
                    }
                }
            }
            return listeInterets;
        }

        public static void ajouterFormationRecherchee(int idEntreprise, int idFormation) {
            string requete = @"INSERT INTO domaineRecherche (idEntreprise, idFormation) VALUES (@idEntreprise, @idFormation)";

            SqlParameter idEntrepriseParam = new SqlParameter("@idEntreprise", idEntreprise);
            SqlParameter idFormationParam = new SqlParameter("@idFormation", idFormation);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEntrepriseParam);
                    cmd.Parameters.Add(idFormationParam);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ajouterInteretRecherche(int idEntreprise, int idInteret) {
            string requete = @"INSERT INTO domaineRecherche (idEntreprise, idInteret) VALUES (@idEntreprise, @idInteret)";

            SqlParameter idEntrepriseParam = new SqlParameter("@idEntreprise", idEntreprise);
            SqlParameter idInteretParam = new SqlParameter("@idInteret", idInteret);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEntrepriseParam);
                    cmd.Parameters.Add(idInteretParam);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ajouterTechnologieRecherche(int idEntreprise, int idTechnologie) {
            string requete = @"INSERT INTO domaineRecherche (idEntreprise, idTechnologie) VALUES (@idEntreprise, @idTechnologie)";

            SqlParameter idEntrepriseParam = new SqlParameter("@idEntreprise", idEntreprise);
            SqlParameter idTechnologieParam = new SqlParameter("@idTechnologie", idTechnologie);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEntrepriseParam);
                    cmd.Parameters.Add(idTechnologieParam);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void deleteDomaineRecherche(int idEntreprise)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //Requete
                    cmd.CommandText = @"delete from domaineRecherche where idEntreprise=@idEntreprise";
                    cmd.Parameters.Add(new SqlParameter("idEntreprise", idEntreprise));
                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
