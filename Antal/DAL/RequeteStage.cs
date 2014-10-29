using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Data;

namespace DAL
{

    static public class RequeteStage
    {
        public static string ConnectionString = DefinitionConnection.ConnectionStringCommun;

        //Recuperer tous les stages
        public static List<Stage> recupererListStage()
        {
            List<Stage> listStage = null;
             using (SqlConnection conn = new SqlConnection(ConnectionString))
             {

                 conn.Open();
                 using (SqlCommand cmd = conn.CreateCommand())
                 {
                     //Requette
                     cmd.CommandText = @"select*from stage where actif=1";

                     using (SqlDataReader rdr = cmd.ExecuteReader())
                     {
                         if (rdr.HasRows)
                         {
                             listStage = new List<Stage>();
                             while (rdr.Read())
                             {
                                 Stage stage = new Stage();
                                 stage.Modification = new Modification();
                                 stage.Id = rdr.GetInt32(rdr.GetOrdinal("id"));
                                 stage.IdEntreprise = rdr.GetInt32(rdr.GetOrdinal("idEntreprise"));
                                 stage.IdEtudiant= rdr.GetInt32(rdr.GetOrdinal("idEtudiant"));
                                 stage.DatePlacement=Convert.ToDateTime(rdr["datePlacement"]);

                                 if (!(rdr["dateDebut"] == DBNull.Value))
                                     stage.DateDebut = Convert.ToDateTime(rdr["dateDebut"]);

                                 if (!(rdr["dateFin"] == DBNull.Value))
                                     stage.DateFin = Convert.ToDateTime(rdr["dateFin"]);
                          
                                 if (!(rdr["commentaire"] == DBNull.Value))
                                     stage.Commentaire = rdr["commentaire"].ToString();

                                  if (!rdr.IsDBNull(rdr.GetOrdinal("idTypeStage")))
                                  stage.TypeStage = rdr.GetInt32(rdr.GetOrdinal("idTypeStage"));

                                 //??????????????????????? ca marche?
                                 if (rdr["salaire"]!=DBNull.Value)
                                     stage.Salaire = Double.Parse(rdr["salaire"].ToString(), NumberStyles.Currency);

                                 if(rdr["retenu"]==DBNull.Value)
                                     stage.Retenu=null;
                                 else 
                                     stage.Retenu=(bool)rdr["retenu"];

                                 stage.Actif=(bool)rdr["actif"];
                                 stage.Modification.DateModification=Convert.ToDateTime(rdr["dateModification"]);
                                 stage.Modification.UtilisateurId = rdr.GetInt32(rdr.GetOrdinal("idUtilisateur"));

                                 listStage.Add(stage);
                             }
                         }
                     }
                 }
             }
             return listStage;
        }

