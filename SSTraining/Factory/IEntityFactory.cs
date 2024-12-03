using SSTraining.Config;
using SSTraining.Model;
using SSTraining.Model.BaseModel;
using SSTraining.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Factory
{
    public abstract class IEntityFactory
    {
        public static BaseEntity CreateBaseEntity(string entity)
        {
            if (entity == "cart")
            {
                string id = Guid.NewGuid().ToString();
                return new Cart
                {
                    Id = id,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null,
                    CustomerId = "1",
                    TotalAmount = 190000,
                    ShoppingCarts = new List<ShoppingCart>
                    {
                        new ShoppingCart
                        {
                            Id = Guid.NewGuid().ToString(),
                            Product_Id = "25",
                            Quantity = 10,
                            Cart_Id = id
                        }
                    }
                };
            }
            else if (entity == "order")
            {
                string id = Guid.NewGuid().ToString();
                return new Order
                {
                    Id = id,
                    OrderDate = DateTime.Today,
                    TotalAmount = 125900,
                    CustomerId = "2",
                    PaidAt = DateTime.Today,
                    PaymentStatus = PaymentStatus.Unpaid,
                    ShippingProviderId = "2",
                    OrderCode = "ORD001234",
                    PaymentMethodId = "12",
                    OverdueDate = DateTime.Now.AddDays(7),
                    DeliveryStatus = DeliveryStatus.Checking,
                    OrderProducts = new List<OrderProduct>
                    {
                        new OrderProduct
                        {
                            Id = Guid.NewGuid().ToString(),
                            Product_Id = "10",
                            Quantity = 678,
                            Price = 7777,
                            Order_Id = id,
                        },
                        new OrderProduct
                        {
                            Id = Guid.NewGuid().ToString(),
                            Product_Id = "7",
                            Quantity = 899,
                            Price = 3333,
                            Order_Id = id
                        }
                    }
                };
            }
            throw new ArgumentException($"Unknown entity type: {entity}");
        }
    }
}
