using SSTraining.Config;
using SSTraining.Model.BaseModel;
using SSTraining.Model;
using SSTraining.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Factory
{
    public class OrderFactory : IEntityFactory
    {
        private readonly string _customerId;
        private readonly decimal _totalAmount;
        private readonly List<OrderProduct> _orderProducts;
        private readonly string _orderCode;

        public OrderFactory(string customerId, decimal totalAmount, List<OrderProduct> orderProducts, string orderCode)
        {
            _customerId = customerId;
            _totalAmount = totalAmount;
            _orderProducts = orderProducts;
            _orderCode = orderCode;
        }

        public override BaseEntity CreateBaseEntity()
        {
            var orderId = Guid.NewGuid().ToString();
            _orderProducts.ForEach(c => c.Order_Id = orderId);

            return new Order
            {
                Id = orderId,
                OrderDate = DateTime.Today,
                TotalAmount = _totalAmount,
                CustomerId = _customerId,
                PaidAt = DateTime.Today,
                PaymentStatus = PaymentStatus.Unpaid,
                ShippingProviderId = "2",
                OrderCode = _orderCode,
                PaymentMethodId = "12",
                OverdueDate = DateTime.Now.AddDays(7),
                DeliveryStatus = DeliveryStatus.Checking,
                OrderProducts = _orderProducts
            };
        }

        public override void SaveBaseEntity(DatabaseContext context, BaseEntity entity)
        {
            entity.Save(context);
        }
    }

}
