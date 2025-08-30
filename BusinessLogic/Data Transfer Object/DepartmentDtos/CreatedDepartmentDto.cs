using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data_Transfer_Object
{
    public class CreatedDepartmentDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateOnly? DateOfCreation { get; set; }
    }
}
