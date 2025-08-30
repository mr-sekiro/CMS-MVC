using DataAccess.Data.DbContexts;
using DataAccess.Models.DepartmentModel;
using DataAccess.Repositories.Interfaces;


namespace DataAccess.Repositories.Classes
{
    public class DepartmentRepo(AppDbContext dbContext) : GenericRepo<Department>(dbContext),IDepartmentRepo
    {
    }
}
