using System;

namespace Entities
{
    public class Entrevue
    {
        public int Id { get; set; }
        public int IdEtudiant { get; set; }
        public int IdEntreprise { get; set; }
        public int? TypeEntrevue { get; set; }
        public int? Resultat { get; set; }
        public DateTime DateEntrevue { get; set; }
        public string Commentaire { get; set; }
        public bool Actif { get; set; }
        public Modification Modification { get; set; }
    }
}