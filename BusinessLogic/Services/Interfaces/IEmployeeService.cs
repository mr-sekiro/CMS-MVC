using BusinessLogic.Data_Transfer_Object;
using BusinessLogic.Data_Transfer_Object.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        int AddEmployee(CreatedEmployeeDto createdEmployeeDto);
        bool DeleteEmployee(int id);
        IEnumerable<EmployeeDto> GetAllEmployees(string? searchName);
        EmployeeDetailsDto? GetEmployeeById(int id);
        int UpdateEmployee(UpdatedEmployeeDto updatedEmployeeDto);
    }
}
