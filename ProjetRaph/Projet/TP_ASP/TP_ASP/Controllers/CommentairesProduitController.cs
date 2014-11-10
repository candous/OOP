using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP_ASP.Models.EF;

namespace TP_ASP.Controllers
{
    public class CommentairesProduitController : Controller
    {
        //
        // GET: /CommentairesProduit/

        public ActionResult Ajouter(CommentairesProduit pModel)
        {
            CommentairesProduit.Save(pModel);
            return RedirectToAction("ProfileProduit", "Produit", new { pId = pModel.ProduitId });
        }
        public ActionResult Modifier(CommentairesProduit pModel)
        {
            CommentairesProduit.Save(pModel);
            return RedirectToAction("ProfileProduit", "Produit", new { pId = pModel.ProduitId });
        }
        public ActionResult Delete(int pCommentId, int pProdId)
        {
            CommentairesProduit.DeleteById(pCommentId);
            return RedirectToAction("ProfileProduit", "Produit", new {pId = pProdId});
        }

    }
}
