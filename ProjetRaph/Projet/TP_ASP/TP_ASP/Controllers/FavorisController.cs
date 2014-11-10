using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP_ASP.Models.EF;

namespace TP_ASP.Controllers
{
    public class FavorisController : Controller
    {
        //
        // GET: /Favoris/

        public ActionResult Ajouter(FavorisUtilisateur pModel)
        {
            FavorisUtilisateur.Save(pModel);
            return RedirectToAction("ProfileProduit", "Produit", new { pId = pModel.ProduitId });
        }

        public ActionResult Ajouter(int pUserId, int pProdId)
        {
            FavorisUtilisateur favoris = new FavorisUtilisateur();
            favoris.UtilisateurId = pUserId;
            favoris.ProduitId = pProdId;
            FavorisUtilisateur.Save(favoris);
            return RedirectToAction("ProfileProduit", "Produit", new { pId = pProdId });
        }

        public ActionResult Delete(int pUserId, int pProdId)
        {
            FavorisUtilisateur.DeleteById(pUserId,pProdId);
            return RedirectToAction("ProfileProduit", "Produit", new { pId = pProdId });
        }

    }
}
