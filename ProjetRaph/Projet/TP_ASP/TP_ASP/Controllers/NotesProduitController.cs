using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP_ASP.Models.EF;
using WebMatrix.WebData;

namespace TP_ASP.Controllers
{
    public class NotesProduitController : Controller
    {
        //
        // GET: /NotesProduit/

        public ActionResult AjouterModel(NotesProduit pModel)
        {
            NotesProduit.Save(pModel);
            //retourner au profile du produit modifie
            return RedirectToAction("ProfileProduit", "Produit", new { pId = pModel.ProduitId });
        }

        public ActionResult Ajouter(double pNoteConfort, double pNoteProprete, double pNoteLocalisation, double pNoteValeur, int pProdID)
        {
            NotesProduit notes = new NotesProduit();
            notes.Confort = pNoteConfort;
            notes.Proprete = pNoteProprete;
            notes.Localisation = pNoteLocalisation;
            notes.Valeur = pNoteValeur;
            notes.ProduitId = pProdID;
            notes.UtilisateurId = WebSecurity.CurrentUserId;
            NotesProduit.Save(notes);
            //retourner au profile du produit modifie
            return RedirectToAction("ProfileProduit", "Produit", new { pId = pProdID });
        }
       
         public ActionResult Delete(int pProdId)
        {
             int userId=WebSecurity.CurrentUserId;
            NotesProduit.DeleteById(userId,pProdId);
             //retourner au profile du user qui a delete la note
            return RedirectToAction("ProfileUtilisateur", "Utilisateur", new { pId = userId });
        }
    }
}
