namespace InsurView360Api.Models
{
    public class Policy
    {
        public string PolicyId { get; set; } = string.Empty;
        public string? MemberId { get; set; } = string.Empty;
        public string? PolicyNumber { get; set; } = string.Empty;
        public string? PolicyType { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Status { get; set; } = string.Empty;
    }
}
