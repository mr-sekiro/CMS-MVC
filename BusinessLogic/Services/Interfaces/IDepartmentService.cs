using BusinessLogic.Data_Transfer_Object.DepartmentDtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IDepartmentService
    {
        int AddDepartment(CreatedDepartmentDto createdDepartmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int UpdateDepartment(UpdatedDepartmentDto updatedDepartmentDto);
    }
}