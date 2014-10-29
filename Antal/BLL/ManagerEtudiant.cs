using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    static public class ManagerEtudiant
    {
        //Recuperer un etudiant
        static public Etudiant recupererEtudiant(int idEtudiant)
        {
            Etudiant etudiant = RequeteEtudiant.recupererEtudiant(idEtudiant);
            etudiant.Documents = RequeteDocument.recupererDocumentEtudiant(idEtudiant);
            etudiant.Communications = RequeteCommunication.recupererCommunicationEtudiantUtilisateur(idEtudiant);
            etudiant.Entrevues = RequeteEntrevue.recupererEntrevuesParIdEtudiant(idEtudiant);
            etudiant.Entreprises = RequeteEtudiant.recupererListeEntreprisePotentielEtudiant(idEtudiant);
            etudiant.Stages = RequeteStage.recupererStageParIdEtudiant(idEtudiant);
            etudiant.Langues = RequeteEtudiant.recupererLanguesEtudiant(idEtudiant);
            etudiant.Interets = RequeteEtudiant.recupererInteretsEtudiant(idEtudiant);
            etudiant.TechonologiesPreferees = RequeteEtudiant.recupererTechnologiesPrefereesEtudiant(idEtudiant);

            return etudiant;
        }

        //Ajouter un etudiant
        public static int ajouterEtudiant(Etudiant etudiant, List<int> idsInterets, List<int> idsTechnologies, List<Langue> listeLangues)
        {

            int idEtudiant = RequeteEtudiant.ajouterEtudiant(etudiant);

            if (idEtudiant != -1)
            {
                //ajouter dans interetsEtudiant
                foreach (int i in idsInterets)
                {
                    RequeteEtudiant.ajouterInteretEtudiant(idEtudiant, i);
                }
                //ajouter dans technologiesPreferees
                foreach (int i in idsTechnologies)
                {
                    RequeteEtudiant.ajouterTechnologieEtudiant(idEtudiant, i);
                }

                //ajouter dans langueEtudiant
                foreach (Langue langue in listeLangues)
                {
                    RequeteEtudiant.ajouterLangueEtudiant(idEtudiant, langue);
                }

            }

            return idEtudiant;

        }

        //Modifier Etudiant
        static public int modifierEtudiant(Etudiant etudiant, List<int> idsInterets, List<int> idsTechnologies, List<Langue> listeLangues)
        {
            int affectes = RequeteEtudiant.modifierEtudiant(etudiant);

           if (affectes > 0)
           {
               //supprimer toutes les preferences d'un etudiant
                   RequeteEtudiant.deleteInteretEtudiant(etudiant.Id);
                   RequeteEtudiant.deleteTechnologieEtudiant(etudiant.Id);
                   RequeteEtudiant.deleteLangueEtudiant(etudiant.Id);
          
               //ajouter dans interetsEtudiant
               if (idsInterets != null)
               foreach (int i in idsInterets)
               {
                   RequeteEtudiant.ajouterInteretEtudiant(etudiant.Id, i);
               }

               //ajouter dans technologiesPreferees
               if (idsTechnologies != null)
               foreach (int i in idsTechnologies)
               {
                   RequeteEtudiant.ajouterTechnologieEtudiant(etudiant.Id, i);
               }

               //ajouter dans langueEtudiant
               if (listeLangues != null)
               foreach (Langue langue in listeLangues)
               {
                   RequeteEtudiant.ajouterLangueEtudiant(etudiant.Id, langue);
               }
           }
           return affectes;
        }

        //Supprimer Etudiant
        static public void supprimerEtudiant(int idEtudiant)
        {
            RequeteEtudiant.supprimerEtudiant(idEtudiant);

        }
        //profiles pour l'affichage des etudiants avec photo, nom et formation
        public static List<Etudiant> recupererListeProfilesEtudiantsRechercheStage()
        {
            return RequeteEtudiant.recupererListeProfilesEtudiantsRechercheStage();
        }

        public static Etudiant recupererProfilesEtudiant(int idEtudiant)
        {
            return RequeteEtudiant.recupererProfileEtudiantParId(idEtudiant);
        }

        public static List<Etudiant> recupererListeProfilesEtudiantsSelonRecherche(Dictionary<string, string> WhereCondition)
        {
            return RequeteEtudiant.recupererListeProfilesEtudiantsSelonRecherche(WhereCondition);
        }

        //Recuperer etudiant par id
        //affichage du profile de l'etudiant
        static public Etudiant recupererEtudiantParId(int idEtudiant)
        {
            Etudiant etudiant = RequeteEtudiant.recupererEtudiant(idEtudiant);
            etudiant.Documents=RequeteDocument.recupererDocumentEtudiant(idEtudiant);
            etudiant.Communications=RequeteCommunication.recupererCommunicationEtudiantUtilisateur(idEtudiant);
            etudiant.Entrevues=ManagerEntrevue.recupererEntrevuesParIdEtudiant(idEtudiant);
            etudiant.Entreprises=RequeteEtudiant.recupererListeEntreprisePotentielEtudiant(idEtudiant);
            etudiant.Stages=ManagerStage.recupererStageParIdEtudiant(idEtudiant);
            etudiant.Langues=RequeteEtudiant.recupererLanguesEtudiant(idEtudiant);
            etudiant.Interets = RequeteEtudiant.recupererInteretsEtudiant(idEtudiant);
            etudiant.TechonologiesPreferees = RequeteEtudiant.recupererTechnologiesPrefereesEtudiant(idEtudiant);
            
            return etudiant;
        }

        //Recuperer etudiant par le nom
        static public List<Etudiant> recupererEtudiantParleNom(string nom)
        {
            List<Etudiant> etudiants = RequeteEtudiant.recupererEtudiantsParNom(nom);
            return etudiants;
        }

        //Recuperer etudiant par la ville
        static public List<Etudiant> recupererEtudiantParlaVille(string nom)
        {
            List<Etudiant> etudiants = RequeteEtudiant.recupererEtudiantsParVille(nom);
            return etudiants;
        }


        //Recuperer etudiant ayant un emploi
        static public List<Etudiant> recupererEtudiantAvecEmploi()
        {
            List<Etudiant> etudiants = RequeteEtudiant.recupererEtudiantAvecEmploi();
            return etudiants;
        }

        //Recuperer etudiant sans un emploi
        //static public List<Etudiant> recupererEtudiantSansEmploi()
        //{
        //    List<Etudiant> etudiants = RequeteEtudiant.recupererEtudiantSansEmploi();
        //    return etudiants;
        //}

        //Recuperer etudiant avec voiture
        static public List<Etudiant> recupererEtudiantAvecVoiture()
        {
            List<Etudiant> etudiants = RequeteEtudiant.recupererEtudiantAvecVoiture();
            return etudiants;
        }

        //Recuperer etudiant sans voiture
        static public List<Etudiant> recupererEtudiantSansVoiture()
        {
            List<Etudiant> etudiants = RequeteEtudiant.recupererEtudiantSansVoiture();
            return etudiants;
        }

        //Recuperer etudiant sans stage
        static public List<Etudiant> recupererEtudiantSansStage()
        {
            List<Etudiant> etudiants = RequeteEtudiant.recupererEtudiantSansStage();
            return etudiants;
        }

             
        //Modifier status carriere etudiant
        static public void modifierStatusCarriere(int idEtudiant, int idStatusCarriere)
        {
            RequeteEtudiant.modifierStatusCarriereEtudiant(idEtudiant, idStatusCarriere);
        }

    }
}
