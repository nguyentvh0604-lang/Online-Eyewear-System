using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalStore.Repositories.Models
{
    [Table("return_requests")]
    public class ReturnRequest
    {
        [Key]
        [Column("return_id")]
        public int ReturnId { get; set; }

        [Column("order_id")]
        [Required]
        public int OrderId { get; set; }

        [Column("user_id")]
        [Required]
        public int UserId { get; set; }

        [Column("policy_id")]
        public int? PolicyId { get; set; }

        [Column("handled_by")]
        public int? HandledBy { get; set; }

        [Column("reason")]
        public string? Reason { get; set; }

        [Column("request_type")]
        [Required]
        public string RequestType { get; set; } = string.Empty;

        [Column("status")]
        [Required]
        public string Status { get; set; } = "pending";

        [Column("refund_amount")]
        public decimal? RefundAmount { get; set; } = 0;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("resolved_at")]
        public DateTime? ResolvedAt { get; set; }

        // Navigation
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        [ForeignKey("PolicyId")]
        public virtual Policy? Policy { get; set; }
        [ForeignKey("HandledBy")]
        public virtual User? HandledByUser { get; set; }
    }
}