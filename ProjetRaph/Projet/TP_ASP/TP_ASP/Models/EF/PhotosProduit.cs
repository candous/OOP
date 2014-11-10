using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP_ASP.Models.EF
{
    public partial class PhotosProduit
    {
        public static int CountNbPhotosProduit(int pProdId, MontRealEstateEntities pDb = null)
        { 
            int nb=0;
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                nb = db.PhotosProduits.Count(m => m.ProduitId == pProdId && m.EstSupprime == false);
            }
            return nb;
        }


         public static List<PhotosProduit> GetPhotosProduitById(int pProdId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                //on a pas de connexion a la bd, c une requete pour chercher l'objet, pas le modifier
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            List<PhotosProduit> rValue = pDb.PhotosProduits.Where(m => m.ProduitId == pProdId && m.EstSupprime == false).ToList();
            //si on a cree la connexion, il faut qu'on la ferme ici
            //si elle vient comme parametre, qui l' envoye va fermer la connexion
            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }

        public static PhotosProduit GetPhotosById(int pId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                //on a pas de connexion a la bd, c une requete pour chercher l'objet, pas le modifier
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            PhotosProduit rValue = pDb.PhotosProduits.Where(m => m.Id == pId && m.EstSupprime==false).FirstOrDefault();
            //si on a cree la connexion, il faut qu'on la ferme ici
            //si elle vient comme parametre, qui l' envoye va fermer la connexion
            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }

        public static PhotosProduit GetPhotoProfilByProduitId(int pProdId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                //on a pas de connexion a la bd, c une requete pour chercher l'objet, pas le modifier
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            PhotosProduit rValue = pDb.PhotosProduits.Where(m => m.ProduitId == pProdId && m.EstProfil==true && m.EstSupprime == false).FirstOrDefault();
            //si on a cree la connexion, il faut qu'on la ferme ici
            //si elle vient comme parametre, qui l' envoye va fermer la connexion
            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }

        public static Boolean SavePhotoProduit(PhotosProduit pModel)
        {

            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                if (pModel.Id > 0)
                {
                    PhotosProduit modelToSave = PhotosProduit.GetPhotosById(pModel.Id, db);
                    modelToSave.ProduitId = pModel.ProduitId;
                    modelToSave.URLPhoto = pModel.URLPhoto;
                    modelToSave.EstProfil = pModel.EstProfil;
                    modelToSave.DateCreation = pModel.DateCreation;
                    modelToSave.ModifiePar = pModel.ModifiePar;
                    modelToSave.EstSupprime = pModel.EstSupprime;
                }
                else
                {
                    pModel.DateCreation = DateTime.Now;
                    db.PhotosProduits.AddObject(pModel);
                }
                db.SaveChanges();
            }

            return true;
        }

        public static void Delete(int id)
        {
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                PhotosProduit modelToDelete = PhotosProduit.GetPhotosById(id, db);
                modelToDelete.EstSupprime = true;
                db.SaveChanges();
            }
        }
    }
    
}