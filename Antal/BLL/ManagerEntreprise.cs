using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL {
    static public class ManagerEntreprise {
        //Recuperer Entreprise
        static public Entreprise recupererEntreprise(int idEntreprise) {

            Entreprise entreprise = RequeteEntreprise.recupererEntreprise(idEntreprise);
            entreprise.Documents = RequeteDocument.recupererDocumentEntreprise(idEntreprise);
            entreprise.Communications = RequeteCommunication.recupererCommunicationEntrepriseUtilisateur(idEntreprise);
            entreprise.Entrevues = RequeteEntrevue.recupererEntrevuesParIdEntreprise(idEntreprise);
            entreprise.Representants = RequeteEntreprise.recupererRepresentantsEntreprise(idEntreprise);
            ////entreprise.Etudiants = RequeteEntreprise.recupererEtudiantPotentielEntreprise(idEntreprise);
            entreprise.stages = RequeteStage.recupererStageParIdEntreprise(idEntreprise);
            entreprise.FormationsRecherchees = RequeteEntreprise.recupererFormationsRecherchees(idEntreprise);
            entreprise.TechnologiesRecherchees = RequeteEntreprise.recupererTechnologiesRecherchees(idEntreprise);
            entreprise.InteretsRecherches = RequeteEntreprise.recupererInteretsRecherches(idEntreprise);

            return entreprise;
        }

        //profiles pour l'affichage des entreprises avec photo, nom et secteur
        public static List<Entreprise> recupererListeProfilesEntreprises() {
            return RequeteEntreprise.recupererListeProfilesEntreprises();
        }


        public static Entreprise recupererProfilesEntreprises(int idEntreprise) {
            return RequeteEntreprise.recupererProfileEntrepriseParId(idEntreprise);
        }

        //retour d une liste d'entreprises selon le nom, avec photo, nom et secteur
        public static List<Entreprise> recupererListeProfilesEntreprisesSelonNom(string nom) {
            return RequeteEntreprise.recupererListeProfilesEntreprisesParNom(nom);
        }

        //retour d une liste d'entreprises selon la recherche, avec photo, nom et secteur
        public static List<Entreprise> recupererListeProfilesEntreprisesSelonRecherche(Dictionary<string, string> WhereCondition) {
            return RequeteEntreprise.recupererListeProfilesEntreprisesSelonRecherche(WhereCondition);
        }

        //Ajouter Entreprise
        //Ajouter Entreprise
        static public bool ajouterEntreprise(Entreprise Entreprise, List<int> idsFormationsRecherchees, List<int> idsInteretsRecherches, List<int> idsTechnologiesRecherchees) {
            bool cree = false;
            cree = RequeteEntreprise.ajouterEntreprise(Entreprise);
            //ajouter les domaines recherche
            if(cree) {
                if (idsInteretsRecherches != null)
                foreach(int id in idsInteretsRecherches) 
                    RequeteEntreprise.ajouterInteretRecherche(Entreprise.Id, id);                

                if (idsFormationsRecherchees != null)
                foreach(int i in idsFormationsRecherchees) 
                    RequeteEntreprise.ajouterFormationRecherchee(Entreprise.Id, i);     

                if (idsTechnologiesRecherchees != null)
                foreach(int i in idsTechnologiesRecherchees) 
                    RequeteEntreprise.ajouterTechnologieRecherche(Entreprise.Id, i);
            }
            return cree;
        }

        //Modifier Entreprise
        static public bool modifierEntreprise(Entreprise Entreprise, List<int> idsFormationsRecherchees, List<int> idsInteretsRecherches, List<int> idsTechnologiesRecherchees) {
            bool modifie = RequeteEntreprise.modifierEntreprise(Entreprise);
            if(modifie) {
            
                RequeteEntreprise.deleteDomaineRecherche(Entreprise.Id);
                
                if(idsInteretsRecherches != null)                   
                    foreach(int i in idsInteretsRecherches) {
                        RequeteEntreprise.ajouterInteretRecherche(Entreprise.Id, i);
                    }

                if(idsFormationsRecherchees != null)
                    foreach(int i in idsFormationsRecherchees) {
                        RequeteEntreprise.ajouterFormationRecherchee(Entreprise.Id, i);
                    }
                if(idsTechnologiesRecherchees != null)
                    foreach(int i in idsTechnologiesRecherchees) {
                        RequeteEntreprise.ajouterTechnologieRecherche(Entreprise.Id, i);
                    }
            }
            return modifie;
        }

        //Supprimer Entreprise
        static public void supprimerEntreprise(int idEntreprise) {
            RequeteEntreprise.supprimerEntreprise(idEntreprise);
        }


        static public List<Representant> recupererRepresentant(int idEntreprise) {
            return RequeteEntreprise.recupererRepresentantsEntreprise(idEntreprise);
        }



        //Ajouter representant
        static public bool ajouterRepresentant(Representant representant) {
            bool cree = false;

            RequeteEntreprise.ajouterRepresentant(representant);
            return cree;
        }

        //Modifier Representant
        static public void supprimerRepresentant(int idEntreprise) {
            RequeteEntreprise.supprimerRepresentant(idEntreprise);
        }

        //Modifier Representant
        static public bool modifierRepresentant(Representant representant) {
            return RequeteEntreprise.modifierRepresentant(representant);
        }

        //Recuperer representant
        static public Representant recupererRepresentantParId(int idRepresentant) {
            return RequeteEntreprise.recupererRepresentantParId(idRepresentant);
        }

    }
}
