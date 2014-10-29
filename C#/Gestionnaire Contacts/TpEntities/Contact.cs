using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpEntities
{
    public class Contact
    {
        public int IdUser { get; set; }
        public int IdContact { get; set; }
        public bool IsFavorite { get; set; }

        public static bool IsModified { get; set; }
    }
}
