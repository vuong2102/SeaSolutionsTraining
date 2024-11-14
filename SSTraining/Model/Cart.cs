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
    public class Cart : BaseEntity, ISaveable
    {
        public decimal TotalAmount { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public override void Save(SqlConnection connection)
        {
            string query = "INSERT INTO Cart (Id, total_amount, created_at, updated_at, customer_id)" +
                           "VALUES (@Id, @TotalAmount, @CreatedAt, @UpdatedAt, @CustomerId)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                cmd.Parameters.AddWithValue("@CreatedAt", CreatedAt);
                cmd.Parameters.AddWithValue("@UpdatedAt", UpdatedAt.HasValue ? (object)UpdatedAt.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
                cmd.ExecuteNonQuery();
            }

            foreach (var item in ShoppingCarts)
            {
                item.Save(connection);
            }
        }

    }
}
