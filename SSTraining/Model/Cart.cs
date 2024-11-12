using SSTraining.Model.BaseModel;
using SSTraining.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Model
{
    public class Cart : BaseEntity, ISaveable
    {
        public decimal TotalAmount { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }

    }
}
