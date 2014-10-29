using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class RequeteUtilisateur
    {
        public static string ConnectionString = DefinitionConnection.ConnectionStringCommun;


        //Recuperer utilisateur connecte
        public static Utilisateur recupererUtilisateurConnecte(string email, string password)
        {
            Utilisateur utilisateur = null;
            // inserer le nouveau contact
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    
                    cmd.CommandText = @"select * from utilisateur where utilisateur = @utilisateur and motDePasse = @motDePasse";
                    cmd.Parameters.Add(new SqlParameter("utilisateur", email));
                    cmd.Parameters.Add(new SqlParameter("motDePasse", password));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        
                        while (reader.Read())
                        {

                            utilisateur = new Utilisateur();
                            utilisateur.modification = new Modification();
                            Object objetUtilisateur;
                            Object objetMotDePass;
                            Object ObjetIdTypeUtilisateur;
                            Object objetPeutEcrire;
                            Object objetPeutLire;
                            Object objetPeutCreerUtlisateur;
                           
                           
                            

                           

                            utilisateur.Id = (int)reader["id"];
                            objetUtilisateur = reader["utilisateur"];
                            utilisateur.Nom = (objetUtilisateur == DBNull.Value) ? null : (string)objetUtilisateur;
                            objetMotDePass = reader["motDePasse"];
                            utilisateur.MotDePasse = (objetMotDePass == DBNull.Value) ? null : (string)objetMotDePass;
                            ObjetIdTypeUtilisateur = reader["idTypeUtilisateur"];
                            utilisateur.IdTypeUtilisateur = (ObjetIdTypeUtilisateur == DBNull.Value) ? null : (int?)ObjetIdTypeUtilisateur;
                            objetPeutEcrire = reader["peutEcrire"];
                            utilisateur.PeutEcrire = (objetPeutEcrire == DBNull.Value) ? null : (bool?)objetPeutEcrire;
                            objetPeutLire = reader["peutLire"];
                            utilisateur.PeutLire = (objetPeutLire == DBNull.Value) ? null : (bool?)objetPeutLire;
                            objetPeutCreerUtlisateur = reader["peutCreerUtilisateur"];
                            utilisateur.PeutCreerUtilisateur = (objetPeutCreerUtlisateur == DBNull.Value) ? null : (bool?)objetPeutCreerUtlisateur;
                            utilisateur.modification.UtilisateurId = (int)reader["idUtilisateur"];
                            utilisateur.modification.DateModification = (DateTime)reader["dateModification"];



                        }
                    }







                }
            }
            return utilisateur;
        }

        //Recuperer utilisateur connecte
        public static Utilisateur recupererUtilisateurParId(int idUser)
        {
            Utilisateur utilisateur = null;
            // inserer le nouveau contact
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()){


                    cmd.CommandText = @"select * from utilisateur where id = @id";
                    cmd.Parameters.Add(new SqlParameter("id", idUser));                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {

                            utilisateur = new Utilisateur();
                            utilisateur.modification = new Modification();
                            Object objetUtilisateur;
                            Object objetMotDePass;
                            Object ObjetIdTypeUtilisateur;
                            Object objetPeutEcrire;
                            Object objetPeutLire;
                            Object objetPeutCreerUtlisateur;


                            utilisateur.Id = (int)reader["id"];
                            objetUtilisateur = reader["utilisateur"];
                            utilisateur.Nom = (objetUtilisateur == DBNull.Value) ? null : (string)objetUtilisateur;
                            objetMotDePass = reader["motDePasse"];
                            utilisateur.MotDePasse = (objetMotDePass == DBNull.Value) ? null : (string)objetMotDePass;
                            ObjetIdTypeUtilisateur = reader["idTypeUtilisateur"];
                            utilisateur.IdTypeUtilisateur = (ObjetIdTypeUtilisateur == DBNull.Value) ? null : (int?)ObjetIdTypeUtilisateur;
                            objetPeutEcrire = reader["peutEcrire"];
                            utilisateur.PeutEcrire = (objetPeutEcrire == DBNull.Value) ? null : (bool?)objetPeutEcrire;
                            objetPeutLire = reader["peutLire"];
                            utilisateur.PeutLire = (objetPeutLire == DBNull.Value) ? null : (bool?)objetPeutLire;
                            objetPeutCreerUtlisateur = reader["peutCreerUtilisateur"];
                            utilisateur.PeutCreerUtilisateur = (objetPeutCreerUtlisateur == DBNull.Value) ? null : (bool?)objetPeutCreerUtlisateur;
                            utilisateur.modification.UtilisateurId = (int)reader["idUtilisateur"];
                            utilisateur.modification.DateModification = (DateTime)reader["dateModification"];
                        }
                    }
                }
            }
            return utilisateur;
        }


        //ajouter Utilisateur
        public static void ajouterUtilisateur(Utilisateur utilisateur)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"INSERT INTO utilisateur (utilisateur, motDePasse, idTypeUtilisateur,idUtilisateur, peutLire, peutEcrire, peutCreerUtilisateur, dateModification) 
                                          VALUES (@utilisateur, @motDePasse, @idTypeUtilisateur, @idUtilisateur, @peutLire, @peutEcrire, @peutCreerUtilisateur, @dateModification);";



                    cmd.Parameters.Add(new SqlParameter("utilisateur", string.IsNullOrEmpty(utilisateur.Nom) ? (object)DBNull.Value : utilisateur.Nom));
                    cmd.Parameters.Add(new SqlParameter("motDePasse", string.IsNullOrEmpty(utilisateur.MotDePasse) ? (object)DBNull.Value : utilisateur.MotDePasse));
                    cmd.Parameters.Add(new SqlParameter("idTypeUtilisateur", utilisateur.IdTypeUtilisateur.HasValue ? utilisateur.IdTypeUtilisateur : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("idUtilisateur", utilisateur.modification.UtilisateurId));
                    cmd.Parameters.Add(new SqlParameter("peutLire", utilisateur.PeutLire.HasValue ? utilisateur.PeutLire : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("peutEcrire", utilisateur.PeutEcrire.HasValue ? utilisateur.PeutEcrire : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("peutCreerUtilisateur", utilisateur.PeutCreerUtilisateur.HasValue ? utilisateur.PeutCreerUtilisateur : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("dateModification", utilisateur.modification.DateModification));


                    cmd.ExecuteNonQuery();


                }
            }

        }

        //modifier Utilisateur
        public static void modifierUtilisateur(Utilisateur utilisateur)
        {


            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"update utilisateur set utilisateur = @utilisateur, motDePasse = @motDePasse, idTypeUtilisateur = @idTypeUtilisateur, idUtilisateur = @idUtilisateur,
                                        peutLire = @peutLire, peutEcrire = @peutEcrire, peutCreerUtilisateur = @peutCreerUtilisateur, dateModification = @dateModification
                                            where id = @id";

                    cmd.Parameters.Add(new SqlParameter("id", utilisateur.Id));
                    cmd.Parameters.Add(new SqlParameter("utilisateur", string.IsNullOrEmpty(utilisateur.Nom) ? (object)DBNull.Value : utilisateur.Nom));
                    cmd.Parameters.Add(new SqlParameter("motDePasse", string.IsNullOrEmpty(utilisateur.MotDePasse) ? (object)DBNull.Value : utilisateur.MotDePasse));
                    cmd.Parameters.Add(new SqlParameter("idTypeUtilisateur", utilisateur.IdTypeUtilisateur.HasValue ? utilisateur.IdTypeUtilisateur : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("idUtilisateur", utilisateur.modification.UtilisateurId));
                    cmd.Parameters.Add(new SqlParameter("peutLire", utilisateur.PeutLire.HasValue ? utilisateur.PeutLire : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("peutEcrire", utilisateur.PeutEcrire.HasValue ? utilisateur.PeutEcrire : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("peutCreerUtilisateur", utilisateur.PeutCreerUtilisateur.HasValue ? utilisateur.PeutCreerUtilisateur : (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("dateModification", utilisateur.modification.DateModification));

                    cmd.ExecuteNonQuery();

                }
            }

        }

        //supprimer Utilisateur
        public static void supprimerUtilisateur(int idUtilisateur)
        {


            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"update utilisateur set actif = 0 where id = @idUtilisateur";
                    cmd.Parameters.Add(new SqlParameter("idUtilisateur", idUtilisateur));
                    cmd.ExecuteNonQuery();
                }
            }

        }

        //Lister utilisateur
        public static List<Utilisateur> recupererListUtilisateur()
        {

            List<Utilisateur> utilisateurs = new List<Utilisateur>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {

                
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {


                    cmd.CommandText = @"select * from utilisateur where actif = 1";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            Object objetUtilisateur;
                            Object objetMotDePass;
                            Object ObjetIdTypeUtilisateur;
                            Object objetPeutEcrire;
                            Object objetPeutLire;
                            Object objetPeutCreerUtlisateur;


                            Utilisateur utilisateur = new Utilisateur();
                            utilisateur.modification = new Modification();
                          
                            utilisateur.Id = (int)reader["id"];
                            objetUtilisateur = reader["utilisateur"];
                            utilisateur.Nom = (objetUtilisateur == DBNull.Value) ? null : (string)objetUtilisateur;
                            objetMotDePass = reader["motDePasse"];
                            utilisateur.MotDePasse = (objetMotDePass == DBNull.Value) ? null : (string)objetMotDePass;
                            ObjetIdTypeUtilisateur = reader["idTypeUtilisateur"];
                            utilisateur.IdTypeUtilisateur = (ObjetIdTypeUtilisateur == DBNull.Value) ? null : (int?)ObjetIdTypeUtilisateur;
                            objetPeutEcrire = reader["peutEcrire"];
                            utilisateur.PeutEcrire = (objetPeutEcrire == DBNull.Value) ? null : (bool?)objetPeutEcrire;
                            objetPeutLire = reader["peutLire"];
                            utilisateur.PeutLire = (objetPeutLire == DBNull.Value) ? null : (bool?)objetPeutLire;
                            objetPeutCreerUtlisateur = reader["peutCreerUtilisateur"];
                            utilisateur.PeutCreerUtilisateur = (objetPeutCreerUtlisateur == DBNull.Value) ? null : (bool?)objetPeutCreerUtlisateur;
                           utilisateur.modification.UtilisateurId = (int)reader["idUtilisateur"];

                            utilisateur.modification.DateModification = (DateTime)reader["dateModification"];
                            

                            utilisateurs.Add(utilisateur);
                        }
                    }




                }
            }
            return utilisateurs;

        }

    }
}
