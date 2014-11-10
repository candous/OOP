using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP_ASP.Models.EF;
using TP_ASP.Tools;

namespace TP_ASP.Controllers
{
    public class PhotosProduitController : Controller
    {
        //
        // GET: /PhotosProduit/

        public ActionResult ChangerImageProfile(Produit pModel)
        {
            Outils.SavePhotoProduitServer(pModel, Server, true);
            return RedirectToAction("DetailProduit", "Produit", new { pId = pModel.Id });
        }
        //ajouter photo et retourner vers les details de l'imeuble
        public ActionResult Ajouter(Produit pModel)
        {
            Outils.SavePhotoProduitServer(pModel, Server, false);
            return RedirectToAction("DetailProduit", "Produit", new { pId = pModel.Id });
        }

        //delete photo et retourne vers les details de l'imeuble
        public ActionResult Delete(int pIdPhoto, int pIdProd)
        {
            PhotosProduit.Delete(pIdPhoto);
            return RedirectToAction("DetailProduit","Produit", new { pId = pIdProd });
        }

        

    }
}
