using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalStore.Repositories.Models
{
    [Table("shipments")]
    public class Shipment
    {
        [Key]
        [Column("shipment_id")]
        public int ShipmentId { get; set; }

        [Column("order_id")]
        [Required]
        public int OrderId { get; set; }

        [Column("handled_by")]
        public int? HandledBy { get; set; }

        [Column("carrier")]
        [MaxLength(100)]
        public string? Carrier { get; set; }

        [Column("tracking_number")]
        [MaxLength(200)]
        public string? TrackingNumber { get; set; }

        [Column("status")]
        [Required]
        public string Status { get; set; } = "preparing";

        [Column("shipped_at")]
        public DateTime? ShippedAt { get; set; }

        [Column("delivered_at")]
        public DateTime? DeliveredAt { get; set; }

        [Column("note")]
        public string? Note { get; set; }

        // Navigation
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
        [ForeignKey("HandledBy")]
        public virtual User? HandledByUser { get; set; }
    }
}