namespace InsurView360Api.Models
{
    public class Payment
    {
        public string PaymentId { get; set; } = string.Empty;
        public string? PolicyId { get; set; } = string.Empty;
        public string? MemberId { get; set; } = string.Empty;
        public DateTime? PaymentDate { get; set; }
        public Decimal? Amount { get; set; } = 0.0M;
    }
}
