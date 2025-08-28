using BusinessLogic.Data_Transfer_Object;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Factories
{
    public static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department department)
        {
            return new DepartmentDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn != null
                ? DateOnly.FromDateTime(department.CreatedOn.Value)
                : null
            };
        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedBy = department.CreatedBy,
                DateOfCreation = department.CreatedOn != null
                ? DateOnly.FromDateTime(department.CreatedOn.Value)
                : null,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn != null
                ? DateOnly.FromDateTime(department.LastModifiedOn.Value)
                : null,
                IsDeleted = department.IsDeleted,
            };
        }

        public static Department ToEntity(this CreatedDepartmentDto createdDepartmentDto)
        {
            return new Department()
            {
                Name = createdDepartmentDto.Name,
                Code = createdDepartmentDto.Code,
                Description = createdDepartmentDto.Description,
                CreatedOn = createdDepartmentDto.DateOfCreation?.ToDateTime(new TimeOnly()),
            };
        }

        public static Department ToEntity(this UpdatedDepartmentDto updatedDepartmentDto)
        {
            return new Department()
            {
                Id = updatedDepartmentDto.Id,
                Name = updatedDepartmentDto.Name,
                Code = updatedDepartmentDto.Code,
                Description = updatedDepartmentDto.Description,
                CreatedOn = updatedDepartmentDto.DateOfCreation?.ToDateTime(new TimeOnly()),
            };
        }
    }
}
