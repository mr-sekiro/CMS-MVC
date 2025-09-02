using Azure;
using DataAccess.Data.DbContexts;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.Repositories.Classes
{

    //IEnumerable vs IQueryable in Data Retrieval
    //===========================================

    //1. Definition
    //-------------
    //IEnumerable<T>
    //- Namespace: System.Collections.Generic
    //- Used for in-memory iteration (for example: List, Array).
    //- Executes queries in memory (LINQ to Objects) after data is fetched from the database.

    //IQueryable<T>
    //- Namespace: System.Linq
    //- Designed for remote querying (for example: Database, Web API).
    //- Executes queries on the database (LINQ to Entities, LINQ to SQL) before bringing results into memory.


    //2. Execution
    //------------
    //IEnumerable
    //- Executes queries immediately when enumerated.
    //- Filtering, sorting, and joins happen after data is loaded into memory.

    //IQueryable
    //- Uses deferred execution. The query runs only when enumerated.
    //- Filtering, sorting, and joins are translated into SQL and executed on the database side.


    //3. Performance Impact
    //---------------------
    //IEnumerable
    //- Loads all matching data from the database into memory.
    //- Operations (filtering, sorting) happen in memory.
    //- Slower for large datasets because unnecessary data is loaded.

    //IQueryable
    //- Fetches only required data from the database.
    //- Operations are optimized at the database level.
    //- More efficient for large datasets.

    //Examples:
    //IEnumerable example (filter happens in memory)
    //var enumerableResult = context.Employees
    //                              .AsEnumerable()
    //                              .Where(e => e.Salary > 5000);
    //---------------------
    // IQueryable example (filter happens in SQL)
    //var queryableResult = context.Employees
    //                             .Where(e => e.Salary > 5000);

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
                return dbContext.Set<T>().Where(E => E.IsDeleted != true).ToList();
            }
            else
            {
                return dbContext.Set<T>().Where(E => E.IsDeleted != true).AsNoTracking().ToList();
            }
        }

        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<T, TResult>> selector)
        {
            return dbContext.Set<T>().Where(E => E.IsDeleted != true).Select(selector).ToList();
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
