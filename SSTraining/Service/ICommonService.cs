using SSTraining.Model;
using SSTraining.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Service
{
    public interface ICommonService
    {
        void Save(Order order);
        void Save(List<ShoppingCart> shoppingCarts);
    }
}
