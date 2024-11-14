using Microsoft.Data.SqlClient;
using SSTraining.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Model.BaseModel
{
    public abstract class BaseProductTransaction
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Product_Id { get; set; }

        public abstract void Save(SqlConnection connection);
    }
}
