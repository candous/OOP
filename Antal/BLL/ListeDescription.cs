using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    static public class ListeDescription
    {
        static public List<Formation> listFormations;
        static public List<IdDescription> listStatusCarrieres;
        static public List<IdDescription> listStatusResidence;
        static public List<IdDescription> listInterets;
        static public List<IdDescription> listNiveauLangue;
        static public List<IdDescription> listTechnologie;
        static public List<Langue> listLangue;
        static public List<IdDescription> listTypeStage;
        static public List<IdDescription> listTypeCommunication;
        static public List<IdDescription> listTypeEntrevue;
        static public List<IdDescription> listTypeResultat;
        static public List<IdDescription> listTypeDocument;
        static public List<IdDescription> listStatusCommunication;
        static public List<IdDescription> listTypeUlisateur;

        static public  void RemplirList()
        {
            listFormations = ManagerInformation.recupererListFormation();
            listStatusCarrieres = ManagerInformation.recupererListStatusCarriere();
            listStatusResidence = ManagerInformation.recupererListStatusResidence();
            listInterets = ManagerInformation.recupererListInteret();
            listNiveauLangue = ManagerInformation.recupererListNiveauLangue();
            listTechnologie = ManagerInformation.recupererListTechnologie();
            listLangue = ManagerInformation.recupererListLangue();
            listTypeStage = ManagerInformation.recupererListTypeStage();
            listTypeCommunication = ManagerInformation.recupererListTypeCommunication();
            listTypeEntrevue = ManagerInformation.recupererListTypeEntrevue();
            listStatusCommunication = ManagerInformation.recupererListStatusCommunication();
            listTypeResultat = ManagerInformation.recupererListTypeResultat();
            listTypeDocument = ManagerInformation.recupererListTypeDocument();
            listTypeUlisateur = ManagerInformation.recupererTypeUtilisateur();
            
        }


        // Methode  pour recuperer le string de la Formation
        public static String recupererLaFormation(int idFormation) {
            string retour = null;

            int i = 0;
            int tailleListe = listFormations.Count;
            while(i < tailleListe && listFormations[i].Id != idFormation)
                i++;

            if(i < tailleListe)
                retour = listFormations[i].Description;  

            return retour;
        }

        public static String recupererLaLangue(int idLangue) {
            string retour = null;

            int i = 0;
            int tailleListe = listFormations.Count;
            while(i < tailleListe && listLangue[i].Id != idLangue)
                i++;

            if(i < tailleListe)
                retour = listLangue[i].Description;

            return retour;
        }

        public static String recupererLaLangue(int? idLangue) {
            string retour = null;

            int i = 0;
            int tailleListe = listFormations.Count;
            while(i < tailleListe && listLangue[i].Id != idLangue)
                i++;

            if(i < tailleListe)
                retour = listLangue[i].Description;

            return retour;
        }

        // Methode  pour recuperer le string de la description
        public static String recupererDescription(int? id, List<IdDescription>liste) {
            string retour = null;

            int i = 0;
            int tailleListe = liste.Count;
            while(i < tailleListe && liste[i].Id != id)
                i++;

            if(i < tailleListe)
                retour = liste[i].Description;

            return retour;
        }

        // Methode  pour recuperer le string de la description
        public static String recupererDescription(int id, List<IdDescription> liste)
        {
            string retour = null;

            int i = 0;
            int tailleListe = liste.Count;
            while (i < tailleListe && liste[i].Id != id)
                i++;

            if (i < tailleListe)
                retour = liste[i].Description;

            return retour;
        }


        // Methode  pour recuperer le Id de la description depuis String
        public static int recupererIdDescription(string description, List<IdDescription> liste)
        {
            int retour = -1;

            int i = 0;
            int tailleListe = liste.Count;
            while (i < tailleListe && !liste[i].Description.Equals(description))
                i++;

            if (i < tailleListe)
                retour = liste[i].Id;

            return retour;
        }

        // Methode  pour recuperer le id de la Formation depuis une description de la Formation
        public static int recupererIdFormation(string formationDescription)
        {
            int retour = -1;

            int i = 0;
            int tailleListe = listFormations.Count;
            while (i < tailleListe && !listFormations[i].Description.Equals(formationDescription))
                i++;

            if (i < tailleListe)
                retour = listFormations[i].Id;

            return retour;
        }

        // Methode  pour recuperer le id de la Langue depuis une description de la langue
        public static int recupererIdLangue(string langueDescription)
        {
            int retour = -1;

            int i = 0;
            int tailleListe = listFormations.Count;
            while (i < tailleListe && !listLangue[i].Description.Equals(langueDescription))
                i++;

            if (i < tailleListe)
                retour = listLangue[i].Id;

            return retour;
        }

    }
}
