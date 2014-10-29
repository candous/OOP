using System;
using System.Collections.Generic;

namespace Entities
{
    public class Representant
    {
        public int Id { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Courriel { get; set; }
        public string Departement { get; set; }
        public string Poste { get; set; }
        public string Telephone1{get; set; }
        public string Telephone2{get; set; }
        public string Telephone3{get; set; }
        public int IdEntreprise{get; set; }
        public int? IdLangue{get; set; }
        public bool Actif{get; set; }
        public Modification Modification { get; set; }
    }
}