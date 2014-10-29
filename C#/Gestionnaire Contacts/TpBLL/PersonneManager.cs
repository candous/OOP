using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpDAL;
using TpEntities;

namespace TpBLL
{
    public static class PersonneManager
    {
        public static int GetNbUsers()
        {
            return SQLHelper.GetNbUsers();
        }
       //methodes LOGIN
        public static Personne GetUserLogin(string courriel,string password)
        {
    	    return SQLHelper.GetUserLogin(courriel, password);
        }
       
        public static int UpdateLastVisit(DateTime now, int id)
        {
            return SQLHelper.UpdateLastVisit(now, id);
        }

        public static List<Personne> GetListePersonnesByListeContacts(List<Contact> listeContacts)
        {
            return SQLHelper.GetListePersonnesByListeContacts(listeContacts);
        }
        //methodes INSCRIPTION
        public static bool UserExistsByEmail(string courriel)
        {
            bool retour=false;
            int? i = SQLHelper.UserExistsByEmail(courriel);
            if (i != null)
               retour= true;

            return retour;
        }

        public static bool InsertUser(Personne personneInscrite)
        {
            return SQLHelper.InsertUser(personneInscrite);
        }


        public static bool ModifiertUser(Personne ContactModifier)
        {
            return SQLHelper.ModifierUser(ContactModifier);
        }

        public static bool SupprimerUser(Personne ContactSupprimer)
        {
            return SQLHelper.SupprimerUser(ContactSupprimer);
        }

        public static List<Personne> Recherher(string colonne, string parametre)
        {
            return SQLHelper.Rechercher(colonne, parametre);
        }
    }
}
