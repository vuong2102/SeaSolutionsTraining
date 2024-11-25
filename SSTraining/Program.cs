using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        var services = new ServiceCollection();
        services.AddSingleton(provider => CommonService.GetInstance(connectionString));
        services.AddSingleton(provider => Helper.GetInstance(connectionString));
        var serviceProvider = services.BuildServiceProvider();

        var orderProducts = new List<OrderProduct>
            {
                new OrderProduct
                {
                    Id = Guid.NewGuid().ToString(),
                    Product_Id = "10",
                    Quantity = 678,
                    Price = 7777,
                },
                new OrderProduct
                {
                    Id = Guid.NewGuid().ToString(),
                    Product_Id = "7",
                    Quantity = 899,
                    Price = 3333,
                }
            };
        var shoppingCarts = new List<ShoppingCart>
            {
                new ShoppingCart
                {
                    Id = Guid.NewGuid().ToString(),
                    Product_Id = "25",
                    Quantity = 10,
                }
            };


        var commonService = serviceProvider.GetRequiredService<CommonService>(); 
        var helperService = serviceProvider.GetRequiredService<Helper>();
        var nextOrderCode = helperService.GetNextOrderCode();

        var cart = EntityFactory.CreateCart("7", 100222, shoppingCarts);
        var order = EntityFactory.CreateOrder("7", 5535353, orderProducts, nextOrderCode);

        commonService.SaveEntity(cart);
        commonService.SaveEntity(order);

    }
}