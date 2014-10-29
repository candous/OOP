
using System;

namespace Entities
{
    public class Document
    {       
        public int Id { get; set; }
        public int IdProprietaire { get; set; }  
        public int? IdTypeDocument { get; set; }
        public DateTime? DateAjout { get; set; }
        public string CheminURL { get; set; }
        public string Titre { get; set; }
        public Modification Modification { get; set; }
    }

}