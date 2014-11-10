using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TP_ASP.Models;
using TP_ASP.Models.EF;
using TP_ASP.Tools;
using WebMatrix.WebData;

namespace TP_ASP.Controllers
{
    public class UtilisateurController : Controller
    {
        //
        // GET: /Utilisateur/

        public ActionResult ProfileUtilisateur(int pId)
        {
            Utilisateur user= Utilisateur.GetById(pId);
            return View(user);
        }

        //action pour afficher le formulaire pour creer ou modifier un user
        [HttpGet]
        public ActionResult Inscription(int? id)
        {
            Utilisateur user = null;
            //modification d'un utilisateur existant
            if (id > 0 && id.HasValue)
                user = Utilisateur.GetProfileById((int)id);

            if (user == null)
            {
                user = new Utilisateur();
                user.RecevoirCourriel = true;
                user.URLPhotoProfil = "/Images/PhotosProfiles/profile.jpg";
            }
            return View(user);
        }

        //action pour stocker le user dans la BD
        //appeler pas POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inscription(Utilisateur pModel, bool estAdmin)
        {
            if (ModelState.IsValid)
            {
                //copy de la photo dans le serveur
                if (pModel.Fichier != null && pModel.Fichier.ContentLength > 0)
                {
                    try
                    {
                        Outils.SavePhotoUserServer(pModel, Server);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("ERREUR:", ex.Message.ToString());
                    }
                }
                //si modification
                if (pModel.UserProfileId > 0)
                    pModel.ModifiePar = pModel.UserProfileId;
                // ajout d'un user
                else
                {
                    //Enregistrer d'abbord le UserProfile pour apres pouvoir le referencer dans les utilisateurs
                    try
                    {
                        if(!estAdmin)
                            Utilisateur.SaveUserProfileAndRole(pModel, UserRole.USER);
                        else
                            Utilisateur.SaveUserProfileAndRole(pModel, UserRole.ADMIN);
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e.Message.ToString());
                        return View(pModel);
                    }
                }
                Utilisateur.Save(pModel);
                TempData["Message"] = "Utilisateur ajoute";
                WebSecurity.Login(pModel.Courriel, pModel.MotDePasse);
                return RedirectToAction("Index", "Produit");
            }
            //ERREUR dans le modele
            else
                return View(pModel);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginModel logModel = new LoginModel();
            return View(logModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel pModel)
        {
            Outils.ConnectWebSecurity();
            //tester si le user est actif d'abbord
            int idUser = WebSecurity.GetUserId(pModel.UserName);
            //user existe
            if (idUser > 0)
            {
                Utilisateur user = Utilisateur.GetProfileById(idUser);
                if (user != null)//user active
                {
                    bool connecte = WebSecurity.Login(pModel.UserName, pModel.Password);
                    if (connecte)
                    {
                        if (Roles.IsUserInRole("Admin"))
                            return RedirectToAction("", "");//admin connecte
                        else
                            return RedirectToAction("Index", "Produit");//user connecte
                    }
                    else
                    {
                        ModelState.AddModelError("", "Mot de Passe ou Courriel incorrect");
                        return View(pModel);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Inactive");
                    return View(pModel);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Inexistent");
                return View(pModel);
            }
        }



    }
}
