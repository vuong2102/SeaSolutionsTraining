using Microsoft.Data.SqlClient;
using SSTraining.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Model.BaseModel
{
    public abstract class BaseEntity
    {
        public virtual string Id { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public abstract void Save(DatabaseContext databaseContext);
    }
}
