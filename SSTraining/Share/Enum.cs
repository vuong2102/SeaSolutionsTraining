using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Share
{
    public enum DeliveryStatus
    {
        [Description("Checking")]               // Kiểm hàng
        Checking,

        [Description("Delivered to Carrier")]   // Đã giao cho đơn vị vận chuyển
        DeliveredToCarrier,

        [Description("In Transit")]             // Đang vận chuyển
        InTransit,

        [Description("Out for Delivery")]       // Đang giao hàng
        OutForDelivery,

        [Description("Delivered")]              // Đã giao hàng
        Delivered,

        [Description("Canceled")]               // Hủy đơn hàng
        Canceled
    }

    public enum PaymentStatus
    {
        [Description("Paid")]
        Paid,               // Đã thanh toán

        [Description("Unpaid")]
        Unpaid,             // Chưa thanh toán
    }
}
