using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll(bool WithTracking = false);
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<T, TResult>> selector);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        T? GetById(int id);
        int Add(T entity);
        int Remove(T entity);
        int Update(T entity);
    }
}
