using Microsoft.Data.SqlClient;
using SSTraining.Model.BaseModel;
using SSTraining.Service;
using SSTraining.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SSTraining.Model
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderCode { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime? OverdueDate { get; set; }
        public string CustomerId { get; set; }
        public string ShippingProviderId { get; set; }
        public string PaymentMethodId { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        public override void Save(SqlConnection connection)
        {
            string query = "INSERT INTO [Order] (Id, OrderDate, TotalAmount, DeliveryStatus, OverdueDate, PaymentStatus, PaidAt, CustomerId, ShippingProviderId, PaymentMethodId, OrderCode) " +
                           "VALUES (@Id, @OrderDate, @TotalAmount, @DeliveryStatus, @OverdueDate, @PaymentStatus, @PaidAt, @CustomerId, @ShippingProviderId, @PaymentMethodId, @OrderCode)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@OrderDate", OrderDate);
                cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                cmd.Parameters.AddWithValue("@DeliveryStatus", DeliveryStatus);
                cmd.Parameters.AddWithValue("@OverdueDate", OverdueDate);
                cmd.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
                cmd.Parameters.AddWithValue("@PaidAt", PaidAt.HasValue ? (object)PaidAt.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
                cmd.Parameters.AddWithValue("@ShippingProviderId", ShippingProviderId);
                cmd.Parameters.AddWithValue("@PaymentMethodId", PaymentMethodId);
                cmd.Parameters.AddWithValue("@OrderCode", OrderCode);

                cmd.ExecuteNonQuery();
            }
            foreach (var item in OrderProducts)
            {
                item.Save(connection);
            }
        }

    }
}
