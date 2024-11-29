using Microsoft.Data.SqlClient;
using SSTraining.Config;
using SSTraining.Model.BaseModel;
using SSTraining.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Model
{
    public class ShoppingCart : BaseProductTransaction
    {
        public string Cart_Id { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public override void Save(DatabaseContext _dbContext)
        {
            string query = "INSERT INTO ShoppingCart (Id, Cart_Id, Product_Id, quantity) " +
                           "VALUES (@Id, @Cart_Id, @Product_Id, @Quantity)";
            using (SqlCommand cmd = new SqlCommand(query, _dbContext.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Cart_Id", Cart_Id);
                cmd.Parameters.AddWithValue("@Product_Id", Product_Id);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
