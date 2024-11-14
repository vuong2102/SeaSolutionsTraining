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
        private string connectionString;
        public CommonService(string connectionString)
        {
            this.connectionString = connectionString; 
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
