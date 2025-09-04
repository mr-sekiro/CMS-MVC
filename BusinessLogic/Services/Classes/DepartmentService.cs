using BusinessLogic.Data_Transfer_Object.DepartmentDtos;
using BusinessLogic.Factories;
using BusinessLogic.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Classes
{
    public class DepartmentService(IUnitOfWork unitOfWork) : IDepartmentService
    //Injection
    {
        // Get All Department
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {

            var departments = unitOfWork.DepartmentRepo.GetAll();
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
            var department = unitOfWork.DepartmentRepo.GetById(id);
            return department is null ? null : department.ToDepartmentDetailsDto();
        }

        // Create New Department
        public int AddDepartment(CreatedDepartmentDto createdDepartmentDto)
        {
            unitOfWork.DepartmentRepo.Add(createdDepartmentDto.ToEntity());
            return unitOfWork.SaveChanges();
        }

        // Update Department
        public int UpdateDepartment(UpdatedDepartmentDto updatedDepartmentDto)
        {
            unitOfWork.DepartmentRepo.Update(updatedDepartmentDto.ToEntity());
            return unitOfWork.SaveChanges();
        }

        // Delete Department
        public bool DeleteDepartment(int id)
        {
            var department = unitOfWork.DepartmentRepo.GetById(id);
            if (department is null) return false;
            else
            {
                unitOfWork.DepartmentRepo.Remove(department);
                int result = unitOfWork.SaveChanges();
                return result > 0 ? true : false;
            }
        }
    }
}
