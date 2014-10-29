using System;
using System.Collections.Generic;

namespace Entities
{
    public class Entreprise
    {
        public List<Document> Documents { get; set; }
        public List<Communication> Communications { get; set; }
        public List<Entrevue> Entrevues { get; set; }
        public List<Representant> Representants { get; set; }
        public List<Etudiant> Etudiants {get; set; }
        public List<Stage> stages {get; set; }
        public List<IdDescription> FormationsRecherchees { get; set; }
        public List<IdDescription> TechnologiesRecherchees { get; set; }
        public List<IdDescription> InteretsRecherches { get; set; }
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Ville { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public string Telephone1 {get; set; }
        public string Telephone2 { get; set; }
        public string Telephone3 { get; set; }
        public string Secteur { get; set; }        
        public DateTime DateSaisie { get; set; }
        public string Commentaire { get; set; }
        public string ImageLogo { get; set; }
        public int? Langue { get; set; }
        public bool Actif { get; set; }
        public Modification Modification { get; set; }
    }
}