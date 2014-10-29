using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Data;

namespace DAL {
    public static class RequeteEtudiant {
        public static string ConnectionString = DefinitionConnection.ConnectionStringCommun;

        //Ajouter Etudiant
        public static int ajouterEtudiant(Etudiant etudiant)
        {

            int id = -1; ;
            // inserer le nouveau contact
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {


                    cmd.CommandText = @"INSERT INTO etudiant (nom, prenom, courriel, tel1, tel2, tel3, adresse, ville, dateNaissance, "
                                    + "dateFinFormation, idFormation, photoURL, idStatusCarriere, vehicule, permisConduire, experiences, "
                                    + "riveNord, riveSud, montreal, salaireEspere, posteDesire, idStatusResidence, commentaire, idUtilisateur) output inserted.id"
                                    + " VALUES (@nom, @prenom, @courriel, @tel1, @tel2, @tel3, @adresse, @ville, @dateNaissance, @dateFinFormation,"
                                    + " @idFormation, @photoURL, @idStatusCarriere, @vehicule, @permisConduire, @experiences, @riveNord, @riveSud,"
                                    + " @montreal, @salaireEspere, @posteDesire, @idStatusResidence, @commentaire, @idUtilisateur)";
                    cmd.Parameters.Add(new SqlParameter("nom", etudiant.Nom));
                    cmd.Parameters.Add(new SqlParameter("prenom", etudiant.Prenom));
                    cmd.Parameters.Add(new SqlParameter("courriel", (etudiant.Courriel == null) ? (object)DBNull.Value : etudiant.Courriel));
                    cmd.Parameters.Add(new SqlParameter("tel1", (etudiant.Telephone1 == null) ? (object)DBNull.Value : etudiant.Telephone1));
                    cmd.Parameters.Add(new SqlParameter("tel2", (etudiant.Telephone2 == null) ? (object)DBNull.Value : etudiant.Telephone2));
                    cmd.Parameters.Add(new SqlParameter("tel3", (etudiant.Telephone3 == null) ? (object)DBNull.Value : etudiant.Telephone3));
                    cmd.Parameters.Add(new SqlParameter("adresse", (etudiant.Adresse == null) ? (object)DBNull.Value : etudiant.Adresse));
                    cmd.Parameters.Add(new SqlParameter("ville", (etudiant.Ville == null) ? (object)DBNull.Value : etudiant.Ville));
                    cmd.Parameters.Add(new SqlParameter("dateNaissance", (etudiant.DateNaissance == null) ? (object)DBNull.Value : etudiant.DateNaissance));
                    cmd.Parameters.Add(new SqlParameter("dateFinFormation", etudiant.DateFinFormation));
                    cmd.Parameters.Add(new SqlParameter("idFormation", etudiant.IdFormation));
                    cmd.Parameters.Add(new SqlParameter("photoURL", (etudiant.PhotoURL == null) ? (object)DBNull.Value : etudiant.PhotoURL));
                    cmd.Parameters.Add(new SqlParameter("idStatusCarriere", etudiant.IdStatusCarriere));
                    cmd.Parameters.Add(new SqlParameter("vehicule", (etudiant.Vehicule == null) ? (object)DBNull.Value : etudiant.Vehicule));
                    cmd.Parameters.Add(new SqlParameter("permisConduire", (etudiant.PermisConduire == null) ? (object)DBNull.Value : etudiant.PermisConduire));
                    cmd.Parameters.Add(new SqlParameter("experiences", (etudiant.Experiences == null) ? (object)DBNull.Value : etudiant.Experiences));
                    cmd.Parameters.Add(new SqlParameter("riveNord", (etudiant.RiveNord == null) ? (object)DBNull.Value : etudiant.RiveNord));
                    cmd.Parameters.Add(new SqlParameter("riveSud", (etudiant.RiveSud == null) ? (object)DBNull.Value : etudiant.RiveSud));
                    cmd.Parameters.Add(new SqlParameter("montreal", (etudiant.Montreal == null) ? (object)DBNull.Value : etudiant.Montreal));
                    cmd.Parameters.Add(new SqlParameter("salaireEspere", (etudiant.SalaireEspere == null) ? (object)DBNull.Value : etudiant.SalaireEspere));
                    cmd.Parameters.Add(new SqlParameter("posteDesire", (etudiant.PosteDesire == null) ? (object)DBNull.Value : etudiant.PosteDesire));
                    cmd.Parameters.Add(new SqlParameter("idStatusResidence", (etudiant.IdStatusResidence == null) ? (object)DBNull.Value : etudiant.IdStatusResidence));
                    cmd.Parameters.Add(new SqlParameter("commentaire", (etudiant.Commentaire == null) ? (object)DBNull.Value : etudiant.Commentaire));
                    cmd.Parameters.Add(new SqlParameter("idUtilisateur", etudiant.Modification.UtilisateurId));
                    id = (int)cmd.ExecuteScalar();


                }
            }
            return id;
        }
        //Modifier  Etudiant
        public static int modifierEtudiant(Etudiant etudiant) {
            int ligne_affecte = 0;
            // inserer le nouveau contact
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {


                    cmd.CommandText = @"UPDATE etudiant SET nom=@nom, prenom=@prenom, courriel=@courriel, tel1=@tel1,"
                                    + "tel2=@tel2, tel3=@tel3, adresse=@adresse, ville=@ville, dateNaissance=@dateNaissance,"
                                    + "dateFinFormation=@dateFinFormation, idFormation=@idFormation, photoURL=@photoURL,"
                                    + " idStatusCarriere=@idStatusCarriere, vehicule=@vehicule, permisConduire=@permisConduire, "
                                    + "experiences=@experiences, riveNord=@riveNord, riveSud=@riveSud, montreal=@montreal, "
                                    + "salaireEspere=@salaireEspere, posteDesire=@posteDesire, idStatusResidence=@idStatusResidence, "
                                    + "commentaire=@commentaire, idUtilisateur=@idUtilisateur WHERE id=@id";
                    cmd.Parameters.Add(new SqlParameter("id", etudiant.Id));
                     cmd.Parameters.Add(new SqlParameter("nom", etudiant.Nom));
                    cmd.Parameters.Add(new SqlParameter("prenom", etudiant.Prenom));
                    cmd.Parameters.Add(new SqlParameter("courriel", (etudiant.Courriel == null) ? (object)DBNull.Value : etudiant.Courriel));
                    cmd.Parameters.Add(new SqlParameter("tel1", (etudiant.Telephone1 == null) ? (object)DBNull.Value : etudiant.Telephone1));
                    cmd.Parameters.Add(new SqlParameter("tel2", (etudiant.Telephone2 == null) ? (object)DBNull.Value : etudiant.Telephone2));
                    cmd.Parameters.Add(new SqlParameter("tel3", (etudiant.Telephone3 == null) ? (object)DBNull.Value : etudiant.Telephone3));
                    cmd.Parameters.Add(new SqlParameter("adresse", (etudiant.Adresse == null) ? (object)DBNull.Value : etudiant.Adresse));
                    cmd.Parameters.Add(new SqlParameter("ville", (etudiant.Ville == null) ? (object)DBNull.Value : etudiant.Ville));
                    cmd.Parameters.Add(new SqlParameter("dateNaissance", (etudiant.DateNaissance == null) ? (object)DBNull.Value : etudiant.DateNaissance));
                    cmd.Parameters.Add(new SqlParameter("dateFinFormation", etudiant.DateFinFormation));
                    cmd.Parameters.Add(new SqlParameter("idFormation", etudiant.IdFormation));
                    cmd.Parameters.Add(new SqlParameter("photoURL", (etudiant.PhotoURL == null) ? (object)DBNull.Value : etudiant.PhotoURL));
                    cmd.Parameters.Add(new SqlParameter("idStatusCarriere", etudiant.IdStatusCarriere));
                    cmd.Parameters.Add(new SqlParameter("vehicule", (etudiant.Vehicule == null) ? (object)DBNull.Value : etudiant.Vehicule));
                    cmd.Parameters.Add(new SqlParameter("permisConduire", (etudiant.PermisConduire == null) ? (object)DBNull.Value : etudiant.PermisConduire));
                    cmd.Parameters.Add(new SqlParameter("experiences", (etudiant.Experiences == null) ? (object)DBNull.Value : etudiant.Experiences));
                    cmd.Parameters.Add(new SqlParameter("riveNord", (etudiant.RiveNord == null) ? (object)DBNull.Value : etudiant.RiveNord));
                    cmd.Parameters.Add(new SqlParameter("riveSud", (etudiant.RiveSud == null) ? (object)DBNull.Value : etudiant.RiveSud));
                    cmd.Parameters.Add(new SqlParameter("montreal", (etudiant.Montreal == null) ? (object)DBNull.Value : etudiant.Montreal));
                    cmd.Parameters.Add(new SqlParameter("salaireEspere", (etudiant.SalaireEspere == null) ? (object)DBNull.Value : etudiant.SalaireEspere));
                    cmd.Parameters.Add(new SqlParameter("posteDesire", (etudiant.PosteDesire == null) ? (object)DBNull.Value : etudiant.PosteDesire));
                    cmd.Parameters.Add(new SqlParameter("idStatusResidence", (etudiant.IdStatusResidence == null) ? (object)DBNull.Value : etudiant.IdStatusResidence));
                    cmd.Parameters.Add(new SqlParameter("commentaire", (etudiant.Commentaire == null) ? (object)DBNull.Value : etudiant.Commentaire));
                    cmd.Parameters.Add(new SqlParameter("idUtilisateur", etudiant.Modification.UtilisateurId));
                    ligne_affecte = cmd.ExecuteNonQuery();
                }
            }
            return ligne_affecte;
        }

        //profiles pour l'affichage des etudiants avec photo, nom et formation
        public static List<Etudiant> recupererListeProfilesEtudiantsRechercheStage() {
            List<Etudiant> listeEtudiants = null;
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"select * from etudiant where idStatusCarriere=1 and actif=1 ";

                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        if(rdr.HasRows) {
                            listeEtudiants = new List<Etudiant>();
                            while(rdr.Read()) {
                                Etudiant etu = new Etudiant();
                                if(!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                    etu.Id = rdr.GetInt32(rdr.GetOrdinal("id"));

                                if(!(rdr["nom"] == DBNull.Value))
                                    etu.Nom = rdr["nom"].ToString();

                                if(!(rdr["prenom"] == DBNull.Value))
                                    etu.Prenom = rdr["prenom"].ToString();

                                if(!(rdr["photoURL"] == DBNull.Value))
                                    etu.PhotoURL = rdr["photoURL"].ToString();

                                if(!rdr.IsDBNull(rdr.GetOrdinal("idFormation")))
                                    etu.IdFormation = rdr.GetInt32(rdr.GetOrdinal("idFormation"));

                                listeEtudiants.Add(etu);
                            }
                        }
                    }
                }
            }
            return listeEtudiants;
        }

        //profiles pour l'affichage des etudiants avec photo, nom et formation apres une recherche
        public static List<Etudiant> recupererListeProfilesEtudiantsSelonRecherche(Dictionary<string, string> WhereCondition) {
            List<Etudiant> listeEtudiants = null;
            string requetebasic = @"select * from etudiant";
            //creation de la requete dynamique
            string requete = SQLHelper.DynamicSQL(WhereCondition, requetebasic);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {

                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        if(rdr.HasRows) {
                            listeEtudiants = new List<Etudiant>();
                            while(rdr.Read()) {
                                Etudiant etu = new Etudiant();
                                if(!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                    etu.Id = rdr.GetInt32(rdr.GetOrdinal("id"));

                                if(!(rdr["nom"] == DBNull.Value))
                                    etu.Nom = rdr["nom"].ToString();

                                if(!(rdr["prenom"] == DBNull.Value))
                                    etu.Prenom = rdr["prenom"].ToString();

                                if(!(rdr["photoURL"] == DBNull.Value))
                                    etu.PhotoURL = rdr["photoURL"].ToString();

                                if(!rdr.IsDBNull(rdr.GetOrdinal("idFormation")))
                                    etu.IdFormation = rdr.GetInt32(rdr.GetOrdinal("idFormation"));

                                listeEtudiants.Add(etu);
                            }
                        }
                    }
                }
            }
            return listeEtudiants;
        }

        public static Etudiant recupererProfileEtudiantParId(int idEtudiant) {
            Etudiant etu = null;
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"select * from etudiant where id=@id and actif=1 ";
                    cmd.Parameters.Add(new SqlParameter("@id", idEtudiant));

                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        while(rdr.Read()) {
                            etu = new Etudiant();
                            if(!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                etu.Id = rdr.GetInt32(rdr.GetOrdinal("id"));

                            if(!(rdr["nom"] == DBNull.Value))
                                etu.Nom = rdr["nom"].ToString();

                            if(!(rdr["prenom"] == DBNull.Value))
                                etu.Prenom = rdr["prenom"].ToString();

                            if(!(rdr["photoURL"] == DBNull.Value))
                                etu.PhotoURL = rdr["photoURL"].ToString();

                            if(!rdr.IsDBNull(rdr.GetOrdinal("idFormation")))
                                etu.IdFormation = rdr.GetInt32(rdr.GetOrdinal("idFormation"));

                        }
                    }
                }
            }
            return etu;
        }

        //Supprimer Etudiant
        public static void supprimerEtudiant(int idEtudiant) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    //Requete
                    cmd.CommandText = @"UPDATE etudiant SET actif=0 WHERE id=@id";
                    cmd.Parameters.Add(new SqlParameter("id", idEtudiant));
                    cmd.ExecuteNonQuery();
                }
            }

        }
        //Recuperer un etudiant
        public static Etudiant recupererEtudiant(int idEtudiant) {
            Etudiant etudiant = null;
            SqlParameter idEtudiantParam = new SqlParameter("@idEtudiant", idEtudiant);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.Parameters.Add(idEtudiantParam);
                    cmd.CommandText = @"select * from etudiant where id=@idEtudiant and actif=1";

                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        while(rdr.Read()) {
                            etudiant = new Etudiant();
                            etudiant.Modification = new Modification();
                            etudiant.Id = (int)rdr["id"];
                            etudiant.Nom = rdr["nom"].ToString();
                            etudiant.Prenom = rdr["prenom"].ToString();

                            if(!(rdr["courriel"] == DBNull.Value))
                                etudiant.Courriel = rdr["courriel"].ToString();

                            if(!(rdr["dateNaissance"] == DBNull.Value))
                                etudiant.DateNaissance = Convert.ToDateTime(rdr["dateNaissance"]);
                            else
                                etudiant.DateNaissance = null;

                            etudiant.DateFinFormation = Convert.ToDateTime(rdr["dateFinFormation"]);
                            etudiant.IdFormation = (int)rdr["idFormation"];

                            if(!(rdr["photoURL"] == DBNull.Value))
                                etudiant.PhotoURL = rdr["photoURL"].ToString();

                            etudiant.IdStatusCarriere = (int)rdr["idStatusCarriere"];

                            if(!(rdr["vehicule"] == DBNull.Value))
                                etudiant.Vehicule = (bool)rdr["vehicule"];
                            else
                                etudiant.Vehicule = null;

                            if(!(rdr["permisConduire"] == DBNull.Value))
                                etudiant.PermisConduire = (bool)rdr["permisConduire"];
                            else
                                etudiant.PermisConduire = null;

                            if(!(rdr["experiences"] == DBNull.Value))
                                etudiant.Experiences = rdr["experiences"].ToString();

                            if(!(rdr["riveNord"] == DBNull.Value))
                                etudiant.RiveNord = (bool)rdr["riveNord"];
                            else
                                etudiant.RiveNord = null;

                            if(!(rdr["riveSud"] == DBNull.Value))
                                etudiant.RiveSud = (bool)rdr["riveSud"];
                            else
                                etudiant.RiveSud = null;

                            if(!(rdr["montreal"] == DBNull.Value))
                                etudiant.Montreal = (bool)rdr["montreal"];
                            else
                                etudiant.Montreal = null;

                            if(rdr["salaireEspere"] != DBNull.Value)
                                etudiant.SalaireEspere = Double.Parse(rdr["salaireEspere"].ToString(), NumberStyles.Currency);
                            else
                                etudiant.SalaireEspere = null;

                            if(!(rdr["posteDesire"] == DBNull.Value))
                                etudiant.PosteDesire = rdr["posteDesire"].ToString();

                            if(!(rdr["idStatusResidence"] == DBNull.Value))
                                etudiant.IdStatusResidence = (int)rdr["idStatusResidence"];
                            else
                                etudiant.IdStatusResidence = null;

                            if(!(rdr["commentaire"] == DBNull.Value))
                                etudiant.Commentaire = rdr["commentaire"].ToString();

                            etudiant.Actif = (bool)rdr["actif"];

                            if(!(rdr["tel1"] == DBNull.Value))
                                etudiant.Telephone1 = rdr["tel1"].ToString();
                            if(!(rdr["tel2"] == DBNull.Value))
                                etudiant.Telephone2 = rdr["tel2"].ToString();
                            if(!(rdr["tel3"] == DBNull.Value))
                                etudiant.Telephone3 = rdr["tel3"].ToString();

                            if(!(rdr["adresse"] == DBNull.Value))
                                etudiant.Adresse = rdr["adresse"].ToString();
                            if(!(rdr["ville"] == DBNull.Value))
                                etudiant.Ville = rdr["ville"].ToString();

                            etudiant.StatusCarriere = (int)rdr["idStatusCarriere"];
                            etudiant.Modification.UtilisateurId = (int)rdr["idUtilisateur"];
                            etudiant.Modification.DateModification = Convert.ToDateTime(rdr["dateModification"]);

                        }
                    }
                }
            }
            return etudiant;
        }



        //Recuperer liste profile etudiants par le nom 
        public static List<Etudiant> recupererEtudiantsParNom(string nom) {
            List<Etudiant> etudiants = null;
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"SELECT * FROM etudiant WHERE (nom LIKE @nom OR prenom LIKE @nom) and actif = 1";
                    cmd.Parameters.Add(new SqlParameter("@nom", "%" + nom + "%"));

                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        if(rdr.HasRows) {
                            etudiants = new List<Etudiant>();
                            while(rdr.Read()) {
                                Etudiant etu = new Etudiant();
                                if(!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                    etu.Id = rdr.GetInt32(rdr.GetOrdinal("id"));

                                if(!(rdr["nom"] == DBNull.Value))
                                    etu.Nom = rdr["nom"].ToString();

                                if(!(rdr["prenom"] == DBNull.Value))
                                    etu.Prenom = rdr["prenom"].ToString();

                                if(!(rdr["photoURL"] == DBNull.Value))
                                    etu.PhotoURL = rdr["photoURL"].ToString();

                                if(!rdr.IsDBNull(rdr.GetOrdinal("idFormation")))
                                    etu.IdFormation = rdr.GetInt32(rdr.GetOrdinal("idFormation"));

                                etudiants.Add(etu);
                            }
                        }
                    }
                }
                return etudiants;
            }
        }

        //Recuperer les etudiants par ville
        public static List<Etudiant> recupererEtudiantsParVille(string ville) {
            List<Etudiant> etudiants = new List<Etudiant>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    //Requette
                    //cmd.CommandText = @"SELECT * FROM etudiant WHERE ville=@ville";
                    //cmd.Parameters.Add(new SqlParameter("ville","'" + nom + "'" ));

                    cmd.CommandText = @"SELECT * FROM etudiant WHERE ville LIKE @ville";
                    cmd.Parameters.Add(new SqlParameter("ville", "%" + ville + "%"));


                    cmd.ExecuteNonQuery();


                }
            }
            return etudiants;

        }



        //Recuperer les etudiants ayant un emploi
        public static List<Etudiant> recupererEtudiantAvecEmploi() {
            List<Etudiant> etudiants = new List<Etudiant>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    //Requette
                    cmd.CommandText = @"SELECT * FROM etudiant WHERE idStatusCarriere=2";


                    cmd.ExecuteNonQuery();

                }
            }
            return etudiants;
        }


        ////Recuperer les etudiants sans emploi
        //public static List<Etudiant> recupererEtudiantSansEmploi()
        //{
        //    List<Etudiant> etudiants = new List<Etudiant>();
        //    using (SqlConnection conn = new SqlConnection(ConnectionString))
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {

        //            //Requette
        //            cmd.CommandText = @"SELECT * FROM etudiant WHERE idStatusCarriere=1";


        //            cmd.ExecuteNonQuery();


        //        }
        //    }
        //    return etudiants;

        //}

        //Recuperer les etudiants sans stage
        public static List<Etudiant> recupererEtudiantSansStage() {
            List<Etudiant> etudiants = new List<Etudiant>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    //Requette
                    cmd.CommandText = @""; // en recherche c'est pour avec emploi ou pas?


                    cmd.ExecuteNonQuery();


                }
            }
            return etudiants;

        }

        //Recuperer les etudiants avec voiture
        public static List<Etudiant> recupererEtudiantAvecVoiture() {
            List<Etudiant> etudiants = new List<Etudiant>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    //Requette
                    cmd.CommandText = @"SELECT * FROM etudiant WHERE vehicule=1";


                    cmd.ExecuteNonQuery();


                }
            }
            return etudiants;

        }

        //Recuperer les etudiants sans voiture
        public static List<Etudiant> recupererEtudiantSansVoiture() {
            List<Etudiant> etudiants = new List<Etudiant>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    //Requette
                    cmd.CommandText = @"SELECT * FROM etudiant WHERE vehicule=0";


                    cmd.ExecuteNonQuery();


                }
            }
            return etudiants;

        }

        //recuperer liste langues d'un etudiant
        public static List<Langue> recupererLanguesEtudiant(int idEtudiant) {
            List<Langue> listeLangues = null;

            string requete = @"SELECT L.id as id,L.[description] as description ,NL.[id] "
                + "as niveau FROM langue L INNER JOIN langueEtudiant LE ON L.id=LE.idLangue INNER JOIN etudiant E ON E.id=LE.idEtudiant INNER JOIN niveauLangue NL ON NL.id=L.id  WHERE e.id=@id";
            SqlParameter idEtudiantParam = new SqlParameter("@id", idEtudiant);
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEtudiantParam);
                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        if(rdr.HasRows) {
                            listeLangues = new List<Langue>();
                            while(rdr.Read()) {
                                Langue langue = new Langue();
                                langue.Id = (int)rdr["id"];
                                langue.Description = rdr["description"].ToString();
                                langue.Niveau = (int)rdr["niveau"];
                                   

                                listeLangues.Add(langue);
                            }
                        }
                    }
                }
            }
            return listeLangues;
        }



        //Recuperer liste entreprises potentiel d'un etudiant
        public static List<Entreprise> recupererListeEntreprisePotentielEtudiant(int idEtudiant) {
            List<Entreprise> entreprises = new List<Entreprise>();
            // inserer le nouveau contact
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {


                    cmd.CommandText = @"SELECT * FROM  entreprise E INNER JOIN domaineRecherche DR ON E.id=DR.idEntreprise "
                                    + " INNER JOIN technologiesPreferes TP ON TP.idTechnologie=DR.idInteret "
                                    + "INNER JOIN etudiant ET ON ET.id=TP.idEtudiant WHERE ET.id=@id";
                    cmd.Parameters.Add(new SqlParameter("id", idEtudiant));

                    cmd.ExecuteNonQuery();




                }
            }
            return entreprises;
        }


        //Modifier status carriere etudiant
        public static void modifierStatusCarriereEtudiant(int idEtudiant, int idStatusCarriere) {
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"UPDATE etudiant SET idStatusCarriere=@idStatus WHERE id=@id";
                    cmd.Parameters.Add(new SqlParameter("idStatus", idStatusCarriere));
                    cmd.Parameters.Add(new SqlParameter("id", idEtudiant));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //retour d'une liste de interets d'un etudiant avec id et description
        public static List<IdDescription> recupererInteretsEtudiant(int idEtudiant) {
            List<IdDescription> listeInterets = null;

            string requete = @"select i.id as id, i.description as description from interetEtudiant ie inner join interet i on i.id=ie.idInteret where i.actif=1 and ie.idEtudiant=@idEtudiant";
            SqlParameter idEtudiantParam = new SqlParameter("@idEtudiant", idEtudiant);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEtudiantParam);
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


        public static void ajouterInteretEtudiant(int idEtudiant, int id) {
            string requete = @"INSERT INTO interetEtudiant(idEtudiant, idInteret) VALUES (@idEtudiant, @idInteret)";

            SqlParameter idEtudiantParam = new SqlParameter("@idEtudiant", idEtudiant);
            SqlParameter idInteretParam = new SqlParameter("@idInteret", id);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEtudiantParam);
                    cmd.Parameters.Add(idInteretParam);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ajouterTechnologieEtudiant(int idEtudiant, int id) {
            string requete = @"INSERT INTO technologiesPreferes(idEtudiant, idTechnologie) VALUES (@idEtudiant, @idTechnologie)";

            SqlParameter idEtudiantParam = new SqlParameter("@idEtudiant", idEtudiant);
            SqlParameter idTechParam = new SqlParameter("@idTechnologie", id);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEtudiantParam);
                    cmd.Parameters.Add(idTechParam);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ajouterLangueEtudiant(int idEtudiant, Langue langue) {
            string requete = @"INSERT INTO langueEtudiant(idEtudiant, idLangue, idNiveauLangue) VALUES (@idEtudiant, @idLangue, @idNiveauLangue)";

            SqlParameter idEtudiantParam = new SqlParameter("@idEtudiant", idEtudiant);
            SqlParameter idLangueParam = new SqlParameter("@idLangue", langue.Id);
            SqlParameter idNiveauLangueParam = new SqlParameter("@idNiveauLangue", langue.Niveau);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEtudiantParam);
                    cmd.Parameters.Add(idLangueParam);
                    cmd.Parameters.Add(idNiveauLangueParam);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        //retour d'une liste de technologies preferees par un etudiant avec id et description
        public static List<IdDescription> recupererTechnologiesPrefereesEtudiant(int idEtudiant) {
            List<IdDescription> listeTechnologies = null;

            string requete = @"select t.[id] as id, t.[description] as description from  technologie t inner join technologiesPreferes tp on t.id=tp.idTechnologie where t.actif=1 and tp.idEtudiant=@idEtudiant";
            SqlParameter idEtudiantParam = new SqlParameter("@idEtudiant", idEtudiant);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEtudiantParam);
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

        public static void deleteInteretEtudiant(int idEtudiant)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //Requete
                    cmd.CommandText = @"delete from interetEtudiant where idEtudiant=@idEtudiant";
                    cmd.Parameters.Add(new SqlParameter("idEtudiant", idEtudiant));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void deleteTechnologieEtudiant(int idEtudiant)
        {
             using(SqlConnection conn = new SqlConnection(ConnectionString))
             {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    //Requete
                    cmd.CommandText = @"delete from technologiesPreferes where idEtudiant=@idEtudiant";
                    cmd.Parameters.Add(new SqlParameter("idEtudiant", idEtudiant));
                   cmd.ExecuteNonQuery();
                }
             }
        }

        public static void deleteLangueEtudiant(int idEtudiant)
        {
             using(SqlConnection conn = new SqlConnection(ConnectionString))
             {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) 
                {
                    //Requete
                    cmd.CommandText = @"delete from langueEtudiant where idEtudiant=@idEtudiant";
                    cmd.Parameters.Add(new SqlParameter("idEtudiant", idEtudiant));
                   cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
