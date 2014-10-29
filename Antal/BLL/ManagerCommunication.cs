using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL {
    public class ManagerCommunication {
        //Ajouter communication entreprise
        static public void ajouterCommunicationEntrepriseUtilisateur(Communication communication) {
            RequeteCommunication.ajouterCommunicationEntrepriseUtilisateur(communication);
        }

        //Ajouter communication Etudiant
        static public void ajouterCommunicationEtudiantUtilisateur(Communication communication) {
            RequeteCommunication.ajouterCommunicationEtudiantUtilisateur(communication);
        }

        //Nombre communication en attente entre utilisateur et l'etudiant
        static public int recupererNbCommunitionEnAttenteUtilisateurEtudiant() {
          return  RequeteCommunication.recupererNbCommunicationEnAttente();
        }

        //Modifier communication Etudiant
        static public bool modifierCommunicationEtudiantUtilisateur(Communication communication) {
            return RequeteCommunication.modifierCommunicationEtudiantUtilisateur(communication);
        }

        //Modifier communication Entreprise
        static public bool modifierCommunicationEntrepriseUtilisateur(Communication communication) {
            return RequeteCommunication.modifierCommunicationEntrepriseUtilisateur(communication);
        }

        //Supprimer communication etudiant
        static public void supprimerCommunicationEtudiantUtilisateur(Communication communication) {
            RequeteCommunication.supprimerCommunicationEtudiantUtilisateur(communication);
        }

        //Supprimer communication entreprise
        static public void supprimerCommunicationEntrepriseUtilisateur(Communication communication) {
            RequeteCommunication.supprimerCommunicationEntrepriseUtilisateur(communication);
        }

        //Retours de la recherche dans la page lister recherches
        public static List<Communication> recupererCommunicationEtudiantUtilisateurRecherche(string recherche) {
            return RequeteCommunication.recupererCommunicationEtudiantUtilisateurRecherche(recherche);
        }
        public static List<Communication> recupererCommunicationEtudiantUtilisateurParId(int idEtudiant) {
            return RequeteCommunication.recupererCommunicationEtudiantUtilisateur(idEtudiant);
        }
        public static List<Communication> recupererCommunicationEntrepriseUtilisateurRecherche(string recherche) {
            return RequeteCommunication.recupererCommunicationEntrepriseUtilisateurRecherche(recherche);
        }
        public static List<Communication> recupererCommunicationEntrepriseUtilisateurParId(int idEtudiant) {
            return RequeteCommunication.recupererCommunicationEntrepriseUtilisateur(idEtudiant);
        }


        //Retours de la recherche dans la page lister recherches
        public static List<Communication> recupererCommunicationsEtudiant() {
            return RequeteCommunication.recupererListeCommunicationEtudiants();
        }
        public static List<Communication> recupererCommunicationsEntreprise() {
            return RequeteCommunication.recupererListeCommunicationEntreprises();
        }

    }
}
