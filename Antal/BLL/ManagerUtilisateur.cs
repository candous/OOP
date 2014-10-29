using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   static public class ManagerUtilisateur
    {

       //Recuperer Utlisateur connectee
       static public Utilisateur recupererUtilisateurConnecte(string email, string password)
       {
          Utilisateur utilisateur =  RequeteUtilisateur.recupererUtilisateurConnecte(email, password);
          return utilisateur;
       }

       static public Utilisateur recupererUtilisateurParId(int IdUtilisateur)
       {
           return RequeteUtilisateur.recupererUtilisateurParId(IdUtilisateur);           
       }

       

       //Ajouter Utilisateur
       static public void ajouterUtilisateur(Utilisateur utilisateur)
       {
           RequeteUtilisateur.ajouterUtilisateur(utilisateur);
       }

       //Supprimer Utilisateur
       static public void SupprimerUtilisateur(int idUtilisateur)
       {
           RequeteUtilisateur.supprimerUtilisateur(idUtilisateur);
       }

       //Modifi Utilisateur
       static public void modifierUtilisateur(Utilisateur utilisateur)
       {
           RequeteUtilisateur.modifierUtilisateur(utilisateur);
       }

       //Lister utilisateur
       static public List<Utilisateur> recupererListUtilisateur()
       {
           List<Utilisateur> listeUtilisateur = RequeteUtilisateur.recupererListUtilisateur();
           return listeUtilisateur;
       }
    }
}
