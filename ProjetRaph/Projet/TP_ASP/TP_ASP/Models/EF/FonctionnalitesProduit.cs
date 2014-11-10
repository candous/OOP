using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TP_ASP.Models.EF
{
    [MetadataType(typeof(FonctionnalitesProduitMetaData))]
    public partial class FonctionnalitesProduit
    {
        public static FonctionnalitesProduit GetFonctionnalityByProduitId(int pId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                //on a pas de connexion a la bd, c une requete pour chercher l'objet, pas le modifier
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            FonctionnalitesProduit rValue = pDb.FonctionnalitesProduits.Where(m => m.ProduitId == pId).FirstOrDefault();
            //si on a cree la connexion, il faut qu'on la ferme ici
            //si elle vient comme parametre, qui l' envoye va fermer la connexion
            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }

        public static Boolean SaveFonctionnaliteProduit(FonctionnalitesProduit pModel)
        {

            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {

                //Option lorsque certain champs ne doit pas etre updatés
                if (pModel.ProduitId > 0)
                {
                    FonctionnalitesProduit modelToSave = FonctionnalitesProduit.GetFonctionnalityByProduitId(pModel.ProduitId, db);
                    modelToSave.Frigo = pModel.Frigo;
                    modelToSave.Poele = pModel.Poele;
                    modelToSave.Piscine = pModel.Piscine;
                    modelToSave.Garage = pModel.Garage;
                    modelToSave.Tv = pModel.Tv;
                    modelToSave.Internet = pModel.Internet;
                    modelToSave.Wifi = pModel.Wifi;
                    modelToSave.TvCable = pModel.TvCable;
                    modelToSave.Chauffage = pModel.Chauffage;
                    modelToSave.AirClimatise = pModel.AirClimatise;
                    modelToSave.Baignoire = pModel.Baignoire;
                    modelToSave.Gym = pModel.Gym;
                    modelToSave.DejeunerInclus = pModel.DejeunerInclus;
                    modelToSave.Chauffage = pModel.Chauffage;
                    modelToSave.AirClimatise = pModel.AirClimatise;
                    modelToSave.Baignoire = pModel.Baignoire;
                    modelToSave.MenageInclus = pModel.MenageInclus;
                    modelToSave.FumeurPermis = pModel.FumeurPermis;
                    modelToSave.AnimauxPermis = pModel.AnimauxPermis;
                }
                else
                {

                    //logique suplementaire dans le cas d'un New
                    db.FonctionnalitesProduits.AddObject(pModel);
                }
                db.SaveChanges();
            }

            return true;
        }
    }

    public class FonctionnalitesProduitMetaData
    {
        [Display(Name = "Air Climatise")]
        public bool AirClimatise { get; set; }

        [Display(Name = "Animaux Permis")]
        public bool AnimauxPermis { get; set; }

        [Display(Name = "Dejeuner Inclus")]
        public bool DejeunerInclus { get; set; }

        [Display(Name = "Fumeur Permis")]
        public bool FumeurPermis { get; set; }

        [Display(Name = "Menage Inclus")]
        public bool MenageInclus { get; set; }

        [Display(Name = "Cable Tv")]
        public bool TvCable { get; set; }
    }
}