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
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Data Source=LAPTOP-CUA_VUON\\SQLEXPRESS;Initial Catalog=SeaSolTraining;User ID=sa;Password=21022002;encrypt=true;trustServerCertificate=true;")
            .Options;
        using (var context = new ApplicationDbContext(options))
        {
            Helper helper = new Helper(context);
            var shoppingCarts = new List<ShoppingCart>
            {
                new ShoppingCart
                {
                    Id = Guid.NewGuid().ToString(),
                    Product_Id = "20",
                    Quantity = 5,
                }
            };

            var cart = new Cart
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                CustomerId = "1",
                TotalAmount = 100,
                ShoppingCarts = shoppingCarts
            };

            var orderProducts = new List<Order_Product>
            {
                new Order_Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Product_Id = "20",
                    Quantity = 455,
                    Price = 500
                },
                new Order_Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Product_Id = "2",
                    Quantity = 300,
                    Price = 5000,
                }
            };
            var nextOrderCode = helper.GetNextOrderCode();
            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                OrderDate = DateTime.Today,
                TotalAmount = 55000,
                CustomerId = "4",
                PaidAt = DateTime.Today,
                PaymentStatus = PaymentStatus.Unpaid,
                ShippingProviderId = "5",
                OrderCode = nextOrderCode,
                PaymentMethodId = "15",
                OverdueDate = DateTime.Now.AddDays(7),
                OrderProducts = orderProducts,
                DeliveryStatus = DeliveryStatus.Checking
            };


            var commonService = new CommonService(context);
            commonService.Save(cart);
            commonService.Save(order);
            
        }
            
    }
}