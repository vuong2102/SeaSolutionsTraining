using SSTraining.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Share
{
    public static class EntityFactory
    {
        public static Cart CreateCart(string customerId, decimal totalAmount, List<ShoppingCart> shoppingCarts)
        {
            var cartId = Guid.NewGuid().ToString();
            shoppingCarts.ForEach(c => c.Cart_Id = cartId);
            return new Cart
            {
                Id = cartId,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                CustomerId = customerId,
                TotalAmount = totalAmount,
                ShoppingCarts = shoppingCarts
            };
        }

        public static Order CreateOrder(string customerId, decimal totalAmount, List<OrderProduct> orderProducts, string orderCode)
        {
            var orderId = Guid.NewGuid().ToString();
            orderProducts.ForEach(c => c.Order_Id = orderId);
            return new Order
            {
                Id = orderId,
                OrderDate = DateTime.Today,
                TotalAmount = totalAmount,
                CustomerId = customerId,
                PaidAt = DateTime.Today,
                PaymentStatus = PaymentStatus.Unpaid,
                ShippingProviderId = "2",
                OrderCode = orderCode,
                PaymentMethodId = "12",
                OverdueDate = DateTime.Now.AddDays(7),
                DeliveryStatus = DeliveryStatus.Checking,
                OrderProducts = orderProducts
            };
        }
    }
}
