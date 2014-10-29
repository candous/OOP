using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL {
    public static class ManagerStatistique {

      
        //nombre total d'etudiants
        public static int recupererNbEtudiants() {
            return RequeteStatistique.recupererNbEtudiants();
        }

        public static int recupererNbEtudiantsStage() {
            return RequeteStatistique.NbTousEtudiantsStage();
        }

        public static int recupererNbEtudiantsPasPlaces()
        {
            return RequeteStatistique.recupererNbEtudiantsPasPlaces();
        }
        //nombre d'etudiants places apres certain delais 
        public static int recupererNbEtudiantsPlacesAvantDelais(int joursApresFin) {
            return RequeteStatistique.recupererNbEtudiantsPlacesAvantDelais(joursApresFin);
        }
        public static int recupererNbEtudiantsPlacesApresDelais(int joursApresFin)
        {
            return RequeteStatistique.recupererNbEtudiantsPlacesApresDelais(joursApresFin);
        }
        //places avant delais (difference entre general et places apres)


        //--//--//--//--//--//--//--//--//--//--//

        /// nombre d'etudiants places avant ou apres delais par AN
        /// <param name="joursApresFin"></param>
        /// <param name="annee"></param>
        public static int recupererNbEtudiantsPlacesApresDelaisParAn(int joursApresFin, int annee) {
            return RequeteStatistique.recupererNbEtudiantsPlacesApresDelaisParAn(joursApresFin, annee);
        }
        public static int recupererNbEtudiantsPlacesAvantDelaisParAn(int joursApresFin, int annee)
        {
            return RequeteStatistique.recupererNbEtudiantsPlacesAvantDelaisParAn(joursApresFin, annee);
        }
        //nombre d'etudiants par an
        public static int recupererNbEtudiantsParAn(int anneeFin) {
            return RequeteStatistique.recupererNbEtudiantsParAn(anneeFin);
        }

        public static int recupererNbEtudiantsJamaisPlacesParAn(int anneeFin)
        {
            return RequeteStatistique.recupererNbEtudiantsJamaisPlacesParAn(anneeFin);
        }

        public static int recupererNbEtudiantsPlacesAvantDelaisParAnParFormation(int joursApresFin, int annee, int idFormationSelect) {
            return RequeteStatistique.recupererNbEtudiantsPlacesAvantDelaisParAnParFormation(joursApresFin, annee, idFormationSelect);
        }

        public static int recupererNbEtudiantsPlacesApresDelaisParAnParFormation(int joursApresFin, int annee, int idFormationSelect)
        {
            return RequeteStatistique.recupererNbEtudiantsPlacesApresDelaisParAnParFormation(joursApresFin, annee, idFormationSelect);
        }


        public static int recupererNbEtudiantsJamaisPlacesParAnParFormation(int anneeFin, int idFormation) {
            return RequeteStatistique.recupererNbEtudiantsJamaisPlacesParAnParFormation(anneeFin, idFormation);
        }

        //--//--//--//--//--//--//--//--//--//--//

        /// nombre d'etudiants places par AN
        /// <param name="annee">annee pour laquelle on veut les statistiques</param>
        /// <returns>nombre d'etudiants places par an</returns>
        public static int recupererNbEtudiantsPlacesParAn(int annee) {
            return RequeteStatistique.recupererNbEtudiantsPlacesParAn(annee);
        }

        public static int recupererNbEtudiantsParAnFormationFin(int anneeFinFormation) {
            return RequeteStatistique.recupererNbEtudiantsParAn(anneeFinFormation);
        }

        public static int recupererNbEtudiantsJamaisPlacesParAnFinFormation(int anneeFin)
        {
            return RequeteStatistique.recupererNbEtudiantsJamaisPlacesParAnFinFormation(anneeFin);
        }

        //--//--//--//--//--//--//--//--//--//--//
        //requetes accueil 
        public static int recupererNbEtudiantsRecherche() {
            return RequeteStatistique.recupererNbEtudiantsRecherche();
        }

        public static int recupererNbEntrevuesDans10Jours() {
            return RequeteStatistique.recupererNbEntrevuesDans10Jours();
        }

        public static int recupererNbEntreprisesEnregistrees() {
            return RequeteStatistique.recupererNbEntreprisesEnregistrees();
        }

        //--//--//--//--//--//--//--//--//--//--//

        /// Nombre d'etudiants places en stage par formation
        /// <param name="idFormation">id de la formation</param>
        public static int NbEtudiantsPlacesStageParFormation(int idFormation) {
            return RequeteStatistique.NbEtudiantsPlacesStageParFormation(idFormation);
        }

        //total d'etudiants dans formation
        public static int NbEtudiantsParFormation(int idFormation) {
            return RequeteStatistique.NbEtudiantsParFormation(idFormation);
        }

        //--//--//--//--//--//--//--//--//--//--//

        //nb etudiants placés en stage par formation ET an
        public static int NbEtudiantsPlacesStageParFormationEtAn(int idFormation, int annee) {
            return RequeteStatistique.NbEtudiantsPlacesStageParFormationEtAn(idFormation, annee);
        }

        public static int NbEtudiantsParFormationEtAn(int idFormation, int annee) {
            return RequeteStatistique.NbEtudiantsParFormationEtAn(idFormation, annee);
        }


        //--//--//--//--//--//--//--//--//--//--//

        //nb etudiants placés en stage par an et date de fin de formation
        public static int NbEtudiantsSansStageParAnEtFinFormation(int anFinFormation, int anPlacement) {
            return RequeteStatistique.NbEtudiantsSansStageParAnEtFinFormation(anFinFormation, anPlacement);
        }


        //--//--//--//--//--//--//--//--//--//--//


        //nb etudiant en emplois par an selon suite stage 
        public static int NbEtudiantsRetenusParAn(int anPlacement) {
            return RequeteStatistique.NbEtudiantsRetenusParAn(anPlacement);
        }



        //nb etudiant en emplois par an selon suite stage 
        public static int NbEtudiantsEnStageParAn(int anPlacement) {
            return RequeteStatistique.NbEtudiantsEnStageParAn(anPlacement);
        }


        //--//--//--//--//--//--//--//--//--//--//

        //nb etudiant en emplois par formation par an selon suite stage
        public static int NbEtudiantsRetenusParAnEtFormation(int anFin, int idFormation) {
            return RequeteStatistique.NbEtudiantsRetenusParAnEtFormation(anFin, idFormation);
        }

        public static int NbEtudiantsPlacesParAnEtFormation(int anPlacement, int idFormation) {
            return RequeteStatistique.NbEtudiantsPlacesParAnEtFormation(anPlacement, idFormation);
        }

        public static int NbEtudiantParAnEtFormation(int anneeFin, int IdFormation)
        {
            return RequeteStatistique.NbEtudiantParAnEtFormation(anneeFin, IdFormation);
        }



        //--//--//--//--//--//--//--//--//--//--//

        //nb pas retenus par an
        public static int NbEtudiantsPasRetenusParAn(int anFinFormation)//faut etre an placement
        {
            return RequeteStatistique.NbEtudiantsPasRetenusParAn(anFinFormation);
        }
        //nb pas retenus par an et formation
        public static int NbEtudiantsPasRetenusParAnEtFormation(int anFinFormation, int idFormation) {
            return RequeteStatistique.NbEtudiantsPasRetenusParAnEtFormation(anFinFormation, idFormation);
        }

        //--//--//--//--//--//--//--//--//--//--//

        public static int NbEtudiantsEnEmploiParAnFormation(int anFinFormation) {
            return RequeteStatistique.NbEtudiantsEnEmploiParAnFormation(anFinFormation);
        }
        public static int NbEtudiantsParAnDeFormation(int anFinFormation) {
            return RequeteStatistique.NbEtudiantsParAnDeFormation(anFinFormation);
        }


        //--//--//--//--//--//--//--//--//--//--//

        //pas d emploi et pas de stage par an de formation
        public static int NbEtudiantsEnRechercheParAnFormation(int anFinFormation) {
            return RequeteStatistique.NbEtudiantsEnRechercheParAnFormation(anFinFormation);
        }

        //en stage par an de formation
        public static int NbEtudiantsEnStageParAnFormation(int anFinFormation) {
            return RequeteStatistique.NbEtudiantsEnStageParAnFormation(anFinFormation);
        }

        //--//--//--//--//--//--//--//--//--//--//

        //nombre d'etudiants avec emploi (tous les etudiant dans le systeme)
        public static int NbTousEtudiantsAvecEmploi() {
            return RequeteStatistique.NbTousEtudiantsAvecEmploi();
        }

        //sans emploi (en stage ou a la recherche)
        public static int NbTousEtudiantsSansEmploi() {
            return RequeteStatistique.NbTousEtudiantsSansEmploi();
        }

        //tous les etudiants a la recherche)
        public static int NbTousEtudiantsRecherche() {
            return RequeteStatistique.NbTousEtudiantsRecherche();
        }

        public static int NbTousEtudiantsEnStage() {
            return RequeteStatistique.NbTousEtudiantsEnStage();
        }


        //--//--//--//--//--//--//--//--//--//--//

        //--nb etudiant avec emplois sans emplois pour toutes les années en fonction des formations
        //--avec emploi par formation
        public static int NbEtudiantsAvecEmploiParFormation(int idFormation) {
            return RequeteStatistique.NbEtudiantsAvecEmploiParFormation(idFormation);
        }

        //--sans emploi par formation(stage ou recherche)
        public static int NbEtudiantsSansEmploiParFormation(int idFormation) {
            return RequeteStatistique.NbEtudiantsSansEmploiParFormation(idFormation);
        }

        //--en recherche par formation
        public static int NbEtudiantsRechercheParFormation(int idFormation) {
            return RequeteStatistique.NbEtudiantsRechercheParFormation(idFormation);
        }
        public static int NbEtudiantsEnStageParFormation(int idFormation) {
            return RequeteStatistique.NbEtudiantsEnStageParFormation(idFormation);
        }

        //--//--//--//--//--//--//--//--//--//--//

        //--nb entreprise donnant salaire ou pas salaire
        //avec salaire
        public static int NbEntreprisesAvecSalaire() {
            return RequeteStatistique.NbEntreprisesAvecSalaire();
        }

        //sans salaire
        public static int NbEntreprisesSansSalaire() {
            return RequeteStatistique.NbEntreprisesSansSalaire();
        }

        //--//--//--//--//--//--//--//--//--//--//

        //--nb entreprise donnant salaire ou pas salaire en fonction des formations
        public static int NbEntreprisesAvecSalaireParFormation(int idFormation)
        {
            return RequeteStatistique.NbEntreprisesAvecSalaireParFormation(idFormation);
        }

        //--nb entreprise donnant salaire ou pas salaire en fonction des formations
        public static int NbEntreprisesSansSalaireParFormation(int idFormation){
            return RequeteStatistique.NbEntreprisesSansSalaireParFormation(idFormation);
        }

        //premier an qui a un etudiant dans la base
        public static int anneeEtudiantPlusAncien()
        {
            return RequeteStatistique.anneeEtudiantPlusAncien();
        }

       
    }
}
