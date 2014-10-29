using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    static public class ManagerDocument
    {
        //Ajouter document d'un etudiant
        public static void ajouterDocumentEtudiant(Document document)
        {
            RequeteDocument.ajouterDocumentEtudiant(document);
        }

        //Ajouter document entreprise
        public static void ajouterDocumentEntreprise(Document document)
        {
            RequeteDocument.ajouterDocumentEntreprise(document);
        }

        //Modifier document entreprise
        public static void modifierDocumentEntreprise(Document document)
        {
            RequeteDocument.modifierDocumentEntreprise(document);
        }

        //Modifier document entreprise
        public static void modifierDocumentEtudiant(Document document)
        {
            RequeteDocument.modifierDocumentEtudiant(document);
        }

        //Supprimer document etudiant
        public static void supprimerDocumentEtudiant(int idDocument)
        {
            RequeteDocument.supprimerDocumentEtudiant(idDocument);
        }

        //Supprimer document entreprise
        public static void supprimerDocumentEntreprise(int idDocument)
        {
            RequeteDocument.supprimerDocumentEntreprise(idDocument);
        }

        //Recuperer document entreprise
        public static List<Document> recupererDocumentEntreprise(int idEntreprise)
        {
            List<Document> documents = 
            documents = RequeteDocument.recupererDocumentEntreprise(idEntreprise);
            return documents;
        }

        //Recuperer document etudiant
        public static List<Document> recupererDocumentEtudiant(int idEtudiant)
        {
            List<Document> documents =
            documents = RequeteDocument.recupererDocumentEtudiant(idEtudiant);
            return documents;
        }


    }
}
