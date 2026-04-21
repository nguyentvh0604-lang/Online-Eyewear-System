using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalStore.Repositories.Models
{
    [Table("payments")]
    public class Payment
    {
        [Key]
        [Column("payment_id")]
        public int PaymentId { get; set; }

        [Column("order_id")]
        [Required]
        public int OrderId { get; set; }

        [Column("method")]
        [Required]
        public string Method { get; set; } = "cod";

        [Column("status")]
        [Required]
        public string Status { get; set; } = "pending";

        [Column("amount")]
        [Required]
        public decimal Amount { get; set; }

        [Column("transaction_ref")]
        [MaxLength(200)]
        public string? TransactionRef { get; set; }

        [Column("paid_at")]
        public DateTime? PaidAt { get; set; }

        // Navigation
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
    }
}