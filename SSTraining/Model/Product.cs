using SSTraining.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Model
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ShopId { get; set; }

        public ICollection<Order_Product> OrderProducts { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
