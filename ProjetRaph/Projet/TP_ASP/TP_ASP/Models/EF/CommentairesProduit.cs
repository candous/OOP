using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP_ASP.Tools;
using WebMatrix.WebData;

namespace TP_ASP.Models.EF
{
    public partial class CommentairesProduit
    {

        //==========SELECTS=================//

        /// Recuperer un commentaire specifique
        public static CommentairesProduit GetById(int pId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            CommentairesProduit rValue = pDb.CommentairesProduits.Where(m => m.Id == pId && m.EstSupprime == false).FirstOrDefault();

            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }
        /// Recuperer la liste de tous les commentaires faites par un user
        public static List<CommentairesProduit> GetByUserId(int pUserId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            List<CommentairesProduit> rValue = pDb.CommentairesProduits.Where(m => m.UtilisateurId == pUserId && m.EstSupprime == false).ToList();

            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }
        /// Recuperer la liste de tous les commentaires d'un produit
        public static List<CommentairesProduit> GetByProduitId(int pProdId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            List<CommentairesProduit> rValue = pDb.CommentairesProduits.Where(m => m.ProduitId == pProdId && m.EstSupprime == false).ToList();

            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }

        public static List<CommentairesProduit> GetALL()
        {
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                var rValue = db.CommentairesProduits.ToList();
                return rValue;
            }
        }

        public static List<CommentairesProduit> GetALLActives()
        {
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                var rValue = db.CommentairesProduits.Where(m => m.EstSupprime == false).ToList();
                return rValue;
            }
        }

        //==========DELETES=================//


        /// Supprimer tous les commentaires d'un produit
        public static bool DeleteById(int pComId)
        {
            bool retour = false;
            if (pComId > 0)
            {
                using (MontRealEstateEntities db = new MontRealEstateEntities())
                {
                    CommentairesProduit commentairesProd = GetById(pComId, db);
                    if (commentairesProd != null)
                    {
                        commentairesProd.EstSupprime = true;
                        commentairesProd.DateModification = DateTime.Now;
                        Outils.ConnectWebSecurity();
                        commentairesProd.ModifiePar= WebSecurity.CurrentUserId;
                        db.SaveChanges();
                        retour = true;
                    }
                }
            }
            return retour;
        }

        /// Supprimer tous les commentaires d'un user
        public static bool DeleteByUserId(int pUserId)
        {
            bool retour = false;
            if (pUserId > 0)
            {
                using (MontRealEstateEntities db = new MontRealEstateEntities())
                {
                    List<CommentairesProduit> ListCommentairesUser = GetByUserId(pUserId, db);
                    if (ListCommentairesUser != null && ListCommentairesUser.Count > 0)
                    {
                        foreach (CommentairesProduit cp in ListCommentairesUser)
                        {
                            cp.EstSupprime = true;
                            cp.DateModification = DateTime.Now; 
                            Outils.ConnectWebSecurity();
                            cp.ModifiePar = WebSecurity.CurrentUserId;
                        }
                        db.SaveChanges();
                        retour = true;
                    }
                }
            }
            return retour;
        }

        /// Supprimer tous les commentaires d'un produit
        public static bool DeleteByProduitId(int pProdId)
        {
            bool retour = false;
            if (pProdId > 0)
            {
                using (MontRealEstateEntities db = new MontRealEstateEntities())
                {
                    List<CommentairesProduit> ListCommentairesProd = GetByProduitId(pProdId, db);
                    if (ListCommentairesProd != null && ListCommentairesProd.Count > 0)
                    {
                        foreach (CommentairesProduit cp in ListCommentairesProd)
                        {
                            cp.EstSupprime = true;
                            cp.DateModification = DateTime.Now;
                            Outils.ConnectWebSecurity();
                            cp.ModifiePar = WebSecurity.CurrentUserId;
                        }
                        db.SaveChanges();
                        retour = true;
                    }
                }
            }
            return retour;
        }

        //==========INSERTS ET UPDATES=================//

        public static void Save(CommentairesProduit pModel)
        {
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                //modification
                if (pModel.Id > 0)
                {
                    CommentairesProduit commentProdModifier = GetById(pModel.Id, db);
                    commentProdModifier.Commentaires = pModel.Commentaires;
                    commentProdModifier.DateModification = DateTime.Now;
                    commentProdModifier.ModifiePar = pModel.ModifiePar;
                }
                else
                { //add
                    pModel.DateCreation = DateTime.Now;
                    pModel.DateModification = DateTime.Now;
                    db.CommentairesProduits.AddObject(pModel);
                }
                //enregistrer les modifications
                db.SaveChanges();
            }
        }

    }
}