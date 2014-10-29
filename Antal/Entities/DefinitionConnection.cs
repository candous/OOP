using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class DefinitionConnection {
        public static bool IsFile = true;

        public static String Serveur;
        public static String DataBase;
        public static String DocumentFolder;

        public static string[] lines ;
        private static string server ;
        private static string database ;
        public static string ConnectionStringCommun;

        public static void lireFichierConfiguration() {

            try {

                using (StreamReader reader = new StreamReader(@"config.antal"))
                {
                    reader.ReadToEnd();
                }
            } catch(FileNotFoundException) { 
                IsFile = false;
            }            

            if(IsFile) {
                lines = System.IO.File.ReadAllLines(@"config.antal");

                foreach(string line in lines) {
                    if(line.StartsWith("server")) {
                        Serveur = line.Substring(8);
                        server += Serveur;
                    }

                    if(line.StartsWith("database")) {
                        DataBase = line.Substring(10);
                        database = DataBase;
                    }

                    if(line.StartsWith("documents")) {
                        DocumentFolder = line.Substring(11);
                    }
                }

                ConnectionStringCommun = @"Data source=" + server + ";Initial catalog = " + database + "; Integrated security= true";
            }
        }
    }
}
