using System;
using System.Collections.Generic;

namespace Entities
{
    public class Etudiant
    {
        public List<Document> Documents { get; set; }
        public List<Communication> Communications { get; set; }
        public List<Entrevue> Entrevues { get; set; }
        public List<Entreprise> Entreprises { get; set; }
        public List<Stage> Stages { get; set; }
        public List<Langue> Langues { get; set; }
        public List<IdDescription> Interets { get; set; }
        public List<IdDescription> TechonologiesPreferees { get; set; }
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Courriel { get; set; }
        public DateTime? DateNaissance { get; set; }
        public DateTime DateFinFormation { get; set; }
        public int IdFormation { get; set; }
        public string PhotoURL { get; set; } 
        public int IdStatusCarriere { get; set; }
        public bool? Vehicule { get; set; }
        public bool? PermisConduire { get; set; }
        public string Experiences { get; set; }
        public bool? RiveNord { get; set; }
        public bool? RiveSud { get; set; }
        public bool? Montreal { get; set; }
        public double? SalaireEspere { get; set; }
        public string PosteDesire { get; set; }
        public int? IdStatusResidence { get; set; }
        public string Commentaire { get; set; }
        public bool Actif { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Telephone3 { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public Modification Modification { get; set;}
        public int StatusCarriere  { get; set; }
    }
}