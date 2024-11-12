using System;

public void ExecuteCreateOrderProcedure(DateTime overdueDate, string userId, DataTable items, string paymentMethodId, string shippingId)
{
    // Connection string
    string connectionString = "YourConnectionStringHere";

    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        using (SqlCommand cmd = new SqlCommand("CreateOrder", conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            // Add the parameters
            cmd.Parameters.Add(new SqlParameter("@OverdueDate", SqlDbType.Date) { Value = overdueDate });
            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.VarChar, 100) { Value = userId });
            cmd.Parameters.Add(new SqlParameter("@PaymentMethodId", SqlDbType.VarChar, 100) { Value = paymentMethodId });
            cmd.Parameters.Add(new SqlParameter("@Shippingid", SqlDbType.VarChar, 100) { Value = shippingId });

            // Add the table-valued parameter
            SqlParameter itemsParam = cmd.Parameters.AddWithValue("@Items", items);
            itemsParam.SqlDbType = SqlDbType.Structured;
            itemsParam.TypeName = "dbo.Order_Item_Pro"; // This should match the type name in SQL Server

            // Open the connection and execute the command
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
