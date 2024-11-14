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
    using System;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;

    public class Helper
    {
        private string connectionString;

        public Helper(string connection)
        {
            this.connectionString = connection;
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
