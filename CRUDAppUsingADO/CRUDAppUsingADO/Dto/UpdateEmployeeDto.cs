namespace CRUDAppUsingADO.Dto
{
    public class UpdateEmployeeDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Designation { get; set; }
        public string? City { get; set; }
    }
}
