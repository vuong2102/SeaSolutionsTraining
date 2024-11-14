using Microsoft.Data.SqlClient;
using SSTraining.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Service
{
    public interface ISaveable
    {
        void Save(SqlConnection connection);
    }
}
