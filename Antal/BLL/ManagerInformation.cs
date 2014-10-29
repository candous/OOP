using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL {
    static public class ManagerInformation {
        //Recuperer list Formation
        static public List<Formation> recupererListFormation() {
            List<Formation> formations = RequeteInformation.recupererFormations();
            return formations;
        }

        //Recuperer list status Carriere
        static public List<IdDescription> recupererListStatusCarriere() {
            List<IdDescription> statusCarriere = RequeteInformation.recupererStatusCarriere();
            return statusCarriere;
        }

        //Recuperer list status Residence
        static public List<IdDescription> recupererListStatusResidence() {
            List<IdDescription> statusResidence = RequeteInformation.recupererStatusResidence();
            return statusResidence;
        }

        //Recuperer list interet
        static public List<IdDescription> recupererListInteret() {
            List<IdDescription> interets = RequeteInformation.recupererInterets();
            return interets;
        }

        //Recuperer list Niveau langue
        static public List<IdDescription> recupererListNiveauLangue() {
            List<IdDescription> niveauxLangue = RequeteInformation.recupererNiveauLangue();
            return niveauxLangue;
        }


        //Recuperer list Technologie
        static public List<IdDescription> recupererListTechnologie() {
            List<IdDescription> technologies = RequeteInformation.recupererTechnologie();
            return technologies;
        }

        //Recuperer list Langue
        static public List<Langue> recupererListLangue() {
            List<Langue> langues = RequeteInformation.recupererLangue();
            return langues;
        }

        //Recuperer list type Stage
        static public List<IdDescription> recupererListTypeStage() {
            List<IdDescription> typeStage = RequeteInformation.recupererListTypeStage();
            return typeStage;
        }

        //Recuperer List type Communication
        static public List<IdDescription> recupererListTypeCommunication() {
            List<IdDescription> typeCommunication = RequeteInformation.recupererListTypeCommunication();
            return typeCommunication;
        }
        //Recuperer List type resultat
        static public List<IdDescription> recupererListTypeResultat() {

            return RequeteInformation.recupererListTypeResultat();
        }
        //Recuperer List type Entrevue
        static public List<IdDescription> recupererListTypeEntrevue() {
            return RequeteInformation.recupererListTypeEntrevue();
        }
        //Recuperer List type Document
        static public List<IdDescription> recupererListTypeDocument() {
            return RequeteInformation.recupererListTypeDocument();
        }

        //Recuperer list status communication
        static public List<IdDescription> recupererListStatusCommunication() {
            List<IdDescription> statusCommunication = RequeteInformation.recupererListStatusCommunication();
            return statusCommunication;
        }

        //Recuperer list type Utilisateur
        static public List<IdDescription> recupererListTypeUtilisateur() {
            List<IdDescription> typeUtilisateur = RequeteInformation.recupererListTypeUtilisateur();
            return typeUtilisateur;
        }
        //Ajouter formation
        public static void ajouterFormation(Formation formation) {
            RequeteInformation.ajouterFormation(formation);
        }

        //Ajouter status Carriere
        public static void ajouterStatusCarriere(IdDescription statusCarrire) {
            RequeteInformation.ajouterStatusCarrire(statusCarrire);
        }

        //Ajouter status resisidence
        public static void ajouterStatusResidence(IdDescription statusResidence) {
            RequeteInformation.ajouterStatusResidence(statusResidence);
        }

        //Ajouter interet
        public static void ajouterInteret(IdDescription interet) {
            RequeteInformation.ajouterInteret(interet);
        }

        //Ajouter Niveau langue
        public static void ajouterNiveauLangue(IdDescription niveauLangue) {
            RequeteInformation.ajouterNiveauLangue(niveauLangue);
        }

        //Ajouter Technologie
        public static void ajouterNiveauTechnologie(IdDescription technologie) {
            RequeteInformation.ajouterTechnologie(technologie);
        }

        //Ajouter type stage
        public static void ajouterTypeStage(IdDescription typeStage) {
            RequeteInformation.ajouterTypeStage(typeStage);
        }

        //Ajouter type stage
        public static void ajouterTypeResultat(IdDescription typeStage) {
            RequeteInformation.ajouterTypeResultat(typeStage);
        }

        //Ajouter type stage
        public static void ajouterTypeEntrevue(IdDescription typeStage) {
            RequeteInformation.ajouterTypeEntrevue(typeStage);
        }
        //Ajouter type stage
        public static void ajouterTypeDocument(IdDescription typeStage) {
            RequeteInformation.ajouterTypeDocument(typeStage);
        }
        //Ajouter type communication
        public static void ajouterTypeCommunication(IdDescription typeCommunication) {
            RequeteInformation.ajouterTypeCommunication(typeCommunication);
        }

        //Ajouter status communication
        public static void ajouterStatusCommunication(IdDescription statusCommunication) {
            RequeteInformation.ajouterStatusCommunication(statusCommunication);
        }


        //Ajouter type utilisateur
        public static void ajouterTypeUtilisateur(IdDescription typeUtilisateur) {
            RequeteInformation.ajouterTypeUtilisateur(typeUtilisateur);
        }

        //Ajouter type utilisateur
        public static void ajouterLangue(Langue langue) {
            RequeteInformation.ajouterLangue(langue);
        }

        //Supprimer formation
        public static void supprimerFormation(int idFormation) {
            RequeteInformation.supprimerFormation(idFormation);
        }

        //Supprimer status Carriere
        public static void supprimerStatusCarriere(int idStatusCarrire) {
            RequeteInformation.supprimerStatusCarriere(idStatusCarrire);
        }

        //Supprimer status resisidence
        public static void supprimerStatusResidence(int idStatusResidence) {
            RequeteInformation.supprimerStatusResidence(idStatusResidence);
        }

        //Supprimer interet
        public static void supprimerInteret(int idInteret) {
            RequeteInformation.supprimerInteret(idInteret);
        }

        //Supprimer Niveau langue
        public static void supprimerNiveauLangue(int idNiveauLangue) {
            RequeteInformation.supprimerNiveauLangue(idNiveauLangue);
        }

        //Supprimer Technologie
        public static void supprimerTechnologie(int idTechnologie) {
            RequeteInformation.supprimerTechnologie(idTechnologie);
        }

        //Supprimer type stage
        public static void supprimerTypeStage(int idTypeStage) {
            RequeteInformation.supprimerTypeStage(idTypeStage);
        }

        //Supprimer type communication
        public static void supprimerTypeCommunication(int idTypeCommunication) {
            RequeteInformation.supprimerTypeCommunication(idTypeCommunication);
        }

        //Supprimer type communication
        public static void supprimerTypeEntrevue(int id) {
            RequeteInformation.supprimerTypeEntrevue(id);
        }

        //Supprimer type communication
        public static void supprimerTypeResultat(int id) {
            RequeteInformation.supprimerTypeResultat(id);
        }

        //Supprimer type communication
        public static void supprimerTypeDocument(int id) {
            RequeteInformation.supprimerTypeDocument(id);
        }

        //Supprimer status communication
        public static void supprimerStatusCommunication(int idStatusCommunication) {
            RequeteInformation.supprimerStatusCommunication(idStatusCommunication);
        }

        //Supprimer Langue
        public static void supprimerLangue(int idLangue) {
            RequeteInformation.supprimerLangue(idLangue);
        }

        //Type Utlisateur
        static public List<IdDescription> recupererTypeUtilisateur() {
            List<IdDescription> typeUtlisateur = RequeteInformation.recupererTypeUtilisateur();
            return typeUtlisateur;
        }

    }
}
