namespace IFW.Models
{
    public class NewUserObject
    {
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string DepartmentName { get; set; }
        public required string IsManager { get; set; }
        public required string IsAdmin { get; set; }
        public required string EmploymentDate { get; set; }
        public required string Password { get; set; }
    }
}
