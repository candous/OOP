using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebMatrix.WebData;
using TP_ASP.Tools;

namespace TP_ASP.Models.EF
{
     [MetadataType(typeof(ProduitMetaData))]
    public partial class Produit
    {
        //recuperer un fichier
        public HttpPostedFileBase Fichier { get; set; }

        public static Produit GetById(int pId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                //on a pas de connexion a la bd, c une requete pour chercher l'objet, pas le modifier
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            Produit rValue = pDb.Produits.Include("CommentairesProduits").Include("FonctionnalitesProduit").Include("PhotosProduits").Include("NotesProduits").Include("CategoriesProduit").Include("FavorisUtilisateurs").Where(m => m.Id == pId && m.EstSupprime == false).FirstOrDefault();
            //si on a cree la connexion, il faut qu'on la ferme ici
            //si elle vient comme parametre, qui l' envoye va fermer la connexion
            if (dbEstNull)
                pDb.Dispose();
            return rValue;
        }

        public static Boolean SaveProduit(Produit pModel)
        {
            bool estModifie;

            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {

                //Option lorsque certain champs ne doit pas etre updatés
                if (pModel.Id > 0)
                {
                    Produit modelToSave = Produit.GetById(pModel.Id, db);
                    modelToSave.CategorieProduitId = pModel.CategorieProduitId;
                    modelToSave.UtilisateurId = pModel.UtilisateurId;
                    modelToSave.Nom = pModel.Nom;
                    modelToSave.Description = pModel.Description;
                    modelToSave.PrixParJour = pModel.PrixParJour;
                    modelToSave.Adresse = pModel.Adresse;
                    modelToSave.Ville = pModel.Ville;
                    modelToSave.Province = pModel.Province;
                    modelToSave.Pays = pModel.Pays;
                    modelToSave.CodePostal = pModel.CodePostal;
                   // modelToSave.DerniereDateLocation = pModel.DerniereDateLocation;
                    modelToSave.NbMaxPersonnes = pModel.NbMaxPersonnes;
                    modelToSave.NbChambres = pModel.NbChambres;
                    modelToSave.SejourMinimum = pModel.SejourMinimum;
                    modelToSave.NbChambres = pModel.NbPhotosMax;
                    modelToSave.DateModification = DateTime.Now;
                    Outils.ConnectWebSecurity();
                    modelToSave.ModifiePar = WebSecurity.CurrentUserId;
                    estModifie = true;
                }
                else
                {
                    //logique suplementaire dans le cas d'un New
                    Outils.ConnectWebSecurity();
                    pModel.UtilisateurId = WebSecurity.CurrentUserId;
                    pModel.DateCreation = DateTime.Now;
                    pModel.DateModification = DateTime.Now;
                    pModel.CreePar = WebSecurity.CurrentUserId;
                    pModel.ModifiePar = WebSecurity.CurrentUserId;
                    pModel.Actif = true;
                    pModel.NbPhotosMax = 6;
                    db.Produits.AddObject(pModel);
                    estModifie = false;
                }
                db.SaveChanges();
                FonctionnalitesProduit.SaveFonctionnaliteProduit(pModel.FonctionnalitesProduit);
                foreach (NotesProduit note in pModel.NotesProduits)
                    NotesProduit.Save(note);

                foreach (PhotosProduit photo in pModel.PhotosProduits)
                    PhotosProduit.SavePhotoProduit(photo);
            }

            return true;
        }

        public static Boolean DeleteProduit(int pId)
        {
            bool retour=false;
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {

                //Option lorsque certain champs ne doit pas etre updatés
                if (pId > 0)
                {
                    Produit modelToSave = Produit.GetById(pId, db);
                    modelToSave.EstSupprime = true;
                    db.SaveChanges();
                    retour=true;
                }
            }
            return retour;
        }

        public static List<Produit> GetLastProduitsAjoutes()
        {
            string nb = "20";
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                List<Produit> rValue = db.Produits.Include("PhotosProduits").Top(nb).Where(m=>m.EstSupprime==false).ToList();
                if (rValue == null)
                    rValue = new List<Produit>();
                return rValue;
            }
        }
    }


     //MetaDatas pour la classe Utilisateurs
    public class ProduitMetaData
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nom*")]
        public string Nom { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Decription*")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Prix par jour*")]
        public Decimal PrixParJour { get; set; }


        [Display(Name = "Code Postal")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^[A-Z]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$", ErrorMessage = "Le code postal doit etre au format H1H 1H1")]
        public string CodePostal { get; set; }

        [Required]
        public String Adresse { get; set; }
        
        [Required]
        public String Ville { get; set; }

        [Required]
        public String Province { get; set; }
        
        [Required]
        public String Pays { get; set; }

        //[Display(Name = "Derniere Date de Location")]
        //[DataType(DataType.DateTime)]
        //public DateTime DerniereDateLocation { get; set; } 

        [Required]
        [Display(Name = "Nombre max de personnes")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La valeur saisie doit etre un intier")]
        public int NbMaxPersonnes { get; set; }
        
        [Required]
        [Display(Name = "Nombre de chambres")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La valeur saisie doit etre un intier")]
        public int NbChambres { get; set; }

        [Required]
        [Display(Name = "Sejour Minimum")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La valeur saisie doit etre un intier")]
        public int SejourMinimum { get; set; }

        [Required]
        [Display(Name = "Categorie")]
        public int CategorieProduitId { get; set; }


    }

}
