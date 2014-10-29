using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BLL
{
    public static class ManagerEntrevue
    {
        public static List<Entrevue> recupererEntrevuesParIdEtudiant(int idEtudiant)
        {
            return RequeteEntrevue.recupererEntrevuesParIdEtudiant(idEtudiant);
        }

        public static List<Entrevue> recupererEntrevuesParIdEntreprise(int idEntreprise) {
            return RequeteEntrevue.recupererEntrevuesParIdEntreprise(idEntreprise);
        }

        public static bool ajouterEntrevue(Entrevue entrevue)
        {
            return RequeteEntrevue.ajouterEntrevue(entrevue);
        }

        public static Entrevue recupererEntrevueParId(int idEntrevue)
        {
            return RequeteEntrevue.recupererEntrevueParId(idEntrevue);
        }

        public static bool modifierEntrevue(Entrevue entrevue)
        {
            return RequeteEntrevue.modifierEntrevue(entrevue);
        }

        public static bool supprimerEntrevue(Entrevue entrevue)
        {
            return RequeteEntrevue.supprimerEntrevue(entrevue);
        }

        public static bool supprimerEntrevueParId(int idEntrevue)
        {
            return RequeteEntrevue.supprimerEntrevueParId(idEntrevue);
        }

       
    }
}
