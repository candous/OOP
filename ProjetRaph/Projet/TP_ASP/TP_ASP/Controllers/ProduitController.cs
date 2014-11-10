using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP_ASP.Models.EF;
using TP_ASP.Tools;

namespace TP_ASP.Controllers
{
    [Authorize]
    public class ProduitController : Controller
    {
        //
        // GET: /Produit/

        //page d'accueil
        public ActionResult Index()
        {
            //profiles des produits
            List<Produit> list = Produit.GetLastProduitsAjoutes();
            return View(list);
        }
        public ActionResult Rechercher()
        {
           // List<Produit> list = Produit.GetLastProduitsAjoutes();
           // return View(list);
            return View();
        }

        //page de profile du produit
        public ActionResult ProfileProduit(int pId)
        {
            Produit prod = Produit.GetById(pId);
            return View(prod);
        }
        //modifier
        [HttpGet]
        public ActionResult DetailProduit(int? pId)
        {
            Produit prod = null;
            //modification d'un utilisateur existant
            if (pId > 0 && pId.HasValue)
                prod = Produit.GetById((int)pId);

            if (prod == null)
            {
                prod = new Produit();
            }
            return View(prod);
        }
        //ajouter un imeuble
        [HttpPost]
        public ActionResult DetailProduit(Produit pModel)
        {
            if (ModelState.IsValid)
            {
                Produit.SaveProduit(pModel);

                //copy de la photo dans le serveur
                if (pModel.Fichier != null && pModel.Fichier.ContentLength > 0)
                {
                    bool imageAjoute=Outils.SavePhotoProduitServer(pModel, Server, true);
                    if (imageAjoute)
                    {
                        TempData["Message"] = "Imeuble ajoute avec acces";
                        return RedirectToAction("ProfileProduit", "Produit", new { pId = pModel.Id });
                    }
                    else {
                        ModelState.AddModelError("", "La photo de l'imeuble n'a pas ete sauvegarde");
                        return View(pModel);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "La photo de l'imeuble est obligatoire");
                    return View(pModel);
                }
            }
            //ERREUR dans le modele
            else
                return View(pModel);
        }

        public ActionResult DeleteProduit(int pIdProduit, int pUserId)
        {
            if (Produit.DeleteProduit(pIdProduit))
            {
                return RedirectToAction("ProfileUtilisateur", "Utilisateur", new { pId = pUserId});
            }
            //ERREUR
            return RedirectToAction("DetailProduit", new { pId = pIdProduit });
        }
    }
}
