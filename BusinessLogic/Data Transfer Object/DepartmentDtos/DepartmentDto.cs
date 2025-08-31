using DataAccess.Models.DepartmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data_Transfer_Object.DepartmentDtos
{
    public class DepartmentDto
    {
        public DepartmentDto(){}
        public DepartmentDto(Department department)
        {
            Id = department.Id;
            Name = department.Name;
            Code = department.Code;
            Description = department.Description;
            DateOfCreation = department.CreatedOn != null
            ? DateOnly.FromDateTime(department.CreatedOn.Value)
            : null;
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateOnly? DateOfCreation { get; set; }
    }
}
