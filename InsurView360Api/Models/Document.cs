namespace InsurView360Api.Models
{
    public class Document
    {
        public string DocumentId { get; set; } = string.Empty;
        public string? PolicyId { get; set; } = string.Empty;
        public string? MemberId { get; set; } = string.Empty;
        public string? DocumentType { get; set; } = string.Empty;
        public DateTime? DateUploaded { get; set; }
    }
}
