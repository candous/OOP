using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpEntities
{
    public class Personne
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Nom { get; set; }
        public string Compagnie { get; set; }
        public string Celulaire { get; set; }
        public string Telephone { get; set; }
        public string SiteWeb { get; set; }
        public string Courriel { get; set; }
        public DateTime? Aniversaire { get; set; }
        public DateTime? LastVisit { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string Province { get; set; }
        public string Pays { get; set; }
        public string UrlPhoto { get; set; }
        public bool IsUser { get; set; }
        public bool IsVisible { get; set; }

        public bool IsFavorite {
            get;
            set;
        }

        public List<Personne> ListePersonnes { get; set; }
        public List<Contact> ListeContact { get; set; }
    }
}
