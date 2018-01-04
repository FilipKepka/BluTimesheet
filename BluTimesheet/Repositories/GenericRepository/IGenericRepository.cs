using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BluTimesheet.Repositories
{
    public interface IGenericRepository<T> : IDisposable where T:class
    {
        T Get(object Id);
        T Add(T obj);
        void Remove(object Id);
        T Update(T obj);
        void Save();
        IEnumerable<T> GetAll();
        IEnumerable<T> Search(Expression<Func<T, bool>> filter);
    }
}