        //Recuperer le stage par id
        public static Stage recupererStageParId(int idStage) {
            Stage stage = null;
            SqlParameter idStageParam = new SqlParameter("@id", idStage);
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.Parameters.Add(idStageParam);
                    cmd.CommandText = @"SELECT * FROM stage WHERE id=@id AND actif=1";

                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        while(rdr.Read()) {
                            stage = new Stage();
                            stage.Modification = new Modification();
                            stage.Id = rdr.GetInt32(rdr.GetOrdinal("id"));
                            stage.IdEntreprise = rdr.GetInt32(rdr.GetOrdinal("idEntreprise"));
                            stage.IdEtudiant = rdr.GetInt32(rdr.GetOrdinal("idEtudiant"));
                            stage.DatePlacement = Convert.ToDateTime(rdr["datePlacement"]);
                            if (!(rdr["dateDebut"] == DBNull.Value))
                                stage.DateDebut = Convert.ToDateTime(rdr["dateDebut"]);

                            if (!(rdr["dateFin"] == DBNull.Value))
                                stage.DateFin = Convert.ToDateTime(rdr["dateFin"]);

                            if(!(rdr["commentaire"] == DBNull.Value))
                                stage.Commentaire = rdr["commentaire"].ToString();

                            if(!rdr.IsDBNull(rdr.GetOrdinal("idTypeStage")))
                                stage.TypeStage = rdr.GetInt32(rdr.GetOrdinal("idTypeStage"));

                            if(rdr["salaire"] != DBNull.Value)
                                stage.Salaire = Double.Parse(rdr["salaire"].ToString(), NumberStyles.Currency);

                            if(rdr["retenu"] == DBNull.Value)
                                stage.Retenu = null;
                            else
                                stage.Retenu = (bool)rdr["retenu"];

                            stage.Actif = (bool)rdr["actif"];
                            stage.Modification.DateModification = Convert.ToDateTime(rdr["dateModification"]);
                            stage.Modification.UtilisateurId = rdr.GetInt32(rdr.GetOrdinal("idUtilisateur"));
                        }
                    }
                }
            }
            return stage;
        }

        //Ajouter stage
        public static bool ajouterStage(Stage stage)
        {
                    bool cree = false;
                    string requete = @"INSERT INTO stage (idEtudiant, idEntreprise, datePlacement, dateDebut, dateFin, commentaire, idTypeStage, salaire, retenu, idUtilisateur) OUTPUT INSERTED.id VALUES (@idEtudiant, @idEntreprise, @datePlacement, @dateDebut, @dateFin, @commentaire, @idTypeStage, @salaire, @retenu, @idUtilisateur)";

                    SqlParameter idEtudiant = new SqlParameter("@idEtudiant", stage.IdEtudiant);
                    SqlParameter idEntreprise = new SqlParameter("@idEntreprise", stage.IdEntreprise);
                    SqlParameter datePlacement = new SqlParameter("@datePlacement", stage.DatePlacement);
                    SqlParameter dateDebut = new SqlParameter("@dateDebut", stage.DateDebut != null ? stage.DateDebut : (object)DBNull.Value);
                    SqlParameter dateFin = new SqlParameter("@dateFin", stage.DateFin != null ? stage.DateFin : (object)DBNull.Value);
                    SqlParameter commentaire = new SqlParameter("@commentaire", !string.IsNullOrEmpty(stage.Commentaire) ? stage.Commentaire : (object)DBNull.Value);
                    SqlParameter idTypeStage = new SqlParameter("@idTypeStage", stage.TypeStage != null ? stage.TypeStage : (object)DBNull.Value);
                    SqlParameter salaire = new SqlParameter("@salaire", stage.Salaire != null ? stage.Salaire : (object)DBNull.Value);
                    SqlParameter retenu = new SqlParameter("@retenu", stage.Retenu!=null ? stage.Retenu : (object)DBNull.Value);
                    SqlParameter idUtilisateur = new SqlParameter("@idUtilisateur", stage.Modification.UtilisateurId);

                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(requete, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(idEtudiant);
                            cmd.Parameters.Add(idEntreprise);
                            cmd.Parameters.Add(datePlacement);
                            cmd.Parameters.Add(dateDebut);
                            cmd.Parameters.Add(dateFin);
                            cmd.Parameters.Add(commentaire);
                            cmd.Parameters.Add(idTypeStage);
                            cmd.Parameters.Add(salaire);
                            cmd.Parameters.Add(retenu);
                            cmd.Parameters.Add(idUtilisateur);

                            stage.Id = (Int32)cmd.ExecuteScalar();
                            if (stage.Id > 0 )
                                cree = true;
                        }
                    }
                    return cree;
        }

        //Supprimer stage
        public static bool supprimerStage(int idStage)
        {
            bool modifie = false;
            string requete = @"UPDATE stage set actif=0 where id=@id";
            SqlParameter id = new SqlParameter("@id", idStage);

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


        //Modifier stage
        public static bool modifierStage(Stage stage)
        {
            bool modifie = false;
            string requete = @"UPDATE stage set idEtudiant=@idEtudiant, idEntreprise=@idEntreprise, datePlacement=@datePlacement, dateDebut=@dateDebut, dateFin=@dateFin, commentaire=@commentaire, idTypeStage=@idTypeStage, salaire=@salaire, retenu=@retenu, idUtilisateur=@idUtilisateur where id=@id";
            SqlParameter id = new SqlParameter("@id", stage.Id);
            SqlParameter idEtudiant = new SqlParameter("@idEtudiant", stage.IdEtudiant);
            SqlParameter idEntreprise = new SqlParameter("@idEntreprise", stage.IdEntreprise);
            SqlParameter datePlacement = new SqlParameter("@datePlacement", stage.DatePlacement);
            SqlParameter dateDebut = new SqlParameter("@dateDebut", stage.DateDebut != null ? stage.DateDebut : (object)DBNull.Value);
            SqlParameter dateFin = new SqlParameter("@dateFin", stage.DateFin != null ? stage.DateFin : (object)DBNull.Value);
            SqlParameter commentaire = new SqlParameter("@commentaire", !string.IsNullOrEmpty(stage.Commentaire) ? stage.Commentaire : (object)DBNull.Value);
            SqlParameter idTypeStage = new SqlParameter("@idTypeStage", stage.TypeStage != null ? stage.TypeStage : (object)DBNull.Value);
            SqlParameter salaire = new SqlParameter("@salaire", stage.Salaire != null ? stage.Salaire : (object)DBNull.Value);
            SqlParameter retenu = new SqlParameter("@retenu", stage.Retenu != null ? stage.Retenu : (object)DBNull.Value);
            SqlParameter idUtilisateur = new SqlParameter("@idUtilisateur", stage.Modification.UtilisateurId);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(id);
                    cmd.Parameters.Add(idEtudiant);
                    cmd.Parameters.Add(idEntreprise);
                    cmd.Parameters.Add(datePlacement);
                    cmd.Parameters.Add(dateDebut);
                    cmd.Parameters.Add(dateFin);
                    cmd.Parameters.Add(commentaire);
                    cmd.Parameters.Add(idTypeStage);
                    cmd.Parameters.Add(salaire);
                    cmd.Parameters.Add(retenu);
                    cmd.Parameters.Add(idUtilisateur);

                    int affectes = (int)cmd.ExecuteNonQuery();
                    if (affectes != 0)
                        modifie = true;
                }
            }
            return modifie;
        }

        //recuperer liste de stages d' un etudiant
        public static List<Stage> recupererStageParIdEtudiant(int idEtudiant) {
            List<Stage> listeStages = null;
            string requete = @"SELECT * FROM stage WHERE actif=1 AND idEtudiant=@idEtudiant";
            SqlParameter idEtudiantParam = new SqlParameter("@idEtudiant", idEtudiant);
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEtudiantParam);
                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        if(rdr.HasRows) {
                            listeStages = new List<Stage>();
                            while(rdr.Read()) {
                                Stage stage = new Stage();
                                stage.Modification = new Modification();
                              
                                stage.Id = (int)rdr["id"];
                                stage.IdEtudiant = (int)rdr["idEtudiant"];
                                stage.IdEntreprise = (int)rdr["idEntreprise"];
                                stage.DatePlacement = Convert.ToDateTime(rdr["datePlacement"]);

                                if(!(rdr["dateDebut"] == DBNull.Value))
                                    stage.DateDebut = Convert.ToDateTime(rdr["dateDebut"]);
                                else
                                    stage.DateDebut = null;

                                if(!(rdr["dateFin"] == DBNull.Value))
                                    stage.DateFin = Convert.ToDateTime(rdr["dateFin"]);
                                else
                                    stage.DateFin = null;

                                if(!(rdr["commentaire"] == DBNull.Value))
                                    stage.Commentaire = rdr["commentaire"].ToString();

                                if(!(rdr["idTypeStage"] == DBNull.Value))
                                    stage.TypeStage = (int)rdr["idTypeStage"];
                                else
                                    stage.TypeStage = null;

                                if(rdr["salaire"] != DBNull.Value)
                                    stage.Salaire = Double.Parse(rdr["salaire"].ToString(), NumberStyles.Currency);
                                else
                                    stage.Salaire = null;

                                if(rdr["retenu"] != DBNull.Value)
                                    stage.Retenu = (bool)rdr["retenu"];
                                else
                                    stage.Retenu = null;

                                stage.Actif = (bool)rdr["actif"];

                                stage.Modification.UtilisateurId = (int)rdr["idUtilisateur"];
                                stage.Modification.DateModification = Convert.ToDateTime(rdr["dateModification"]);

                                listeStages.Add(stage);
                            }
                        }
                    }
                }
            }
            return listeStages;
        }

        public static List<Stage> recupererStageParIdEntreprise(int idEntreprise) {
            List<Stage> listeStages = null;
            string requete = @"SELECT * FROM stage WHERE actif=1 AND idEntreprise=@idEntreprise";
            SqlParameter idEntrepriseParam = new SqlParameter("@idEntreprise", idEntreprise);
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idEntrepriseParam);
                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        if(rdr.HasRows) {
                            listeStages = new List<Stage>();
                            while(rdr.Read()) {
                                Stage stage = new Stage();
                                stage.Modification = new Modification();
                                stage.Id = (int)rdr["id"];
                                ////////////  bouuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu ahhhhhhhhhhhhhhhhhhhhhhhhhhh ca fait une heure que je suis sur cette erreur 
                                stage.IdEtudiant = (int)rdr["idEtudiant"];
                                stage.IdEntreprise = (int)rdr["idEntreprise"];
                                stage.DatePlacement = Convert.ToDateTime(rdr["datePlacement"]);

                                if(!(rdr["dateDebut"] == DBNull.Value))
                                    stage.DateDebut = Convert.ToDateTime(rdr["dateDebut"]);
                                else
                                    stage.DateDebut = null;

                                if(!(rdr["dateFin"] == DBNull.Value))
                                    stage.DateFin = Convert.ToDateTime(rdr["dateFin"]);
                                else
                                    stage.DateFin = null;

                                if(!(rdr["commentaire"] == DBNull.Value))
                                    stage.Commentaire = rdr["commentaire"].ToString();

                                if(!(rdr["idTypeStage"] == DBNull.Value))
                                    stage.TypeStage = (int)rdr["idTypeStage"];
                                else
                                    stage.TypeStage = null;

                                //??????????????????????????????????????
                                if(rdr["salaire"] != DBNull.Value)
                                    stage.Salaire = Double.Parse(rdr["salaire"].ToString(), NumberStyles.Currency);
                                else
                                    stage.Salaire = null;

                                if(rdr["retenu"] != DBNull.Value)
                                    stage.Retenu = (bool)rdr["retenu"];
                                else
                                    stage.Retenu = null;

                                stage.Actif = (bool)rdr["actif"];
                                stage.Modification.UtilisateurId = (int)rdr["idUtilisateur"];
                                stage.Modification.DateModification = Convert.ToDateTime(rdr["dateModification"]);

                                listeStages.Add(stage);
                            }
                        }
                    }
                }
            }
            return listeStages;
        }

        public static List<Stage> recupererListeStageParNomEtudiant(string recherche)
        {
            List<Stage> listeStages = null;
            string requete = @"SELECT s.* FROM stage s inner join etudiant e on e.id=s.idEtudiant WHERE (e.prenom like @recherche OR e.nom like @recherche) AND s.actif=1";
            SqlParameter rechercheParam = new SqlParameter("@recherche", "%" + recherche + "%");
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(rechercheParam);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            listeStages = new List<Stage>();
                            while (rdr.Read())
                            {
                                Stage stage = new Stage();
                                stage.Modification = new Modification();
                                stage.Id = (int)rdr["id"];
                                stage.IdEtudiant = (int)rdr["idEtudiant"];
                                stage.IdEntreprise = (int)rdr["idEntreprise"];
                                stage.DatePlacement = Convert.ToDateTime(rdr["datePlacement"]);

                                if (!(rdr["dateDebut"] == DBNull.Value))
                                    stage.DateDebut = Convert.ToDateTime(rdr["dateDebut"]);
                                else
                                    stage.DateDebut = null;

                                if (!(rdr["dateFin"] == DBNull.Value))
                                    stage.DateFin = Convert.ToDateTime(rdr["dateFin"]);
                                else
                                    stage.DateFin = null;

                                if (!(rdr["commentaire"] == DBNull.Value))
                                    stage.Commentaire = rdr["commentaire"].ToString();

                                if (!(rdr["idTypeStage"] == DBNull.Value))
                                    stage.TypeStage = (int)rdr["idTypeStage"];
                                else
                                    stage.TypeStage = null;
                                if (rdr["salaire"] != DBNull.Value)
                                    stage.Salaire = Double.Parse(rdr["salaire"].ToString(), NumberStyles.Currency);
                                else
                                    stage.Salaire = null;

                                if (rdr["retenu"] != DBNull.Value)
                                    stage.Retenu = (bool)rdr["retenu"];
                                else
                                    stage.Retenu = null;

                                stage.Actif = (bool)rdr["actif"];
                                stage.Modification.UtilisateurId = (int)rdr["idUtilisateur"];
                                stage.Modification.DateModification = Convert.ToDateTime(rdr["dateModification"]);

                                listeStages.Add(stage);
                            }
                        }
                    }
                }
            }
            return listeStages;
        }
        public static List<Stage> recupererListeStageParNomEntreprise(string recherche)
        {
            List<Stage> listeStages = null;
            string requete = @"SELECT s.* FROM stage s inner join entreprise e on e.id=s.idEntreprise WHERE e.nom like @recherche  AND s.actif=1";
            SqlParameter rechercheParam = new SqlParameter("@recherche", "%" + recherche + "%");
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(rechercheParam);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            listeStages = new List<Stage>();
                            while (rdr.Read())
                            {
                                Stage stage = new Stage();
                                stage.Modification = new Modification();
                                stage.Id = (int)rdr["id"];
                                stage.IdEtudiant = (int)rdr["idEtudiant"];
                                stage.IdEntreprise = (int)rdr["idEntreprise"];
                                stage.DatePlacement = Convert.ToDateTime(rdr["datePlacement"]);

                                if (!(rdr["dateDebut"] == DBNull.Value))
                                    stage.DateDebut = Convert.ToDateTime(rdr["dateDebut"]);
                                else
                                    stage.DateDebut = null;

                                if (!(rdr["dateFin"] == DBNull.Value))
                                    stage.DateFin = Convert.ToDateTime(rdr["dateFin"]);
                                else
                                    stage.DateFin = null;

                                if (!(rdr["commentaire"] == DBNull.Value))
                                    stage.Commentaire = rdr["commentaire"].ToString();

                                if (!(rdr["idTypeStage"] == DBNull.Value))
                                    stage.TypeStage = (int)rdr["idTypeStage"];
                                else
                                    stage.TypeStage = null;
                                if (rdr["salaire"] != DBNull.Value)
                                    stage.Salaire = Double.Parse(rdr["salaire"].ToString(), NumberStyles.Currency);
                                else
                                    stage.Salaire = null;

                                if (rdr["retenu"] != DBNull.Value)
                                    stage.Retenu = (bool)rdr["retenu"];
                                else
                                    stage.Retenu = null;

                                stage.Actif = (bool)rdr["actif"];
                                stage.Modification.UtilisateurId = (int)rdr["idUtilisateur"];
                                stage.Modification.DateModification = Convert.ToDateTime(rdr["dateModification"]);

                                listeStages.Add(stage);
                            }
                        }
                    }
                }
            }
            return listeStages;
        }
        public static List<Stage> recupererListStageRecherche(string recherche)
        {
            List<Stage> listeStages = null;
            SqlParameter rechercheParam = new SqlParameter("@param", recherche);
            string requete = @"select s.* from etudiant e inner join stage s  on s.idEtudiant=e.id inner join entreprise en on s.idEntreprise=en.id where s.actif=1 and e.nom like '%@param%' or e.prenom like '%@param%' or en.nom like '%@param%'";
            
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(rechercheParam);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            listeStages = new List<Stage>();
                            while (rdr.Read())
                            {
                                Stage stage = new Stage();
                                stage.Id = (int)rdr["id"];
                                stage.Id = (int)rdr["idEtudiant"];
                                stage.Id = (int)rdr["idEntreprise"];
                                stage.DatePlacement = Convert.ToDateTime(rdr["datePlacement"]);

                                if (!(rdr["dateDebut"] == DBNull.Value))
                                    stage.DateDebut = Convert.ToDateTime(rdr["dateDebut"]);
                                else
                                    stage.DateDebut = null;

                                if (!(rdr["dateFin"] == DBNull.Value))
                                    stage.DateFin = Convert.ToDateTime(rdr["dateFin"]);
                                else
                                    stage.DateFin = null;

                                if (!(rdr["commentaire"] == DBNull.Value))
                                    stage.Commentaire = rdr["commentaire"].ToString();

                                if (!(rdr["idTypeStage"] == DBNull.Value))
                                    stage.TypeStage = (int)rdr["idTypeStage"];
                                else
                                    stage.TypeStage = null;

                                //??????????????????????????????????????
                                if (rdr["salaire"] != DBNull.Value)
                                    stage.Salaire = Double.Parse(rdr["salaire"].ToString(), NumberStyles.Currency);
                                else
                                    stage.Salaire = null;

                                if (rdr["retenu"] != DBNull.Value)
                                    stage.Retenu = (bool)rdr["retenu"];
                                else
                                    stage.Retenu = null;

                                stage.Actif = (bool)rdr["actif"];
                                stage.Modification.UtilisateurId = (int)rdr["idUtilisateur"];
                                stage.Modification.DateModification = Convert.ToDateTime(rdr["dateModification"]);

                                listeStages.Add(stage);
                            }
                        }
                    }
                }
            }
            return listeStages;
        }
    }
}
