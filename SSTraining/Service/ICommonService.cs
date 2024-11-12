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
        void Save<T>(T entity) where T : class, ISaveable;

        void Save<T>(List<T> entities) where T : class, ISaveable;
    }

}
