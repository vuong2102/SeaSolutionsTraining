using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSTraining.Share
{
    using SSTraining.Service;
    using System;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;

    public class Helper
    {
        private static Helper _instance;
        private static readonly object _lock = new object();
        private string connectionString;

        private Helper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public static Helper GetInstance(string connectionString)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Helper(connectionString);
                    }
                }
            }
            return _instance;
        }

        public string GetNextOrderCode()
        {
            string newOrderCode = "ORD0001";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string lastOrderCodeQuery = "SELECT TOP 1 OrderCode FROM [Order] ORDER BY OrderCode DESC";
                using (SqlCommand cmd = new SqlCommand(lastOrderCodeQuery, connection))
                {
                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string lastOrderCode = result.ToString();
                        string numericPart = Regex.Match(lastOrderCode, @"\d+").Value;

                        if (int.TryParse(numericPart, out int lastNumber))
                        {
                            lastNumber++;
                            newOrderCode = $"ORD{lastNumber:D4}";
                        }
                    }
                }
            }

            return newOrderCode;
        }
    }

}
