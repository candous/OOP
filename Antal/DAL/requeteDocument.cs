using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    static public class RequeteDocument
    {

        public static string ConnectionString = DefinitionConnection.ConnectionStringCommun;
        //Recuperer documents d'un etudiant
        public static List<Document> recupererDocumentEtudiant(int idEtudiant)
        {
            List<Document> documents = new List<Document>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    //Requette
                    cmd.CommandText = @"select * from documentEtudiant where idEtudiant  = @idEtudiant";
                    cmd.Parameters.Add(new SqlParameter("idEtudiant", idEtudiant));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                           // Object objetIdEtudiant;
                            Object objetIdTypeDocument;
                            Object ObjetDateAjout;
                            Object objetCheminURL;
                            Object objetTitre;
                            //Object objetDateModification;
                            //Object objetIdUtilisateur;

                            Document document = new Document();
                            document.Modification = new Modification();

                            document.Id = (int)reader["id"];
                            document.IdProprietaire = (int)reader["idEtudiant"];
                            objetIdTypeDocument = reader["idTypeDocument"];
                            document.IdTypeDocument = (objetIdTypeDocument == DBNull.Value) ? null : (int?)objetIdTypeDocument;
                            ObjetDateAjout = reader["dateAjout"];
                            document.DateAjout = (ObjetDateAjout == DBNull.Value) ? null : (DateTime?)ObjetDateAjout;
                            objetCheminURL = reader["cheminURL"];
                            document.CheminURL = (objetCheminURL == DBNull.Value) ? null : (string)objetCheminURL;
                            objetTitre = reader["titre"];
                            document.Titre = (objetTitre == DBNull.Value) ? null : (string)objetTitre;
                            document.Modification.DateModification = (DateTime)reader["dateModification"];

                            document.Modification.UtilisateurId = (int)reader["idUtilisateur"];



                            documents.Add(document);
                        }
                    }



                }
            }
            return documents;

        }

        //Recuperer documents entreprise
        public static List<Document> recupererDocumentEntreprise(int idEntreprise)
        {
            List<Document> documents = new List<Document>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    //Requette
                    cmd.CommandText = @"select * from documentEntreprise where idEntreprise  = @idEntreprise";
                    cmd.Parameters.Add(new SqlParameter("idEntreprise", idEntreprise));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                          //  Object objetIdEntreprise;
                            Object objetIdTypeDocument;
                            Object ObjetDateAjout;
                            Object objetCheminURL;
                            Object objetTitre;
                            Object objetDateModification;
                          //  Object objetIdUtilisateur;

                            Document document = new Document();
                            document.Modification = new Modification();
                            document.Id = (int)reader["id"];
                            document.IdProprietaire = (int)reader["idEntreprise"];

                            objetIdTypeDocument = reader["idTypeDocument"];
                            document.IdTypeDocument = (objetIdTypeDocument == DBNull.Value) ? null : (int?)objetIdTypeDocument;
                            ObjetDateAjout = reader["dateAjout"];
                            document.DateAjout = (ObjetDateAjout == DBNull.Value) ? null : (DateTime?)ObjetDateAjout;
                            objetCheminURL = reader["cheminURL"];
                            document.CheminURL = (objetCheminURL == DBNull.Value) ? null : (string)objetCheminURL;
                            objetTitre = reader["titre"];
                            document.Titre = (objetTitre == DBNull.Value) ? null : (string)objetTitre;
                            objetDateModification = reader["dateModification"];
                            document.Modification.DateModification = (DateTime)reader["dateModification"];

                            document.Modification.UtilisateurId = (int)reader["idUtilisateur"];


                            documents.Add(document);
                        }
                    }


                }
            }
            return documents;

        }

        //Recuperer document d'un etudiant au cour d'un stage
        public static List<Document> recupererDocumentEtudiantStage(int idEtudiant)
        {
            List<Document> documents = new List<Document>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    //Requette
                    cmd.CommandText = @"";


                    cmd.ExecuteNonQuery();


                }
            }
            return documents;

        }

        //Ajouter document d'un etudiant
        public static void ajouterDocumentEtudiant(Document document)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    //Requette
                    cmd.CommandText = @"insert into documentEtudiant(idEtudiant, idTypeDocument, dateAjout, cheminURL, titre, dateModification, idUtilisateur) 
                                            values(@idEtudiant, @idTypeDocument, @dateAjout, @cheminURL, @titre, @dateModification, @idUtilisateur)";

                    cmd.Parameters.Add(new SqlParameter("idEtudiant", document.IdProprietaire));
                    cmd.Parameters.Add(new SqlParameter("idTypeDocument", document.IdTypeDocument.HasValue ? document.IdTypeDocument : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("dateAjout", document.DateAjout.HasValue ? document.DateAjout : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("cheminURL", string.IsNullOrEmpty(document.CheminURL) ? (object)DBNull.Value : document.CheminURL));
                    cmd.Parameters.Add(new SqlParameter("titre", string.IsNullOrEmpty(document.Titre) ? (object)DBNull.Value : document.Titre));
                    cmd.Parameters.Add(new SqlParameter("dateModification", document.Modification.DateModification));
                    cmd.Parameters.Add(new SqlParameter("idUtilisateur", document.Modification.UtilisateurId));

                    cmd.ExecuteNonQuery();
                }
            }


        }

        //Ajouter document d'un entreprise
        public static void ajouterDocumentEntreprise(Document document)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    //Requette
                    cmd.CommandText = @"INSERT INTO documentEntreprise(idEntreprise, idTypeDocument, dateAjout, cheminURL, titre, dateModification, idUtilisateur) 
                                            VALUES(@idEntreprise, @idTypeDocument, @dateAjout, @cheminURL, @titre, @dateModification, @idUtilisateur)";

                    cmd.Parameters.Add(new SqlParameter("idEntreprise", document.IdProprietaire));
                    cmd.Parameters.Add(new SqlParameter("idTypeDocument", document.IdTypeDocument.HasValue ? document.IdTypeDocument : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("dateAjout", document.DateAjout.HasValue ? document.DateAjout : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("cheminURL", string.IsNullOrEmpty(document.CheminURL) ? (object)DBNull.Value : document.CheminURL));
                    cmd.Parameters.Add(new SqlParameter("titre", string.IsNullOrEmpty(document.Titre) ? (object)DBNull.Value : document.Titre));
                    cmd.Parameters.Add(new SqlParameter("dateModification", document.Modification.DateModification));
                    cmd.Parameters.Add(new SqlParameter("idUtilisateur", document.Modification.UtilisateurId));

                    cmd.ExecuteNonQuery();
                }
            }


        }


        //Supprimer document d'un entreprise
        public static void supprimerDocumentEntreprise(int idDocument)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    //Requette
                    cmd.CommandText = @"delete documentEntreprise where id = @idDocument";
                    cmd.Parameters.Add(new SqlParameter("idDocument", idDocument));



                    cmd.ExecuteNonQuery();
                }
            }


        }

        //Supprimer document d'un etudiant
        public static void supprimerDocumentEtudiant(int idDocument)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"delete documentEtudiant where id = @idDocument";
                    cmd.Parameters.Add(new SqlParameter("idDocument", idDocument));

                    cmd.ExecuteNonQuery();


                }
            }


        }

        //Modifier document Etudiant
        public static void modifierDocumentEtudiant(Document document)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"update documentEtudiant set idEtudiant = @idEtudiant, idTypeDocument = @idTypeDocument, dateAjout = @dateAjout, cheminURL = @cheminURL,
                                           titre = @titre, dateModification = @dateModification, idUtilisateur = @idUtilisateur
                                                where id = @id";


                    cmd.Parameters.Add(new SqlParameter("id", document.Id));
                    cmd.Parameters.Add(new SqlParameter("idEtudiant", document.IdProprietaire));
                    cmd.Parameters.Add(new SqlParameter("idTypeDocument", document.IdTypeDocument.HasValue ? document.IdTypeDocument : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("dateAjout", document.DateAjout.HasValue ? document.DateAjout : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("cheminURL", string.IsNullOrEmpty(document.CheminURL) ? (object)DBNull.Value : document.CheminURL));
                    cmd.Parameters.Add(new SqlParameter("titre", string.IsNullOrEmpty(document.Titre) ? (object)DBNull.Value : document.Titre));
                    cmd.Parameters.Add(new SqlParameter("dateModification", document.Modification.DateModification));
                    cmd.Parameters.Add(new SqlParameter("idUtilisateur", document.Modification.UtilisateurId));

                    cmd.ExecuteNonQuery();


                }
            }


        }

        //Modifier document Entreprise
        public static void modifierDocumentEntreprise(Document document)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"update documentEntreprise set idEntreprise = @idEntreprise, idTypeDocument = @idTypeDocument, dateAjout = @dateAjout, cheminURL = @cheminURL,
                                           titre = @titre, dateModification = @dateModification, idUtilisateur = @idUtilisateur
                                                where id = @id";


                    cmd.Parameters.Add(new SqlParameter("id", document.Id));
                    cmd.Parameters.Add(new SqlParameter("idEntreprise", document.IdProprietaire));
                    cmd.Parameters.Add(new SqlParameter("idTypeDocument", document.IdTypeDocument.HasValue ? document.IdTypeDocument : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("dateAjout", document.DateAjout.HasValue ? document.DateAjout : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("cheminURL", string.IsNullOrEmpty(document.CheminURL) ? (object)DBNull.Value : document.CheminURL));
                    cmd.Parameters.Add(new SqlParameter("titre", string.IsNullOrEmpty(document.Titre) ? (object)DBNull.Value : document.Titre));
                    cmd.Parameters.Add(new SqlParameter("dateModification", document.Modification.DateModification));
                    cmd.Parameters.Add(new SqlParameter("idUtilisateur", document.Modification.UtilisateurId));

                    cmd.ExecuteNonQuery();

                }
            }


        }


    }



}
