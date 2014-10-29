using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL {
    public static class RequeteInformation {
        public static string ConnectionString = DefinitionConnection.ConnectionStringCommun;

        //Recuperer list formation
        public static List<Formation> recupererFormations() {
            List<Formation> formations = new List<Formation>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    //Requette 
                    cmd.CommandText = @"select * from formation where actif = 1";
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        while(reader.Read()) {

                            Formation formation = new Formation();

                            formation.Id = (int)reader["id"];
                            formation.Description = (string)reader["description"];
                            formation.NbHeure = (int)reader["nbHeures"];

                            formations.Add(formation);

                        }
                    }




                }
            }
            return formations;

        }

        //Recuperer list satus Carriere
        public static List<IdDescription> recupererStatusCarriere() {
            List<IdDescription> statusCarrieres = new List<IdDescription>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"select * from statusCarriere where actif = 1";
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        while(reader.Read()) {

                            IdDescription statusCarriere = new IdDescription();

                            statusCarriere.Id = (int)reader["id"];
                            statusCarriere.Description = (string)reader["description"];


                            statusCarrieres.Add(statusCarriere);

                        }
                    }
                }
            }
            return statusCarrieres;

        }

        //Recuperer list type Utlisateur
        public static List<IdDescription> recupererTypeUtilisateur() {
            List<IdDescription> typeutlisateur = new List<IdDescription>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"select * from typeUtilisateur where actif = 1";
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        while(reader.Read()) {

                            IdDescription statusCarriere = new IdDescription();

                            statusCarriere.Id = (int)reader["id"];
                            statusCarriere.Description = (string)reader["description"];


                            typeutlisateur.Add(statusCarriere);

                        }
                    }
                }
            }
            return typeutlisateur;

        }

        //Recuperer list status residense
        public static List<IdDescription> recupererStatusResidence() {
            List<IdDescription> statusResidenses = new List<IdDescription>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"select * from statusResidence where actif = 1";
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        while(reader.Read()) {

                            IdDescription statusResidense = new IdDescription();

                            statusResidense.Id = (int)reader["id"];
                            statusResidense.Description = (string)reader["description"];


                            statusResidenses.Add(statusResidense);

                        }
                    }


                }
            }
            return statusResidenses;

        }

        //Recuperer list interet
        public static List<IdDescription> recupererInterets() {
            List<IdDescription> interets = new List<IdDescription>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"select * from interet where actif = 1";
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        while(reader.Read()) {

                            IdDescription interet = new IdDescription();

                            interet.Id = (int)reader["id"];
                            interet.Description = (string)reader["description"];


                            interets.Add(interet);

                        }
                    }
                }
            }
            return interets;

        }

        //Recuperer list Niveau langue
        public static List<IdDescription> recupererNiveauLangue() {
            List<IdDescription> niveauLangues = new List<IdDescription>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"select * from niveauLangue where actif = 1";
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        while(reader.Read()) {

                            IdDescription niveauLangue = new IdDescription();

                            niveauLangue.Id = (int)reader["id"];
                            niveauLangue.Description = (string)reader["description"];


                            niveauLangues.Add(niveauLangue);

                        }
                    }

                }
            }
            return niveauLangues;

        }


        //Recuperer list technologie
        public static List<IdDescription> recupererTechnologie() {
            List<IdDescription> technologies = new List<IdDescription>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"select * from technologie where actif = 1";
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        while(reader.Read()) {

                            IdDescription technologie = new IdDescription();

                            technologie.Id = (int)reader["id"];
                            technologie.Description = (string)reader["description"];


                            technologies.Add(technologie);

                        }
                    }

                }
            }
            return technologies;

        }

        //Recuperer list Langue
        public static List<Langue> recupererLangue() {
            List<Langue> langues = new List<Langue>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"select * from langue where actif = 1";
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        while(reader.Read()) {

                            Langue langue = new Langue();

                            langue.Id = (int)reader["id"];
                            langue.Description = (string)reader["description"];


                            langues.Add(langue);

                        }
                    }


                }
            }
            return langues;



        }

        //Recuperer list Type stage
        public static List<IdDescription> recupererListTypeStage() {
            List<IdDescription> typeStages = new List<IdDescription>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"select * from typeStage where actif = 1";
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        while(reader.Read()) {

                            IdDescription typeStage = new IdDescription();

                            typeStage.Id = (int)reader["id"];
                            typeStage.Description = (string)reader["description"];


                            typeStages.Add(typeStage);

                        }
                    }

                }
            }
            return typeStages;



        }

        //Recuperer list Type Communication
        public static List<IdDescription> recupererListTypeCommunication() {
            List<IdDescription> typeCommunications = new List<IdDescription>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"select * from typeCommunication where actif = 1";
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        while(reader.Read()) {

                            IdDescription typeCommunication = new IdDescription();

                            typeCommunication.Id = (int)reader["id"];
                            typeCommunication.Description = (string)reader["description"];


                            typeCommunications.Add(typeCommunication);

                        }
                    }

                }
            }
            return typeCommunications;



        }

        //Recuperer list Type Entrevue
        public static List<IdDescription> recupererListTypeEntrevue() {
            List<IdDescription> listTypeEntrevue = new List<IdDescription>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"select * from typeEntrevue where actif = 1";
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        while(reader.Read()) {

                            IdDescription typeEntrevue = new IdDescription();

                            typeEntrevue.Id = (int)reader["id"];
                            typeEntrevue.Description = (string)reader["description"];


                            listTypeEntrevue.Add(typeEntrevue);

                        }
                    }

                }
            }
            return listTypeEntrevue;



        }

        //Recuperer list Type resultat
        public static List<IdDescription> recupererListTypeResultat() {
            List<IdDescription> listTypeResultat = new List<IdDescription>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"select * from resultat where actif = 1";
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        while(reader.Read()) {

                            IdDescription resultat = new IdDescription();

                            resultat.Id = (int)reader["id"];
                            resultat.Description = (string)reader["description"];


                            listTypeResultat.Add(resultat);

                        }
                    }

                }
            }
            return listTypeResultat;



        }

        //Recuperer list Type Doccument
        public static List<IdDescription> recupererListTypeDocument() {
            List<IdDescription> listTypeDocument = new List<IdDescription>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"select * from typeDocument where actif = 1";
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        while(reader.Read()) {

                            IdDescription typeDocument = new IdDescription();

                            typeDocument.Id = (int)reader["id"];
                            typeDocument.Description = (string)reader["description"];


                            listTypeDocument.Add(typeDocument);

                        }
                    }

                }
            }
            return listTypeDocument;



        }

        //Recuperer list status Communication
        public static List<IdDescription> recupererListStatusCommunication() {
            List<IdDescription> statusCommunications = new List<IdDescription>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"select * from statusCommunication where actif = 1";
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        while(reader.Read()) {

                            IdDescription statusCommunication = new IdDescription();

                            statusCommunication.Id = (int)reader["id"];
                            statusCommunication.Description = (string)reader["description"];


                            statusCommunications.Add(statusCommunication);

                        }
                    }
                }
            }
            return statusCommunications;



        }

        //Recuperer list type utlisateur
        public static List<IdDescription> recupererListTypeUtilisateur() {
            List<IdDescription> typeUtilisateurs = new List<IdDescription>();
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"select * from typeUtilisateur where actif = 1";
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        while(reader.Read()) {

                            IdDescription typeUtilisateur = new IdDescription();

                            typeUtilisateur.Id = (int)reader["id"];
                            typeUtilisateur.Description = (string)reader["description"];


                            typeUtilisateurs.Add(typeUtilisateur);

                        }
                    }
                }
            }
            return typeUtilisateurs;



        }

        //Ajouter Formation
        public static void ajouterFormation(Formation formation) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"insert into formation (description, nbHeures) values (@description, @nbHeures)";
                    cmd.Parameters.Add(new SqlParameter("description", formation.Description));
                    cmd.Parameters.Add(new SqlParameter("nbHeures", formation.NbHeure));

                    cmd.ExecuteNonQuery();

                }
            }

        }

        //Ajouter status Carriere
        public static void ajouterStatusCarrire(IdDescription statusCarriere) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"insert into statusCarriere (description) values (@description)";
                    cmd.Parameters.Add(new SqlParameter("description", statusCarriere.Description));


                    cmd.ExecuteNonQuery();

                }
            }

        }


        //Ajouter status resisidence
        public static void ajouterStatusResidence(IdDescription statusResidence) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"insert into statusResidence (description) values (@description)";
                    cmd.Parameters.Add(new SqlParameter("description", statusResidence.Description));


                    cmd.ExecuteNonQuery();


                }
            }

        }


        //Ajouter interet
        public static void ajouterInteret(IdDescription interet) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"insert into interet (description) values (@description)";
                    cmd.Parameters.Add(new SqlParameter("description", interet.Description));


                    cmd.ExecuteNonQuery();


                }
            }

        }


        //Ajouter Niveau langue
        public static void ajouterNiveauLangue(IdDescription niveauLangue) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"insert into niveauLangue (description) values (@description)";
                    cmd.Parameters.Add(new SqlParameter("description", niveauLangue.Description));


                    cmd.ExecuteNonQuery();


                }
            }

        }


        //Ajouter Technologie
        public static void ajouterTechnologie(IdDescription technologie) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"insert into technologie (description) values (@description)";
                    cmd.Parameters.Add(new SqlParameter("description", technologie.Description));


                    cmd.ExecuteNonQuery();


                }
            }

        }
        //Ajouter type stage
        public static void ajouterTypeStage(IdDescription TypeStage) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"insert into TypeStage (description) values (@description)";
                    cmd.Parameters.Add(new SqlParameter("description", TypeStage.Description));


                    cmd.ExecuteNonQuery();


                }
            }

        }


        //Ajouter type stage
        public static void ajouterTypeResultat(IdDescription TypeStage) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"insert into resultat (description) values (@description)";
                    cmd.Parameters.Add(new SqlParameter("description", TypeStage.Description));


                    cmd.ExecuteNonQuery();


                }
            }

        }
        //Ajouter type stage
        public static void ajouterTypeEntrevue(IdDescription TypeStage) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"insert into typeEntrevue (description) values (@description)";
                    cmd.Parameters.Add(new SqlParameter("description", TypeStage.Description));


                    cmd.ExecuteNonQuery();


                }
            }

        }//Ajouter type stage
        public static void ajouterTypeDocument(IdDescription TypeStage) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"insert into typeDocument (description) values (@description)";
                    cmd.Parameters.Add(new SqlParameter("description", TypeStage.Description));


                    cmd.ExecuteNonQuery();


                }
            }

        }
        //Ajouter type communication
        public static void ajouterTypeCommunication(IdDescription typeCommunication) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"insert into typeCommunication (description) values (@description)";
                    cmd.Parameters.Add(new SqlParameter("description", typeCommunication.Description));


                    cmd.ExecuteNonQuery();

                }
            }

        }


        //Ajouter status communication
        public static void ajouterStatusCommunication(IdDescription statusCommunication) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"insert into statusCommunication (description) values (@description)";
                    cmd.Parameters.Add(new SqlParameter("description", statusCommunication.Description));


                    cmd.ExecuteNonQuery();

                }
            }

        }


        //Ajouter type utilisateur
        public static void ajouterTypeUtilisateur(IdDescription typeUtilisateur) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"insert into typeUtilisateur (description) values (@description)";
                    cmd.Parameters.Add(new SqlParameter("description", typeUtilisateur.Description));


                    cmd.ExecuteNonQuery();

                }
            }

        }
        //Ajouter langue
        public static void ajouterLangue(Langue langue) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"insert into langue (description) values (@description)";
                    cmd.Parameters.Add(new SqlParameter("description", langue.Description));


                    cmd.ExecuteNonQuery();


                }
            }

        }

        //Supprimer status Carriere
        public static void supprimerStatusCarriere(int idStatusCarriere) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    //Requette
                    cmd.CommandText = @"update statusCarriere set actif = 0 where id = @idStatusCarriere";
                    cmd.Parameters.Add(new SqlParameter("idStatusCarriere", idStatusCarriere));

                    cmd.ExecuteNonQuery();


                }
            }

        }

        //Supprimer status resisidence
        public static void supprimerStatusResidence(int idStatusResidence) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"update statusResidence set actif = 0 where id = @idStatusResidence";
                    cmd.Parameters.Add(new SqlParameter("idInteret", idStatusResidence));

                    cmd.ExecuteNonQuery();


                }
            }

        }
        //Supprimer interet
        public static void supprimerInteret(int idInteret) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"update interet set actif = 0 where id = @idInteret";
                    cmd.Parameters.Add(new SqlParameter("idInteret", idInteret));

                    cmd.ExecuteNonQuery();


                }
            }

        }
        //Supprimer Niveau langue
        public static void supprimerNiveauLangue(int idNiveaulangue) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"update niveauLangue set actif = 0 where id = @idNiveaulangue";
                    cmd.Parameters.Add(new SqlParameter("idNiveaulangue", idNiveaulangue));

                    cmd.ExecuteNonQuery();


                }
            }

        }

        //Supprimer Technologie
        public static void supprimerTechnologie(int IdTechnologie) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"update technologie set actif = 0 where id = @IdTechnologie";
                    cmd.Parameters.Add(new SqlParameter("IdTechnologie", IdTechnologie));

                    cmd.ExecuteNonQuery();
                }
            }

        }

        //Supprimer type stage
        public static void supprimerTypeStage(int idTypeStage) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"update typeStage set actif = 0 where id = @idTypeStage";
                    cmd.Parameters.Add(new SqlParameter("idTypeStage", idTypeStage));

                    cmd.ExecuteNonQuery();


                }
            }

        }

        //Supprimer type communication
        public static void supprimerTypeCommunication(int idTypeCommunication) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"update typeCommunication set actif = 0 where id = @idTypeCommunication";
                    cmd.Parameters.Add(new SqlParameter("idTypeCommunication", idTypeCommunication));

                    cmd.ExecuteNonQuery();


                }
            }

        }

        //Supprimer type entrevue
        public static void supprimerTypeEntrevue(int idTypeCommunication) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"update typeEntrevue set actif = 0 where id = @idTypeCommunication";
                    cmd.Parameters.Add(new SqlParameter("idTypeCommunication", idTypeCommunication));

                    cmd.ExecuteNonQuery();


                }
            }

        }

        //Supprimer type communication
        public static void supprimerTypeResultat(int idTypeCommunication) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"update resultat set actif = 0 where id = @idTypeCommunication";
                    cmd.Parameters.Add(new SqlParameter("idTypeCommunication", idTypeCommunication));

                    cmd.ExecuteNonQuery();


                }
            }

        }

        //Supprimer type communication
        public static void supprimerTypeDocument(int idTypeCommunication) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"update typeDocument set actif = 0 where id = @idTypeCommunication";
                    cmd.Parameters.Add(new SqlParameter("idTypeCommunication", idTypeCommunication));

                    cmd.ExecuteNonQuery();


                }
            }

        }


        //Supprimer status communication
        public static void supprimerStatusCommunication(int idStatusCommunication) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"update statusCommunication set actif = 0 where id = @idStatusCommunication";
                    cmd.Parameters.Add(new SqlParameter("idStatusCommunication", idStatusCommunication));

                    cmd.ExecuteNonQuery();


                }
            }

        }


        //Ajouter type utilisateur
        public static void supprimerTypeUtilisateur(int idTypeUtilisateur) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"update typeUtilisateur set actif = 0 where id = @idTypeUtilisateur";
                    cmd.Parameters.Add(new SqlParameter("idTypeUtilisateur", idTypeUtilisateur));

                    cmd.ExecuteNonQuery();


                }
            }

        }

        //Supprimer Langue
        public static void supprimerLangue(int idLangue) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"update langue set actif = 0 where id = @idLangue";
                    cmd.Parameters.Add(new SqlParameter("idLangue", idLangue));

                    cmd.ExecuteNonQuery();

                }
            }

        }

        //Supprimer formation
        public static void supprimerFormation(int idFormation) {

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"update formation set actif = 0 where id = @idFormation";
                    cmd.Parameters.Add(new SqlParameter("idFormation", idFormation));

                    cmd.ExecuteNonQuery();


                }
            }

        }


    }
}
