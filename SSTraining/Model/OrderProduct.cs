using Microsoft.Data.SqlClient;
using SSTraining.Config;
using SSTraining.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Model
{
    public class OrderProduct : BaseProductTransaction
    {
        public string Order_Id { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public override void Save(DatabaseContext _dataContext)
        {
            string query = "INSERT INTO Order_Product (Id, order_id, product_id, quantity, price) " +
                           "VALUES (@Id, @Order_Id, @Product_Id, @Quantity, @Price)";
            using (SqlCommand cmd = new SqlCommand(query, _dataContext.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Order_Id", Order_Id);
                cmd.Parameters.AddWithValue("@Product_Id", Product_Id);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
                cmd.Parameters.AddWithValue("@Price", Price);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
