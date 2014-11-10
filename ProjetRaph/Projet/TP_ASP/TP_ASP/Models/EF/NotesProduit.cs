using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TP_ASP.Tools;

namespace TP_ASP.Models.EF
{
    public partial class NotesProduit
    {
        //connection String
        public static string ConnectionString = Outils.ConnectionStringCommun;
        //==========SELECTS=================//

        public static int NbNotesProduit(int prodId)
        {
            int nbNotes = 0;
            string requete = @"SELECT COUNT(*) FROM NotesProduits Where ProduitId=@id";

            SqlParameter id = new SqlParameter("@id", prodId);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(id);

                    nbNotes = (int)cmd.ExecuteScalar();
                }
            }
            return nbNotes;
        }

        public static double MoyenneConfortProduit(int prodId)
        {
            double MoyenneConfort = 0;
            string requete = @"SELECT AVG(Confort) FROM NotesProduits Where ProduitId=@id";
            SqlParameter id = new SqlParameter("@id", prodId);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(id);
                    MoyenneConfort = (double)cmd.ExecuteScalar();
                }
            }
            return MoyenneConfort;
        }
        public static double MoyennePropreteProduit(int prodId)
        {
            double moyenne = 0;
            string requete = @"SELECT AVG(Proprete) FROM NotesProduits Where ProduitId=@id";
            SqlParameter id = new SqlParameter("@id", prodId);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(id);
                    moyenne = (double)cmd.ExecuteScalar();
                }
            }
            return moyenne;
        }
        public static double MoyenneLocalisationProduit(int prodId)
        {
            double moyenne = 0;
            string requete = @"SELECT AVG(Localisation) FROM NotesProduits Where ProduitId=@id";
            SqlParameter id = new SqlParameter("@id", prodId);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(id);
                    moyenne = (double)cmd.ExecuteScalar();
                }
            }
            return moyenne;
        }
        public static double MoyenneValeurProduit(int prodId)
        {
            double moyenne = 0;
            string requete = @"SELECT AVG(Valeur) FROM NotesProduits Where ProduitId=@id";
            SqlParameter id = new SqlParameter("@id", prodId);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(id);
                    moyenne = (double)cmd.ExecuteScalar();
                }
            }
            return moyenne;
        }
        public static double MoyenneGeneralProduit(int prodId)
        {
            double moyenne = 0;
            string requete = @"SELECT((SELECT AVG(Confort) FROM NotesProduits Where ProduitId=@id) + (SELECT AVG(Proprete) FROM NotesProduits Where ProduitId=@id)+(SELECT AVG(Localisation) FROM NotesProduits Where ProduitId=@id)+(SELECT AVG(Valeur) FROM NotesProduits Where ProduitId=@id))/4";
            SqlParameter id = new SqlParameter("@id", prodId);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(id);
                    moyenne = (double)cmd.ExecuteScalar();
                }
            }
            return moyenne;
        }

        
        /// Recuperer un favoris specifique
        public static NotesProduit GetById(int pUserId, int pProdId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            NotesProduit rValue = pDb.NotesProduits.Where(m => m.UtilisateurId == pUserId && m.ProduitId == pProdId).FirstOrDefault();

            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }

        public static List<NotesProduit> GetByUserId(int pUserId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            List<NotesProduit> rValue = pDb.NotesProduits.Where(m => m.UtilisateurId == pUserId).ToList();

            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }


        public static List<NotesProduit> GetByProduitId(int pProdId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            List<NotesProduit> rValue = pDb.NotesProduits.Where(m => m.ProduitId == pProdId).ToList();

            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }

        public static List<NotesProduit> GetALL()
        {
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                List<NotesProduit> rValue = db.NotesProduits.ToList();
                return rValue;
            }
        }


        //==========DELETES=================//

        /// Supprimer un faboris specifique
        public static bool DeleteById(int pUserId, int pProdId)
        {
            bool retour = false;
            if (pUserId > 0 && pProdId > 0)
            {
                using (MontRealEstateEntities db = new MontRealEstateEntities())
                {
                    NotesProduit notesProduit = GetById(pUserId, pProdId, db);
                    if (notesProduit != null)
                    {
                        db.DeleteObject(notesProduit);
                        db.SaveChanges();
                        retour = true;
                    }
                }
            }
            return retour;
        }

        /// Supprimer tous les favoris d'un user
        public static bool DeleteByUserId(int pUserId)
        {
            bool retour = false;
            if (pUserId > 0)
            {
                using (MontRealEstateEntities db = new MontRealEstateEntities())
                {
                    List<NotesProduit> notesProduit = GetByUserId(pUserId, db);
                    if (notesProduit != null && notesProduit.Count > 0)
                    {
                        foreach (NotesProduit np in notesProduit)
                        {
                            db.DeleteObject(np);
                        }
                        db.SaveChanges();
                        retour = true;
                    }
                }
            }
            return retour;
        }

        /// Supprimer un produit de tous les favoris
        public static bool DeleteByProduitId(int pProdId)
        {
            bool retour = false;
            if (pProdId > 0)
            {
                using (MontRealEstateEntities db = new MontRealEstateEntities())
                {
                    List<NotesProduit> notesProduit = GetByProduitId(pProdId, db);
                    if (notesProduit != null && notesProduit.Count > 0)
                    {
                        foreach (NotesProduit np in notesProduit)
                        {
                            db.DeleteObject(np);
                        }
                        db.SaveChanges();
                        retour = true;
                    }
                }
            }
            return retour;
        }

        //==========INSERTS ET UPDATES=================//

        public static void Save(NotesProduit pModel)
        {
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                NotesProduit notesProduitModif = GetById(pModel.UtilisateurId, pModel.ProduitId, db);
                //modification
                if (notesProduitModif != null)
                {
                    notesProduitModif.Confort = pModel.Confort;
                    notesProduitModif.Proprete = pModel.Proprete;
                    notesProduitModif.Localisation = pModel.Localisation;
                    notesProduitModif.Valeur = pModel.Valeur;
                }
                else
                { //add
                    db.NotesProduits.AddObject(pModel);
                }
                //enregistrer les modifications
                db.SaveChanges();
            }
        }

    }
}