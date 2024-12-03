using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SSTraining.Config;
using SSTraining.Factory;
using SSTraining.Model;
using SSTraining.Model.BaseModel;
using SSTraining.Service;
using SSTraining.Share;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

public class Program
{
    public static void Main()
    {
        string connectionString = "Data Source=LAPTOP-CUA_VUON\\SQLEXPRESS;Initial Catalog=SeaSolTraining;User ID=sa;Password=21022002;encrypt=true;trustServerCertificate=true;";
        var services = new ServiceCollection();
        services.AddSingleton<DatabaseContext>(provider => DatabaseContext.GetInstance(connectionString));
        services.AddScoped<CommonService>();
        services.AddScoped<Helper>();
        var serviceProvider = services.BuildServiceProvider();

        var commonService = serviceProvider.GetRequiredService<CommonService>();

        BaseEntity cart = IEntityFactory.CreateBaseEntity("cart");
        BaseEntity order = IEntityFactory.CreateBaseEntity("order");
        commonService.SaveBaseEntity(cart);
        commonService.SaveBaseEntity(order);
    }
}