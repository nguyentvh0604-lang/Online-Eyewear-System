using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalStore.Repositories.Models
{
    [Table("inventory")]
    public class Inventory
    {
        [Key]
        [Column("inventory_id")]
        public int InventoryId { get; set; }

        [Column("variant_id")]
        [Required]
        public int VariantId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; } = 0;

        [Column("reserved_qty")]
        public int ReservedQty { get; set; } = 0;

        [Column("last_updated")]
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        // Navigation
        [ForeignKey("VariantId")]
        public virtual ProductVariant? Variant { get; set; }
    }
}