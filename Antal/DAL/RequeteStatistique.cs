using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities;


namespace DAL {
    public static class RequeteStatistique {
        public static string ConnectionString = DefinitionConnection.ConnectionStringCommun;

        ///nb etudiants placés en stage par an selon un nombre de jour donnée apres la fin de la formation
        ///parametre:jours limite apres fin de formation
        public static int recupererNbEtudiantsPlacesAvantDelais(int joursApresFin) {
            int nbEtudiants = 0;
            string requete = @"SELECT COUNT(*) FROM etudiant E"
                           + " INNER JOIN stage S ON S.idEtudiant=E.id"
                           + " WHERE S.datePlacement-E.dateFinFormation<@jours";

            SqlParameter jours = new SqlParameter("@jours", joursApresFin);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(jours);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }
        public static int recupererNbEtudiantsPlacesApresDelais(int joursApresFin)
        {
            int nbEtudiants = 0;
            string requete = @"SELECT COUNT(*) FROM etudiant E"
                           + " INNER JOIN stage S ON S.idEtudiant=E.id"
                           + " WHERE S.datePlacement-E.dateFinFormation>@jours";

            SqlParameter jours = new SqlParameter("@jours", joursApresFin);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(jours);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }
        ///nb etudiants placés en stage par an selon un nombre de jour donnée apres la fin de la formation par an
        public static int recupererNbEtudiantsPlacesApresDelaisParAn(int joursApresFin, int annee) {
            int nbEtudiants = 0;
            string requete = @"SELECT COUNT(*) FROM etudiant E"
                           + " INNER JOIN stage S ON S.idEtudiant=E.id"
                           + " WHERE S.datePlacement-E.dateFinFormation>@jours and year(E.dateFinFormation)=@dateFinFormation";

            SqlParameter jours = new SqlParameter("@jours", joursApresFin);
            SqlParameter dateFinFormation = new SqlParameter("@dateFinFormation", annee);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(jours);
                    cmd.Parameters.Add(dateFinFormation);
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int recupererNbEtudiantsPlacesAvantDelaisParAnParFormation(int joursApresFin, int annee, int idFormationSelect) {
            int nbEtudiants = 0;
            string requete = @"SELECT COUNT(*) FROM etudiant E"
                           + " INNER JOIN stage S ON S.idEtudiant=E.id"
                           + " WHERE S.datePlacement-E.dateFinFormation<=@jours and year(E.dateFinFormation)=@dateFinFormation"
                           + " AND E.idFormation = @idFormation";

            SqlParameter jours = new SqlParameter("@jours", joursApresFin);
            SqlParameter dateFinFormation = new SqlParameter("@dateFinFormation", annee);
            SqlParameter idFormation = new SqlParameter("@idFormation", idFormationSelect);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(jours);
                    cmd.Parameters.Add(dateFinFormation);
                    cmd.Parameters.Add(idFormation);
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int recupererNbEtudiantsPlacesApresDelaisParAnParFormation(int joursApresFin, int annee, int idFormationSelect)
        {
            int nbEtudiants = 0;
            string requete = @"SELECT COUNT(*) FROM etudiant E"
                           + " INNER JOIN stage S ON S.idEtudiant=E.id"
                           + " WHERE S.datePlacement-E.dateFinFormation > @jours and year(E.dateFinFormation)=@dateFinFormation"
                           + " AND E.idFormation = @idFormation";

            SqlParameter jours = new SqlParameter("@jours", joursApresFin);
            SqlParameter dateFinFormation = new SqlParameter("@dateFinFormation", annee);
            SqlParameter idFormation = new SqlParameter("@idFormation", idFormationSelect);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(jours);
                    cmd.Parameters.Add(dateFinFormation);
                    cmd.Parameters.Add(idFormation);
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }


        public static int recupererNbEtudiantsJamaisPlacesParAnParFormation(int anneeFin, int idFormationSelect) {
            int nbEtudiants = 0;
            string requete = @"select(select COUNT(*)from etudiant et where et.idFormation=@idFormation AND YEAR(et.dateFinFormation)=@annee)-(SELECT COUNT(distinct S.idEtudiant)FROM etudiant E INNER JOIN stage S ON S.idEtudiant=E.id where E.idFormation=@idFormation AND YEAR(E.dateFinFormation)=@annee)";

            SqlParameter anneeParam = new SqlParameter("@annee", anneeFin);
            SqlParameter idFromation = new SqlParameter("@idFormation", idFormationSelect);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(anneeParam);
                    cmd.Parameters.Add(idFromation);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }


        public static int NbTousEtudiantsStage() {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*) from etudiant e where e.idStatusCarriere=3";

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int recupererNbEtudiantsPlacesAvantDelaisParAn(int joursApresFin, int annee)
        {
            int nbEtudiants = 0;
            string requete = @"SELECT COUNT(*) FROM etudiant E"
                           + " INNER JOIN stage S ON S.idEtudiant=E.id"
                           + " WHERE S.datePlacement-E.dateFinFormation<@jours and year(E.dateFinFormation)=@dateFinFormation";

            SqlParameter jours = new SqlParameter("@jours", joursApresFin); 
            SqlParameter dateFinFormation = new SqlParameter("@dateFinFormation", annee);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(jours);
                    cmd.Parameters.Add(dateFinFormation);
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        /// <param name="annee">annee pour laquelle on veut les statistiques</param>
        /// <returns>nombre d'etudiants places par an</returns>
        public static int recupererNbEtudiantsPlacesParAn(int annee) {
            int nbEtudiants = 0;
            string requete = @"SELECT COUNT(distinct S.idEtudiant) FROM etudiant E"
                           + " INNER JOIN stage S ON S.idEtudiant=E.id"
                           + " WHERE year(S.datePlacement)=@annee";

            SqlParameter anneeParam = new SqlParameter("@annee", annee);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(anneeParam);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int recupererNbEtudiantsRecherche() {
            int nbEtudiants = 0;
            string requete = @"SELECT COUNT(*) FROM etudiant E WHERE E.idStatusCarriere=1";

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }


        public static int recupererNbEntrevuesDans10Jours() {
            int nbEntrevues = 0;
            string requete = @"SELECT COUNT(*) FROM entrevue E WHERE getDate()+10>=E.dateEntrevue and GETDATE()<E.dateEntrevue";

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    nbEntrevues = (int)cmd.ExecuteScalar();
                }
            }
            return nbEntrevues;
        }

        public static int recupererNbEntreprisesEnregistrees() {
            int nbEntreprises = 0;
            string requete = @"SELECT COUNT(*) FROM entreprise where actif = 1";

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    nbEntreprises = (int)cmd.ExecuteScalar();
                }
            }
            return nbEntreprises;
        }

