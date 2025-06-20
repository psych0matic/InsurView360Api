namespace InsurView360Api.Models
{
    public class Claim
    {
        public string ClaimId { get; set; } = string.Empty;
        public string? PolicyId { get; set; } = string.Empty;
        public string? MemberId { get; set; } = string.Empty;
        public DateTime? ClaimDate { get; set; }
        public string? ClaimStatus { get; set; } = string.Empty;
        public Decimal? ClaimAmount { get; set; } = 0.0M;
        public string? ClaimReason { get; set; } = string.Empty;

    }
}
