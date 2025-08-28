using BusinessLogic.Data_Transfer_Object;
using BusinessLogic.Factories;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class DepartmentService(IDepartmentRepo departmentRepo) : IDepartmentService
    //Injection
    {
        // Get All Department
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {

            var departments = departmentRepo.GetAll();
            ////Manual Mapping:
            //var departmentsToReturn = departments.Select(D => new DepartmentDto()
            //{
            //    Id = D.Id,
            //    Name = D.Name,
            //    Code = D.Code,
            //    Description = D.Description,
            //    DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)
            //});

            ////Constructor Mapping
            //var departmentsToReturn = departments.Select(D => new DepartmentDto(D));

            //Extension Mapping

            return departments.Select(D => D.ToDepartmentDto()); ;
        }

        // Get Department By Id
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = departmentRepo.GetById(id);
            return department is null ? null : department.ToDepartmentDetailsDto();
        }

        // Create New Department
        public int AddDepartment(CreatedDepartmentDto createdDepartmentDto)
        {
            return departmentRepo.Add(createdDepartmentDto.ToEntity());
        }

        // Update Department
        public int UpdateDepartment(UpdatedDepartmentDto updatedDepartmentDto)
        {
            return departmentRepo.Update(updatedDepartmentDto.ToEntity());
        }

        // Delete Department
        public bool DeleteDepartment(int id)
        {
            var department = departmentRepo.GetById(id);
            if (department is null) return false;
            else
            {
                int result = departmentRepo.Remove(department);
                return result > 0 ? true : false;
            }
        }
    }
}
