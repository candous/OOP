﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DefinitionConnection
    {



      //  private static string server = @"VIEWW7-2013-32\SQLEXPRESS";
        //private static string server = @"MOOGLOOCH-3";

        //private static string server = @"VIEWW7-2013-32\SQLEXPRESS";
       // private static string server = @"MOOGLOOCH-3";



       //private static string server = @"VIEWW7-2013-58\SQLEXPRESS";
        private static string server = @"VIEWW7-2013-4\SQLEXPRESS";

      // private static string server = @"VIEWW7-2013-58\SQLEXPRESS";



        private static string database = @"BD_antal";
        public static string ConnectionStringCommun = @"Data source=" + server + ";Initial catalog = " + database + "; Integrated security= true";
    }
}
