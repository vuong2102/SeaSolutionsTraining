using Microsoft.EntityFrameworkCore;
using SSTraining.Model;
using SSTraining.Model.BaseModel;
using SSTraining.Service;
using SSTraining.Share;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        string connectionString = "Data Source=LAPTOP-CUA_VUON\\SQLEXPRESS;Initial Catalog=SeaSolTraining;User ID=sa;Password=21022002;encrypt=true;trustServerCertificate=true;";

        Helper helper = new Helper(connectionString);

        var cart = new Cart
        {
            Id = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            CustomerId = "7",
            TotalAmount = 100222,
        };
        var shoppingCarts = new List<ShoppingCart>
            {
                new ShoppingCart
                {
                    Id = Guid.NewGuid().ToString(),
                    Product_Id = "25",
                    Quantity = 10,
                    Cart_Id = cart.Id
                }
            };
        cart.ShoppingCarts = shoppingCarts;


        var nextOrderCode = helper.GetNextOrderCode();
        var order = new Order
        {
            Id = Guid.NewGuid().ToString(),
            OrderDate = DateTime.Today,
            TotalAmount = 5535353,
            CustomerId = "7",
            PaidAt = DateTime.Today,
            PaymentStatus = PaymentStatus.Unpaid,
            ShippingProviderId = "2",
            OrderCode = nextOrderCode,
            PaymentMethodId = "12",
            OverdueDate = DateTime.Now.AddDays(7),
            DeliveryStatus = DeliveryStatus.Checking
        };
        var orderProducts = new List<OrderProduct>
            {
                new OrderProduct
                {
                    Id = Guid.NewGuid().ToString(),
                    Product_Id = "10",
                    Quantity = 678,
                    Price = 7777,
                    Order_Id = order.Id,
                },
                new OrderProduct
                {
                    Id = Guid.NewGuid().ToString(),
                    Product_Id = "7",
                    Quantity = 899,
                    Price = 3333,
                    Order_Id = order.Id,
                }
            };
        order.OrderProducts = orderProducts;

        var commonService = new CommonService(connectionString);
        commonService.SaveEntity(cart);
        commonService.SaveEntity(order);

    }
}