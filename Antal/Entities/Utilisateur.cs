using System;

namespace Entities
{
    public class Utilisateur
    {
        public int Id { get; set;}
        public string Nom { get; set;}
        public string MotDePasse { get; set;}
        public int? IdTypeUtilisateur { get; set;}
        public bool? PeutLire { get; set;}
        public bool? PeutEcrire { get; set;}
        public bool? PeutCreerUtilisateur { get; set;}
        public Modification modification { get; set; }
    }
}