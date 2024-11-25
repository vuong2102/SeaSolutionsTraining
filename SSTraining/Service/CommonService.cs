using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SSTraining.Model;
using SSTraining.Model.BaseModel;
using SSTraining.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Service
{
    public class CommonService
    {
        private static CommonService _instance;
        private static readonly object _lock = new object();
        private string connectionString;

        private CommonService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public static CommonService GetInstance(string connectionString)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CommonService(connectionString);
                    }
                }
            }
            return _instance;
        }

        public void SaveEntity(BaseEntity common)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                common.Save(connection);
                Console.WriteLine("Saved SuccessFully");
            }
        }

    }
        

}
