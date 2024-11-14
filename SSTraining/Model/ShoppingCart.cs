using Microsoft.Data.SqlClient;
using SSTraining.Model.BaseModel;
using SSTraining.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Model
{
    public class ShoppingCart : BaseProductTransaction, ISaveable
    {
        public string Cart_Id { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public override void Save(SqlConnection connection)
        {
            string query = "INSERT INTO ShoppingCart (Id, Cart_Id, Product_Id, quantity) " +
                           "VALUES (@Id, @Cart_Id, @Product_Id, @Quantity)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
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
