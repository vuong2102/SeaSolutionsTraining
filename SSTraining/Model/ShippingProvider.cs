using Microsoft.Data.SqlClient;
using SSTraining.Config;
using SSTraining.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Model
{
    public class ShippingProvider : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public override void Save(DatabaseContext _dbContext)
        { 
        }
    }
}
