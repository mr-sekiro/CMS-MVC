using DataAccess.Data.DbContexts;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        //private readonly IEmployeeRepo _employeeRepo;
        //private readonly IDepartmentRepo _departmentRepo;
        //private readonly AppDbContext dbContext;
        //public UnitOfWork(IDepartmentRepo departmentRepo, IEmployeeRepo employeeRepo, AppDbContext dbContext)
        //{
        //    _employeeRepo = employeeRepo;
        //    _departmentRepo = departmentRepo;
        //    this.dbContext = dbContext;
        //}
        //public IEmployeeRepo EmployeeRepo => _employeeRepo;
        //public IDepartmentRepo DepartmentRepo => _departmentRepo;

        //public int SaveChanges() => dbContext.SaveChanges();
        //public void Dispose() => dbContext.Dispose();

        //=============== Lazy Implementation ================//
        private readonly Lazy<IEmployeeRepo> _employeeRepo;
        private readonly Lazy<IDepartmentRepo> _departmentRepo;
        private readonly AppDbContext dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            _employeeRepo = new Lazy<IEmployeeRepo>(valueFactory: () => new EmployeeRepo(dbContext));
            _departmentRepo = new Lazy<IDepartmentRepo>(valueFactory: () => new DepartmentRepo(dbContext));
        }
        public IEmployeeRepo EmployeeRepo => _employeeRepo.Value;
        public IDepartmentRepo DepartmentRepo => _departmentRepo.Value;

        public int SaveChanges() => dbContext.SaveChanges();
        public void Dispose() => dbContext.Dispose();


    }
}
