using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    static public class  ManagerStage
    {
        //Recuperer tous les stage
        static public List<Stage> recupererListStage()
        {
            List<Stage> stage = RequeteStage.recupererListStage();
            return stage;
        }
        public static List<Stage> recupererListeStageParNomEntreprise(string recherche)
        {
            return RequeteStage.recupererListeStageParNomEntreprise(recherche);
        }
        public static List<Stage> recupererListeStageParNomEtudiant(string recherche)
        {
            return RequeteStage.recupererListeStageParNomEtudiant(recherche);
        }

        static public List<Stage> recupererListStageRecherche(string recherche)
        {
            return RequeteStage.recupererListStageRecherche(recherche);
        }
        //Ajouter stage
        static public bool ajouterStage(Stage stage)
        {
            bool cree = RequeteStage.ajouterStage(stage);
            if (cree)
            {
                //si le stage est cree, on change le status de la carriere de l'etudiant
                ManagerEtudiant.modifierStatusCarriere(stage.IdEtudiant, 3);
            }
            return cree;
        }

        //Supprimer stage
        static public void supprimerStage(int idStage)
        {
            RequeteStage.supprimerStage(idStage);
        }

        //Modifier stage
        static public bool modifierStage(Stage stage)
        {
            //changer le status de l'etudiant pour en emploi
            if (stage.Retenu == true)
                RequeteEtudiant.modifierStatusCarriereEtudiant(stage.IdEtudiant, 2);
            return RequeteStage.modifierStage(stage);
        }
        //recuperer liste de stages d' un etudiant
        public static List<Stage> recupererStageParIdEtudiant(int idEtudiant)
        {
            return RequeteStage.recupererStageParIdEtudiant(idEtudiant);
        }

        //Recuperer stage par son id
        public static Stage recupererStageParId(int idStage)
        {
            return RequeteStage.recupererStageParId(idStage);
        }
    }
}
