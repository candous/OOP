using System;
using System.Collections.Generic;

namespace Entities
{
    public class JourneeCarriere
    {
        
        public int Id { get; set; }
        public DateTime DateJourneeCarriere { get; set; }    
        public int nbEntreprise { get; set; }
        public int nbEntrepriseConfirme { get; set; }
        List<Entreprise> Entreprises {get; set; }
        List<Etudiant> Etudiants {get; set; }

    }
}