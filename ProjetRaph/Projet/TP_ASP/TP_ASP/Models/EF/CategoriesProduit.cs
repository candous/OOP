using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP_ASP.Tools;
using WebMatrix.WebData;

namespace TP_ASP.Models.EF
{
    public partial class CategoriesProduit
    {
        public static IEnumerable<SelectListItem> GetSelectList(int? pSelectdValue)
        {
            //recuperer la valeur du champ
            int selValue = pSelectdValue.HasValue ? pSelectdValue.Value : -1;

            //aller vers la BD avec nom de la BD suivi de Entites
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                //aller vers la BD, dans la table REFERENCE e chaque NAME, convertir en liste
                //caster la liste pour retourner le bon type de valeur ou faire:
                List<CategoriesProduit> rValue = db.CategoriesProduits.ToList();
                IEnumerable<SelectListItem> listeItems = rValue.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Description, Selected = (d.Id == selValue) });
                return listeItems;
            }
        }

        public static List<CategoriesProduit> GetAll(MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                //on a pas de connexion a la bd, c une requete pour chercher l'objet, pas le modifier
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            List<CategoriesProduit> rValue = pDb.CategoriesProduits.ToList();
            //si on a cree la connexion, il faut qu'on la ferme ici
            //si elle vient comme parametre, qui l' envoye va fermer la connexion
            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }

        public static CategoriesProduit GetByCategorieId(int pId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                //on a pas de connexion a la bd, c une requete pour chercher l'objet, pas le modifier
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            CategoriesProduit rValue = pDb.CategoriesProduits.Where(m => m.Id == pId).FirstOrDefault();
            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }

        public static Boolean SaveCategorieProduit(CategoriesProduit pModel)
        {

            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {

                //Option lorsque certain champs ne doit pas etre updatés
                if (pModel.Id > 0)
                {
                    CategoriesProduit modelToSave = CategoriesProduit.GetByCategorieId(pModel.Id, db);
                    modelToSave.Description = pModel.Description;
                    modelToSave.DateCreation = pModel.DateCreation;
                    modelToSave.CreePar = pModel.CreePar;
                    modelToSave.DateModification = pModel.DateModification;
                    modelToSave.ModifiePar = pModel.ModifiePar;
                    modelToSave.EstSupprime = pModel.EstSupprime;

                }
                else
                {
                    Outils.ConnectWebSecurity();
                    pModel.ModifiePar = WebSecurity.CurrentUserId;
                    pModel.CreePar = WebSecurity.CurrentUserId;
                    db.CategoriesProduits.AddObject(pModel);
                }
                db.SaveChanges();
            }

            return true;
        }

        public static void Delete(int id)
        {
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                CategoriesProduit modelToDelete = CategoriesProduit.GetByCategorieId(id, db);
                Outils.ConnectWebSecurity();
                modelToDelete.ModifiePar = WebSecurity.CurrentUserId;
                modelToDelete.EstSupprime = true;
                db.SaveChanges();

            }

        }

    }
}