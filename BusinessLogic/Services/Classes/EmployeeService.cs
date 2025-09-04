using AutoMapper;
using BusinessLogic.Data_Transfer_Object.EmployeeDtos;
using BusinessLogic.Services.Interfaces;
using DataAccess.Data.DbContexts;
using DataAccess.Models.EmployeeModel;
using DataAccess.Repositories.Classes;
using DataAccess.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Classes
{
    //Auto Mapper
    //The object-to-object mapping library simplify the conversion of data between objects
    //  Maps properties with the same name automatically
    //  Supports complex mappings and value transformations
    //  Maps complex object graphs to simpler models
    public class EmployeeService(IUnitOfWork unitOfWork, IMapper mapper) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(string? searchName)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(searchName))
                employees = unitOfWork.EmployeeRepo.GetAll();
            else
                employees = unitOfWork.EmployeeRepo.GetAll(E => E.Name.ToLower().Contains(searchName.ToLower()));

            // Auto Mapper

            //src = Employee
            //dest = EmployeeDto
            var employeesDto = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            return employeesDto;
        }

        //public IEnumerable<EmployeeDto> GetAllEmployees()
        //{
        //    var employees = employeeRepo.GetAll(E => mapper.Map<Employee, EmployeeDto>(E));
        //    return employees;
        //}

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = unitOfWork.EmployeeRepo.GetById(id);
            return employee is null ? null : mapper.Map<EmployeeDetailsDto>(employee);

        }
        public int AddEmployee(CreatedEmployeeDto createdEmployeeDto)
        {
            var employee = mapper.Map<Employee>(createdEmployeeDto);
            unitOfWork.EmployeeRepo.Add(employee);
            return unitOfWork.SaveChanges();
        }
        public int UpdateEmployee(UpdatedEmployeeDto updatedEmployeeDto)
        {
            //var employee = mapper.Map<Employee>(updatedEmployeeDto);
            //return employeeRepo.Update(employee);

            var employee = unitOfWork.EmployeeRepo.GetById(updatedEmployeeDto.Id);
            if (employee is null)
            {
                return 0;
            }
            mapper.Map(updatedEmployeeDto, employee);
            unitOfWork.EmployeeRepo.Update(employee);
            return unitOfWork.SaveChanges();
        }
        public bool DeleteEmployee(int id)
        {
            //Soft delete
            var employee = unitOfWork.EmployeeRepo.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                unitOfWork.EmployeeRepo.Update(employee);
                return unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }
    }
}
