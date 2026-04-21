using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalStore.Repositories.Models
{
    [Table("order_items")]
    public class OrderItem
    {
        [Key]
        [Column("order_item_id")]
        public int OrderItemId { get; set; }

        [Column("order_id")]
        [Required]
        public int OrderId { get; set; }

        [Column("variant_id")]
        [Required]
        public int VariantId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; } = 1;

        [Column("unit_price")]
        [Required]
        public decimal UnitPrice { get; set; }

        [Column("subtotal")]
        [Required]
        public decimal Subtotal { get; set; }

        // Navigation
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
        [ForeignKey("VariantId")]
        public virtual ProductVariant? Variant { get; set; }
    }
}