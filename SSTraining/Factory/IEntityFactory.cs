using SSTraining.Config;
using SSTraining.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Factory
{
    public abstract class IEntityFactory
    {
        public abstract BaseEntity CreateBaseEntity();
        public abstract void SaveBaseEntity(DatabaseContext context, BaseEntity entity);
        public virtual BaseProductTransaction CreateBaseProductEntity() { return null; }
        public virtual void SaveBaseProductEntity(DatabaseContext context, BaseEntity entity) { }
    }
}
