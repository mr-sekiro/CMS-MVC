
using DataAccess.Models.DepartmentModel;

namespace DataAccess.Repositories
{
    public interface IDepartmentRepo
    {
        IEnumerable<Department> GetAll(bool WithTracking = false);
        Department? GetById(int id);
        int Add(Department department);
        int Remove(Department department);
        int Update(Department department);
    }
}