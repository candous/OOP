using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpDAL;
using TpEntities;

namespace TpBLL
{
    public class ContactManager
    {
        public static List<Contact> GetListeContactsByUserId(int id)
        {
            return SQLHelper.GetListeContactsByUserId(id);
        }

        public static bool InsertContact(Contact contactAjoute)
        {
            return SQLHelper.InsertContact(contactAjoute);
        }
        //methode pour ajouter ou supprimer favoris
        public static bool ModifierContact(Contact contactTableModif)
        {
            return SQLHelper.ModifierContact(contactTableModif);
        }

        public static bool SupprimerContact(Contact contactSupprimer)
        {
            return SQLHelper.SupprimerContact(contactSupprimer);
        }
    }
}
