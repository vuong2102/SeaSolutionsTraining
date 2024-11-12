using SSTraining.Model.BaseModel;
using SSTraining.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Model
{
    public class ShoppingCart : BaseProductTransaction, ISaveable
    {
        public string Cart_Id { get; set; }
        public Cart Cart { get; set; } // Liên kết với Cart
        public Product Product { get; set; }
        public override void Save()
        {
            Console.WriteLine("ShoppingCart saved successFully");
        }
    }
}