        /// Nombre d'etudiants places en stage par formation
        /// <param name="idFormation">id de la formation</param>
        public static int NbEtudiantsPlacesStageParFormation(int idFormation) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(distinct s.idEtudiant)from etudiant e INNER JOIN stage s ON s.idEtudiant=e.id where e.idFormation=@idFormation";
            SqlParameter idFormationParam = new SqlParameter("@idFormation", idFormation);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idFormationParam);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        //nb etudiants placés en stage par formation ET an
        public static int NbEtudiantsPlacesStageParFormationEtAn(int idFormation, int annee) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(distinct s.idEtudiant)from etudiant e INNER JOIN stage s ON s.idEtudiant=e.id where e.idFormation=@idFormation  AND year(s.datePlacement)=@an";
            SqlParameter idFormationParam = new SqlParameter("@idFormation", idFormation);
            SqlParameter anParam = new SqlParameter("@an", annee);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idFormationParam);
                    cmd.Parameters.Add(anParam);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        //nb etudiants PAS placés en stage par an et date de fin de formation
        public static int NbEtudiantsSansStageParAnEtFinFormation(int anFinFormation, int anPlacement) {
            int nbEtudiants = 0;
            string requete = @"select(select COUNT(*)from etudiant where year(dateFinFormation)=@dateFinFormation) - (select COUNT(distinct s.idEtudiant)from etudiant e INNER JOIN stage s ON s.idEtudiant=e.id where year(e.dateFinFormation)=@dateFinFormation and year(s.datePlacement)=@datePlacement)";
            SqlParameter dateFinFormation = new SqlParameter("@dateFinFormation", anFinFormation);
            SqlParameter datePlacement = new SqlParameter("@datePlacement", anPlacement);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(dateFinFormation);
                    cmd.Parameters.Add(datePlacement);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        //nb etudiant en emplois par an selon suite stage 
        public static int NbEtudiantsRetenusParAn(int anPlacement) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*) from etudiant e inner join stage s ON s.idEtudiant=e.id where s.retenu=1 and year(s.datePlacement)=@an";

            SqlParameter an = new SqlParameter("@an", anPlacement);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(an);
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        //nb etudiant en emplois par formation par an selon suite stage ou autre entreprise
        public static int NbEtudiantsRetenusParAnEtFormation(int anFin, int idFormation) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(distinct s.idEtudiant) from etudiant e inner join stage s ON s.idEtudiant=e.id where s.retenu=1 and year(e.dateFinFormation)=@dateFinFormation and e.idFormation=@idFormation";

            SqlParameter dateFinFormation = new SqlParameter("@dateFinFormation", anFin);
            SqlParameter idFormationParam = new SqlParameter("@idFormation", idFormation);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(dateFinFormation);
                    cmd.Parameters.Add(idFormationParam);
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        //nb pas retenus par an
        public static int NbEtudiantsPasRetenusParAn(int anFinFormation) {
            int nbEtudiants = 0;
            string requete = @" select COUNT(*) from etudiant e inner join stage s ON s.idEtudiant=e.id where s.retenu=0 and year(e.dateFinFormation)=@dateFinFormation";

            SqlParameter dateFinFormation = new SqlParameter("@dateFinFormation", anFinFormation);


            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(dateFinFormation);
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        //nb pas retenus par an et formation
        public static int NbEtudiantsPasRetenusParAnEtFormation(int anFinFormation, int idFormation) {
            int nbEtudiants = 0;
            string requete = @" select COUNT(*) from etudiant e inner join stage s ON s.idEtudiant=e.id where s.retenu=0 and year(e.dateFinFormation)=@dateFinFormation and e.idFormation=@idFormation";

            SqlParameter dateFinFormation = new SqlParameter("@dateFinFormation", anFinFormation);
            SqlParameter idFormationParam = new SqlParameter("@idFormation", idFormation);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(dateFinFormation);
                    cmd.Parameters.Add(idFormationParam);
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        //nb pas retenus par an et formation
        public static int NbEtudiantsEnEmploiParAnFormation(int anFinFormation) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*) from etudiant e where e.idStatusCarriere=2 and year(e.dateFinFormation)=@dateFinFormation";

            SqlParameter dateFinFormation = new SqlParameter("@dateFinFormation", anFinFormation);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(dateFinFormation);
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        //pas d emploi et pas de stage par an de formation
        public static int NbEtudiantsEnRechercheParAnFormation(int anFinFormation) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*) from etudiant e where e.idStatusCarriere=1 and year(e.dateFinFormation)=@dateFinFormation";

            SqlParameter dateFinFormation = new SqlParameter("@dateFinFormation", anFinFormation);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(dateFinFormation);
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        //en stage par an de formation
        public static int NbEtudiantsEnStageParAnFormation(int anFinFormation) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*) from etudiant e where e.idStatusCarriere=3 and year(e.dateFinFormation)=@dateFinFormation";

            SqlParameter dateFinFormation = new SqlParameter("@dateFinFormation", anFinFormation);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(dateFinFormation);
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        //nombre d'etudiants avec emploi (tous les etudiant dans le systeme)
        public static int NbTousEtudiantsAvecEmploi() {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*) from etudiant e where e.idStatusCarriere=2";

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        //sans emploi (en stage ou a la recherche)
        public static int NbTousEtudiantsSansEmploi() {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*) from etudiant e where e.idStatusCarriere=1 or e.idStatusCarriere=3";

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        //tous les etudiants a la recherche)
        public static int NbTousEtudiantsRecherche() {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*) from etudiant e where e.idStatusCarriere=1";

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }
        //--nb etudiant avec emplois sans emplois pour toutes les années en fonction des formations
        //--avec emploi par formation
        public static int NbEtudiantsAvecEmploiParFormation(int idFormation) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*) from etudiant e where e.idStatusCarriere=2 and e.idFormation=@idFormation";
            SqlParameter idFormationParam = new SqlParameter("@idFormation", idFormation);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idFormationParam);
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        //--sans emploi par formation(stage ou recherche)
        public static int NbEtudiantsSansEmploiParFormation(int idFormation) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*) from etudiant e where (e.idStatusCarriere=1 or e.idStatusCarriere=3) and e.idFormation=@idFormation";
            SqlParameter idFormationParam = new SqlParameter("@idFormation", idFormation);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idFormationParam);
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        //--en recherche par formation
        public static int NbEtudiantsRechercheParFormation(int idFormation) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*) from etudiant e where e.idStatusCarriere=1 and e.idFormation=@idFormation";
            SqlParameter idFormationParam = new SqlParameter("@idFormation", idFormation);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idFormationParam);
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        //--nb entreprise donnant salaire ou pas salaire
        //avec salaire
        public static int NbEntreprisesAvecSalaire() {
            int nbEntreprises = 0;
            string requete = @"select COUNT(DISTINCT idEntreprise) from stage where salaire>0";

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    nbEntreprises = (int)cmd.ExecuteScalar();
                }
            }
            return nbEntreprises;
        }

        //sans salaire
        public static int NbEntreprisesSansSalaire() {
            int nbEntreprises = 0;
            string requete = @"select COUNT (DISTINCT idEntreprise) from stage where salaire<=0";

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    nbEntreprises = (int)cmd.ExecuteScalar();
                }
            }
            return nbEntreprises;
        }

        //--nb entreprise donnant salaire ou pas salaire en fonction des formations
        public static int NbEntreprisesAvecSalaireParFormation(int idFormation) {
            int nbEntreprises = 0;
            string requete = @"select COUNT(DISTINCT idEntreprise) from stage s inner join etudiant e ON e.id=s.idEtudiant where salaire>0 and e.idFormation=@idFormation";
            SqlParameter idFormationParam = new SqlParameter("@idFormation", idFormation);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idFormationParam);
                    nbEntreprises = (int)cmd.ExecuteScalar();
                }
            }
            return nbEntreprises;
        }

        //--nb entreprise donnant salaire ou pas salaire en fonction des formations
        public static int NbEntreprisesSansSalaireParFormation(int idFormation) {
            int nbEntreprises = 0;
            string requete = @"select COUNT (DISTINCT idEntreprise) from stage s inner join etudiant e ON e.id=s.idEtudiant where salaire<=0 and e.idFormation=@idFormation";
            SqlParameter idFormationParam = new SqlParameter("@idFormation", idFormation);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idFormationParam);
                    nbEntreprises = (int)cmd.ExecuteScalar();
                }
            }
            return nbEntreprises;
        }


        public static int recupererNbEtudiants() {
            int nbEtudiants = 0;
            string requete = @"SELECT COUNT(*) FROM etudiant where actif=1" ;

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int recupererNbEtudiantsParAn(int annee) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*)from etudiant e where year(dateFinFormation)=@an";
            SqlParameter an = new SqlParameter("@an", annee);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(an);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int NbEtudiantsParFormation(int idFormation) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*)from etudiant e where idFormation=@idFormation";
            SqlParameter idFormationParam = new SqlParameter("@idFormation", idFormation);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idFormationParam);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int NbEtudiantsParFormationEtAn(int idFormation, int annee) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*)from etudiant e where idFormation=@idFormation and year(dateFinFormation)=@an";
            SqlParameter idFormationParam = new SqlParameter("@idFormation", idFormation);
            SqlParameter an = new SqlParameter("@an", annee);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idFormationParam);
                    cmd.Parameters.Add(an);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int NbEtudiantsEnStageParAn(int anPlacement) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*)from stage where year(datePlacement)=@an";
            SqlParameter an = new SqlParameter("@an", anPlacement);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(an);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int NbEtudiantsPlacesParAnEtFormation(int anPlacement, int idFormation) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*)from stage s inner join etudiant e on s.idEtudiant=e.id where year(datePlacement)=@an and e.idFormation=@idFormation";
            SqlParameter idFormationParam = new SqlParameter("@idFormation", idFormation);
            SqlParameter an = new SqlParameter("@an", anPlacement);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idFormationParam);
                    cmd.Parameters.Add(an);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int NbEtudiantsParAnDeFormation(int anFinFormation) {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*)from etudiant e where year(dateFinFormation)=@an";
            SqlParameter an = new SqlParameter("@an", anFinFormation);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(an);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int NbTousEtudiantsEnStage() {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*)from etudiant where idStatusCarriere=3";

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int NbEtudiantsEnStageParFormation(int idFormation) {

            int nbEtudiants = 0;
            string requete = @"select COUNT(*)from etudiant where idStatusCarriere=3 and idFormation=@idFormation";
            SqlParameter idFormationParam = new SqlParameter("@idFormation", idFormation);

            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(idFormationParam);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int anneeEtudiantPlusAncien()
        {
            int nbEtudiants = 0;
            string requete = @"select MIN(YEAR(dateFinFormation)) from etudiant";
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int recupererNbEtudiantsPasPlaces()
        {
            
            int nbEtudiants = 0;
            string requete = @"select(select COUNT(*) from etudiant)-(SELECT COUNT(distinct S.idEtudiant) FROM etudiant E INNER JOIN stage S ON S.idEtudiant=E.id)";
            using(SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(requete, conn)) {
                    cmd.CommandType = CommandType.Text;
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;

        }

        public static int recupererNbEtudiantsJamaisPlacesParAn(int anneeFin)
        {
            int nbEtudiants = 0;
            string requete = @"select(select COUNT(*) from etudiant et where YEAR(et.dateFinFormation)=@annee)-(SELECT COUNT(distinct S.idEtudiant) FROM etudiant E INNER JOIN stage S ON S.idEtudiant=E.id WHERE year(S.datePlacement)=@annee)";

            SqlParameter anneeParam = new SqlParameter("@annee", anneeFin);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(anneeParam);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int recupererNbEtudiantsJamaisPlacesParAnFinFormation(int anneeFin)
        {
            int nbEtudiants = 0;
            string requete = @"select(select COUNT(*) from etudiant et where YEAR(et.dateFinFormation)=@annee)-(SELECT COUNT(distinct S.idEtudiant) FROM etudiant E INNER JOIN stage S ON S.idEtudiant=E.id WHERE YEAR(E.dateFinFormation)=@annee and year(S.datePlacement)=@annee)";

            SqlParameter anneeParam = new SqlParameter("@annee", anneeFin);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(anneeParam);

                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }

        public static int NbEtudiantParAnEtFormation(int anneeFin, int IdFormation)
        {
            int nbEtudiants = 0;
            string requete = @"select COUNT(*) from etudiant where year(dateFinFormation)=@dateFinFormation and idFormation=@idFormation";

            SqlParameter dateFinFormation = new SqlParameter("@dateFinFormation", anneeFin);
            SqlParameter idFormationParam = new SqlParameter("@idFormation", IdFormation);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(dateFinFormation);
                    cmd.Parameters.Add(idFormationParam);
                    nbEtudiants = (int)cmd.ExecuteScalar();
                }
            }
            return nbEtudiants;
        }
    }
}
