using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL
{
    public static class RequeteEntrevue
    {
        public static string ConnectionString = DefinitionConnection.ConnectionStringCommun;

        public static List<Entrevue> recupererEntrevuesParIdEtudiant(int idEtudiant)
        {
            List<Entrevue> listeEntrevues = null;

            string requete = @"select * from entrevue where idEtudiant=@idEtudiant and actif=1";
            SqlParameter idEtudiantParam = new SqlParameter("@idEtudiant", idEtudiant);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEtudiantParam);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            listeEntrevues = new List<Entrevue>();
                            while (rdr.Read())
                            {
                                Entrevue entrevue = new Entrevue();
                                entrevue.Modification = new Modification();
                                entrevue.Id = (int)rdr["id"];
                                entrevue.IdEtudiant = (int)rdr["idEtudiant"];
                                entrevue.IdEntreprise = (int)rdr["idEntreprise"];

                                if (!(rdr["idTypeEntrevue"] == DBNull.Value))
                                    entrevue.TypeEntrevue = (int)rdr["idTypeEntrevue"];
                                else
                                    entrevue.TypeEntrevue = null;

                                if (!(rdr["idResultat"] == DBNull.Value))
                                    entrevue.Resultat = (int)rdr["idResultat"];
                                else
                                    entrevue.Resultat = null;

                                entrevue.DateEntrevue = Convert.ToDateTime(rdr["dateEntrevue"]);

                                if (!(rdr["commentaire"] == DBNull.Value))
                                    entrevue.Commentaire = rdr["commentaire"].ToString();

                                entrevue.Actif = (bool)rdr["actif"];

                                entrevue.Modification.UtilisateurId = (int)rdr["idUtilisateur"];
                                entrevue.Modification.DateModification = Convert.ToDateTime(rdr["dateModification"]);
                                //ajouter l'entrevue a la liste d'entrevues d'un etudiant
                                listeEntrevues.Add(entrevue);
                            }
                        }
                    }
                }
            }
            return listeEntrevues;
        }

        public static List<Entrevue> recupererEntrevuesParIdEntreprise(int idEntreprise)
        {
            List<Entrevue> listeEntrevuesEntreprise = null;

            string requete = @"SELECT * FROM entrevue WHERE idEntreprise=@idEntreprise AND actif=1";
            SqlParameter idEntrepriseParam = new SqlParameter("@idEntreprise", idEntreprise);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEntrepriseParam);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            listeEntrevuesEntreprise = new List<Entrevue>();
                            while (rdr.Read())
                            {
                                Entrevue entrevue = new Entrevue();
                                entrevue.Modification = new Modification();
                                entrevue.Id = (int)rdr["id"];
                                entrevue.IdEtudiant = (int)rdr["idEtudiant"];
                                entrevue.IdEntreprise = (int)rdr["idEntreprise"];

                                if (!(rdr["idTypeEntrevue"] == DBNull.Value))
                                    entrevue.TypeEntrevue = (int)rdr["idTypeEntrevue"];
                                else
                                    entrevue.TypeEntrevue = null;

                                if (!(rdr["idResultat"] == DBNull.Value))
                                    entrevue.Resultat = (int)rdr["idResultat"];
                                else
                                    entrevue.Resultat = null;

                                entrevue.DateEntrevue = Convert.ToDateTime(rdr["dateEntrevue"]);

                                if (!(rdr["commentaire"] == DBNull.Value))
                                    entrevue.Commentaire = rdr["commentaire"].ToString();

                                entrevue.Actif = (bool)rdr["actif"];

                                entrevue.Modification.UtilisateurId = (int)rdr["idUtilisateur"];
                                entrevue.Modification.DateModification = Convert.ToDateTime(rdr["dateModification"]);

                                listeEntrevuesEntreprise.Add(entrevue);
                            }
                        }
                    }
                }
            }
            return listeEntrevuesEntreprise;
        }

        public static bool ajouterEntrevue(Entrevue entrevue)
        {
            bool cree = false;
            string requete = @" INSERT INTO entrevue (idEtudiant, idEntreprise, idTypeEntrevue, idResultat, dateEntrevue, commentaire, idUtilisateur) OUTPUT INSERTED.id VALUES (@idEtudiant, @idEntreprise, @idTypeEntrevue, @idResultat, @dateEntrevue, @commentaire, @idUtilisateur)";

            SqlParameter idEtudiant = new SqlParameter("@idEtudiant", entrevue.IdEtudiant);
            SqlParameter idEntreprise = new SqlParameter("@idEntreprise", entrevue.IdEntreprise);
            SqlParameter idTypeEntrevue = new SqlParameter("@idTypeEntrevue", entrevue.TypeEntrevue != null ? entrevue.TypeEntrevue : (object)DBNull.Value);
            SqlParameter idResultat = new SqlParameter("@idResultat", entrevue.Resultat != null ? entrevue.Resultat : (object)DBNull.Value);
            SqlParameter dateEntrevue = new SqlParameter("@dateEntrevue", entrevue.DateEntrevue);
            SqlParameter commentaire = new SqlParameter("@commentaire", !string.IsNullOrEmpty(entrevue.Commentaire) ? entrevue.Commentaire : (object)DBNull.Value);
            SqlParameter idUtilisateur = new SqlParameter("@idUtilisateur", entrevue.Modification.UtilisateurId);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEtudiant);
                    cmd.Parameters.Add(idEntreprise);
                    cmd.Parameters.Add(idTypeEntrevue);
                    cmd.Parameters.Add(idResultat);
                    cmd.Parameters.Add(dateEntrevue);
                    cmd.Parameters.Add(commentaire);
                    cmd.Parameters.Add(idUtilisateur);

                    entrevue.Id = (Int32)cmd.ExecuteScalar();
                    if (entrevue.Id != 0)
                        cree = true;
                }
            }
            return cree;
        }

        public static Entrevue recupererEntrevueParId(int idEntrevue)
        {
            Entrevue entrevue = null;
            SqlParameter idEntrevueParam = new SqlParameter("@id", idEntrevue);
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Parameters.Add(idEntrevueParam);
                    cmd.CommandText = @"SELECT * FROM entrevue WHERE id=@id AND actif=1";

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            entrevue = new Entrevue();
                            entrevue.Modification = new Modification();
                            entrevue.Id = (int)rdr["id"];
                            entrevue.IdEtudiant = (int)rdr["idEtudiant"];
                            entrevue.IdEntreprise = (int)rdr["idEntreprise"];

                            if (!(rdr["idTypeEntrevue"] == DBNull.Value))
                                entrevue.TypeEntrevue = (int)rdr["idTypeEntrevue"];
                            else
                                entrevue.TypeEntrevue = null;

                            if (!(rdr["idResultat"] == DBNull.Value))
                                entrevue.Resultat = (int)rdr["idResultat"];
                            else
                                entrevue.Resultat = null;

                            entrevue.DateEntrevue = Convert.ToDateTime(rdr["dateEntrevue"]);

                            if (!(rdr["commentaire"] == DBNull.Value))
                                entrevue.Commentaire = rdr["commentaire"].ToString();

                            entrevue.Actif = (bool)rdr["actif"];
                            entrevue.Modification.UtilisateurId = (int)rdr["idUtilisateur"];
                            entrevue.Modification.DateModification = Convert.ToDateTime(rdr["dateModification"]);
                        }
                    }
                }
            }
            return entrevue;
        }

        public static bool modifierEntrevue(Entrevue entrevue)
        {
            bool modifie = false;
            string requete = @"UPDATE entrevue SET idEtudiant=@idEtudiant, idEntreprise=@idEntreprise, idTypeEntrevue=@idTypeEntrevue, idResultat=@idResultat, dateEntrevue=@dateEntrevue, commentaire=@commentaire, idUtilisateur=@idUtilisateur WHERE id=@id";

            SqlParameter idParam = new SqlParameter("@id", entrevue.Id);
            SqlParameter idEtudiant = new SqlParameter("@idEtudiant", entrevue.IdEtudiant);
            SqlParameter idEntreprise = new SqlParameter("@idEntreprise", entrevue.IdEntreprise);
            SqlParameter idTypeEntrevue = new SqlParameter("@idTypeEntrevue", entrevue.TypeEntrevue != null ? entrevue.TypeEntrevue : (object)DBNull.Value);
            SqlParameter idResultat = new SqlParameter("@idResultat", entrevue.Resultat != null ? entrevue.Resultat : (object)DBNull.Value);
            SqlParameter dateEntrevue = new SqlParameter("@dateEntrevue", entrevue.DateEntrevue);
            SqlParameter commentaire = new SqlParameter("@commentaire", !string.IsNullOrEmpty(entrevue.Commentaire) ? entrevue.Commentaire : (object)DBNull.Value);
            SqlParameter idUtilisateur = new SqlParameter("@idUtilisateur", entrevue.Modification.UtilisateurId);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idParam);
                    cmd.Parameters.Add(idEtudiant);
                    cmd.Parameters.Add(idEntreprise);
                    cmd.Parameters.Add(idTypeEntrevue);
                    cmd.Parameters.Add(idResultat);
                    cmd.Parameters.Add(dateEntrevue);
                    cmd.Parameters.Add(commentaire);
                    cmd.Parameters.Add(idUtilisateur);

                    int affectes = (int)cmd.ExecuteNonQuery();
                    if (affectes != 0)
                        modifie = true;
                }
            }
            return modifie;
        }

        public static bool supprimerEntrevue(Entrevue entrevue)
        {
            bool modifie = false;
            string requete = @"UPDATE entrevue SET actif=0 WHERE id=@id";
            SqlParameter id = new SqlParameter("@id", entrevue.Id);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
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

        public static bool supprimerEntrevueParId(int idEntrevue)
        {
            bool modifie = false;
            string requete = @"UPDATE entrevue SET actif=0 WHERE id=@id";
            SqlParameter id = new SqlParameter("@id", idEntrevue);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
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
    }
}
