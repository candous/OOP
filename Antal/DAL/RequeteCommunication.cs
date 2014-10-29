using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL
{
    static public class RequeteCommunication
    {
        public static string ConnectionString = DefinitionConnection.ConnectionStringCommun;


         public static List<Communication> recupererListeCommunicationEtudiants()
         {
             List<Communication> listCommunication = null;
             using (SqlConnection conn = new SqlConnection(ConnectionString))
             {

                 conn.Open();
                 using (SqlCommand cmd = conn.CreateCommand())
                 {
                     //Requette
                     cmd.CommandText = @"select * from communicationEtudiant";

                     using (SqlDataReader rdr = cmd.ExecuteReader())
                     {
                         if (rdr.HasRows)
                         {
                             listCommunication = new List<Communication>();

                             while (rdr.Read())
                             {
                                 Communication communication = new Communication();
                                 if (!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                     communication.Id = rdr.GetInt32(rdr.GetOrdinal("id"));
                                 if (!rdr.IsDBNull(rdr.GetOrdinal("idUtilisateur")))
                                     communication.IdUtilisateur = rdr.GetInt32(rdr.GetOrdinal("idUtilisateur"));
                                 if (!rdr.IsDBNull(rdr.GetOrdinal("idEtudiant")))
                                     communication.IdTo = rdr.GetInt32(rdr.GetOrdinal("idEtudiant"));
                                 //not null
                                     communication.DateCommunication = Convert.ToDateTime(rdr["dateCommunication"]);
                                     communication.TypeCommunication = rdr.GetInt32(rdr.GetOrdinal("idTypeCommunication"));
                                 if (!(rdr["commentaire"] == DBNull.Value))
                                     communication.Commentaire = rdr["commentaire"].ToString();
                                 if (!rdr.IsDBNull(rdr.GetOrdinal("idStatusCommunication")))
                                     communication.StatusCommunication = rdr.GetInt32(rdr.GetOrdinal("idStatusCommunication"));

                                 listCommunication.Add(communication);
                             }
                         }
                     }

                 }
             }
             return listCommunication;
         }
         public static List<Communication> recupererListeCommunicationEntreprises()
         {
             List<Communication> listCommunication = null;

             using (SqlConnection conn = new SqlConnection(ConnectionString))
             {

                 conn.Open();
                 using (SqlCommand cmd = conn.CreateCommand())
                 {
                     //Requette
                     cmd.CommandText = @"select * from communicationEntreprise";

                     using (SqlDataReader rdr = cmd.ExecuteReader())
                     {
                         if (rdr.HasRows)
                         {
                             listCommunication = new List<Communication>();

                             while (rdr.Read())
                             {
                                 Communication communication = new Communication();
                                 if (!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                     communication.Id = rdr.GetInt32(rdr.GetOrdinal("id"));
                                 if (!rdr.IsDBNull(rdr.GetOrdinal("idUtilisateur")))
                                     communication.IdUtilisateur = rdr.GetInt32(rdr.GetOrdinal("idUtilisateur"));
                                 if (!rdr.IsDBNull(rdr.GetOrdinal("idEntreprise")))
                                     communication.IdTo = rdr.GetInt32(rdr.GetOrdinal("idEntreprise"));
                                 //not null
                                 communication.DateCommunication = Convert.ToDateTime(rdr["dateCommunication"]);
                                
                                     communication.TypeCommunication = rdr.GetInt32(rdr.GetOrdinal("idTypeCommunication"));
                                 if (!(rdr["commentaire"] == DBNull.Value))
                                     communication.Commentaire = rdr["commentaire"].ToString();
                                 if (!rdr.IsDBNull(rdr.GetOrdinal("idStatusCommunication")))
                                     communication.StatusCommunication = rdr.GetInt32(rdr.GetOrdinal("idStatusCommunication"));

                                 listCommunication.Add(communication);
                             }
                         }
                     }

                 }
             }
             return listCommunication;
         }
        //Recuperer la liste des communications entre un etudiant et un utlisateur
        public static List<Communication> recupererCommunicationEtudiantUtilisateur(int idEtudiant)
        {
            List<Communication> listCommunication=null;
            SqlParameter idEtudiantParam = new SqlParameter("@idEtudiant", idEtudiant);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Parameters.Add(idEtudiantParam);
                    //Requette
                    cmd.CommandText = @"select * from communicationEtudiant where idEtudiant=@idEtudiant";

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if(rdr.HasRows)
                        {
                            listCommunication = new List<Communication>();

                            while (rdr.Read())
                            {
                                Communication communication = new Communication();
                                if (!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                    communication.Id = rdr.GetInt32(rdr.GetOrdinal("id"));
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idUtilisateur")))
                                    communication.IdUtilisateur = rdr.GetInt32(rdr.GetOrdinal("idUtilisateur"));
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idEtudiant")))
                                    communication.IdTo = rdr.GetInt32(rdr.GetOrdinal("idEtudiant"));
                                //not null
                                communication.DateCommunication = Convert.ToDateTime(rdr["dateCommunication"]);
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idTypeCommunication")))
                                    communication.TypeCommunication = rdr.GetInt32(rdr.GetOrdinal("idTypeCommunication"));
                                if (!(rdr["commentaire"] == DBNull.Value))
                                    communication.Commentaire = rdr["commentaire"].ToString();
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idStatusCommunication")))
                                    communication.StatusCommunication = rdr.GetInt32(rdr.GetOrdinal("idStatusCommunication"));

                                listCommunication.Add(communication);
                            }
                        }
                    }
                }
            }
            return listCommunication;
        }

        public static List<Communication> recupererCommunicationEtudiantUtilisateurRecherche(string recherche)
        {
            List<Communication> listCommunication = null;
            SqlParameter rechercheParam = new SqlParameter("@recherche", "%" + recherche + "%");

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {

                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Parameters.Add(rechercheParam);
                    //Requette
                    cmd.CommandText = @"select CE.* from communicationEtudiant CE INNER JOIN etudiant E ON CE.idEtudiant=E.id where E.nom like @recherche or E.prenom like @recherche";

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            listCommunication = new List<Communication>();

                            while (rdr.Read())
                            {
                                Communication communication = new Communication();
                                if (!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                    communication.Id = rdr.GetInt32(rdr.GetOrdinal("id"));
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idUtilisateur")))
                                    communication.IdUtilisateur = rdr.GetInt32(rdr.GetOrdinal("idUtilisateur"));
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idEtudiant")))
                                    communication.IdTo = rdr.GetInt32(rdr.GetOrdinal("idEtudiant"));
                                //not null
                                communication.DateCommunication = Convert.ToDateTime(rdr["dateCommunication"]);
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idTypeCommunication")))
                                    communication.TypeCommunication = rdr.GetInt32(rdr.GetOrdinal("idTypeCommunication"));
                                if (!(rdr["commentaire"] == DBNull.Value))
                                    communication.Commentaire = rdr["commentaire"].ToString();
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idStatusCommunication")))
                                    communication.StatusCommunication = rdr.GetInt32(rdr.GetOrdinal("idStatusCommunication"));

                                listCommunication.Add(communication);
                            }
                        }
                    }
                }
            }
            return listCommunication;
        }

        public static List<Communication> recupererCommunicationEntrepriseUtilisateurRecherche(string recherche)
        {
            List<Communication> listCommunication = null;
            SqlParameter rechercheParam = new SqlParameter("@recherche", "%" + recherche + "%");

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {

                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Parameters.Add(rechercheParam);
                    cmd.CommandText = @"select CE.* from communicationEntreprise CE INNER JOIN entreprise E ON CE.idEntreprise=E.id where E.nom like @recherche";

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            listCommunication = new List<Communication>();

                            while (rdr.Read())
                            {
                                Communication communication = new Communication();
                                if (!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                    communication.Id = rdr.GetInt32(rdr.GetOrdinal("id"));
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idUtilisateur")))
                                    communication.IdUtilisateur = rdr.GetInt32(rdr.GetOrdinal("idUtilisateur"));
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idEntreprise")))
                                    communication.IdTo = rdr.GetInt32(rdr.GetOrdinal("idEntreprise"));
                                //not null
                                communication.DateCommunication = Convert.ToDateTime(rdr["dateCommunication"]);
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idTypeCommunication")))
                                    communication.TypeCommunication = rdr.GetInt32(rdr.GetOrdinal("idTypeCommunication"));
                                if (!(rdr["commentaire"] == DBNull.Value))
                                    communication.Commentaire = rdr["commentaire"].ToString();
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idStatusCommunication")))
                                    communication.StatusCommunication = rdr.GetInt32(rdr.GetOrdinal("idStatusCommunication"));

                                listCommunication.Add(communication);
                            }
                        }
                    }
                }
            }
            return listCommunication;
        }

        //Recuperer une communication specifique avec une entreprise  par son id 
        public static Communication recupererCommunicationEntrepriseParId(int idCommunication)
        {
            Communication communication = null;
            SqlParameter idCommunicationParam = new SqlParameter("@id", idCommunication);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Parameters.Add(idCommunicationParam);
                    //Requette
                    cmd.CommandText = @"select * from communicationEntreprise where id=@id";

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                            while (rdr.Read())
                            {
                                communication = new Communication();
                                if (!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                    communication.Id = rdr.GetInt32(rdr.GetOrdinal("id"));
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idUtilisateur")))
                                    communication.IdUtilisateur = rdr.GetInt32(rdr.GetOrdinal("idUtilisateur"));
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idEntreprise")))
                                    communication.IdTo = rdr.GetInt32(rdr.GetOrdinal("idEntreprise"));
                                //not null
                                communication.DateCommunication = Convert.ToDateTime(rdr["dateCommunication"]);
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idTypeCommunication")))
                                    communication.TypeCommunication = rdr.GetInt32(rdr.GetOrdinal("idTypeCommunication"));
                                if (!(rdr["commentaire"] == DBNull.Value))
                                    communication.Commentaire = rdr["commentaire"].ToString();
                                if (!rdr.IsDBNull(rdr.GetOrdinal("idStatusCommunication")))
                                    communication.StatusCommunication = rdr.GetInt32(rdr.GetOrdinal("idStatusCommunication"));
                            }
                        }
                    }
                }
            return communication;
        }

        //Recuperer une communication specifique avec un etudiant  par son id 
        public static Communication recupererCommunicationEtudiantParId(int idCommunication)
        {

                    Communication communication = null;
                    SqlParameter idCommunicationParam = new SqlParameter("@id", idCommunication);

                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Parameters.Add(idCommunicationParam);
                            //Requette
                            cmd.CommandText = @"select * from communicationEtudiant where id=@id";

                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    communication = new Communication();
                                    if (!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                        communication.Id = rdr.GetInt32(rdr.GetOrdinal("id"));
                                    if (!rdr.IsDBNull(rdr.GetOrdinal("idUtilisateur")))
                                        communication.IdUtilisateur = rdr.GetInt32(rdr.GetOrdinal("idUtilisateur"));
                                    if (!rdr.IsDBNull(rdr.GetOrdinal("idEtudiant")))
                                        communication.IdTo = rdr.GetInt32(rdr.GetOrdinal("idEtudiant"));
                                    //not null
                                    communication.DateCommunication = Convert.ToDateTime(rdr["dateCommunication"]);
                                    if (!rdr.IsDBNull(rdr.GetOrdinal("idTypeCommunication")))
                                        communication.TypeCommunication = rdr.GetInt32(rdr.GetOrdinal("idTypeCommunication"));
                                    if (!(rdr["commentaire"] == DBNull.Value))
                                        communication.Commentaire = rdr["commentaire"].ToString();
                                    if (!rdr.IsDBNull(rdr.GetOrdinal("idStatusCommunication")))
                                        communication.StatusCommunication = rdr.GetInt32(rdr.GetOrdinal("idStatusCommunication"));
                                }
                            }
                        }
                    }
                    return communication;
        }

        //Recuperer la liste des communications entre un entreprise et un utilisateur
        public static List<Communication> recupererCommunicationEntrepriseUtilisateur(int idEntreprise) {
            List<Communication> listCommunication = null;
            SqlParameter idEntrepriseParam = new SqlParameter("@idEntreprise", idEntreprise);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.Parameters.Add(idEntrepriseParam);
                    //Requette
                    cmd.CommandText = @"SELECT * FROM communicationEntreprise WHERE idEntreprise=@idEntreprise";

                    using(SqlDataReader rdr = cmd.ExecuteReader()) {
                        if(rdr.HasRows) {
                            listCommunication = new List<Communication>();

                            while(rdr.Read()) {
                                Communication communication = new Communication();
                                if(!rdr.IsDBNull(rdr.GetOrdinal("id")))
                                    communication.Id = rdr.GetInt32(rdr.GetOrdinal("id"));
                                if(!rdr.IsDBNull(rdr.GetOrdinal("idUtilisateur")))
                                    communication.IdUtilisateur = rdr.GetInt32(rdr.GetOrdinal("idUtilisateur"));
                                if(!rdr.IsDBNull(rdr.GetOrdinal("idEntreprise")))
                                    communication.IdTo = rdr.GetInt32(rdr.GetOrdinal("idEntreprise"));
                                //not null
                                communication.DateCommunication = Convert.ToDateTime(rdr["dateCommunication"]);
                                communication.TypeCommunication = rdr.GetInt32(rdr.GetOrdinal("idTypeCommunication"));
                                if(!(rdr["commentaire"] == DBNull.Value))
                                    communication.Commentaire = rdr["commentaire"].ToString();
                                if(!rdr.IsDBNull(rdr.GetOrdinal("idStatusCommunication")))
                                    communication.StatusCommunication = rdr.GetInt32(rdr.GetOrdinal("idStatusCommunication"));

                                listCommunication.Add(communication);
                            }
                        }
                    }

                }
            }
            return listCommunication;
        }

        //Ajouter une communication entre un etudiant et un utilisateur
        public static bool ajouterCommunicationEtudiantUtilisateur(Communication communication) {
            bool cree = false;
            string requete = @"INSERT INTO communicationEtudiant (idUtilisateur, idEtudiant, dateCommunication, idTypeCommunication, commentaire, idStatusCommunication) OUTPUT INSERTED.id VALUES(@idUtilisateur, @idEtudiant, @dateCommunication, @idTypeCommunication, @commentaire, @idStatusCommunication)";

            SqlParameter idUtilisateur = new SqlParameter("@idUtilisateur", communication.IdUtilisateur);
            SqlParameter idEtudiant = new SqlParameter("@idEtudiant", communication.IdTo);
            SqlParameter dateCommunication = new SqlParameter("@dateCommunication", communication.DateCommunication);
            SqlParameter idTypeCommunication = new SqlParameter("@idTypeCommunication", communication.TypeCommunication);
            SqlParameter commentaire = new SqlParameter("@commentaire", !string.IsNullOrEmpty(communication.Commentaire) ? communication.Commentaire : (object)DBNull.Value);
            SqlParameter idStatusCommunication = new SqlParameter("@idStatusCommunication", communication.StatusCommunication);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idUtilisateur);
                    cmd.Parameters.Add(idEtudiant);
                    cmd.Parameters.Add(dateCommunication);
                    cmd.Parameters.Add(idTypeCommunication);
                    cmd.Parameters.Add(commentaire);
                    cmd.Parameters.Add(idStatusCommunication);

                    communication.Id = (Int32)cmd.ExecuteScalar();
                    if(communication.Id != 0)
                        cree = true;
                }
            }
            return cree;
        }

        //Ajouter une communication entre un entreprise et un utilisateur
        public static bool ajouterCommunicationEntrepriseUtilisateur(Communication communication) {

            bool cree = false;
            string requete = @"INSERT INTO communicationEntreprise (idUtilisateur, idEntreprise, dateCommunication, idTypeCommunication, commentaire, idStatusCommunication) OUTPUT INSERTED.id  VALUES (@idUtilisateur, @idEntreprise, @dateCommunication, @idTypeCommunication, @commentaire, @idStatusCommunication)";

            SqlParameter idUtilisateur = new SqlParameter("@idUtilisateur", communication.IdUtilisateur);
            SqlParameter idEntreprise = new SqlParameter("@idEntreprise", communication.IdTo);
            SqlParameter dateCommunication = new SqlParameter("@dateCommunication", communication.DateCommunication);
            SqlParameter idTypeCommunication = new SqlParameter("@idTypeCommunication", communication.TypeCommunication);
            SqlParameter commentaire = new SqlParameter("@commentaire", !string.IsNullOrEmpty(communication.Commentaire) ? communication.Commentaire : (object)DBNull.Value);
            SqlParameter idStatusCommunication = new SqlParameter("@idStatusCommunication", communication.StatusCommunication);
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idUtilisateur);
                    cmd.Parameters.Add(idEntreprise);
                    cmd.Parameters.Add(dateCommunication);
                    cmd.Parameters.Add(idTypeCommunication);
                    cmd.Parameters.Add(commentaire);
                    cmd.Parameters.Add(idStatusCommunication);

                    communication.Id = (Int32)cmd.ExecuteScalar();
                    if(communication.Id != 0)
                        cree = true;
                }
            }
            return cree;
        }
        //Nombre de communication en attente
        public static int recupererNbCommunicationEnAttente()
        {
            int nbCommunication = 0;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //Requete
                    cmd.CommandText = @"select(select COUNT (*) from communicationEtudiant where idStatusCommunication=2)+ (select COUNT (*) from communicationEntreprise where idStatusCommunication=2)";
                    nbCommunication = (int)cmd.ExecuteScalar();
                }
            }
            return nbCommunication;
        }

        //Modifier Communication Etudiant
        public static bool modifierCommunicationEtudiantUtilisateur(Communication communication)
        {
                    bool modifie = false;
                    string requete = @"UPDATE communicationEtudiant set idUtilisateur=@idUtilisateur, idEtudiant=@idEtudiant, dateCommunication=@dateCommunication, idTypeCommunication=@idTypeCommunication, commentaire=@commentaire, idStatusCommunication=@idStatusCommunication where id=@id";

                    SqlParameter idUtilisateur = new SqlParameter("@idUtilisateur", communication.IdUtilisateur);
                    SqlParameter idEtudiant = new SqlParameter("@idEtudiant", communication.IdTo);
                    SqlParameter dateCommunication = new SqlParameter("@dateCommunication",  communication.DateCommunication);
                    SqlParameter idTypeCommunication = new SqlParameter("@idTypeCommunication", communication.TypeCommunication==null ? (object)DBNull.Value : communication.TypeCommunication);
                    SqlParameter commentaire = new SqlParameter("@commentaire", string.IsNullOrEmpty(communication.Commentaire) ? (object)DBNull.Value : communication.Commentaire);
                    SqlParameter idStatusCommunication = new SqlParameter("@idStatusCommunication", communication.StatusCommunication);
                    SqlParameter id = new SqlParameter("@id", communication.Id);


                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(requete, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(idUtilisateur);
                            cmd.Parameters.Add(idEtudiant);
                            cmd.Parameters.Add(dateCommunication);
                            cmd.Parameters.Add(idTypeCommunication);
                            cmd.Parameters.Add(commentaire);
                            cmd.Parameters.Add(idStatusCommunication);
                            cmd.Parameters.Add(id);

                            int affectes = (int)cmd.ExecuteNonQuery();
                            if (affectes != 0)
                                modifie = true;
                        }
                    }
                    return modifie;
        }

        //Modifier Communication Entreprise
        public static bool modifierCommunicationEntrepriseUtilisateur(Communication communication)
        {
                    bool modifie = false;
                    string requete = @"UPDATE communicationEntreprise set idUtilisateur=@idUtilisateur, idEntreprise=@idEntreprise, dateCommunication=@dateCommunication, idTypeCommunication=@idTypeCommunication, commentaire=@commentaire, idStatusCommunication=@idStatusCommunication where id=@id";

                    SqlParameter idUtilisateur = new SqlParameter("@idUtilisateur", communication.IdUtilisateur);
                    SqlParameter idEntreprise = new SqlParameter("@idEntreprise", communication.IdTo);
                    SqlParameter dateCommunication = new SqlParameter("@dateCommunication", communication.DateCommunication);
                    SqlParameter idTypeCommunication = new SqlParameter("@idTypeCommunication", communication.TypeCommunication == null ? (object)DBNull.Value : communication.TypeCommunication);
                    SqlParameter commentaire = new SqlParameter("@commentaire", string.IsNullOrEmpty(communication.Commentaire) ? (object)DBNull.Value : communication.Commentaire);
                    SqlParameter idStatusCommunication = new SqlParameter("@idStatusCommunication", communication.StatusCommunication);
                    SqlParameter id = new SqlParameter("@id", communication.Id);


                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(requete, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(idUtilisateur);
                            cmd.Parameters.Add(idEntreprise);
                            cmd.Parameters.Add(dateCommunication);
                            cmd.Parameters.Add(idTypeCommunication);
                            cmd.Parameters.Add(commentaire);
                            cmd.Parameters.Add(idStatusCommunication);
                            cmd.Parameters.Add(id);

                            int affectes = (int)cmd.ExecuteNonQuery();
                            if (affectes != 0)
                                modifie = true;
                        }
                    }
                    return modifie;
        }

        // Supprimer Communication Etudiant
        public static bool supprimerCommunicationEtudiantUtilisateur(Communication communication)
        {
                    bool modifie = false;
                    string requete = @"DELETE from communicationEtudiant where id=@id";

                    SqlParameter id = new SqlParameter("@id", communication.Id);

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

        // Supprimer Communication Entreprise
        public static bool supprimerCommunicationEntrepriseUtilisateur(Communication communication)
        {
             bool modifie = false;
             string requete = @"DELETE from communicationEntreprise where id=@id";

                    SqlParameter id = new SqlParameter("@id", communication.Id);

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
