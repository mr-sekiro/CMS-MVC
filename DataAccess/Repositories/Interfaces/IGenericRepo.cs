using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll(bool WithTracking = false);
        T? GetById(int id);
        int Add(T entity);
        int Remove(T entity);
        int Update(T entity);
    }
}
