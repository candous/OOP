using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebMatrix.WebData;
using System.Web.Security;
using TP_ASP.Tools;

namespace TP_ASP.Models.EF
{
    [MetadataType(typeof(UtilisateurMetaData))]
    public partial class Utilisateur
    {
        //proprietes de confirmation
        public string Courriel_Confirm { get; set; }
        public string MotDePasse { get; set; }
        public string MotDePasse_Confirm { get; set; }
        public HttpPostedFileBase Fichier { get; set; }


        //==========SELECTS=================//
        public static Utilisateur GetProfileById(int pId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                //on a pas de connexion a la bd, c une requete pour chercher l'objet, pas le modifier
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            Utilisateur rValue = pDb.Utilisateurs.Where(m => m.UserProfileId == pId && m.EstSupprime==false).FirstOrDefault();
            //si on a cree la connexion, il faut qu'on la ferme ici
            //si elle vient comme parametre, qui l' envoye va fermer la connexion
            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }

        public static Utilisateur GetById(int pId, MontRealEstateEntities pDb = null)
        {
            bool dbEstNull = false;
            if (pDb == null)
            {
                //on a pas de connexion a la bd, c une requete pour chercher l'objet, pas le modifier
                pDb = new MontRealEstateEntities();
                dbEstNull = true;
            }
            Utilisateur rValue = pDb.Utilisateurs.Include("FavorisUtilisateurs").Include("NotesProduits").Include("CommentaireProduits").Include("Produits").Where(m => m.UserProfileId == pId && m.EstSupprime==false).FirstOrDefault();
            //si on a cree la connexion, il faut qu'on la ferme ici
            //si elle vient comme parametre, qui l' envoye va fermer la connexion
            if (dbEstNull)
                pDb.Dispose();

            return rValue;
        }

        public static List<Utilisateur> GetALL()
        {
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                var rValue = db.Utilisateurs.ToList();
                return rValue;
            }
        }

        public static List<Utilisateur> GetALLActives()
        {
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                var rValue = db.Utilisateurs.Where(m => m.EstSupprime == false).ToList();
                return rValue;
            }
        }

        //==========INSERTS ET UPDATES=================//

        public static void Save(Utilisateur pModel)
        {
            using (MontRealEstateEntities db = new MontRealEstateEntities())
            {
                Utilisateur utilisateurModifier = GetProfileById(pModel.UserProfileId, db);
                //modification
                if (utilisateurModifier != null)
                {
                    utilisateurModifier.Nom = pModel.Nom;
                    utilisateurModifier.Courriel = pModel.Courriel;
                    utilisateurModifier.URLPhotoProfil = pModel.URLPhotoProfil;
                    utilisateurModifier.RecevoirCourriel = pModel.RecevoirCourriel;
                    utilisateurModifier.DateModification = DateTime.Now;
                    utilisateurModifier.ModifiePar = pModel.ModifiePar;
                    utilisateurModifier.Telephone = pModel.Telephone;
                    utilisateurModifier.Adresse = pModel.Adresse;
                    utilisateurModifier.Ville = pModel.Ville;
                    utilisateurModifier.Province = pModel.Province;
                    utilisateurModifier.Pays = pModel.Pays;
                    utilisateurModifier.CodePostal = pModel.CodePostal;
                }
                else //add
                {
                    pModel.DateCreation = DateTime.Now;
                    pModel.DateModification = DateTime.Now;
                    pModel.ModifiePar = pModel.UserProfileId;
                    if (pModel.URLPhotoProfil == null)
                        pModel.URLPhotoProfil = "/Images/PhotosProfiles/profile.jpg";
                    db.Utilisateurs.AddObject(pModel);
                }
                //enregistrer les modifications
                db.SaveChanges();
            }
        }

        public static void SaveUserProfileAndRole(Utilisateur pModel, UserRole role)
        {
            try
            {
                Outils.ConnectWebSecurity();//initialiser la connection
                WebSecurity.CreateUserAndAccount(pModel.Courriel, pModel.MotDePasse);
                int userId = WebSecurity.GetUserId(pModel.Courriel);
                pModel.UserProfileId = userId;
                //ajouter le role pour le user
                string roleFinal=null;
                if (role == UserRole.ADMIN)
                    roleFinal = "Admin";
                else if (role == UserRole.USER)
                    roleFinal = "User";

                Roles.AddUserToRole(pModel.Courriel, roleFinal);
            }
            catch (MembershipCreateUserException e)
            {
               throw e;
            }
        
        }

        //==========DELETES=================//

        public static bool Delete(int pUserId, int pIdQuiSupprime)
        {
            bool retour=false;
            if (pUserId > 0 && pIdQuiSupprime>0)
            {
                using (MontRealEstateEntities db = new MontRealEstateEntities())
                {
                    Utilisateur utilisateurModifier = GetProfileById(pUserId, db);
                    if (utilisateurModifier != null)
                    {
                        utilisateurModifier.EstSupprime = true;
                        utilisateurModifier.ModifiePar = pIdQuiSupprime;
                        utilisateurModifier.DateModification = DateTime.Now;
                        db.SaveChanges();
                        retour = true;
                    }
                }
            }
            return retour;
        }
        

    }

    //MetaDatas pour la classe Utilisateurs
    public class UtilisateurMetaData
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nom*")]
        public string Nom { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Courriel*")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}", ErrorMessage = "La valeur saisie ne correspond pas à un courriel")]
        public string Courriel { get; set; }

        [Required]
        [Compare("Courriel", ErrorMessage = "Les 2 courriels doivent etre identiques")]
        [Display(Name = "Confirmation de Courriel*")]
        public string Courriel_Confirm { get; set; }

        [Display(Name = "Telephone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{10}", ErrorMessage = "Le telephone doit etre compose de 10 chiffres")]
        public string Telephone { get; set; }

        [Display(Name = "Code Postal")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^[A-Z]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$", ErrorMessage = "Le code postal doit etre au format H1H 1H1")]
        public string CodePostal { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de Passe*")]
        [MinLength(5, ErrorMessage = "Min 5 caracteres")]
        public string MotDePasse { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation de mot de passe*")]
        [Compare("MotDePasse", ErrorMessage = "Les 2 mots de passe doivent etre identiques")]
        public string MotDePasse_Confirm { get; set; }

    }

}