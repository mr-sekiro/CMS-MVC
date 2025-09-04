using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepo EmployeeRepo { get; }
        public IDepartmentRepo DepartmentRepo { get; }

        int SaveChanges();
    }
}
