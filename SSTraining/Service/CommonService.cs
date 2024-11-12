using Microsoft.EntityFrameworkCore;
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
    public class CommonService : ICommonService
    {
        private readonly ApplicationDbContext _context;

        public CommonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save<T>(T entity) where T : class, ISaveable
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            Console.WriteLine($"{typeof(T).Name} saved successfully.");
        }

        public void Save<T>(List<T> entities) where T : class, ISaveable
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
            Console.WriteLine($"{typeof(T).Name} list saved successfully.");
        }
    }
}
