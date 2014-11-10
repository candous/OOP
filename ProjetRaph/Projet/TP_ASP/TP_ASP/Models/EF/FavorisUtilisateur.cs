using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP_ASP.Tools;
using WebMatrix.WebData;

namespace TP_ASP.Models.EF
{
    public partial class FavorisUtilisateur
    {

          //==========SELECTS=================//

        /// Recuperer un favoris specifique
        public static FavorisUtilisateur GetById(int pUserId, int pProdId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            FavorisUtilisateur rValue = pDb.FavorisUtilisateurs.Where(m => m.UtilisateurId == pUserId && m.ProduitId == pProdId && m.EstSupprime == false).FirstOrDefault();

            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }

        public static List<FavorisUtilisateur> GetByUserId(int pUserId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            List<FavorisUtilisateur> rValue = pDb.FavorisUtilisateurs.Where(m => m.UtilisateurId == pUserId && m.EstSupprime == false).ToList();

            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }

        
        public static List<FavorisUtilisateur> GetByProduitId(int pProdId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            List<FavorisUtilisateur> rValue = pDb.FavorisUtilisateurs.Where(m => m.ProduitId == pProdId && m.EstSupprime == false).ToList();

            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }

        public static List<FavorisUtilisateur> GetALL()
        {
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                List<FavorisUtilisateur> rValue = db.FavorisUtilisateurs.ToList();
                return rValue;
            }
        }

        public static List<FavorisUtilisateur> GetALLActives()
        {
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                List<FavorisUtilisateur> rValue = db.FavorisUtilisateurs.Where(m => m.EstSupprime == false).ToList();
                return rValue;
            }
        }

        //==========DELETES=================//

        /// Supprimer un faboris specifique
        public static bool DeleteById(int pUserId, int pProdId)
        {
            bool retour = false;
            if (pUserId > 0 && pProdId>0 && pIdQuiSupprime > 0)
            {
                using (MontRealEstateEntities db = new MontRealEstateEntities())
                {
                    FavorisUtilisateur favsUtilisaeur = GetById(pUserId, pProdId, db);
                    if (favsUtilisaeur != null)
                    {
                        favsUtilisaeur.EstSupprime = true;
                        favsUtilisaeur.DateModification = DateTime.Now;
                        Outils.ConnectWebSecurity();
                        favsUtilisaeur.ModifiePar = WebSecurity.CurrentUserId;
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
            if (pUserId > 0 && pIdQuiSupprime > 0)
            {
                using (MontRealEstateEntities db = new MontRealEstateEntities())
                {
                    List<FavorisUtilisateur> favsUtilisaeur = GetByUserId(pUserId, db);
                    if (favsUtilisaeur != null && favsUtilisaeur.Count > 0)
                    {
                        foreach (FavorisUtilisateur fu in favsUtilisaeur)
                        {
                            fu.EstSupprime = true;
                            fu.DateModification = DateTime.Now;
                            Outils.ConnectWebSecurity();
                            fu.ModifiePar = WebSecurity.CurrentUserId;
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
            if (pProdId > 0 && pIdQuiSupprime > 0)
            {
                using (MontRealEstateEntities db = new MontRealEstateEntities())
                {
                    List<FavorisUtilisateur> favorisUtilisateur = GetByProduitId(pProdId, db);
                    if (favorisUtilisateur != null && favorisUtilisateur.Count > 0)
                    {
                        foreach (FavorisUtilisateur fu in favorisUtilisateur)
                        {
                            fu.EstSupprime = true;
                            fu.DateModification = DateTime.Now;
                            Outils.ConnectWebSecurity();
                            fu.ModifiePar = WebSecurity.CurrentUserId;
                        }
                        db.SaveChanges();
                        retour = true;
                    }
                }
            }
            return retour;
        }

        //==========INSERTS ET UPDATES=================//

        public static void Save(FavorisUtilisateur pModel)
        {
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
               FavorisUtilisateur FavorisModifier = GetById(pModel.UtilisateurId, pModel.ProduitId, db);
                 //modification
                if(FavorisModifier!=null)
                {
                    Outils.ConnectWebSecurity();
                    FavorisModifier.DateModification = DateTime.Now;
                    FavorisModifier.ModifiePar = WebSecurity.CurrentUserId;
                }
                else
                { //add
                    pModel.DateCreation = DateTime.Now;
                    pModel.DateModification = DateTime.Now;
                    Outils.ConnectWebSecurity();
                    pModel.ModifiePar = WebSecurity.CurrentUserId;
                    pModel.EstSupprime = false;
                    db.FavorisUtilisateurs.AddObject(pModel);
                }
                //enregistrer les modifications
                db.SaveChanges();
            }
        }

    

    }
}