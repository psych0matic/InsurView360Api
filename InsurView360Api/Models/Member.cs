namespace InsurView360Api.Models
{
    public class Member
    {
        public string MemberId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Employer { get; set; } = string.Empty;
        public string? EmploymentStatus { get; set; } = string.Empty;

    }
}
