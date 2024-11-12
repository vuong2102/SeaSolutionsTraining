using SSTraining.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Model
{
    public class Order_Product : BaseProductTransaction
    {
        public string Order_Id { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public override void Save()
        {
            Console.WriteLine("Saved SuccessFully");
        }
    }
}
