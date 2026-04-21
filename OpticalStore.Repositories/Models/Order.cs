using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalStore.Repositories.Models
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("user_id")]
        [Required]
        public int UserId { get; set; }

        [Column("prescription_id")]
        public int? PrescriptionId { get; set; }

        [Column("promotion_id")]
        public int? PromotionId { get; set; }

        [Column("order_type")]
        [Required]
        public string OrderType { get; set; } = "standard";

        [Column("status")]
        [Required]
        public string Status { get; set; } = "pending";

        [Column("total_amount")]
        public decimal TotalAmount { get; set; } = 0;

        [Column("discount_amount")]
        public decimal DiscountAmount { get; set; } = 0;

        [Column("final_amount")]
        public decimal FinalAmount { get; set; } = 0;

        [Column("shipping_address")]
        public string? ShippingAddress { get; set; }

        [Column("note")]
        public string? Note { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        [ForeignKey("PrescriptionId")]
        public virtual Prescription? Prescription { get; set; }
        [ForeignKey("PromotionId")]
        public virtual Promotion? Promotion { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual Payment? Payment { get; set; }
        public virtual Shipment? Shipment { get; set; }
        public virtual ICollection<ReturnRequest> ReturnRequests { get; set; } = new List<ReturnRequest>();
    }
}