using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SSTraining.Config;
using SSTraining.Factory;
using SSTraining.Model;
using SSTraining.Model.BaseModel;
using SSTraining.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Service
{
    public class CommonService
    {
        private readonly DatabaseContext _dataContext;

        public CommonService(DatabaseContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void SaveBaseEntity(BaseEntity baseEntity)
        {
            baseEntity.Save(_dataContext);
            Console.WriteLine("Saved Successfully");
        }
    }
}
