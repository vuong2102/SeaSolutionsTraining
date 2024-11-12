using SSTraining.Model.BaseModel;
using SSTraining.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual ICollection<Order_Product> OrderProducts { get; set; }
    }
}
