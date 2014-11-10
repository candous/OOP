using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP_ASP.Models.EF;

namespace TP_ASP.Controllers
{
   //[Authorize(Roles = "Admin")]
    public class CategoriesProduitController : Controller
    {
        //
        // GET: /CategoriesProduit/

         public ActionResult ListeCategories()
        {
            List<CategoriesProduit> listeCat=CategoriesProduit.GetAll();
            return View(listeCat);
        }
        [HttpGet]
        public ActionResult Ajouter(int? id)
        {
            CategoriesProduit cat = CategoriesProduit.GetByCategorieId((int)id);
            if (cat == null)
                cat = new CategoriesProduit();

            return View(cat);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Ajouter(CategoriesProduit pModel)
        {
            CategoriesProduit.SaveCategorieProduit(pModel);
            return RedirectToAction("ListeCategories", "CategoriesProduit");
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Ajouter(string pNomCateg)
        {
            CategoriesProduit cat = new CategoriesProduit();
            cat.Description = pNomCateg;
            CategoriesProduit.SaveCategorieProduit(cat);
            return RedirectToAction("ListeCategories", "CategoriesProduit");
        }

        public ActionResult Delete(int pCatId)
        {
            CategoriesProduit.Delete(pCatId);
            return RedirectToAction("ListeCategories", "CategoriesProduit");
        }

    }
}
