using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class SQLHelper
    {
        //creer des requetes dynamiques
        public static string DynamicSQL(Dictionary<string, string> WhereCondition, string SqlQuery)
        {
            int size = WhereCondition.Count;
            SqlQuery += " where actif=1 ";
            if (size > 0)  { 

                foreach (KeyValuePair<string, string> pair in WhereCondition)
                {
                    if (pair.Value != null && pair.Value.Length > 0)
                    SqlQuery += "and "+pair.Key + " like '%" + pair.Value + "%'";
                }
              //  SqlQuery = SqlQuery.Remove(SqlQuery.LastIndexOf("and"));
            }

            return SqlQuery;
        }
    }
}
