using DataAccess.Data.DbContexts;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Classes
{
    public class GenericRepo<T>(AppDbContext dbContext) : IGenericRepo<T> where T : BaseEntity
        //Primary Constructor (.NET8 C#12)
    {
        //private readonly AppDbContext _dbContext;
        //public DepartmentRepo(AppDbContext dbContext) //Injection
        //{
        //    _dbContext = dbContext;
        //}
        // CRUD Operations

        //Get All
        public IEnumerable<T> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
            {
                return dbContext.Set<T>().ToList();
            }
            else
            {
                return dbContext.Set<T>().AsNoTracking().ToList();
            }
        }

        //Get By Id
        public T? GetById(int id) => dbContext.Set<T>().Find(id);

        //Update
        public int Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            return dbContext.SaveChanges();
        }

        //Delete
        public int Remove(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return dbContext.SaveChanges();
        }

        //Insert
        public int Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
            return dbContext.SaveChanges();
        }

    }
}
