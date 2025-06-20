namespace InsurView360Api.Models
{
    public class Dependent
    {
        public string DependentId { get; set; } = string.Empty;
        public string? MemberId { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Relation { get; set; } = string.Empty;
    }
}
