using System;

namespace Entities
{
    public class Communication
    {        
        public int Id { get; set; }
        public int IdUtilisateur { get; set; }
        public int IdTo { get; set; }
        public DateTime DateCommunication { get; set; }
        public int? TypeCommunication { get; set; }
        public string Commentaire { get; set; }
        public int StatusCommunication { get; set; }       
    }
}