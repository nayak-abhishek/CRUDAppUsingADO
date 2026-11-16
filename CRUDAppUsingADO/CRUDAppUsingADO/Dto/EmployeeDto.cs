using System.ComponentModel.DataAnnotations;

namespace CRUDAppUsingADO.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string City { get; set; }
    }
}
