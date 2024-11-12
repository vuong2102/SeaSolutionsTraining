using SSTraining.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Model
{
    public class Cart : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }

    }
}
