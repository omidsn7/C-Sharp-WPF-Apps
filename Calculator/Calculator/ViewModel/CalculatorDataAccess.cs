using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Calculator
{
    public class CalculatorDataAccess
    {
        public static List<equation> LoadEquations()
        {
            using(IDbConnection cnn = new SQLiteConnection(ConnectionString()))
            {
                var output = cnn.Query<equation>("Select * from Equations", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveEquations(equation equation)
        {
            using (IDbConnection cnn = new SQLiteConnection(ConnectionString()))
            {
                cnn.Execute("insert into Equations (Equation) values (@Equation)", equation);
            }
        }

        private static string ConnectionString(string id = "Calculator")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
