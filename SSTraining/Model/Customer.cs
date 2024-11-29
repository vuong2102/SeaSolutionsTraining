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
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsVip { get; set; }
        public Cart Cart { get; set; }

        public Customer()
        {
        }
        public override void Save(DatabaseContext _dbContext)
        {
        }
    }
}
