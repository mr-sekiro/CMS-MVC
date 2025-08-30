using DataAccess.Data.DbContexts;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Classes
{
    public class EmployeeRepo(AppDbContext dbContext) : GenericRepo<Employee>(dbContext), IEmployeeRepo
    {
    }
}
