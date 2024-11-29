using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Config
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        private static DatabaseContext _instance;

        private static readonly object _lock = new object();

        private DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static DatabaseContext GetInstance(string connectionString)
        {
            lock (_lock)  
            {
                if (_instance == null)
                {
                    _instance = new DatabaseContext(connectionString);
                }
            }
            return _instance;
        }

        public SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }


}
