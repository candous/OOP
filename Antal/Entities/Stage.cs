using System;

namespace Entities
{
    public class Stage
    {
        public int Id {get; set;}
        public int IdEtudiant { get; set;}
        public int IdEntreprise { get; set; } 
        public DateTime DatePlacement { get; set;}
        public DateTime? DateDebut { get; set;}
        public DateTime? DateFin { get; set;}
        public string Commentaire { get; set;}
        public int? TypeStage {get; set;}
        public double? Salaire {get; set;}
        public bool? Retenu {get; set;}  // permet de savoir si l etudiant a ete embauché ou pas 3 états
        public bool Actif {get; set;}
        public Modification Modification {get; set;}
    }
}