using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TP_ASP.Models.EF;
using WebMatrix.WebData;

namespace TP_ASP.Tools
{
    public static class Outils
    {
        //connectionString
        private static string server = @"VIEWW7-2013-4\SQLEXPRESS";
        private static string database = @"MontRealEstate";
        public static string ConnectionStringCommun = @"Data source=" + server + ";Initial catalog = " + database + "; Integrated security= true";


        public static void SavePhotoUserServer(Utilisateur pUser, HttpServerUtilityBase Server)
        {
            try
            {
                string path2 = System.Guid.NewGuid().ToString() + pUser.Fichier.FileName;
                string path = Path.Combine(Server.MapPath("~/Images/PhotosProfiles"), path2);
                pUser.Fichier.SaveAs(path);
                pUser.URLPhotoProfil = "/Images/PhotosProfiles/" + path2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool SavePhotoProduitServer(Produit pProd, HttpServerUtilityBase Server, bool isProfile)
        {
            try
            {
                if (isProfile)
                {
                    //effacer la photo de profile
                    PhotosProduit photoProfile = PhotosProduit.GetPhotoProfilByProduitId(pProd.Id);

                    if (photoProfile != null)
                    {
                        PhotosProduit.Delete(photoProfile.Id);
                    }
                }
                if(pProd.NbPhotosMax>PhotosProduit.CountNbPhotosProduit(pProd.Id))
                {
                    string path2 = System.Guid.NewGuid().ToString() + pProd.Fichier.FileName;
                    string path = Path.Combine(Server.MapPath("~/Images/PhotosProduits"), path2);
                    pProd.Fichier.SaveAs(path);
                    string URL="/Images/PhotosProduits" + path2;

                    PhotosProduit nouvPhoto = new PhotosProduit();
                    nouvPhoto.EstSupprime = false;
                    nouvPhoto.ModifiePar = WebSecurity.CurrentUserId;
                    nouvPhoto.ProduitId = pProd.Id;
                    nouvPhoto.URLPhoto = URL;
                    nouvPhoto.EstProfil = isProfile;

                    PhotosProduit.SavePhotoProduit(nouvPhoto);
                    return true;
                }
                else
                    return false; //max photos
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }


        public static void ConnectWebSecurity()
        {
            //initialiser la connection
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }
    }
}