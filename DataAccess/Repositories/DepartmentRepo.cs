using DataAccess.Data.DbContexts;
using DataAccess.Models.DepartmentModel;


namespace DataAccess.Repositories
{
    public class DepartmentRepo(AppDbContext dbContext) : IDepartmentRepo
    //Primary Constructor (.NET8 C#12)
    {
        //private readonly AppDbContext _dbContext;
        //public DepartmentRepo(AppDbContext dbContext) //Injection
        //{
        //    _dbContext = dbContext;
        //}
        // CRUD Operations

        //Get All
        public IEnumerable<Department> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
            {
                return dbContext.Departments.ToList();
            }
            else
            {
                return dbContext.Departments.AsNoTracking().ToList();
            }
        }

        //Get By Id
        public Department? GetById(int id) => dbContext.Departments.Find(id);

        //Update
        public int Update(Department department)
        {
            dbContext.Departments.Update(department);
            return dbContext.SaveChanges();
        }

        //Delete
        public int Remove(Department department)
        {
            dbContext.Departments.Remove(department);
            return dbContext.SaveChanges();
        }

        //Insert
        public int Add(Department department)
        {
            dbContext.Departments.Add(department);
            return dbContext.SaveChanges();
        }

    }
}
